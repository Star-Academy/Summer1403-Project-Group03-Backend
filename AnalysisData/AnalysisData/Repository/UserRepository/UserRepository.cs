﻿using AnalysisData.Data;
using AnalysisData.Repository.UserRepository.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace AnalysisData.Repository.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<User> GetUser(string userName)
        {
            return _context.Users.FirstOrDefault(x => x.Username == userName);
        }
        
        public async Task<IReadOnlyList<User>> GetAllUser()
        {
            return await _context.Users.ToListAsync();
        }
        
        public bool DeleteUser(string userName)
        {
            var user = _context.Users.FirstOrDefault(x => x.Username == userName);
            if (user == null) return false;
            _context.Users.Remove(user);
            _context.SaveChanges(); 
            return true;
        }
        
        public bool AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges(); 
            return true;
        }
    }
}