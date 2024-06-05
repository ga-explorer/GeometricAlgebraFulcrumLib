namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic;

public interface IIronyAstObject
{
    /// <summary>
    /// The parent Irony DSL where the object is stored
    /// </summary>
    IronyAst RootAst { get; }
}