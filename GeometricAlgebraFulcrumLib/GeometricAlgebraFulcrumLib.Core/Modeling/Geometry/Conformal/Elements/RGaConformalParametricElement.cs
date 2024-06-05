using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Visualizer;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Angles;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Bivectors;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Curves;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Bivectors;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Quaternions;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Trivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Elements;

public sealed class RGaConformalParametricElement :
    IAlgebraicElement
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement Create(RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, Func<double, RGaConformalElement> getElementFunc)
    {
        return new RGaConformalParametricElement(
            conformalSpace,
            parameterRange,
            getElementFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement Create(RGaConformalElementSpecs specs, Float64ScalarRange parameterRange, Func<double, RGaConformalBlade> getBladeFunc, ILinFloat64Vector2D egaProbePoint)
    {
        var egaProbePointBlade =
            egaProbePoint.EncodeEGaVectorBlade(specs.ConformalSpace);

        return new RGaConformalParametricElement(
            specs.ConformalSpace,
            parameterRange,
            t =>
                getBladeFunc(t).DecodeElement(
                    egaProbePointBlade,
                    specs
                )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement Create(RGaConformalElementSpecs specs, Float64ScalarRange parameterRange, Func<double, RGaConformalBlade> getBladeFunc, ILinFloat64Vector3D egaProbePoint)
    {
        var egaProbePointBlade =
            egaProbePoint.EncodeEGaVectorBlade(specs.ConformalSpace);

        return new RGaConformalParametricElement(
            specs.ConformalSpace,
            parameterRange,
            t =>
                getBladeFunc(t).DecodeElement(
                    egaProbePointBlade,
                    specs
                )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement Create(RGaConformalElementSpecs specs, Float64ScalarRange parameterRange, Func<double, RGaConformalBlade> getBladeFunc, IFloat64ParametricCurve2D egaProbePointCurve)
    {
        return new RGaConformalParametricElement(
            specs.ConformalSpace,
            parameterRange,
            t =>
                getBladeFunc(t).DecodeElement(
                    egaProbePointCurve
                        .GetPoint(t)
                        .EncodeEGaVectorBlade(specs.ConformalSpace),
                    specs
                )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement Create(RGaConformalElementSpecs specs, Float64ScalarRange parameterRange, Func<double, RGaConformalBlade> getBladeFunc, IParametricCurve3D egaProbePointCurve)
    {
        return new RGaConformalParametricElement(
            specs.ConformalSpace,
            parameterRange,
            t =>
                getBladeFunc(t).DecodeElement(
                    egaProbePointCurve
                        .GetPoint(t)
                        .EncodeEGaVectorBlade(specs.ConformalSpace),
                    specs
                )
        );
    }


    //public RGaConformalElementSpecs Specs { get; }

    //public RGaConformalElementKind Kind
    //    => Specs.Kind;

    //public int EGaDirectionGrade
    //    => Specs.EGaDirectionGrade;

    public Func<double, RGaConformalElement> GetElementFunc { get; }

    public RGaConformalSpace ConformalSpace { get; }
    
    public RGaConformalVisualizer Visualizer 
        => ConformalSpace switch
        {
            RGaConformalSpace4D space => space.Visualizer,
            RGaConformalSpace5D space => space.Visualizer,
            _ => throw new InvalidOperationException()
        };

    public Float64ScalarRange ParameterRange { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RGaConformalParametricElement(RGaConformalSpace conformalSpace, Float64ScalarRange parameterRange, Func<double, RGaConformalElement> getBladeFunc)
    {
        if (parameterRange.IsInfinite)
            throw new ArgumentException();

        //if (specs.Encoding == RGaConformalElementEncoding.OpnsOrIpns)
        //    throw new ArgumentOutOfRangeException();

        //if (specs.EGaDirectionGrade < 0)
        //    throw new ArgumentOutOfRangeException();

        ConformalSpace = conformalSpace;
        ParameterRange = parameterRange;
        GetElementFunc = getBladeFunc;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaConformalElement GetElement(double parameterValue)
    {
        var element =
            GetElementFunc(parameterValue);

        Debug.Assert(element.IsValid());

        //// TODO: Complete this
        //if (Specs.Kind == element.Specs.Kind)
        //{
        //    if (Specs.Encoding == RGaConformalElementEncoding.Opns)
        //    {
        //        return element.Specs.Encoding switch
        //        {
        //            RGaConformalElementEncoding.Opns => blade,
        //            RGaConformalElementEncoding.Ipns => blade.IpnsToOpns(),
        //            RGaConformalElementEncoding.PGa => blade.PGaToOpns(),
        //            _ => element
        //        };
        //    }

        //    if (Specs.Encoding == RGaConformalElementEncoding.Ipns)
        //    {
        //        return element.Specs.Encoding switch
        //        {
        //            RGaConformalElementEncoding.Opns => blade.OpnsToIpns(),
        //            RGaConformalElementEncoding.Ipns => blade,
        //            RGaConformalElementEncoding.PGa => blade.PGaToIpns(),
        //            _ => element
        //        };
        //    }
        //}

        return element;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricScalar GetParametricScalar(Func<RGaConformalElement, double> elementMapping)
    {
        return ComputedParametricScalar.Create(
            ParameterRange,
            t => elementMapping(GetElementFunc(t))
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricPolarAngle GetParametricAngle(Func<RGaConformalElement, LinFloat64PolarAngle> elementMapping)
    {
        return ComputedParametricPolarAngle.Create(
            ParameterRange,
            t => elementMapping(GetElementFunc(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricCurve2D GetParametricCurve2D(Func<RGaConformalElement, LinFloat64Vector2D> elementMapping)
    {
        return ComputedParametricCurve2D.Create(
            ParameterRange,
            t => elementMapping(GetElementFunc(t))
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricCurve3D GetParametricCurve3D(Func<RGaConformalElement, LinFloat64Vector3D> elementMapping)
    {
        return ComputedParametricCurve3D.Create(
            ParameterRange,
            t => elementMapping(GetElementFunc(t))
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricBivector2D GetParametricBivector2D(Func<RGaConformalElement, LinFloat64Bivector2D> elementMapping)
    {
        return ComputedParametricBivector2D.Create(
            ParameterRange,
            t => elementMapping(GetElementFunc(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricBivector3D GetParametricBivector3D(Func<RGaConformalElement, LinFloat64Bivector3D> elementMapping)
    {
        return ComputedParametricBivector3D.Create(
            ParameterRange,
            t => elementMapping(GetElementFunc(t))
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricTrivector3D GetParametricTrivector3D(Func<RGaConformalElement, LinFloat64Trivector3D> elementMapping)
    {
        return ComputedParametricTrivector3D.Create(
            ParameterRange,
            t => elementMapping(GetElementFunc(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricQuaternion GetParametricQuaternion(Func<RGaConformalElement, LinFloat64Quaternion> elementMapping)
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
            element is RGaConformalRound round
                ? round.CenterToVector2D()
                : LinFloat64Vector2D.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricCurve3D GetRoundCenterCurve3D()
    {
        return GetParametricCurve3D(element => 
            element is RGaConformalRound round
                ? round.CenterToVector3D()
                : LinFloat64Vector3D.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<ComputedParametricCurve2D> GetRoundPointPairCurves2D()
    {
        var point1Curve = GetParametricCurve2D(element => 
            element is RGaConformalRound { IsRoundPointPair: true } round
                ? round.CenterToVector2D() - round.DirectionToVector2D().SetLength(round.RealRadius)
                : LinFloat64Vector2D.Zero
        );

        var point2Curve = GetParametricCurve2D(element => 
            element is RGaConformalRound { IsRoundPointPair: true } round
                ? round.CenterToVector2D() + round.DirectionToVector2D().SetLength(round.RealRadius)
                : LinFloat64Vector2D.Zero
        );

        return new Pair<ComputedParametricCurve2D>(point1Curve, point2Curve);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<ComputedParametricCurve3D> GetRoundPointPairCurves3D()
    {
        var point1Curve = GetParametricCurve3D(element => 
            element is RGaConformalRound { IsRoundPointPair: true } round
                ? round.CenterToVector3D() - round.DirectionToVector3D().SetLength(round.RealRadius)
                : LinFloat64Vector3D.Zero
        );

        var point2Curve = GetParametricCurve3D(element => 
            element is RGaConformalRound { IsRoundPointPair: true } round
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
                
                return el is RGaConformalRound round
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
                
                return el is RGaConformalRound round
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
                
                return el is RGaConformalRound round
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
                
                return el is RGaConformalRound round
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