using Behavioral_Design_Patterns.Models;

namespace Behavioral_Design_Patterns.Iterator;

public class PlaystationNetwork : IPlaystationNetwork
{
    private List<PlayerProfile> _playerProfiles;

    public PlaystationNetwork(List<PlayerProfile> playerProfiles)
    {
        _playerProfiles = playerProfiles;
    }

    public void AddProfile(PlayerProfile profile)
    {
        _playerProfiles.Add(profile);
    }

    public void RemoveProfile(PlayerProfile profile)
    {
        _playerProfiles.Remove(profile);
    }

    public IProfileIterator ProfileIterator()
    {
        return new ProfileIterator(_playerProfiles);
    }
}