using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.VGa.Float64;

public class XGaEuclideanGeometrySpace3D :
    XGaEuclideanGeometrySpace
{
    public static XGaEuclideanGeometrySpace3D Instance { get; }
        = new XGaEuclideanGeometrySpace3D();


    public XGaFloat64Vector E3 { get; }

    public XGaFloat64Bivector E13 { get; }

    public XGaFloat64Bivector E23 { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private XGaEuclideanGeometrySpace3D() : base(3)
    {
        E3 = EuclideanProcessor.VectorTerm(2);

        E13 = EuclideanProcessor.BivectorTerm(0, 2);
        E23 = EuclideanProcessor.BivectorTerm(1, 2);

    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector EncodeVector(double x, double y, double z)
    {
        return Processor.Vector(x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector EncodeBivector(double xy, double xz, double yz)
    {
        return Processor.Bivector(
            new Dictionary<IndexSet, double>
            {
                {(IndexSet)3UL, xy},
                {(IndexSet)5UL, xz},
                {(IndexSet)6UL, yz}
            }
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector EncodeBivector(LinFloat64Bivector3D bivector)
    {
        return Processor.Bivector(
            new Dictionary<IndexSet, double>
            {
                {(IndexSet) 3UL, bivector.Xy},
                {(IndexSet) 5UL, bivector.Xz},
                {(IndexSet) 6UL, bivector.Yz}
            }
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector EncodeTrivector(double s)
    {
        return Processor.HigherKVector(
            3,
            new SingleItemDictionary<IndexSet, double>((IndexSet)7UL, s)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector EncodeQuaternion(double scalar, double iScalar, double jScalar, double kScalar)
    {
        return Processor
            .CreateMultivectorComposer()
            .SetScalarTerm(scalar)
            .SetBivectorTerm(0, 1, -kScalar)
            .SetBivectorTerm(0, 2, jScalar)
            .SetBivectorTerm(1, 2, -iScalar)
            .GetMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Quaternion DecodeQuaternion(XGaFloat64Multivector mv)
    {
        return LinFloat64Quaternion.Create(mv.Scalar(), -mv[1, 2], mv[0, 2], -mv[0, 1]);
    }


}