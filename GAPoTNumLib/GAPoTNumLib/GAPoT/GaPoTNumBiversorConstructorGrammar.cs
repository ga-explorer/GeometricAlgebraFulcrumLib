using Irony.Interpreter;
using Irony.Interpreter.Evaluator;
using Irony.Parsing;

namespace GAPoTNumLib.GAPoT
{
    public class GeoPoTNumBiversorConstructorGrammar : InterpretedLanguageGrammar
    {
        //Examples:
        //GAPoT biversor using terms form:
        //  -1.3<>, 1.2<1,2>, -4.6<3,4>
        public GeoPoTNumBiversorConstructorGrammar()
            : base(caseSensitive: true)
        {
            // 1. Terminals
            var number = TerminalFactory.CreateCSharpNumber("number");
            number.Options = NumberOptions.AllowSign;

            var comma1 = ToTerm(",");

            // 2. Non-terminals
            var biversor = new NonTerminal("biversor");
            var biversorTerm = new NonTerminal("biversorTerm");
            var biversorTerm0 = new NonTerminal("biversorTerm0");
            var biversorTerm2 = new NonTerminal("biversorTerm2");

            biversorTerm0.Rule = number + "<" + ">";
            biversorTerm2.Rule = number + "<" + number + comma1 + number + ">";
            biversorTerm.Rule = biversorTerm0 | biversorTerm2;
            biversor.Rule = MakePlusRule(biversor, comma1, biversorTerm);

            // Set grammar root
            Root = biversor;

            // 5. Punctuation and transient terms
            MarkPunctuation("<", ">", ",");
            RegisterBracePair("<", ">");
            MarkTransient(biversorTerm);

            // 7. Syntax error reporting
            AddToNoReportGroup("<");
            AddToNoReportGroup(NewLine);

            //9. Language flags. 
            // Automatically add NewLine before EOF so that our BNF rules work correctly when there's no final line break in source
            LanguageFlags = LanguageFlags.NewLineBeforeEOF;
        }

        public override LanguageRuntime CreateRuntime(LanguageData language)
        {
            return new ExpressionEvaluatorRuntime(language);
        }
    }
}