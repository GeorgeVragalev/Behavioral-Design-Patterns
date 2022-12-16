using Behavioral_Design_Patterns.Models;

namespace Behavioral_Design_Patterns.Mediator;

public interface IGameChat
{
    public void SendMessage(string message, PlayerProfile profile);
    void AddPlayerProfile(PlayerProfile profile);   
}