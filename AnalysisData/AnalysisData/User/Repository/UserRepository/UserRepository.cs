﻿using AnalysisData.Data;
using AnalysisData.User.Repository.UserRepository.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace AnalysisData.User.Repository.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Model.User> GetUserByUsernameAsync(string userName)
        {
            return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(x => x.Username == userName);
        }

        public async Task<Model.User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(x => x.Email == email);
        }


        public async Task<Model.User> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<List<Model.User>> GetAllUserPaginationAsync(int page, int limit)
        {
            return await _context.Users.Include(u => u.Role).Skip((page) * limit).Take(limit).ToListAsync();
        }

        public async Task<int> GetUsersCountAsync()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await GetUserByIdAsync(id);
            if (user == null) return false;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddUserAsync(Model.User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateUserAsync(Guid id, Model.User newUser)
        {
            var user = await GetUserByIdAsync(id);
            newUser.Id = user.Id;
            _context.Users.Update(newUser);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Model.User>> GetTopUsersByUsernameSearchAsync(string username)
        {
            return await _context.Users.Include(u => u.Role)
                .Where(x => x.Username.Contains(username) && x.Role.RoleName != "dataanalyst").Take(10).ToListAsync();
        }
    }
}