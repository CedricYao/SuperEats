using SuperEats.Features.MenuItems.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class MenuItemRepository : IMenuItemRepository
{
    private readonly ConcurrentDictionary<int, MenuItem> _menuItems = new ConcurrentDictionary<int, MenuItem>();

    public MenuItemRepository()
    {
        // Add some sample menu items for testing purposes.  Remove in production.
        _menuItems.TryAdd(1, new MenuItem
        {
            ItemId = 1,
            RestaurantId = 1,
            Name = "Pizza Margherita",
            Price = 12.99m
        });
        _menuItems.TryAdd(2, new MenuItem
        {
            ItemId = 2,
            RestaurantId = 2,
            Name = "Cheeseburger",
            Price = 8.99m
        });

    }

    public Task<IEnumerable<MenuItem>> GetAllAsync()
    {
        return Task.FromResult(_menuItems.Values.AsEnumerable());
    }

    public Task<MenuItem?> GetByIdAsync(int menuItemId)
    {
        return Task.FromResult(_menuItems.GetValueOrDefault(menuItemId));
    }
    public Task AddMenuItemAsync(MenuItem menuItem)
    {
        _menuItems.TryAdd(menuItem.ItemId, menuItem);
        return Task.CompletedTask;
    }

    public Task UpdateMenuItemAsync(MenuItem menuItem)
    {
         _menuItems.TryGetValue(menuItem.ItemId, out var oldMenuItem);
        if (oldMenuItem != null)
        {
            _menuItems.TryUpdate(menuItem.ItemId, menuItem, oldMenuItem);
        }
        return Task.CompletedTask;
    }

    public Task DeleteMenuItemAsync(int menuItemId)
    {
         _menuItems.TryRemove(menuItemId, out MenuItem menuItem);
         return Task.CompletedTask;
    }
}

public interface IMenuItemRepository
{
    Task<IEnumerable<MenuItem>> GetAllAsync();
    Task<MenuItem?> GetByIdAsync(int menuItemId);
    Task AddMenuItemAsync(MenuItem menuItem);
    Task UpdateMenuItemAsync(MenuItem menuItem);
    Task DeleteMenuItemAsync(int menuItemId);
}
