//using System.Diagnostics;
//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;

//namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.PGa.Blades;

//public static class PGaFloat64BladeConversionUtils
//{
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static PGaFloat64Blade ToConformalBlade(this RGaFloat64KVector cgaKVector, PGaFloat64GeometricSpace cgaGeometricSpace)
//    {
//        return new PGaFloat64Blade(
//            cgaGeometricSpace, 
//            cgaKVector
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static PGaFloat64Blade ScalarPartToConformalBlade(this RGaFloat64Multivector cgaMultivector, PGaFloat64GeometricSpace cgaGeometricSpace)
//    {
//        return new PGaFloat64Blade(
//            cgaGeometricSpace, 
//            cgaMultivector.GetScalarPart()
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static PGaFloat64Blade VectorPartToConformalBlade(this RGaFloat64Multivector cgaMultivector, PGaFloat64GeometricSpace cgaGeometricSpace)
//    {
//        return new PGaFloat64Blade(
//            cgaGeometricSpace, 
//            cgaMultivector.GetVectorPart()
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static PGaFloat64Blade VectorPartToConformalVGaBlade(this RGaFloat64Multivector cgaMultivector, PGaFloat64GeometricSpace cgaGeometricSpace)
//    {
//        return new PGaFloat64Blade(
//            cgaGeometricSpace, 
//            cgaMultivector.GetVectorPart((int i) => i >= 2)
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static PGaFloat64Blade BivectorPartToConformalBlade(this RGaFloat64Multivector cgaMultivector, PGaFloat64GeometricSpace cgaGeometricSpace)
//    {
//        return new PGaFloat64Blade(
//            cgaGeometricSpace, 
//            cgaMultivector.GetBivectorPart()
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static PGaFloat64Blade KVectorPartToConformalBlade(this RGaFloat64Multivector cgaMultivector, int grade, PGaFloat64GeometricSpace cgaGeometricSpace)
//    {
//        return new PGaFloat64Blade(
//            cgaGeometricSpace, 
//            cgaMultivector.GetKVectorPart(grade)
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static PGaFloat64Blade FirstKVectorPartToConformalBlade(this RGaFloat64Multivector cgaMultivector, PGaFloat64GeometricSpace cgaGeometricSpace)
//    {
//        return new PGaFloat64Blade(
//            cgaGeometricSpace, 
//            cgaMultivector.GetFirstKVectorPart()
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static PGaFloat64Blade OpnsToIpns(this PGaFloat64Blade blade)
//    {
//        return blade.PGaDual();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static PGaFloat64Blade OpnsToPGa(this PGaFloat64Blade blade)
//    {
//        var kVector = blade.GeometricSpace.MusicalIsomorphism.OmMap(
//            blade.PGaDual().InternalKVector
//        );

//        return new PGaFloat64Blade(
//            blade.GeometricSpace,
//            kVector
//        );
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static PGaFloat64Blade IpnsToOpns(this PGaFloat64Blade blade)
//    {
//        return blade.PGaUnDual();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static PGaFloat64Blade IpnsToPGa(this PGaFloat64Blade blade)
//    {
//        Debug.Assert(blade.IsPGaInfBlade());

//        var kVector =
//            blade.GeometricSpace.MusicalIsomorphism.OmMap(blade.InternalKVector);
        
//        return new PGaFloat64Blade(
//            blade.GeometricSpace,
//            kVector
//        );
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static PGaFloat64Blade PGaToOpns(this PGaFloat64Blade blade)
//    {
//        Debug.Assert(blade.IsPGaBlade());

//        var kVector =
//            blade.PGaToIpns().PGaUnDual().InternalKVector;

//        return new PGaFloat64Blade(
//            blade.GeometricSpace,
//            kVector
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static PGaFloat64Blade PGaToIpns(this PGaFloat64Blade blade)
//    {
//        Debug.Assert(blade.IsPGaBlade());

//        var kVector = 
//            blade.GeometricSpace.MusicalIsomorphism.OmMap(blade.InternalKVector);

//        return new PGaFloat64Blade(
//            blade.GeometricSpace,
//            kVector
//        );
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static PGaFloat64Blade GetVGaVectorPart(this PGaFloat64Blade vector)
//    {
//        Debug.Assert(vector.IsVector);

//        return new PGaFloat64Blade(
//            vector.GeometricSpace,
//            vector.InternalVector.GetVectorPart((int i) => i >= 2)
//        );
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static PGaFloat64Blade VGaVectorToHGaPoint(this PGaFloat64Blade blade)
//    {
//        Debug.Assert(
//            blade.IsVGaVector()
//        );
        
//        var cgaGeometricSpace = blade.GeometricSpace;

//        return cgaGeometricSpace.Eo + blade;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static PGaFloat64Blade VGaVectorToPGaPoint(this PGaFloat64Blade blade)
//    {
//        Debug.Assert(
//            blade.IsVGaVector()
//        );
        
//        var cgaGeometricSpace = blade.GeometricSpace;

//        return (cgaGeometricSpace.Eo + blade).PGaUnDual();
//    }

//    public static PGaFloat64Blade VGaVectorToOpnsFlatPoint(this PGaFloat64Blade blade)
//    {
//        Debug.Assert(
//            blade.IsVGaVector()
//        );

//        var cgaGeometricSpace = blade.GeometricSpace;

//        var p = blade.InternalVector;

//        var kVector = 
//            cgaGeometricSpace.EoiBivector +
//            p.Op(cgaGeometricSpace.EiVector);

//        return new PGaFloat64Blade(cgaGeometricSpace, kVector);
//    }

//    public static PGaFloat64Blade VGaVectorToIpnsPoint(this PGaFloat64Blade blade)
//    {
//        Debug.Assert(
//            blade.IsVGaVector()
//        );

//        var cgaGeometricSpace = blade.GeometricSpace;

//        var p = blade.InternalVector;

//        var kVector = 
//            cgaGeometricSpace.EoVector +
//            p +
//            0.5d * p.NormSquared() * cgaGeometricSpace.EiVector;

//        return new PGaFloat64Blade(cgaGeometricSpace, kVector);
//    }

//}