using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarbageCollector;

public class Payment
{
    public int Id { get; set; }
    public string Currency { get; set; } = string.Empty;
    public decimal Value { get; set; }

    public Payment(int id)
    {
        Id = id;
        Console.WriteLine($"Payment {Id} created");
    }

    ~Payment()
    {
        Console.WriteLine($"Payment {Id} destroyed by the Garbage Collector!!");
    }
}
