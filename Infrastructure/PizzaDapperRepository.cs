using TestTasks__API_.Domain.Core.Models;
using TestTasks__API_.Domain.Interfaces;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;


namespace TestTasks__API_
{
    public class PizzaDapperRepository : IPizzaRepository
    {
        private readonly DapperContext _context;
        protected IDbConnection _db => _context.CreateConnection();

        public PizzaDapperRepository(DapperContext dpContext)
        {
            _context = dpContext;
        }

        public async Task<List<PizzaModel>> GetAllAsync()
        {
            var pizzas = await _db.QueryAsync<Pizza>("SELECT * FROM Pizza");
            return pizzas.Select(p => new PizzaModel(p.Id, p.Image, p.Name, p.Description, p.Weight, p.Price)).ToList();
        }


        public async Task<List<PizzaModel>> PizzaSearchAsync(string searchString)
        {
            var query = "SELECT * FROM Pizzas WHERE Name LIKE @searchString OR Description LIKE @searchString";
            var products = await _db.QueryAsync<PizzaModel>(query, new { searchString = $"%{searchString}%" });
            return products.ToList();
        }

        public async Task<PizzaModel> FindByIdAsync(int id)
        {
            var query = "SELECT * FROM Pizzas WHERE Id = @Id";
            var products = await _db.QueryFirstOrDefaultAsync<PizzaModel>(query, new { id });
            return products;
        }

        public async Task<PizzaModel> FindByIdExceptionAsync(int id)
        {
            var query = "SELECT * FROM Pizzas WHERE Id = @Id";
            var defaultProduct = await _db.QueryFirstOrDefaultAsync<PizzaModel>(query, new { id });

            if (defaultProduct == null)
            {
                throw new InvalidOperationException($"Произошло исключение при попытке получить информацию о пицце с ID {id} в методе FindByIdException");
            }

            return defaultProduct;
        }

        public async Task CreateAsync(PizzaModel pizza)
        {
            var query = "INSERT INTO Pizzas (Name, Image, Description, Weight, Price) VALUES (@Name, @Image, @Description, @Weight, @Price)";

            await _db.ExecuteAsync(query, new
            {
                pizza.Name,
                pizza.Image,
                pizza.Description,
                pizza.Weight,
                pizza.Price
            });
        }

        public async Task UpdateAsync(PizzaModel pizza)
        {
            var query = "UPDATE Pizzas SET Name = @Name, Image = @Image, Description = @Description, Weight = @Weight, Price = @Price WHERE Id = @Id";
            await _db.ExecuteAsync(query, new
            {
                pizza.Name,
                pizza.Image,
                pizza.Description,
                pizza.Weight,
                pizza.Price,
                pizza.Id
            });
        }

        public async Task DeleteAsync(int pizzaId)
        {
            var query = "DELETE FROM Pizzas WHERE Id = @Id";
            await _db.ExecuteAsync(query, new { Id = pizzaId });
        }
    }
}