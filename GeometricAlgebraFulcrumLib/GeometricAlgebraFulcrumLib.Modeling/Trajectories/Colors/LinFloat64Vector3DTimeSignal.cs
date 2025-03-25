//using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
//using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
//using GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars;

//namespace GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Colors;

//public sealed class LinFloat64Vector3DTimeSignal
//    : TimeSignal<LinFloat64Vector3D>
//{
//    public static LinFloat64Vector3DTimeSignal Finite(Float64ScalarRange timeRange, Float64ScalarSignal x, Float64ScalarSignal y, Float64ScalarSignal z)
//    {
//        return new LinFloat64Vector3DTimeSignal(timeRange, false, x, y, z);
//    }

//    public static LinFloat64Vector3DTimeSignal Periodic(Float64ScalarRange timeRange, Float64ScalarSignal x, Float64ScalarSignal y, Float64ScalarSignal z)
//    {
//        return new LinFloat64Vector3DTimeSignal(timeRange, true, x, y, z);
//    }


//    public Float64ScalarSignal X { get; }

//    public Float64ScalarSignal Y { get; }

//    public Float64ScalarSignal Z { get; }
    

//    private LinFloat64Vector3DTimeSignal(Float64ScalarRange timeRange, bool isPeriodic, Float64ScalarSignal x, Float64ScalarSignal y, Float64ScalarSignal z)
//        : base(timeRange, isPeriodic)
//    {
//        X = x;
//        Y = y;
//        Z = z;
//    }


//    public override bool IsValid()
//    {
//        return TimeRange.IsValid() &&
//               TimeRange.IsFinite &&
//               X.IsValid() &&
//               Y.IsValid() &&
//               Z.IsValid();
//    }

//    public override LinFloat64Vector3D GetValue(double t)
//    {
//        return LinFloat64Vector3D.Create(
//            X.GetValue(t),
//            Y.GetValue(t),
//            Z.GetValue(t)
//        );
//    }
//}