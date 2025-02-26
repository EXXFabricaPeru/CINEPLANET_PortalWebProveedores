
using System.Collections.Generic;
using WebProov_API.Models;

namespace WebProov_API.Data.Interfaces
{
    public interface ILoginRepository
    {
        public User GetLogin(User user);
        public bool ChangePassword(User user);

    }
}