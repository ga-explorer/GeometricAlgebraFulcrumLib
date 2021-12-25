using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves.Sampled
{
    public abstract class GrParametricCurveTreeNode3D :
        IReadOnlyCollection<GrParametricCurveTreeNode3D>
    {
        public GrParametricCurveTree3D ParentTree { get; }

        public GrParametricCurveTreeBranch3D ParentBranch { get; }
        
        public IEnumerable<GrParametricCurveTreeBranch3D> ParentBranches 
        {
            get
            {
                for (var node = ParentBranch; node is not null; node = node.ParentBranch)
                    yield return node;
            }
        }

        public bool IsRoot 
            => ParentBranch is null;
        
        public bool IsLeaf 
            => this is GrParametricCurveTreeLeaf3D;

        public bool IsBranch 
            => this is GrParametricCurveTreeBranch3D;

        public bool IsChild 
            => ParentBranch is not null;
        
        public bool IsLeftChild 
            => (CellIndex & 1) == 0;

        public bool IsRightChild 
            => (CellIndex & 1) == 1;
        
        public abstract int Count { get; }

        public int Level { get; }
        
        public int CellIndex { get; }

        public int FrameIndex0 
            => Corner0.Index;

        public int FrameIndex1 
            => Corner1.Index;

        public GrParametricCurveTreeCorner3D Corner0 { get; }
        
        public GrParametricCurveTreeCorner3D Corner1 { get; }
        
        public int GridIndex0
            => Corner0.GridIndex;
        
        public int GridIndex1
            => Corner1.GridIndex;

        public GrParametricCurveLocalFrame3D Frame0
            => Corner0.Frame;
        
        public GrParametricCurveLocalFrame3D Frame1
            => Corner1.Frame;
        
        public double MinParameterValue
            => Frame0.ParameterValue;

        public double MaxParameterValue
            => Frame1.ParameterValue;

        public double Length0 { get; internal set; }

        public double Length1 { get; internal set; }

        public double Length 
            => Length1 - Length0;

        public IEnumerable<GrParametricCurveTreeLeaf3D> LeafNodes
        {
            get
            {
                var stack = new Stack<GrParametricCurveTreeNode3D>();

                stack.Push(this);

                while (stack.Count > 0)
                {
                    var node = stack.Pop();

                    if (node is not GrParametricCurveTreeBranch3D branchNode)
                    {
                        yield return (GrParametricCurveTreeLeaf3D) node;
                        continue;
                    }

                    stack.Push(branchNode.Child1);
                    stack.Push(branchNode.Child0);
                }
            }
        }


        /// <summary>
        /// Construct root node of tree
        /// </summary>
        /// <param name="parentTree"></param>
        protected GrParametricCurveTreeNode3D([NotNull] GrParametricCurveTree3D parentTree)
        {
            ParentTree = parentTree;
            ParentBranch = null;
            Level = 0;
            CellIndex = 0;

            ParentTree.TreeLevelCount = 0;

            var position0 = new GrParametricTreeCornerPosition3D(0, 0);
            var position1 = new GrParametricTreeCornerPosition3D(0, 1);

            Corner0 = ParentTree.GetOrAddCorner(position0);
            Corner1 = ParentTree.GetOrAddCorner(position1);
        }

        /// <summary>
        /// Construct sub-node of tree
        /// </summary>
        /// <param name="parentBranch"></param>
        /// <param name="isRightChild"></param>
        protected GrParametricCurveTreeNode3D([NotNull] GrParametricCurveTreeBranch3D parentBranch, bool isRightChild)
        {
            Debug.Assert(parentBranch.Level < 30);

            ParentTree = parentBranch.ParentTree;
            ParentBranch = parentBranch;
            Level = parentBranch.Level + 1;
            CellIndex = (parentBranch.CellIndex << 1) | (isRightChild ? 1 : 0);

            if (ParentTree.TreeLevelCount < Level)
                ParentTree.TreeLevelCount = Level;

            var position0 = new GrParametricTreeCornerPosition3D(Level, CellIndex);
            var position1 = new GrParametricTreeCornerPosition3D(Level, CellIndex + 1);

            Corner0 = ParentTree.GetOrAddCorner(position0);
            Corner1 = ParentTree.GetOrAddCorner(position1);
        }


        internal abstract double UpdateLengthData(double length0);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(double parameterValue)
        {
            return parameterValue >= MinParameterValue &&
                   parameterValue <= MaxParameterValue;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsLength(double length)
        {
            return length >= Length0 &&
                   length <= Length1;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Pair<GrParametricCurveLocalFrame3D> GetEdgeFramePair()
        {
            return new Pair<GrParametricCurveLocalFrame3D>(Frame0, Frame1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double EdgeFrameDistance()
        {
            return Frame0.Point.GetDistanceToPoint(Frame1.Point);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PlanarAngle EdgeFrameMaxAngle()
        {
            var maxAngle = 0d;

            var angle = Frame0.Normal1.GetVectorsAngle(Frame1.Normal1);
            if (angle > maxAngle) maxAngle = angle;

            angle = Frame0.Normal2.GetVectorsAngle(Frame1.Normal2);
            if (angle > maxAngle) maxAngle = angle;
            
            angle = Frame0.Tangent.GetVectorsAngle(Frame1.Tangent);
            if (angle > maxAngle) maxAngle = angle;

            return maxAngle.RadiansToAngle();
        }

        public bool HasNearEdgeFrames(GrParametricCurveTreeOptions3D options)
        {
            var maxDistanceError = options.MaxEdgeFramesDistance;
            var maxAngleErrorInRadians = options.MaxEdgeFramesAngle.Radians;

            if (Frame0.Point.GetDistanceToPoint(Frame1.Point) <= maxDistanceError)
                return true;

            var angle = Frame0.Normal1.GetVectorsAngle(Frame1.Normal1);
            if (angle > maxAngleErrorInRadians) return false;

            angle = Frame0.Normal2.GetVectorsAngle(Frame1.Normal2);
            if (angle > maxAngleErrorInRadians) return false;
            
            angle = Frame0.Tangent.GetVectorsAngle(Frame1.Tangent);
            if (angle > maxAngleErrorInRadians) return false;

            return true;
        }

        public abstract IEnumerator<GrParametricCurveTreeNode3D> GetEnumerator();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}