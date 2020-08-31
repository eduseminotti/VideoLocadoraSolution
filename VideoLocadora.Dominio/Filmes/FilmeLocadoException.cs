using System;
using System.Runtime.Serialization;

namespace VideoLocadora.Dominio.Filmes
{
    [Serializable]
    public class FilmeLocadoException : Exception
    {
        public FilmeLocadoException()
        {
        }

        public FilmeLocadoException(string message) : base(message)
        {
        }

        public FilmeLocadoException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FilmeLocadoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}