using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.FrameUtils
{
    internal class FrameUtilsClassFileComposer : 
        GaFuLLibraryFileComposerBase 
    {
        internal FrameUtilsClassFileComposer(GaFuLLibraryComposer libGen)
            : base(libGen)
        {
        }


        public override void Generate()
        {
            TextComposer.Append(
                Templates["frame_utils"],
                "signature", CurrentNamespace,
                "vspacedim", VSpaceDimension
            );

            FileComposer.FinalizeText();
        }
    }
}
