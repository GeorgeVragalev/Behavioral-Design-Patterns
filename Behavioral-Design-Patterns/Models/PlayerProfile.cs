using Behavioral_Design_Patterns.Iterator;
using Behavioral_Design_Patterns.Mediator;

namespace Behavioral_Design_Patterns.Models;

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

    public int GetId()
    {
        return _id;
    }
    
    public ProfileEnum GetUserName()
    {
        return _userName;
    }
    
    public int GetRank()
    {
        return _rank;
    }
    
    public string GetDateOfBirth()
    {
        return _dateOfBirth;
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