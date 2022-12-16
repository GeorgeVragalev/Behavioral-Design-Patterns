using Behavioral_Design_Patterns.Models;

namespace Behavioral_Design_Patterns.Mediator;

public class GameChat : IGameChat
{
    private readonly List<PlayerProfile> _playerProfiles;

    public GameChat()
    {
        _playerProfiles = new List<PlayerProfile>();
    }

    public void SendMessage(string message, PlayerProfile profile)
    {
        foreach (var playerProfile in _playerProfiles)
        {
            if (!playerProfile.Equals(profile))
            {
                playerProfile.Receive(message);
            }
        }
    }

    public void AddPlayerProfile(PlayerProfile profile)
    {
        _playerProfiles.Add(profile);
    }
}