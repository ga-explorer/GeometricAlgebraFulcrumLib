﻿using System;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.DSLException;

public class ParserException : CompilerException
{
    public ParserException(string message)
        : base(message)
    {
    }

    public ParserException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}