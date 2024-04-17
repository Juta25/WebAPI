using TestTasks__API_.Domain.Core.Models;
using TestTasks__API_.Domain.Interfaces;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TestTasks__API_
{
    public class PizzaRepository : IPizzaRepository
    {
        private readonly MankovaJV_TaskContext _bd;
        public PizzaRepository(MankovaJV_TaskContext bd)
        {
            _bd = bd;
        }

        public async Task<List<PizzaModel>> GetAllAsync()
        {
            List<PizzaModel> products = await _bd.Pizzas
                .Select(list => new PizzaModel()
                {
                    Id = list.Id,
                    Image = list.Image,
                    Name = list.Name,
                    Description = list.Description,
                    Weight = list.Weight,
                    Price = list.Price,
                })
                .ToListAsync();
            return products;
        }

        public async Task<List<PizzaModel>> PizzaSearchAsync(string searchString)
        {
            List<PizzaModel> products = await _bd.Pizzas
               .Where(p => p.Name.ToLower().Contains(searchString.ToLower()) || p.Description.ToLower().Contains(searchString.ToLower()))
               .Select(p => new PizzaModel
               {
                   Id = p.Id,
                   Image = p.Image,
                   Name = p.Name,
                   Description = p.Description,
                   Weight = p.Weight,
                   Price = p.Price,
               }).ToListAsync();
            return products;
        }

        public async Task<PizzaModel> FindByIdAsync(int id)
        {
            var defaultProduct = await _bd.Pizzas
                .Where(p => p.Id == id)
                .Select(p => new PizzaModel
                {
                    Id = p.Id,
                    Image = p.Image,
                    Name = p.Name,
                    Description = p.Description,
                    Weight = p.Weight,
                    Price = p.Price
                }).FirstOrDefaultAsync();
            return defaultProduct;
        }

        public async Task<PizzaModel> FindByIdExceptionAsync(int id)
        {
            var defaultProduct = await _bd.Pizzas
                .Where(p => p.Id == id)
                .Select(p => new PizzaModel
                {
                    Id = p.Id,
                    Image = p.Image,
                    Name = p.Name,
                    Description = p.Description,
                    Weight = p.Weight,
                    Price = p.Price
                }).FirstOrDefaultAsync();

            if (defaultProduct == null)
            {
                throw new InvalidOperationException($"Произошло исключение при попытке получить информацию о пицце с ID {id} в методе FindByIdException");
            }

            return defaultProduct;
        }

        public async Task CreateAsync(PizzaModel pizza)
        {
            var newPizza = new Pizza
            {
                Name = pizza.Name,
                Image = pizza.Image,
                Description = pizza.Description,
                Weight = pizza.Weight,
                Price = pizza.Price
            };
            _bd.Pizzas.Add(newPizza);
            await _bd.SaveChangesAsync();
        }

        public async Task UpdateAsync(PizzaModel pizza)
        {
            var existingPizza = await _bd.Pizzas.FindAsync(pizza.Id);
            if (existingPizza != null)
            {
                existingPizza.Name = pizza.Name;
                existingPizza.Image = pizza.Image;
                existingPizza.Description = pizza.Description;
                existingPizza.Weight = pizza.Weight;
                existingPizza.Price = pizza.Price;
                await _bd.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int pizzaId)
        {
            var pizzaToDelete = await _bd.Pizzas.FindAsync(pizzaId);
            if (pizzaToDelete != null)
            {
                _bd.Pizzas.Remove(pizzaToDelete);
                await _bd.SaveChangesAsync();
            }
        }
    }
}