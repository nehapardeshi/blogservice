using BusinessLogicLayer.Models;

namespace BusinessLogicLayer
{
    public interface IUserService
    {
        // Create new User
        Task<User> AddUserAsync(string firstName, string lastName, string email, string password);
        // Login User
        User? LoginUser(string email, string password);
        //// Update Password
        User? GetUser(int id);
        Task<bool> UpdatePasswordAsync(string email, string newPassword);
        //void EditPassword(string newPassword);
        //Validate new User
        //User Validate User(string User user);
        //Save new User
        //void Save();    

    }
}
