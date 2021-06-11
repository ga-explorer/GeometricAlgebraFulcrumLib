using CodeComposerLib.SyntaxTree;
using DataStructuresLib;

namespace CodeComposerLib.Languages.GMacDSL
{
    //TODO: Complete this for GMacDSL
    public class GMacDslCodeGenerator : LanguageCodeGenerator
    {
        internal GMacDslCodeGenerator()
        {
            LanguageInfo = new LanguageInfo("GMacDSL", "1.0", "GMacDSL 1.0");

            ScalarTypeName = "scalar";
            ScalarZero = "'0'";
        }

        public override void Visit(SteComment code)
        {
            foreach (var commentLine in code.CommentedTextLines)
                TextComposer.Append("//").AppendLine(commentLine);

            TextComposer.AppendAtNewLine();
        }

        public override void Visit(SteDeclareDataStore code)
        {
            if (code.LocalDataStore == false)
            {
                AddInternalComment("Non-local declaration not implemented: ", code.ToString());

                return;
            }

            if (code.InitialValue == null)
            {
                TextComposer
                    .Append("declare ")
                    .Append(code.DataStoreName)
                    .Append(" : ")
                    .Append(code.DataStoreType);

                TextComposer.AppendAtNewLine();

                return;
            }

            TextComposer
                .Append("let ")
                .Append(code.DataStoreName)
                .Append(" : ")
                .Append(code.DataStoreType)
                .Append(" = ");

            code.InitialValue.AcceptVisitor(this);

            TextComposer.AppendAtNewLine();
        }

        public override void Visit(SteDeclareFixedSizeArray code)
        {
            AddInternalComment("Fixed-size array declaration not implemented: ", code.ToString());
        }

        public override void Visit(SteAssign code)
        {
            if (code.LocalAssignment == false)
            {
                AddInternalComment("Non-local assignment not implemented: ", code.ToString());

                return;
            }

            TextComposer.Append("let ");

            code.LeftHandSide.AcceptVisitor(this);

            TextComposer.Append(" = ");

            code.RightHandSide.AcceptVisitor(this);

            TextComposer.AppendAtNewLine();
        }

        public override void Visit(SteReturn code)
        {
            TextComposer.AppendAtNewLine("return ");

            code.ReturnedValue.AcceptVisitor(this);
        }
    }
}
