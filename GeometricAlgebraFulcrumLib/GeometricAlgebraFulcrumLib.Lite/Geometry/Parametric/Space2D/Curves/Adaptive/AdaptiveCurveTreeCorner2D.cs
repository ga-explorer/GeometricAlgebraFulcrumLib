using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Curves.Adaptive
{
    public sealed record AdaptiveCurveTreeCorner2D
    {
        public AdaptiveCurve2D ParentTree { get; }

        public AdaptiveCurveTreeCornerPosition2D Position { get; }

        public int Index { get; }

        public ParametricCurveLocalFrame2D Frame { get; }

        public int GridIndex
            => Position.GetGridIndex(ParentTree.TreeLevelCount);

        public double InterpolationValue
            => Position.GetInterpolationValue();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal AdaptiveCurveTreeCorner2D(AdaptiveCurve2D parentTree, int index, ParametricCurveLocalFrame2D frame, AdaptiveCurveTreeCornerPosition2D position)
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