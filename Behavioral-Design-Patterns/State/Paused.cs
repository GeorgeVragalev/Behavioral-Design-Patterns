using Behavioral_Design_Patterns.Memento;
using Behavioral_Design_Patterns.Models;

namespace Behavioral_Design_Patterns.State;

public class Paused : IState
{
    private Player _player;

    public Paused(Player player)
    {
        _player = player;
        player.SetStatus(PlayerStatus.Pause);
    }

    public void InviteToGame()
    {
        Console.WriteLine($"{_player.Name} Sending game invite...");
        _player.GetMediator().SendMessage($"{_player.Name} sent a game invite", _player.GetProfile());
    }

    public void SendMessage(string message)
    {
        Console.WriteLine($"{_player.Name}: sent a message: {message}"); 
        _player.GetMediator().SendMessage(message, _player.GetProfile());
    }
}