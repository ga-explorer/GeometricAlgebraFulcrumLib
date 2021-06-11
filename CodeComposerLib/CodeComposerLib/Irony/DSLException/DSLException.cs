using System;

namespace CodeComposerLib.Irony.DSLException
{
    public abstract class DslException : Exception
    {
        protected DslException(string message)
            : base(message)
        {
        }

        protected DslException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
