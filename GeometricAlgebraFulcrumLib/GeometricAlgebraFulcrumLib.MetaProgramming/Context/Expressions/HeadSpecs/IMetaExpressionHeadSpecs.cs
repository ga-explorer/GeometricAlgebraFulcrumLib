﻿namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.HeadSpecs;

/// <summary>
/// Any expression header must be an immutable class that implements this interface
/// </summary>
public interface IMetaExpressionHeadSpecs :
    IMetaExpressionElement
{
    string HeadText { get; }

    bool IsNumber { get; }

    bool IsSymbolicNumber { get; }

    bool IsLiteralNumber { get; }

    bool IsSymbolicNumberOrVariable { get; }

    bool IsVariable { get; }

    bool IsAtomic { get; }

    bool IsComposite { get; }

    bool IsFunction { get; }

    bool IsOperator { get; }

    bool IsArrayAccess { get; }
}