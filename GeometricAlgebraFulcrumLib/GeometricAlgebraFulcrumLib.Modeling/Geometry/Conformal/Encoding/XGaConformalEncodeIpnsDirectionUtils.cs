using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;

public static class XGaConformalEncodeIpnsDirectionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsDirection<T>(this XGaConformalSpace<T> conformalSpace, LinVector2D<T> egaDirectionBlade)
    {
        return conformalSpace.EncodeIpnsDirection(
            conformalSpace.EncodeEGaVector(egaDirectionBlade)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsDirection<T>(this XGaConformalSpace<T> conformalSpace, LinBivector2D<T> egaDirectionBlade)
    {
        return conformalSpace.EncodeIpnsDirection(
            conformalSpace.EncodeEGaBivector(egaDirectionBlade)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsDirection<T>(this XGaConformalSpace<T> conformalSpace, LinVector3D<T> egaDirectionBlade)
    {
        return conformalSpace.EncodeIpnsDirection(
            conformalSpace.EncodeEGaVector(egaDirectionBlade)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsDirection<T>(this XGaConformalSpace<T> conformalSpace, LinBivector3D<T> egaDirectionBlade)
    {
        return conformalSpace.EncodeIpnsDirection(
            conformalSpace.EncodeEGaBivector(egaDirectionBlade)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsDirection<T>(this XGaConformalSpace<T> conformalSpace, LinTrivector3D<T> egaDirectionBlade)
    {
        return conformalSpace.EncodeIpnsDirection(
            conformalSpace.EncodeEGaTrivector(egaDirectionBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsDirection<T>(this XGaConformalSpace<T> conformalSpace, LinVector<T> egaDirectionBlade)
    {
        return conformalSpace.EncodeIpnsDirection(
            conformalSpace.EncodeEGaVector(egaDirectionBlade)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsDirection<T>(this XGaConformalSpace<T> conformalSpace, XGaKVector<T> egaDirectionBlade)
    {
        return conformalSpace.EncodeIpnsDirection(
            conformalSpace.EncodeEGaBlade(egaDirectionBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> EncodeIpnsDirection<T>(this XGaConformalSpace<T> conformalSpace, XGaConformalBlade<T> egaDirectionBlade)
    {
        return -egaDirectionBlade.EGaDual().Op(conformalSpace.Ei);
    }

}