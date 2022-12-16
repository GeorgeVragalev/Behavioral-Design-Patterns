using Behavioral_Design_Patterns.Models;

namespace Behavioral_Design_Patterns.Memento;

public class PlayerSnapShot
{
    private Player _player;
    private PlayerStatus _playerStatus;
    private string _name;
    private int _healthPoints;


    public PlayerSnapShot(Player player, PlayerStatus playerStatus, string name, int healthPoints)
    {
        _player = player;
        _playerStatus = playerStatus;
        _name = name;
        _healthPoints = healthPoints;
    }

    public void RestoreStatus()
    {
        _player.SetHealth(_healthPoints);
        _player.SetStatus(_playerStatus);
        _player.SetName(_name);
    }
}