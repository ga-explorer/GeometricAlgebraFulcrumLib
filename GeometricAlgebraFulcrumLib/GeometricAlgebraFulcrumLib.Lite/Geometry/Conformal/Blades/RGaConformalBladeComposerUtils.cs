using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;

public static class RGaConformalBladeComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade ToConformalBlade(this RGaFloat64KVector cgaKVector, RGaConformalSpace conformalSpace)
    {
        return new RGaConformalBlade(
            conformalSpace, 
            cgaKVector
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade ScalarPartToConformalBlade(this RGaFloat64Multivector cgaMultivector, RGaConformalSpace conformalSpace)
    {
        return new RGaConformalBlade(
            conformalSpace, 
            cgaMultivector.GetScalarPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade VectorPartToConformalBlade(this RGaFloat64Multivector cgaMultivector, RGaConformalSpace conformalSpace)
    {
        return new RGaConformalBlade(
            conformalSpace, 
            cgaMultivector.GetVectorPart()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade VectorPartToConformalEGaBlade(this RGaFloat64Multivector cgaMultivector, RGaConformalSpace conformalSpace)
    {
        return new RGaConformalBlade(
            conformalSpace, 
            cgaMultivector.GetVectorPart((int i) => i >= 2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade BivectorPartToConformalBlade(this RGaFloat64Multivector cgaMultivector, RGaConformalSpace conformalSpace)
    {
        return new RGaConformalBlade(
            conformalSpace, 
            cgaMultivector.GetBivectorPart()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade KVectorPartToConformalBlade(this RGaFloat64Multivector cgaMultivector, int grade, RGaConformalSpace conformalSpace)
    {
        return new RGaConformalBlade(
            conformalSpace, 
            cgaMultivector.GetKVectorPart(grade)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade FirstKVectorPartToConformalBlade(this RGaFloat64Multivector cgaMultivector, RGaConformalSpace conformalSpace)
    {
        return new RGaConformalBlade(
            conformalSpace, 
            cgaMultivector.GetFirstKVectorPart()
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeEGaVectorBlade(this IFloat64Vector2D egaKVector, RGaConformalSpace conformalSpace)
    {
        return conformalSpace.EncodeEGaVector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeEGaVectorBlade(this IFloat64Vector3D egaKVector, RGaConformalSpace conformalSpace)
    {
        return conformalSpace.EncodeEGaVector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeEGaVectorBlade(this Float64Vector egaKVector, RGaConformalSpace conformalSpace)
    {
        return conformalSpace.EncodeEGaVector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeEGaVectorBlade(this RGaFloat64Vector egaKVector, RGaConformalSpace conformalSpace)
    {
        return conformalSpace.EncodeEGaVector(egaKVector);
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeEGaBivectorBlade(this Float64Bivector2D egaKVector, RGaConformalSpace conformalSpace)
    {
        return conformalSpace.EncodeEGaBivector(egaKVector);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeEGaBivectorBlade(this Float64Bivector3D egaKVector, RGaConformalSpace conformalSpace)
    {
        return conformalSpace.EncodeEGaBivector(egaKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeEGaBivectorBlade(this RGaFloat64Bivector egaKVector, RGaConformalSpace conformalSpace)
    {
        return conformalSpace.EncodeEGaBivector(egaKVector);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeEGaTrivectorBlade(this Float64Trivector3D egaKVector, RGaConformalSpace conformalSpace)
    {
        return conformalSpace.EncodeEGaTrivector(egaKVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeEGaBlade(this RGaFloat64KVector egaKVector, RGaConformalSpace conformalSpace)
    {
        return conformalSpace.EncodeEGaBlade(egaKVector);
    }

    
}