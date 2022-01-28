using Visitor;

namespace Principal;
public abstract class Veiculo
{
    public decimal Preco { get; set; }
    public Veiculo(decimal preco){
        Preco = preco;
    }

    public abstract decimal Comissao(IVisitorComissao calculadoraComissao);
    public abstract decimal Imposto(IVisitorImposto calculadoraImposto);
}

public class Carro: Veiculo{
    public Carro(decimal preco):base(preco){}

    public override decimal Comissao(IVisitorComissao visitor)
    {
        return visitor.CalculaComissao(this);
    }
    public override decimal Imposto(IVisitorImposto visitor){
        return visitor.CalculaImposto(this);
    }
}

public class Moto: Veiculo{
    public Moto(decimal preco):base(preco){}

    public override decimal Comissao(IVisitorComissao visitor)
    {
        return visitor.CalculaComissao(this);
    }
    public override decimal Imposto(IVisitorImposto visitor){
        return visitor.CalculaImposto(this);
    }
}

public class Monociclo: Veiculo{
    public Monociclo(decimal preco):base(preco){}

    public override decimal Comissao(IVisitorComissao visitor)
    {
        return visitor.CalculaComissao(this);
    }
    public override decimal Imposto(IVisitorImposto visitor){
        return visitor.CalculaImposto(this);
    }
}