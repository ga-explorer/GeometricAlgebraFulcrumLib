using System.Collections.Generic;
using Wolfram.NETLink;

namespace GeometricAlgebraLib.Symbolic.Mathematica.ExprFactory
{
    public static class CharacterSymbols
    {
        private static Dictionary<char, Expr> _scriptChars;

        private static Dictionary<char, Expr> _gothicChars;

        private static Dictionary<char, Expr> _doubleStruckChars;

        private static Dictionary<char, Expr> _formalChars;


        private static Dictionary<char, Expr> FillScriptChars()
        {
            var scriptChars = new Dictionary<char, Expr>();

            for (var c = 'a'; c <= 'z'; c++)
                scriptChars.Add(c, new Expr(ExpressionType.Symbol, "\\[Script" + c + "]"));

            for (var c = 'A'; c <= 'Z'; c++)
                scriptChars.Add(c, new Expr(ExpressionType.Symbol, "\\[ScriptCapital" + c + "]"));

            return scriptChars;
        }

        private static Dictionary<char, Expr> FillGothicChars()
        {
            var gothicChars = new Dictionary<char, Expr>();

            for (var c = 'a'; c <= 'z'; c++)
                gothicChars.Add(c, new Expr(ExpressionType.Symbol, "\\[Gothic" + c + "]"));

            for (var c = 'A'; c <= 'Z'; c++)
                gothicChars.Add(c, new Expr(ExpressionType.Symbol, "\\[GothicCapital" + c + "]"));

            return gothicChars;
        }

        private static Dictionary<char, Expr> FillDoubleStruckChars()
        {
            var doubleStruckChars = new Dictionary<char, Expr>();

            for (var c = 'a'; c <= 'z'; c++)
                doubleStruckChars.Add(c, new Expr(ExpressionType.Symbol, "\\[DoubleStruck" + c + "]"));

            for (var c = 'A'; c <= 'Z'; c++)
                doubleStruckChars.Add(c, new Expr(ExpressionType.Symbol, "\\[DoubleStruckCapital" + c + "]"));

            return doubleStruckChars;
        }

        private static Dictionary<char, Expr> FillFormalChars()
        {
            var formalChars = new Dictionary<char, Expr>();

            for (var c = 'a'; c <= 'z'; c++)
                formalChars.Add(c, new Expr(ExpressionType.Symbol, "\\[Formal" + c + "]"));

            for (var c = 'A'; c <= 'Z'; c++)
                formalChars.Add(c, new Expr(ExpressionType.Symbol, "\\[FormalCapital" + c + "]"));

            return formalChars;
        }


        public static Expr Script(char c)
        {
            if (_scriptChars == null)
                _scriptChars = FillScriptChars();

            return _scriptChars[c];
        }

        public static Expr Gothic(char c)
        {
            if (_gothicChars == null)
                _gothicChars = FillGothicChars();

            return _gothicChars[c];
        }

        public static Expr DoubleStruck(char c)
        {
            if (_doubleStruckChars == null)
                _doubleStruckChars = FillDoubleStruckChars();

            return _doubleStruckChars[c];
        }

        public static Expr Formal(char c)
        {
            if (_formalChars == null)
                _formalChars = FillFormalChars();

            return _formalChars[c];
        }
    }
}
