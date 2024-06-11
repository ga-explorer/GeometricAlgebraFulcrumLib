//using System.Diagnostics;
//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
//using GeometricAlgebraFulcrumLib.Core;
//using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Angles;
//using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Scalars;
//using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Bivectors;
//using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Curves;
//using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Bivectors;
//using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves;
//using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Quaternions;
//using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Trivectors;
//using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
//using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars;
//using GeometricAlgebraFulcrumLib.Algebra.Geometry.Conformal.Blades;
//using GeometricAlgebraFulcrumLib.Algebra.Geometry.Conformal.Decoding;
//using GeometricAlgebraFulcrumLib.Algebra.Geometry.Conformal.Encoding;
//using GeometricAlgebraFulcrumLib.Algebra.Geometry.Parametric.Space1D;
//using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic;
//using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
//using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
//using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;

//namespace GeometricAlgebraFulcrumLib.Algebra.Geometry.Conformal.Elements;

//public sealed class XGaConformalParametricElement<T> :
//    IGeometricElement
//{
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> Create(XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, Func<Scalar<T>, XGaConformalElement<T>> getElementFunc)
//    {
//        return new XGaConformalParametricElement<T>(
//            cgaGeometricSpace,
//            parameterRange,
//            getElementFunc
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> Create(XGaConformalElementSpecs<T> specs, ScalarRange<T> parameterRange, Func<Scalar<T>, XGaConformalBlade<T>> getBladeFunc, ILinVector2D<T> egaProbePoint)
//    {
//        var egaProbePointBlade =
//            egaProbePoint.EncodeEGaVectorBlade(specs.ConformalSpace);

//        return new XGaConformalParametricElement<T>(
//            specs.ConformalSpace,
//            parameterRange,
//            t =>
//                getBladeFunc(t).DecodeElement(
//                    egaProbePointBlade,
//                    specs
//                )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> Create(XGaConformalElementSpecs<T> specs, ScalarRange<T> parameterRange, Func<Scalar<T>, XGaConformalBlade<T>> getBladeFunc, ILinVector3D<T> egaProbePoint)
//    {
//        var egaProbePointBlade =
//            egaProbePoint.EncodeEGaVectorBlade(specs.ConformalSpace);

//        return new XGaConformalParametricElement<T>(
//            specs.ConformalSpace,
//            parameterRange,
//            t =>
//                getBladeFunc(t).DecodeElement(
//                    egaProbePointBlade,
//                    specs
//                )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> Create(XGaConformalElementSpecs<T> specs, ScalarRange<T> parameterRange, Func<Scalar<T>, XGaConformalBlade<T>> getBladeFunc, IFloat64ParametricCurve2D egaProbePointCurve)
//    {
//        return new XGaConformalParametricElement<T>(
//            specs.ConformalSpace,
//            parameterRange,
//            t =>
//                getBladeFunc(t).DecodeElement(
//                    egaProbePointCurve
//                        .GetPoint(t)
//                        .EncodeEGaVectorBlade(specs.ConformalSpace),
//                    specs
//                )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static XGaConformalParametricElement<T> Create(XGaConformalElementSpecs<T> specs, ScalarRange<T> parameterRange, Func<Scalar<T>, XGaConformalBlade<T>> getBladeFunc, IParametricCurve3D egaProbePointCurve)
//    {
//        return new XGaConformalParametricElement<T>(
//            specs.ConformalSpace,
//            parameterRange,
//            t =>
//                getBladeFunc(t).DecodeElement(
//                    egaProbePointCurve
//                        .GetPoint(t)
//                        .EncodeEGaVectorBlade(specs.ConformalSpace),
//                    specs
//                )
//        );
//    }


//    //public XGaConformalElementSpecs<T> Specs { get; }

//    //public XGaConformalElementKind Kind
//    //    => Specs.Kind;

//    //public int EGaDirectionGrade
//    //    => Specs.EGaDirectionGrade;

//    public Func<Scalar<T>, XGaConformalElement<T>> GetElementFunc { get; }

//    public XGaConformalSpace<T> ConformalSpace { get; }
    
//    public ScalarRange<T> ParameterRange { get; }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private XGaConformalParametricElement(XGaConformalSpace<T> cgaGeometricSpace, ScalarRange<T> parameterRange, Func<Scalar<T>, XGaConformalElement<T>> getBladeFunc)
//    {
//        if (parameterRange.IsInfinite)
//            throw new ArgumentException();

//        //if (specs.Encoding == XGaConformalElementEncoding.OpnsOrIpns)
//        //    throw new ArgumentOutOfRangeException();

//        //if (specs.EGaDirectionGrade < 0)
//        //    throw new ArgumentOutOfRangeException();

//        ConformalSpace = cgaGeometricSpace;
//        ParameterRange = parameterRange;
//        GetElementFunc = getBladeFunc;
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public bool IsValid()
//    {
//        return true;
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public XGaConformalElement<T> GetElement(Scalar<T> parameterValue)
//    {
//        var element =
//            GetElementFunc(parameterValue);

//        Debug.Assert(element.IsValid());

//        //// TODO: Complete this
//        //if (Specs.Kind == element.Specs.Kind)
//        //{
//        //    if (Specs.Encoding == XGaConformalElementEncoding.Opns)
//        //    {
//        //        return element.Specs.Encoding switch
//        //        {
//        //            XGaConformalElementEncoding.Opns => blade,
//        //            XGaConformalElementEncoding.Ipns => blade.IpnsToOpns(),
//        //            XGaConformalElementEncoding.PGa => blade.PGaToOpns(),
//        //            _ => element
//        //        };
//        //    }

//        //    if (Specs.Encoding == XGaConformalElementEncoding.Ipns)
//        //    {
//        //        return element.Specs.Encoding switch
//        //        {
//        //            XGaConformalElementEncoding.Opns => blade.OpnsToIpns(),
//        //            XGaConformalElementEncoding.Ipns => blade,
//        //            XGaConformalElementEncoding.PGa => blade.PGaToIpns(),
//        //            _ => element
//        //        };
//        //    }
//        //}

//        return element;
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricScalar GetParametricScalar(Func<XGaConformalElement<T>, Scalar<T>> elementMapping)
//    {
//        return ComputedParametricScalar.Create(
//            ParameterRange,
//            t => elementMapping(GetElementFunc(t))
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricAngle GetParametricAngle(Func<XGaConformalElement<T>, LinAngle<T>> elementMapping)
//    {
//        return ComputedParametricAngle.Create(
//            ParameterRange,
//            t => elementMapping(GetElementFunc(t))
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricCurve2D GetParametricCurve2D(Func<XGaConformalElement<T>, LinVector2D<T>> elementMapping)
//    {
//        return ComputedParametricCurve2D.Create(
//            ParameterRange,
//            t => elementMapping(GetElementFunc(t))
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricCurve3D GetParametricCurve3D(Func<XGaConformalElement<T>, LinVector3D<T>> elementMapping)
//    {
//        return ComputedParametricCurve3D.Create(
//            ParameterRange,
//            t => elementMapping(GetElementFunc(t))
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricBivector2D GetParametricBivector2D(Func<XGaConformalElement<T>, LinBivector2D<T>> elementMapping)
//    {
//        return ComputedParametricBivector2D.Create(
//            ParameterRange,
//            t => elementMapping(GetElementFunc(t))
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricBivector3D GetParametricBivector3D(Func<XGaConformalElement<T>, LinBivector3D<T>> elementMapping)
//    {
//        return ComputedParametricBivector3D.Create(
//            ParameterRange,
//            t => elementMapping(GetElementFunc(t))
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricTrivector3D GetParametricTrivector3D(Func<XGaConformalElement<T>, LinTrivector3D<T>> elementMapping)
//    {
//        return ComputedParametricTrivector3D.Create(
//            ParameterRange,
//            t => elementMapping(GetElementFunc(t))
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricQuaternion GetParametricQuaternion(Func<XGaConformalElement<T>, Float64Quaternion> elementMapping)
//    {
//        return ComputedParametricQuaternion.Create(
//            ParameterRange,
//            t => elementMapping(GetElementFunc(t))
//        );
//    }

    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricScalar RadiusSquaredToParametricScalar()
//    {
//        return GetParametricScalar(
//            element => element.RadiusSquared
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricScalar RealRadiusToParametricScalar()
//    {
//        return GetParametricScalar(
//            element => element.RealRadius
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricScalar RealRadiusSquaredToParametricScalar()
//    {
//        return GetParametricScalar(
//            element => element.RealRadiusSquared
//        );
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricCurve2D PositionToParametricCurve2D()
//    {
//        return GetParametricCurve2D(
//            element => element.PositionToVector2D()
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricCurve3D PositionToParametricCurve3D()
//    {
//        return GetParametricCurve3D(
//            element => element.PositionToVector3D()
//        );
//    }
    

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricCurve2D DirectionToParametricCurve2D()
//    {
//        return GetParametricCurve2D(
//            element => element.DirectionToVector2D()
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricCurve2D DirectionToParametricCurve2D(Scalar<T> length)
//    {
//        return GetParametricCurve2D(
//            element => element.DirectionToVector2D(length)
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricCurve2D DirectionToParametricCurve2D(IParametricScalar<T> length)
//    {
//        return ComputedParametricCurve2D.Create(
//            ParameterRange,
//            t => GetElement(t).DirectionToVector2D(length.GetValue(t))
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricCurve3D DirectionToParametricCurve3D()
//    {
//        return GetParametricCurve3D(
//            element => element.DirectionToVector3D()
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricCurve3D DirectionToParametricCurve3D(Scalar<T> length)
//    {
//        return GetParametricCurve3D(
//            element => element.DirectionToVector3D(length)
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricCurve3D DirectionToParametricCurve3D(IParametricScalar<T> length)
//    {
//        return ComputedParametricCurve3D.Create(
//            ParameterRange,
//            t => GetElement(t).DirectionToVector3D(length.GetValue(t))
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricBivector2D DirectionToParametricBivector2D()
//    {
//        return GetParametricBivector2D(
//            element => element.DirectionToBivector2D()
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricBivector3D DirectionToParametricBivector3D()
//    {
//        return GetParametricBivector3D(
//            element => element.DirectionToBivector3D()
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricTrivector3D DirectionToParametricTrivector3D()
//    {
//        return GetParametricTrivector3D(
//            element => element.DirectionToTrivector3D()
//        );
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricCurve2D NormalDirectionToParametricCurve2D()
//    {
//        return GetParametricCurve2D(
//            element => element.NormalDirectionToVector2D()
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricCurve2D NormalDirectionToParametricCurve2D(Scalar<T> length)
//    {
//        return GetParametricCurve2D(
//            element => element.NormalDirectionToVector2D(length)
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricCurve2D NormalDirectionToParametricCurve2D(IParametricScalar<T> length)
//    {
//        return ComputedParametricCurve2D.Create(
//            ParameterRange,
//            t => GetElement(t).NormalDirectionToVector2D(length.GetValue(t))
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricCurve3D NormalDirectionToParametricCurve3D()
//    {
//        return GetParametricCurve3D(
//            element => element.NormalDirectionToVector3D()
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricCurve3D NormalDirectionToParametricCurve3D(Scalar<T> length)
//    {
//        return GetParametricCurve3D(
//            element => element.NormalDirectionToVector3D(length)
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricCurve3D NormalDirectionToParametricCurve3D(IParametricScalar<T> length)
//    {
//        return ComputedParametricCurve3D.Create(
//            ParameterRange,
//            t => GetElement(t).NormalDirectionToVector3D(length.GetValue(t))
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricBivector2D NormalDirectionToParametricBivector2D()
//    {
//        return GetParametricBivector2D(
//            element => element.NormalDirectionToBivector2D()
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricBivector3D NormalDirectionToParametricBivector3D()
//    {
//        return GetParametricBivector3D(
//            element => element.NormalDirectionToBivector3D()
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricTrivector3D NormalDirectionToParametricTrivector3D()
//    {
//        return GetParametricTrivector3D(
//            element => element.NormalDirectionToTrivector3D()
//        );
//    }

    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricCurve2D GetRoundCenterCurve2D()
//    {
//        return GetParametricCurve2D(element => 
//            element is XGaConformalRound<T> round
//                ? round.CenterToVector2D()
//                : LinVector2D<T>.Zero
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricCurve3D GetRoundCenterCurve3D()
//    {
//        return GetParametricCurve3D(element => 
//            element is XGaConformalRound<T> round
//                ? round.CenterToVector3D()
//                : LinVector3D<T>.Zero
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Pair<ComputedParametricCurve2D> GetRoundPointPairCurves2D()
//    {
//        var point1Curve = GetParametricCurve2D(element => 
//            element is XGaConformalRound<T> { IsRoundPointPair: true } round
//                ? round.CenterToVector2D() - round.DirectionToVector2D().SetLength(round.RealRadius)
//                : LinVector2D<T>.Zero
//        );

//        var point2Curve = GetParametricCurve2D(element => 
//            element is XGaConformalRound<T> { IsRoundPointPair: true } round
//                ? round.CenterToVector2D() + round.DirectionToVector2D().SetLength(round.RealRadius)
//                : LinVector2D<T>.Zero
//        );

//        return new Pair<ComputedParametricCurve2D>(point1Curve, point2Curve);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Pair<ComputedParametricCurve3D> GetRoundPointPairCurves3D()
//    {
//        var point1Curve = GetParametricCurve3D(element => 
//            element is XGaConformalRound<T> { IsRoundPointPair: true } round
//                ? round.CenterToVector3D() - round.DirectionToVector3D().SetLength(round.RealRadius)
//                : LinVector3D<T>.Zero
//        );

//        var point2Curve = GetParametricCurve3D(element => 
//            element is XGaConformalRound<T> { IsRoundPointPair: true } round
//                ? round.CenterToVector3D() + round.DirectionToVector3D().SetLength(round.RealRadius)
//                : LinVector3D<T>.Zero
//        );

//        return new Pair<ComputedParametricCurve3D>(point1Curve, point2Curve);
//    }
    
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricCurve2D GetRoundSurfacePointCurve2D(LinVector2D<T> egaProbeDirection, Scalar<T> distanceFromSurface)
//    {
//        return ComputedParametricCurve2D.Create(t =>
//            {
//                var el = GetElement(t);
                
//                return el is XGaConformalRound<T> round
//                    ? round.RoundSurfacePointToVector2D(
//                        egaProbeDirection,
//                        distanceFromSurface
//                    )
//                    : el.SurfacePointToVector2D(
//                        egaProbeDirection,
//                        0,
//                        distanceFromSurface
//                    );
//            }
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricCurve2D GetRoundSurfacePointCurve2D(IFloat64ParametricCurve2D egaProbeDirection, IParametricScalar<T> distanceFromSurface)
//    {
//        return ComputedParametricCurve2D.Create(t =>
//            {
//                var el = GetElement(t);
                
//                return el is XGaConformalRound<T> round
//                    ? round.RoundSurfacePointToVector2D(
//                        egaProbeDirection.GetPoint(t),
//                        distanceFromSurface.GetValue(t)
//                    )
//                    : el.SurfacePointToVector2D(
//                        egaProbeDirection.GetPoint(t),
//                        0,
//                        distanceFromSurface.GetValue(t)
//                    );
//            }
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricCurve3D GetRoundSurfacePointCurve3D(LinVector3D<T> egaProbeDirection, Scalar<T> distanceFromSurface)
//    {
//        return ComputedParametricCurve3D.Create(t => 
//            {
//                var el = GetElement(t);
                
//                return el is XGaConformalRound<T> round
//                    ? round.RoundSurfacePointToVector3D(
//                        egaProbeDirection,
//                        distanceFromSurface
//                    )
//                    : el.SurfacePointToVector3D(
//                        egaProbeDirection,
//                        0,
//                        distanceFromSurface
//                    );
//            }
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricCurve3D GetRoundSurfacePointCurve3D(IParametricCurve3D egaProbeDirection, IParametricScalar<T> distanceFromSurface)
//    {
//        return ComputedParametricCurve3D.Create(t =>
//            {
//                var el = GetElement(t);
                
//                return el is XGaConformalRound<T> round
//                    ? round.RoundSurfacePointToVector3D(
//                        egaProbeDirection.GetPoint(t),
//                        distanceFromSurface.GetValue(t)
//                    )
//                    : el.SurfacePointToVector3D(
//                        egaProbeDirection.GetPoint(t),
//                        0,
//                        distanceFromSurface.GetValue(t)
//                    );
//            }
//        );
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricCurve2D GetSurfacePointCurve2D(LinVector2D<T> egaProbeDirection, Scalar<T> distanceFromPosition, Scalar<T> distanceFromSurface)
//    {
//        return ComputedParametricCurve2D.Create(t => 
//            GetElement(t).SurfacePointToVector2D(
//                    egaProbeDirection, 
//                    distanceFromPosition,
//                    distanceFromSurface
//                )
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricCurve2D GetSurfacePointCurve2D(IFloat64ParametricCurve2D egaProbeDirection, IParametricScalar<T> distanceFromPosition, IParametricScalar<T> distanceFromSurface)
//    {
//        return ComputedParametricCurve2D.Create(t => 
//            GetElement(t).SurfacePointToVector2D(
//                    egaProbeDirection.GetPoint(t), 
//                    distanceFromPosition.GetValue(t),
//                    distanceFromSurface.GetValue(t)
//                )
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricCurve3D GetSurfacePointCurve3D(LinVector3D<T> egaProbeDirection, Scalar<T> distanceFromPosition, Scalar<T> distanceFromSurface)
//    {
//        return ComputedParametricCurve3D.Create(t => 
//                GetElement(t).SurfacePointToVector3D(
//                    egaProbeDirection, 
//                    distanceFromPosition,
//                    distanceFromSurface
//            )
//        );
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public ComputedParametricCurve3D GetSurfacePointCurve3D(IParametricCurve3D egaProbeDirection, IParametricScalar<T> distanceFromPosition, IParametricScalar<T> distanceFromSurface)
//    {
//        return ComputedParametricCurve3D.Create(t => 
//                GetElement(t).SurfacePointToVector3D(
//                    egaProbeDirection.GetPoint(t), 
//                    distanceFromPosition.GetValue(t),
//                    distanceFromSurface.GetValue(t)
//            )
//        );
//    }
    
//}