﻿using System;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.DSLException;

public class CompilerException : DslException
{
    public CompilerException(string message)
        : base(message)
    {
    }

    public CompilerException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}