using Principal;
namespace Commands;

public sealed class RemoverUnidade : ICommand
{
    private readonly CarrinhoDeCompras _carrinho;
    private readonly Produto _produto;
    public RemoverUnidade(CarrinhoDeCompras carrinho, Produto produto)
    {
        _carrinho = carrinho;
        _produto = produto;
    }
    public bool CanExecute()
    {
        if (_produto is null) return false;
        return true;
    }

    public void Execute()
    {
        var item = _carrinho.Find(p => p.Nome == _produto.Nome);
        if (item is null) return;
        else if (item.Quantidade == 1) _carrinho.Remove(item);
        else item.Quantidade--;
    }

    public void Undo()
    {
        var item = _carrinho.Find(p => p.Nome == _produto.Nome);
        if (item is null) _carrinho.Add(_produto);
        else item.Quantidade++;
    }
}