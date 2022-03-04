namespace Commands;

public class Pessoa
{
    public Stack<ICommand> comandos = new Stack<ICommand>();

    public void ExecutarAcao(ICommand comando)
    {
        if (comando.CanExecute())
        {
            comando.Execute();
            comandos.Push(comando);
        }
    }

    public void DesfazerUltimaAcao()
    {
        if (comandos.Count() != 0)
        {
            comandos.Pop().Undo();
        }
    }

    public void DesfazerTudo()
    {
        while (comandos.Count() != 0)
        {
            comandos.Pop().Undo();
        }
    }
}