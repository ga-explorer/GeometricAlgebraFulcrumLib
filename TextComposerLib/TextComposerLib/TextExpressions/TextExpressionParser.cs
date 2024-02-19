using System;
using System.Linq;
using Irony;
using Irony.Parsing;
using TextComposerLib.Text;
using TextComposerLib.TextExpressions.Ast;

namespace TextComposerLib.TextExpressions;

public static class TextExpressionParser
{
    private static readonly TextExpressionDslGrammar Grammar = new TextExpressionDslGrammar();
    private static readonly LanguageData GrammarLangData = new LanguageData(Grammar);
    private static readonly Parser GrammarParser = new Parser(GrammarLangData);

    private static void Assert(this ParseTreeNode exprNode, string nodeName)
    {
        if (exprNode.Term.Name != nodeName)
            throw new InvalidOperationException("Expecting parse tree node <" + nodeName + ">; got <" + exprNode.Term.Name + "> instead!");
    }


    private static TeLiteralString TranslateDqString(ParseTreeNode exprNode)
    {
        exprNode.Assert(TextExpressionDslParseNodeNames.DqString);

        return new TeLiteralString(
            exprNode.FindTokenAndGetText().QuoteLiteralToValue()
        );
    }

    private static TeLiteralString TranslateSqString(ParseTreeNode exprNode)
    {
        exprNode.Assert(TextExpressionDslParseNodeNames.SqString);

        return new TeLiteralString(
            exprNode.FindTokenAndGetText().QuoteLiteralToValue()
        );
    }

    private static TeLiteralNumber TranslateLiteralNumber(ParseTreeNode exprNode)
    {
        exprNode.Assert(TextExpressionDslParseNodeNames.LiteralNumber);

        var numberText = exprNode.FindTokenAndGetText();

        return new TeLiteralNumber(numberText);
    }

    public static string Translate_Identifier(ParseTreeNode node)
    {
        node.Assert(TextExpressionDslParseNodeNames.Identifier);

        return node.FindTokenAndGetText();
    }

    private static TeIdentifier TranslateQualifiedIdentifier(ParseTreeNode exprNode)
    {
        exprNode.Assert(TextExpressionDslParseNodeNames.QualifiedIdentifier);

        return
            new TeIdentifier(
                exprNode.ChildNodes.Select(Translate_Identifier).Concatenate(".")
            );
    }

    private static TeIdentifier TranslateExpressionName(ParseTreeNode exprNode)
    {
        exprNode.Assert(TextExpressionDslParseNodeNames.ExpressionName);

        return 
            exprNode.ChildNodes.Count == 0 
                ? null 
                : TranslateQualifiedIdentifier(exprNode.ChildNodes[0]);
    }

    private static TeList TranslateList(ParseTreeNode exprNode)
    {
        exprNode.Assert(TextExpressionDslParseNodeNames.List);

        var name = TranslateExpressionName(exprNode.ChildNodes[0]);

        var listArgsNode = exprNode.ChildNodes[1];

        var expr = new TeList()
        {
            Name = name
        };

        expr.AddRange(
            listArgsNode.ChildNodes.Select(TranslateExpression)
        );

        return expr;
    }

    private static TeDictionary TranslateDictionary(ParseTreeNode exprNode)
    {
        exprNode.Assert(TextExpressionDslParseNodeNames.Dictionary);

        var name = TranslateExpressionName(exprNode.ChildNodes[0]);

        var dictArgsNode = exprNode.ChildNodes[1];

        var expr = new TeDictionary()
        {
            Name = name
        };

        foreach (var dictArgNode in dictArgsNode.ChildNodes)
        {
            var dictArgName = TranslateQualifiedIdentifier(dictArgNode.ChildNodes[0]);
            var dictArgExpr = TranslateExpression(dictArgNode.ChildNodes[1]);

            expr.Add(dictArgName.Text, dictArgExpr);
        }

        return expr;
    }

    private static ITextExpression TranslateExpression(ParseTreeNode exprNode)
    {
        exprNode.Assert(TextExpressionDslParseNodeNames.Expression);

        var childNode = exprNode.ChildNodes[0];

        switch (childNode.Term.Name)
        {
            case TextExpressionDslParseNodeNames.DqString:
                return TranslateDqString(childNode);

            case TextExpressionDslParseNodeNames.SqString:
                return TranslateSqString(childNode);

            case TextExpressionDslParseNodeNames.LiteralNumber:
                return TranslateLiteralNumber(childNode);

            case TextExpressionDslParseNodeNames.QualifiedIdentifier:
                return TranslateQualifiedIdentifier(childNode);

            case TextExpressionDslParseNodeNames.List:
                return TranslateList(childNode);

            case TextExpressionDslParseNodeNames.Dictionary:
                return TranslateDictionary(childNode);

            default:
                throw new InvalidOperationException("Invalid expression type: " + childNode + " at " + childNode.Span.Location);
        }
    }

    public static ITextExpression ParseToTextExpression(this string textExpr)
    {
        var parseTree = GrammarParser.Parse(textExpr);

        var errorMessage =
            parseTree
                .ParserMessages
                .FirstOrDefault(message => message.Level == ErrorLevel.Error);

        if (ReferenceEquals(errorMessage, null) == false)
            throw new InvalidOperationException(errorMessage.ToString() + errorMessage.Location.Position);

        return TranslateExpression(parseTree.Root);
    }

    public static string TryParseToTextExpression(this string textExpr, out ITextExpression expr)
    {
        try
        {
            expr = ParseToTextExpression(textExpr);
            return string.Empty;
        }
        catch (Exception e)
        {
            expr = null;
            return e.Message;
        }
    }
}