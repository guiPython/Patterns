namespace Principal;
public class ProxyRepositorio: IRepositorio{

    private readonly IRepositorio _repositorio;
    private List<Usuario> _cacheUsuarios = new List<Usuario>();

    public ProxyRepositorio(string path){
        _repositorio = new Repositorio(path);
    }

    public List<Usuario> FindAll(){
        var usuarios = _repositorio.FindAll();
        foreach(var u in usuarios) _cacheUsuarios.Append(u);
        return usuarios;
    }

    public Usuario? FindByID(int id){
        Usuario? usuario = null;
        if(_cacheUsuarios.Count() > 0){
            usuario = _cacheUsuarios.FirstOrDefault(u => u.Id == id);
        }else if(usuario is null){
            usuario = _repositorio.FindByID(id);
            if(usuario is not null) _cacheUsuarios.Add(usuario);
        }
        return usuario;
    }
}