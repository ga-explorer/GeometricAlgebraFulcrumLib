using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Scope;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic;

public interface IIronyAstObjectWithScope : IIronyAstObject
{
    /// <summary>
    /// The child scope of some Irony DSL object that may have a single child scope
    /// For example a block command, a class definition, a procedure, a composite expression, etc.
    /// </summary>
    LanguageScope ChildScope { get; }
}