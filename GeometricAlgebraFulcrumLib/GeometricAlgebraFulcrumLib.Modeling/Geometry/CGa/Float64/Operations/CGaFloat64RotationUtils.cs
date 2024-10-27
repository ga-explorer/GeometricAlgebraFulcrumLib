using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Angles;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space2D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Operations;

public static class CGaFloat64RotationUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Element RotateUsing(this CGaFloat64Element element, LinFloat64Angle angle, LinFloat64Vector2D egaAxisPoint)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint)
            .Decode.OpnsElement();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Element RotateUsing(this CGaFloat64Element element, LinFloat64Angle angle, LinFloat64Vector3D egaAxisPoint, LinFloat64Vector3D egaAxisVector)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint, egaAxisVector)
            .Decode.OpnsElement();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement RotateUsing(this CGaFloat64Element element, IParametricPolarAngle angle, LinFloat64Vector2D egaAxisPoint)
    {
        return CGaFloat64ParametricElement.Create(
            element.GeometricSpace,
            angle.ParameterRange,
            t => element.RotateUsing(
                angle.GetAngle(t),
                egaAxisPoint
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement RotateUsing(this CGaFloat64Element element, IParametricPolarAngle angle, IFloat64ParametricCurve2D egaAxisPoint)
    {
        return CGaFloat64ParametricElement.Create(
            element.GeometricSpace,
            angle.ParameterRange
                .Intersect(egaAxisPoint.ParameterRange),
            t => element.RotateUsing(
                angle.GetAngle(t),
                egaAxisPoint.GetPoint(t)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement RotateUsing(this CGaFloat64Element element, IParametricPolarAngle angle, LinFloat64Vector3D egaAxisPoint, LinFloat64Vector3D egaAxisVector)
    {
        return CGaFloat64ParametricElement.Create(
            element.GeometricSpace,
            angle.ParameterRange,
            t => element.RotateUsing(
                angle.GetAngle(t),
                egaAxisPoint,
                egaAxisVector
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement RotateUsing(this CGaFloat64Element element, IParametricPolarAngle angle, IParametricCurve3D egaAxisPoint, IParametricCurve3D egaAxisVector)
    {
        return CGaFloat64ParametricElement.Create(
            element.GeometricSpace,
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
    public static CGaFloat64ParametricElement RotateUsing(this CGaFloat64ParametricElement element, LinFloat64Angle angle, LinFloat64Vector2D egaAxisPoint)
    {
        return CGaFloat64ParametricElement.Create(
            element.GeometricSpace,
            element.ParameterRange,
            t =>
                element.GetElement(t).RotateUsing(
                    angle,
                    egaAxisPoint
                )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement RotateUsing(this CGaFloat64ParametricElement element, LinFloat64Angle angle, LinFloat64Vector3D egaAxisPoint, LinFloat64Vector3D egaAxisVector)
    {
        return CGaFloat64ParametricElement.Create(
            element.GeometricSpace,
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
    public static CGaFloat64ParametricElement RotateUsing(this CGaFloat64ParametricElement element, IParametricPolarAngle angle, LinFloat64Vector2D egaAxisPoint)
    {
        return CGaFloat64ParametricElement.Create(
            element.GeometricSpace,
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
    public static CGaFloat64ParametricElement RotateUsing(this CGaFloat64ParametricElement element, IParametricPolarAngle angle, LinFloat64Vector3D egaAxisPoint, LinFloat64Vector3D egaAxisVector)
    {
        return CGaFloat64ParametricElement.Create(
            element.GeometricSpace,
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
    public static CGaFloat64ParametricElement RotateUsing(this CGaFloat64ParametricElement element, IParametricPolarAngle angle, IFloat64ParametricCurve2D egaAxisPoint)
    {
        return CGaFloat64ParametricElement.Create(
            element.GeometricSpace,
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
    public static CGaFloat64ParametricElement RotateUsing(this CGaFloat64ParametricElement element, IParametricPolarAngle angle, IParametricCurve3D egaAxisPoint, IParametricCurve3D egaAxisVector)
    {
        return CGaFloat64ParametricElement.Create(
            element.GeometricSpace,
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
    public static CGaFloat64Direction RotateUsing(this CGaFloat64Direction element, LinFloat64Angle angle, LinFloat64Vector2D egaAxisPoint)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint)
            .DecodeOpnsDirection.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction RotateUsing(this CGaFloat64Direction element, LinFloat64Angle angle, LinFloat64Vector3D egaAxisPoint, LinFloat64Vector3D egaAxisVector)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint, egaAxisVector)
            .DecodeOpnsDirection.Element();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent RotateUsing(this CGaFloat64Tangent element, LinFloat64Angle angle, LinFloat64Vector2D egaAxisPoint)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint)
            .DecodeOpnsTangent.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent RotateUsing(this CGaFloat64Tangent element, LinFloat64Angle angle, LinFloat64Vector3D egaAxisPoint, LinFloat64Vector3D egaAxisVector)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint, egaAxisVector)
            .DecodeOpnsTangent.Element();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat RotateUsing(this CGaFloat64Flat element, LinFloat64Angle angle, LinFloat64Vector2D egaAxisPoint)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint)
            .DecodeOpnsFlat.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat RotateUsing(this CGaFloat64Flat element, LinFloat64Angle angle, LinFloat64Vector3D egaAxisPoint, LinFloat64Vector3D egaAxisVector)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint, egaAxisVector)
            .DecodeOpnsFlat.Element();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round RotateUsing(this CGaFloat64Round element, LinFloat64Angle angle, LinFloat64Vector2D egaAxisPoint)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint)
            .DecodeOpnsRound.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round RotateUsing(this CGaFloat64Round element, LinFloat64Angle angle, LinFloat64Vector3D egaAxisPoint, LinFloat64Vector3D egaAxisVector)
    {
        return element
            .EncodeOpnsBlade()
            .RotateUsing(angle, egaAxisPoint, egaAxisVector)
            .DecodeOpnsRound.Element();
    }


    public static CGaFloat64Blade RotateUsing(this CGaFloat64Blade blade, LinFloat64Angle angle, LinFloat64Vector2D egaAxisPoint)
    {
        var bivector =
            blade.GeometricSpace.EncodeIpnsFlat.Point(
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
            .KVectorPartToConformalBlade(blade.Grade, blade.GeometricSpace);
    }

    public static CGaFloat64Blade RotateUsing(this CGaFloat64Blade blade, LinFloat64Angle angle, LinFloat64Vector3D egaAxisPoint, LinFloat64Vector3D egaAxisVector)
    {
        var bivector =
            blade.GeometricSpace.EncodeIpnsFlat.Line(
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
            .KVectorPartToConformalBlade(blade.Grade, blade.GeometricSpace);
    }
}