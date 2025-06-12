using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;


namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.VGa.Float64;

public class XGaEuclideanGeometrySpace2D :
    XGaEuclideanGeometrySpace
{
    public static XGaEuclideanGeometrySpace2D Instance { get; }
        = new XGaEuclideanGeometrySpace2D();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private XGaEuclideanGeometrySpace2D()
        : base(2)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector EncodeVector(double x, double y)
    {
        return EuclideanProcessor.Vector(x, y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector EncodeBivector(double xyScalar)
    {
        return Processor.BivectorTerm(0, 1, xyScalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector EncodeComplex(double scalar, double iScalar)
    {
        return Processor
            .CreateMultivectorComposer()
            .SetScalarTerm(scalar)
            .SetBivectorTerm(0, 1, -iScalar)
            .GetMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Complex DecodeComplex(XGaFloat64Multivector mv)
    {
        return new Complex(
            mv.Scalar(),
            -mv.Scalar(0, 1)
        );
    }


}