using TestTasks__API_.Domain.Core.Models;

namespace TestTasks__API_.Domain.Interfaces
{
    public interface IPizzaRepository
    {
        Task<List<PizzaModel>> GetAllAsync();
        Task<List<PizzaModel>> PizzaSearchAsync(string searchString);
        Task<PizzaModel> FindByIdAsync(int id);
        Task<PizzaModel> FindByIdExceptionAsync(int id);
        Task CreateAsync(PizzaModel pizza);
        Task UpdateAsync(PizzaModel pizza);
        Task DeleteAsync(int pizzaId);
    }
}