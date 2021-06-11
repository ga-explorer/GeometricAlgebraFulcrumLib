using System;

namespace CodeComposerLib.Irony.DSLException
{
    //TODO: Implement exceptions in this way
    //[Serializable]
    //public class MyException : Exception
    //{
    //    //
    //    // For guidelines regarding the creation of new exception types, see
    //    //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
    //    // and
    //    //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
    //    //

    //    public MyException()
    //    {
    //    }

    //    public MyException(string message) : base(message)
    //    {
    //    }

    //    public MyException(string message, Exception inner) : base(message, inner)
    //    {
    //    }

    //    protected MyException(
    //        SerializationInfo info,
    //        StreamingContext context) : base(info, context)
    //    {
    //    }
    //}

    public class AstException : DslException
    {
        public AstException(string message)
            : base(message)
        {
        }

        public AstException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

}
