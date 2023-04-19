using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves.Sampled;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Surfaces.Sampled
{
    public sealed class GrParametricSurfaceTreeLeafEdge3D :
        IReadOnlyCollection<GrParametricSurfaceTreeCorner3D>
    {
        private readonly Dictionary<Pair<ParametricTreeCornerPosition3D>, GrParametricSurfaceTreeCorner3D> _cornerDictionary
            = new Dictionary<Pair<ParametricTreeCornerPosition3D>, GrParametricSurfaceTreeCorner3D>();


        public int Count 
            => _cornerDictionary.Count;

        public GrParametricSurfaceTreeLeaf3D LeafNode { get; }

        public GrParametricSurfaceTreeNodeSide3D Side { get; }

        public bool IsSideX0OrX1
            => Side is 
                GrParametricSurfaceTreeNodeSide3D.SideX0 or 
                GrParametricSurfaceTreeNodeSide3D.SideX1;
        
        public bool IsSide0XOr1X
            => Side is 
                GrParametricSurfaceTreeNodeSide3D.Side0X or 
                GrParametricSurfaceTreeNodeSide3D.Side1X;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GrParametricSurfaceTreeLeafEdge3D([NotNull] GrParametricSurfaceTreeLeaf3D leafNode, GrParametricSurfaceTreeNodeSide3D edgePosition)
        {
            LeafNode = leafNode;
            Side = edgePosition;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricSurfaceTreeLeafEdge3D Clear()
        {
            _cornerDictionary.Clear();
            return this;
        }

        internal GrParametricSurfaceTreeLeafEdge3D AddCornersFromSide(GrParametricSurfaceTreeNodeSide3D side, GrParametricSurfaceTreeLeaf3D node)
        {
            Debug.Assert(
                side switch
                {
                    GrParametricSurfaceTreeNodeSide3D.Side0X => IsSide0XOr1X,
                    GrParametricSurfaceTreeNodeSide3D.Side1X => IsSide0XOr1X,
                    GrParametricSurfaceTreeNodeSide3D.SideX0 => IsSideX0OrX1,
                    _ => IsSideX0OrX1,
                }
            );

            var cornerList = 
                node.GetTreeCornersOnSide(side);

            foreach (var corner in cornerList)
            {
                var position = 
                    corner.Position;

                if (!_cornerDictionary.ContainsKey(position))
                    _cornerDictionary.Add(position, corner);
            }

            return this;
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<GrParametricSurfaceTreeCorner3D> GetEnumerator()
        {
            var corners =
                IsSideX0OrX1
                    ? _cornerDictionary.Values.OrderBy(c => c.GridIndex.Item1)
                    : _cornerDictionary.Values.OrderBy(c => c.GridIndex.Item2);

            return corners.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}