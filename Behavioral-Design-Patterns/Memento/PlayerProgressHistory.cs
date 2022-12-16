namespace Behavioral_Design_Patterns.Memento;

public class PlayerProgressHistory
{
    private PlayerSnapShot SnapshotBackup;

    public void SaveProgress(Player player)
    {
        SnapshotBackup = player.CreateSnapShot();
    }

    public void LoadBackup()
    {
        SnapshotBackup.RestoreStatus();
    }
}