using SuperEats.Features.Users.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class UserRepository : IUserRepository
{
    private readonly ConcurrentDictionary<int, User> _users = new ConcurrentDictionary<int, User>();

    public UserRepository()
    {
        // Add some sample users for testing purposes.  Remove in production.
        _users.TryAdd(1, new User
        {
            UserId = 1,
            Username = "testuser1",
            Email = "test1@example.com",
            Password = "password123"
        });
        _users.TryAdd(2, new User
        {
            UserId = 2,
            Username = "testuser2",
            Email = "test2@example.com",
            Password = "password456"
        });

    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        return Task.FromResult(_users.Values.AsEnumerable());
    }

    public Task<User?> GetByIdAsync(int userId)
    {
        return Task.FromResult(_users.GetValueOrDefault(userId));
    }

    public Task AddUserAsync(User user)
    {
        _users.TryAdd(user.UserId, user);
        return Task.CompletedTask;
    }

    public Task UpdateUserAsync(User user)
    {
        _users.TryGetValue(user.UserId, out var oldUser);
        if (oldUser != null)
        {
            _users.TryUpdate(user.UserId, user, oldUser);
        }

         return Task.CompletedTask;
    }

    public Task DeleteUserAsync(int userId)
    {
        _users.TryRemove(userId, out User user);
        return Task.CompletedTask;
    }
}

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int userId);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(int userId);
}
