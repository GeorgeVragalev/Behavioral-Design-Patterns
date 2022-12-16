using Behavioral_Design_Patterns.Models;

namespace Behavioral_Design_Patterns.Iterator;

public interface IProfileIterator
{
    public PlayerProfile GetNext();
    public bool HasMore();
}