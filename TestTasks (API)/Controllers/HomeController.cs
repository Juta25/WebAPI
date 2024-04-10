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

        // Объединяем два конструктора в один
        public HomeController(ILogger<HomeController> logger, IPizzaRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }


        [HttpGet("Index", Name = "Index")]  //маршрут для HttpGet и наименование маршрута для использования в других частях кода
        public IActionResult Index()
        {
            List<PizzaModel> products = _repository.GetAll();
            return Ok(products);
        }

        [HttpGet("Privacy", Name = "Privacy")]
        public IActionResult Privacy()
        {
            return Ok();
        }

        [HttpGet("GetPizzas", Name = "GetPizzas")]
        public IActionResult GetPizzas()
        {
            var pizzas = new List<PizzaModel>
            {
                new PizzaModel
                {
                    Id = 1,
                    Name = "Новогодняя",
                    Image = "P1.jpg",
                    Description = "Соус 'Гавайский', Сыр моцарелла, Куриная грудка, Мандарины консервированные, Стружка миндаля, Кокосовая стружка",
                    Weight = 670,
                    Price = 470
                },
                new PizzaModel
                {
                    Id = 2,
                    Name = "Capriccio",
                    Image = "P2.jpg",
                    Description = "Сыр моцарелла, Соус 'Барбекю', Соус 'Кальяри', Пепперони, Овощи гриль, Бекон, Ветчина, Томаты черри, Шампиньоны",
                    Weight = 980,
                    Price = 600
                },
                new PizzaModel
                {
                    Id = 3,
                    Name = "XXXL",
                    Image = "P3.jpg",
                    Description = "Сыр моцарелла, Соус '1000 островов', Куриный рулет, Ветчина, Колбаски охотничьи, Бекон, Сервелат, Огурцы маринованные, Томаты черри, Маслины, Лук маринованный",
                    Weight = 740,
                    Price = 510
                },
                new PizzaModel
                {
                    Id = 4,
                    Name = "4 вкуса",
                    Image = "P4.jpg",
                    Description = "Соус '1000 островов', Сыр моцарелла, Рулет куриный, Ветчина, Пепперони, Сыр пармезан, Шампиньоны, Томаты свежие, Маслины/оливки",
                    Weight = 540,
                    Price = 450
                },
                new PizzaModel
                {
                    Id = 5,
                    Name = "Амазонка",
                    Image = "P5.jpg",
                    Description = "Соус 'Томатный', Сыр моцарелла, Куриная грудка, Брокколи, Огурцы маринованные, Перец болгарский, Шампиньоны, Томаты черри, Маслины, Лук маринованный",
                    Weight = 600,
                    Price = 450
                },
                new PizzaModel
                {
                    Id = 6,
                    Name = "БананZZа",
                    Image = "P6.jpg",
                    Description = "Бананы, Соус 'Гавайский', Сыр моцарелла, Ананас, Шоколад молочный, Кокос/миндаль, Топпинг клубничный",
                    Weight = 520,
                    Price = 420
                },
                new PizzaModel
                {
                    Id = 7,
                    Name = "Барбекю",
                    Image = "P7.jpg",
                    Description = "Соус 'Томатный', Сыр моцарелла, Ветчина, Бекон, Пепперони, Соус 'Барбекю', Томаты, Перец болгарский, Лук маринованный",
                    Weight = 590,
                    Price = 450
                },
                new PizzaModel
                {
                    Id = 8,
                    Name = "Буритто",
                    Image = "P8.jpg",
                    Description = "Соус 'Томатный острый', Сыр моцарелла, Куриная грудка, Кукуруза, Фасоль консервированная, Соус сырный 'Пармеджано', Перец болгарскийX, Лук маринованный",
                    Weight = 670,
                    Price = 450
                },
                new PizzaModel
                {
                    Id = 9,
                    Name = "Гавайская",
                    Image = "P9.jpg",
                    Description = "Ветчина, Соус 'Гавайский', Сыр моцарелла, Ананас, Перец болгарский",
                    Weight = 550,
                    Price = 450
                },
                new PizzaModel
                {
                    Id = 10,
                    Name = "Гавайская Premium",
                    Image = "P10.jpg",
                    Description = "Соус 'Гавайский', Сыр моцарелла, Ананас, Ветчина, Куриный рулет, Кукуруза, Перец болгарский",
                    Weight = 590,
                    Price = 470
                },
                new PizzaModel
                {
                    Id = 11,
                    Name = "Греческая",
                    Image = "P11.jpg",
                    Description = "Соус 'Кальяри', Сыр моцарелла, Сливочный сыр, Брокколи, Томаты черри, Перец болгарский, Маслины",
                    Weight = 570,
                    Price = 480
                },
                new PizzaModel
                {
                    Id = 12,
                    Name = "Грибная",
                    Image = "P12.jpg",
                    Description = "Соус 'Грибной', Сыр моцарелла, Опята маринованные, Укроп, Шампиньоны, Лук маринованный",
                    Weight = 550,
                    Price = 450
                }
                };

            //return Json(pizzas);
            return Ok(pizzas); //вместо Json
        }

        [HttpGet("Detail", Name = "Detail")]
        public IActionResult Detail(int id)
        {
            MankovaJV_TaskContext context = new MankovaJV_TaskContext();
            PizzaRepository repository = new PizzaRepository(context);
            var products = repository.FindById(id);

            return Ok(products);
        }

        [HttpGet("IndexNew", Name = "IndexNew")]
        public IActionResult IndexNew()
        {
            MankovaJV_TaskContext context = new MankovaJV_TaskContext();
            PizzaRepository repository = new PizzaRepository(context);
            List<PizzaModel> products = repository.GetAll();
            return Ok(products);
        }

        [HttpGet("GetPizzaById", Name = "GetPizzaById")]
        public IActionResult GetPizzaById(int id)
        {
            var pizza = _repository.FindById(id);
            //return Json(pizza);
            return Ok(pizza); //вместо Json
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
                //return Ok("CheckExceptions", result);

                var response = new { Message = "CheckExceptions", Result = result }; // Создаем объект с двумя свойствами

                return Ok(response); // Возвращаем объект в методе Ok
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"A change occurred while trying to get information about the pizza with ID {pizzaId} in the CheckExceptions method"); // Записываем исключение

                return StatusCode(StatusCodes.Status404NotFound , "Error code: " + StatusCodes.Status404NotFound + ". " + ex.Message);
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
