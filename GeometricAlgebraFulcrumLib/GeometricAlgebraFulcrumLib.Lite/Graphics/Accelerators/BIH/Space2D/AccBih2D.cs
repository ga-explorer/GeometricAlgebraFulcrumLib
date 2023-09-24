using System.Collections;
using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space2D.Immutable;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space2D.Mutable;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Accelerators.BIH.Space2D
{
    public class AccBih2D<T> : IAccBih2D<T> 
        where T : IFiniteGeometricShape2D
    {
        public bool IsValid()
        {
            return true;
        }

        public int BihDepth { get; }

        public BoundingBox2D BoundingBox { get; }

        public int Count => RootNode.Count;

        public T this[int index] => RootNode[index];

        public bool IntersectionTestsEnabled { get; set; } = true;

        public IAccBihNode2D<T> RootNode { get; }


        public AccBih2D(IReadOnlyList<T> geometricObjectsList, int depthLimit, int singleDepthLimit, int leafObjectsLimit)
        {
            var result = geometricObjectsList.CreateBihRootNode2D(
                depthLimit,
                singleDepthLimit,
                leafObjectsLimit
            );

            RootNode = result.Item1;
            BoundingBox = result.Item2;
            BihDepth = RootNode.GetChildHierarchyDepth();

            foreach (var node in RootNode.GetNodes())
                node.NodeId = node.NodeId.PadRight(BihDepth, '-');
        }


        public AccBihNodeInfo2D GetRootNodeInfo()
        {
            return AccBihNodeInfo2D.Create(this);
        }

        public BoundingBox2D GetBoundingBox()
        {
            return BoundingBox2D.Create(
                (IEnumerable<IFiniteGeometricShape2D>) RootNode
            );
        }

        public MutableBoundingBox2D GetMutableBoundingBox()
        {
            return MutableBoundingBox2D.Create(
                (IEnumerable<IFiniteGeometricShape2D>) RootNode
            );
        }

        public IEnumerator<T> GetEnumerator()
        {
            return RootNode.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return RootNode.GetEnumerator();
        }
    }
}
