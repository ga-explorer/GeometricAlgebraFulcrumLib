using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.Parametric.Frames;

namespace GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves.Sampled
{
    public sealed record SampledParametricCurveTreeCorner3D
    {
        public SampledParametricCurve3D ParentTree { get; }

        public ParametricTreeCornerPosition3D Position { get; }

        public int Index { get; }

        public ParametricCurveLocalFrame3D Frame { get; }

        public int GridIndex
            => Position.GetGridIndex(ParentTree.TreeLevelCount);

        public double InterpolationValue
            => Position.GetInterpolationValue();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal SampledParametricCurveTreeCorner3D(SampledParametricCurve3D parentTree, int index, ParametricCurveLocalFrame3D frame, ParametricTreeCornerPosition3D position)
        {
            ParentTree = parentTree;
            Index = index;
            Frame = frame;
            Position = position;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return $"({GridIndex})";
        }
    }
}