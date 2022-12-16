namespace Behavioral_Design_Patterns.Command;

public class Invoker
{
    private readonly ICommand _command;

    public Invoker(ICommand command)
    {
        _command = command;
    }

    public void SendNotification()
    {
        _command.Execute();
    }
}