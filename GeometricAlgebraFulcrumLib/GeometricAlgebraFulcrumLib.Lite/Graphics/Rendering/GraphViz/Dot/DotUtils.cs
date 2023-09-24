using System.Data;
using System.Text;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.GraphViz.Dot.Color;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.GraphViz.Dot.Label;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.GraphViz.Dot.Label.Table;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.GraphViz.Dot.Label.Text;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.GraphViz.Dot.Value;
using TextComposerLib;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.GraphViz.Dot
{
    /// <summary>
    /// A utilities class for GraphViz dot AST construction
    /// </summary>
    public static class DotUtils
    {
        /// <summary>
        /// Add a statement to the given graph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="statement"></param>
        internal static void AddStatement(this IDotGraph graph, IDotStatement statement)
        {
            var mainGraph = graph as DotGraph;

            if (mainGraph != null)
            {
                mainGraph.StatementsList.Add(statement);

                return;
            }

            var subGraph = graph as DotSubGraph;

            subGraph?.StatementsList.Add(statement);
        }


        #region Conversion Utilities
        internal static string ToDotDouble(this float value)
        {
            return value.ToString("0.0000000");
        }

        internal static string ToDotDouble(this double value)
        {
            return value.ToString("0.0000000");
        }

        internal static string ToDotSignedDouble(this float value)
        {
            var v =
                value > 0
                    ? "+" + value.ToString("0.0000000")
                    : value.ToString("0.0000000");

            return v.ValueToQuotedLiteral();
        }

        internal static string ToDotSignedDouble(this double value)
        {
            var v =
                value > 0
                ? "+" + value.ToString("0.0000000")
                : value.ToString("0.0000000");

            return v.ValueToQuotedLiteral();
        }

        internal static string ToDotPoint(float x, float y)
        {
            var s = new StringBuilder();

            s.Append('\"')
                .Append(x.ToDotDouble())
                .Append(',')
                .Append(y.ToDotDouble())
                .Append('\"');

            return s.ToString();
        }

        internal static string ToDotPoint(double x, double y)
        {
            var s = new StringBuilder();

            s.Append('\"')
                .Append(x.ToDotDouble())
                .Append(',')
                .Append(y.ToDotDouble())
                .Append('\"');

            return s.ToString();
        }

        internal static string ToDotPoint(double x, double y, double z)
        {
            var s = new StringBuilder();

            s.Append('\"')
                .Append(x.ToDotDouble())
                .Append(',')
                .Append(y.ToDotDouble())
                .Append(',')
                .Append(z.ToDotDouble())
                .Append('\"');

            return s.ToString();
        }

        internal static string ToDotSignedPoint(float x, float y)
        {
            var s = new StringBuilder();

            s.Append('\"')
                .Append(x.ToDotSignedDouble())
                .Append(',')
                .Append(y.ToDotSignedDouble())
                .Append('\"');

            return s.ToString();
        }

        internal static string ToDotSignedPoint(double x, double y)
        {
            var s = new StringBuilder();

            s.Append('\"')
                .Append(x.ToDotSignedDouble())
                .Append(',')
                .Append(y.ToDotSignedDouble())
                .Append('\"');

            return s.ToString();
        }

        internal static string ToDotRect(float x1, float y1, float x2, float y2)
        {
            var llx = Math.Min(x1, x2).ToDotDouble();
            var lly = Math.Min(y1, y2).ToDotDouble();
            var urx = Math.Max(x1, x2).ToDotDouble();
            var ury = Math.Max(y1, y2).ToDotDouble();

            var s = new StringBuilder();

            s.Append(llx).Append(',').Append(lly).Append(',').Append(urx).Append(ury);

            return s.ToString();
        }

        internal static string ToDotStyle(this IEnumerable<DotClusterStyle> styles)
        {
            var s = new StringBuilder();

            s.Append('\"');

            var flag = false;
            foreach (var style in styles)
            {
                if (flag)
                    s.Append(',');
                else
                    flag = true;

                s.Append(style.Value);
            }

            s.Append('\"');

            return s.ToString();
        }

        internal static string ToDotStyle(this IEnumerable<DotNodeStyle> styles)
        {
            var s = new StringBuilder();

            s.Append('\"');

            var flag = false;
            foreach (var style in styles)
            {
                if (flag)
                    s.Append(',');
                else
                    flag = true;

                s.Append(style.Value);
            }

            s.Append('\"');

            return s.ToString();
        }

        /// <summary>
        /// Convert a Color object into a dot RGB color value
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static DotRgbColor ToDotRgbColor(this System.Drawing.Color color)
        {
            return new DotRgbColor(color);
        }

        /// <summary>
        /// Convert a Color object into a dot RGBA color value
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static DotRgbaColor ToDotRgbaColor(this System.Drawing.Color color)
        {
            return new DotRgbaColor(color);
        }

        /// <summary>
        /// Convert a Color object into a dot HSV color value
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static DotHsvColor ToDotHsvColor(this System.Drawing.Color color)
        {
            return new DotHsvColor(color);
        }
        #endregion

        #region Attributes Utilities
        internal static string GetAttribute(this Dictionary<string, string> attrList, string attrName)
        {
            return
                attrList.TryGetValue(attrName, out var value)
                    ? value
                    : String.Empty;
        }

        internal static void ClearAttribute(this Dictionary<string, string> attrList, string attrName)
        {
            if (attrList.ContainsKey(attrName))
                attrList.Remove(attrName);
        }

        internal static void SetAttribute(this Dictionary<string, string> attrList, string attrName, bool attrValue)
        {
            if (attrList.ContainsKey(attrName))
                attrList[attrName] = attrValue.ToString();

            else
                attrList.Add(attrName, attrValue.ToString());
        }

        internal static void SetAttribute(this Dictionary<string, string> attrList, string attrName, string attrValue)
        {
            if (String.IsNullOrEmpty(attrValue))
            {
                if (attrList.ContainsKey(attrName))
                    attrList.Remove(attrName);

                return;
            }

            if (attrList.ContainsKey(attrName))
                attrList[attrName] = attrValue;

            else
                attrList.Add(attrName, attrValue);
        }

        internal static void SetAttribute(this Dictionary<string, string> attrList, string attrName, int attrValue)
        {
            if (attrList.ContainsKey(attrName))
                attrList[attrName] = attrValue.ToString();

            else
                attrList.Add(attrName, attrValue.ToString());
        }

        internal static void SetAttribute(this Dictionary<string, string> attrList, string attrName, float attrValue)
        {
            if (attrList.ContainsKey(attrName))
                attrList[attrName] = attrValue.ToDotDouble();

            else
                attrList.Add(attrName, attrValue.ToDotDouble());
        }
        #endregion

        #region Create an edge from a node Utilities
        /// <summary>
        /// Create an edge from this node to the given side (node or subgraph)
        /// </summary>
        /// <param name="node"></param>
        /// <param name="side"></param>
        /// <returns></returns>
        public static DotEdge AddEdgeTo(this DotNode node, string side)
        {
            var edge = new DotEdge(node.ParentGraph);

            edge.SidesList.Add(node.ToNodeRef());
            edge.SidesList.Add(side.ToNodeRef());

            node.ParentGraph.AddStatement(edge);

            return edge;
        }

        /// <summary>
        /// Create a edges starting from this node and going sequentially to the
        /// given sides one by one
        /// </summary>
        /// <param name="node"></param>
        /// <param name="sides"></param>
        /// <returns></returns>
        public static DotEdge AddEdgeTo(this DotNode node, params string[] sides)
        {
            var edge = new DotEdge(node.ParentGraph);

            edge.SidesList.Add(node.ToNodeRef());
            edge.SidesList.AddRange(sides.Select(ToNodeRef));

            node.ParentGraph.AddStatement(edge);

            return edge;
        }

        /// <summary>
        /// Create a edges starting from this node and going sequentially to the
        /// given sides one by one
        /// </summary>
        /// <param name="node"></param>
        /// <param name="sides"></param>
        /// <returns></returns>
        public static DotEdge AddEdgeTo(this DotNode node, IEnumerable<string> sides)
        {
            var edge = new DotEdge(node.ParentGraph);

            edge.SidesList.Add(node.ToNodeRef());
            edge.SidesList.AddRange(sides.Select(ToNodeRef));

            node.ParentGraph.AddStatement(edge);

            return edge;
        }

        /// <summary>
        /// Create an edge from this node to the given node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="node1"></param>
        /// <returns></returns>
        public static DotEdge AddEdgeTo(this DotNode node, DotNode node1)
        {
            var edge = new DotEdge(node.ParentGraph);

            edge.SidesList.Add(node.ToNodeRef());
            edge.SidesList.Add(node1.ToNodeRef());

            node.ParentGraph.AddStatement(edge);

            return edge;
        }

        /// <summary>
        /// Create a edges starting from this node and going sequentially to the
        /// given nodes one by one
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static DotEdge AddEdgeTo(this DotNode node, params DotNode[] nodes)
        {
            var edge = new DotEdge(node.ParentGraph);

            edge.SidesList.Add(node.ToNodeRef());
            edge.SidesList.AddRange(nodes.Select(ToNodeRef));

            node.ParentGraph.AddStatement(edge);

            return edge;
        }

        /// <summary>
        /// Create a edges starting from this node and going sequentially to the
        /// given nodes one by one
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static DotEdge AddEdgeTo(this DotNode node, IEnumerable<DotNode> nodes)
        {
            var edge = new DotEdge(node.ParentGraph);

            edge.SidesList.Add(node.ToNodeRef());
            edge.SidesList.AddRange(nodes.Select(ToNodeRef));

            node.ParentGraph.AddStatement(edge);

            return edge;
        }

        /// <summary>
        /// Create an edge from this node to the given side
        /// </summary>
        /// <param name="node"></param>
        /// <param name="side1"></param>
        /// <returns></returns>
        public static DotEdge AddEdgeTo(this DotNode node, IDotEdgeSide side1)
        {
            var edge = new DotEdge(node.ParentGraph);

            edge.SidesList.Add(node.ToNodeRef());
            edge.SidesList.Add(side1);

            node.ParentGraph.AddStatement(edge);

            return edge;
        }

        /// <summary>
        /// Create a edges starting from this node and going sequentially to the
        /// given sides one by one
        /// </summary>
        /// <param name="node"></param>
        /// <param name="sides"></param>
        /// <returns></returns>
        public static DotEdge AddEdgeTo(this DotNode node, params IDotEdgeSide[] sides)
        {
            var edge = new DotEdge(node.ParentGraph);

            edge.SidesList.Add(node.ToNodeRef());
            edge.SidesList.AddRange(sides);

            node.ParentGraph.AddStatement(edge);

            return edge;
        }

        /// <summary>
        /// Create a edges starting from this node and going sequentially to the
        /// given sides one by one
        /// </summary>
        /// <param name="node"></param>
        /// <param name="sides"></param>
        /// <returns></returns>
        public static DotEdge AddEdgeTo(this DotNode node, IEnumerable<IDotEdgeSide> sides)
        {
            var edge = new DotEdge(node.ParentGraph);

            edge.SidesList.Add(node.ToNodeRef());
            edge.SidesList.AddRange(sides);

            node.ParentGraph.AddStatement(edge);

            return edge;
        }

        /// <summary>
        /// Create an egde to this node from the given side
        /// </summary>
        /// <param name="node"></param>
        /// <param name="side"></param>
        /// <returns></returns>
        public static DotEdge AddEdgeFrom(this DotNode node, string side)
        {
            var edge = new DotEdge(node.ParentGraph);

            edge.SidesList.Add(side.ToNodeRef());
            edge.SidesList.Add(node.ToNodeRef());

            node.ParentGraph.AddStatement(edge);

            return edge;
        }

        /// <summary>
        /// Create sequential edges between the given sides ending at this node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="sides"></param>
        /// <returns></returns>
        public static DotEdge AddEdgeFrom(this DotNode node, IEnumerable<string> sides)
        {
            var edge = new DotEdge(node.ParentGraph);

            edge.SidesList.AddRange(sides.Select(ToNodeRef));
            edge.SidesList.Add(node.ToNodeRef());

            node.ParentGraph.AddStatement(edge);

            return edge;
        }

        /// <summary>
        /// Create an edge to this node from the given node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="node1"></param>
        /// <returns></returns>
        public static DotEdge AddEdgeFrom(this DotNode node, DotNode node1)
        {
            var edge = new DotEdge(node.ParentGraph);

            edge.SidesList.Add(node1.ToNodeRef());
            edge.SidesList.Add(node.ToNodeRef());

            node.ParentGraph.AddStatement(edge);

            return edge;
        }

        /// <summary>
        /// Create sequential edges between the given nodes ending at this node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static DotEdge AddEdgeFrom(this DotNode node, params DotNode[] nodes)
        {
            var edge = new DotEdge(node.ParentGraph);

            edge.SidesList.AddRange(nodes.Select(ToNodeRef));
            edge.SidesList.Add(node.ToNodeRef());

            node.ParentGraph.AddStatement(edge);

            return edge;
        }

        /// <summary>
        /// Create sequential edges between the given nodes ending at this node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static DotEdge AddEdgeFrom(this DotNode node, IEnumerable<DotNode> nodes)
        {
            var edge = new DotEdge(node.ParentGraph);

            edge.SidesList.AddRange(nodes.Select(ToNodeRef));
            edge.SidesList.Add(node.ToNodeRef());

            node.ParentGraph.AddStatement(edge);

            return edge;
        }

        /// <summary>
        /// Create an edge to this node from the given side
        /// </summary>
        /// <param name="node"></param>
        /// <param name="side1"></param>
        /// <returns></returns>
        public static DotEdge AddEdgeFrom(this DotNode node, IDotEdgeSide side1)
        {
            var edge = new DotEdge(node.ParentGraph);

            edge.SidesList.Add(side1);
            edge.SidesList.Add(node.ToNodeRef());

            node.ParentGraph.AddStatement(edge);

            return edge;
        }

        /// <summary>
        /// Create sequential edges between the given sides ending at this node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="sides"></param>
        /// <returns></returns>
        public static DotEdge AddEdgeFrom(this DotNode node, params IDotEdgeSide[] sides)
        {
            var edge = new DotEdge(node.ParentGraph);

            edge.SidesList.AddRange(sides);
            edge.SidesList.Add(node.ToNodeRef());

            node.ParentGraph.AddStatement(edge);

            return edge;
        }

        /// <summary>
        /// Create sequential edges between the given sides ending at this node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="sides"></param>
        /// <returns></returns>
        public static DotEdge AddEdgeFrom(this DotNode node, IEnumerable<IDotEdgeSide> sides)
        {
            var edge = new DotEdge(node.ParentGraph);

            edge.SidesList.AddRange(sides);
            edge.SidesList.Add(node.ToNodeRef());

            node.ParentGraph.AddStatement(edge);

            return edge;
        }

        /// <summary>
        /// Create multiple edges from this node to all given sides
        /// </summary>
        /// <param name="node"></param>
        /// <param name="sides"></param>
        /// <returns></returns>
        public static DotNode AddEdgesTo(this DotNode node, params string[] sides)
        {
            foreach (var item in sides)
                node.AddEdgeTo(item);

            return node;
        }

        /// <summary>
        /// Create multiple edges from this node to all given sides
        /// </summary>
        /// <param name="node"></param>
        /// <param name="sides"></param>
        /// <returns></returns>
        public static DotNode AddEdgesTo(this DotNode node, IEnumerable<string> sides)
        {
            foreach (var item in sides)
                node.AddEdgeTo(item);

            return node;
        }

        /// <summary>
        /// Create multiple edges from this node to all given nodes
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static DotNode AddEdgesTo(this DotNode node, params DotNode[] nodes)
        {
            foreach (var item in nodes)
                node.AddEdgeTo(item);

            return node;
        }

        /// <summary>
        /// Create multiple edges from this node to all given nodes
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static DotNode AddEdgesTo(this DotNode node, IEnumerable<DotNode> nodes)
        {
            foreach (var item in nodes)
                node.AddEdgeTo(item);

            return node;
        }

        /// <summary>
        /// Create multiple edges from this node to all given sides
        /// </summary>
        /// <param name="node"></param>
        /// <param name="sides"></param>
        /// <returns></returns>
        public static DotNode AddEdgesTo(this DotNode node, params IDotEdgeSide[] sides)
        {
            foreach (var item in sides)
                node.AddEdgeTo(item);

            return node;
        }

        /// <summary>
        /// Create multiple edges from this node to all given sides
        /// </summary>
        /// <param name="node"></param>
        /// <param name="sides"></param>
        /// <returns></returns>
        public static DotNode AddEdgesTo(this DotNode node, IEnumerable<IDotEdgeSide> sides)
        {
            foreach (var item in sides)
                node.AddEdgeTo(item);

            return node;
        }

        /// <summary>
        /// Create multiple edges to this node from all given sides
        /// </summary>
        /// <param name="node"></param>
        /// <param name="sides"></param>
        /// <returns></returns>
        public static DotNode AddEdgesFrom(this DotNode node, params string[] sides)
        {
            foreach (var item in sides)
                node.AddEdgeFrom(item);

            return node;
        }

        /// <summary>
        /// Create multiple edges to this node from all given sides
        /// </summary>
        /// <param name="node"></param>
        /// <param name="sides"></param>
        /// <returns></returns>
        public static DotNode AddEdgesFrom(this DotNode node, IEnumerable<string> sides)
        {
            foreach (var item in sides)
                node.AddEdgeFrom(item);

            return node;
        }

        /// <summary>
        /// Create multiple edges to this node from all given nodes
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static DotNode AddEdgesFrom(this DotNode node, params DotNode[] nodes)
        {
            foreach (var item in nodes)
                node.AddEdgeFrom(item);

            return node;
        }

        /// <summary>
        /// Create multiple edges to this node from all given nodes
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static DotNode AddEdgesFrom(this DotNode node, IEnumerable<DotNode> nodes)
        {
            foreach (var item in nodes)
                node.AddEdgeFrom(item);

            return node;
        }

        /// <summary>
        /// Create multiple edges to this node from all given sides
        /// </summary>
        /// <param name="node"></param>
        /// <param name="sides"></param>
        /// <returns></returns>
        public static DotNode AddEdgesFrom(this DotNode node, params IDotEdgeSide[] sides)
        {
            foreach (var item in sides)
                node.AddEdgeFrom(item);

            return node;
        }

        /// <summary>
        /// Create multiple edges to this node from all given sides
        /// </summary>
        /// <param name="node"></param>
        /// <param name="sides"></param>
        /// <returns></returns>
        public static DotNode AddEdgesFrom(this DotNode node, IEnumerable<IDotEdgeSide> sides)
        {
            foreach (var item in sides)
                node.AddEdgeFrom(item);

            return node;
        }
        #endregion

        #region Add Sides to Edge Utilities
        /// <summary>
        /// Add more sides to this edge sequence
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public static DotEdge AddSides(this DotEdge edge, DotNode node)
        {
            var side1 = node.ToNodeRef();

            edge.SidesList.Add(side1);
            edge.SidesList.Add(side1);

            return edge;
        }

        /// <summary>
        /// Add more sides to this edge sequence
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        /// <returns></returns>
        public static DotEdge AddSides(this DotEdge edge, DotNode node1, DotNode node2)
        {
            edge.SidesList.Add(node1.ToNodeRef());
            edge.SidesList.Add(node2.ToNodeRef());

            return edge;
        }

        /// <summary>
        /// Add more sides to this edge sequence
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static DotEdge AddSides(this DotEdge edge, params DotNode[] nodes)
        {
            edge.SidesList.AddRange(nodes.Select(node => node.ToNodeRef()));

            return edge;
        }

        /// <summary>
        /// Add more sides to this edge sequence
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static DotEdge AddSides(this DotEdge edge, string nodeName)
        {
            var side1 = nodeName.ToNodeRef();

            edge.SidesList.Add(side1);
            edge.SidesList.Add(side1);

            return edge;
        }

        /// <summary>
        /// Add more sides to this edge sequence
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="nodeName1"></param>
        /// <param name="nodeName2"></param>
        /// <returns></returns>
        public static DotEdge AddSides(this DotEdge edge, string nodeName1, string nodeName2)
        {
            edge.SidesList.Add(nodeName1.ToNodeRef());
            edge.SidesList.Add(nodeName2.ToNodeRef());

            return edge;
        }

        /// <summary>
        /// Add more sides to this edge sequence
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="nodeNames"></param>
        /// <returns></returns>
        public static DotEdge AddSides(this DotEdge edge, params string[] nodeNames)
        {
            edge.SidesList.AddRange(nodeNames.Select(node => node.ToNodeRef()));

            return edge;
        }

        /// <summary>
        /// Add more sides to this edge sequence
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="nodeNames"></param>
        /// <returns></returns>
        public static DotEdge AddSides(this DotEdge edge, IEnumerable<string> nodeNames)
        {
            edge.SidesList.AddRange(nodeNames.Select(node => node.ToNodeRef()));

            return edge;
        }

        /// <summary>
        /// Add more sides to this edge sequence
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static DotEdge AddSides(this DotEdge edge, float nodeName)
        {
            var side1 = nodeName.ToNodeRef();

            edge.SidesList.Add(side1);
            edge.SidesList.Add(side1);

            return edge;
        }

        /// <summary>
        /// Add more sides to this edge sequence
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="nodeName1"></param>
        /// <param name="nodeName2"></param>
        /// <returns></returns>
        public static DotEdge AddSides(this DotEdge edge, float nodeName1, float nodeName2)
        {
            edge.SidesList.Add(nodeName1.ToNodeRef());
            edge.SidesList.Add(nodeName2.ToNodeRef());

            return edge;
        }

        /// <summary>
        /// Add more sides to this edge sequence
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="nodeNames"></param>
        /// <returns></returns>
        public static DotEdge AddSides(this DotEdge edge, params float[] nodeNames)
        {
            edge.SidesList.AddRange(nodeNames.Select(node => node.ToNodeRef()));

            return edge;
        }

        /// <summary>
        /// Add more sides to this edge sequence
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="nodeNames"></param>
        /// <returns></returns>
        public static DotEdge AddSides(this DotEdge edge, IEnumerable<float> nodeNames)
        {
            edge.SidesList.AddRange(nodeNames.Select(node => node.ToNodeRef()));

            return edge;
        }

        /// <summary>
        /// Add more sides to this edge sequence
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static DotEdge AddSides(this DotEdge edge, double nodeName)
        {
            var side1 = nodeName.ToNodeRef();

            edge.SidesList.Add(side1);
            edge.SidesList.Add(side1);

            return edge;
        }

        /// <summary>
        /// Add more sides to this edge sequence
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="nodeName1"></param>
        /// <param name="nodeName2"></param>
        /// <returns></returns>
        public static DotEdge AddSides(this DotEdge edge, double nodeName1, double nodeName2)
        {
            edge.SidesList.Add(nodeName1.ToNodeRef());
            edge.SidesList.Add(nodeName2.ToNodeRef());

            return edge;
        }

        /// <summary>
        /// Add more sides to this edge sequence
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="nodeNames"></param>
        /// <returns></returns>
        public static DotEdge AddSides(this DotEdge edge, params double[] nodeNames)
        {
            edge.SidesList.AddRange(nodeNames.Select(node => node.ToNodeRef()));

            return edge;
        }

        /// <summary>
        /// Add more sides to this edge sequence
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="nodeNames"></param>
        /// <returns></returns>
        public static DotEdge AddSides(this DotEdge edge, IEnumerable<double> nodeNames)
        {
            edge.SidesList.AddRange(nodeNames.Select(node => node.ToNodeRef()));

            return edge;
        }

        /// <summary>
        /// Add more sides to this edge sequence
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static DotEdge AddSides(this DotEdge edge, object nodeName)
        {
            var side1 = nodeName.ToNodeRef();

            edge.SidesList.Add(side1);
            edge.SidesList.Add(side1);

            return edge;
        }

        /// <summary>
        /// Add more sides to this edge sequence
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="nodeName1"></param>
        /// <param name="nodeName2"></param>
        /// <returns></returns>
        public static DotEdge AddSides(this DotEdge edge, object nodeName1, object nodeName2)
        {
            edge.SidesList.Add(nodeName1.ToNodeRef());
            edge.SidesList.Add(nodeName2.ToNodeRef());

            return edge;
        }

        /// <summary>
        /// Add more sides to this edge sequence
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="nodeNames"></param>
        /// <returns></returns>
        public static DotEdge AddSides(this DotEdge edge, params object[] nodeNames)
        {
            edge.SidesList.AddRange(nodeNames.Select(node => node.ToNodeRef()));

            return edge;
        }

        /// <summary>
        /// Add more sides to this edge sequence
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="nodeNames"></param>
        /// <returns></returns>
        public static DotEdge AddSides(this DotEdge edge, IEnumerable<object> nodeNames)
        {
            edge.SidesList.AddRange(nodeNames.Select(node => node.ToNodeRef()));

            return edge;
        }

        /// <summary>
        /// Add more sides to this edge sequence
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="side"></param>
        /// <returns></returns>
        public static DotEdge AddSides(this DotEdge edge, IDotEdgeSide side)
        {
            edge.SidesList.Add(side);
            edge.SidesList.Add(side);

            return edge;
        }

        /// <summary>
        /// Add more sides to this edge sequence
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="side1"></param>
        /// <param name="side2"></param>
        /// <returns></returns>
        public static DotEdge AddSides(this DotEdge edge, IDotEdgeSide side1, IDotEdgeSide side2)
        {
            edge.SidesList.Add(side1);
            edge.SidesList.Add(side2);

            return edge;
        }

        /// <summary>
        /// Add more sides to this edge sequence
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="sides"></param>
        /// <returns></returns>
        public static DotEdge AddSides(this DotEdge edge, params IDotEdgeSide[] sides)
        {
            edge.SidesList.AddRange(sides);

            return edge;
        }

        /// <summary>
        /// Add more sides to this edge sequence
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="sides"></param>
        /// <returns></returns>
        public static DotEdge AddSides(this DotEdge edge, IEnumerable<IDotEdgeSide> sides)
        {
            edge.SidesList.AddRange(sides);

            return edge;
        }
        #endregion

        #region Create NodeRef Utilities
        /// <summary>
        /// Create a node reference object from this node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static DotNodeRef ToNodeRef(this DotNode node)
        {
            return new DotNodeRef(node.NodeName, "", null);
        }

        /// <summary>
        /// Create a node reference object from this node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="portName"></param>
        /// <returns></returns>
        public static DotNodeRef ToNodeRef(this DotNode node, string portName)
        {
            return new DotNodeRef(node.NodeName, portName, null);
        }

        /// <summary>
        /// Create a node reference object from this node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="compass"></param>
        /// <returns></returns>
        public static DotNodeRef ToNodeRef(this DotNode node, DotCompass compass)
        {
            return new DotNodeRef(node.NodeName, "", compass);
        }

        /// <summary>
        /// Create a node reference object from this node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="portName"></param>
        /// <param name="compass"></param>
        /// <returns></returns>
        public static DotNodeRef ToNodeRef(this DotNode node, string portName, DotCompass compass)
        {
            return new DotNodeRef(node.NodeName, portName, compass);
        }

        /// <summary>
        /// Create a node reference object from this node
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static DotNodeRef ToNodeRef(this string nodeName)
        {
            return new DotNodeRef(nodeName, "", null);
        }

        /// <summary>
        /// Create a node reference object from this node
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="portName"></param>
        /// <returns></returns>
        public static DotNodeRef ToNodeRef(this string nodeName, string portName)
        {
            return new DotNodeRef(nodeName, portName, null);
        }

        /// <summary>
        /// Create a node reference object from this node
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="compass"></param>
        /// <returns></returns>
        public static DotNodeRef ToNodeRef(this string nodeName, DotCompass compass)
        {
            return new DotNodeRef(nodeName, "", compass);
        }

        /// <summary>
        /// Create a node reference object from this node
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="portName"></param>
        /// <param name="compass"></param>
        /// <returns></returns>
        public static DotNodeRef ToNodeRef(this string nodeName, string portName, DotCompass compass)
        {
            return new DotNodeRef(nodeName, portName, compass);
        }

        /// <summary>
        /// Create a node reference object from this node
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static DotNodeRef ToNodeRef(this float nodeName)
        {
            return new DotNodeRef(nodeName.ToDotDouble(), "", null);
        }

        /// <summary>
        /// Create a node reference object from this node
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="portName"></param>
        /// <returns></returns>
        public static DotNodeRef ToNodeRef(this float nodeName, string portName)
        {
            return new DotNodeRef(nodeName.ToDotDouble(), portName, null);
        }

        /// <summary>
        /// Create a node reference object from this node
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="compass"></param>
        /// <returns></returns>
        public static DotNodeRef ToNodeRef(this float nodeName, DotCompass compass)
        {
            return new DotNodeRef(nodeName.ToDotDouble(), "", compass);
        }

        /// <summary>
        /// Create a node reference object from this node
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="portName"></param>
        /// <param name="compass"></param>
        /// <returns></returns>
        public static DotNodeRef ToNodeRef(this float nodeName, string portName, DotCompass compass)
        {
            return new DotNodeRef(nodeName.ToDotDouble(), portName, compass);
        }

        /// <summary>
        /// Create a node reference object from this node
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static DotNodeRef ToNodeRef(this double nodeName)
        {
            return new DotNodeRef(nodeName.ToDotDouble(), "", null);
        }

        /// <summary>
        /// Create a node reference object from this node
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="portName"></param>
        /// <returns></returns>
        public static DotNodeRef ToNodeRef(this double nodeName, string portName)
        {
            return new DotNodeRef(nodeName.ToDotDouble(), portName, null);
        }

        /// <summary>
        /// Create a node reference object from this node
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="compass"></param>
        /// <returns></returns>
        public static DotNodeRef ToNodeRef(this double nodeName, DotCompass compass)
        {
            return new DotNodeRef(nodeName.ToDotDouble(), "", compass);
        }

        /// <summary>
        /// Create a node reference object from this node
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="portName"></param>
        /// <param name="compass"></param>
        /// <returns></returns>
        public static DotNodeRef ToNodeRef(this double nodeName, string portName, DotCompass compass)
        {
            return new DotNodeRef(nodeName.ToDotDouble(), portName, compass);
        }

        /// <summary>
        /// Create a node reference object from this node
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static DotNodeRef ToNodeRef(this object nodeName)
        {
            return new DotNodeRef(nodeName.ToString(), "", null);
        }

        /// <summary>
        /// Create a node reference object from this node
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="portName"></param>
        /// <returns></returns>
        public static DotNodeRef ToNodeRef(this object nodeName, string portName)
        {
            return new DotNodeRef(nodeName.ToString(), portName, null);
        }

        /// <summary>
        /// Create a node reference object from this node
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="compass"></param>
        /// <returns></returns>
        public static DotNodeRef ToNodeRef(this object nodeName, DotCompass compass)
        {
            return new DotNodeRef(nodeName.ToString(), "", compass);
        }

        /// <summary>
        /// Create a node reference object from this node
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="portName"></param>
        /// <param name="compass"></param>
        /// <returns></returns>
        public static DotNodeRef ToNodeRef(this object nodeName, string portName, DotCompass compass)
        {
            return new DotNodeRef(nodeName.ToString(), portName, compass);
        }
        #endregion

        #region Fixed Code Statements
        /// <summary>
        /// Add some fixed code to this graph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static DotFixedCode AddFixedCode(this IDotGraph graph, string code)
        {
            var fixedCode = new DotFixedCode(graph, code);

            AddStatement(graph, fixedCode);

            return fixedCode;
        }

        /// <summary>
        /// Add an empty line to the code of this graph
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static DotFixedCode AddEmptyLine(this IDotGraph graph)
        {
            var fixedCode = new DotFixedCode(graph, Environment.NewLine);

            AddStatement(graph, fixedCode);

            return fixedCode;
        }

        /// <summary>
        /// Add a number of empty lines to the code of this graph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static DotFixedCode AddEmptyLines(this IDotGraph graph, int count)
        {
            var fixedCode = new DotFixedCode(graph, Environment.NewLine.Repeat(count));

            AddStatement(graph, fixedCode);

            return fixedCode;
        }

        /// <summary>
        /// Add one or more lines of text commented using //
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static DotFixedCode AddSingleLineComment(this IDotGraph graph, string code)
        {
            var fixedCode = new DotFixedCode(graph, code);

            AddStatement(graph, fixedCode);

            fixedCode.CodeType = DotFixedCodeType.SingleLineComment;

            return fixedCode;
        }

        /// <summary>
        /// Add one or more lines of text commented using /* */
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static DotFixedCode AddMultiLineComment(this IDotGraph graph, string code)
        {
            var fixedCode = new DotFixedCode(graph, code);

            AddStatement(graph, fixedCode);

            fixedCode.CodeType = DotFixedCodeType.MultiLineComment;

            return fixedCode;
        }
        #endregion

        #region Edge Statements
        /// <summary>
        /// Add a new self-edge to this graph if possible
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public static DotEdge AddEdge(this IDotGraph graph, DotNode node)
        {
            if (graph.MainGraph.GraphType == DotGraphType.StrictDirected)
                throw new InvalidOperationException("Can't add a self-edge to a strict directed graph");

            var edge = new DotEdge(graph);

            var side1 = node.ToNodeRef();

            edge.SidesList.Add(side1);
            edge.SidesList.Add(side1);

            AddStatement(graph, edge);

            return edge;
        }

        /// <summary>
        /// Add a new edge sequence to this graph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static DotEdge AddEdge(this IDotGraph graph, params DotNode[] nodes)
        {
            var edge = new DotEdge(graph);

            edge.SidesList.AddRange(nodes.Select(node => node.ToNodeRef()));

            AddStatement(graph, edge);

            return edge;
        }

        /// <summary>
        /// Add a new edge sequence to this graph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static DotEdge AddEdge(this IDotGraph graph, IEnumerable<DotNode> nodes)
        {
            var edge = new DotEdge(graph);

            edge.SidesList.AddRange(nodes.Select(node => node.ToNodeRef()));

            AddStatement(graph, edge);

            return edge;
        }

        /// <summary>
        /// Add a new self-edge to this graph if possible
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static DotEdge AddEdge(this IDotGraph graph, string nodeName)
        {
            if (graph.MainGraph.GraphType == DotGraphType.StrictDirected)
                throw new InvalidOperationException("Can't add a self-edge to a strict directed graph");

            var edge = new DotEdge(graph);

            var side1 = nodeName.ToNodeRef();

            edge.SidesList.Add(side1);
            edge.SidesList.Add(side1);

            AddStatement(graph, edge);

            return edge;
        }

        /// <summary>
        /// Add a new edge sequence to this graph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="nodeNames"></param>
        /// <returns></returns>
        public static DotEdge AddEdge(this IDotGraph graph, params string[] nodeNames)
        {
            var edge = new DotEdge(graph);

            edge.SidesList.AddRange(nodeNames.Select(node => node.ToNodeRef()));

            AddStatement(graph, edge);

            return edge;
        }

        /// <summary>
        /// Add a new edge sequence to this graph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="nodeNames"></param>
        /// <returns></returns>
        public static DotEdge AddEdge(this IDotGraph graph, IEnumerable<string> nodeNames)
        {
            var edge = new DotEdge(graph);

            edge.SidesList.AddRange(nodeNames.Select(node => node.ToNodeRef()));

            AddStatement(graph, edge);

            return edge;
        }

        /// <summary>
        /// Add a new self-edge to this graph if possible
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static DotEdge AddEdge(this IDotGraph graph, float nodeName)
        {
            if (graph.MainGraph.GraphType == DotGraphType.StrictDirected)
                throw new InvalidOperationException("Can't add a self-edge to a strict directed graph");

            var edge = new DotEdge(graph);

            var side1 = nodeName.ToNodeRef();

            edge.SidesList.Add(side1);
            edge.SidesList.Add(side1);

            AddStatement(graph, edge);

            return edge;
        }

        /// <summary>
        /// Add a new edge sequence to this graph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="nodeNames"></param>
        /// <returns></returns>
        public static DotEdge AddEdge(this IDotGraph graph, params float[] nodeNames)
        {
            var edge = new DotEdge(graph);

            edge.SidesList.AddRange(nodeNames.Select(node => node.ToNodeRef()));

            AddStatement(graph, edge);

            return edge;
        }

        /// <summary>
        /// Add a new edge sequence to this graph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="nodeNames"></param>
        /// <returns></returns>
        public static DotEdge AddEdge(this IDotGraph graph, IEnumerable<float> nodeNames)
        {
            var edge = new DotEdge(graph);

            edge.SidesList.AddRange(nodeNames.Select(node => node.ToNodeRef()));

            AddStatement(graph, edge);

            return edge;
        }

        /// <summary>
        /// Add a new edge sequence to this graph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static DotEdge AddEdge(this IDotGraph graph, double nodeName)
        {
            if (graph.MainGraph.GraphType == DotGraphType.StrictDirected)
                throw new InvalidOperationException("Can't add a self-edge to a strict directed graph");

            var edge = new DotEdge(graph);

            var side1 = nodeName.ToNodeRef();

            edge.SidesList.Add(side1);
            edge.SidesList.Add(side1);

            AddStatement(graph, edge);

            return edge;
        }

        /// <summary>
        /// Add a new edge sequence to this graph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="nodeNames"></param>
        /// <returns></returns>
        public static DotEdge AddEdge(this IDotGraph graph, params double[] nodeNames)
        {
            var edge = new DotEdge(graph);

            edge.SidesList.AddRange(nodeNames.Select(node => node.ToNodeRef()));

            AddStatement(graph, edge);

            return edge;
        }

        /// <summary>
        /// Add a new edge sequence to this graph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="nodeNames"></param>
        /// <returns></returns>
        public static DotEdge AddEdge(this IDotGraph graph, IEnumerable<double> nodeNames)
        {
            var edge = new DotEdge(graph);

            edge.SidesList.AddRange(nodeNames.Select(node => node.ToNodeRef()));

            AddStatement(graph, edge);

            return edge;
        }

        /// <summary>
        /// Add a new self-edge to this graph if possible
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static DotEdge AddEdge(this IDotGraph graph, object nodeName)
        {
            if (graph.MainGraph.GraphType == DotGraphType.StrictDirected)
                throw new InvalidOperationException("Can't add a self-edge to a strict directed graph");

            var edge = new DotEdge(graph);

            var side1 = nodeName.ToNodeRef();

            edge.SidesList.Add(side1);
            edge.SidesList.Add(side1);

            AddStatement(graph, edge);

            return edge;
        }

        /// <summary>
        /// Add a new edge sequence to this graph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="nodeNames"></param>
        /// <returns></returns>
        public static DotEdge AddEdge(this IDotGraph graph, params object[] nodeNames)
        {
            var edge = new DotEdge(graph);

            edge.SidesList.AddRange(nodeNames.Select(node => node.ToNodeRef()));

            AddStatement(graph, edge);

            return edge;
        }

        /// <summary>
        /// Add a new edge sequence to this graph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="nodeNames"></param>
        /// <returns></returns>
        public static DotEdge AddEdge(this IDotGraph graph, IEnumerable<object> nodeNames)
        {
            var edge = new DotEdge(graph);

            edge.SidesList.AddRange(nodeNames.Select(node => node.ToNodeRef()));

            AddStatement(graph, edge);

            return edge;
        }

        /// <summary>
        /// Add a new self-edge to this graph if possible
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="side"></param>
        /// <returns></returns>
        public static DotEdge AddEdge(this IDotGraph graph, IDotEdgeSide side)
        {
            if (graph.MainGraph.GraphType == DotGraphType.StrictDirected)
                throw new InvalidOperationException("Can't add a self-edge to a strict directed graph");

            var edge = new DotEdge(graph);

            edge.SidesList.Add(side);
            edge.SidesList.Add(side);

            AddStatement(graph, edge);

            return edge;
        }

        /// <summary>
        /// Add a new edge sequence to this graph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="sides"></param>
        /// <returns></returns>
        public static DotEdge AddEdge(this IDotGraph graph, params IDotEdgeSide[] sides)
        {
            var edge = new DotEdge(graph);

            edge.SidesList.AddRange(sides);

            AddStatement(graph, edge);

            return edge;
        }

        /// <summary>
        /// Add a new edge sequence to this graph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="sides"></param>
        /// <returns></returns>
        public static DotEdge AddEdge(this IDotGraph graph, IEnumerable<IDotEdgeSide> sides)
        {
            var edge = new DotEdge(graph);

            edge.SidesList.AddRange(sides);

            AddStatement(graph, edge);

            return edge;
        }
        #endregion

        #region Edge Defaults Statements
        /// <summary>
        /// Add a new edge defaults statement to this graph
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static DotEdge AddEdgeDefaults(this IDotGraph graph)
        {
            var edgeDefaults = new DotEdge(graph);

            AddStatement(graph, edgeDefaults);

            return edgeDefaults;
        }
        #endregion

        #region Node Statements
        /// <summary>
        /// Add a node statement to the graph. If a node with the given name is already defined in any 
        /// node statement anywhere in the main graph that statement is returned and no new node is defined
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DotNode AddNode(this IDotGraph graph, float name)
        {
            var mainGraph = graph as DotGraph;

            if (mainGraph != null)
                return mainGraph.FindOrDefineNode(name.ToDotDouble());

            var subGraph = graph as DotSubGraph;

            if (subGraph != null)
                return subGraph.FindOrDefineNode(name.ToDotDouble());

            throw new InvalidOperationException();
        }

        /// <summary>
        /// This adds a node statement to the graph. If a node with the given name is already defined in any 
        /// node statement anywhere in the main graph that statement is returned and no new node is defined
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DotNode AddNode(this IDotGraph graph, double name)
        {
            var mainGraph = graph as DotGraph;

            if (mainGraph != null)
                return mainGraph.FindOrDefineNode(name.ToDotDouble());

            var subGraph = graph as DotSubGraph;

            if (subGraph != null)
                return subGraph.FindOrDefineNode(name.ToDotDouble());

            throw new InvalidOperationException();
        }

        /// <summary>
        /// This adds a node statement to the graph. If a node with the given name is already defined in any 
        /// node statement anywhere in the main graph that statement is returned and no new node is defined
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DotNode AddNode(this IDotGraph graph, string name)
        {
            var mainGraph = graph as DotGraph;

            if (mainGraph != null)
                return mainGraph.FindOrDefineNode(name);

            var subGraph = graph as DotSubGraph;

            if (subGraph != null)
                return subGraph.FindOrDefineNode(name);

            throw new InvalidOperationException();
        }

        /// <summary>
        /// This adds a node statement to the graph. If a node with the given name is already defined in any 
        /// node statement anywhere in the main graph that statement is returned and no new node is defined
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DotNode AddNode(this IDotGraph graph, object name)
        {
            var mainGraph = graph as DotGraph;

            if (mainGraph != null)
                return mainGraph.FindOrDefineNode(name.ToString());

            var subGraph = graph as DotSubGraph;

            if (subGraph != null)
                return subGraph.FindOrDefineNode(name.ToString());

            throw new InvalidOperationException();
        }

        /// <summary>
        /// Add several nodes to this graph if not already defined
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="nodeNames"></param>
        /// <returns></returns>
        public static IDotGraph AddNodes(this IDotGraph graph, params string[] nodeNames)
        {
            foreach (var nodeName in nodeNames)
                AddNode(graph, nodeName);

            return graph;
        }

        /// <summary>
        /// Add several nodes to this graph if not already defined
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="nodeNames"></param>
        /// <returns></returns>
        public static IDotGraph AddNodes(this IDotGraph graph, IEnumerable<string> nodeNames)
        {
            foreach (var nodeName in nodeNames)
                AddNode(graph, nodeName);

            return graph;
        }

        /// <summary>
        /// Add several nodes to this graph if not already defined
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="nodeNames"></param>
        /// <returns></returns>
        public static IDotGraph AddNodes(this IDotGraph graph, params float[] nodeNames)
        {
            foreach (var nodeName in nodeNames)
                AddNode(graph, nodeName);

            return graph;
        }

        /// <summary>
        /// Add several nodes to this graph if not already defined
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="nodeNames"></param>
        /// <returns></returns>
        public static IDotGraph AddNodes(this IDotGraph graph, IEnumerable<float> nodeNames)
        {
            foreach (var nodeName in nodeNames)
                AddNode(graph, nodeName);

            return graph;
        }

        /// <summary>
        /// Add several nodes to this graph if not already defined
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="nodeNames"></param>
        /// <returns></returns>
        public static IDotGraph AddNodes(this IDotGraph graph, params double[] nodeNames)
        {
            foreach (var nodeName in nodeNames)
                AddNode(graph, nodeName);

            return graph;
        }

        /// <summary>
        /// Add several nodes to this graph if not already defined
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="nodeNames"></param>
        /// <returns></returns>
        public static IDotGraph AddNodes(this IDotGraph graph, IEnumerable<double> nodeNames)
        {
            foreach (var nodeName in nodeNames)
                AddNode(graph, nodeName);

            return graph;
        }

        /// <summary>
        /// Add several nodes to this graph if not already defined
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="nodeNames"></param>
        /// <returns></returns>
        public static IDotGraph AddNodes(this IDotGraph graph, params object[] nodeNames)
        {
            foreach (var nodeName in nodeNames)
                AddNode(graph, nodeName);

            return graph;
        }

        /// <summary>
        /// Add several nodes to this graph if not already defined
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="nodeNames"></param>
        /// <returns></returns>
        public static IDotGraph AddNodes(this IDotGraph graph, IEnumerable<object> nodeNames)
        {
            foreach (var nodeName in nodeNames)
                AddNode(graph, nodeName);

            return graph;
        }
        #endregion

        #region Node Defaults Statements
        /// <summary>
        /// Add a new node defaults statement to this graph
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static DotNode AddNodeDefaults(this IDotGraph graph)
        {
            var nodeDefaults = new DotNode(graph, String.Empty);

            AddStatement(graph, nodeDefaults);

            return nodeDefaults;
        }
        #endregion

        #region SubGraph Statements
        /// <summary>
        /// Add a named cluster definition statement to this graph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DotSubGraph AddCluster(this IDotGraph graph, string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new NoNullAllowedException("Cluster name postfix can't be null");

            var subGraph = new DotSubGraph(graph, ("cluster" + name));

            AddStatement(graph, subGraph);

            return subGraph;
        }

        /// <summary>
        /// Add a subgraph definition statement to this graph
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static DotSubGraph AddSubGraph(this IDotGraph graph)
        {
            var subGraph = new DotSubGraph(graph, "");

            AddStatement(graph, subGraph);

            return subGraph;
        }

        /// <summary>
        /// Add a named subgraph definition statement to this graph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DotSubGraph AddSubGraph(this IDotGraph graph, float name)
        {
            var subGraph = new DotSubGraph(graph, name.ToDotDouble());

            AddStatement(graph, subGraph);

            return subGraph;
        }

        /// <summary>
        /// Add a named subgraph definition statement to this graph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DotSubGraph AddSubGraph(this IDotGraph graph, double name)
        {
            var subGraph = new DotSubGraph(graph, name.ToDotDouble());

            AddStatement(graph, subGraph);

            return subGraph;
        }

        /// <summary>
        /// Add a named subgraph definition statement to this graph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DotSubGraph AddSubGraph(this IDotGraph graph, string name)
        {
            var subGraph = new DotSubGraph(graph, name);

            AddStatement(graph, subGraph);

            return subGraph;
        }

        /// <summary>
        /// Add a named subgraph definition statement to this graph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DotSubGraph AddSubGraph(this IDotGraph graph, object name)
        {
            var subGraph = new DotSubGraph(graph, name.ToString());

            AddStatement(graph, subGraph);

            return subGraph;
        }
        #endregion

        #region SubGraph Defaults Statements
        /// <summary>
        /// Add a subgraph defaults statement to this graph
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static DotSubGraphDefaults AddSubGraphDefaults(this IDotGraph graph)
        {
            var graphDefaults = new DotSubGraphDefaults(graph);

            AddStatement(graph, graphDefaults);

            return graphDefaults;
        }
        #endregion

        #region HTML Label Utilities
        /// <summary>
        /// Convert this string into a dot HTML string value
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static DotHtmlString ToHtmlString(this string text)
        {
            return new DotHtmlString(text);
        }

        /// <summary>
        /// Convert this list of strings into a dot HTML string list value
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static DotHtmlTextItemsList ToItemsList(this IEnumerable<DotHtmlTextItem> items)
        {
            return new DotHtmlTextItemsList().AddRange(items);
        }

        /// <summary>
        /// Add an italic formatting tag around this dot HTML string value
        /// </summary>
        /// <param name="htmlText"></param>
        /// <returns></returns>
        public static DotHtmlText Italic(this DotHtmlText htmlText)
        {
            return new DotHtmlFormattedText(DotHtmlTag.Italic(), htmlText);
        }

        /// <summary>
        /// Add a bold formatting tag around this dot HTML string value
        /// </summary>
        /// <param name="htmlText"></param>
        /// <returns></returns>
        public static DotHtmlText Bold(this DotHtmlText htmlText)
        {
            return new DotHtmlFormattedText(DotHtmlTag.Bold(), htmlText);
        }

        /// <summary>
        /// Add an underline formatting tag around this dot HTML string value
        /// </summary>
        /// <param name="htmlText"></param>
        /// <returns></returns>
        public static DotHtmlText Underline(this DotHtmlText htmlText)
        {
            return new DotHtmlFormattedText(DotHtmlTag.Underline(), htmlText);
        }

        /// <summary>
        /// Add an overline formatting tag around this dot HTML string value
        /// </summary>
        /// <param name="htmlText"></param>
        /// <returns></returns>
        public static DotHtmlText Overline(this DotHtmlText htmlText)
        {
            return new DotHtmlFormattedText(DotHtmlTag.Overline(), htmlText);
        }

        /// <summary>
        /// Add a superscript formatting tag around this dot HTML string value
        /// </summary>
        /// <param name="htmlText"></param>
        /// <returns></returns>
        public static DotHtmlText Superscript(this DotHtmlText htmlText)
        {
            return new DotHtmlFormattedText(DotHtmlTag.Superscript(), htmlText);
        }

        /// <summary>
        /// Add a subscript formatting tag around this dot HTML string value
        /// </summary>
        /// <param name="htmlText"></param>
        /// <returns></returns>
        public static DotHtmlText Subscript(this DotHtmlText htmlText)
        {
            return new DotHtmlFormattedText(DotHtmlTag.Subscript(), htmlText);
        }

        /// <summary>
        /// Add a strikethrough formatting tag around this dot HTML string value
        /// </summary>
        /// <param name="htmlText"></param>
        /// <returns></returns>
        public static DotHtmlText StrikeThrough(this DotHtmlText htmlText)
        {
            return new DotHtmlFormattedText(DotHtmlTag.StrikeThrough(), htmlText);
        }

        /// <summary>
        /// Add a given formatting tag around this dot HTML string value
        /// </summary>
        /// <param name="htmlText"></param>
        /// <param name="htmlTag"></param>
        /// <returns></returns>
        public static DotHtmlText FormatUsing(this DotHtmlText htmlText, IDotHtmlTagFormatting htmlTag)
        {
            return new DotHtmlFormattedText(htmlTag, htmlText);
        }

        /// <summary>
        /// Add the given formatting tags around this dot HTML string value.
        /// The given string may contain any combination for the following characters:
        /// I: italic, B: bold, U: underline, O: overline, ^ superscript, _: subscript,
        /// S: strikethrough
        /// </summary>
        /// <param name="htmlText"></param>
        /// <param name="formatFlags"></param>
        /// <returns></returns>
        public static DotHtmlText FormatUsing(this DotHtmlText htmlText, string formatFlags)
        {
            if (String.IsNullOrEmpty(formatFlags))
                return htmlText;

            var result = htmlText;

            foreach (var c in formatFlags.ToUpper())
            {
                switch (c)
                {
                    case 'I':
                        result = new DotHtmlFormattedText(DotHtmlTag.Italic(), result);
                        break;

                    case 'B':
                        result = new DotHtmlFormattedText(DotHtmlTag.Bold(), result);
                        break;

                    case 'U':
                        result = new DotHtmlFormattedText(DotHtmlTag.Underline(), result);
                        break;

                    case 'O':
                        result = new DotHtmlFormattedText(DotHtmlTag.Overline(), result);
                        break;

                    case '^':
                        result = new DotHtmlFormattedText(DotHtmlTag.Superscript(), result);
                        break;

                    case '_':
                        result = new DotHtmlFormattedText(DotHtmlTag.Subscript(), result);
                        break;

                    case 'S':
                        result = new DotHtmlFormattedText(DotHtmlTag.StrikeThrough(), result);
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// Add an italic formatting tag around this string and return a dot HTML string value
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static DotHtmlText Italic(this string text)
        {
            return new DotHtmlFormattedText(DotHtmlTag.Italic(), text.ToHtmlString());
        }

        /// <summary>
        /// Add a bold formatting tag around this string and return a dot HTML string value
        /// </summary>
        /// <param name="htmlText"></param>
        /// <returns></returns>
        public static DotHtmlText Bold(this string htmlText)
        {
            return new DotHtmlFormattedText(DotHtmlTag.Bold(), htmlText.ToHtmlString());
        }

        /// <summary>
        /// Add an underline formatting tag around this string and return a dot HTML string value
        /// </summary>
        /// <param name="htmlText"></param>
        /// <returns></returns>
        public static DotHtmlText Underline(this string htmlText)
        {
            return new DotHtmlFormattedText(DotHtmlTag.Underline(), htmlText.ToHtmlString());
        }

        /// <summary>
        /// Add an overline formatting tag around this string and return a dot HTML string value
        /// </summary>
        /// <param name="htmlText"></param>
        /// <returns></returns>
        public static DotHtmlText Overline(this string htmlText)
        {
            return new DotHtmlFormattedText(DotHtmlTag.Overline(), htmlText.ToHtmlString());
        }

        /// <summary>
        /// Add a superscript formatting tag around this string and return a dot HTML string value
        /// </summary>
        /// <param name="htmlText"></param>
        /// <returns></returns>
        public static DotHtmlText Superscript(this string htmlText)
        {
            return new DotHtmlFormattedText(DotHtmlTag.Superscript(), htmlText.ToHtmlString());
        }

        /// <summary>
        /// Add a subscript formatting tag around this string and return a dot HTML string value
        /// </summary>
        /// <param name="htmlText"></param>
        /// <returns></returns>
        public static DotHtmlText Subscript(this string htmlText)
        {
            return new DotHtmlFormattedText(DotHtmlTag.Subscript(), htmlText.ToHtmlString());
        }

        /// <summary>
        /// Add a strikethrough formatting tag around this string and return a dot HTML string value
        /// </summary>
        /// <param name="htmlText"></param>
        /// <returns></returns>
        public static DotHtmlText StrikeThrough(this string htmlText)
        {
            return new DotHtmlFormattedText(DotHtmlTag.StrikeThrough(), htmlText.ToHtmlString());
        }

        /// <summary>
        /// Add a formatting tag around this string and return a dot HTML string value
        /// </summary>
        /// <param name="htmlText"></param>
        /// <param name="htmlTag"></param>
        /// <returns></returns>
        public static DotHtmlText FormatUsing(this string htmlText, IDotHtmlTagFormatting htmlTag)
        {
            return new DotHtmlFormattedText(htmlTag, htmlText.ToHtmlString());
        }

        /// <summary>
        /// Add formatting tags around this string and return a dot HTML string value.
        /// The given string may contain any combination for the following characters:
        /// I: italic, B: bold, U: underline, O: overline, ^ superscript, _: subscript,
        /// S: strikethrough
        /// </summary>
        /// <param name="htmlText"></param>
        /// <param name="formatFlags"></param>
        /// <returns></returns>
        public static DotHtmlText FormatUsing(this string htmlText, string formatFlags)
        {
            return FormatUsing(htmlText.ToHtmlString(), formatFlags);
        }


        /// <summary>
        /// Create an HTML cell with image content
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static DotHtmlCell ImageCell(string filePath)
        {
            return
                new DotHtmlCell()
                .SetContents(
                    new DotHtmlCellImage().SetImageSource(filePath)
                    );
        }

        /// <summary>
        /// Create an HTML cell with image content
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static DotHtmlCell ImageCell(string filePath, DotNodeImageScale scale)
        {
            return
                new DotHtmlCell()
                .SetContents(
                    new DotHtmlCellImage()
                    .SetImageSource(filePath)
                    .SetImageScale(scale)
                    );
        }

        /// <summary>
        /// Create an HTML cell with no contents
        /// </summary>
        /// <returns></returns>
        public static DotHtmlCell Cell()
        {
            return new DotHtmlCell();
        }

        /// <summary>
        /// Create an HTML table row with no cells
        /// </summary>
        /// <returns></returns>
        public static DotHtmlRow Row()
        {
            return new DotHtmlRow();
        }

        /// <summary>
        /// Create an HTML table with no rows
        /// </summary>
        /// <returns></returns>
        public static DotHtmlTable Table()
        {
            return new DotHtmlTable();
        }
        #endregion
    }
}
