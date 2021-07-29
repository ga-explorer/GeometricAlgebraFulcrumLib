namespace GeometricAlgebraFulcrumLib.CodeComposer.Composers
{
    /// <summary>
    /// This abstract class can be used to implement a sub-process of string code generation
    /// using the main code library generator components
    /// </summary>
    public abstract class GaCodeTextComposerBase : 
        GaCodePartComposerBase
    {
        protected GaCodeTextComposerBase(IGaCodeComposer codeLibraryComposer)
            : base(codeLibraryComposer)
        {
        }


        public abstract string Generate();
    }
}
