using System.Collections;
using System.Collections.Generic;
using NumericalGeometryLib.BasicShapes;
using NumericalGeometryLib.Borders.Space3D.Immutable;
using NumericalGeometryLib.Borders.Space3D.Mutable;

namespace NumericalGeometryLib.Accelerators.BIH.Space3D
{
    public sealed class AccBihNodeLeaf3D<T> : IAccBihNode3D<T>
        where T : IFiniteGeometricShape3D
    {
        private readonly T[] _geometricObjectsArray;


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
            get { return true; }
        }

        public bool IsInternal
        {
            get { return false; }
        }

        public bool IsSingleInternal
        {
            get { return false; }
        }

        public int NodeDepth { get; }

        public int BihDepth
        {
            get { return NodeId.Length; }
        }

        public int FirstObjectIndex { get; }

        public int LastObjectIndex { get; }

        public IEnumerable<IFiniteGeometricShape3D> Contents
        {
            get
            {
                for (var i = FirstObjectIndex; i <= LastObjectIndex; i++)
                    yield return _geometricObjectsArray[i];
            }
        }

        public int SplitAxisIndex
        {
            get { return -1; }
        }

        public string SplitAxisName
        {
            get { return "none"; }
        }

        public double ClipValue0
        {
            get { return double.PositiveInfinity; }
        }

        public double ClipValue1
        {
            get { return double.PositiveInfinity; }
        }

        public IAccBihNode3D LeftChild
        {
            get { return null; }
        }

        public IAccBihNode3D RightChild
        {
            get { return null; }
        }

        public IAccBihNode3D<T> LeftChildNode
        {
            get { return null; }
        }

        public IAccBihNode3D<T> RightChildNode
        {
            get { return null; }
        }

        public bool HasLeftChild
        {
            get { return false; }
        }

        public bool HasRightChild
        {
            get { return false; }
        }

        public bool HasNoChildren
        {
            get { return true; }
        }


        internal AccBihNodeLeaf3D(string nodeId, int nodeDepth, T[] geometricObjectsArray, int firstArrayIndex, int lastArrayIndex)
        {
            NodeId = nodeId;
            NodeDepth = nodeDepth;

            _geometricObjectsArray = geometricObjectsArray;
            FirstObjectIndex = firstArrayIndex;
            LastObjectIndex = lastArrayIndex;
        }


        public IAccBihNode3D<T> GetChild(int index)
        {
            return null;
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
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (var i = FirstObjectIndex; i <= LastObjectIndex; i++)
                yield return _geometricObjectsArray[i];
        }
    }
}