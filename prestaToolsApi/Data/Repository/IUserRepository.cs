﻿namespace prestaToolsApi.Data.Repository
{
    public interface IUserRepository
    {

        Task<IEnumerable<User>> GetAllUser(); 
        Task<User> GetByUserId(int id);
        Task<Boolean>InsertUser(User user);
        Task<Boolean> UpdateUser(User user);
        Task<Boolean> DeleteUser(int id); 
   


    }
}