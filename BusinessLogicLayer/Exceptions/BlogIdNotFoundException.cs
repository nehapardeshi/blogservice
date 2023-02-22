namespace BusinessLogicLayer.Exceptions
{
    public class BlogIdNotFoundException : Exception
    {
        private readonly int _blogId;
        public BlogIdNotFoundException(int blogId)
        {
            _blogId = blogId;
        }

        public override string ToString()
        {
            return $"No blog found with this blogId {_blogId}";
        }
    }
}
