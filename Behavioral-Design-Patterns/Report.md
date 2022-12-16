# Topic: Behavioral Design Patterns.
## Course: Software design techniques
## Author: George Vragalev

# Theory
The best techniques employed by experienced object-oriented software engineers
are represented by design patterns. Design patterns are solutions to common issues
that software developers ran across when creating new applications. Many software
developers over a sizable period of time came up with these solutions through trial and error.

One type of the design pattern that will be implemented in this laboratory work is Behavioral
Design Pattern. Behavioral design patterns is a category of design patterns that focus on
communication between objects. These patterns are used to help objects cooperate and coordinate their
behavior in order to achieve a common goal.
In these design patterns, the interaction between the objects should be in such a way that
they can easily talk to each other and still should be loosely coupled.


Types of Behavioral design patterns are:
1. **Chain of Responsibility Pattern**. A behavioral design pattern where sender sends a request to a chain
   of objects. The request can be handled by any object in the chain.
2. **Command Pattern**. A behavioral design pattern that encapsulates a command request as an object.
3. **Interpreter Pattern**. A behavioral design pattern that is used in a way to include language elements in a program.
4. **Iterator Pattern**. A behavioral design pattern that sequentially access the elements of a collection.
5. **Mediator Pattern**.  A behavioral design pattern that defines simplified communication between classes.
6. **Memento Pattern** . A behavioral design pattern that capture and restore an object's internal state.
7. **Observer Pattern**. A behavioral design pattern that is used in a way of notifying change to a number of classes.
8. **State Pattern**. A behavioral design pattern that alter an object's behavior when its state changes.
9. **Strategy Pattern**. A behavioral design pattern that encapsulates an algorithm inside a class.
10. **Template Pattern**. A behavioral design pattern that defer the exact steps of an algorithm to a subclass.
11. **Visitor Pattern**. A behavioral design pattern that defines a new operation to a class without change.
12. **Null Object**. A behavioral design pattern that was designed to act as a default value of an object.

# Objectives:
1. Study and understand the Behavioral Design Patterns.
2. Choose a domain, define its main classes/models/entities and
   choose the appropriate instantiation mechanisms.
3. Use some behavioral design patterns for object instantiation in a sample project.

# Implementation description
This project is based on an online game simulation, where players can have profiles and how they can use the chat
based on their game status.

### Mediator Design Pattern
Firstly, the Mediator Pattern was implemented. This pattern define an object that encapsulates
how a set of objects interact. Mediator promotes loose coupling by keeping objects from
referring to each other explicitly, and it lets you vary their interaction independently.
To start with, a target interface IGameChat was created with two methods SendMessage() and AddPlayerProfile().
This is the mediator interface class which will be used further.

```c#
public interface IGameChat
{
    public void SendMessage(string message, PlayerProfile profile);
    void AddPlayerProfile(PlayerProfile profile);   
}
  
```

Further players can send and receive messages, so the Profile abstract is created and used by PlayerProfile.
Notice the profile has a reference to the mediator object, it’s required for the communication
between different players in the game.

```c#
public abstract class Profile
{
    protected IGameChat _gameChat;

    protected Profile(IGameChat gameChat)
    {
        _gameChat = gameChat;
    }

    public abstract string Send(string message);

    public abstract void Receive(string message);

    public IGameChat GetMediator()
    {
        return _gameChat;
    }
}
```

The implementation of the concrete mediator class GameChat, which inherits from IGameChat.
It has a list of players that will chat between each other like a group chat.

```c#
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
```

Now we can create concrete profile class PlayerProfile.

```c#
public class PlayerProfile : Profile
{
    private readonly int _id;
    private readonly int _rank;
    private readonly ProfileEnum _userName;
    private readonly string _dateOfBirth;
    
    //mediator
    public PlayerProfile(IGameChat gameChat, int id, int rank, ProfileEnum userName, string dateOfBirth) : base(gameChat)
    {
        _id = id;
        _rank = rank;
        _userName = userName;
        _dateOfBirth = dateOfBirth;
    }

    public override string Send(string message)
    {
        Console.WriteLine($"{_userName} is sending message: {message}");
        _gameChat.SendMessage(message, this);
        return message;
    }

    public override void Receive(string message)
    {
        Console.WriteLine($"{_userName} received message: {message}");
    }
}
```

### Command Pattern
Secondly, the Command Pattern was implemented. This pattern encapsulate a request
under an object as a command and pass it to invoker object. Invoker object looks
for the appropriate object which can handle this command and pass the command to
the corresponding object and that object executes the command.

The ICommand interface was created with the execute() method. This interface will act
as a command.

```c#
public interface ICommand {
    public void execute();
}
```

The receiver class will use the PlayerProfile class implemented earlier. It is shared across multiple design
pattern implementations. Concrete command SendMessageCommand implements the ICommand interface
and overrides its methods.
```c#
public class SendMessageCommand : ICommand
{
    private readonly IGameChat _gameChat;
    private readonly string _message;
    private readonly PlayerProfile _playerProfile;
    
    public SendMessageCommand(IGameChat gameChat, PlayerProfile playerProfile, string message)
    {
        _gameChat = gameChat;
        _message = message;
        _playerProfile = playerProfile;
    }

    public void Execute()
    {
        _gameChat.SendMessage(_message, _playerProfile);
    }
}
```
Further, the Invoker class CommandInvoker was implemented, which invokes the call of the concrete command
```c#
public class Invoker
{
    private readonly ICommand _command;

    public Invoker(ICommand command)
    {
        _command = command;
    }

    public void SendNotification()
    {
        _command.Execute();
    }
}
```

### Iterator Pattern
This pattern is used to access the elements of an aggregate object sequentially without exposing
its underlying implementation. The Iterator pattern is also known as Cursor.

We have already the Profile class, that generates different students in the game chat.
Since this chat is based in a network, we can have different network implementations and groups that have
chats for this game, like the playstation network, Xbox, PC groups etc.
I implemented the IPlaystationNetwork interface that has the add and remove methods,
as well as the iterator method that returns the iterator for traversal of the profiles that are
connected to that network. Note that this can be extended and provide a new wawy of iterating thorugh the 
profiles for example an iterator that goes through the players that have a specific rank.
```c#
public interface IPlaystationNetwork
{
    public void AddProfile(PlayerProfile profile);
    public void RemoveProfile(PlayerProfile profile);
    public IProfileIterator ProfileIterator();
}
```
The IProfileIterator interface defines the GetNext() and HasMore() methods.

```c#
public interface IProfileIterator
{
    public PlayerProfile GetNext();
    public bool HasMore();
}
```

Now we can implement the concrete implemntation of the iterator for our playstation community.
Firstly, we have the PlaystationNetwork class, that implements the IPlaystationNetwork
interface with its methods, for adding removing player profiles and the iteration among the
list of those profiles.

```c#
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
```
The ProfileIterator class implements the IProfileIterator interface and its methods. This will let us
iterate through the profiles list.
```c#
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
```

### Memento Pattern
Memento is a behavioral design pattern that lets you save and restore 
the previous state of an object without revealing the details of its implementation. 
The implementation of this pattern is based on nested classes.

First we create the Memento - PlayerSnapShot class, which is used to save the current status of the player progress, as a copy
of the Player.
```c#
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
```

Now we have the Player class itself. The idea is that we don't want to lose the players progress
after closing the game, so we have a method that creates a SnapShot of the current player progress and returns the 
Player object.

```c#
public class Player
    {
        private PlayerProfile Profile { get; set; }
        public PlayerStatus PlayerStatus { get; set; }
        public string Name { get; set; }
        public int HealthPoints = 100;
        
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
```

In order for saving to take place we have to manage the history of the player's game progress with a Caretaker 
class - PlayerProgressHistory. This allows to save the player's progress in a private snapshot field
and retrieve that saved data when loading back into the game.

```c#
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
```

### State Pattern
A State Pattern says that "the class behavior changes based on its state".
In State Pattern, we create objects which
represent various states and a context object whose behavior varies as its state object changes.
The idea behind state is that it the same operation like sending a message to a player will have different
implemnattion and execution outcomes depending on the state of that player. ie weather he's in a game or he is paused.

Firstly, the IState interface was created.
```c#
public interface IState
{
    public void InviteToGame();
    public void SendMessage(string message);
}
```

Now the two concrete states that a player can have are InGame and Paused states that implement this 
interface, that define the behavior for the corresponding states.

```c#
public class InGame : IState
{
    private readonly Player _player;

    public InGame(Player player)
    {
        _player = player;
        player.SetStatus(PlayerStatus.Playing);
    }

    public void InviteToGame()
    {
        Console.WriteLine($"{_player.Name} is currently in game and cannot join your session");
    }

    public void SendMessage(string message)
    {
        Console.WriteLine($"{_player.Name} is currently in game and cannot respond to messages"); 
    }
}
```

The State interface defines InviteToGame() and SendMessage() methods that are used to
communicate to players in the chat room. When a player is in game, he cannot accept any game invites as that
would make him lose his current progress, hence the notification is shown. Whereas if a player paused the game
and is in lobby menu, he will receive the messages and game invites.

```c#
public class Paused : IState
{
    private Player _player;

    public Paused(Player player)
    {
        _player = player;
        player.SetStatus(PlayerStatus.Pause);
    }

    public void InviteToGame()
    {
        Console.WriteLine($"{_player.Name} Sending game invite...");
        _player.GetMediator().SendMessage($"{_player.Name} sent a game invite", _player.GetProfile());
    }

    public void SendMessage(string message)
    {
        Console.WriteLine($"{_player.Name}: sent a message: {message}"); 
        _player.GetMediator().SendMessage(message, _player.GetProfile());
    }
}
````
When initialised the state sets the status of that player to the corresponding one. If the object
InGame is created, the player which is passed in ctr will have Playing status. 

### Conclusion

   In conclusion, behavioral design patterns are a category of design patterns that
focus on communication and the flow of control between objects in a system.
These patterns are useful for designing interactive systems where the behavior 
of one object depends on the behavior of other objects. 
   
They can help to improve the flexibility and extensibility of a system by decoupling objects
and allowing them to communicate in a loosely-coupled way. Some examples of behavioral design patterns include
Command, Iterator, Mediator, Observer, State, Strategy, and Template Method.