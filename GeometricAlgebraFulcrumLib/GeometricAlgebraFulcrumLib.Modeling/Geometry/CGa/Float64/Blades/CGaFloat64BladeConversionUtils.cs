using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;

public static class CGaFloat64BladeConversionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade ToConformalBlade(this XGaFloat64KVector cgaKVector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return new CGaFloat64Blade(
            cgaGeometricSpace,
            cgaKVector
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade ScalarPartToConformalBlade(this XGaFloat64Multivector cgaMultivector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return new CGaFloat64Blade(
            cgaGeometricSpace,
            cgaMultivector.GetScalarPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade VectorPartToConformalBlade(this XGaFloat64Multivector cgaMultivector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return new CGaFloat64Blade(
            cgaGeometricSpace,
            cgaMultivector.GetVectorPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade VectorPartToConformalVGaBlade(this XGaFloat64Multivector cgaMultivector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return new CGaFloat64Blade(
            cgaGeometricSpace,
            cgaMultivector.GetVectorPart((int i) => i >= 2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade BivectorPartToConformalBlade(this XGaFloat64Multivector cgaMultivector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return new CGaFloat64Blade(
            cgaGeometricSpace,
            cgaMultivector.GetBivectorPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade KVectorPartToConformalBlade(this XGaFloat64Multivector cgaMultivector, int grade, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return new CGaFloat64Blade(
            cgaGeometricSpace,
            cgaMultivector.GetKVectorPart(grade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade FirstKVectorPartToConformalBlade(this XGaFloat64Multivector cgaMultivector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return new CGaFloat64Blade(
            cgaGeometricSpace,
            cgaMultivector.GetFirstKVectorPart()
        );
    }

}