namespace GeometricAlgebraLib.Frames
{
    public interface IGaFrame
    {
        int VSpaceDimension { get; }

        ulong GaSpaceDimension { get; }

        ulong MaxBasisBladeId { get; }

        int GradesCount { get; }
    }
}
