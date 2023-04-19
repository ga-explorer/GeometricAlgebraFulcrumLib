using System.Collections;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.MathBase.BasicShapes;
using GeometricAlgebraFulcrumLib.MathBase.Borders.Space2D.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Borders.Space2D.Mutable;

namespace NumericalGeometryLib.Accelerators.BIH.Space2D
{
    public sealed class AccBihNodeLeaf2D<T> 
        : IAccBihNode2D<T> where T : IFiniteGeometricShape2D
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

        public IAccBihNode2D LeftChild
        {
            get { return null; }
        }

        public IAccBihNode2D RightChild
        {
            get { return null; }
        }

        public IAccBihNode2D<T> LeftChildNode
        {
            get { return null; }
        }

        public IAccBihNode2D<T> RightChildNode
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

        public IEnumerable<IFiniteGeometricShape2D> Contents
        {
            get
            {
                for (var i = FirstObjectIndex; i <= LastObjectIndex; i++)
                    yield return _geometricObjectsArray[i];
            }
        }


        internal AccBihNodeLeaf2D(string nodeId, int nodeDepth, T[] geometricObjectsArray, int firstArrayIndex, int lastArrayIndex)
        {
            NodeId = nodeId;
            NodeDepth = nodeDepth;

            _geometricObjectsArray = geometricObjectsArray;
            FirstObjectIndex = firstArrayIndex;
            LastObjectIndex = lastArrayIndex;
        }


        public IAccBihNode2D<T> GetChild(int index)
        {
            return null;
        }

        public bool Contains(IFiniteGeometricShape2D shape)
        {
            for (var i = FirstObjectIndex; i <= LastObjectIndex; i++)
                if (ReferenceEquals(_geometricObjectsArray[i], shape))
                    return true;

            return false;
        }

        public BoundingBox2D GetBoundingBox()
        {
            return BoundingBox2D.Create((IEnumerable<T>)this);
        }

        public MutableBoundingBox2D GetMutableBoundingBox()
        {
            return MutableBoundingBox2D.Create((IEnumerable<T>)this);
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