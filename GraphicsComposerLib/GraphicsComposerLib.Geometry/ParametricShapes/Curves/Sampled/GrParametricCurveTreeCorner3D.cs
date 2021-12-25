using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves.Sampled
{
    public sealed record GrParametricCurveTreeCorner3D
    {
        public GrParametricCurveTree3D ParentTree { get; }

        public GrParametricTreeCornerPosition3D Position { get; }
        
        public int Index { get; }

        public GrParametricCurveLocalFrame3D Frame { get; }

        public int GridIndex 
            => Position.GetGridIndex(ParentTree.TreeLevelCount);

        public double InterpolationValue
            => Position.GetInterpolationValue();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GrParametricCurveTreeCorner3D([NotNull] GrParametricCurveTree3D parentTree, int index, [NotNull] GrParametricCurveLocalFrame3D frame, [NotNull] GrParametricTreeCornerPosition3D position)
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