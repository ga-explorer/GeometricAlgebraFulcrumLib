using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Visualizer;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Angles;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space2D.Bivectors;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space2D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Bivectors;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Quaternions;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Trivectors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

public sealed class CGaFloat64ParametricElement :
    IAlgebraicElement
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement Create(CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, Func<double, CGaFloat64Element> getElementFunc)
    {
        return new CGaFloat64ParametricElement(
            cgaGeometricSpace,
            parameterRange,
            getElementFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement Create(CGaFloat64ElementSpecs specs, Float64ScalarRange parameterRange, Func<double, CGaFloat64Blade> getBladeFunc, ILinFloat64Vector2D egaProbePoint)
    {
        var egaProbePointBlade =
            egaProbePoint.EncodeVGaVectorBlade(specs.GeometricSpace);

        return new CGaFloat64ParametricElement(
            specs.GeometricSpace,
            parameterRange,
            t =>
                getBladeFunc(t).DecodeElement(
                    egaProbePointBlade,
                    specs
                )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement Create(CGaFloat64ElementSpecs specs, Float64ScalarRange parameterRange, Func<double, CGaFloat64Blade> getBladeFunc, ILinFloat64Vector3D egaProbePoint)
    {
        var egaProbePointBlade =
            egaProbePoint.EncodeVGaVectorBlade(specs.GeometricSpace);

        return new CGaFloat64ParametricElement(
            specs.GeometricSpace,
            parameterRange,
            t =>
                getBladeFunc(t).DecodeElement(
                    egaProbePointBlade,
                    specs
                )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement Create(CGaFloat64ElementSpecs specs, Float64ScalarRange parameterRange, Func<double, CGaFloat64Blade> getBladeFunc, IFloat64ParametricCurve2D egaProbePointCurve)
    {
        return new CGaFloat64ParametricElement(
            specs.GeometricSpace,
            parameterRange,
            t =>
                getBladeFunc(t).DecodeElement(
                    egaProbePointCurve
                        .GetPoint(t)
                        .EncodeVGaVectorBlade(specs.GeometricSpace),
                    specs
                )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement Create(CGaFloat64ElementSpecs specs, Float64ScalarRange parameterRange, Func<double, CGaFloat64Blade> getBladeFunc, IParametricCurve3D egaProbePointCurve)
    {
        return new CGaFloat64ParametricElement(
            specs.GeometricSpace,
            parameterRange,
            t =>
                getBladeFunc(t).DecodeElement(
                    egaProbePointCurve
                        .GetPoint(t)
                        .EncodeVGaVectorBlade(specs.GeometricSpace),
                    specs
                )
        );
    }


    //public CGaFloat64ElementSpecs Specs { get; }

    //public CGaFloat64ElementKind Kind
    //    => Specs.Kind;

    //public int VGaDirectionGrade
    //    => Specs.VGaDirectionGrade;

    public Func<double, CGaFloat64Element> GetElementFunc { get; }

    public CGaFloat64GeometricSpace GeometricSpace { get; }

    public CGaFloat64Visualizer Visualizer
        => GeometricSpace switch
        {
            CGaFloat64GeometricSpace4D space => space.Visualizer,
            CGaFloat64GeometricSpace5D space => space.Visualizer,
            _ => throw new InvalidOperationException()
        };

    public Float64ScalarRange ParameterRange { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private CGaFloat64ParametricElement(CGaFloat64GeometricSpace cgaGeometricSpace, Float64ScalarRange parameterRange, Func<double, CGaFloat64Element> getBladeFunc)
    {
        if (parameterRange.IsInfinite)
            throw new ArgumentException();

        //if (specs.Encoding == CGaFloat64ElementEncoding.OpnsOrIpns)
        //    throw new ArgumentOutOfRangeException();

        //if (specs.VGaDirectionGrade < 0)
        //    throw new ArgumentOutOfRangeException();

        GeometricSpace = cgaGeometricSpace;
        ParameterRange = parameterRange;
        GetElementFunc = getBladeFunc;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Element GetElement(double parameterValue)
    {
        var element =
            GetElementFunc(parameterValue);

        Debug.Assert(element.IsValid());

        //// TODO: Complete this
        //if (Specs.Kind == element.Specs.Kind)
        //{
        //    if (Specs.Encoding == CGaFloat64ElementEncoding.Opns)
        //    {
        //        return element.Specs.Encoding switch
        //        {
        //            CGaFloat64ElementEncoding.Opns => blade,
        //            CGaFloat64ElementEncoding.Ipns => blade.IpnsToOpns(),
        //            CGaFloat64ElementEncoding.PGa => blade.PGaToOpns(),
        //            _ => element
        //        };
        //    }

        //    if (Specs.Encoding == CGaFloat64ElementEncoding.Ipns)
        //    {
        //        return element.Specs.Encoding switch
        //        {
        //            CGaFloat64ElementEncoding.Opns => blade.OpnsToIpns(),
        //            CGaFloat64ElementEncoding.Ipns => blade,
        //            CGaFloat64ElementEncoding.PGa => blade.PGaToIpns(),
        //            _ => element
        //        };
        //    }
        //}

        return element;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricScalar GetParametricScalar(Func<CGaFloat64Element, double> elementMapping)
    {
        return ComputedParametricScalar.Create(
            ParameterRange,
            t => elementMapping(GetElementFunc(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricPolarAngle GetParametricAngle(Func<CGaFloat64Element, LinFloat64PolarAngle> elementMapping)
    {
        return ComputedParametricPolarAngle.Create(
            ParameterRange,
            t => elementMapping(GetElementFunc(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricCurve2D GetParametricCurve2D(Func<CGaFloat64Element, LinFloat64Vector2D> elementMapping)
    {
        return ComputedParametricCurve2D.Create(
            ParameterRange,
            t => elementMapping(GetElementFunc(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricCurve3D GetParametricCurve3D(Func<CGaFloat64Element, LinFloat64Vector3D> elementMapping)
    {
        return ComputedParametricCurve3D.Create(
            ParameterRange,
            t => elementMapping(GetElementFunc(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricBivector2D GetParametricBivector2D(Func<CGaFloat64Element, LinFloat64Bivector2D> elementMapping)
    {
        return ComputedParametricBivector2D.Create(
            ParameterRange,
            t => elementMapping(GetElementFunc(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricBivector3D GetParametricBivector3D(Func<CGaFloat64Element, LinFloat64Bivector3D> elementMapping)
    {
        return ComputedParametricBivector3D.Create(
            ParameterRange,
            t => elementMapping(GetElementFunc(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricTrivector3D GetParametricTrivector3D(Func<CGaFloat64Element, LinFloat64Trivector3D> elementMapping)
    {
        return ComputedParametricTrivector3D.Create(
            ParameterRange,
            t => elementMapping(GetElementFunc(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricQuaternion GetParametricQuaternion(Func<CGaFloat64Element, LinFloat64Quaternion> elementMapping)
    {
        return ComputedParametricQuaternion.Create(
            ParameterRange,
            t => elementMapping(GetElementFunc(t))
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricScalar RadiusSquaredToParametricScalar()
    {
        return GetParametricScalar(
            element => element.RadiusSquared
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricScalar RealRadiusToParametricScalar()
    {
        return GetParametricScalar(
            element => element.RealRadius
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricScalar RealRadiusSquaredToParametricScalar()
    {
        return GetParametricScalar(
            element => element.RealRadiusSquared
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricCurve2D PositionToParametricCurve2D()
    {
        return GetParametricCurve2D(
            element => element.PositionToVector2D()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricCurve3D PositionToParametricCurve3D()
    {
        return GetParametricCurve3D(
            element => element.PositionToVector3D()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricCurve2D DirectionToParametricCurve2D()
    {
        return GetParametricCurve2D(
            element => element.DirectionToVector2D()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricCurve2D DirectionToParametricCurve2D(double length)
    {
        return GetParametricCurve2D(
            element => element.DirectionToVector2D(length)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricCurve2D DirectionToParametricCurve2D(IFloat64ParametricScalar length)
    {
        return ComputedParametricCurve2D.Create(
            ParameterRange,
            t => GetElement(t).DirectionToVector2D(length.GetValue(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricCurve3D DirectionToParametricCurve3D()
    {
        return GetParametricCurve3D(
            element => element.DirectionToVector3D()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricCurve3D DirectionToParametricCurve3D(double length)
    {
        return GetParametricCurve3D(
            element => element.DirectionToVector3D(length)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricCurve3D DirectionToParametricCurve3D(IFloat64ParametricScalar length)
    {
        return ComputedParametricCurve3D.Create(
            ParameterRange,
            t => GetElement(t).DirectionToVector3D(length.GetValue(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricBivector2D DirectionToParametricBivector2D()
    {
        return GetParametricBivector2D(
            element => element.DirectionToBivector2D()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricBivector3D DirectionToParametricBivector3D()
    {
        return GetParametricBivector3D(
            element => element.DirectionToBivector3D()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricTrivector3D DirectionToParametricTrivector3D()
    {
        return GetParametricTrivector3D(
            element => element.DirectionToTrivector3D()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricCurve2D NormalDirectionToParametricCurve2D()
    {
        return GetParametricCurve2D(
            element => element.NormalDirectionToVector2D()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricCurve2D NormalDirectionToParametricCurve2D(double length)
    {
        return GetParametricCurve2D(
            element => element.NormalDirectionToVector2D(length)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricCurve2D NormalDirectionToParametricCurve2D(IFloat64ParametricScalar length)
    {
        return ComputedParametricCurve2D.Create(
            ParameterRange,
            t => GetElement(t).NormalDirectionToVector2D(length.GetValue(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricCurve3D NormalDirectionToParametricCurve3D()
    {
        return GetParametricCurve3D(
            element => element.NormalDirectionToVector3D()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricCurve3D NormalDirectionToParametricCurve3D(double length)
    {
        return GetParametricCurve3D(
            element => element.NormalDirectionToVector3D(length)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricCurve3D NormalDirectionToParametricCurve3D(IFloat64ParametricScalar length)
    {
        return ComputedParametricCurve3D.Create(
            ParameterRange,
            t => GetElement(t).NormalDirectionToVector3D(length.GetValue(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricBivector2D NormalDirectionToParametricBivector2D()
    {
        return GetParametricBivector2D(
            element => element.NormalDirectionToBivector2D()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricBivector3D NormalDirectionToParametricBivector3D()
    {
        return GetParametricBivector3D(
            element => element.NormalDirectionToBivector3D()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricTrivector3D NormalDirectionToParametricTrivector3D()
    {
        return GetParametricTrivector3D(
            element => element.NormalDirectionToTrivector3D()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricCurve2D GetRoundCenterCurve2D()
    {
        return GetParametricCurve2D(element =>
            element is CGaFloat64Round round
                ? round.CenterToVector2D()
                : LinFloat64Vector2D.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricCurve3D GetRoundCenterCurve3D()
    {
        return GetParametricCurve3D(element =>
            element is CGaFloat64Round round
                ? round.CenterToVector3D()
                : LinFloat64Vector3D.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<ComputedParametricCurve2D> GetRoundPointPairCurves2D()
    {
        var point1Curve = GetParametricCurve2D(element =>
            element is CGaFloat64Round { IsRoundPointPair: true } round
                ? round.CenterToVector2D() - round.DirectionToVector2D().SetLength(round.RealRadius)
                : LinFloat64Vector2D.Zero
        );

        var point2Curve = GetParametricCurve2D(element =>
            element is CGaFloat64Round { IsRoundPointPair: true } round
                ? round.CenterToVector2D() + round.DirectionToVector2D().SetLength(round.RealRadius)
                : LinFloat64Vector2D.Zero
        );

        return new Pair<ComputedParametricCurve2D>(point1Curve, point2Curve);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<ComputedParametricCurve3D> GetRoundPointPairCurves3D()
    {
        var point1Curve = GetParametricCurve3D(element =>
            element is CGaFloat64Round { IsRoundPointPair: true } round
                ? round.CenterToVector3D() - round.DirectionToVector3D().SetLength(round.RealRadius)
                : LinFloat64Vector3D.Zero
        );

        var point2Curve = GetParametricCurve3D(element =>
            element is CGaFloat64Round { IsRoundPointPair: true } round
                ? round.CenterToVector3D() + round.DirectionToVector3D().SetLength(round.RealRadius)
                : LinFloat64Vector3D.Zero
        );

        return new Pair<ComputedParametricCurve3D>(point1Curve, point2Curve);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricCurve2D GetRoundSurfacePointCurve2D(LinFloat64Vector2D egaProbeDirection, double distanceFromSurface)
    {
        return ComputedParametricCurve2D.Create(t =>
            {
                var el = GetElement(t);

                return el is CGaFloat64Round round
                    ? round.RoundSurfacePointToVector2D(
                        egaProbeDirection,
                        distanceFromSurface
                    )
                    : el.SurfacePointToVector2D(
                        egaProbeDirection,
                        0,
                        distanceFromSurface
                    );
            }
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricCurve2D GetRoundSurfacePointCurve2D(IFloat64ParametricCurve2D egaProbeDirection, IFloat64ParametricScalar distanceFromSurface)
    {
        return ComputedParametricCurve2D.Create(t =>
            {
                var el = GetElement(t);

                return el is CGaFloat64Round round
                    ? round.RoundSurfacePointToVector2D(
                        egaProbeDirection.GetPoint(t),
                        distanceFromSurface.GetValue(t)
                    )
                    : el.SurfacePointToVector2D(
                        egaProbeDirection.GetPoint(t),
                        0,
                        distanceFromSurface.GetValue(t)
                    );
            }
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricCurve3D GetRoundSurfacePointCurve3D(LinFloat64Vector3D egaProbeDirection, double distanceFromSurface)
    {
        return ComputedParametricCurve3D.Create(t =>
            {
                var el = GetElement(t);

                return el is CGaFloat64Round round
                    ? round.RoundSurfacePointToVector3D(
                        egaProbeDirection,
                        distanceFromSurface
                    )
                    : el.SurfacePointToVector3D(
                        egaProbeDirection,
                        0,
                        distanceFromSurface
                    );
            }
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricCurve3D GetRoundSurfacePointCurve3D(IParametricCurve3D egaProbeDirection, IFloat64ParametricScalar distanceFromSurface)
    {
        return ComputedParametricCurve3D.Create(t =>
            {
                var el = GetElement(t);

                return el is CGaFloat64Round round
                    ? round.RoundSurfacePointToVector3D(
                        egaProbeDirection.GetPoint(t),
                        distanceFromSurface.GetValue(t)
                    )
                    : el.SurfacePointToVector3D(
                        egaProbeDirection.GetPoint(t),
                        0,
                        distanceFromSurface.GetValue(t)
                    );
            }
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricCurve2D GetSurfacePointCurve2D(LinFloat64Vector2D egaProbeDirection, double distanceFromPosition, double distanceFromSurface)
    {
        return ComputedParametricCurve2D.Create(t =>
            GetElement(t).SurfacePointToVector2D(
                    egaProbeDirection,
                    distanceFromPosition,
                    distanceFromSurface
                )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricCurve2D GetSurfacePointCurve2D(IFloat64ParametricCurve2D egaProbeDirection, IFloat64ParametricScalar distanceFromPosition, IFloat64ParametricScalar distanceFromSurface)
    {
        return ComputedParametricCurve2D.Create(t =>
            GetElement(t).SurfacePointToVector2D(
                    egaProbeDirection.GetPoint(t),
                    distanceFromPosition.GetValue(t),
                    distanceFromSurface.GetValue(t)
                )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricCurve3D GetSurfacePointCurve3D(LinFloat64Vector3D egaProbeDirection, double distanceFromPosition, double distanceFromSurface)
    {
        return ComputedParametricCurve3D.Create(t =>
                GetElement(t).SurfacePointToVector3D(
                    egaProbeDirection,
                    distanceFromPosition,
                    distanceFromSurface
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricCurve3D GetSurfacePointCurve3D(IParametricCurve3D egaProbeDirection, IFloat64ParametricScalar distanceFromPosition, IFloat64ParametricScalar distanceFromSurface)
    {
        return ComputedParametricCurve3D.Create(t =>
                GetElement(t).SurfacePointToVector3D(
                    egaProbeDirection.GetPoint(t),
                    distanceFromPosition.GetValue(t),
                    distanceFromSurface.GetValue(t)
            )
        );
    }

}