namespace BusinessLogicLayer.Exceptions
{
    public class EmailNotValidException : Exception
    {
        private readonly string _email;
        public EmailNotValidException(string email)
        {
            _email = email;
        }
        public override string ToString()
        {
            return $"This is not a valid email {_email}!";
        }
    }
}
