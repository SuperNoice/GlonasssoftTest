using Microsoft.EntityFrameworkCore;
using System;
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
                    SignAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    User = user
                };
                _db.SignIns.Add(signIn);
                _db.SaveChanges();
            }
            );
        }

        public async Task AddUserStatisticsRequest(Request request)
        {
            await Task.Factory.StartNew(() =>
            {
                _db.Requests.Add(request);
                _db.SaveChanges();
            }
            );
        }

        public async Task<Request> GetRequestByGuid(Guid guid)
        {
            return await Task.Factory.StartNew(() =>
            {
                return _db.Requests.Include(request => request.User).FirstOrDefault(request => request.Guid == guid);
            }
            );
        }

        public async Task<int> GetUserSignInsCount(Guid userGuid, long from, long to)
        {
            return await Task.Factory.StartNew(() =>
            {
                return _db.SignIns.Include(singin => singin.User).Count(row => row.User.Guid == userGuid && row.SignAt >= from && row.SignAt <= to);
            }
            );
        }
    }
}
