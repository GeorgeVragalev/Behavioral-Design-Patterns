using Behavioral_Design_Patterns.Command;
using Behavioral_Design_Patterns.Iterator;
using Behavioral_Design_Patterns.Mediator;
using Behavioral_Design_Patterns.Memento;
using Behavioral_Design_Patterns.Models;
using Behavioral_Design_Patterns.State;

namespace Behavioral_Design_Patterns;

public static class Program
{
    public static void Main()
    {
        var playerHistory = new PlayerProgressHistory();
        
        var gameChat = new GameChat();
        var playerProfile = new PlayerProfile(gameChat, 1, 5, ProfileEnum.Splendido76, "24/02/2002");
        
        //mediator
        playerProfile.Send("hello");
        playerProfile.Receive("hey");
        
        var player1 = new PlayerProfile(gameChat, 1, 1, ProfileEnum.Petrichor, "02/02/2001");
        var player2 = new PlayerProfile(gameChat, 2, 2, ProfileEnum.Swishswish, "02/02/2002");
        var player3 = new PlayerProfile(gameChat, 3, 3, ProfileEnum.Wanderlust, "02/03/2001");
        var player4 = new PlayerProfile(gameChat, 4, 4, ProfileEnum.Hero4Hire, "02/04/2001");
        
        gameChat.AddPlayerProfile(player1);
        gameChat.AddPlayerProfile(player2);
        gameChat.AddPlayerProfile(player3);
        gameChat.AddPlayerProfile(player4);

        player2.Send("Hi everyone");
        
        Console.WriteLine();
        //command
        var sendCommand = new SendMessageCommand(gameChat, playerProfile, "I have just joined the game!");
        var invoker = new Invoker(sendCommand);
        invoker.SendNotification();
        
        Console.WriteLine();

        //memento
        var player = new Player(playerProfile);
        player.SetName("George");
        player.SetStatus(PlayerStatus.Playing);
        
        playerHistory.SaveProgress(player);
        
        player.SetStatus(PlayerStatus.Died);
        
        playerHistory.LoadBackup();
        
        //state
        var ingame = new InGame(player);
        ingame.SendMessage("This is a spam message");
        ingame.InviteToGame();
        
        Console.WriteLine();

        var paused = new Paused(player);
        paused.SendMessage("This is a spam message v2");
        paused.InviteToGame();
        
        Console.WriteLine();

        //iterator
        var players = new List<PlayerProfile>();
        players.Add(player1);
        players.Add(player2);
        players.Add(player3);
        players.Add(player4);
        
        var playstationNetwork = new PlaystationNetwork(players);
        var iterator = playstationNetwork.ProfileIterator();
        
        var newPlayer = new PlayerProfile(gameChat, 5, 0, ProfileEnum.ProcrastiNation, "02/02/2002");
        playstationNetwork.AddProfile(newPlayer);

        iterator.GetNext().Send("Welcome to the community from PlayStation");

    }
}