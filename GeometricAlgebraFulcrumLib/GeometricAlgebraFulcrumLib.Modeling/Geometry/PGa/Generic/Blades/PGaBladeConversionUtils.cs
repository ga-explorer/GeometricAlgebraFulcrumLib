using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Blades;

public static class PGaBladeConversionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> ToProjectiveBlade<T>(this XGaKVector<T> cgaKVector, PGaGeometricSpace<T> pgaGeometricSpace)
    {
        return new PGaBlade<T>(
            pgaGeometricSpace,
            cgaKVector
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> ScalarPartToProjectiveBlade<T>(this XGaMultivector<T> cgaMultivector, PGaGeometricSpace<T> pgaGeometricSpace)
    {
        return new PGaBlade<T>(
            pgaGeometricSpace,
            cgaMultivector.GetScalarPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> VectorPartToProjectiveBlade<T>(this XGaMultivector<T> cgaMultivector, PGaGeometricSpace<T> pgaGeometricSpace)
    {
        return new PGaBlade<T>(
            pgaGeometricSpace,
            cgaMultivector.GetVectorPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> VectorPartToProjectiveVGaBlade<T>(this XGaMultivector<T> cgaMultivector, PGaGeometricSpace<T> pgaGeometricSpace)
    {
        return new PGaBlade<T>(
            pgaGeometricSpace,
            cgaMultivector.GetVectorPart().GetVectorPart(i => i >= 2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> BivectorPartToProjectiveBlade<T>(this XGaMultivector<T> cgaMultivector, PGaGeometricSpace<T> pgaGeometricSpace)
    {
        return new PGaBlade<T>(
            pgaGeometricSpace,
            cgaMultivector.GetBivectorPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> KVectorPartToProjectiveBlade<T>(this XGaMultivector<T> cgaMultivector, int grade, PGaGeometricSpace<T> pgaGeometricSpace)
    {
        return new PGaBlade<T>(
            pgaGeometricSpace,
            cgaMultivector.GetKVectorPart(grade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> FirstKVectorPartToProjectiveBlade<T>(this XGaMultivector<T> cgaMultivector, PGaGeometricSpace<T> pgaGeometricSpace)
    {
        return new PGaBlade<T>(
            pgaGeometricSpace,
            cgaMultivector.GetFirstKVectorPart()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> GetVGaVectorPart<T>(this PGaBlade<T> vector)
    {
        Debug.Assert(vector.IsVector);

        return new PGaBlade<T>(
            vector.GeometricSpace,
            vector.InternalVector.GetVectorPart(i => i >= 2)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> VGaVectorToPGaPoint<T>(this PGaBlade<T> blade)
    {
        Debug.Assert(
            blade.IsVGaVector()
        );

        var pgaGeometricSpace = blade.GeometricSpace;

        return (pgaGeometricSpace.Eo + blade).PGaDual();
    }

}