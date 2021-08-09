namespace GeometricAlgebraFulcrumLib.Algebra
{
    public interface IGaSpaceElement
    {
        IGaSpace Space { get; }

        uint VSpaceDimension { get; }

        ulong GaSpaceDimension { get; }
    }
}