using System.Collections.Generic;
using TextComposerLib.Samples.Samples;
using TextComposerLib.WinForms.UserInterface;

namespace TextComposerLib.Samples
{
    public static class SamplesFactory
    {
        private static ISampleTasksTreeNode InitializeTextTasks()
        {
            var node = new SampleTasksCollection
            {
                NodeLabel = "Text Composers"
            };

            var childNode = node.AddCollection("Simple Composition");

            childNode.AddTask(
                @"Simple Concatenate",
                @"Simple string composition by the Concatenate() extension methods",
                SimpleTextSamples.Task1
                );

            childNode.AddTask(
                @"Simple Join Pairs",
                @"Simple string composition by the JoinPairs() and Concatenate() extension methods",
                SimpleTextSamples.Task2
                );

            childNode = node.AddCollection("StructuredComposersTasks", "Structured Composers");

            childNode.AddTask(
                @"ListComposer Example 1",
                @"String composition by the ListComposer class",
                StructuredComposersSamples.Task1
                );

            childNode.AddTask(
                @"ListComposer Example 2",
                @"String composition by the ListComposer class",
                StructuredComposersSamples.Task2
                );

            childNode.AddTask(
                @"Integrating Structured Composers",
                @"String composition by integrating ListComposer and StackComposer classes",
                StructuredComposersSamples.Task3
                );

            childNode = node.AddCollection("Mapping Composer");

            childNode.AddTask(
                @"MappingComposer Example 1",
                @"String composition by the MappingComposer class from identified text",
                MappingComposerSamples.Task1
                );

            childNode.AddTask(
                @"MappingComposer Example 2",
                @"String composition by the MappingComposer class from delimited text",
                MappingComposerSamples.Task2
                );

            childNode = node.AddCollection("LinearComposerTasks", "Linear Composer");

            childNode.AddTask(
                @"LinearComposer",
                @"String composition by the LinearComposer class",
                LinearComposerSamples.Task1
                );

            childNode = node.AddCollection("Parametric Composer");

            childNode.AddTask(
                @"ParametricComposer",
                @"String composition by the ParametricComposer class",
                ParametricComposerSamples.Task1
                );

            childNode.AddTask(
                @"ParametricComposerCollection",
                @"Handling multiple parametric templates by the ParametricComposerCollection class",
                ParametricComposerSamples.Task2
                );

            childNode = node.AddCollection("Region Composer");

            childNode.AddTask(
                @"RegionComposer Example 1",
                @"Constructing a RegionComposer manually",
                RegionComposerSamples.Task1
                );

            childNode.AddTask(
                @"RegionComposer Example 2",
                @"Parsing a RegionComposer from a template",
                RegionComposerSamples.Task2
                );

            childNode = node.AddCollection("Text Columns Composer");

            childNode.AddTask(
                @"TextColumnsComposer Example 1",
                @"Constructing a TextColumnsComposer",
                TextColumnsComposerSamples.Task1
                );



            return node;
        }

        private static ISampleTasksTreeNode InitializeSettingsTasks()
        {
            var node = new SampleTasksCollection
            {
                NodeLabel = "Settings Composer"
            };

            //var childNode = node.AddCollection("Simple Composition");

            node.AddTask(
                @"Create Settings",
                @"Simple setting composition",
                SettingsComposerSamples.Task1
                );

            node.AddTask(
                @"Using Defaults",
                @"Using default values for some settings",
                SettingsComposerSamples.Task2
                );



            return node;
        }

        private static ISampleTasksTreeNode InitializeCodeTasks()
        {
            var node = new SampleTasksCollection
            {
                NodeLabel = "Code Composers"
            };

            var childNode = node.AddCollection("CSharpCode", "C# Code Generation");

            childNode.AddTask(
                @"Example 1",
                @"Create simple C# syntax tree and generate its code",
                CodeComposersSamples.Task1
                );

            return node;
        }

        private static ISampleTasksTreeNode InitializeFileTasks()
        {
            var node = new SampleTasksCollection
            {
                NodeLabel = "File Composers"
            };

            node.AddTask(
                @"FilesComposer",
                @"Text files structuring and composition by the FilesComposer class",
                FileComposerSamples.Task1
                );

            return node;
        }

        private static ISampleTasksTreeNode InitializeLogTasks()
        {
            var node = new SampleTasksCollection
            {
                NodeLabel = "Log Composers"
            };

            var childNode = node.AddCollection("ProgressComposerTasks", "Progress Composer");

            childNode.AddTask(
                @"ProgressComposer",
                @"Text tracking and logging using the ProgressComposer class",
                ProgressComposerSamples.Task1
                );

            return node;
        }

        private static ISampleTasksTreeNode InitializeTextExpressionTasks()
        {
            var node = new SampleTasksCollection
            {
                NodeLabel = "Text Expressions"
            };

            node.AddTask(
                @"TextExpression Example 1",
                "Construct and display an expression tree",
                TextExpressionSamples.Task1
                );

            node.AddTask(
                @"TextExpression Example 2",
                "Create an expression from text, manipulate it, and display the results",
                TextExpressionSamples.Task2
                );

            return node;
        }

        //private static ISampleTasksTreeNode InitializeDiagramTasks()
        //{
        //    var node = new SampleTasksCollection
        //    {
        //        NodeLabel = "Diagram Composers"
        //    };

        //    var childNode = node.AddCollection("GraphViz", "GraphViz Composer");

        //    childNode.AddTask(
        //        @"GraphViz Example",
        //        @"Example for generating dot code for GraphViz and rendering the graph",
        //        GraphVizSamples.Task1
        //        );

        //    //childNode = node.AddCollection("SVG", "SVG Composer");

        //    //childNode.AddTask(
        //    //    @"SVG Sample 1",
        //    //    @"Example for generating svg",
        //    //    SvgSamples.Task1
        //    //);

        //    //childNode.AddTask(
        //    //    @"SVG Sample 2",
        //    //    @"Example for generating a rectangular grid using svg",
        //    //    SvgSamples.Task2
        //    //);

        //    return node;
        //}



        public static List<ISampleTasksTreeNode> CreateSamples()
        {
            var samplesList = new List<ISampleTasksTreeNode>
            {
                InitializeTextTasks(),
                InitializeSettingsTasks(),
                InitializeTextExpressionTasks(),
                InitializeCodeTasks(),
                InitializeFileTasks(),
                InitializeLogTasks(),
                //InitializeDiagramTasks()
            };

            return samplesList;
        }
    }
}
