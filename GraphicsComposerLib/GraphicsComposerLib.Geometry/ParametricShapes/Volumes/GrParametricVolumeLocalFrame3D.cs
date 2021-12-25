using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Volumes
{
    public sealed record GrParametricVolumeLocalFrame3D :
        ITuple3D
    {
        public double Item1
            => Point.X;
        
        public double Item2
            => Point.Y;
        
        public double Item3
            => Point.Z;

        public double X 
            => Point.X;

        public double Y 
            => Point.Y;

        public double Z 
            => Point.Z;

        public int Index { get; internal set; } 
            = -1;

        public Tuple3D ParameterValue { get; }

        public Tuple3D Point { get; }
        
        public Color Color { get; set; }
        
        public double ScalarDistance { get; }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GrParametricVolumeLocalFrame3D(double parameterValue1, double parameterValue2, double parameterValue3, [NotNull] ITuple3D point, double scalarDistance)
        {
            ParameterValue = new Tuple3D(parameterValue1, parameterValue2, parameterValue3);
            Point = point.ToTuple3D();
            ScalarDistance = scalarDistance;

            Debug.Assert(IsValid());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GrParametricVolumeLocalFrame3D([NotNull] ITuple3D parameterValue, [NotNull] ITuple3D point, double scalarDistance)
        {
            ParameterValue = parameterValue.ToTuple3D();
            Point = point.ToTuple3D();
            ScalarDistance = scalarDistance;

            Debug.Assert(IsValid());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GrParametricVolumeLocalFrame3D([NotNull] IGraphicsParametricVolume3D volume, double parameterValue1, double parameterValue2, double parameterValue3)
        {
            ParameterValue = new Tuple3D(parameterValue1, parameterValue2, parameterValue3);
            Point = volume.GetPoint(parameterValue1, parameterValue2, parameterValue3);
            ScalarDistance = volume.GetScalarDistance(parameterValue1, parameterValue2, parameterValue3);

            Debug.Assert(IsValid());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GrParametricVolumeLocalFrame3D([NotNull] IGraphicsParametricVolume3D volume, ITuple3D parameterValue)
        {
            ParameterValue = parameterValue.ToTuple3D();
            Point = volume.GetPoint(parameterValue);
            ScalarDistance = volume.GetScalarDistance(parameterValue);

            Debug.Assert(IsValid());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return ParameterValue.Item1.IsValid() &&
                   ParameterValue.Item2.IsValid() &&
                   ParameterValue.Item3.IsValid() &&
                   Point.IsValid() &&
                   ScalarDistance.IsValid();
        }
    }
}