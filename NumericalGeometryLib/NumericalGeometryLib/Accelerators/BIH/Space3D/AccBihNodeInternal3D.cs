﻿using System.Collections;
using System.Collections.Generic;
using NumericalGeometryLib.BasicShapes;
using NumericalGeometryLib.Borders.Space3D.Immutable;
using NumericalGeometryLib.Borders.Space3D.Mutable;

namespace NumericalGeometryLib.Accelerators.BIH.Space3D
{
    public sealed class AccBihNodeInternal3D<T> : IAccBihNode3D<T>
        where T : IFiniteGeometricShape3D
    {
        private readonly T[] _geometricObjectsArray;
        private readonly double[] _clipValues = new double[2];


        public bool IntersectionTestsEnabled { get; set; } = true;

        public bool IsValid()
        {
            return true;
        }

        public int Count
        {
            get { return LastObjectIndex - FirstObjectIndex + 1; }
        }

        public T this[int index]
        {
            get { return _geometricObjectsArray[FirstObjectIndex + index]; }
        }

        public string NodeId { get; set; }

        public bool IsRoot
        {
            get { return NodeDepth == 0; }
        }

        public bool IsLeaf
        {
            get { return false; }
        }

        public bool IsInternal
        {
            get { return true; }
        }

        public bool IsSingleInternal
        {
            get { return HasLeftChild && !HasRightChild; }
        }

        public int NodeDepth { get; }

        public int BihDepth
        {
            get { return NodeId.Length; }
        }

        public int FirstObjectIndex { get; }

        public int LastObjectIndex { get; }

        public IAccBihNode3D LeftChild
        {
            get { return LeftChildNode; }
        }

        public IAccBihNode3D RightChild
        {
            get { return RightChildNode; }
        }

        public IAccBihNode3D<T> LeftChildNode { get; internal set; }

        public IAccBihNode3D<T> RightChildNode { get; internal set; }

        public bool HasLeftChild
        {
            get { return !ReferenceEquals(LeftChildNode, null); }
        }

        public bool HasRightChild
        {
            get { return !ReferenceEquals(RightChildNode, null); }
        }

        public bool HasNoChildren
        {
            get { return !(HasLeftChild || HasRightChild); }
        }

        public int SplitAxisIndex { get; }

        public string SplitAxisName
        {
            get
            {
                switch (SplitAxisIndex)
                {
                    case 0:
                        return "x-axis";
                    case 1:
                        return "y-axis";
                    case 2:
                        return "z-axis";
                    case 3:
                        return "w-axis";
                    default:
                        return "none";
                }
            }
        }

        public IEnumerable<IFiniteGeometricShape3D> Contents
        {
            get
            {
                for (var i = FirstObjectIndex; i <= LastObjectIndex; i++)
                    yield return _geometricObjectsArray[i];
            }
        }

        public double ClipValue0
        {
            get { return _clipValues[0]; }
            internal set { _clipValues[0] = value; }
        }

        public double ClipValue1
        {
            get { return _clipValues[1]; }
            internal set { _clipValues[1] = value; }
        }


        internal AccBihNodeInternal3D(string nodeId, int nodeDepth, T[] geometricObjectsArray, int firstArrayIndex, int lastArrayIndex, int axisIndex)
        {
            NodeId = nodeId;
            NodeDepth = nodeDepth;

            _geometricObjectsArray = geometricObjectsArray;
            FirstObjectIndex = firstArrayIndex;
            LastObjectIndex = lastArrayIndex;

            SplitAxisIndex = axisIndex;
        }


        public double GetClipValue(int index)
        {
            return _clipValues[index & 1];
        }

        public IAccBihNode3D<T> GetChild(int index)
        {
            return (index & 1) == 0 ? LeftChildNode : RightChildNode;
        }

        public bool Contains(IFiniteGeometricShape3D shape)
        {
            for (var i = FirstObjectIndex; i <= LastObjectIndex; i++)
                if (ReferenceEquals(_geometricObjectsArray[i], shape))
                    return true;

            return false;
        }

        public BoundingBox3D GetBoundingBox()
        {
            return BoundingBox3D.Create((IEnumerable<T>)this);
        }

        public MutableBoundingBox3D GetMutableBoundingBox()
        {
            return MutableBoundingBox3D.Create((IEnumerable<T>)this);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = FirstObjectIndex; i <= LastObjectIndex; i++)
                yield return _geometricObjectsArray[i];

            //var stack = new Stack<IBihNode3D>();
            //stack.Push(this);

            //while (stack.Count > 0)
            //{
            //    var node = stack.Pop();

            //    if (node.IsLeaf)
            //    {
            //        foreach (var surface in node.Surfaces)
            //            yield return surface;

            //        continue;
            //    }

            //    if (!ReferenceEquals(LeftChildNode, null))
            //        stack.Push(LeftChildNode);

            //    if (!ReferenceEquals(RightChildNode, null))
            //        stack.Push(RightChildNode);
            //}
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (var i = FirstObjectIndex; i <= LastObjectIndex; i++)
                yield return _geometricObjectsArray[i];
        }
    }
}