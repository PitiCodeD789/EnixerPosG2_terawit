using System;
using System.Collections.Generic;
using System.Text;
using EnixerPos.Domain.Entities;

namespace EnixerPos.Domain.Repositories
{
    public interface IUserRepository
    {
        UserEntity GetUserByUserName(string user);
        UserEntity GetUserByEmialAndPin(int storeId, string hashPin);
        bool CreateUserInStore(int storeId, string pin, string nameUser);
        UserEntity GetUserByEmialAndUser(int storeId, string user);
        List<UserEntity> GetAllUserInStore(int storeId);
        bool DeleteUserByEmialAndUser(int storeId, string user);
        bool EditUserInStore(int storeId, string pin, string nameUser, string oldUser);
    }
}
