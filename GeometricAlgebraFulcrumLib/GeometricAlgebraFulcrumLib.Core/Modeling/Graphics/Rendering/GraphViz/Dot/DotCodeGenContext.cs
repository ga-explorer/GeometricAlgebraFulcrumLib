using GeometricAlgebraFulcrumLib.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.GraphViz.Dot;

/// <summary>
/// This class converts a dot AST into GraphViz text code
/// </summary>
internal sealed class DotCodeGenContext
{
    private static string ToDotId(string text)
    {
        if (String.IsNullOrEmpty(text))
            return "";

        var firstChar = text[0];

        if (text.Length == 1)
        {
            if (Char.IsLetterOrDigit(firstChar) || firstChar == '_' || (firstChar >= 200 && firstChar <= 377))
                return text;

            return text.ValueToQuotedLiteral();
        }

        //Test if it's a quoted string
        if (firstChar == '\"' && text[text.Length - 1] == '\"')
            return text;

        //Test if it's an integer number
        if ((firstChar == '-' || Char.IsDigit(firstChar)) && text.Skip(1).All(Char.IsDigit))
            return text;

        //Test if it's a name
        if (Char.IsLetter(firstChar) && text.Skip(1).All(Char.IsLetterOrDigit))
            return text;

        //Test if HTML with matching <>
        if (text[0] == '<')
        {
            var k = 0;
            foreach (var c in text)
            {
                if (c == '<')
                    k++;
                else if (c == '>')
                    k--;

                if (k < 0)
                    break;
            }

            if (k == 0)
                return text;
        }

        return text.ValueToQuotedLiteral();
    }


    internal LinearTextComposer TextComposer { get; }

    internal DotGraphType GraphType { get; private set; }


    internal DotCodeGenContext(DotGraphType graphType)
    {
        TextComposer = new LinearTextComposer();
        GraphType = graphType;
    }


    internal DotCodeGenContext Reset(DotGraphType graphType)
    {
        TextComposer.Clear();

        GraphType = graphType;

        return this;
    }


    internal void GenerateDotCode(KeyValuePair<string, string> attr)
    {
        TextComposer.Append(attr.Key);

        if (attr.Value != "true")
            TextComposer
                .Append(" = ")
                .Append(attr.Value);
    }

    internal void GenerateDotCode(Dictionary<string, string> attrList)
    {
        if (attrList.Count == 0)
            return;

        TextComposer.Append(" [ ");

        var flag = false;
        foreach (var attr in attrList)
        {
            if (flag)
                TextComposer.Append(", ");
            else
                flag = true;

            GenerateDotCode(attr);
        }

        TextComposer.Append(" ]");
    }

    internal void GenerateDotCode(DotFixedCode fixedCode)
    {
        if (fixedCode.CodeType == DotFixedCodeType.MultiLineComment)
        {
            TextComposer.AppendAtNewLine(@"/*");
            TextComposer.AppendAtNewLine(fixedCode.Code);
            TextComposer.AppendAtNewLine(@"*/");

            return;
        }

        if (fixedCode.CodeType == DotFixedCodeType.SingleLineComment)
        {
            TextComposer.IncreaseIndentation("// ");
            TextComposer.AppendAtNewLine(fixedCode.Code);
            TextComposer.DecreaseIndentation();

            return;
        }

        TextComposer.AppendAtNewLine(fixedCode.Code);
    }

    internal void GenerateDotCode(DotEdge edge)
    {
        if (edge.IsEdgeDefaults && edge.HasAttributes == false)
            return;

        if (edge.IsEdge)
            for (var i = 0; i < edge.SidesList.Count; i++)
            {
                if (i > 0)
                    TextComposer.Append(GraphType == DotGraphType.Undirected ? " -- " : " -> ");

                GenerateDotCode(edge.SidesList[i]);
            }
        else
            TextComposer.Append("edge");

        GenerateDotCode(edge.AttrValues);

        TextComposer.Append(";");
    }

    internal void GenerateDotCode(DotGraphAttribute graphAttr)
    {
        TextComposer.Append(graphAttr.AttrName);

        if (graphAttr.AttrValue != "true")
            TextComposer
                .Append(" = ")
                .Append(graphAttr.AttrValue)
                .Append(";");
    }

    internal void GenerateDotCode(DotNode node)
    {
        if (node.IsNodeDefaults && node.HasAttributes == false)
            return;

        TextComposer.Append(node.IsNode ? ToDotId(node.NodeName) : "node");

        GenerateDotCode(node.AttrValues);

        TextComposer.Append(";");
    }

    internal void GenerateDotCode(DotNodeRef nodeRef)
    {
        TextComposer.Append(ToDotId(nodeRef.NodeName));

        if (nodeRef.HasPort)
            TextComposer
                .Append(":")
                .Append(ToDotId(nodeRef.PortName));

        if (nodeRef.HasCompass)
            TextComposer
                .Append(":")
                .Append(nodeRef.Compass.Value);
    }

    internal void GenerateDotCode(IDotEdgeSide edgeSide)
    {
        var nodeRef = edgeSide as DotNodeRef;

        if (nodeRef != null)
        {
            GenerateDotCode(nodeRef);

            return;
        }

        var subGraph = edgeSide as DotSubGraph;

        if (subGraph != null)
            GenerateDotCode(subGraph);
    }

    internal void GenerateDotCode(DotSubGraph graph)
    {
        if (String.IsNullOrEmpty(graph.SubGraphName) == false)
            TextComposer
                .Append("subgraph ")
                .Append(ToDotId(graph.SubGraphName));

        TextComposer
            .AppendLine()
            .Append("{")
            .IncreaseIndentation();

        foreach (var statement in graph.StatementsList)
            GenerateDotCode(statement);

        TextComposer
            .DecreaseIndentation()
            .AppendLine()
            .Append("}");
    }

    internal void GenerateDotCode(DotSubGraphDefaults graphDefaults)
    {
        if (graphDefaults.HasAttributes == false)
            return;

        TextComposer.Append("graph ");

        GenerateDotCode(graphDefaults.AttrValues);

        TextComposer.Append(";");
    }

    internal void GenerateDotCode(IDotStatement statement)
    {
        TextComposer.AppendAtNewLine();

        var fixedCode = statement as DotFixedCode;

        if (fixedCode != null)
        {
            GenerateDotCode(fixedCode);

            return;
        }

        var edge = statement as DotEdge;

        if (edge != null)
        {
            GenerateDotCode(edge);

            return;
        }

        var graphAttr = statement as DotGraphAttribute;

        if (graphAttr != null)
        {
            GenerateDotCode(graphAttr);

            return;
        }

        var node = statement as DotNode;

        if (node != null)
        {
            GenerateDotCode(node);

            return;
        }

        var subGraph = statement as DotSubGraph;

        if (subGraph != null)
        {
            GenerateDotCode(subGraph);

            return;
        }

        var subGraphDefault = statement as DotSubGraphDefaults;

        if (subGraphDefault != null)
        {
            GenerateDotCode(subGraphDefault);

            return;
        }

        throw new InvalidOperationException();
    }

    internal void GenerateDotCode(DotGraph graph)
    {
        switch (graph.GraphType)
        {
            case DotGraphType.StrictDirected:
                TextComposer.Append("strict digraph ");
                break;

            case DotGraphType.Directed:
                TextComposer.Append("digraph ");
                break;

            default:
                TextComposer.Append("graph ");
                break;
        }

        //if (String.IsNullOrEmpty(graph.GraphName) == false)
        //    TextComposer.Append(ToDotId(graph.GraphName));

        TextComposer.AppendLine().Append("{").IncreaseIndentation();

        foreach (var statement in graph.StatementsList)
            GenerateDotCode(statement);

        TextComposer
            .DecreaseIndentation()
            .AppendLine()
            .Append("}");
    }


    public override string ToString()
    {
        return TextComposer.ToString();
    }
}