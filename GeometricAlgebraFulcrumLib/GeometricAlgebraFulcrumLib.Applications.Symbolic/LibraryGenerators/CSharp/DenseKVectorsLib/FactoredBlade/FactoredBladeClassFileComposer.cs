﻿using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.DenseKVectorsLib.FactoredBlade;

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
            "double", GeoLanguage.ScalarTypeName
        );

        FileComposer.FinalizeText();
    }
}