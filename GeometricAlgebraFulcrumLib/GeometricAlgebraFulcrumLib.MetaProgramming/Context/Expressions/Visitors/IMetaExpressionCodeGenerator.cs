using GeometricAlgebraFulcrumLib.Utilities.Code.Languages;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Visitors;

public interface IMetaExpressionCodeGenerator :
    IMetaExpressionDynamicVisitor
{
    LinearTextComposer TextComposer { get; }

    /// <summary>
    /// Target language information
    /// </summary>
    CclLanguageInfo LanguageInfo { get; }

    /// <summary>
    /// The indentation string for this language code composer
    /// </summary>
    string Indentation { get; }
}