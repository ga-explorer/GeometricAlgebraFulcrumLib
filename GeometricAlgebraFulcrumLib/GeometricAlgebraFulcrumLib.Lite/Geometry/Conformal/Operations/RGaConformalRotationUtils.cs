using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Angles;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Curves;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Operations;

public static class RGaConformalRotationUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalElement RotateUsing(this RGaConformalElement element, Float64PlanarAngle angle, Float64Vector2D egaAxisPoint)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint)
            .DecodeOpnsElement();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalElement RotateUsing(this RGaConformalElement element, Float64PlanarAngle angle, Float64Vector3D egaAxisPoint, Float64Vector3D egaAxisVector)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint, egaAxisVector)
            .DecodeOpnsElement();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement RotateUsing(this RGaConformalElement element, IParametricAngle angle, Float64Vector2D egaAxisPoint)
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
    public static RGaConformalParametricElement RotateUsing(this RGaConformalElement element, IParametricAngle angle, IParametricCurve2D egaAxisPoint)
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
    public static RGaConformalParametricElement RotateUsing(this RGaConformalElement element, IParametricAngle angle, Float64Vector3D egaAxisPoint, Float64Vector3D egaAxisVector)
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
    public static RGaConformalParametricElement RotateUsing(this RGaConformalElement element, IParametricAngle angle, IParametricCurve3D egaAxisPoint, IParametricCurve3D egaAxisVector)
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
    public static RGaConformalParametricElement RotateUsing(this RGaConformalParametricElement element, Float64PlanarAngle angle, Float64Vector2D egaAxisPoint)
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
    public static RGaConformalParametricElement RotateUsing(this RGaConformalParametricElement element, Float64PlanarAngle angle, Float64Vector3D egaAxisPoint, Float64Vector3D egaAxisVector)
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
    public static RGaConformalParametricElement RotateUsing(this RGaConformalParametricElement element, IParametricAngle angle, Float64Vector2D egaAxisPoint)
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
    public static RGaConformalParametricElement RotateUsing(this RGaConformalParametricElement element, IParametricAngle angle, Float64Vector3D egaAxisPoint, Float64Vector3D egaAxisVector)
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
    public static RGaConformalParametricElement RotateUsing(this RGaConformalParametricElement element, IParametricAngle angle, IParametricCurve2D egaAxisPoint)
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
    public static RGaConformalParametricElement RotateUsing(this RGaConformalParametricElement element, IParametricAngle angle, IParametricCurve3D egaAxisPoint, IParametricCurve3D egaAxisVector)
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
    public static RGaConformalDirection RotateUsing(this RGaConformalDirection element, Float64PlanarAngle angle, Float64Vector2D egaAxisPoint)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint)
            .DecodeOpnsDirection();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection RotateUsing(this RGaConformalDirection element, Float64PlanarAngle angle, Float64Vector3D egaAxisPoint, Float64Vector3D egaAxisVector)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint, egaAxisVector)
            .DecodeOpnsDirection();
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent RotateUsing(this RGaConformalTangent element, Float64PlanarAngle angle, Float64Vector2D egaAxisPoint)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint)
            .DecodeOpnsTangent();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent RotateUsing(this RGaConformalTangent element, Float64PlanarAngle angle, Float64Vector3D egaAxisPoint, Float64Vector3D egaAxisVector)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint, egaAxisVector)
            .DecodeOpnsTangent();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalFlat RotateUsing(this RGaConformalFlat element, Float64PlanarAngle angle, Float64Vector2D egaAxisPoint)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint)
            .DecodeOpnsFlat();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalFlat RotateUsing(this RGaConformalFlat element, Float64PlanarAngle angle, Float64Vector3D egaAxisPoint, Float64Vector3D egaAxisVector)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint, egaAxisVector)
            .DecodeOpnsFlat();
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound RotateUsing(this RGaConformalRound element, Float64PlanarAngle angle, Float64Vector2D egaAxisPoint)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint)
            .DecodeOpnsRound();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound RotateUsing(this RGaConformalRound element, Float64PlanarAngle angle, Float64Vector3D egaAxisPoint, Float64Vector3D egaAxisVector)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint, egaAxisVector)
            .DecodeOpnsRound();
    }
    

    public static RGaConformalBlade RotateUsing(this RGaConformalBlade blade, Float64PlanarAngle angle, Float64Vector2D egaAxisPoint)
    {
        var bivector = 
            blade.ConformalSpace.EncodeIpnsFlatPoint(
                egaAxisPoint
            ).InternalBivector;

        var halfAngle = angle.GetHalfAngleInPositiveRange();

        var s0 = halfAngle.Cos().Value;
        var s2 = halfAngle.Sin().Value / bivector.Norm().Scalar();

        var mv = s0 + s2 * bivector;
        var mvInv = s0 - s2 * bivector;

        return mv
            .Gp(blade.InternalKVector)
            .Gp(mvInv)
            .KVectorPartToConformalBlade(blade.Grade, blade.ConformalSpace);
    }

    public static RGaConformalBlade RotateUsing(this RGaConformalBlade blade, Float64PlanarAngle angle, Float64Vector3D egaAxisPoint, Float64Vector3D egaAxisVector)
    {
        var bivector = 
            blade.ConformalSpace.EncodeIpnsFlatLine(
                egaAxisPoint,
                egaAxisVector
            ).InternalBivector;

        var halfAngle = angle.GetHalfAngleInPositiveRange();

        var s0 = halfAngle.Cos().Value;
        var s2 = halfAngle.Sin().Value / bivector.Norm().Scalar();

        var mv = s0 + s2 * bivector;
        var mvInv = s0 - s2 * bivector;

        return mv
            .Gp(blade.InternalKVector)
            .Gp(mvInv)
            .KVectorPartToConformalBlade(blade.Grade, blade.ConformalSpace);
    }
}