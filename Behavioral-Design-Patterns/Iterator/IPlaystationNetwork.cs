using Behavioral_Design_Patterns.Models;

namespace Behavioral_Design_Patterns.Iterator;

public interface IPlaystationNetwork
{
    public void AddProfile(PlayerProfile profile);
    public void RemoveProfile(PlayerProfile profile);
    public IProfileIterator ProfileIterator();
}