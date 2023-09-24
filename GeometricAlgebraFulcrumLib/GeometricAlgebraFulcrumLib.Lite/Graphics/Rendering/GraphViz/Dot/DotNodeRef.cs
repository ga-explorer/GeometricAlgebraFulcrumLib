using System.Text;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.GraphViz.Dot.Value;
using TextComposerLib;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.GraphViz.Dot
{
    /// <summary>
    /// This class represents a node reference in the dot code
    /// See http://www.graphviz.org/content/attrs for more details
    /// </summary>
    public sealed class DotNodeRef : IDotEdgeSide
    {
        /// <summary>
        /// The name of the referenced node
        /// </summary>
        public string NodeName { get; }

        /// <summary>
        /// An optional port of the referenced node
        /// </summary>
        public string PortName { get; }

        /// <summary>
        /// An optional compass direction of the referenced node
        /// </summary>
        public DotCompass Compass { get; }

        /// <summary>
        /// True if this node reference contains a port name
        /// </summary>
        public bool HasPort => String.IsNullOrEmpty(PortName) == false;

        /// <summary>
        /// True if this node reference contains a compass direction
        /// </summary>
        public bool HasCompass => Compass != null;


        internal DotNodeRef(string nodeName, string portName, DotCompass compass)
        {
            NodeName = nodeName;
            PortName = portName;
            Compass = compass;
        }


        public override string ToString()
        {
            var s = new StringBuilder();

            s.Append(NodeName.ValueToQuotedLiteral());

            if (HasPort)
                s.Append(":").Append(PortName.ValueToQuotedLiteral());

            if (HasCompass)
                s.Append(":").Append(Compass.Value);

            return s.ToString();
        }
    }
}
