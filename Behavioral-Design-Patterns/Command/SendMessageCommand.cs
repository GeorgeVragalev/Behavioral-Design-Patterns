using Behavioral_Design_Patterns.Mediator;
using Behavioral_Design_Patterns.Models;

namespace Behavioral_Design_Patterns.Command;

public class SendMessageCommand : ICommand
{
    private readonly IGameChat _gameChat;
    private readonly string _message;
    private readonly PlayerProfile _playerProfile;
    
    public SendMessageCommand(IGameChat gameChat, PlayerProfile playerProfile, string message)
    {
        _gameChat = gameChat;
        _message = message;
        _playerProfile = playerProfile;
    }

    public void Execute()
    {
        _gameChat.SendMessage(_message, _playerProfile);
    }
}