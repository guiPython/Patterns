
namespace Models;

public class Product{
    public string Name { get; private set;}
    public decimal Price { get; private set;}
    public int Quantity { get; set;}

    public Product(string name, decimal price, int quantity){
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    public override string ToString()
    {
        return $"{Name} {Price} {Quantity}";
    }
}