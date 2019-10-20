using System;

namespace ComicVineApi.Models
{
    public class ComicVineException : Exception
    {
        public ComicVineException()
        {
        }

        public ComicVineException(string message)
            : base(message)
        {
        }

        public ComicVineException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
