
namespace Principal;

public class Usuario{
    public int? Id {get; set;}
    public string? Nome {get; set;}
    public int? Idade {get; set;}
    public string? Profissao {get; set;}
    public Usuario(){}
    public Usuario(int id, string nome, int idade, string profissao)
    {
        Id = id;
        Nome = nome;
        Idade = idade;
        Profissao = profissao;
    }

    public override string ToString()
    {
        return $"{Id} - {Nome} - {Idade} - {Profissao}";
    }
}