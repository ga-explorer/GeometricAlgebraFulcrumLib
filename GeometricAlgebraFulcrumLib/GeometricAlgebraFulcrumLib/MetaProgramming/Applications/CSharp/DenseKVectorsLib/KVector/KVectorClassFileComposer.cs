﻿using GeometricAlgebraFulcrumLib.MetaProgramming.Languages;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.DenseKVectorsLib.KVector;

/// <summary>
/// Generate the main code file for the blade class
/// </summary>
internal sealed class KVectorClassFileComposer : 
    GaFuLLibraryFileComposerBase
{
    internal KVectorClassFileComposer(GaFuLLibraryComposer libGen)
        : base(libGen)
    {
    }


    public override void Generate()
    {
        GenerateKVectorFileStartCode();

        TextComposer.Append(
            Templates["kvector"],
            "signature", CurrentNamespace,
            "double", GeoLanguage.ScalarTypeName,
            "norm2_opname", GaFuLLanguageOperationKind.UnaryNormSquared.CreateEuclideanOperationSpecs().GetName()
        );

        GenerateKVectorFileFinishCode();

        FileComposer.FinalizeText();
    }
}