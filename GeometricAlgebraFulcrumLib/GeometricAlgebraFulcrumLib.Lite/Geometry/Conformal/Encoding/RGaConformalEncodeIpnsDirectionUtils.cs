using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Encoding;

public static class RGaConformalEncodeIpnsDirectionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsDirection(this RGaConformalSpace conformalSpace, Float64Vector2D egaDirectionBlade)
    {
        return conformalSpace.EncodeIpnsDirection(
            conformalSpace.EncodeEGaVector(egaDirectionBlade)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsDirection(this RGaConformalSpace conformalSpace, Float64Bivector2D egaDirectionBlade)
    {
        return conformalSpace.EncodeIpnsDirection(
            conformalSpace.EncodeEGaBivector(egaDirectionBlade)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsDirection(this RGaConformalSpace conformalSpace, Float64Vector3D egaDirectionBlade)
    {
        return conformalSpace.EncodeIpnsDirection(
            conformalSpace.EncodeEGaVector(egaDirectionBlade)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsDirection(this RGaConformalSpace conformalSpace, Float64Bivector3D egaDirectionBlade)
    {
        return conformalSpace.EncodeIpnsDirection(
            conformalSpace.EncodeEGaBivector(egaDirectionBlade)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsDirection(this RGaConformalSpace conformalSpace, Float64Trivector3D egaDirectionBlade)
    {
        return conformalSpace.EncodeIpnsDirection(
            conformalSpace.EncodeEGaTrivector(egaDirectionBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsDirection(this RGaConformalSpace conformalSpace, Float64Vector egaDirectionBlade)
    {
        return conformalSpace.EncodeIpnsDirection(
            conformalSpace.EncodeEGaVector(egaDirectionBlade)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsDirection(this RGaConformalSpace conformalSpace, RGaFloat64KVector egaDirectionBlade)
    {
        return conformalSpace.EncodeIpnsDirection(
            conformalSpace.EncodeEGaBlade(egaDirectionBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsDirection(this RGaConformalSpace conformalSpace, RGaConformalBlade egaDirectionBlade)
    {
        return -egaDirectionBlade.EGaDual().Op(conformalSpace.Ei);
    }

}