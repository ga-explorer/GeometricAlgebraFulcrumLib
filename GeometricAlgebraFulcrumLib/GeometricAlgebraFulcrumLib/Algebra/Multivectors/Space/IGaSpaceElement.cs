namespace GeometricAlgebraFulcrumLib.Algebra.Multivectors.Space
{
    public interface IGaSpaceElement
    {
        IGaSpace Space { get; }

        uint VSpaceDimension { get; }

        ulong GaSpaceDimension { get; }
    }
}