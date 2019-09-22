using System;
using System.Collections.Generic;
using System.Text;
using EnixerPos.Domain.Entities;

namespace EnixerPos.Domain.Repositories
{
    public interface IUserRepository
    {
        UserEntity GetUserByUserName(string user);
        UserEntity GetUserByEmialAndPin(string email, string hashPin);
    }
}
