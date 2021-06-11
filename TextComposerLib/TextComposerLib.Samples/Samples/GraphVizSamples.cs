using System;
using CodeComposerLib.GraphViz.Dot;
using CodeComposerLib.GraphViz.Dot.Value;

namespace TextComposerLib.Samples.Samples
{
    internal static class GraphVizSamples
    {
        internal static string Task1()
        {
            var graph = DotGraph.Directed();

            graph.AddSingleLineComment("Nodes");

            graph.AddNode("A").SetShape(DotNodeShape.Septagon);
            graph.AddNode("B").SetShape(DotNodeShape.Box3D);
            graph.AddNode("C").SetShape(DotNodeShape.Circle);

            graph.AddEmptyLine();

            graph.AddSingleLineComment("Edges");

            graph.AddEdge("A", "B");
            graph.AddEdge("B", "C");
            graph.AddEdge("C", "A");

            graph.AddEmptyLine();

            graph.AddNodeDefaults().SetShape(DotNodeShape.Component);
            graph.AddCluster("1").AddEdge("1", "2", "3", "4");

            graph.AddEmptyLine();

            graph.AddSingleLineComment("Change shape of node C" + Environment.NewLine + "note that no code is emmitted here");

            graph.AddNode("C").SetShape(DotNodeShape.Diamond);

            //var form = new FormGraphVizEditor(graph);

            //form.ShowDialog();

            return graph.GenerateDotCode();
        }
    }
}
