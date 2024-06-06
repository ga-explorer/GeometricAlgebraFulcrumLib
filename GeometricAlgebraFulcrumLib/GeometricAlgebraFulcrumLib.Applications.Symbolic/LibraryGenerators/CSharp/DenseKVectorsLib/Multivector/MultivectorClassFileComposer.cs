using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.DenseKVectorsLib.Multivector;

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
            "grades_count", VSpaceDimensions + 1
        );

        FileComposer.FinalizeText();
    }
}