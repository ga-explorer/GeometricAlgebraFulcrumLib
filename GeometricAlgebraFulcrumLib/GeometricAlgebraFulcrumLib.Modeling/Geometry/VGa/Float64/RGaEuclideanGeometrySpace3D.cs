using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.VGa.Float64;

public class RGaEuclideanGeometrySpace3D :
    RGaEuclideanGeometrySpace
{
    public static RGaEuclideanGeometrySpace3D Instance { get; }
        = new RGaEuclideanGeometrySpace3D();


    public RGaFloat64Vector E3 { get; }

    public RGaFloat64Bivector E13 { get; }

    public RGaFloat64Bivector E23 { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RGaEuclideanGeometrySpace3D() : base(3)
    {
        E3 = EuclideanProcessor.VectorTerm(2);

        E13 = EuclideanProcessor.BivectorTerm(0, 2);
        E23 = EuclideanProcessor.BivectorTerm(1, 2);

    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector EncodeVector(double x, double y, double z)
    {
        return Processor.Vector(x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector EncodeBivector(double xy, double xz, double yz)
    {
        return Processor.Bivector(
            new Dictionary<ulong, double>
            {
                {3UL, xy},
                {5UL, xz},
                {6UL, yz}
            }
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector EncodeBivector(LinFloat64Bivector3D bivector)
    {
        return Processor.Bivector(
            new Dictionary<ulong, double>
            {
                {3UL, bivector.Xy},
                {5UL, bivector.Xz},
                {6UL, bivector.Yz}
            }
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64HigherKVector EncodeTrivector(double s)
    {
        return Processor.HigherKVector(
            3,
            new SingleItemDictionary<ulong, double>(7UL, s)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector EncodeQuaternion(double scalar, double iScalar, double jScalar, double kScalar)
    {
        return Processor
            .CreateComposer()
            .SetTerm(0UL, scalar)
            .SetTerm(3UL, -kScalar)
            .SetTerm(5UL, jScalar)
            .SetTerm(6UL, -iScalar)
            .GetSimpleMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Quaternion DecodeQuaternion(RGaFloat64Multivector mv)
    {
        return LinFloat64Quaternion.Create(
            -mv[1, 2],
            mv[0, 2],
            -mv[0, 1],
            mv.Scalar()
        );
    }


}