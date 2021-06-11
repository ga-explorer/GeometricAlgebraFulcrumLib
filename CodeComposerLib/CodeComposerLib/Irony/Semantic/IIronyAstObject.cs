namespace CodeComposerLib.Irony.Semantic
{
    public interface IIronyAstObject
    {
        /// <summary>
        /// The parent Irony DSL where the object is stored
        /// </summary>
        IronyAst RootAst { get; }
    }
}
