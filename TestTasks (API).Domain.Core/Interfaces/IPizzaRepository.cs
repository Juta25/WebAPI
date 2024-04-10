using TestTasks__API_.Domain.Core.Models;

namespace TestTasks__API_.Domain.Interfaces
{
    public interface IPizzaRepository
    {
        public List<PizzaModel> GetAll();
        public PizzaModel FindById(int id);
        public PizzaModel FindByIdException(int id);
        public void Create(PizzaModel pizza);
        public void Update(PizzaModel pizza);
        public void Delete(int pizzaId);
    }
}