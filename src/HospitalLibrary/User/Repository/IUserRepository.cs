using System.Collections;
using System.Collections.Generic;

namespace HospitalLibrary.User.Repository
{
    public interface IUserRepository
    {
        IEnumerable<Model.User> GetAll();
        
    }
}