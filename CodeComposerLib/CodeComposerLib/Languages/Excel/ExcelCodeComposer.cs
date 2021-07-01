using CodeComposerLib.SyntaxTree;
using CodeComposerLib.SyntaxTree.Expressions;
using DataStructuresLib;

namespace CodeComposerLib.Languages.Excel
{
    public class ExcelCodeComposer : 
        LanguageCodeComposer, 
        IExpressionTreeCodeComposer
    {
        internal ExcelCodeComposer()
        {
            LanguageInfo = ExcelUtils.Excel2007Info;

            ScalarTypeName = "double";
            ScalarZero = "0.0";
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
                    if (flag) TextComposer.Append(";");
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

        public override void Visit(SteComment code)
        {
            throw new System.NotImplementedException();
        }

        public override void Visit(SteDeclareDataStore code)
        {
            throw new System.NotImplementedException();
        }

        public override void Visit(SteDeclareFixedSizeArray code)
        {
            throw new System.NotImplementedException();
        }

        public override void Visit(SteAssign code)
        {
            throw new System.NotImplementedException();
        }

        public override void Visit(SteReturn code)
        {
            throw new System.NotImplementedException();
        }
    }
}