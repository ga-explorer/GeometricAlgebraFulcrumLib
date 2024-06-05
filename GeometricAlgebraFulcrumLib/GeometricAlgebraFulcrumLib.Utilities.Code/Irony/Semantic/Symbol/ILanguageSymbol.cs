using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.SourceCode;
using Irony.Parsing;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Symbol;

/// <summary>
/// This is the main interface for language symbols
/// </summary>
public interface ILanguageSymbol : IIronyAstObjectNamed
{
    /// <summary>
    /// The role of the symbol
    /// </summary>
    LanguageRole SymbolRole { get; }

    /// <summary>
    /// A list of parse nodes for the symbol
    /// </summary>
    IEnumerable<ParseTreeNode> ParseNodes { get; }

    /// <summary>
    /// A list of code locations for the symbol
    /// </summary>
    IEnumerable<IronyAstObjectCodeLocation> CodeLocations { get; }

    /// <summary>
    /// The access name for the symbol
    /// </summary>
    string SymbolAccessName { get; }

    /// <summary>
    /// The full qualified name for the symbol
    /// </summary>
    string SymbolQualifiedName { get; }
}