using Irony.Parsing;

namespace CodeComposerLib.Irony.DSLs.IronyEBNF;

public sealed class IronyEbnfGrammar : Grammar
{
    private void InitializeGrammar()
    {
        #region Terminals

        var multiLineComment = new CommentTerminal("multi_line_comment", "/*", "*/");
        NonGrammarTerminals.Add(multiLineComment);

        var singleLineComment = new CommentTerminal("single_line_comment", "//", "\n", "\r\n");
        NonGrammarTerminals.Add(singleLineComment);

        var stringText = new StringLiteral("StringText", "\"", StringOptions.None);

        var identifier = new IdentifierTerminal("Identifier");

        var punctAppend = ToTerm(">>");
        var punctAppendLine = ToTerm("|>");
        var punctAppendNewLine = ToTerm("]>");
        var punctAppendAtNewLine = ToTerm(")>");
        var punctIncreaseIndent = ToTerm("+>");
        var punctDecreaseIndent = ToTerm("->");
        var punctGenerateTemplate = ToTerm("=>");
        var punctAt = ToTerm("@");
        var punctLrb = ToTerm("(");
        var punctRrb = ToTerm(")");
        var punctLcb = ToTerm("{");
        var punctRcb = ToTerm("}");
        var punctEqual = ToTerm("=");
        var punctComma = ToTerm(",");
        var punctColon = ToTerm(":");
        var punctDot = ToTerm(".");
        var punctMatchCase = ToTerm("|");
        var punctQuestion = ToTerm("?");

        var keywordNamespace = ToTerm("namespace");
        var keywordTemplate = ToTerm("template");
        var keywordFor = ToTerm("for");
        var keywordIn = ToTerm("in");
        var keywordDo = ToTerm("do");
        var keywordBegin = ToTerm("begin");
        var keywordEnd = ToTerm("end");
        var keywordMatch = ToTerm("match");
        var keywordWith = ToTerm("with");
        var keywordElse = ToTerm("else");
        var keywordNothing = ToTerm("nothing");
        var keywordLet = ToTerm("let");
        var keywordConstant = ToTerm("constant");

        #endregion

        #region Non-Terminals

        var problemDomain = new NonTerminal("ProblemDomain");
        var dslDefinitions = new NonTerminal("DSL_Definitions");
        var dslDefinitionsItem = new NonTerminal("DSL_Definitions_Item");

        var Namespace = new NonTerminal("Namespace");

        var identifierList = new NonTerminal("Identifier_List");
        var identifierQualList = new NonTerminal("Identifier_Qual_List");

        var rValueOpt = new NonTerminal("RValue_opt");
        var rValue = new NonTerminal("RValue");
        var rValueBracketed = new NonTerminal("RValue_Bracketed");
        var rValueList = new NonTerminal("RValue_List");

        var rValueParametricString = new NonTerminal("RValue_ParametricString");

        var rValueTree = new NonTerminal("RValue_Tree");
        var rValueTreeItemsList = new NonTerminal("RValue_Tree_Items_List");
        var rValueTreeItem = new NonTerminal("RValue_Tree_Item");

        var rValueConditional = new NonTerminal("RValue_Conditional");
        var rValueConditionalCasesList = new NonTerminal("RValue_Conditional_Cases_list");
        var rValueConditionalCase = new NonTerminal("RValue_Conditional_Case");

        var constant = new NonTerminal("Constant");

        var template = new NonTerminal("Template");

        var templateCommand = new NonTerminal("TemplateCommand");
        var templateCommandNothing = new NonTerminal("TemplateCommand_Nothing");
        var templateCommandAppend = new NonTerminal("TemplateCommand_Append");
        var templateCommandAppendLine = new NonTerminal("TemplateCommand_AppendLine");
        var templateCommandAppendNewLine = new NonTerminal("TemplateCommand_AppendNewLine");
        var templateCommandAppendAtNewLine = new NonTerminal("TemplateCommand_AppendAtNewLine");
        var templateCommandIncreaseIndent = new NonTerminal("TemplateCommand_IncreaseIndent");
        var templateCommandDecreaseIndent = new NonTerminal("TemplateCommand_DecreaseIndent");
        var templateCommandGenerateTemplate = new NonTerminal("TemplateCommand_GenerateTemplate");
        var templateCommandGenerateTemplateArgument = new NonTerminal("TemplateCommand_GenerateTemplate_Argument");
        var templateCommandLet = new NonTerminal("TemplateCommand_Let");
        var templateCommandForLoop = new NonTerminal("TemplateCommand_ForLoop");
        var templateCommandMatchWith = new NonTerminal("TemplateCommand_MatchWith");
        var templateCommandMatchWithCaseList = new NonTerminal("TemplateCommand_MatchWith_Case_list");
        var templateCommandMatchWithCase = new NonTerminal("TemplateCommand_MatchWith_Case");
        var templateCommandBlock = new NonTerminal("TemplateCommand_Block");
        var templateCommandBlockCommands = new NonTerminal("TemplateCommand_Block_Commands");

        #endregion

        #region Rules


        Root = problemDomain;


        problemDomain.Rule =
            Namespace + dslDefinitions;

        dslDefinitions.Rule =
            MakeStarRule(dslDefinitions, dslDefinitionsItem);

        dslDefinitionsItem.Rule =
            Namespace | template | constant;


        Namespace.Rule =
            keywordNamespace + identifierQualList;

        constant.Rule =
            keywordConstant + identifier + punctEqual + stringText;


        identifierQualList.Rule =
            MakePlusRule(identifierQualList, punctDot, identifier);

        rValue.Rule =
            identifierQualList | stringText | rValueParametricString | rValueConditional | rValueBracketed | rValueTree;

        rValueBracketed.Rule =
            punctLrb + rValue + punctRrb;

        rValueOpt.Rule =
            rValue | Empty;

        rValueList.Rule =
            MakePlusRule(rValueList, punctComma, rValue);


        rValueParametricString.Rule =
            punctAt + stringText;


        rValueTree.Rule =
            punctLcb + rValueTreeItemsList + punctRcb;

        rValueTreeItemsList.Rule =
            MakePlusRule(rValueTreeItemsList, punctComma, rValueTreeItem);

        rValueTreeItem.Rule =
            identifier + punctColon + rValue;


        rValueConditional.Rule =
            punctQuestion + rValue + rValueConditionalCasesList + punctMatchCase + punctColon + rValue;

        rValueConditionalCasesList.Rule =
            MakePlusRule(rValueConditionalCasesList, rValueConditionalCase);

        rValueConditionalCase.Rule =
            punctMatchCase + rValueList + punctColon + rValue;


        template.Rule =
            keywordTemplate + identifier + punctLrb + identifierList + punctRrb + punctEqual + templateCommand;

        identifierList.Rule =
            MakeStarRule(identifierList, punctComma, identifier);


        templateCommand.Rule =
            templateCommandNothing |
            templateCommandAppend |
            templateCommandAppendLine |
            templateCommandAppendNewLine |
            templateCommandAppendAtNewLine |
            templateCommandIncreaseIndent |
            templateCommandDecreaseIndent |
            templateCommandGenerateTemplate |
            templateCommandLet |
            templateCommandForLoop |
            templateCommandMatchWith |
            templateCommandBlock;

        templateCommandNothing.Rule =
            keywordNothing;

        templateCommandAppend.Rule =
            punctAppend + rValue;

        templateCommandAppendLine.Rule =
            punctAppendLine + rValueOpt;

        templateCommandAppendNewLine.Rule =
            punctAppendNewLine + rValueOpt;

        templateCommandAppendAtNewLine.Rule =
            punctAppendAtNewLine + rValueOpt;

        templateCommandIncreaseIndent.Rule =
            punctIncreaseIndent;

        templateCommandDecreaseIndent.Rule =
            punctDecreaseIndent;

        templateCommandGenerateTemplate.Rule =
            punctGenerateTemplate + identifierQualList + templateCommandGenerateTemplateArgument;

        templateCommandGenerateTemplateArgument.Rule =
            identifierQualList | rValueTree;

        templateCommandLet.Rule =
            keywordLet + identifierQualList + punctEqual + rValue;

        templateCommandForLoop.Rule =
            keywordFor + identifier + keywordIn + identifierQualList + keywordDo + templateCommand;

        templateCommandMatchWith.Rule =
            keywordMatch + rValue + keywordWith + templateCommandMatchWithCaseList + keywordElse + keywordDo + templateCommand;

        templateCommandMatchWithCaseList.Rule =
            MakePlusRule(templateCommandMatchWithCaseList, templateCommandMatchWithCase);

        templateCommandMatchWithCase.Rule =
            punctMatchCase + rValueList + keywordDo + templateCommand;

        templateCommandBlock.Rule =
            keywordBegin + templateCommandBlockCommands + keywordEnd;

        templateCommandBlockCommands.Rule =
            MakeStarRule(templateCommandBlockCommands, templateCommand);

        #endregion

        #region Configuration

        RegisterBracePair("(", ")");

        MarkPunctuation(
            keywordBegin,
            keywordEnd,
            keywordNamespace,
            keywordTemplate,
            keywordFor,
            keywordIn,
            keywordDo,
            keywordMatch,
            keywordWith,
            keywordElse,
            keywordNothing,
            keywordLet,
            keywordConstant,
            punctAppend,
            punctAppendLine,
            punctAppendNewLine,
            punctAppendAtNewLine,
            punctGenerateTemplate,
            punctIncreaseIndent,
            punctDecreaseIndent,
            punctAt,
            punctComma,
            punctColon,
            punctDot,
            punctEqual,
            punctLrb,
            punctRrb,
            punctLcb,
            punctRcb,
            punctMatchCase,
            punctQuestion
        );

        #endregion
    }

    public IronyEbnfGrammar()
    {
        InitializeGrammar();
    }
}