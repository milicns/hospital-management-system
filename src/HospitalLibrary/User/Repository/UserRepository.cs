using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Settings;

namespace HospitalLibrary.User.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly HospitalDbContext _context;

        public UserRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Model.User> GetAll()
        {
            return _context.Users.ToList();
        }
        
    }
}