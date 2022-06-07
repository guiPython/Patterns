

namespace Commands;

public class Manager{
    private Stack<ICommand> commands = new Stack<ICommand>();

    public void Invoke(ICommand command){
        if(command.CanExecute()){
            command.Execute();
            commands.Push(command);
        }
    }

    public void Undo(){
        while(commands.Count() > 0){
            var command = commands.Pop();
            command.Undo();
        }
    }
}