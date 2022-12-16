using Behavioral_Design_Patterns.Mediator;

namespace Behavioral_Design_Patterns.Models;

public abstract class Profile
{
    protected IGameChat _gameChat;

    protected Profile(IGameChat gameChat)
    {
        _gameChat = gameChat;
    }

    public abstract string Send(string message);

    public abstract void Receive(string message);

    public IGameChat GetMediator()
    {
        return _gameChat;
    }
}