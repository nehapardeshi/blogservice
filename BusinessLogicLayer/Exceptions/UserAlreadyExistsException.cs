namespace BusinessLogicLayer.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        private readonly string _email;
        public UserAlreadyExistsException(string email)
        {
            _email = email;
        }
        public override string ToString()
        {
            return $"User already exists with this email {_email}";
        }
    }
}
