using System.Collections.Generic;

namespace Principal;
public interface IRepositorio{
    List<Usuario> FindAll();
    Usuario? FindByID(int id);
}