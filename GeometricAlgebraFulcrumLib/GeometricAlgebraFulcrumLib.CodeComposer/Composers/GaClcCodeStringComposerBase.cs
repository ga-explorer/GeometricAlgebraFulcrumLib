namespace GeometricAlgebraFulcrumLib.CodeComposer.Composers
{
    /// <summary>
    /// This abstract class can be used to implement a sub-process of string code generation
    /// using the main code library generator components
    /// </summary>
    public abstract class GaClcCodeStringComposerBase : 
        GaClcCodePartComposerBase
    {
        protected GaClcCodeStringComposerBase(GaCodeLibraryComposerBase codeLibraryComposer)
            : base(codeLibraryComposer)
        {
        }


        public abstract string Generate();
    }
}
