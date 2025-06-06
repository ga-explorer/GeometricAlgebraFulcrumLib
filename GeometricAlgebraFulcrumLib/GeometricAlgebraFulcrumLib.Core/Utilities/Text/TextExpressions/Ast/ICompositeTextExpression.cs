﻿namespace GeometricAlgebraFulcrumLib.Core.Utilities.Text.TextExpressions.Ast;

public interface ICompositeTextExpression : ITextExpression
{
    TeIdentifier Name { get; }

    bool IsNamed { get; }

    bool IsNameless { get; }

    IEnumerable<ITextExpression> ChildExpressions { get; }
}