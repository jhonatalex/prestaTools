namespace prestaToolsApi.Data.Repository
{
    public class UserRepositoryImp : InterfaceUserRepository
    {



        Task<bool> InterfaceUserRepository.InsertUser(User user)
        {
            throw new NotImplementedException();
        }



        Task<bool> InterfaceUserRepository.DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<User>> InterfaceUserRepository.GetAllUser()
        {
            throw new NotImplementedException();
        }

        Task<User> InterfaceUserRepository.GetByUserId(int id)
        {
            throw new NotImplementedException();
        }


        Task<bool> InterfaceUserRepository.UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
