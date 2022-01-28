using Principal;

namespace Visitor;

public class VisitorImpostoSaoPaulo: IVisitorImposto{
    public decimal CalculaImposto(Carro carro){
        return carro.Preco * (decimal) 0.1;
    }

    public decimal CalculaImposto(Monociclo monociclo){
        return monociclo.Preco * (decimal) 0.08;
    }

    public decimal CalculaImposto(Moto moto){
        return moto.Preco * (decimal) 0.04;
    }
}

public class VisitorComissaoSaoPaulo: IVisitorComissao{

    public decimal CalculaComissao(Carro carro){
        return carro.Preco * (decimal) 0.01;
    }
    
    public decimal CalculaComissao(Moto moto){
        return moto.Preco * (decimal) 0.01;
    }

    public decimal CalculaComissao(Monociclo monociclo){
        return monociclo.Preco * (decimal) 0.01;
    }
}