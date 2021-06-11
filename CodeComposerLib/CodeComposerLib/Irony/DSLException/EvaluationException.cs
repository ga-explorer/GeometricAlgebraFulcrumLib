﻿using System;

namespace CodeComposerLib.Irony.DSLException
{
    public class EvaluationException : DslException
    {
        public EvaluationException(string message)
            : base(message)
        {
        }

        public EvaluationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
