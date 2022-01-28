using Visitor;

namespace Principal;

public class Vendedor
{
    public static decimal Vender(
        Veiculo veiculo, 
        IVisitorComissao calculadoraComissao, 
        IVisitorImposto calculadoraImposto
    )
    {
        decimal comissao = veiculo.Comissao(calculadoraComissao);
        decimal imposto = veiculo.Imposto(calculadoraImposto);
        return veiculo.Preco + imposto - comissao;
    }

}


