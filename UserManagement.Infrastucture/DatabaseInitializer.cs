using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Core.Entities;

namespace UserManagement.Infrastucture
{
    public interface IDatabaseInitializer
    {
        Task InitDbData();
    }
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly ApplicationDbContext _dbContext;

        public DatabaseInitializer(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task InitDbData()
        {
            await _dbContext.Database.EnsureCreatedAsync().ConfigureAwait(false);

            /* if (!_dbContext.Users.Any())
            {
                var user1 = new User
                {
                    FullName = "Test User 1",
                    MobileNumber = "+919000000000",
                    Username = "testuser1",
                };
                _dbContext.Users.Add(user1);
                _dbContext.SaveChanges();
            } */

        }
    }
}
