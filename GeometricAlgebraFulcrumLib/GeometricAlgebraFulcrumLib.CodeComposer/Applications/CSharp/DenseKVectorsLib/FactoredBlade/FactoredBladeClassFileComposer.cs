using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.FactoredBlade
{
    internal class FactoredBladeClassFileComposer : 
        GaFuLLibraryFileComposerBase
    {
        internal FactoredBladeClassFileComposer(GaFuLLibraryComposer libGen)
            : base(libGen)
        {
        }


        public override void Generate()
        {
            TextComposer.Append(
                Templates["factored_blade"],
                "signature", CurrentNamespace,
                "double", GaLanguage.ScalarTypeName
            );

            FileComposer.FinalizeText();
        }
    }
}
