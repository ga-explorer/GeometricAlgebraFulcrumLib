//using System.Collections.Immutable;
//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;
//using ExCSS;
//using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Borders;
//using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Elements;
//using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.Functions;
//using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves;
//using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Vectors.Space3D;

//namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;

//public class GrVisualAnimatedConformalCircle3D :
//    GrVisualAnimatedGeometry
//{
    
//    public Func<double, CGaFloat64Element> BaseCurve { get; }
    
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

//                    return new KeyValuePair<int, CGaFloat64Element>(
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