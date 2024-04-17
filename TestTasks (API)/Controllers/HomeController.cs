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
        private readonly TransientCounterService _trcounterService;
        private readonly IScopedCounter _sccounter;
        private readonly ScopedCounterService _sccounterService;
        private readonly ISingletonCounter _sincounter;

        // инициализация полей в конструкторе
        public HomeController(ILogger<HomeController> logger, IPizzaRepository repository, ITransientCounter trcounter, TransientCounterService trcounterService, IScopedCounter sccounter, ScopedCounterService sccounterService, ISingletonCounter sincounter)
        {
            _logger = logger;
            _repository = repository;
            _trcounter = trcounter;
            _trcounterService = trcounterService;
            _sccounter = sccounter;
            _sccounterService = sccounterService;
            _sincounter = sincounter;
        }


        [HttpGet("GetPizzas", Name = "GetPizzas")]
        public async Task<IActionResult> GetPizzas()
        {
            List<PizzaModel> products = await _repository.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("GetPizzasBySearch", Name = "GetPizzasBySearch")]

        public async Task<IActionResult> GetPizzasBySearch(string searchString)
        {
            var products = await _repository.PizzaSearchAsync(searchString);
            return Ok(products);
        }

        [HttpGet("GetTransientCounter", Name = "GetTransientCounter")]
        public async Task<IActionResult> GetTransientCounter()
        {
            int randomValue = await _trcounter.GetValueAsync();
            int randomValue2 = await _trcounterService.GetValueAsync();
            return Ok(new
            {
                randomValue,
                randomValue2
            });
        }

        [HttpGet("GetScopedCounter", Name = "GetScopedCounter")]
        public async Task<IActionResult> GetScopedCounter()
        {
            int randomValue = await _sccounter.GetValueAsync();
            int randomValue2 = await _sccounterService.GetValueAsync();
            return Ok(new
            {
                randomValue,
                randomValue2
            });
        }

        [HttpGet("GetSingletonCounter", Name = "GetSingletonCounter")]
        public async Task<IActionResult> GetSingletonCounter()
        {
            int randomValue = await _sincounter.GetValueAsync();
            return Ok(randomValue);
        }

        [HttpGet("GetCounterValue", Name = "GetCounterValue")]
        public async Task<IActionResult> GetCounterValue([FromServices] ITransientCounter transientCounter,
                                    [FromServices] TransientCounterService transientCounterService,
                                    [FromServices] IScopedCounter scopedCounter,
                                    [FromServices] ScopedCounterService scopedCounterService,
                                    [FromServices] ISingletonCounter singletonCounter)
        {
            int transientValue = await transientCounter.GetValueAsync();
            int transientService = await transientCounterService.GetValueAsync();
            int scopedValue = await scopedCounter.GetValueAsync();
            int scopedService = await scopedCounterService.GetValueAsync();
            int singletonValue = await singletonCounter.GetValueAsync();

            var result = new
            {
                TransientValue = transientValue,
                TransientService = transientService,
                ScopedValue = scopedValue,
                ScopedService = scopedService,
                SingletonValue = singletonValue
            };

            return Ok(result);
        }


        [HttpGet("GetPizzaById", Name = "GetPizzaById")]
        public async Task<IActionResult> GetPizzaById(int id)
        {
            var pizza = await _repository.FindByIdAsync(id);
            return Ok(pizza);
        }

        [HttpGet("CheckExceptionsGet", Name = "CheckExceptionsGet")]
        public async Task<IActionResult> CheckExceptionsGet()
        {
            return Ok();
        }

        [HttpPost("CheckExceptions", Name = "CheckExceptions")]
        public async Task<IActionResult> CheckExceptions(int pizzaId)
        {
            try
            {
                var result = await _repository.FindByIdExceptionAsync(pizzaId);
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
        public async Task<IActionResult> CreateUpdate(int? id)
        {
            PizzaModel pizzaModel = new PizzaModel();
            if (id.HasValue)
            {
                pizzaModel = await _repository.FindByIdAsync(id.Value);
            }
            return Ok(pizzaModel);
        }

        //[HttpPut("CreateUpdate", Name = "CreateUpdate")]
        [HttpPost("CreateUpdate", Name = "CreateUpdate")]
        public async Task<IActionResult> CreateUpdate(PizzaModel pizza)
        {
            if (ModelState.IsValid)
            {
                if (pizza.Id != 0)
                {
                    await _repository.UpdateAsync(pizza);
                }
                else
                {
                    await _repository.CreateAsync(pizza);
                }
            }
            return Ok(pizza);
        }

        //[HttpDelete("DeletePizza", Name = "DeletePizza")]
        [HttpPost("DeletePizza", Name = "DeletePizza")]
        public async Task<IActionResult> DeletePizza(int id)
        {
            await _repository.DeleteAsync(id);
            return Ok();
        }
    }
}