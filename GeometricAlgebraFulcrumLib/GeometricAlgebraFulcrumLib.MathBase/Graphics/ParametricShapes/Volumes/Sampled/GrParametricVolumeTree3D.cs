using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders.Space3D.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves.Adaptive;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.ParametricShapes.Volumes.Sampled
{
    public sealed class GrParametricVolumeTree3D :
        IGraphicsParametricVolume3D
    {
        private readonly HashSet<AdaptiveCurveTreeCornerPosition3D> _cornerPositionSet
            = new HashSet<AdaptiveCurveTreeCornerPosition3D>();

        private readonly Dictionary<Triplet<AdaptiveCurveTreeCornerPosition3D>, int> _cornerDictionary
            = new Dictionary<Triplet<AdaptiveCurveTreeCornerPosition3D>, int>();

        private readonly List<GrParametricVolumeTreeCorner3D> _cornerList
            = new List<GrParametricVolumeTreeCorner3D>();

        private readonly List<GrParametricVolumeTreeLeaf3D> _leafNodeList
            = new List<GrParametricVolumeTreeLeaf3D>();


        public IGraphicsParametricVolume3D Volume { get; }

        public GrParametricVolumeTreeBranch3D RootNode { get; private set; }
        
        public int TreeLevelCount { get; internal set; }

        /// <summary>
        /// The number of segments per grid side for this tree
        /// </summary>
        public int GridSegmentCount 
            => 1 << TreeLevelCount;

        public int LeafNodeCount 
            => _leafNodeList.Count;
        
        public IEnumerable<GrParametricVolumeTreeNode3D> Nodes
        {
            get
            {
                var stack = new Stack<GrParametricVolumeTreeNode3D>();

                stack.Push(RootNode);

                while (stack.Count > 0)
                {
                    var node = stack.Pop();

                    yield return node;

                    if (node is not GrParametricVolumeTreeBranch3D branchNode) 
                        continue;

                    stack.Push(branchNode.Child111);
                    stack.Push(branchNode.Child110);
                    stack.Push(branchNode.Child101);
                    stack.Push(branchNode.Child100);
                    stack.Push(branchNode.Child011);
                    stack.Push(branchNode.Child010);
                    stack.Push(branchNode.Child001);
                    stack.Push(branchNode.Child000);
                }
            }
        }

        public IEnumerable<GrParametricVolumeTreeBranch3D> BranchNodes
        {
            get
            {
                var stack = new Stack<GrParametricVolumeTreeBranch3D>();

                stack.Push(RootNode);

                while (stack.Count > 0)
                {
                    var branchNode = stack.Pop();

                    yield return branchNode;
                    
                    if (branchNode.Child111 is GrParametricVolumeTreeBranch3D childBranchNode111)
                        stack.Push(childBranchNode111);

                    if (branchNode.Child110 is GrParametricVolumeTreeBranch3D childBranchNode110)
                        stack.Push(childBranchNode110);
                    
                    if (branchNode.Child101 is GrParametricVolumeTreeBranch3D childBranchNode101)
                        stack.Push(childBranchNode101);

                    if (branchNode.Child100 is GrParametricVolumeTreeBranch3D childBranchNode100)
                        stack.Push(childBranchNode100);

                    if (branchNode.Child011 is GrParametricVolumeTreeBranch3D childBranchNode011)
                        stack.Push(childBranchNode011);

                    if (branchNode.Child010 is GrParametricVolumeTreeBranch3D childBranchNode010)
                        stack.Push(childBranchNode010);
                    
                    if (branchNode.Child001 is GrParametricVolumeTreeBranch3D childBranchNode001)
                        stack.Push(childBranchNode001);

                    if (branchNode.Child000 is GrParametricVolumeTreeBranch3D childBranchNode000)
                        stack.Push(childBranchNode000);
                }
            }
        }

        public IEnumerable<GrParametricVolumeTreeLeaf3D> LeafNodes
        {
            get
            {
                var stack = new Stack<GrParametricVolumeTreeNode3D>();

                stack.Push(RootNode);

                while (stack.Count > 0)
                {
                    var node = stack.Pop();

                    if (node is GrParametricVolumeTreeLeaf3D leafNode)
                    {
                        yield return leafNode;
                        continue;
                    }

                    var branchNode = (GrParametricVolumeTreeBranch3D) node;
                    
                    stack.Push(branchNode.Child111);
                    stack.Push(branchNode.Child110);
                    stack.Push(branchNode.Child101);
                    stack.Push(branchNode.Child100);
                    stack.Push(branchNode.Child011);
                    stack.Push(branchNode.Child010);
                    stack.Push(branchNode.Child001);
                    stack.Push(branchNode.Child000);
                }
            }
        }

        public IReadOnlyList<GrParametricVolumeTreeLeaf3D> LeafNodesList 
            => _leafNodeList;
        
        public IBoundingBox3D ParameterValueRange { get; }

        public double MinParameterValue1 
            => ParameterValueRange.MinX;

        public double MinParameterValue2 
            => ParameterValueRange.MinY;
        
        public double MinParameterValue3 
            => ParameterValueRange.MinZ;

        public double MaxParameterValue1 
            => ParameterValueRange.MaxX;

        public double MaxParameterValue2 
            => ParameterValueRange.MaxY;
        
        public double MaxParameterValue3 
            => ParameterValueRange.MaxZ;


        public int CornerCount 
            => _cornerList.Count;

        public IReadOnlyList<GrParametricVolumeTreeCorner3D> CornerList 
            => _cornerList;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricVolumeTree3D(IGraphicsParametricVolume3D surface, IBoundingBox3D parameterValueRange)
        {
            Volume = surface;
            ParameterValueRange = parameterValueRange;
            RootNode = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricVolumeTree3D(IGraphicsParametricVolume3D surface)
        {
            Volume = surface;
            ParameterValueRange = BoundingBox3D.Create(0, 0, 0, 1, 1, 1);
            RootNode = null;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricVolumeTree3D Clear()
        {
            RootNode = null;
            _cornerList.Clear();
            _leafNodeList.Clear();
            _cornerDictionary.Clear();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Triplet<double> GetParameterValue(Triplet<AdaptiveCurveTreeCornerPosition3D> cornerPosition)
        {
            var t1 = cornerPosition.Item1.GetInterpolationValue();
            var t2 = cornerPosition.Item2.GetInterpolationValue();
            var t3 = cornerPosition.Item3.GetInterpolationValue();

            var parameterValue1 = t1.Lerp(MinParameterValue1, MaxParameterValue1);
            var parameterValue2 = t2.Lerp(MinParameterValue2, MaxParameterValue2);
            var parameterValue3 = t3.Lerp(MinParameterValue3, MaxParameterValue3);

            return new Triplet<double>(parameterValue1, parameterValue2, parameterValue3);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricVolumeTree3D ClearLeafEdgeData()
        {
            foreach (var leafNode in LeafNodesList) 
                leafNode.ClearFaceData();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricVolumeTree3D GenerateLeafFaceData()
        {
            foreach (var leafNode in LeafNodesList) 
                leafNode.GenerateFaceData();

            return this;
        }

        public GrParametricVolumeTree3D GenerateTree(GrParametricVolumeTreeOptions3D options)
        {
            Clear();

            RootNode = new GrParametricVolumeTreeBranch3D(this);

            RootNode.GenerateTree(options);
            
            if (options.ForceBalancedTree)
                BalanceTree();

            foreach (var corner in _cornerList)
                corner.Frame.Index = corner.Index;
            
            _cornerPositionSet.Clear();
            _cornerDictionary.Clear();

            return this;
        }
        
        private void BalanceTree()
        {
            var leafNodeSet = new HashSet<GrParametricVolumeTreeLeaf3D>();

            foreach (var leafNode in _leafNodeList.Where(n => n.Level < TreeLevelCount - 1))
                leafNodeSet.Add(leafNode);

            while (leafNodeSet.Count > 0)
            {
                var leafNode = leafNodeSet.First();
                leafNodeSet.Remove(leafNode);

                var neighborLeafNodesList = leafNode.GetLeafNeighbors();
                var deepestLevel = neighborLeafNodesList.Max(n => n.Level);

                // The leaf node needs to be split
                if (deepestLevel > leafNode.Level + 1)
                {
                    // Replace the leaf node by a new branch
                    var branchNode = leafNode.Split();

                    // Add neighbor leaf nodes that require splitting due to new level difference
                    foreach (var neighborLeafNode in neighborLeafNodesList.Where(n => n.Level < leafNode.Level))
                        leafNodeSet.Add(neighborLeafNode);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsCorner(Triplet<AdaptiveCurveTreeCornerPosition3D> cornerPosition)
        {
            return _cornerDictionary.ContainsKey(cornerPosition);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricVolumeTreeCorner3D GetCorner(int cornerIndex)
        {
            return _cornerList[cornerIndex];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricVolumeTreeCorner3D GetCorner(Triplet<AdaptiveCurveTreeCornerPosition3D> cornerPosition)
        {
            var cornerIndex = _cornerDictionary[cornerPosition];

            return _cornerList[cornerIndex];
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetCornerIndex(Triplet<AdaptiveCurveTreeCornerPosition3D> cornerPosition)
        {
            return _cornerDictionary[cornerPosition];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetCorner(Triplet<AdaptiveCurveTreeCornerPosition3D> cornerPosition, out GrParametricVolumeTreeCorner3D corner)
        {
            if (_cornerDictionary.TryGetValue(cornerPosition, out var cornerIndex))
            {
                corner = _cornerList[cornerIndex];
                return true;
            }

            corner = default;
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetCornerIndex(Triplet<AdaptiveCurveTreeCornerPosition3D> cornerPosition, out int cornerIndex)
        {
            return _cornerDictionary.TryGetValue(cornerPosition, out cornerIndex);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal AdaptiveCurveTreeCornerPosition3D GetOrAddCornerPosition(int level, int segmentCount)
        {
            var newPosition = new AdaptiveCurveTreeCornerPosition3D(level, segmentCount);

            if (_cornerPositionSet.TryGetValue(newPosition, out var position))
                return position;

            _cornerPositionSet.Add(newPosition);

            return newPosition;
        }

        internal GrParametricVolumeTreeCorner3D GetOrAddCorner(AdaptiveCurveTreeCornerPosition3D position1, AdaptiveCurveTreeCornerPosition3D position2, AdaptiveCurveTreeCornerPosition3D position3)
        {
            var cornerPosition = 
                new Triplet<AdaptiveCurveTreeCornerPosition3D>(position1, position2, position3);

            if (_cornerDictionary.TryGetValue(cornerPosition, out var index))
                return _cornerList[index];

            var (parameterValue1, parameterValue2, parameterValue3) = 
                GetParameterValue(cornerPosition);

            index = _cornerList.Count;
            var frame = Volume.GetFrame(parameterValue1, parameterValue2, parameterValue3);
            var corner = new GrParametricVolumeTreeCorner3D(this, index, frame, cornerPosition);

            _cornerList.Add(corner);
            _cornerDictionary.Add(cornerPosition, index);

            return corner;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GrParametricVolumeTreeLeaf3D AddLeafNode(GrParametricVolumeTreeLeaf3D leafNode)
        {
            _leafNodeList.Add(leafNode);
            
            return leafNode;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<Pair<Float64Vector3D>> GetEdgePointPairs()
        {
            return LeafNodes.SelectMany(n =>
                n.GetEdgeFramePairs().Select(p => new Pair<Float64Vector3D>(p.Item1.Point, p.Item2.Point))
            ).Distinct();
        }

        public GrParametricVolumeTreeSample3D GetSample(IFloat64Vector3D parameterValue)
        {
            var parameterValue1 = parameterValue.Item1;
            var parameterValue2 = parameterValue.Item2;
            var parameterValue3 = parameterValue.Item3;

            if (!RootNode.Contains(parameterValue1, parameterValue2, parameterValue3))
                throw new ArgumentOutOfRangeException();

            var branchNode = RootNode;

            while (true)
            {
                var childNode = 
                    branchNode.GetChildContaining(parameterValue1, parameterValue2, parameterValue3);

                if (childNode is GrParametricVolumeTreeLeaf3D leafNode)
                    return new GrParametricVolumeTreeSample3D(
                        leafNode, 
                        parameterValue1, 
                        parameterValue2,
                        parameterValue3
                    );

                branchNode = (GrParametricVolumeTreeBranch3D) childNode;
            }
        }

        public GrParametricVolumeTreeSample3D GetSample(double parameterValue1, double parameterValue2, double parameterValue3)
        {
            if (!RootNode.Contains(parameterValue1, parameterValue2, parameterValue3))
                throw new ArgumentOutOfRangeException();

            var branchNode = RootNode;

            while (true)
            {
                var childNode = 
                    branchNode.GetChildContaining(parameterValue1, parameterValue2, parameterValue3);

                if (childNode is GrParametricVolumeTreeLeaf3D leafNode)
                    return new GrParametricVolumeTreeSample3D(
                        leafNode, 
                        parameterValue1, 
                        parameterValue2,
                        parameterValue3
                    );

                branchNode = (GrParametricVolumeTreeBranch3D) childNode;
            }
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return RootNode is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D GetPoint(IFloat64Vector3D parameterValue)
        {
            return GetSample(parameterValue).GetPoint();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D GetPoint(double parameterValue1, double parameterValue2, double parameterValue3)
        {
            return GetSample(parameterValue1, parameterValue2, parameterValue3).GetPoint();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetScalarDistance(IFloat64Vector3D parameterValue)
        {
            return GetSample(parameterValue).GetScalarDistance();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetScalarDistance(double parameterValue1, double parameterValue2, double parameterValue3)
        {
            return GetSample(parameterValue1, parameterValue2, parameterValue3).GetScalarDistance();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricVolumeLocalFrame3D GetFrame(IFloat64Vector3D parameterValue)
        {
            return GetSample(parameterValue).GetFrame();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricVolumeLocalFrame3D GetFrame(double parameterValue1, double parameterValue2, double parameterValue3)
        {
            return GetSample(parameterValue1, parameterValue2, parameterValue3).GetFrame();
        }
    }
}