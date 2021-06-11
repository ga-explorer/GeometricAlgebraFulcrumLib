namespace GAPoTNumLib.Text.Linear.LineHeader
{
    public sealed class LtcLineCount : LtcLineHeader
    {
        public string FormatString { get; } = "D4";

        public LinearTextComposer ParentComposer { get; }


        public LtcLineCount(LinearTextComposer parentComposer)
        {
            ParentComposer = parentComposer;
        }

        public LtcLineCount(LinearTextComposer parentComposer, string formatString)
        {
            ParentComposer = parentComposer;
            FormatString = formatString;
        }


        public override void Reset()
        {
        }

        public override string GetHeaderText()
        {
            return 
                string.IsNullOrEmpty(FormatString) 
                ? ParentComposer.LinesCount.ToString() 
                : ParentComposer.LinesCount.ToString(FormatString);
        }
    }
}
