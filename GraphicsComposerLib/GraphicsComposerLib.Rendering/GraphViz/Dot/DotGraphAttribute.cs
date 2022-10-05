using System.Text;

namespace GraphicsComposerLib.Rendering.GraphViz.Dot
{
    /// <summary>
    /// This class represents a graph attribute statement in the AST
    /// </summary>
    public sealed class DotGraphAttribute : IDotStatement
    {
        public IDotGraph ParentGraph { get; }

        public DotGraph MainGraph => ParentGraph.MainGraph;

        /// <summary>
        /// The name of the attribute
        /// </summary>
        public string AttrName { get; }

        /// <summary>
        /// The string value of the attribute
        /// </summary>
        public string AttrValue { get; }


        internal DotGraphAttribute(IDotGraph parentGraph, string name, string value)
        {
            AttrName = name;
            AttrValue = value;
            ParentGraph = parentGraph;
        }


        public override string ToString()
        {
            var s = new StringBuilder(64);

            s.Append(AttrName).Append("=").Append(AttrValue);

            return s.ToString();
        }
    }
}
