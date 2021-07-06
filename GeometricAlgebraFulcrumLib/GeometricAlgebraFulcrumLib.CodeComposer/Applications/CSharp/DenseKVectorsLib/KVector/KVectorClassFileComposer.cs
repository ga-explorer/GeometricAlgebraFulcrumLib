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
                "frame", CurrentNamespace,
                "double", GaClcLanguage.ScalarTypeName,
                "norm2_opname", GaClcOperationKind.UnaryNormSquared.CreateEuclideanOperationSpecs().GetName()
            );

            GenerateKVectorFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
