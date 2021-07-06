using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.Multivector
{
    internal class MultivectorClassFileComposer : 
        GaLibraryFileComposerBase 
    {
        internal MultivectorClassFileComposer(GaLibraryComposer libGen)
            : base(libGen)
        {
        }


        public override void Generate()
        {
            TextComposer.Append(Templates["multivector"],
                "frame", CurrentNamespace,
                "double", GaClcLanguage.ScalarTypeName,
                "grades_count", VSpaceDimension + 1
            );

            FileComposer.FinalizeText();
        }
    }
}
