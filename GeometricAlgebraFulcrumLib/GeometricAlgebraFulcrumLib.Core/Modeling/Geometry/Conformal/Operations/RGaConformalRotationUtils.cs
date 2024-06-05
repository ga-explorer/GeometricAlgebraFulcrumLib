using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Angles;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Curves;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Operations;

public static class RGaConformalRotationUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalElement RotateUsing(this RGaConformalElement element, LinFloat64Angle angle, LinFloat64Vector2D egaAxisPoint)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint)
            .DecodeOpnsElement();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalElement RotateUsing(this RGaConformalElement element, LinFloat64Angle angle, LinFloat64Vector3D egaAxisPoint, LinFloat64Vector3D egaAxisVector)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint, egaAxisVector)
            .DecodeOpnsElement();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement RotateUsing(this RGaConformalElement element, IParametricPolarAngle angle, LinFloat64Vector2D egaAxisPoint)
    {
        return RGaConformalParametricElement.Create(
            element.ConformalSpace,
            angle.ParameterRange,
            t => element.RotateUsing(
                angle.GetAngle(t),
                egaAxisPoint
            )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement RotateUsing(this RGaConformalElement element, IParametricPolarAngle angle, IFloat64ParametricCurve2D egaAxisPoint)
    {
        return RGaConformalParametricElement.Create(
            element.ConformalSpace,
            angle.ParameterRange
                .Intersect(egaAxisPoint.ParameterRange),
            t => element.RotateUsing(
                angle.GetAngle(t),
                egaAxisPoint.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement RotateUsing(this RGaConformalElement element, IParametricPolarAngle angle, LinFloat64Vector3D egaAxisPoint, LinFloat64Vector3D egaAxisVector)
    {
        return RGaConformalParametricElement.Create(
            element.ConformalSpace,
            angle.ParameterRange,
            t => element.RotateUsing(
                angle.GetAngle(t),
                egaAxisPoint,
                egaAxisVector
            )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement RotateUsing(this RGaConformalElement element, IParametricPolarAngle angle, IParametricCurve3D egaAxisPoint, IParametricCurve3D egaAxisVector)
    {
        return RGaConformalParametricElement.Create(
            element.ConformalSpace,
            angle.ParameterRange
                .Intersect(egaAxisPoint.ParameterRange)
                .Intersect(egaAxisVector.ParameterRange),
            t => element.RotateUsing(
                angle.GetAngle(t),
                egaAxisPoint.GetPoint(t),
                egaAxisVector.GetPoint(t)
            )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement RotateUsing(this RGaConformalParametricElement element, LinFloat64Angle angle, LinFloat64Vector2D egaAxisPoint)
    {
        return RGaConformalParametricElement.Create(
            element.ConformalSpace,
            element.ParameterRange,
            t => 
                element.GetElement(t).RotateUsing(
                    angle,
                    egaAxisPoint
                )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement RotateUsing(this RGaConformalParametricElement element, LinFloat64Angle angle, LinFloat64Vector3D egaAxisPoint, LinFloat64Vector3D egaAxisVector)
    {
        return RGaConformalParametricElement.Create(
            element.ConformalSpace,
            element.ParameterRange,
            t => 
                element.GetElement(t).RotateUsing(
                    angle,
                    egaAxisPoint,
                    egaAxisVector
                )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement RotateUsing(this RGaConformalParametricElement element, IParametricPolarAngle angle, LinFloat64Vector2D egaAxisPoint)
    {
        return RGaConformalParametricElement.Create(
            element.ConformalSpace,
            element.ParameterRange
                .Intersect(angle.ParameterRange),
            t => 
                element.GetElement(t).RotateUsing(
                    angle.GetAngle(t),
                    egaAxisPoint
                )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement RotateUsing(this RGaConformalParametricElement element, IParametricPolarAngle angle, LinFloat64Vector3D egaAxisPoint, LinFloat64Vector3D egaAxisVector)
    {
        return RGaConformalParametricElement.Create(
            element.ConformalSpace,
            element.ParameterRange
                .Intersect(angle.ParameterRange),
            t => 
                element.GetElement(t).RotateUsing(
                    angle.GetAngle(t),
                    egaAxisPoint,
                    egaAxisVector
                )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement RotateUsing(this RGaConformalParametricElement element, IParametricPolarAngle angle, IFloat64ParametricCurve2D egaAxisPoint)
    {
        return RGaConformalParametricElement.Create(
            element.ConformalSpace,
            element.ParameterRange
                .Intersect(angle.ParameterRange)
                .Intersect(egaAxisPoint.ParameterRange),
            t => 
                element.GetElement(t).RotateUsing(
                    angle.GetAngle(t),
                    egaAxisPoint.GetPoint(t)
                )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement RotateUsing(this RGaConformalParametricElement element, IParametricPolarAngle angle, IParametricCurve3D egaAxisPoint, IParametricCurve3D egaAxisVector)
    {
        return RGaConformalParametricElement.Create(
            element.ConformalSpace,
            element.ParameterRange
                .Intersect(angle.ParameterRange)
                .Intersect(egaAxisPoint.ParameterRange)
                .Intersect(egaAxisVector.ParameterRange),
            t => 
                element.GetElement(t).RotateUsing(
                    angle.GetAngle(t),
                    egaAxisPoint.GetPoint(t),
                    egaAxisVector.GetPoint(t)
                )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection RotateUsing(this RGaConformalDirection element, LinFloat64Angle angle, LinFloat64Vector2D egaAxisPoint)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint)
            .DecodeOpnsDirection();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection RotateUsing(this RGaConformalDirection element, LinFloat64Angle angle, LinFloat64Vector3D egaAxisPoint, LinFloat64Vector3D egaAxisVector)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint, egaAxisVector)
            .DecodeOpnsDirection();
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent RotateUsing(this RGaConformalTangent element, LinFloat64Angle angle, LinFloat64Vector2D egaAxisPoint)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint)
            .DecodeOpnsTangent();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent RotateUsing(this RGaConformalTangent element, LinFloat64Angle angle, LinFloat64Vector3D egaAxisPoint, LinFloat64Vector3D egaAxisVector)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint, egaAxisVector)
            .DecodeOpnsTangent();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalFlat RotateUsing(this RGaConformalFlat element, LinFloat64Angle angle, LinFloat64Vector2D egaAxisPoint)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint)
            .DecodeOpnsFlat();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalFlat RotateUsing(this RGaConformalFlat element, LinFloat64Angle angle, LinFloat64Vector3D egaAxisPoint, LinFloat64Vector3D egaAxisVector)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint, egaAxisVector)
            .DecodeOpnsFlat();
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound RotateUsing(this RGaConformalRound element, LinFloat64Angle angle, LinFloat64Vector2D egaAxisPoint)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint)
            .DecodeOpnsRound();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound RotateUsing(this RGaConformalRound element, LinFloat64Angle angle, LinFloat64Vector3D egaAxisPoint, LinFloat64Vector3D egaAxisVector)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint, egaAxisVector)
            .DecodeOpnsRound();
    }
    

    public static RGaConformalBlade RotateUsing(this RGaConformalBlade blade, LinFloat64Angle angle, LinFloat64Vector2D egaAxisPoint)
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

    public static RGaConformalBlade RotateUsing(this RGaConformalBlade blade, LinFloat64Angle angle, LinFloat64Vector3D egaAxisPoint, LinFloat64Vector3D egaAxisVector)
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