using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Euclidean;

public abstract class RGaEuclideanGeometrySpace :
    RGaGeometrySpace
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
        : base(RGaGeometrySpaceBasisSpecs.CreateEGa(vSpaceDimensions))
    {
        if (vSpaceDimensions < 2)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        E1 = EuclideanProcessor.CreateTermVector(0);
        E2 = EuclideanProcessor.CreateTermVector(1);

        E12 = EuclideanProcessor.CreateTermBivector(0, 1);

        I = EuclideanProcessor.CreateTermHigherKVector(GaSpaceDimensions - 1, 1);
        Iinv = I.Inverse();
        Irev = I.Reverse();
    }

}