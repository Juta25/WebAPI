using TestTasks__API_.Domain.Core.Models;
using TestTasks__API_.Domain.Interfaces;

namespace TestTasks__API_
{
    public class PizzaRepository : IPizzaRepository
    {
        private readonly MankovaJV_TaskContext _bd;
        public PizzaRepository(MankovaJV_TaskContext bd)
        {
            _bd = bd;
        }

        public List<PizzaModel> GetAll()
        {
            List<PizzaModel> products = new List<PizzaModel>();
            using (MankovaJV_TaskContext bd = new MankovaJV_TaskContext()) // работа с базой
            {
                products = bd.Pizzas.Select(list => new PizzaModel()
                {
                    Id = list.Id,
                    Image = list.Image,
                    Name = list.Name,
                    Description = list.Description,
                    Weight = list.Weight,
                    Price = list.Price,
                }).ToList();
            }
            return products;
        }

        public PizzaModel FindById(int id)
        {
            List<PizzaModel> product = new List<PizzaModel>();
            using (MankovaJV_TaskContext bd = new MankovaJV_TaskContext()) // работа с базой
            {
                product = bd.Pizzas.Select(list => new PizzaModel()
                {
                    Id = list.Id,
                    Image = list.Image,
                    Name = list.Name,
                    Description = list.Description,
                    Weight = list.Weight,
                    Price = list.Price,
                }).ToList();
            }

            var defaultProduct = product.FirstOrDefault(f => f.Id == id);
            return defaultProduct;
        }

        public PizzaModel FindByIdException(int id)
        {
            List<PizzaModel> product = new List<PizzaModel>();
            using (MankovaJV_TaskContext bd = new MankovaJV_TaskContext()) // работа с базой
            {
                product = bd.Pizzas.Select(list => new PizzaModel()
                {
                    Id = list.Id,
                    Image = list.Image,
                    Name = list.Name,
                    Description = list.Description,
                    Weight = list.Weight,
                    Price = list.Price,
                }).ToList();
            }

            var defaultProduct = product.FirstOrDefault(f => f.Id == id);

            if (defaultProduct == null)
            {
                throw new InvalidOperationException($"Произошло исключение при попытке получить информацию о пицце с ID {id} в методе FindByIdException");
            }
            return defaultProduct;
        }

        public void Create(PizzaModel pizza)
        {
            using (MankovaJV_TaskContext bd = new MankovaJV_TaskContext())
            {
                var newPizza = new Pizza
                {
                    Name = pizza.Name,
                    Image = pizza.Image,
                    Description = pizza.Description,
                    Weight = pizza.Weight,
                    Price = pizza.Price
                };
                bd.Pizzas.Add(newPizza);
                bd.SaveChanges();
            }
        }

        public void Update(PizzaModel pizza)
        {
            using (MankovaJV_TaskContext bd = new MankovaJV_TaskContext())
            {
                var existingPizza = bd.Pizzas.Find(pizza.Id);
                if (existingPizza != null)
                {
                    existingPizza.Name = pizza.Name;
                    existingPizza.Image = pizza.Image;
                    existingPizza.Description = pizza.Description;
                    existingPizza.Weight = pizza.Weight;
                    existingPizza.Price = pizza.Price;
                    bd.SaveChanges();
                }
            }
        }

        public void Delete(int pizzaId)
        {
            using (MankovaJV_TaskContext db = new MankovaJV_TaskContext())
            {
                var pizzaToDelete = db.Pizzas.Find(pizzaId);
                if (pizzaToDelete != null)
                {
                    db.Pizzas.Remove(pizzaToDelete);
                    db.SaveChanges();
                }
            }
        }
    }
}