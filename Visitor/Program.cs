using Visitor;
using System;
using System.Collections.Generic;

namespace Principal;

class Program
{
    public static void Main()
    {
        Func<dynamic, string> getName = variable => variable.GetType().Name;

        Veiculo carro = new Carro((decimal) 100_000);
        Veiculo moto = new Moto((decimal) 30_000);
        Veiculo monociclo = new Monociclo((decimal) 1_000);

        IVisitorImposto imposto = new VisitorImpostoSaoPaulo();
        IVisitorComissao comissao = new VisitorComissaoSaoPaulo();
        Veiculo[] veiculos = {carro, moto, monociclo};

        foreach (dynamic veiculo in veiculos)
        {   Console.WriteLine($"Valor do {getName(veiculo)}: {veiculo.Preco}");
            Console.WriteLine($"Valor de Venda {getName(veiculo)}: {Vendedor.Vender(veiculo, comissao, imposto)}\n");
        }
    }
}
