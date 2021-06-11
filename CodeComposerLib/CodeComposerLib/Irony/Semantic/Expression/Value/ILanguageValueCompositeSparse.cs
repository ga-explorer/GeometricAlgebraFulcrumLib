using System;
using System.Collections.Generic;

namespace CodeComposerLib.Irony.Semantic.Expression.Value
{
    public interface ILanguageValueCompositeSparse<TK> : ILanguageValueComposite, IDictionary<TK, ILanguageValue> where TK : IComparable<TK>
    {
    }
}
