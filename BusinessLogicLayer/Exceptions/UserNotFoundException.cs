namespace BusinessLogicLayer.Exceptions
{
    public class UserNotFoundException : Exception
    {
        private readonly string _email;
        public UserNotFoundException(string email)
        {
            _email = email;
        }

        private int _userId = 0;

        public UserNotFoundException(int userId)
        {
            _email = string.Empty;
            _userId = userId;
        }
        public override string ToString()
        {
            if (_userId == 0)
                return $"User not found with email {_email}";
            else
                return $"User not found with Id {_userId}";
        }
    }
}
