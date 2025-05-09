using Irony.Parsing;

namespace GeometricAlgebraFulcrumLib.Core.Utilities.Text.TextExpressions;

internal static class TextExpressionDslParseNodeNames
{
    public const string DqString = "DQString";

    public const string SqString = "SQString";

    public const string Identifier = "Identifier";

    public const string LiteralNumber = "LiteralNumber";

    public const string Expression = "Expression";

    public const string QualifiedIdentifier = "QualifiedIdentifier";

    public const string ExpressionName = "ExpressionName";

    public const string List = "List";

    public const string ListArgs = "ListArgs";

    public const string Dictionary = "Dictionary";

    public const string DictionaryArgs = "DictionaryArgs";

    public const string DictionaryArg = "DictionaryArg";
}

internal sealed class TextExpressionDslGrammar : Grammar
{
    public TextExpressionDslGrammar()
    {
        #region Terminals

        //Double quoted string
        var dqString = new StringLiteral(TextExpressionDslParseNodeNames.DqString, "\"", StringOptions.AllowsAllEscapes);
        dqString.AddPrefix("@", StringOptions.NoEscapes | StringOptions.AllowsLineBreak | StringOptions.AllowsDoubledQuote);

        //Single quoted string
        var sqString = new StringLiteral(TextExpressionDslParseNodeNames.SqString, "'", StringOptions.AllowsAllEscapes);
        sqString.AddPrefix("@", StringOptions.NoEscapes | StringOptions.AllowsLineBreak | StringOptions.AllowsDoubledQuote);

        var identifier = new IdentifierTerminal(TextExpressionDslParseNodeNames.Identifier);

        var literalNumber = new NumberLiteral(TextExpressionDslParseNodeNames.LiteralNumber, NumberOptions.AllowSign);

        var punctColon = ToTerm(":");
        var punctDot = ToTerm(".");
        var punctComma = ToTerm(",");
        var punctLsb = ToTerm("[");
        var punctRsb = ToTerm("]");
        var punctLcb = ToTerm("{");
        var punctRcb = ToTerm("}");

        #endregion

        #region Non-Terminals

        var ntExpression = new NonTerminal(TextExpressionDslParseNodeNames.Expression);

        var ntQualifiedIdentifier = new NonTerminal(TextExpressionDslParseNodeNames.QualifiedIdentifier);

        var ntExpressionName = new NonTerminal(TextExpressionDslParseNodeNames.ExpressionName);

        var ntList = new NonTerminal(TextExpressionDslParseNodeNames.List);

        var ntListArgs = new NonTerminal(TextExpressionDslParseNodeNames.ListArgs);

        var ntDictionary = new NonTerminal(TextExpressionDslParseNodeNames.Dictionary);

        var ntDictionaryArgs = new NonTerminal(TextExpressionDslParseNodeNames.DictionaryArgs);

        var ntDictionaryArg = new NonTerminal(TextExpressionDslParseNodeNames.DictionaryArg);

        #endregion

        #region Rules

        //Set root of grammar
        Root = ntExpression;

        ntExpression.Rule =
            ntQualifiedIdentifier | dqString | sqString | literalNumber | ntList | ntDictionary;

        ntQualifiedIdentifier.Rule =
            MakePlusRule(ntQualifiedIdentifier, punctDot, identifier);

        ntList.Rule =
            ntExpressionName + punctLsb + ntListArgs + punctRsb;

        ntExpressionName.Rule =
            Empty | ntQualifiedIdentifier;

        ntListArgs.Rule =
            MakeStarRule(ntListArgs, punctComma, ntExpression);

        ntDictionary.Rule =
            ntExpressionName + punctLcb + ntDictionaryArgs + punctRcb;

        ntDictionaryArgs.Rule =
            MakeStarRule(ntDictionaryArgs, punctComma, ntDictionaryArg);

        ntDictionaryArg.Rule =
            ntQualifiedIdentifier + punctColon + ntExpression;

        #endregion

        #region Configuration

        RegisterBracePair("[", "]");
        RegisterBracePair("{", "}");

        MarkPunctuation(
            punctColon,
            punctDot,
            punctComma,
            punctLsb,
            punctRsb,
            punctLcb,
            punctRcb
        );

        #endregion
    }
}