using BusinessLogicLayer;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Models;

namespace BlogTest.Mock
{
    public class UserServiceMock : IUserService
    {
        private readonly List<User> _users;
        public UserServiceMock()
        {
            _users = new List<User>()
            {
                new User { UserId = 1, Email = "userone@test.com", FirstName = "User", LastName = "one", Password = "user1" },
                new User { UserId = 2, Email = "usertwo@test.com", FirstName = "User", LastName = "two", Password = "user2" }
            };
        }

        public async Task<User> AddUserAsync(string firstName, string lastName, string email, string password)
        {
            var userId = _users.Count + 1;
            var user = new User { UserId = userId, Email = email, FirstName = firstName, LastName = lastName, Password = password };
            _users.Add(user);
            return user;
        }

        public User? GetUser(int id) => _users?.FirstOrDefault(u => u.UserId == id);

        public User? LoginUser(string email, string password)
        {
            return _users?.FirstOrDefault(
                    u => u.Email.Trim().ToUpper() == email.Trim().ToUpper()
                    && u.Password == password);
        }

        public async Task<bool> UpdatePasswordAsync(string email, string newPassword)
        {
            var user = _users?.FirstOrDefault(u => u.Email == email);

            if (user == null)
                throw new UserNotFoundException(email);

            user.Password = newPassword;
            return true;
        }
    }
}
