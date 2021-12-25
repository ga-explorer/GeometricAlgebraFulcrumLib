﻿using System.Collections;
using System.Collections.Generic;
using NumericalGeometryLib.BasicShapes;
using NumericalGeometryLib.Borders.Space3D.Immutable;
using NumericalGeometryLib.Borders.Space3D.Mutable;

namespace NumericalGeometryLib.Accelerators.BIH.Space3D
{
    public class AccBih3D<T> : IAccBih3D<T> 
        where T : IFiniteGeometricShape3D
    {
        public bool IsValid()
        {
            return true;
        }

        public int BihDepth { get; }

        public BoundingBox3D BoundingBox { get; }

        public int Count
        {
            get { return RootNode.Count; }
        }

        public T this[int index]
        {
            get { return RootNode[index]; }
        }

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
        {
            return BoundingBox3D.Create(
                (IEnumerable<IFiniteGeometricShape3D>) RootNode
            );
        }

        public MutableBoundingBox3D GetMutableBoundingBox()
        {
            return MutableBoundingBox3D.Create(
                (IEnumerable<IFiniteGeometricShape3D>) RootNode
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