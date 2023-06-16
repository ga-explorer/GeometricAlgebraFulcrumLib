using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Surfaces.Sampled
{
    public sealed class GrParametricSurfaceTreeBranch3D :
        GrParametricSurfaceTreeNode3D
    {
        public override int Count 
            => 4;

        public GrParametricSurfaceTreeNode3D Child00 { get; private set; }

        public GrParametricSurfaceTreeNode3D Child01 { get; private set; }

        public GrParametricSurfaceTreeNode3D Child10 { get; private set; }

        public GrParametricSurfaceTreeNode3D Child11 { get; private set; }


        public int MidFrameIndexX0 
            => Child00.FrameIndex10;

        public int MidFrameIndexX1 
            => Child01.FrameIndex11;

        public int MidFrameIndex0X 
            => Child00.FrameIndex01;

        public int MidFrameIndex1X
            => Child10.FrameIndex11;
        
        public int MidFrameIndex 
            => Child00.FrameIndex11;


        public GrParametricSurfaceTreeCorner3D MidCornerIndexX0 
            => Child00.Corner10;

        public GrParametricSurfaceTreeCorner3D MidCornerIndexX1 
            => Child01.Corner11;

        public GrParametricSurfaceTreeCorner3D MidCornerIndex0X 
            => Child00.Corner01;

        public GrParametricSurfaceTreeCorner3D MidCornerIndex1X
            => Child10.Corner11;
        
        public GrParametricSurfaceTreeCorner3D MidCornerIndex 
            => Child00.Corner11;
        

        public Pair<int> MidGridIndexX0 
            => Child00.GridIndex10;

        public Pair<int> MidGridIndexX1 
            => Child01.GridIndex11;

        public Pair<int> MidGridIndex0X 
            => Child00.GridIndex01;

        public Pair<int> MidGridIndex1X
            => Child10.GridIndex11;
        
        public Pair<int> MidGridIndex 
            => Child00.GridIndex11;


        public GrParametricSurfaceLocalFrame3D MidFrameX0 
            => Child00.Frame10;

        public GrParametricSurfaceLocalFrame3D MidFrameX1 
            => Child01.Frame11;

        public GrParametricSurfaceLocalFrame3D MidFrame0X 
            => Child00.Frame01;

        public GrParametricSurfaceLocalFrame3D MidFrame1X
            => Child10.Frame11;
        
        public GrParametricSurfaceLocalFrame3D MidFrame 
            => Child00.Frame11;


        /// <summary>
        /// Constructor of the root node of the tree
        /// </summary>
        /// <param name="parentTree"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GrParametricSurfaceTreeBranch3D(GrParametricSurfaceTree3D parentTree) 
            : base(parentTree)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GrParametricSurfaceTreeBranch3D(GrParametricSurfaceTreeBranch3D parentBranch, bool isChild1X, bool isChildX1, bool createLeafChildren = false)
            : base(parentBranch, isChild1X, isChildX1)
        {
            if (createLeafChildren)
            {
                Child00 = ParentTree.AddLeafNode(
                    new GrParametricSurfaceTreeLeaf3D(this, false, false)
                );

                Child01 = ParentTree.AddLeafNode(
                    new GrParametricSurfaceTreeLeaf3D(this, false, true)
                );

                Child10 = ParentTree.AddLeafNode(
                    new GrParametricSurfaceTreeLeaf3D(this, true, false)
                );

                Child11 = ParentTree.AddLeafNode(
                    new GrParametricSurfaceTreeLeaf3D(this, true, true)
                );
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void SetChild(GrParametricSurfaceTreeNode3D node)
        {
            Debug.Assert(ReferenceEquals(node.ParentBranch, this) && node.Level == Level + 1);

            if (node.IsChild00)
                Child00 = node;

            else if (node.IsChild01)
                Child01 = node;

            else if (node.IsChild10)
                Child10 = node;

            else
                Child11 = node;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private GrParametricSurfaceTreeBranch3D CreateBranchChildren(GrParametricSurfaceTreeOptions3D options)
        {
            Child00 = new GrParametricSurfaceTreeBranch3D(
                this, 
                false, 
                false
            ).GenerateTree(options);

            Child01 = new GrParametricSurfaceTreeBranch3D(
                this, 
                false, 
                true
            ).GenerateTree(options);

            Child10 = new GrParametricSurfaceTreeBranch3D(
                this, 
                true, 
                false
            ).GenerateTree(options);

            Child11 = new GrParametricSurfaceTreeBranch3D(
                this, 
                true, 
                true
            ).GenerateTree(options);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GrParametricSurfaceTreeNode3D GenerateTree(GrParametricSurfaceTreeOptions3D options)
        {
            var continueSubdivision =
                // Always subdivide the root node
                IsRoot ||
                
                // Continue subdivision for the required initial number of levels
                Level < options.MinLevelCount ||

                // Continue subdivision if not at at max level and frame normals are far from parallel
                (Level < options.MaxLevelCount && !HasNearEdgeFrames(options));

            if (continueSubdivision)
                return CreateBranchChildren(options);

            // Stop subdivision and replace this branch with a leaf node
            return ParentTree.AddLeafNode(
                new GrParametricSurfaceTreeLeaf3D(ParentBranch, IsChild1X, IsChildX1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GrParametricSurfaceTreeNode3D GetChildContaining(double parameterValue1, double parameterValue2)
        {
            if (Child00.Contains(parameterValue1, parameterValue2))
                return Child00;

            if (Child01.Contains(parameterValue1, parameterValue2))
                return Child01;

            if (Child10.Contains(parameterValue1, parameterValue2))
                return Child10;

            if (Child11.Contains(parameterValue1, parameterValue2))
                return Child11;

            throw new InvalidOperationException();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerator<GrParametricSurfaceTreeNode3D> GetEnumerator()
        {
            yield return Child00;
            yield return Child01;
            yield return Child10;
            yield return Child11;
        }
    }
}