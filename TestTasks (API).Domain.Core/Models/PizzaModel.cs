namespace TestTasks__API_.Domain.Core.Models
{
    public class PizzaModel
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Weight { get; set; }
        public int Price { get; set; }

        public PizzaModel()
        {

        }

        public PizzaModel(int id, string image, string name, string description, int weight, int price)
        {
            Id = id;
            Image = image;
            Name = name;
            Description = description;
            Weight = weight;
            Price = price;
        }
    }
}