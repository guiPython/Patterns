using System.Text;

namespace Principal;

public class CarrinhoDeCompras
{
    private List<ItemCarrinho> _itens;

    public CarrinhoDeCompras()
    {
        _itens = new List<ItemCarrinho>();
    }

    public void Add(Produto produto)
    {
        _itens.Add(new ItemCarrinho(produto, 1));
    }

    public void Remove(ItemCarrinho item)
    {
        _itens.Remove(item);
    }

    public ItemCarrinho? Find(Predicate<Produto> predicate)
    {
        var item = _itens.Find(item => predicate.Invoke(item.Produto));
        return item;
    }

    public decimal CalculaTotal()
    {
        return _itens.Sum(i => i.Produto.Preco * i.Quantidade);
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("\nCarrinho de Compras");
        if (_itens.Count == 0) return sb.AppendLine("Carrinho Vazio\n").ToString();

        foreach (var item in _itens)
        {
            sb.AppendLine($"{item}");
        }
        sb.AppendLine($"Total {CalculaTotal()}\n");
        return sb.ToString();
    }
}