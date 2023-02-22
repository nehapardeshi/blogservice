using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Models;

namespace BusinessLogicLayer
{
    public class UserService : IUserService
    {
        private readonly BlogDbContext _context;
        public UserService(BlogDbContext context)
        {
            _context = context;
        }

        public User? LoginUser(string email, string password)
        {
            return _context?.Users?.FirstOrDefault(
                    u => u.Email.Trim().ToUpper() == email.Trim().ToUpper()
                    && u.Password == password);
        }

        public async Task<bool> UpdatePasswordAsync(string email, string newPassword)
        {
            // Find first user from database
            var user = _context.Users.FirstOrDefault(u => u.Email.Trim().ToUpper()
                                                      == email.Trim().ToUpper());
            if (user == null)
                throw new UserNotFoundException(email);

            // If user found update password
            user.Password = newPassword;
            await _context.SaveChangesAsync();

            return true;
        }

        // Create new User
        public async Task<User> AddUserAsync(string firstName, string lastName, string email, string password)
        {
            // Check email first if it is valid
            var isEmailValid = IsEmailValid(email);

            if (!isEmailValid)
            {
                throw new EmailNotValidException($"This email {email} is not valid!");
            }

            // Check if user exists already
            var existingUser = _context.Users.FirstOrDefault(u => u.Email.Trim().ToUpper() == email.Trim().ToUpper());
            if (existingUser != null)
                throw new UserAlreadyExistsException($"This user exists already{email}, " +
                    "Please login instead of making new user!");

            // Add new User
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public User? GetUser(int id) => _context?.Users?.FirstOrDefault(u => u.UserId == id);

        private static bool IsEmailValid(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}
