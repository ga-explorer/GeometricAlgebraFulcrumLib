using System.Collections.Generic;
using System.Linq;
using Irony;
using Irony.Parsing;
using TextComposerLib.Text;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Markdown;
using TextComposerLib.Text.Markdown.Tables;

namespace CodeComposerLib.Irony;

public sealed class IronyParsingResults
{
    public string SourceText { get; }

    public Grammar IronyGrammar { get; }

    public LanguageData IronyLanguageData { get; }

    public Parser IronyParser { get; }

    public ParseTree IronyParseTree { get; }

    public bool ContainsParseTreeRoot 
        => IronyParseTree.Root != null;

    public ParseTreeNode ParseTreeRoot 
        => IronyParseTree.Root;

    public bool ContainsMessages
        => IronyParseTree.ParserMessages.Count > 0;

    public bool ContainsErrorMessages
        => IronyParseTree.ParserMessages.Any(m => m.Level == ErrorLevel.Error);

    public bool ContainsWarningMessages
        => IronyParseTree.ParserMessages.Any(m => m.Level == ErrorLevel.Warning);

    public bool ContainsInfoMessages
        => IronyParseTree.ParserMessages.Any(m => m.Level == ErrorLevel.Info);

    public IEnumerable<LogMessage> Messages
        => IronyParseTree.ParserMessages;

    public IEnumerable<LogMessage> ErrorMessages
        => IronyParseTree.ParserMessages.Where(m => m.Level == ErrorLevel.Error);

    public IEnumerable<LogMessage> WarningMessages
        => IronyParseTree.ParserMessages.Where(m => m.Level == ErrorLevel.Warning);

    public IEnumerable<LogMessage> InfoMessages
        => IronyParseTree.ParserMessages.Where(m => m.Level == ErrorLevel.Info);

    public string MessagesText 
    {
        get
        {
            var composer = new LinearTextComposer();

            foreach (var msg in Messages)
            {
                composer
                    .AppendLine($"Line: {msg.Location.Line}, Column: {msg.Location.Column}, Position: {msg.Location.Position}")
                    .AppendLine($"{msg.Level.ToString()}: {msg.Message}")
                    .AppendLine();
            }

            return composer.ToString();
        }
    }

    public string ErrorMessagesText 
    {
        get
        {
            var composer = new LinearTextComposer();

            foreach (var msg in ErrorMessages)
            {
                composer
                    .AppendLine($"Line: {msg.Location.Line}, Column: {msg.Location.Column}, Position: {msg.Location.Position}")
                    .AppendLine($"{msg.Level.ToString()}: {msg.Message}")
                    .AppendLine();
            }

            return composer.ToString();
        }
    }

    public string WarningMessagesText 
    {
        get
        {
            var composer = new LinearTextComposer();

            foreach (var msg in WarningMessages)
            {
                composer
                    .AppendLine($"Line: {msg.Location.Line}, Column: {msg.Location.Column}, Position: {msg.Location.Position}")
                    .AppendLine($"{msg.Level.ToString()}: {msg.Message}")
                    .AppendLine();
            }

            return composer.ToString();
        }
    }

    public string InfoMessagesText 
    {
        get
        {
            var composer = new LinearTextComposer();

            foreach (var msg in InfoMessages)
            {
                composer
                    .AppendLine($"Line: {msg.Location.Line}, Column: {msg.Location.Column}, Position: {msg.Location.Position}")
                    .AppendLine($"{msg.Level.ToString()}: {msg.Message}")
                    .AppendLine();
            }

            return composer.ToString();
        }
    }


    public IronyParsingResults(Grammar grammar, string sourceText)
    {
        SourceText = sourceText;
        IronyGrammar = grammar;
        IronyLanguageData = new LanguageData(grammar);
        IronyParser = new Parser(IronyLanguageData);
        IronyParseTree = IronyParser.Parse(sourceText);
    }


    public override string ToString()
    {
        var composer = new MarkdownComposer();

        composer
            .AppendHeader("Source Text:", 2)
            .AppendLine(SourceText)
            .AppendLine();
            
        composer
            .AppendHeader($"Contains messages: {ContainsMessages}", 2);

        if (ContainsMessages)
            composer
                .IncreaseIndentation()
                .AppendLine(MessagesText)
                .DecreaseIndentation()
                .AppendLine();

        composer
            .AppendHeader($"Contains tree root: {ContainsParseTreeRoot}", 2)
            .AppendLine();

        if (ContainsParseTreeRoot)
        {
            var nodesStack = new Stack<ParseTreeNode>();
            nodesStack.Push(ParseTreeRoot);

            var levelsStack = new Stack<int>();
            levelsStack.Push(0);

            var indexStack = new Stack<List<int>>();
            indexStack.Push(new List<int>());

            var mdTableComposer = new MarkdownTable();
            mdTableComposer.AddColumn("Path Index", MarkdownTableColumnAlignment.Left, "Path Index");
            mdTableComposer.AddColumn("Node", MarkdownTableColumnAlignment.Left, "Node");
            mdTableComposer.AddColumn("Token", MarkdownTableColumnAlignment.Left, "Token");

            while (nodesStack.Count > 0)
            {
                var node = nodesStack.Pop();
                var level = levelsStack.Pop();
                var indexList = indexStack.Pop();
                    
                var indexText = 
                    indexList.Concatenate(",", "[", "]");

                var sourceTextStart = node.Span.Location.Position;
                var sourceTextLength = node.Span.Length;
                var sourceText = SourceText.Substring(sourceTextStart, sourceTextLength);

                mdTableComposer[0].Add(indexText);
                mdTableComposer[1].Add("".PadLeft(level * 2) + node);
                mdTableComposer[2].Add(node.FindTokenAndGetText());
                //mdTableComposer[2].Add(sourceText);

                if (node.ChildNodes.Count == 0)
                    continue;

                var childLevel = level + 1;
                var childNodes =
                    ((IEnumerable<ParseTreeNode>) node.ChildNodes).Reverse();

                var childIndex = node.ChildNodes.Count - 1;
                foreach (var childNode in childNodes)
                {
                    nodesStack.Push(childNode);
                    levelsStack.Push(childLevel);

                    var childIndexList = new List<int>(indexList) {childIndex};
                    indexStack.Push(childIndexList);

                    childIndex--;
                }
            }

            composer.AppendAtNewLine(mdTableComposer.ToString());
        }

        return composer.ToString();
    }
}