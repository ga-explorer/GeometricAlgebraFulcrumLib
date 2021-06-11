﻿using System;

namespace CodeComposerLib.Irony.DSLException
{
    public class AstGeneratorException : CompilerException
    {
        public AstGeneratorException(string message)
            : base(message)
        {
        }

        public AstGeneratorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
