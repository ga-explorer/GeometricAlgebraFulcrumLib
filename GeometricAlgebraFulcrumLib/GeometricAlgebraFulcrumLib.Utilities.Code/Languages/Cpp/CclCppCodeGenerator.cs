using GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree;
using GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree.Expressions;
using GeometricAlgebraFulcrumLib.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Languages.Cpp;

public class CclCppCodeGenerator :
    CclLanguageCodeGeneratorBase,
    ICclImperativeCodeGenerator,
    ICclObjectOrientedCodeGenerator
{
    internal CclCppCodeGenerator()
    {
        LanguageInfo = CclCppUtils.Cpp11Info;

        ScalarTypeName = "double";
        ScalarZero = "0.0";
    }


    public override void Visit(SteComment code)
    {
        if (code.MultiLineComment)
        {
            TextComposer.AppendLine("/*");

            foreach (var commentLine in code.CommentedTextLines)
                TextComposer.AppendLine(commentLine);

            TextComposer.AppendLine("*/");

            return;
        }

        foreach (var commentLine in code.CommentedTextLines)
            TextComposer.Append("//").AppendLine(commentLine);
    }

    public override void Visit(SteDeclareDataStore code)
    {
        var modifiersText = code.ModifiersList.Concatenate(" ");

        TextComposer
            .Append(modifiersText)
            .Append(code.DataStoreType)
            .Append(" ")
            .Append(code.DataStoreName);

        if (code.InitialValue != null)
        {
            TextComposer.Append(" = ");
            code.InitialValue.AcceptVisitor(this);
        }

        TextComposer.AppendLine(";");
    }

    public override void Visit(SteDeclareFixedSizeArray code)
    {
        if (code.LocalDataStore == false)
        {
            AddInternalComment("TODO: Non-local array declaration not yet implemented: ", code.ToString());

            return;
        }

        TextComposer
            .Append(code.DataStoreType)
            .Append("[] ")
            .Append(code.DataStoreName)
            .Append(" = ");

        if (code.InitialValue != null)
        {
            code.InitialValue.AcceptVisitor(this);
        }
        else
        {
            TextComposer
                .Append("new ")
                .Append(code.DataStoreType)
                .Append("[")
                .Append(code.ArraySize)
                .Append("]");
        }

        TextComposer.AppendLine(";");
    }

    public override void Visit(SteAssign code)
    {
        if (code.LocalAssignment == false)
        {
            AddInternalComment("TODO: Non-local assignment not yet implemented: ", code.ToString());

            return;
        }

        code.LeftHandSide.AcceptVisitor(this);

        TextComposer.Append(" = ");

        code.RightHandSide.AcceptVisitor(this);

        TextComposer.AppendLine(";");
    }

    public override void Visit(SteReturn code)
    {
        if (ReferenceEquals(code.ReturnedValue, null))
        {
            TextComposer.AppendLine("return;");
            return;
        }

        TextComposer.Append("return ");

        code.ReturnedValue.AcceptVisitor(this);

        TextComposer.AppendLine(";");
    }

    public void Visit(SteExpression code)
    {
        if (code.IsAtomic)
        {
            TextComposer.Append(code.HeadText);
            return;
        }

        if (code.IsFunction)
        {
            TextComposer.Append(code.HeadText).Append("(");

            var flag = false;
            foreach (var argCode in code.Arguments)
            {
                if (flag) TextComposer.Append(", ");
                else flag = true;

                argCode.AcceptVisitor(this);
            }

            TextComposer.Append(")");
        }

        if (code.IsArrayAccess)
        {
            TextComposer.Append(code.HeadText).Append("[");

            var flag = false;
            foreach (var argCode in code.Arguments)
            {
                if (flag) TextComposer.Append(", ");
                else flag = true;

                argCode.AcceptVisitor(this);
            }

            TextComposer.Append("]");
        }

        if (code.IsOperator)
        {
            var opHeaderSpecs = (SteOperatorSpecs)code.HeadSpecs;

            if (opHeaderSpecs.Position == SteOperatorPosition.Infix)
            {
                var flag = false;
                foreach (var argCode in code.Arguments)
                {
                    if (flag) TextComposer.Append(opHeaderSpecs.Symbol);
                    else flag = true;

                    if (argCode.Precedence > code.Precedence)
                    {
                        TextComposer.Append("(");
                        argCode.AcceptVisitor(this);
                        TextComposer.Append(")");
                    }
                    else
                    {
                        argCode.AcceptVisitor(this);
                    }
                }

                return;
            }

            var argCode1 = code.FirstArgument;

            if (ReferenceEquals(argCode1, null) == false)
            {
                if (opHeaderSpecs.Position == SteOperatorPosition.Prefix)
                    TextComposer.Append(opHeaderSpecs.Symbol);

                if (argCode1.Precedence > code.Precedence)
                {
                    TextComposer.Append("(");
                    argCode1.AcceptVisitor(this);
                    TextComposer.Append(")");
                }
                else
                {
                    argCode1.AcceptVisitor(this);
                }

                if (opHeaderSpecs.Position == SteOperatorPosition.Suffix)
                    TextComposer.Append(opHeaderSpecs.Symbol);
            }
        }
    }

    public void Visit(SteDeclareMethod code)
    {
        var modifiersText = code.ModifiersList.Concatenate(" ");

        TextComposer
            .Append(modifiersText)
            .Append(code.ReturnType)
            .Append(code.MethodName)
            .Append("(");

        foreach (var item in code.Parameters)
            item.AcceptVisitor(this);

        TextComposer
            .AppendLine(")")
            .Append("{")
            .IncreaseIndentation();

        code.MethodCode.AcceptVisitor(this);

        TextComposer
            .DecreaseIndentation()
            .AppendAtNewLine("}");
    }

    public void Visit(SteDeclareSimpleDataStore code)
    {
        TextComposer
            .Append(code.DataStoreType)
            .Append(" ")
            .Append(code.DataStoreName);
    }

    public void Visit(SteIf code)
    {
        TextComposer.AppendAtNewLine("if (");

        code.Condition.AcceptVisitor(this);

        TextComposer
            .AppendLine(")")
            .AppendLine("{")
            .IncreaseIndentation();

        code.TrueCode.AcceptVisitor(this);

        TextComposer
            .DecreaseIndentation()
            .AppendLineAtNewLine("}");
    }

    public void Visit(SteIfElse code)
    {
        TextComposer.AppendAtNewLine("if (");

        code.Condition.AcceptVisitor(this);

        TextComposer
            .AppendLine(")")
            .AppendLine("{")
            .IncreaseIndentation();

        code.TrueCode.AcceptVisitor(this);

        TextComposer
            .DecreaseIndentation()
            .AppendLineAtNewLine("}");

        if (code.ElseCode == null) return;

        TextComposer.AppendLine("else")
            .AppendLine("{")
            .IncreaseIndentation();

        code.ElseCode.AcceptVisitor(this);

        TextComposer
            .DecreaseIndentation()
            .AppendLineAtNewLine("}");
    }

    public void Visit(SteIfElseIfElse code)
    {
        var flag = false;
        foreach (var item in code.IfList)
        {
            if (flag == false)
            {
                TextComposer.AppendAtNewLine("if (");
                flag = true;
            }
            else
                TextComposer.AppendAtNewLine("else if (");

            item.Condition.AcceptVisitor(this);

            TextComposer
                .AppendLine(")")
                .AppendLine("{")
                .IncreaseIndentation();

            item.TrueCode.AcceptVisitor(this);

            TextComposer
                .DecreaseIndentation()
                .AppendLineAtNewLine("}");
        }

        if (code.ElseCode == null) return;

        TextComposer.AppendLine("else")
            .AppendLine("{")
            .IncreaseIndentation();

        code.ElseCode.AcceptVisitor(this);

        TextComposer
            .DecreaseIndentation()
            .AppendLineAtNewLine("}");
    }

    public void Visit(SteForLoop code)
    {
        TextComposer.AppendAtNewLine("for (");

        code.LoopInitialization.AcceptVisitor(this);

        TextComposer.Append("; ");

        code.LoopCondition.AcceptVisitor(this);

        TextComposer.Append("; ");

        code.LoopUpdate.AcceptVisitor(this);

        TextComposer.AppendLine(")").Append("{").IncreaseIndentation();

        code.LoopCode.AcceptVisitor(this);

        TextComposer.DecreaseIndentation().AppendLineAtNewLine("}");
    }

    public void Visit(SteForEachLoop code)
    {
        TextComposer
            .AppendAtNewLine("foreach (")
            .Append(code.LoopVariableType)
            .Append(code.LoopVariableName)
            .Append(" in ");

        code.LoopCollection.AcceptVisitor(this);

        TextComposer
            .AppendLine(")")
            .Append("{")
            .IncreaseIndentation();

        code.LoopCode.AcceptVisitor(this);

        TextComposer
            .DecreaseIndentation()
            .AppendLineAtNewLine("}");
    }

    public void Visit(SteWhileLoop code)
    {
        if (code.DoLoop)
        {
            TextComposer
                .AppendAtNewLine("do")
                .AppendLine("{")
                .IncreaseIndentation();

            code.LoopCode.AcceptVisitor(this);

            TextComposer
                .DecreaseIndentation()
                .AppendAtNewLine("} while (");

            code.LoopCondition.AcceptVisitor(this);

            TextComposer.AppendNewLine(");");

            return;
        }

        TextComposer.AppendAtNewLine("while (");

        code.LoopCondition.AcceptVisitor(this);

        TextComposer
            .AppendLine(")")
            .AppendLine("{")
            .IncreaseIndentation();

        code.LoopCode.AcceptVisitor(this);

        TextComposer
            .DecreaseIndentation()
            .AppendLineAtNewLine("}");
    }

    public void Visit(SteImportNamespaces code)
    {
        foreach (var item in code.ImportedNamespaces)
            TextComposer.Append("using ").Append(item).AppendLine(";");
    }

    public void Visit(SteSetNamespace code)
    {
        TextComposer
            .Append("namespace ")
            .Append(code.NamespaceName)
            .AppendLineAtNewLine("{")
            .IncreaseIndentation();

        code.SubCode.AcceptVisitor(this);

        TextComposer
            .DecreaseIndentation()
            .AppendLineAtNewLine("}");
    }

    public void Visit(TccSwitchCaseItem code)
    {
        TextComposer.AppendAtNewLine("case ");

        code.CaseValue.AcceptVisitor(this);

        TextComposer.AppendLine(":").IncreaseIndentation();

        code.CaseCode.AcceptVisitor(this);

        if (code.BreakCase)
            TextComposer.AppendLineAtNewLine("break;");

        TextComposer.DecreaseIndentation();
    }

    public void Visit(SteSwitchCase code)
    {
        TextComposer.AppendAtNewLine("switch (");

        code.SwitchExpression.AcceptVisitor(this);

        TextComposer
            .AppendLine(")")
            .AppendLine("{")
            .IncreaseIndentation();

        foreach (var item in code.CasesList)
            item.AcceptVisitor(this);

        if (code.DefaultCode != null)
        {
            TextComposer.AppendLineAtNewLine("default:").IncreaseIndentation();
            code.DefaultCode.AcceptVisitor(this);
            TextComposer.AppendLineAtNewLine("break;");
        }

        TextComposer
            .DecreaseIndentation()
            .AppendLineAtNewLine("}");
    }

    public void Visit(SteThrowException code)
    {
        TextComposer.Append("throw ");
        code.ExceptionCode.AcceptVisitor(this);
    }

    public void Visit(SteTryCatchItem code)
    {
        TextComposer.AppendLineAtNewLine("catch (");

        code.CatchException.AcceptVisitor(this);

        TextComposer
            .AppendLine(")")
            .AppendLine("{")
            .IncreaseIndentation();

        code.CatchCode.AcceptVisitor(this);

        TextComposer
            .DecreaseIndentation()
            .AppendLineAtNewLine("}");
    }

    public void Visit(SteTryCatch code)
    {
        TextComposer
            .AppendLineAtNewLine("try")
            .AppendLine("{")
            .IncreaseIndentation();

        code.TryCode.AcceptVisitor(this);

        TextComposer
            .DecreaseIndentation()
            .AppendLineAtNewLine("}");

        if (code.CatchItems.Count == 0)
        {
            TextComposer
                .AppendLineAtNewLine("catch")
                .AppendLine("{")
                .AppendLine("}");
        }
        else
        {
            foreach (var item in code.CatchItems)
                item.AcceptVisitor(this);
        }

        if (code.FinallyCode != null)
        {
            TextComposer
                .AppendLineAtNewLine("finally")
                .AppendLine("{")
                .IncreaseIndentation();

            code.FinallyCode.AcceptVisitor(this);

            TextComposer
                .DecreaseIndentation()
                .AppendLineAtNewLine("}");
        }
    }

    public void Visit(SteDeclareLanguageConstruct code)
    {
        throw new System.NotImplementedException();
    }
}