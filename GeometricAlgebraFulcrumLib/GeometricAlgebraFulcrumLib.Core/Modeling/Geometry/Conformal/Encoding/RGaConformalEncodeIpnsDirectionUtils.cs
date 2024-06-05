using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Encoding;

public static class RGaConformalEncodeIpnsDirectionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsDirection(this RGaConformalSpace conformalSpace, LinFloat64Vector2D egaDirectionBlade)
    {
        return conformalSpace.EncodeIpnsDirection(
            conformalSpace.EncodeEGaVector(egaDirectionBlade)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsDirection(this RGaConformalSpace conformalSpace, LinFloat64Bivector2D egaDirectionBlade)
    {
        return conformalSpace.EncodeIpnsDirection(
            conformalSpace.EncodeEGaBivector(egaDirectionBlade)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsDirection(this RGaConformalSpace conformalSpace, LinFloat64Vector3D egaDirectionBlade)
    {
        return conformalSpace.EncodeIpnsDirection(
            conformalSpace.EncodeEGaVector(egaDirectionBlade)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsDirection(this RGaConformalSpace conformalSpace, LinFloat64Bivector3D egaDirectionBlade)
    {
        return conformalSpace.EncodeIpnsDirection(
            conformalSpace.EncodeEGaBivector(egaDirectionBlade)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsDirection(this RGaConformalSpace conformalSpace, LinFloat64Trivector3D egaDirectionBlade)
    {
        return conformalSpace.EncodeIpnsDirection(
            conformalSpace.EncodeEGaTrivector(egaDirectionBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade EncodeIpnsDirection(this RGaConformalSpace conformalSpace, LinFloat64Vector egaDirectionBlade)
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