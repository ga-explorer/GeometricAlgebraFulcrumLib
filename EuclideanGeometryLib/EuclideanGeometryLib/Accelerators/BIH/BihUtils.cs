using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DataStructuresLib;
using EuclideanGeometryLib.Accelerators.BIH.Space2D;
using EuclideanGeometryLib.Accelerators.BIH.Space2D.Traversal;
using EuclideanGeometryLib.Accelerators.BIH.Space3D;
using EuclideanGeometryLib.Accelerators.BIH.Space3D.Traversal;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicShapes;
using EuclideanGeometryLib.BasicShapes.Lines;
using EuclideanGeometryLib.Borders;
using EuclideanGeometryLib.Borders.Space1D;
using EuclideanGeometryLib.Borders.Space1D.Immutable;
using EuclideanGeometryLib.Borders.Space2D;
using EuclideanGeometryLib.Borders.Space2D.Immutable;
using EuclideanGeometryLib.Borders.Space2D.Mutable;
using EuclideanGeometryLib.Borders.Space3D;
using EuclideanGeometryLib.Borders.Space3D.Immutable;
using EuclideanGeometryLib.Borders.Space3D.Mutable;
using TextComposerLib.Text.Linear;

namespace EuclideanGeometryLib.Accelerators.BIH
{
    public static class BihUtils
    {
        #region Construction of BIH
        private static int _leafObjectsLimit = 1;
        private static int _depthLimit = 100;
        private static int _singleDepthLimit = 12;


        private static MutableBoundingBox2D GetLeftChildBoundingBox2D(IBoundingBox2D boundingBox, int axisIndex, double axisMidSplitValue)
        {
            var minCorner = boundingBox.GetMinCorner();
            var maxCorner = boundingBox.GetMaxCorner().ToMutableTuple2D();
            maxCorner[axisIndex] = axisMidSplitValue;

            return MutableBoundingBox2D.CreateFromPoints(minCorner, maxCorner);
        }

        private static MutableBoundingBox2D GetRightChildBoundingBox2D(IBoundingBox2D boundingBox, int axisIndex, double axisMidSplitValue)
        {
            var minCorner = boundingBox.GetMinCorner().ToMutableTuple2D();
            var maxCorner = boundingBox.GetMaxCorner();
            minCorner[axisIndex] = axisMidSplitValue;

            return MutableBoundingBox2D.CreateFromPoints(minCorner, maxCorner);
        }

        private static IAccBihNode2D<T> CreateNode2D<T>(string nodeId, T[] objectsArray, int firstArrayIndex, int lastArrayIndex, IBoundingBox2D boundingBox, int depth, int singleDepth)
            where T : IFiniteGeometricShape2D
        {
            //Number of geometric objects must be more than zero
            var objectsCount = lastArrayIndex - firstArrayIndex + 1;

            Debug.Assert(objectsCount > 0);

            //If any of the limits is reached, create a leaf node containing
            //all remaining geometric objects
            if (objectsCount <= _leafObjectsLimit || depth >= _depthLimit || singleDepth >= _singleDepthLimit)
                return new AccBihNodeLeaf2D<T>(
                    nodeId,
                    depth,
                    objectsArray,
                    firstArrayIndex,
                    lastArrayIndex
                );

            //Construct a new internal node
            var leftChildMinValue = double.PositiveInfinity;
            var leftChildMaxValue = double.NegativeInfinity;

            var rightChildMinValue = double.PositiveInfinity;
            var rightChildMaxValue = double.NegativeInfinity;

            var i = firstArrayIndex;
            var j = lastArrayIndex;

            //Using the current bounding box, select an axis to split the
            //new internal node into two children
            var axisIndex = boundingBox.GetLongestSideIndex();
            var axisMidSplitValue = boundingBox.GetLongestSideMidValue();

            while (i <= j)
            {
                //Get current geometric object and its bounding box
                var geometricObject = objectsArray[i];
                var gobb = geometricObject.GetBoundingBox();

                var gobbMidValue = gobb.GetSideMidValue(axisIndex);
                var gobbMinValue = gobb.GetSideMinValue(axisIndex);
                var gobbMaxValue = gobb.GetSideMaxValue(axisIndex);

                //If the middle of the geometric objects' bounding box is to the
                //left of the split value, put the geometric objects in left child
                if (gobbMidValue < axisMidSplitValue)
                {
                    //Update min-max values of left child
                    if (leftChildMinValue > gobbMinValue) leftChildMinValue = gobbMinValue;
                    if (leftChildMaxValue < gobbMaxValue) leftChildMaxValue = gobbMaxValue;

                    i++;
                }
                //Else, put geometricObject in right child
                else
                {
                    //Update min-max values of right child
                    if (rightChildMinValue > gobbMinValue) rightChildMinValue = gobbMinValue;
                    if (rightChildMaxValue < gobbMaxValue) rightChildMaxValue = gobbMaxValue;

                    //Swap objects to obtain sorting for almost free!
                    if (i != j)
                    {
                        (objectsArray[i], objectsArray[j]) = (objectsArray[j], objectsArray[i]);
                    }

                    j--;
                }
            }

            //At this point, i = j + 1 and i is the leftmost element of the
            //right interval in the geometric objects array

            //Create the new internal node to be returned at the end
            var internalNode = new AccBihNodeInternal2D<T>(
                nodeId,
                depth,
                objectsArray,
                firstArrayIndex,
                lastArrayIndex,
                axisIndex
            );


            //Create child nodes of the new internal node
            //Create bounding boxes for the child nodes. This implements the
            //global heuristic as described in the BIH paper because we always
            //split along the middle of the largest axis of the bounding box and
            //do not modify our boxes in any other way. Shrinking the boxes to the
            //contained objects did not provide better performance, leads to an
            //inefficient clipping of empty space
            if (i > lastArrayIndex)
            {
                //No right child; create a "single" node with different clipping
                internalNode.ClipValue0 = leftChildMinValue;
                internalNode.ClipValue1 = leftChildMaxValue;

                var leftChildBoundingBox =
                    GetLeftChildBoundingBox2D(boundingBox, axisIndex, axisMidSplitValue);

                internalNode.LeftChildNode =
                    CreateNode2D(
                        nodeId + "0",
                        objectsArray,
                        firstArrayIndex,
                        lastArrayIndex,
                        leftChildBoundingBox,
                        depth + 1,
                        singleDepth + 1
                    );
            }
            else if (i <= firstArrayIndex)
            {
                //No left child; create a "single" node with different clipping
                internalNode.ClipValue0 = rightChildMinValue;
                internalNode.ClipValue1 = rightChildMaxValue;

                var rightChildBoundingBox =
                    GetRightChildBoundingBox2D(boundingBox, axisIndex, axisMidSplitValue);

                internalNode.RightChildNode =
                    CreateNode2D(
                        nodeId + "1",
                        objectsArray,
                        firstArrayIndex,
                        lastArrayIndex,
                        rightChildBoundingBox,
                        depth + 1,
                        singleDepth + 1
                    );
            }
            else
            {
                //Generate node with both children
                internalNode.ClipValue0 = leftChildMaxValue;
                internalNode.ClipValue1 = rightChildMinValue;

                var leftChildBoundingBox =
                    GetLeftChildBoundingBox2D(boundingBox, axisIndex, axisMidSplitValue);

                internalNode.LeftChildNode =
                    CreateNode2D(
                        nodeId + "0",
                        objectsArray,
                        firstArrayIndex,
                        i - 1,
                        leftChildBoundingBox,
                        depth + 1,
                        0
                    );


                var rightChildBoundingBox =
                    GetRightChildBoundingBox2D(boundingBox, axisIndex, axisMidSplitValue);

                internalNode.RightChildNode =
                    CreateNode2D(
                        nodeId + "1",
                        objectsArray,
                        i,
                        lastArrayIndex,
                        rightChildBoundingBox,
                        depth + 1,
                        0
                    );
            }

            Debug.Assert(internalNode.ValidateNode());

            return internalNode;
        }

        internal static Tuple<IAccBihNode2D<T>, BoundingBox2D> CreateBihRootNode2D<T>(this IReadOnlyList<T> geometricObjectsList, int depthLimit = 100, int singleDepthLimit = 10, int leafObjectsLimit = 1)
            where T : IFiniteGeometricShape2D
        {
            _leafObjectsLimit = Math.Max(leafObjectsLimit, 1);
            _depthLimit = Math.Max(depthLimit, 2);
            _singleDepthLimit = Math.Max(singleDepthLimit, 2);

            var boundingBox = BoundingBox2D.Create(
                geometricObjectsList
            );

            var bihRootNode = CreateNode2D(
                "",
                geometricObjectsList.ToArray(),
                0,
                geometricObjectsList.Count - 1,
                boundingBox,
                0,
                0
            );

            return new Tuple<IAccBihNode2D<T>, BoundingBox2D>(
                bihRootNode, 
                boundingBox
            );
        }


        public static bool ValidateNode<T>(this IAccBihNode2D<T> node)
            where T : IFiniteGeometricShape2D
        {
            foreach (var shape in node.Contents)
            {
                var inLeftChild =
                    node.HasLeftChild && node.LeftChild.Contains(shape);

                var inRightChild =
                    node.HasRightChild && node.RightChild.Contains(shape);

                //If geometric object is in both children, this is an error
                if (inLeftChild && inRightChild)
                    return false;

                //Geometric object is in neither children, this is an error
                if (!inLeftChild && !inRightChild)
                    return false;
            }

            return true;
        }

        public static BoundingBox2D GetLeftChildBoundingBox(this IAccBihNode2D node, IBoundingBox2D boundingBox)
        {
            if (!node.HasLeftChild) return null;

            var minCorner = boundingBox.GetMinCorner().ToMutableTuple2D();
            var maxCorner = boundingBox.GetMaxCorner().ToMutableTuple2D();
            
            if (node.HasRightChild)
            {
                maxCorner[node.SplitAxisIndex] = node.ClipValue0;
            }
            else
            {
                //For the special case of a single child, the clip values define
                //the two boundaries of the child.
                minCorner[node.SplitAxisIndex] = node.ClipValue0;
                maxCorner[node.SplitAxisIndex] = node.ClipValue1;
            }

            return BoundingBox2D.Create(minCorner, maxCorner);
        }

        public static BoundingBox2D GetRightChildBoundingBox(this IAccBihNode2D node, IBoundingBox2D boundingBox)
        {
            if (!node.HasRightChild) return null;

            var minCorner = boundingBox.GetMinCorner().ToMutableTuple2D();
            var maxCorner = boundingBox.GetMaxCorner().ToMutableTuple2D();

            if (node.HasLeftChild)
            {
                minCorner[node.SplitAxisIndex] = node.ClipValue1;
            }
            else
            {
                //For the special case of a single child, the clip values define
                //the two boundaries of the child.
                minCorner[node.SplitAxisIndex] = node.ClipValue0;
                maxCorner[node.SplitAxisIndex] = node.ClipValue1;
            }

            return BoundingBox2D.Create(minCorner, maxCorner);
        }

        public static IAccBihNode2D<T> GetNode<T>(this IAccBih2D<T> bih, string nodeId)
            where T : IFiniteGeometricShape2D
        {
            var node = bih.RootNode;
            foreach (var childChar in nodeId)
            {
                if (childChar == '0')
                {
                    node = node.LeftChildNode;
                }
                else if (childChar == '1')
                {
                    node = node.RightChildNode;
                }
                else
                    break;

                if (ReferenceEquals(node, null))
                    throw new InvalidOperationException("Invalid Node Id or BIH structure");
            }

            return node;
        }

        public static BoundingBox2D GetNodeBoundingBox<T>(this IAccBih2D<T> bih, string nodeId)
            where T : IFiniteGeometricShape2D
        {
            //Initial bounding box of root node
            var boundingBox = bih.BoundingBox;

            var node = bih.RootNode;
            foreach (var childChar in nodeId)
            {
                if (childChar == '0')
                {
                    boundingBox = node.GetLeftChildBoundingBox(boundingBox);
                    node = node.LeftChildNode;
                }
                else if (childChar == '1')
                {
                    boundingBox = node.GetRightChildBoundingBox(boundingBox);
                    node = node.RightChildNode;
                }
                else
                    break;

                if (ReferenceEquals(boundingBox, null))
                    throw new InvalidOperationException("Invalid Node Id or BIH structure");
            }

            return boundingBox;
        }

        public static AccBihNodeInfo2D GetRootNodeInfo<T>(this IAccBih2D<T> bih)
            where T : IFiniteGeometricShape2D
        {
            return new AccBihNodeInfo2D(
                bih.RootNode,
                BoundingBox2D.CreateInfinite()
            );
        }

        public static AccBihNodeInfo2D GetNodeInfo<T>(this IAccBih2D<T> bih, string nodeId)
            where T : IFiniteGeometricShape2D
        {
            //Initial bounding box of root node
            var boundingBox = bih.BoundingBox;

            var node = bih.RootNode;
            foreach (var childChar in nodeId)
            {
                if (childChar == '0')
                {
                    boundingBox = node.GetLeftChildBoundingBox(boundingBox);
                    node = node.LeftChildNode;
                }
                else if (childChar == '1')
                {
                    boundingBox = node.GetRightChildBoundingBox(boundingBox);
                    node = node.RightChildNode;
                }
                else
                    break;

                if (ReferenceEquals(boundingBox, null))
                    throw new InvalidOperationException("Invalid Node Id or BIH structure");
            }

            return new AccBihNodeInfo2D(node, boundingBox);
        }

        public static AccBih2D<T> ToBih2D<T>(this IReadOnlyList<T> geometricObjectsList, int depthLimit = 100, int singleDepthLimit = 10, int leafObjectsLimit = 1)
            where T : IFiniteGeometricShape2D
        {
            return new AccBih2D<T>(
                geometricObjectsList,
                depthLimit,
                singleDepthLimit,
                leafObjectsLimit
            );
        }


        private static MutableBoundingBox3D GetLeftChildBoundingBox3D(IBoundingBox3D boundingBox, int axisIndex, double axisMidSplitValue)
        {
            var minCorner = boundingBox.GetMinCorner();
            var maxCorner = boundingBox.GetMaxCorner().ToMutableTuple3D();
            maxCorner[axisIndex] = axisMidSplitValue;

            return MutableBoundingBox3D.CreateFromPoints(minCorner, maxCorner);
        }

        private static MutableBoundingBox3D GetRightChildBoundingBox3D(IBoundingBox3D boundingBox, int axisIndex, double axisMidSplitValue)
        {
            var minCorner = boundingBox.GetMinCorner().ToMutableTuple3D();
            var maxCorner = boundingBox.GetMaxCorner();
            minCorner[axisIndex] = axisMidSplitValue;

            return MutableBoundingBox3D.CreateFromPoints(minCorner, maxCorner);
        }

        private static IAccBihNode3D<T> CreateNode3D<T>(string nodeId, T[] objectsArray, int firstArrayIndex, int lastArrayIndex, IBoundingBox3D boundingBox, int depth, int singleDepth)
            where T : IFiniteGeometricShape3D
        {
            //Number of geometric objects must be more than zero
            var objectsCount = lastArrayIndex - firstArrayIndex + 1;

            Debug.Assert(objectsCount > 0);

            //If any of the limits is reached, create a leaf node containing
            //all remaining geometric objects
            if (objectsCount <= _leafObjectsLimit || depth >= _depthLimit || singleDepth >= _singleDepthLimit)
                return new AccBihNodeLeaf3D<T>(
                    nodeId,
                    depth,
                    objectsArray,
                    firstArrayIndex,
                    lastArrayIndex
                );

            //Construct a new internal node
            var leftChildMinValue = double.PositiveInfinity;
            var leftChildMaxValue = double.NegativeInfinity;

            var rightChildMinValue = double.PositiveInfinity;
            var rightChildMaxValue = double.NegativeInfinity;

            var i = firstArrayIndex;
            var j = lastArrayIndex;

            //Using the current bounding box, select an axis to split the
            //new internal node into two children
            var axisIndex = boundingBox.GetLongestSideIndex();
            var axisMidSplitValue = boundingBox.GetLongestSideMidValue();

            while (i <= j)
            {
                //Get current geometric object and its bounding box
                var geometricObject = objectsArray[i];
                var gobb = geometricObject.GetBoundingBox();

                var gobbMidValue = gobb.GetSideMidValue(axisIndex);
                var gobbMinValue = gobb.GetSideMinValue(axisIndex);
                var gobbMaxValue = gobb.GetSideMaxValue(axisIndex);

                //If the middle of the geometric objects's bounding box is to the
                //left of the split value, put the geometric objects in left child
                if (gobbMidValue < axisMidSplitValue)
                {
                    //Update min-max values of left child
                    if (leftChildMinValue > gobbMinValue) leftChildMinValue = gobbMinValue;
                    if (leftChildMaxValue < gobbMaxValue) leftChildMaxValue = gobbMaxValue;

                    i++;
                }
                //Else, put geometricObject in right child
                else
                {
                    //Update min-max values of right child
                    if (rightChildMinValue > gobbMinValue) rightChildMinValue = gobbMinValue;
                    if (rightChildMaxValue < gobbMaxValue) rightChildMaxValue = gobbMaxValue;

                    //Swap objects to obtain sorting for almost free!
                    if (i != j)
                    {
                        (objectsArray[i], objectsArray[j]) = (objectsArray[j], objectsArray[i]);
                    }

                    j--;
                }
            }

            //At this point, i = j + 1 and i is the leftmost element of the
            //right interval in the geometric objects array

            //Create the new internal node to be returned at the end
            var internalNode = new AccBihNodeInternal3D<T>(
                nodeId,
                depth,
                objectsArray,
                firstArrayIndex,
                lastArrayIndex,
                axisIndex
            );


            //Create child nodes of the new internal node
            //Create bounding boxes for the child nodes. This implements the
            //global heuristic as described in the BIH paper because we always
            //split along the middle of the largest axis of the bounding box and
            //do not modify our boxes in any other way. Shrinking the boxes to the
            //contained objects did not provide better performance, leads to an
            //inefficient clipping of empty space
            if (i > lastArrayIndex)
            {
                //No right child; create a "single" node with different clipping
                internalNode.ClipValue0 = leftChildMinValue;
                internalNode.ClipValue1 = leftChildMaxValue;

                var leftChildBoundingBox = 
                    GetLeftChildBoundingBox3D(boundingBox, axisIndex, axisMidSplitValue);

                internalNode.LeftChildNode = 
                    CreateNode3D(
                        nodeId + "0",
                        objectsArray,
                        firstArrayIndex, 
                        lastArrayIndex,
                        leftChildBoundingBox,
                        depth + 1, 
                        singleDepth + 1
                    );
            }
            else if (i <= firstArrayIndex)
            {
                //No left child; create a "single" node with different clipping
                internalNode.ClipValue0 = rightChildMinValue;
                internalNode.ClipValue1 = rightChildMaxValue;

                var rightChildBoundingBox = 
                    GetRightChildBoundingBox3D(boundingBox, axisIndex, axisMidSplitValue);

                internalNode.RightChildNode = 
                    CreateNode3D(
                        nodeId + "1",
                        objectsArray,
                        firstArrayIndex, 
                        lastArrayIndex,
                        rightChildBoundingBox,
                        depth + 1, 
                        singleDepth + 1
                    );
            }
            else
            {
                //Generate node with both children
                internalNode.ClipValue0 = leftChildMaxValue;
                internalNode.ClipValue1 = rightChildMinValue;

                var leftChildBoundingBox = 
                    GetLeftChildBoundingBox3D(boundingBox, axisIndex, axisMidSplitValue);

                internalNode.LeftChildNode = 
                    CreateNode3D(
                        nodeId + "0",
                        objectsArray,
                        firstArrayIndex, 
                        i - 1, 
                        leftChildBoundingBox, 
                        depth + 1, 
                        0
                    );


                var rightChildBoundingBox =
                    GetRightChildBoundingBox3D(boundingBox, axisIndex, axisMidSplitValue);

                internalNode.RightChildNode = 
                    CreateNode3D(
                        nodeId + "1",
                        objectsArray,
                        i, 
                        lastArrayIndex, 
                        rightChildBoundingBox, 
                        depth + 1, 
                        0
                    );
            }

            Debug.Assert(internalNode.ValidateNode());

            return internalNode;
        }

        internal static Tuple<IAccBihNode3D<T>, BoundingBox3D> CreateBihRootNode3D<T>(this IReadOnlyList<T> geometricObjectsList, int depthLimit = 100, int singleDepthLimit = 10, int leafObjectsLimit = 1)
            where T : IFiniteGeometricShape3D
        {
            _leafObjectsLimit = Math.Max(leafObjectsLimit, 1);
            _depthLimit = Math.Max(depthLimit, 2);
            _singleDepthLimit = Math.Max(singleDepthLimit, 2);

            var boundingBox = BoundingBox3D.Create(
                geometricObjectsList
            );

            var bihRootNode = CreateNode3D(
                "",
                geometricObjectsList.ToArray(),
                0,
                geometricObjectsList.Count - 1,
                boundingBox,
                0,
                0
            );

            return new Tuple<IAccBihNode3D<T>, BoundingBox3D>(
                bihRootNode,
                boundingBox
            );
        }


        public static bool ValidateNode<T>(this IAccBihNode3D<T> node)
            where T : IFiniteGeometricShape3D
        {
            foreach (var shape in node.Contents)
            {
                var inLeftChild =
                    node.HasLeftChild && node.LeftChild.Contains(shape);

                var inRightChild =
                    node.HasRightChild && node.RightChild.Contains(shape);

                //If geometric object is in both children, this is an error
                if (inLeftChild && inRightChild)
                    return false;

                //Geometric object is in neither children, this is an error
                if (!inLeftChild && !inRightChild)
                    return false;
            }

            return true;
        }

        public static BoundingBox3D GetLeftChildBoundingBox(this IAccBihNode3D node, IBoundingBox3D boundingBox)
        {
            if (!node.HasLeftChild) return null;

            var minCorner = boundingBox.GetMinCorner().ToMutableTuple3D();
            var maxCorner = boundingBox.GetMaxCorner().ToMutableTuple3D();

            if (node.HasRightChild)
            {
                maxCorner[node.SplitAxisIndex] = node.ClipValue0;
            }
            else
            {
                //For the special case of a single child, the clip values define
                //the two boundaries of the child.
                minCorner[node.SplitAxisIndex] = node.ClipValue0;
                maxCorner[node.SplitAxisIndex] = node.ClipValue1;
            }

            return BoundingBox3D.CreateFromPoints(minCorner, maxCorner);
        }

        public static BoundingBox3D GetRightChildBoundingBox(this IAccBihNode3D node, IBoundingBox3D boundingBox)
        {
            if (!node.HasRightChild) return null;

            var minCorner = boundingBox.GetMinCorner().ToMutableTuple3D();
            var maxCorner = boundingBox.GetMaxCorner().ToMutableTuple3D();

            if (node.HasLeftChild)
            {
                minCorner[node.SplitAxisIndex] = node.ClipValue1;
            }
            else
            {
                //For the special case of a single child, the clip values define
                //the two boundaries of the child.
                minCorner[node.SplitAxisIndex] = node.ClipValue0;
                maxCorner[node.SplitAxisIndex] = node.ClipValue1;
            }

            return BoundingBox3D.CreateFromPoints(minCorner, maxCorner);
        }

        public static IAccBihNode3D<T> GetNode<T>(this IAccBih3D<T> bih, string nodeId)
            where T : IFiniteGeometricShape3D
        {
            var node = bih.RootNode;
            foreach (var childChar in nodeId)
            {
                if (childChar == '0')
                {
                    node = node.LeftChildNode;
                }
                else if (childChar == '1')
                {
                    node = node.RightChildNode;
                }
                else
                    break;

                if (ReferenceEquals(node, null))
                    throw new InvalidOperationException("Invalid Node Id or BIH structure");
            }

            return node;
        }

        public static BoundingBox3D GetNodeBoundingBox<T>(this IAccBih3D<T> bih, string nodeId)
            where T : IFiniteGeometricShape3D
        {
            //Initial bounding box of root node
            var boundingBox = bih.BoundingBox;

            var node = bih.RootNode;
            foreach (var childChar in nodeId)
            {
                if (childChar == '0')
                {
                    boundingBox = node.GetLeftChildBoundingBox(boundingBox);
                    node = node.LeftChildNode;
                }
                else if (childChar == '1')
                {
                    boundingBox = node.GetRightChildBoundingBox(boundingBox);
                    node = node.RightChildNode;
                }
                else
                    break;

                if (ReferenceEquals(boundingBox, null))
                    throw new InvalidOperationException("Invalid Node Id or BIH structure");
            }

            return boundingBox;
        }

        public static AccBihNodeInfo3D GetRootNodeInfo<T>(this IAccBih3D<T> bih)
            where T : IFiniteGeometricShape3D
        {
            return new AccBihNodeInfo3D(
                bih.RootNode,
                BoundingBox3D.CreateInfinite()
            );
        }

        public static AccBihNodeInfo3D GetNodeInfo<T>(this IAccBih3D<T> bih, string nodeId)
            where T : IFiniteGeometricShape3D
        {
            //Initial bounding box of root node
            var boundingBox = bih.BoundingBox;

            var node = bih.RootNode;
            foreach (var childChar in nodeId)
            {
                if (childChar == '0')
                {
                    boundingBox = node.GetLeftChildBoundingBox(boundingBox);
                    node = node.LeftChildNode;
                }
                else if (childChar == '1')
                {
                    boundingBox = node.GetRightChildBoundingBox(boundingBox);
                    node = node.RightChildNode;
                }
                else
                    break;

                if (ReferenceEquals(boundingBox, null))
                    throw new InvalidOperationException("Invalid Node Id or BIH structure");
            }

            return new AccBihNodeInfo3D(node, boundingBox);
        }

        public static AccBih3D<T> ToBih3D<T>(this IReadOnlyList<T> geometricObjectsList, int depthLimit = 100, int singleDepthLimit = 10, int leafObjectsLimit = 1)
            where T : IFiniteGeometricShape3D
        {
            return new AccBih3D<T>(
                geometricObjectsList,
                depthLimit,
                singleDepthLimit,
                leafObjectsLimit
            );
        }
        #endregion


        /// <summary>
        /// Get all nodes in the BIH starting at this node in breadth-first order
        /// </summary>
        /// <param name="bihNode"></param>
        /// <param name="depthFirst"></param>
        /// <returns></returns>
        public static IEnumerable<IAccBihNode2D<T>> GetNodes<T>(this IAccBihNode2D<T> bihNode, bool depthFirst = false)
            where T : IFiniteGeometricShape2D
        {
            if (bihNode.IsLeaf)
            {
                yield return bihNode;
                yield break;
            }

            var queue = new QueueStack<IAccBihNode2D<T>>(depthFirst)
            {
                bihNode
            };

            while (queue.Count > 0)
            {
                var node = queue.Remove();
                yield return node;

                if (node.IsLeaf)
                    continue;

                if (!ReferenceEquals(node.LeftChildNode, null))
                    queue.Add(node.LeftChildNode);

                if (!ReferenceEquals(node.RightChildNode, null))
                    queue.Add(node.RightChildNode);
            }
        }

        /// <summary>
        /// Get all nodes in the BIH starting at this node in breadth-first or
        /// depth-first order
        /// </summary>
        /// <param name="bihNode"></param>
        /// <param name="depthFirst"></param>
        /// <returns></returns>
        public static IEnumerable<IAccBihNode2D> GetNodes(this IAccBihNode2D bihNode, bool depthFirst = false)
        {
            if (bihNode.IsLeaf)
            {
                yield return bihNode;
                yield break;
            }

            var queue = new QueueStack<IAccBihNode2D>(depthFirst) { bihNode };

            while (queue.Count > 0)
            {
                var node = queue.Remove();
                yield return node;

                if (node.IsLeaf)
                    continue;

                if (node.HasLeftChild)
                    queue.Add(node.LeftChild);

                if (node.HasRightChild)
                    queue.Add(node.RightChild);
            }
        }

        /// <summary>
        /// Get all nodes in the BIH starting at this node in breadth-first order
        /// </summary>
        /// <param name="bihNode"></param>
        /// <param name="depthFirst"></param>
        /// <returns></returns>
        public static IEnumerable<AccBihNodeLeaf2D<T>> GetLeafNodes<T>(this IAccBihNode2D<T> bihNode, bool depthFirst = false)
            where T : IFiniteGeometricShape2D
        {
            if (bihNode.IsLeaf)
            {
                yield return bihNode as AccBihNodeLeaf2D<T>;
                yield break;
            }

            var queue = new QueueStack<IAccBihNode2D<T>>(depthFirst)
            {
                bihNode
            };

            while (queue.Count > 0)
            {
                var node = queue.Remove();
                
                if (node.IsLeaf)
                {
                    yield return node as AccBihNodeLeaf2D<T>;
                    continue;
                }

                if (!ReferenceEquals(node.LeftChildNode, null))
                    queue.Add(node.LeftChildNode);

                if (!ReferenceEquals(node.RightChildNode, null))
                    queue.Add(node.RightChildNode);
            }
        }

        /// <summary>
        /// Get all nodes in the BIH starting at this node in breadth-first or
        /// depth-first order
        /// </summary>
        /// <param name="bihNode"></param>
        /// <param name="depthFirst"></param>
        /// <returns></returns>
        public static IEnumerable<IAccBihNode2D> GetLeafNodes(this IAccBihNode2D bihNode, bool depthFirst = false)
        {
            if (bihNode.IsLeaf)
            {
                yield return bihNode;
                yield break;
            }

            var queue = new QueueStack<IAccBihNode2D>(depthFirst) { bihNode };

            while (queue.Count > 0)
            {
                var node = queue.Remove();

                if (node.IsLeaf)
                {
                    yield return node;
                    continue;
                }

                if (node.HasLeftChild)
                    queue.Add(node.LeftChild);

                if (node.HasRightChild)
                    queue.Add(node.RightChild);
            }
        }

        /// <summary>
        /// Computes the depth difference between this node and its deepest descendant
        /// </summary>
        /// <param name="bihNode"></param>
        /// <returns></returns>
        public static int GetChildHierarchyDepth(this IAccBihNode2D bihNode)
        {
            if (bihNode.IsLeaf) return 0;

            var maxNodeDepth =
                bihNode
                    .GetNodes()
                    .Max(n => n.NodeDepth);

            return maxNodeDepth - bihNode.NodeDepth;
        }

        public static AccBihLineTraverser2D<T> GetLineTraverser<T>(this IAccBih2D<T> bih, ILine2D line)
            where T : IFiniteGeometricShape2D
        {
            return AccBihLineTraverser2D<T>.Create(bih, line);
        }

        public static AccBihLineTraverser2D<T> GetLineTraverser<T>(this IAccBih2D<T> bih, ILine2D line, double lineParamValue1, double lineParamValue2)
            where T : IFiniteGeometricShape2D
        {
            return AccBihLineTraverser2D<T>.Create(
                bih,
                line,
                BoundingBox1D.Create(lineParamValue1, lineParamValue2)
            );
        }

        public static AccBihLineTraverser2D<T> GetLineTraverser<T>(this IAccBih2D<T> bih, ILine2D line, IBoundingBox1D lineParamRange)
            where T : IFiniteGeometricShape2D
        {
            return AccBihLineTraverser2D<T>.Create(
                bih,
                line,
                lineParamRange
            );
        }


        /// <summary>
        /// Get all nodes in the BIH starting at this node in breadth-first order
        /// </summary>
        /// <param name="bihNode"></param>
        /// <param name="depthFirst"></param>
        /// <returns></returns>
        public static IEnumerable<IAccBihNode3D<T>> GetNodes<T>(this IAccBihNode3D<T> bihNode, bool depthFirst = false)
            where T : IFiniteGeometricShape3D
        {
            if (bihNode.IsLeaf)
            {
                yield return bihNode;
                yield break;
            }

            var queue = new QueueStack<IAccBihNode3D<T>>(depthFirst)
            {
                bihNode
            };

            while (queue.Count > 0)
            {
                var node = queue.Remove();
                yield return node;

                if (node.IsLeaf)
                    continue;

                if (!ReferenceEquals(node.LeftChildNode, null))
                    queue.Add(node.LeftChildNode);

                if (!ReferenceEquals(node.RightChildNode, null))
                    queue.Add(node.RightChildNode);
            }
        }

        /// <summary>
        /// Get all nodes in the BIH starting at this node in breadth-first or
        /// depth-first order
        /// </summary>
        /// <param name="bihNode"></param>
        /// <param name="depthFirst"></param>
        /// <returns></returns>
        public static IEnumerable<IAccBihNode3D> GetNodes(this IAccBihNode3D bihNode, bool depthFirst = false)
        {
            if (bihNode.IsLeaf)
            {
                yield return bihNode;
                yield break;
            }

            var queue = new QueueStack<IAccBihNode3D>(depthFirst) { bihNode };

            while (queue.Count > 0)
            {
                var node = queue.Remove();
                yield return node;

                if (node.IsLeaf)
                    continue;

                if (node.HasLeftChild)
                    queue.Add(node.LeftChild);

                if (node.HasRightChild)
                    queue.Add(node.RightChild);
            }
        }

        /// <summary>
        /// Get all nodes in the BIH starting at this node in breadth-first order
        /// </summary>
        /// <param name="bihNode"></param>
        /// <param name="depthFirst"></param>
        /// <returns></returns>
        public static IEnumerable<AccBihNodeLeaf3D<T>> GetLeafNodes<T>(this IAccBihNode3D<T> bihNode, bool depthFirst = false)
            where T : IFiniteGeometricShape3D
        {
            if (bihNode.IsLeaf)
            {
                yield return bihNode as AccBihNodeLeaf3D<T>;
                yield break;
            }

            var queue = new QueueStack<IAccBihNode3D<T>>(depthFirst)
            {
                bihNode
            };

            while (queue.Count > 0)
            {
                var node = queue.Remove();

                if (node.IsLeaf)
                {
                    yield return node as AccBihNodeLeaf3D<T>;
                    continue;
                }

                if (!ReferenceEquals(node.LeftChildNode, null))
                    queue.Add(node.LeftChildNode);

                if (!ReferenceEquals(node.RightChildNode, null))
                    queue.Add(node.RightChildNode);
            }
        }

        /// <summary>
        /// Get all nodes in the BIH starting at this node in breadth-first or
        /// depth-first order
        /// </summary>
        /// <param name="bihNode"></param>
        /// <param name="depthFirst"></param>
        /// <returns></returns>
        public static IEnumerable<IAccBihNode3D> GetLeafNodes(this IAccBihNode3D bihNode, bool depthFirst = false)
        {
            if (bihNode.IsLeaf)
            {
                yield return bihNode;
                yield break;
            }

            var queue = new QueueStack<IAccBihNode3D>(depthFirst) { bihNode };

            while (queue.Count > 0)
            {
                var node = queue.Remove();

                if (node.IsLeaf)
                {
                    yield return node;
                    continue;
                }

                if (node.HasLeftChild)
                    queue.Add(node.LeftChild);

                if (node.HasRightChild)
                    queue.Add(node.RightChild);
            }
        }

        /// <summary>
        /// Computes the depth difference between this node and its deepest descendant
        /// </summary>
        /// <param name="bihNode"></param>
        /// <returns></returns>
        public static int GetChildHierarchyDepth(this IAccBihNode3D bihNode)
        {
            if (bihNode.IsLeaf) return 0;

            var maxNodeDepth =
                bihNode
                    .GetNodes()
                    .Max(n => n.NodeDepth);

            return maxNodeDepth - bihNode.NodeDepth;
        }

        public static AccBihLineTraverser3D<T> GetLineTraverser<T>(this IAccBih3D<T> bih, ILine3D line)
            where T : IFiniteGeometricShape3D
        {
            return AccBihLineTraverser3D<T>.Create(bih, line);
        }

        public static AccBihLineTraverser3D<T> GetLineTraverser<T>(this IAccBih3D<T> bih, ILine3D line, double lineParamValue1, double lineParamValue2)
            where T : IFiniteGeometricShape3D
        {
            return AccBihLineTraverser3D<T>.Create(
                bih,
                line,
                BoundingBox1D.Create(lineParamValue1, lineParamValue2)
            );
        }

        public static AccBihLineTraverser3D<T> GetLineTraverser<T>(this IAccBih3D<T> bih, ILine3D line, IBoundingBox1D lineParamRange)
            where T : IFiniteGeometricShape3D
        {
            return AccBihLineTraverser3D<T>.Create(
                bih,
                line,
                lineParamRange
            );
        }


        public static string Describe(this AccBihNodeInfo2D bihNodeInfo)
        {
            var composer = new LinearTextComposer();

            composer
                .AppendAtNewLine("Leaf Nodes: ")
                .AppendLine(bihNodeInfo.Node.GetLeafNodes().Count());

            foreach (var nodeInfo in bihNodeInfo.GetNodesInfo())
            {
                composer
                    .AppendAtNewLine(nodeInfo.NodeId)
                    .Append(nodeInfo.IsLeaf ? " Leaf Node: " : " Internal Node: ")
                    //.Append(nodeInfo.IntersectionTestsEnabled ? "Enabled, " : "Disabled, ")
                    .Append("<")
                    .Append(nodeInfo.FirstObjectIndex)
                    .Append(", ")
                    .Append(nodeInfo.LastObjectIndex)
                    .AppendLine(">");
            }

            return composer.ToString();
        }

        public static string Describe<T>(this IAccBihNode3D<T> bihNode)
            where T : IFiniteGeometricShape3D
        {
            var composer = new LinearTextComposer();

            var maxDepth = bihNode.GetChildHierarchyDepth();

            var idsStack = new Stack<string>();
            idsStack.Push("");

            var nodesStack = new Stack<IAccBihNode3D<T>>();
            nodesStack.Push(bihNode);

            while (nodesStack.Count > 0)
            {
                var id = idsStack.Pop();
                var node = nodesStack.Pop();

                composer
                    .AppendAtNewLine(id.PadLeft(maxDepth, '-'))
                    .Append(node.IsLeaf ? " Leaf Node: " : " Internal Node: ")
                    //.Append(node.IntersectionTestsEnabled ? "Enabled, " : "Disabled, ")
                    .Append("<")
                    .Append(node.FirstObjectIndex)
                    .Append(", ")
                    .Append(node.LastObjectIndex)
                    .AppendLine(">");

                if (node.IsLeaf)
                {

                    continue;
                }

                if (node.HasLeftChild)
                {
                    idsStack.Push("0" + id);
                    nodesStack.Push(node.LeftChildNode);
                }

                if (node.HasRightChild)
                {
                    idsStack.Push("1" + id);
                    nodesStack.Push(node.RightChildNode);
                }
            }

            return composer.ToString();
        }
    }
}
