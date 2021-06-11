using System.Collections;
using System.Collections.Generic;
using EuclideanGeometryLib.BasicShapes;
using EuclideanGeometryLib.Borders.Space3D.Immutable;
using EuclideanGeometryLib.Borders.Space3D.Mutable;

namespace EuclideanGeometryLib.Accelerators.BIH.Space3D
{
    public class AccBih3D<T> : IAccBih3D<T> 
        where T : IFiniteGeometricShape3D
    {
        public bool IsValid => true;

        public bool IsInvalid => false;

        public int BihDepth { get; }

        public BoundingBox3D BoundingBox { get; }

        public int Count
            => RootNode.Count;

        public T this[int index]
            => RootNode[index];

        public bool IntersectionTestsEnabled { get; set; } = true;

        public IAccBihNode3D<T> RootNode { get; }


        public AccBih3D(IReadOnlyList<T> geometricObjectsList, int depthLimit, int singleDepthLimit, int leafObjectsLimit)
        {
            var result = geometricObjectsList.CreateBihRootNode3D(
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


        public BoundingBox3D GetBoundingBox()
            => BoundingBox3D.Create(
                (IEnumerable<IFiniteGeometricShape3D>)RootNode
            );

        public MutableBoundingBox3D GetMutableBoundingBox()
            => MutableBoundingBox3D.Create(
                (IEnumerable<IFiniteGeometricShape3D>)RootNode
            );

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