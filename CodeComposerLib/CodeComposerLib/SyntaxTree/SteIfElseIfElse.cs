using System.Collections.Generic;

namespace CodeComposerLib.SyntaxTree
{
    public class SteIfElseIfElse : SteSyntaxElement
    {
        public List<SteIf> IfList { get; }

        public ISyntaxTreeElement ElseCode { get; set; }


        public SteIfElseIfElse()
        {
            IfList = new List<SteIf>();
        }


        public SteIfElseIfElse AddElseIf(ISyntaxTreeElement condition, ISyntaxTreeElement code)
        {
            IfList.Add(
                new SteIf()
                {
                    Condition = condition,
                    TrueCode = code
                });

            return this;
        }

        public SteIfElseIfElse AddElseIf(string condition, ISyntaxTreeElement code)
        {
            IfList.Add(
                new SteIf()
                {
                    Condition = new SteFixedCode(condition),
                    TrueCode = code
                });

            return this;
        }

        public SteIfElseIfElse AddElseIf(string condition, string code)
        {
            IfList.Add(
                new SteIf()
                {
                    Condition = new SteFixedCode(condition),
                    TrueCode = new SteFixedCode(code)
                });

            return this;
        }
    }
}
