namespace GraphicsComposerLib.GraphViz.Dot
{
    /// <summary>
    /// The type of the fixed code
    /// </summary>
    public enum DotFixedCodeType
    {
        /// <summary>
        /// The code text is output as-is to the final dot code
        /// </summary>
        AsIs,

        /// <summary>
        /// The code text is separated into lines with each starting with a //
        /// </summary>
        SingleLineComment,

        /// <summary>
        /// The code text is surrounded by /* */
        /// </summary>
        MultiLineComment
    }

    /// <summary>
    /// This class represents fixed code that will be output as-is in the
    /// final dot code
    /// </summary>
    public sealed class DotFixedCode : IDotStatement
    {
        public IDotGraph ParentGraph { get; }

        public DotGraph MainGraph => ParentGraph.MainGraph;

        /// <summary>
        /// The text of this fixed code
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// The type of the fixed code: as-is, single line comment, or multi-line comment
        /// </summary>
        public DotFixedCodeType CodeType { get; set; }


        internal DotFixedCode(IDotGraph parentGraph, string code)
        {
            ParentGraph = parentGraph;
            Code = code;
        }


        public override string ToString()
        {
            return Code;
        }
    }
}
