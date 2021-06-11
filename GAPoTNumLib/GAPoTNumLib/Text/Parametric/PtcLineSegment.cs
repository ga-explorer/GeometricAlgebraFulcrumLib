namespace GAPoTNumLib.Text.Parametric
{
    internal sealed class PtcLineSegment
    {
        internal string SegmentText { get; }

        internal PtcParameter SegmentParameter { get; }

        internal bool IsFixed => SegmentParameter == null;

        internal bool IsParametric => SegmentParameter != null;


        internal PtcLineSegment(string segmentText)
        {
            SegmentText = segmentText;

            SegmentParameter = null;
        }

        internal PtcLineSegment(PtcParameter segmentParameter)
        {
            SegmentText = segmentParameter.ParameterPlaceholder;

            SegmentParameter = segmentParameter;
        }


        public override string ToString()
        {
            return SegmentText;
        }
    }
}
