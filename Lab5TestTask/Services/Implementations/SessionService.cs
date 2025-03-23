using Lab5TestTask.Data;
using Lab5TestTask.Enums;
using Lab5TestTask.Models;
using Lab5TestTask.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab5TestTask.Services.Implementations;

/// <summary>
/// SessionService implementation.
/// Implement methods here.
/// </summary>
public class SessionService : ISessionService
{
    private readonly ApplicationDbContext _dbContext;

    public SessionService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Session> GetSessionAsync()
    {
        DbSet<Session> sessions = (DbSet<Session>)_dbContext.Sessions.AsQueryable();

        var desktopSession = sessions.Where(s => s.DeviceType == DeviceType.Desktop).ToList();

        var sortedSessions = from session in desktopSession
                             orderby session.StartedAtUTC
                             select session;

        return sortedSessions.First();

        throw new NotImplementedException();
    }

    public async Task<List<Session>> GetSessionsAsync()
    {
        DbSet<Session> sessions = (DbSet<Session>)_dbContext.Sessions.AsQueryable();

        var selectedSessions = sessions.Where(s => s.EndedAtUTC.Year < 2025).
                                        Where(s => s.User.Id == s.UserId).
                                        Where(s => s.User.Status == UserStatus.Active).ToList();

        return selectedSessions;

        throw new NotImplementedException();
    }
}
