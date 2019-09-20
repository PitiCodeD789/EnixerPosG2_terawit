using System;
using System.Collections.Generic;
using System.Text;
using EnixerPos.Domain.Entities;

namespace EnixerPos.Domain.Repositories
{
    public interface IUserRepository
    {
        UserEntity GetUserByUserName(int userId);
        UserEntity GetUserByEmialAndPin(string email, string hashPin);
    }
}
