using Models;
namespace Commands;

public sealed class AddProduct : ICommand
{
    private readonly List<Product> _products;
    private readonly List<Product> _cart;
    private readonly Product _product;
    public AddProduct(List<Product> products, List<Product> cart, Product product){
        _products = products;
        _cart = cart;
        _product = product;
    }
    public bool CanExecute()
    {
        if(_product is null) return false;
        var product = _products.Find(p => p.Name == _product.Name);
        return product?.Quantity > 0;
    }

    public void Execute()
    {
        var product = new Product(_product.Name, _product.Price, 1);
        _cart.Add(product);
        var productRepository = _products.Find(p => p.Name == _product.Name);
        if(productRepository is not null) productRepository.Quantity--;
    }

    public void Undo()
    {
        _cart.RemoveAll(p => p.Name == _product.Name);
        var productRepository = _products.Find(p => p.Name == _product.Name);
        if(productRepository is not null) productRepository.Quantity++;
    }
}