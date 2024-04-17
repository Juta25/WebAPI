using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
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
        private readonly ITransientCounter _trcounter;
        private readonly IScopedCounter _sccounter;
        private readonly ScopedCounterService _sccounterService;
        private readonly ISingletonCounter _sincounter;

        // инициализация полей в конструкторе
        public HomeController(ILogger<HomeController> logger, IPizzaRepository repository, ITransientCounter trcounter, IScopedCounter sccounter, ScopedCounterService sccounterService, ISingletonCounter sincounter)
        {
            _logger = logger;
            _repository = repository;
            _trcounter = trcounter;
            _sccounter = sccounter;
            _sccounterService = sccounterService;
            _sincounter = sincounter;
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

        [HttpGet("GetTransientCounter", Name = "GetTransientCounter")]
        public IActionResult GetTransientCounter()
        {
            int randomValue = _trcounter.Value;
            return Ok(randomValue);
        }

        [HttpGet("GetScopedCounter", Name = "GetScopedCounter")]
        public IActionResult GetScopedCounter()
        {
            int randomValue = _sccounter.Value;
            int randomValue2 = _sccounterService.Value;
            return Ok(new
            {
                randomValue,
                randomValue2
            });
        }

        [HttpGet("GetSingletonCounter", Name = "GetSingletonCounter")]
        public IActionResult GetSingletonCounter()
        {
            int randomValue = _sincounter.Value;
            return Ok(randomValue);
        }

        [HttpGet("GetCounterValue", Name = "GetCounterValue")]
        public IActionResult GetCounterValue([FromServices] ITransientCounter transientCounter,
                                    [FromServices] IScopedCounter scopedCounter,
                                    [FromServices] ScopedCounterService scopedCounterService,
                                    [FromServices] ISingletonCounter singletonCounter)
        {
            int transientValue = transientCounter.Value;
            int scopedValue = scopedCounter.Value;
            int scopedService = scopedCounterService.Value;
            int singletonValue = singletonCounter.Value;

            var result = new
            {
                TransientValue = transientValue,
                ScopedValue = scopedValue,
                ScopedService = scopedService,
                SingletonValue = singletonValue
            };

            return Ok(result);
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
