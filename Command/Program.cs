using Principal;
using Commands;

Produto abacaxi = new Produto("Abacaxi", 7.56m);
CarrinhoDeCompras carrinho = new CarrinhoDeCompras();
ICommand adicionarUnidade = new AdicionarUnidade(carrinho, abacaxi);
ICommand removerUnidade = new RemoverUnidade(carrinho, abacaxi);
Pessoa pessoa = new Pessoa();

Console.WriteLine($"Carrinho no Inicio.{carrinho}");

pessoa.ExecutarAcao(adicionarUnidade);
Console.WriteLine($"Adicionando Abacaxi ao Carrinho.{carrinho}");

pessoa.ExecutarAcao(adicionarUnidade);
Console.WriteLine($"Adicionando mais um Abacaxi ao Carrinho.{carrinho}");

pessoa.ExecutarAcao(adicionarUnidade);
Console.WriteLine($"Adicionando mais um Abacaxi ao Carrinho.{carrinho}");


pessoa.DesfazerUltimaAcao();
Console.Write("Removendo um Abacaxi do Carrinho, ");
Console.ForegroundColor = ConsoleColor.Yellow;
Console.Write($"usando Undo do AdicionaUnidade.\n");
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine(carrinho);

pessoa.ExecutarAcao(removerUnidade);
Console.Write("Removendo um Abacaxi do Carrinho, ");
Console.ForegroundColor = ConsoleColor.Yellow;
Console.Write($"usando RemoverUnidade.\n");
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine(carrinho);

pessoa.DesfazerUltimaAcao();
Console.Write("Adicionando um Abacaxi do Carrinho, ");
Console.ForegroundColor = ConsoleColor.Yellow;
Console.Write($"usando Undo do RemoverUnidade.\n");
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine(carrinho);

pessoa.DesfazerTudo();
Console.Write("Removendo todos os Abacaxis do Carrinho, ");
Console.ForegroundColor = ConsoleColor.Yellow;
Console.Write($"usando DesfazerTudo.\n");
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine(carrinho);