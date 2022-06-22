using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Models;

namespace Test.Services
{
    public class DatabaseController
    {
        private readonly ApplicationContext _db;

        public DatabaseController(ApplicationContext context)
        {
            _db = context;
        }

        public async Task AddUser(User user)
        {
            await Task.Factory.StartNew(() =>
            {
                _db.Users.Add(user);
                _db.SaveChanges();
            }
            );
        }

        public async Task<User> CreateUser()
        {
            return await Task.Factory.StartNew(() =>
            {
                var user = new User();
                _db.Users.Add(user);
                _db.SaveChanges();
                return user;
            }
            );
        }

        public async Task<User> GetUserByGuid(Guid guid)
        {
            return await Task.Factory.StartNew(() =>
            {
                return _db.Users.FirstOrDefault(user => user.Guid == guid);
            }
            );
        }

        public async Task SignInUser(User user)
        {
            await Task.Factory.StartNew(() =>
            {
                var signIn = new SignIn()
                {
                    SignAt = DateTime.Now,
                    User = user
                };
                _db.SignIns.Add(signIn);
                _db.SaveChanges();
            }
            );
        }
    }
}
