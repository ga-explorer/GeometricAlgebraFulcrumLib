using System.Collections.Generic;

namespace TextComposerLib.Text.Parametric
{
    internal sealed class PtcLine
    {
        internal string LineText { get; set; }

        internal List<PtcLineSegment> LineSegments { get; }


        internal PtcLine(string lineText)
        {
            LineText = lineText;

            LineSegments = new List<PtcLineSegment>();
        }


        internal int AddFixedSegment(int segmentTextStart, int segmentTextEnd)
        {
            if (segmentTextEnd < segmentTextStart)
                return -1;

            LineSegments.Add(
                new PtcLineSegment(
                    LineText.Substring(
                        segmentTextStart, 
                        segmentTextEnd - segmentTextStart + 1
                        )
                    )
                );

            //Return the location of the start of the next segment
            return segmentTextEnd + 1;
        }

        internal int AddParametricSegment(int segmentTextStart, PtcParameter parameter)
        {
            LineSegments.Add(
                new PtcLineSegment(parameter)
                );

            //Return the location of the start of the next segment
            return segmentTextStart + parameter.ParameterPlaceholderLength;
        }


        public override string ToString()
        {
            return LineText;
        }
    }
}
