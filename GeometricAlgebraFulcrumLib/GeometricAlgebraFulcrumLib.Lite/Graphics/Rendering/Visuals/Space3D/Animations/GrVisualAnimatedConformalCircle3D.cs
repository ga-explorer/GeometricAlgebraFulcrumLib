//using System.Collections.Immutable;
//using System.Runtime.CompilerServices;
//using DataStructuresLib.Extensions;
//using ExCSS;
//using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders;
//using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Elements;
//using GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions;
//using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves;
//using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

//namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Animations;

//public class GrVisualAnimatedConformalCircle3D :
//    GrVisualAnimatedGeometry
//{
    
//    public Func<double, RGaConformalElement> BaseCurve { get; }
    
//    public Float64Range1D BaseParameterRange { get; }

//    public override Float64Range1D TimeRange { get; }

//    public DifferentialFunction BaseParameterToTimeMap { get; }

//    public DifferentialFunction TimeToBaseParameterMap { get; }
    
//    public double MinBaseParameter 
//        => BaseParameterRange.MinValue;

//    public double MaxBaseParameter 
//        => BaseParameterRange.MaxValue;
    
//    public Float64Range1D ParameterRange 
//        => TimeRange;

    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public sealed override bool IsValid()
//    {
//        return TimeRange.IsValid() &&
//               TimeRange.IsFinite &&
//               TimeRange.MinValue >= 0 &&
//               BaseParameterRange.IsValid() &&
//               BaseParameterRange.IsFinite;
//    }

    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public IEnumerable<KeyValuePair<int, RGaConformalRound>> GetKeyFrameIndexCirclePairs(int frameRate)
//    {
//        var indexElementPairs = 
//            GetKeyFrameIndexTimePairs(frameRate).ToImmutableArray(
//                indexTimePair =>
//                {
//                    var (frameIndex, time) = indexTimePair;

//                    var t = TimeToBaseParameterMap.GetValue(time);

//                    return new KeyValuePair<int, RGaConformalElement>(
//                        frameIndex,
//                        BaseCurve(t)
//                    );
//                }
//            );
        
//        var nonCircleIndexList =
//            indexElementPairs.ToImmutableArray(p =>
//                    p.Value is RGaConformalRound round &&
//                    round.IsCircle() &&
//                    round.Weight > 0
//                ).Select((b, i) => Tuple.Create(i, b))
//                .Where(t => !t.Item2)
//                .ToImmutableArray(t => t.Item1);


//    }

//}