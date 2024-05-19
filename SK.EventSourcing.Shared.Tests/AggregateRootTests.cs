namespace SK.EventSourcing.Shared.Tests;
using SK.EventSourcing.Shared;

public class AggregateRootTests
{
    public class User : SK.EventSourcing.Shared.AggregateRoot
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }

        public User(string username, string password)
        {
            this.ApplyChange(new UserCreated(username, password));
        }

        private void Apply(UserCreated @event)
        {
            this.UserName = @event.UserName;
            this.Password = @event.Password;
        }
    }

    public class UserCreated : SK.EventSourcing.Shared.DomainEvent
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public UserCreated(string username, string password)
        {
            UserName = username;
            Password = password;
        }

        public UserCreated()
        {
            
        }
    }


    [Fact]
    public void Test1()
    {

    }
}