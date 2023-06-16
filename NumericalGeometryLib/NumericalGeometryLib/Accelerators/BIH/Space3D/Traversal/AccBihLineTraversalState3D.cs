using System;
using System.Collections.Generic;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using NumericalGeometryLib.Computers;

namespace NumericalGeometryLib.Accelerators.BIH.Space3D.Traversal
{
    public struct AccBihLineTraversalState3D
    {
        public IAccBihNode3D BihNode { get; }

        public double LineParameterMinValue { get; private set; }

        public double LineParameterMaxValue { get; private set; }

        public Float64Range1D LineParameterRange
            => Float64Range1D.Create(
                LineParameterMinValue,
                LineParameterMaxValue
            );
        


        internal AccBihLineTraversalState3D(IAccBihNode3D bihNode, double lineParameterMinValue, double lineParameterMaxValue)
        {
            Debug.Assert(!ReferenceEquals(bihNode, null));

            LineParameterMinValue = lineParameterMinValue;
            LineParameterMaxValue = lineParameterMaxValue;
            BihNode = bihNode;
        }

        internal AccBihLineTraversalState3D(IAccBihNode3D bihNode, Float64Range1D lineParameterRange)
        {
            Debug.Assert(!ReferenceEquals(bihNode, null));

            LineParameterMinValue = lineParameterRange.MinValue;
            LineParameterMaxValue = lineParameterRange.MaxValue;
            BihNode = bihNode;
        }


        public bool RestrictLineParameterRange(Float64Range1D lineParamLimits)
        {
            if (LineParameterMaxValue < lineParamLimits.MinValue ||
                LineParameterMinValue > lineParamLimits.MaxValue)
                return false;

            if (LineParameterMinValue < lineParamLimits.MinValue)
                LineParameterMinValue = lineParamLimits.MinValue;

            if (LineParameterMaxValue > lineParamLimits.MaxValue)
                LineParameterMaxValue = lineParamLimits.MaxValue;

            return true;
        }


        public AccBihLineTraversalData3D GetTraversalData(LineTraversalData3D lineData)
        {
            return new AccBihLineTraversalData3D(
                BihNode,
                lineData
            );
        }


        #region Compute child states of this state
        /// <summary>
        /// Case with single internal node (an internal node with a single child)
        /// </summary>
        private IEnumerable<AccBihLineTraversalState3D> GetChildStates_SingleInternalNode(AccBihLineTraversalData3D bihLineData)
        {
            var node = BihNode;
            var t0 = LineParameterMinValue;
            var t1 = LineParameterMaxValue;
            var s0 = bihLineData.LineClipParameterValue0;
            var s1 = bihLineData.LineClipParameterValue1;

            //Line direction is positive
            if (bihLineData.IsDirectionPositive)
            {
                if (t0 <= s1 && t1 >= s0)
                    yield return new AccBihLineTraversalState3D(
                        node.HasLeftChild ? node.LeftChild : node.RightChild,
                        Math.Max(t0, s0),
                        Math.Min(t1, s1)
                    );
            }

            //Line direction is negative
            else if (bihLineData.IsDirectionNegative)
            {
                if (t0 <= s0 && t1 >= s1)
                    yield return new AccBihLineTraversalState3D(
                        node.HasLeftChild ? node.LeftChild : node.RightChild,
                        Math.Max(t0, s1),
                        Math.Min(t1, s0)
                    );
            }

            //Line direction is zero
            else
            {
                if (bihLineData.OriginValue >= node.ClipValue0 && bihLineData.OriginValue <= node.ClipValue1)
                    yield return new AccBihLineTraversalState3D(
                        node.HasLeftChild ? node.LeftChild : node.RightChild,
                        t0,
                        t1
                    );
            }
        }


        /// <summary>
        /// Case when both child nodes exist, line direction is positive, and
        /// child node regions do not overlap
        /// </summary>
        /// <param name="bihLineData"></param>
        /// <returns></returns>
        private IEnumerable<AccBihLineTraversalState3D> GetChildStates_PDirection_NoOverlap(AccBihLineTraversalData3D bihLineData)
        {
            var node = BihNode;
            var t0 = LineParameterMinValue;
            var t1 = LineParameterMaxValue;
            var s0 = bihLineData.LineClipParameterValue0;
            var s1 = bihLineData.LineClipParameterValue1;

            Debug.Assert(s0 < s1);

            //Case  1: s0 < s1 < t0 < t1
            if (s1 <= t0)
                yield return new AccBihLineTraversalState3D(
                    node.RightChild, t0, t1
                );

            //Case  2: s0 < t0 < s1 < t1
            else if (s0 <= t0 && t0 <= s1 && s1 <= t1)
                yield return new AccBihLineTraversalState3D(
                    node.RightChild, s1, t1
                );

            //Case  3: s0 < t0 < t1 < s1
            //No traversal

            //Case  4: t0 < s0 < s1 < t1
            else if (t0 <= s0 && s1 <= t1)
            {
                yield return new AccBihLineTraversalState3D(
                    node.LeftChild, t0, s0
                );

                yield return new AccBihLineTraversalState3D(
                    node.RightChild, s1, t1
                );
            }

            //Case  5: t0 < s0 < t1 < s1
            else if (t0 <= s0 && s0 <= t1 && t1 <= s1)
                yield return new AccBihLineTraversalState3D(
                    node.LeftChild, t0, s0
                );

            //Case  6: t0 < t1 < s0 < s1
            else if (t1 <= s0)
                yield return new AccBihLineTraversalState3D(
                    node.LeftChild, t0, t1
                );
        }

        /// <summary>
        /// Case when both child nodes exist, line direction is positive, and
        /// child node regions overlap
        /// </summary>
        /// <param name="bihLineData"></param>
        /// <returns></returns>
        private IEnumerable<AccBihLineTraversalState3D> GetChildStates_PDirection_Overlap(AccBihLineTraversalData3D bihLineData)
        {
            var node = BihNode;
            var t0 = LineParameterMinValue;
            var t1 = LineParameterMaxValue;
            var s0 = bihLineData.LineClipParameterValue0;
            var s1 = bihLineData.LineClipParameterValue1;

            Debug.Assert(s1 < s0);

            //Case  1: s1 < s0 < t0 < t1
            if (s0 <= t0)
                yield return new AccBihLineTraversalState3D(
                    node.RightChild, t0, t1
                );

            //Case  2: s1 < t0 < s0 < t1
            else if (s1 <= t0 && t0 <= s0 && s0 <= t1)
            {
                yield return new AccBihLineTraversalState3D(
                    node.LeftChild, t0, s0
                );

                yield return new AccBihLineTraversalState3D(
                    node.RightChild, t0, t1
                );
            }

            //Case  3: s1 < t0 < t1 < s0
            else if (s1 <= t0 && t1 <= s0)
            {
                yield return new AccBihLineTraversalState3D(
                    node.LeftChild, t0, t1
                );

                yield return new AccBihLineTraversalState3D(
                    node.RightChild, t0, t1
                );
            }

            //Case 4: t0 < s1 < s0 < t1
            else if (t0 <= s1 && s0 <= t1)
            {
                yield return new AccBihLineTraversalState3D(
                    node.LeftChild, t0, s0
                );

                yield return new AccBihLineTraversalState3D(
                    node.RightChild, s1, t1
                );
            }

            //Case 5: t0 < s1 < t1 < s0
            else if (t0 <= s1 && s1 <= t1 && t1 <= s0)
            {
                yield return new AccBihLineTraversalState3D(
                    node.LeftChild, t0, t1
                );

                yield return new AccBihLineTraversalState3D(
                    node.RightChild, s1, t1
                );
            }

            //Case 6: t0 < t1 < s1 < s0
            else if (t1 <= s1)
                yield return new AccBihLineTraversalState3D(
                    node.LeftChild, t0, t1
                );
        }

        /// <summary>
        /// Case when both child nodes exist, line direction is positive, and
        /// child node regions touch
        /// </summary>
        /// <param name="bihLineData"></param>
        /// <returns></returns>
        private IEnumerable<AccBihLineTraversalState3D> GetChildStates_PDirection_Touch(AccBihLineTraversalData3D bihLineData)
        {
            var node = BihNode;
            var t0 = LineParameterMinValue;
            var t1 = LineParameterMaxValue;
            var s0 = bihLineData.LineClipParameterValue0;

            //Case 13: s0 < t0 < t1
            if (s0 < t0 && node.HasRightChild)
                yield return
                    new AccBihLineTraversalState3D(node.RightChild, t0, t1);

            //Case 14: t0 < s0 < t1
            else if (t0 <= s0 && s0 <= t1)
            {
                if (node.HasRightChild)
                    yield return
                        new AccBihLineTraversalState3D(node.RightChild, s0, t1);

                if (node.HasLeftChild)
                    yield return
                        new AccBihLineTraversalState3D(node.LeftChild, t0, s0);
            }

            //Case 15: t0 < t1 < s0
            else if (t1 <= s0 && node.HasLeftChild)
                yield return
                    new AccBihLineTraversalState3D(node.LeftChild, t0, t1);
        }


        /// <summary>
        /// Case when both child nodes exist, line direction is negative, and
        /// child node regions do not overlap
        /// </summary>
        /// <param name="bihLineData"></param>
        /// <returns></returns>
        private IEnumerable<AccBihLineTraversalState3D> GetChildStates_NDirection_NoOverlap(AccBihLineTraversalData3D bihLineData)
        {
            var node = BihNode;
            var t0 = LineParameterMinValue;
            var t1 = LineParameterMaxValue;
            var s0 = bihLineData.LineClipParameterValue0;
            var s1 = bihLineData.LineClipParameterValue1;

            Debug.Assert(s1 < s0);

            //Case 16: s1 < s0 < t0 < t1
            if (s0 <= t0)
                yield return new AccBihLineTraversalState3D(
                    node.LeftChild, t0, t1
                );

            //Case 17: s1 < t0 < s0 < t1
            else if (s1 <= t0 && t0 <= s0 && s0 <= t1)
                yield return new AccBihLineTraversalState3D(
                    node.LeftChild, s0, t1
                );

            //Case 18: s1 < t0 < t1 < s0
            //No traversal

            //Case 19: t0 < s1 < s0 < t1
            else if (t0 <= s1 && s0 <= t1)
            {
                yield return new AccBihLineTraversalState3D(
                    node.LeftChild, s0, t1
                );

                yield return new AccBihLineTraversalState3D(
                    node.RightChild, t0, s1
                );
            }

            //Case 20: t0 < s1 < t1 < s0
            else if (t0 <= s1 && s1 <= t1 && t1 <= s0)
                yield return new AccBihLineTraversalState3D(
                    node.RightChild, t0, s1
                );

            //Case 21: t0 < t1 < s1 < s0
            else if (t1 <= s1)
                yield return new AccBihLineTraversalState3D(
                    node.RightChild, t0, t1
                );
        }

        /// <summary>
        /// Case when both child nodes exist, line direction is negative, and
        /// child node regions overlap
        /// </summary>
        /// <param name="bihLineData"></param>
        /// <returns></returns>
        private IEnumerable<AccBihLineTraversalState3D> GetChildStates_NDirection_Overlap(AccBihLineTraversalData3D bihLineData)
        {
            var node = BihNode;
            var t0 = LineParameterMinValue;
            var t1 = LineParameterMaxValue;
            var s0 = bihLineData.LineClipParameterValue0;
            var s1 = bihLineData.LineClipParameterValue1;

            Debug.Assert(s0 < s1);

            //Case 1: s0 < s1 < t0 < t1
            if (s1 <= t0)
                yield return new AccBihLineTraversalState3D(
                    node.LeftChild, t0, t1
                );

            //Case 2: s0 < t0 < s1 < t1
            else if (s0 <= t0 && t0 <= s1 && s1 <= t1)
            {
                yield return new AccBihLineTraversalState3D(
                    node.LeftChild, t0, t1
                );

                yield return new AccBihLineTraversalState3D(
                    node.RightChild, t0, s1
                );
            }

            //Case 3: s0 < t0 < t1 < s1
            else if (s0 <= t0 && t1 <= s1)
            {
                yield return new AccBihLineTraversalState3D(
                    node.LeftChild, t0, t1
                );

                yield return new AccBihLineTraversalState3D(
                    node.RightChild, t0, t1
                );
            }

            //Case 4: t0 < s0 < s1 < t1
            else if (t0 <= s0 && s1 <= t1)
            {
                yield return new AccBihLineTraversalState3D(
                    node.LeftChild, s0, t1
                );

                yield return new AccBihLineTraversalState3D(
                    node.RightChild, t0, s1
                );
            }

            //Case 5: t0 < s0 < t1 < s1
            else if (t0 <= s0 && s0 <= t1 && t1 <= s1)
            {
                yield return new AccBihLineTraversalState3D(
                    node.LeftChild, s0, t1
                );

                yield return new AccBihLineTraversalState3D(
                    node.RightChild, t0, t1
                );
            }

            //Case 6: t0 < t1 < s0 < s1
            else if (t1 <= s0)
                yield return new AccBihLineTraversalState3D(
                    node.RightChild, t0, t1
                );
        }

        /// <summary>
        /// Case when both child nodes exist, line direction is negative, and
        /// child node regions touch
        /// </summary>
        /// <param name="bihLineData"></param>
        /// <returns></returns>
        private IEnumerable<AccBihLineTraversalState3D> GetChildStates_NDirection_Touch(AccBihLineTraversalData3D bihLineData)
        {
            var node = BihNode;
            var t0 = LineParameterMinValue;
            var t1 = LineParameterMaxValue;
            var s0 = bihLineData.LineClipParameterValue0;

            //Case 28: s0 < t0 < t1
            if (s0 <= t0 && node.HasLeftChild)
                yield return
                    new AccBihLineTraversalState3D(node.LeftChild, t0, t1);

            //Case 29: t0 < s0 < t1
            else if (t0 <= s0 && s0 <= t1)
            {
                if (node.HasRightChild)
                    yield return
                        new AccBihLineTraversalState3D(node.RightChild, t0, s0);

                if (node.HasLeftChild)
                    yield return
                        new AccBihLineTraversalState3D(node.LeftChild, s0, t1);
            }

            //Case 30: t0 < t1 < s0
            else if (t1 <= s0 && node.HasRightChild)
                yield return
                    new AccBihLineTraversalState3D(node.RightChild, t0, t1);
        }


        /// <summary>
        /// Case when both child nodes exist, and line direction is zero
        /// </summary>
        /// <param name="bihLineData"></param>
        /// <returns></returns>
        private IEnumerable<AccBihLineTraversalState3D> GetChildStates_ZDirection(AccBihLineTraversalData3D bihLineData)
        {
            var node = BihNode;
            var t0 = LineParameterMinValue;
            var t1 = LineParameterMaxValue;

            if (bihLineData.OriginValue <= node.ClipValue0 && node.HasLeftChild)
                yield return
                    new AccBihLineTraversalState3D(node.LeftChild, t0, t1);

            if (bihLineData.OriginValue >= node.ClipValue1 && node.HasRightChild)
                yield return
                    new AccBihLineTraversalState3D(node.RightChild, t0, t1);
        }


        public IEnumerable<AccBihLineTraversalState3D> GetChildStates(LineTraversalData3D lineData)
        {
            Debug.Assert(LineParameterMinValue <= LineParameterMaxValue);

            //Use pre-calculated line values to encode line-BIH child nodes
            //traversal cases
            var bihLineData =
                new AccBihLineTraversalData3D(BihNode, lineData);


            //This is a BihNode with a single child, node clip values define the
            //two boundaries of the child. This requires a special treatment
            if (BihNode.IsSingleInternal)
                return GetChildStates_SingleInternalNode(bihLineData);


            //Both children exist, use original meaning of clip values

            //Both children exist and Line Direction is Positive
            if (bihLineData.IsDirectionPositive)
            {
                //Child Nodes Do Not Overlap
                if (BihNode.ClipValue0 < BihNode.ClipValue1)
                    return GetChildStates_PDirection_NoOverlap(bihLineData);

                //Child Nodes Overlap
                if (BihNode.ClipValue0 > BihNode.ClipValue1)
                    return GetChildStates_PDirection_Overlap(bihLineData);

                //Child Nodes Touch
                return GetChildStates_PDirection_Touch(bihLineData);
            }


            //Both children exist and Line Direction is Negative
            if (bihLineData.IsDirectionNegative)
            {
                //Child Nodes Do Not Overlap
                if (BihNode.ClipValue0 < BihNode.ClipValue1)
                    return GetChildStates_NDirection_NoOverlap(bihLineData);

                //Child Nodes Overlap
                if (BihNode.ClipValue0 > BihNode.ClipValue1)
                    return GetChildStates_NDirection_Overlap(bihLineData);

                //Child Nodes Touch
                return GetChildStates_NDirection_Touch(bihLineData);
            }


            //Both children exist and Line Direction zero
            return GetChildStates_ZDirection(bihLineData);
        }
        #endregion
    }
}