using TextComposerLib.Text.Linear;

namespace GeometricAlgebraLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.FrameUtils
{
    internal class FrameUtilsClassFileComposer : 
        GaLibraryFileComposerBase 
    {
        internal FrameUtilsClassFileComposer(GaLibraryComposer libGen)
            : base(libGen)
        {
        }


        public override void Generate()
        {
            TextComposer.Append(
                Templates["frame_utils"],
                "frame", CurrentNamespace,
                "vspacedim", VSpaceDimension
            );

            FileComposer.FinalizeText();
        }
    }
}
