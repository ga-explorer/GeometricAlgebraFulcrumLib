using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves.Sampled;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Surfaces.Sampled
{
    public sealed record GrParametricSurfaceTreeCorner3D
    {
        public GrParametricSurfaceTree3D ParentTree { get; }

        public Pair<ParametricTreeCornerPosition3D> Position { get; }
        
        public int Index { get; }

        public GrParametricSurfaceLocalFrame3D Frame { get; }

        public Pair<int> GridIndex
        {
            get
            {
                var maxTreeLevel = ParentTree.TreeLevelCount;

                return new Pair<int>(
                    Position.Item1.GetGridIndex(maxTreeLevel),
                    Position.Item2.GetGridIndex(maxTreeLevel)
                );
            }
        }

        public Pair<double> InterpolationValue
            => new Pair<double>(
                Position.Item1.GetInterpolationValue(),
                Position.Item2.GetInterpolationValue()
            );


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GrParametricSurfaceTreeCorner3D([NotNull] GrParametricSurfaceTree3D parentTree, int index, [NotNull] GrParametricSurfaceLocalFrame3D frame, [NotNull] Pair<ParametricTreeCornerPosition3D> position)
        {
            ParentTree = parentTree;
            Index = index;
            Frame = frame;
            Position = position;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            var (i1, i2) = GridIndex;

            return $"({i1}, {i2})";
        }
    }
}