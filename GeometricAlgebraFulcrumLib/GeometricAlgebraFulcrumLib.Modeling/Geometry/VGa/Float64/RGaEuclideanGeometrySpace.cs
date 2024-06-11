using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.VGa.Float64;

public abstract class RGaEuclideanGeometrySpace :
    GaFloat64GeometricSpace
{
    public RGaFloat64EuclideanProcessor EuclideanProcessor
        => RGaFloat64EuclideanProcessor.Instance;

    public RGaFloat64Vector E1 { get; }

    public RGaFloat64Vector E2 { get; }

    public RGaFloat64Bivector E12 { get; }

    public RGaFloat64HigherKVector I { get; }

    public RGaFloat64HigherKVector Iinv { get; }

    public RGaFloat64HigherKVector Irev { get; }


    protected RGaEuclideanGeometrySpace(int vSpaceDimensions)
        : base(GaFloat64GeometricSpaceBasisSpecs.CreateEGa(vSpaceDimensions))
    {
        if (vSpaceDimensions < 2)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        E1 = EuclideanProcessor.VectorTerm(0);
        E2 = EuclideanProcessor.VectorTerm(1);

        E12 = EuclideanProcessor.BivectorTerm(0, 1);

        I = EuclideanProcessor.HigherKVectorTerm(GaSpaceDimensions - 1, 1);
        Iinv = I.Inverse();
        Irev = I.Reverse();
    }

}