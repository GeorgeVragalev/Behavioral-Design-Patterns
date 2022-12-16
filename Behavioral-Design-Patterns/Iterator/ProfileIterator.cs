using Behavioral_Design_Patterns.Models;

namespace Behavioral_Design_Patterns.Iterator;

public class ProfileIterator : IProfileIterator
{
    private List<PlayerProfile> _playerProfiles;
    private int _position = 0;

    public ProfileIterator(List<PlayerProfile> playerProfiles)
    {
        _playerProfiles = playerProfiles;
    }

    public PlayerProfile GetNext()
    {
        if (HasMore())
        {
            var player = _playerProfiles.ElementAt(_position);
            _position++;
            return player;
        }

        return null;
    }

    public bool HasMore()
    {
        return _position < _playerProfiles.Count;
    }
}