using Lab5TestTask.Data;
using Lab5TestTask.Enums;
using Lab5TestTask.Models;
using Lab5TestTask.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab5TestTask.Services.Implementations;

/// <summary>
/// UserService implementation.
/// Implement methods here.
/// </summary>
public class UserService : IUserService
{
    private readonly ApplicationDbContext _dbContext;

    public UserService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<User> GetUserAsync()
    {
        var users = _dbContext.Users.AsQueryable();

        var maxUser = users.OrderByDescending(u => u.Sessions.Count).First();

        return maxUser;

        throw new NotImplementedException();
    }

    public async Task<List<User>> GetUsersAsync()
    {
        var sessions = _dbContext.Sessions.AsQueryable();

        var users = _dbContext.Users.AsQueryable();

        var selectedUsers = from session in sessions
                            from user in users
                            where session.DeviceType == DeviceType.Mobile
                            where user.Id == session.UserId
                            select user;


        return selectedUsers.Distinct().ToList();

        throw new NotImplementedException();
    }
}
