﻿namespace CodeComposerLib.Irony.Semantic.Expression.Value
{
    public interface ILanguageValue : ILanguageExpressionAtomic
    {
        ILanguageValue DuplicateValue(bool deepCopy);
    }
}
