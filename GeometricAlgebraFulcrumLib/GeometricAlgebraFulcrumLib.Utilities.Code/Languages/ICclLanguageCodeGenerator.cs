using GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Languages;

public interface ICclLanguageCodeGenerator : 
    ISteDynamicVisitor
{
    /// <summary>
    /// Target language information
    /// </summary>
    CclLanguageInfo LanguageInfo { get; }

    /// <summary>
    /// The indentation string for this language code composer
    /// </summary>
    string Indentation { get; }
}