using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.Multivector
{
    internal class MultivectorClassFileComposer : 
        GaFuLLibraryFileComposerBase 
    {
        internal MultivectorClassFileComposer(GaFuLLibraryComposer libGen)
            : base(libGen)
        {
        }


        public override void Generate()
        {
            TextComposer.Append(Templates["multivector"],
                "signature", CurrentNamespace,
                "double", GeoLanguage.ScalarTypeName,
                "grades_count", VSpaceDimension + 1
            );

            FileComposer.FinalizeText();
        }
    }
}
