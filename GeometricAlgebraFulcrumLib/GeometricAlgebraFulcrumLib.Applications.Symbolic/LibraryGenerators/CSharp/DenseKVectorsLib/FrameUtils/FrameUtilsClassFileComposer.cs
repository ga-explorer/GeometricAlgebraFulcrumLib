using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.DenseKVectorsLib.FrameUtils;

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
            "vspacedim", VSpaceDimensions
        );

        FileComposer.FinalizeText();
    }
}