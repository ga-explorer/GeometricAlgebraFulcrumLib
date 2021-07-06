using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.FactoredBlade
{
    internal class FactoredBladeClassFileComposer : 
        GaLibraryFileComposerBase
    {
        internal FactoredBladeClassFileComposer(GaLibraryComposer libGen)
            : base(libGen)
        {
        }


        public override void Generate()
        {
            TextComposer.Append(
                Templates["factored_blade"],
                "frame", CurrentNamespace,
                "double", GaClcLanguage.ScalarTypeName
            );

            FileComposer.FinalizeText();
        }
    }
}
