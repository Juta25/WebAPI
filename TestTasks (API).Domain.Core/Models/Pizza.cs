using System;
using System.Collections.Generic;

namespace TestTasks__API_;

public partial class Pizza
{
    public int Id { get; set; }

    public string Image { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Weight { get; set; }

    public int Price { get; set; }
}
