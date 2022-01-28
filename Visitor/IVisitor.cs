using Principal;
namespace Visitor;

public interface IVisitorComissao{
    decimal CalculaComissao(Carro carro);
    decimal CalculaComissao(Moto moto);
    decimal CalculaComissao(Monociclo monociclo);
}
public interface IVisitorImposto{
    decimal CalculaImposto(Carro carro);
    decimal CalculaImposto(Moto moto);
    decimal CalculaImposto(Monociclo monociclo);
}