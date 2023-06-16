using NumericalGeometryLib.Computers;

namespace NumericalGeometryLib.Accelerators.BIH.Space3D.Traversal
{
    public readonly struct AccBihLineTraversalData3D
    {
        public double OriginValue { get; }

        public double DirectionValue { get; }

        public double DirectionInvValue { get; }

        public double LineClipParameterValue0 { get; }

        public double LineClipParameterValue1 { get; }

        public bool IsDirectionPositive => DirectionValue > 0;

        public bool IsDirectionNegative => DirectionValue < 0;

        public bool IsDirectionZero => DirectionValue == 0;


        internal AccBihLineTraversalData3D(IAccBihNode3D bihNode, LineTraversalData3D lineData)
        {
            var splitAxisIndex = bihNode.SplitAxisIndex;

            OriginValue = lineData.Origin[1 << splitAxisIndex];
            DirectionValue = lineData.Direction[1 << splitAxisIndex];
            DirectionInvValue = lineData.DirectionInv[1 << splitAxisIndex];
            LineClipParameterValue0 = (bihNode.ClipValue0 - OriginValue) * DirectionInvValue;
            LineClipParameterValue1 = (bihNode.ClipValue1 - OriginValue) * DirectionInvValue;
        }
    }
}