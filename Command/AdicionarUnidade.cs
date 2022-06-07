using Principal;
namespace Commands;

public sealed class AdicionarUnidade : ICommand
{
    private readonly CarrinhoDeCompras _carrinho;
    private readonly Produto _produto;
    public AdicionarUnidade(CarrinhoDeCompras carrinho, Produto produto)
    {
        _carrinho = carrinho;
        _produto = produto;
    }
    public bool CanExecute() => true;

    public void Execute()
    {
        var item = _carrinho.Find(p => p.Nome == _produto.Nome);
        if (item is null) _carrinho.Add(_produto);
        else item.Quantidade++;
    }

    public void Undo()
    {
        var item = _carrinho.Find(p => p.Nome == _produto.Nome);
        if (item?.Quantidade == 1) _carrinho.Remove(item);
        else if (item?.Quantidade >= 1) item.Quantidade--;
    }
}