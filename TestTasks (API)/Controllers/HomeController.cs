using Microsoft.AspNetCore.Mvc;
using TestTasks__API_.Domain.Core.Models;
using TestTasks__API_.Domain.Interfaces;

namespace TestTasks__API_.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPizzaRepository _repository;

        // инициализация полей в конструкторе
        public HomeController(ILogger<HomeController> logger, IPizzaRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }


        [HttpGet("GetPizzas", Name = "GetPizzas")]  //маршрут для HttpGet и наименование маршрута для использования в других частях кода
        public IActionResult GetPizzas()
        {
            List<PizzaModel> products = _repository.GetAll();
            return Ok(products);
        }

        [HttpGet("GetPizzasBySearch", Name = "GetPizzasBySearch")]
        public IActionResult GetPizzasBySearch(string searchString)
        {
            var products = _repository.PizzaSearch(searchString);
            return Ok(products);
        }

        [HttpGet("GetPizzaById", Name = "GetPizzaById")]
        public IActionResult GetPizzaById(int id)
        {
            var pizza = _repository.FindById(id);
            return Ok(pizza);
        }

        [HttpGet("CheckExceptionsGet", Name = "CheckExceptionsGet")]
        public IActionResult CheckExceptionsGet()
        {
            return Ok();
        }

        [HttpPost("CheckExceptions", Name = "CheckExceptions")]
        public IActionResult CheckExceptions(int pizzaId)
        {
            try
            {
                var result = _repository.FindByIdException(pizzaId);
                //return View("CheckExceptions", result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"A change occurred while trying to get information about the pizza with ID {pizzaId} in the CheckExceptions method"); // Записываем исключение

                return StatusCode(StatusCodes.Status404NotFound, "Error code: " + StatusCodes.Status404NotFound + ". " + ex.Message);
            }
        }

        [HttpGet("CreateUpdate", Name = "CreateUpdate")]
        public IActionResult CreateUpdate(int? id)
        {
            PizzaModel pizzaModel = new PizzaModel();
            if (id.HasValue)
            {
                pizzaModel = _repository.FindById(id.Value);
            }
            return Ok(pizzaModel);
        }

        //[HttpPut("CreateUpdate", Name = "CreateUpdate")]
        [HttpPost("CreateUpdate", Name = "CreateUpdate")]
        public IActionResult CreateUpdate(PizzaModel pizza)
        {
            if (ModelState.IsValid)
            {
                if (pizza.Id != 0)
                {
                    _repository.Update(pizza);
                }
                else
                {
                    _repository.Create(pizza);
                }
            }
            return Ok(pizza);
        }

        //[HttpDelete("DeletePizza", Name = "DeletePizza")]
        [HttpPost("DeletePizza", Name = "DeletePizza")]
        public IActionResult DeletePizza(int id)
        {
            _repository.Delete(id);
            return RedirectToAction("Index", "Home");
        }
    }
}
