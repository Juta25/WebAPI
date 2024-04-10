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

        // ���������� ��� ������������ � ����
        public HomeController(ILogger<HomeController> logger, IPizzaRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }


        [HttpGet("Index", Name = "Index")]  //������� ��� HttpGet � ������������ �������� ��� ������������� � ������ ������ ����
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
                    Name = "����������",
                    Image = "P1.jpg",
                    Description = "���� '���������', ��� ���������, ������� ������, ��������� ����������������, ������� �������, ��������� �������",
                    Weight = 670,
                    Price = 470
                },
                new PizzaModel
                {
                    Id = 2,
                    Name = "Capriccio",
                    Image = "P2.jpg",
                    Description = "��� ���������, ���� '�������', ���� '�������', ���������, ����� �����, �����, �������, ������ �����, ����������",
                    Weight = 980,
                    Price = 600
                },
                new PizzaModel
                {
                    Id = 3,
                    Name = "XXXL",
                    Image = "P3.jpg",
                    Description = "��� ���������, ���� '1000 ��������', ������� �����, �������, �������� ���������, �����, ��������, ������ ������������, ������ �����, �������, ��� ������������",
                    Weight = 740,
                    Price = 510
                },
                new PizzaModel
                {
                    Id = 4,
                    Name = "4 �����",
                    Image = "P4.jpg",
                    Description = "���� '1000 ��������', ��� ���������, ����� �������, �������, ���������, ��� ��������, ����������, ������ ������, �������/������",
                    Weight = 540,
                    Price = 450
                },
                new PizzaModel
                {
                    Id = 5,
                    Name = "��������",
                    Image = "P5.jpg",
                    Description = "���� '��������', ��� ���������, ������� ������, ��������, ������ ������������, ����� ����������, ����������, ������ �����, �������, ��� ������������",
                    Weight = 600,
                    Price = 450
                },
                new PizzaModel
                {
                    Id = 6,
                    Name = "�����ZZ�",
                    Image = "P6.jpg",
                    Description = "������, ���� '���������', ��� ���������, ������, ������� ��������, �����/�������, ������� ����������",
                    Weight = 520,
                    Price = 420
                },
                new PizzaModel
                {
                    Id = 7,
                    Name = "�������",
                    Image = "P7.jpg",
                    Description = "���� '��������', ��� ���������, �������, �����, ���������, ���� '�������', ������, ����� ����������, ��� ������������",
                    Weight = 590,
                    Price = 450
                },
                new PizzaModel
                {
                    Id = 8,
                    Name = "�������",
                    Image = "P8.jpg",
                    Description = "���� '�������� ������', ��� ���������, ������� ������, ��������, ������ ����������������, ���� ������ '����������', ����� ����������X, ��� ������������",
                    Weight = 670,
                    Price = 450
                },
                new PizzaModel
                {
                    Id = 9,
                    Name = "���������",
                    Image = "P9.jpg",
                    Description = "�������, ���� '���������', ��� ���������, ������, ����� ����������",
                    Weight = 550,
                    Price = 450
                },
                new PizzaModel
                {
                    Id = 10,
                    Name = "��������� Premium",
                    Image = "P10.jpg",
                    Description = "���� '���������', ��� ���������, ������, �������, ������� �����, ��������, ����� ����������",
                    Weight = 590,
                    Price = 470
                },
                new PizzaModel
                {
                    Id = 11,
                    Name = "���������",
                    Image = "P11.jpg",
                    Description = "���� '�������', ��� ���������, ��������� ���, ��������, ������ �����, ����� ����������, �������",
                    Weight = 570,
                    Price = 480
                },
                new PizzaModel
                {
                    Id = 12,
                    Name = "�������",
                    Image = "P12.jpg",
                    Description = "���� '�������', ��� ���������, ����� ������������, �����, ����������, ��� ������������",
                    Weight = 550,
                    Price = 450
                }
                };

            //return Json(pizzas);
            return Ok(pizzas); //������ Json
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
            return Ok(pizza); //������ Json
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

                var response = new { Message = "CheckExceptions", Result = result }; // ������� ������ � ����� ����������

                return Ok(response); // ���������� ������ � ������ Ok
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"A change occurred while trying to get information about the pizza with ID {pizzaId} in the CheckExceptions method"); // ���������� ����������

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
