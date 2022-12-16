using Behavioral_Design_Patterns.Memento;
using Behavioral_Design_Patterns.Models;

namespace Behavioral_Design_Patterns.State;

public class InGame : IState
{
    private readonly Player _player;

    public InGame(Player player)
    {
        _player = player;
        player.SetStatus(PlayerStatus.Playing);
    }

    public void InviteToGame()
    {
        Console.WriteLine($"{_player.Name} is currently in game and cannot join your session");
    }

    public void SendMessage(string message)
    {
        Console.WriteLine($"{_player.Name} is currently in game and cannot respond to messages"); 
    }
}