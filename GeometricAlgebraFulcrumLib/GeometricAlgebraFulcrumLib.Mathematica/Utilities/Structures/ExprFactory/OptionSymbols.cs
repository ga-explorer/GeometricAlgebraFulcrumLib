﻿using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures.ExprFactory;

public static class OptionSymbols
{
    public static readonly Expr All = new Expr(ExpressionType.Symbol, "All");
}