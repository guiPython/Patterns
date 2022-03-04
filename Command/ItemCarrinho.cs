
namespace Principal;

public class ItemCarrinho
{
    public Produto Produto;
    public int Quantidade;

    public ItemCarrinho(Produto produto, int quantidade)
    {
        Produto = produto;
        Quantidade = quantidade;
    }

    public override string ToString()
    {
        return $"Nome: {Produto.Nome} Quantidade {Quantidade} Preço {Produto.Preco} Subtotal R${Produto.Preco * Quantidade}";
    }
}