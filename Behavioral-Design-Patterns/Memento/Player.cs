using Behavioral_Design_Patterns.Iterator;
using Behavioral_Design_Patterns.Mediator;
using Behavioral_Design_Patterns.Models;

namespace Behavioral_Design_Patterns.Memento
{
    public class Player
    {
        private PlayerProfile Profile { get; set; }
        public PlayerStatus PlayerStatus { get; set; }
        public string Name { get; set; }
        
        public int HealthPoints = 100;
        
        public Player(PlayerProfile profile)
        {
            Profile = profile;
        }

        public void SetStatus(PlayerStatus status)
        {
            PlayerStatus = status;
        }
        
        public void SetName(string name)
        {
            Name = name;
        }

        public void SetHealth(int health)
        {
            HealthPoints = health;
        }
        
        public PlayerSnapShot CreateSnapShot()
        {
            return new PlayerSnapShot(this, PlayerStatus, Name, HealthPoints);
        }

        public IGameChat GetMediator()
        {
            return Profile.GetMediator();
        }
        
        public PlayerProfile GetProfile()
        {
            return Profile;
        }
    }
}