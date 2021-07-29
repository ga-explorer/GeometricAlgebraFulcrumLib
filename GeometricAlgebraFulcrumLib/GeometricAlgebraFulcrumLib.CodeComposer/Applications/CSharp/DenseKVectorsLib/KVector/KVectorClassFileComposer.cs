using GeometricAlgebraFulcrumLib.CodeComposer.Languages;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.KVector
{
    /// <summary>
    /// Generate the main code file for the blade class
    /// </summary>
    internal sealed class KVectorClassFileComposer : 
        GaLibraryFileComposerBase
    {
        internal KVectorClassFileComposer(GaLibraryComposer libGen)
            : base(libGen)
        {
        }


        public override void Generate()
        {
            GenerateKVectorFileStartCode();

            TextComposer.Append(
                Templates["kvector"],
                "signature", CurrentNamespace,
                "double", GaLanguage.ScalarTypeName,
                "norm2_opname", GaLanguageOperationKind.UnaryNormSquared.CreateEuclideanOperationSpecs().GetName()
            );

            GenerateKVectorFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
