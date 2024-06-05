using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Operations;

public static class XGaConformalRotationUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalElement<T> RotateUsing<T>(this XGaConformalElement<T> element, LinAngle<T> angle, LinVector2D<T> egaAxisPoint)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint)
            .DecodeOpnsElement();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalElement<T> RotateUsing<T>(this XGaConformalElement<T> element, LinAngle<T> angle, LinVector3D<T> egaAxisPoint, LinVector3D<T> egaAxisVector)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint, egaAxisVector)
            .DecodeOpnsElement();
    }
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> RotateUsing<T>(this XGaConformalElement<T> element, IParametricAngle angle, LinVector2D<T> egaAxisPoint)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        element.ConformalSpace,
    //        angle.ParameterRange,
    //        t => element.RotateUsing(
    //            angle.GetAngle(t),
    //            egaAxisPoint
    //        )
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> RotateUsing<T>(this XGaConformalElement<T> element, IParametricAngle angle, IFloat64ParametricCurve2D egaAxisPoint)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        element.ConformalSpace,
    //        angle.ParameterRange
    //            .Intersect(egaAxisPoint.ParameterRange),
    //        t => element.RotateUsing(
    //            angle.GetAngle(t),
    //            egaAxisPoint.GetPoint(t)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> RotateUsing<T>(this XGaConformalElement<T> element, IParametricAngle angle, LinVector3D<T> egaAxisPoint, LinVector3D<T> egaAxisVector)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        element.ConformalSpace,
    //        angle.ParameterRange,
    //        t => element.RotateUsing(
    //            angle.GetAngle(t),
    //            egaAxisPoint,
    //            egaAxisVector
    //        )
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> RotateUsing<T>(this XGaConformalElement<T> element, IParametricAngle angle, IParametricCurve3D egaAxisPoint, IParametricCurve3D egaAxisVector)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        element.ConformalSpace,
    //        angle.ParameterRange
    //            .Intersect(egaAxisPoint.ParameterRange)
    //            .Intersect(egaAxisVector.ParameterRange),
    //        t => element.RotateUsing(
    //            angle.GetAngle(t),
    //            egaAxisPoint.GetPoint(t),
    //            egaAxisVector.GetPoint(t)
    //        )
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> RotateUsing<T>(this XGaConformalParametricElement<T> element, LinAngle<T> angle, LinVector2D<T> egaAxisPoint)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        element.ConformalSpace,
    //        element.ParameterRange,
    //        t => 
    //            element.GetElement(t).RotateUsing(
    //                angle,
    //                egaAxisPoint
    //            )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> RotateUsing<T>(this XGaConformalParametricElement<T> element, LinAngle<T> angle, LinVector3D<T> egaAxisPoint, LinVector3D<T> egaAxisVector)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        element.ConformalSpace,
    //        element.ParameterRange,
    //        t => 
    //            element.GetElement(t).RotateUsing(
    //                angle,
    //                egaAxisPoint,
    //                egaAxisVector
    //            )
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> RotateUsing<T>(this XGaConformalParametricElement<T> element, IParametricAngle angle, LinVector2D<T> egaAxisPoint)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        element.ConformalSpace,
    //        element.ParameterRange
    //            .Intersect(angle.ParameterRange),
    //        t => 
    //            element.GetElement(t).RotateUsing(
    //                angle.GetAngle(t),
    //                egaAxisPoint
    //            )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> RotateUsing<T>(this XGaConformalParametricElement<T> element, IParametricAngle angle, LinVector3D<T> egaAxisPoint, LinVector3D<T> egaAxisVector)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        element.ConformalSpace,
    //        element.ParameterRange
    //            .Intersect(angle.ParameterRange),
    //        t => 
    //            element.GetElement(t).RotateUsing(
    //                angle.GetAngle(t),
    //                egaAxisPoint,
    //                egaAxisVector
    //            )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> RotateUsing<T>(this XGaConformalParametricElement<T> element, IParametricAngle angle, IFloat64ParametricCurve2D egaAxisPoint)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        element.ConformalSpace,
    //        element.ParameterRange
    //            .Intersect(angle.ParameterRange)
    //            .Intersect(egaAxisPoint.ParameterRange),
    //        t => 
    //            element.GetElement(t).RotateUsing(
    //                angle.GetAngle(t),
    //                egaAxisPoint.GetPoint(t)
    //            )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> RotateUsing<T>(this XGaConformalParametricElement<T> element, IParametricAngle angle, IParametricCurve3D egaAxisPoint, IParametricCurve3D egaAxisVector)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        element.ConformalSpace,
    //        element.ParameterRange
    //            .Intersect(angle.ParameterRange)
    //            .Intersect(egaAxisPoint.ParameterRange)
    //            .Intersect(egaAxisVector.ParameterRange),
    //        t => 
    //            element.GetElement(t).RotateUsing(
    //                angle.GetAngle(t),
    //                egaAxisPoint.GetPoint(t),
    //                egaAxisVector.GetPoint(t)
    //            )
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> RotateUsing<T>(this XGaConformalDirection<T> element, LinAngle<T> angle, LinVector2D<T> egaAxisPoint)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint)
            .DecodeOpnsDirection();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> RotateUsing<T>(this XGaConformalDirection<T> element, LinAngle<T> angle, LinVector3D<T> egaAxisPoint, LinVector3D<T> egaAxisVector)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint, egaAxisVector)
            .DecodeOpnsDirection();
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> RotateUsing<T>(this XGaConformalTangent<T> element, LinAngle<T> angle, LinVector2D<T> egaAxisPoint)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint)
            .DecodeOpnsTangent();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> RotateUsing<T>(this XGaConformalTangent<T> element, LinAngle<T> angle, LinVector3D<T> egaAxisPoint, LinVector3D<T> egaAxisVector)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint, egaAxisVector)
            .DecodeOpnsTangent();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalFlat<T> RotateUsing<T>(this XGaConformalFlat<T> element, LinAngle<T> angle, LinVector2D<T> egaAxisPoint)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint)
            .DecodeOpnsFlat();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalFlat<T> RotateUsing<T>(this XGaConformalFlat<T> element, LinAngle<T> angle, LinVector3D<T> egaAxisPoint, LinVector3D<T> egaAxisVector)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint, egaAxisVector)
            .DecodeOpnsFlat();
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> RotateUsing<T>(this XGaConformalRound<T> element, LinAngle<T> angle, LinVector2D<T> egaAxisPoint)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint)
            .DecodeOpnsRound();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> RotateUsing<T>(this XGaConformalRound<T> element, LinAngle<T> angle, LinVector3D<T> egaAxisPoint, LinVector3D<T> egaAxisVector)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint, egaAxisVector)
            .DecodeOpnsRound();
    }
    

    public static XGaConformalBlade<T> RotateUsing<T>(this XGaConformalBlade<T> blade, LinAngle<T> angle, LinVector2D<T> egaAxisPoint)
    {
        var bivector = 
            blade.ConformalSpace.EncodeIpnsFlatPoint(
                egaAxisPoint
            ).InternalBivector;

        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();

        var s0 = halfAngleCos;
        var s2 = halfAngleSin / bivector.Norm();

        var mv = s0 + s2 * bivector;
        var mvInv = s0 - s2 * bivector;

        return mv
            .Gp(blade.InternalKVector)
            .Gp(mvInv)
            .KVectorPartToConformalBlade(blade.Grade, blade.ConformalSpace);
    }

    public static XGaConformalBlade<T> RotateUsing<T>(this XGaConformalBlade<T> blade, LinAngle<T> angle, LinVector3D<T> egaAxisPoint, LinVector3D<T> egaAxisVector)
    {
        var bivector = 
            blade.ConformalSpace.EncodeIpnsFlatLine(
                egaAxisPoint,
                egaAxisVector
            ).InternalBivector;

        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();

        var s0 = halfAngleCos;
        var s2 = halfAngleSin / bivector.Norm();

        var mv = s0 + s2 * bivector;
        var mvInv = s0 - s2 * bivector;

        return mv
            .Gp(blade.InternalKVector)
            .Gp(mvInv)
            .KVectorPartToConformalBlade(blade.Grade, blade.ConformalSpace);
    }
}