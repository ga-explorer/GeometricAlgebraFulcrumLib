using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.VGa.Float64;

public abstract class XGaEuclideanGeometrySpace :
    GaFloat64GeometricSpace
{
    public XGaFloat64EuclideanProcessor EuclideanProcessor
        => XGaFloat64EuclideanProcessor.Instance;

    public XGaFloat64Vector E1 { get; }

    public XGaFloat64Vector E2 { get; }

    public XGaFloat64Bivector E12 { get; }

    public XGaFloat64HigherKVector I { get; }

    public XGaFloat64HigherKVector Iinv { get; }

    public XGaFloat64HigherKVector Irev { get; }


    protected XGaEuclideanGeometrySpace(int vSpaceDimensions)
        : base(GaFloat64GeometricSpaceBasisSpecs.CreateVGa(vSpaceDimensions))
    {
        if (vSpaceDimensions < 2)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        E1 = EuclideanProcessor.VectorTerm(0);
        E2 = EuclideanProcessor.VectorTerm(1);

        E12 = EuclideanProcessor.BivectorTerm(0, 1);

        I = EuclideanProcessor.HigherKVectorTerm((IndexSet)(GaSpaceDimensions - 1), 1);
        Iinv = I.Inverse();
        Irev = I.Reverse();
    }

}