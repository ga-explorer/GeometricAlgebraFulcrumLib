using System;
using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Expression.Value;

public interface ILanguageValueCompositeSparse<TK> : ILanguageValueComposite, IDictionary<TK, ILanguageValue> where TK : IComparable<TK>
{
}