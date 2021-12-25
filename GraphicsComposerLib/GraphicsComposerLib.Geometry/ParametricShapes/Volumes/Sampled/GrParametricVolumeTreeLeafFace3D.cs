using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;

// ReSharper disable InconsistentNaming

namespace GraphicsComposerLib.Geometry.ParametricShapes.Volumes.Sampled
{
    public sealed class GrParametricVolumeTreeLeafFace3D :
        IReadOnlyCollection<GrParametricVolumeTreeCorner3D>
    {
        private readonly Dictionary<Triplet<int>, GrParametricVolumeTreeCorner3D> _frameDictionary
            = new Dictionary<Triplet<int>, GrParametricVolumeTreeCorner3D>();


        public int Count 
            => _frameDictionary.Count;

        public GrParametricVolumeTreeLeaf3D LeafNode { get; }

        public GrParametricVolumeTreeNodeSide3D Side { get; }

        public bool IsSideXX0OrXX1
            => Side is 
                GrParametricVolumeTreeNodeSide3D.SideXX0 or 
                GrParametricVolumeTreeNodeSide3D.SideXX1;
        
        public bool IsSideX0XOrX1X
            => Side is 
                GrParametricVolumeTreeNodeSide3D.SideX0X or 
                GrParametricVolumeTreeNodeSide3D.SideX1X;
        
        public bool IsSide0XXOr1XX
            => Side is 
                GrParametricVolumeTreeNodeSide3D.Side0XX or 
                GrParametricVolumeTreeNodeSide3D.Side1XX;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GrParametricVolumeTreeLeafFace3D([NotNull] GrParametricVolumeTreeLeaf3D leafNode, GrParametricVolumeTreeNodeSide3D side)
        {
            LeafNode = leafNode;
            Side = side;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricVolumeTreeLeafFace3D Clear()
        {
            _frameDictionary.Clear();

            return this;
        }

        internal GrParametricVolumeTreeLeafFace3D AddFramesFromSide(GrParametricVolumeTreeNodeSide3D side, GrParametricVolumeTreeLeaf3D node)
        {
            Debug.Assert(
                side switch
                {
                    GrParametricVolumeTreeNodeSide3D.Side0XX => IsSide0XXOr1XX,
                    GrParametricVolumeTreeNodeSide3D.Side1XX => IsSide0XXOr1XX,
                    GrParametricVolumeTreeNodeSide3D.SideX0X => IsSideX0XOrX1X,
                    GrParametricVolumeTreeNodeSide3D.SideX1X => IsSideX0XOrX1X,
                    GrParametricVolumeTreeNodeSide3D.SideXX0 => IsSideXX0OrXX1,
                    _ => IsSideXX0OrXX1
                }
            );

            var frameList = 
                node.GetCornersOnSide(side);

            foreach (var frame in frameList)
            {
                var index = frame.GridIndex;

                if (!_frameDictionary.ContainsKey(index))
                    _frameDictionary.Add(index, frame);
            }

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<GrParametricVolumeTreeCorner3D> GetEnumerator()
        {
            if (IsSide0XXOr1XX)
                return _frameDictionary
                    .Values
                    .OrderBy(t => t.GridIndex.Item2)
                    .ThenBy(t => t.GridIndex.Item3)
                    .GetEnumerator();

            if (IsSideX0XOrX1X)
                return _frameDictionary
                    .Values
                    .OrderBy(t => t.GridIndex.Item1)
                    .ThenBy(t => t.GridIndex.Item3)
                    .GetEnumerator();

            return _frameDictionary
                .Values
                .OrderBy(t => t.GridIndex.Item1)
                .ThenBy(t => t.GridIndex.Item2)
                .GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}