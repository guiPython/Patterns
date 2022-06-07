
namespace Principal;

public class Repositorio : IRepositorio{
    private readonly string _path;
    public Repositorio(string path){
        _path = path;
    }

    //Esse método simula uma acesso custoso a uma fonte de dados
    public List<Usuario> FindAll(){
        var usuarios = new List<Usuario>();
        var usuario = new Usuario();

        using(var fs = new StreamReader(_path)){
            string[]? cabecalho = fs.ReadLine()?.Split(',');
            string? linha;

            while((linha = fs.ReadLine()) != null){

                string[] campos = linha.Split(',');
                usuario.Id = int.Parse(campos[0]);
                usuario.Nome = campos[1];
                usuario.Idade = int.Parse(campos[2]);
                usuario.Profissao = campos[3];
                usuarios.Add(usuario);
                Thread.Sleep(1000);

            }
            
        }
        return usuarios;
    }

    public Usuario? FindByID(int id){
        var usuario = new Usuario();
        string[] linhas = File.ReadAllLines(_path);
        string[] registro = linhas[id+1].Split(',');

        usuario.Id = int.Parse(registro[0]);
        usuario.Nome = registro[1];
        usuario.Idade = int.Parse(registro[2]);
        usuario.Profissao = registro[3];
        GC.SuppressFinalize(linhas);
        
        return usuario;
    }
}