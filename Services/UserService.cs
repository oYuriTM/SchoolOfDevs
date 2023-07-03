using Microsoft.EntityFrameworkCore;
using SchoolOfDevs.Controllers;
using SchoolOfDevs.Entities;
using SchoolOfDevs.Helpers;

namespace SchoolOfDevs.Services
{
    public interface IUserService
    {
        public Task<User> Create(User user);
        public Task<User> GetById(int id);
        public Task<List<User>> GetAll();
        public Task Update(User userIn, int id);
        public Task Delete(int id);
        Task<object?> Create(UserController user);
    }
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }
        public async Task<User> Create(User user)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            User userDb = await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.UserName == user.UserName);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            if (userDb is not null)
                throw new Exception($"UserName {user.UserName} already exist");

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public Task<object?> Create(UserController user)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id)
        {
            User userDb = await _context.Users.
                SingleOrDefaultAsync(u => u.Id == id);

            if (userDb is null)
                throw new Exception($"User {id} not found");

            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAll() => await _context.Users.ToListAsync();

        public async Task<User> GetById(int id)
        {
            User userDb = await _context.Users.
                SingleOrDefaultAsync(u => u.Id == id);

            if (userDb is null)
                throw new Exception($"User {id} not found");

            return userDb;
        }

        public async Task Update(User userIn, int id)
        {
            if (userIn.Id != id)
                throw new Exception("Route is different from User id");

            User userDb = await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == id);

            if (userDb is null)
                throw new Exception($"UserName {id} not found");

            _context.Entry(userIn).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
