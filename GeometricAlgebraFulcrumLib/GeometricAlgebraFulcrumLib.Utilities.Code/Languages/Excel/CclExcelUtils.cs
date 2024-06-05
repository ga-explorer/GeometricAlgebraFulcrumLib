using GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree.Expressions;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Languages.Excel;

public static class CclExcelUtils
{
    /// <summary>
    /// Most important Excel Formula operators
    /// </summary>
    public static class Operators
    {
        public static SteOperatorSpecs UnaryMinus { get; private set; }

        public static SteOperatorSpecs Percent { get; private set; }

        public static SteOperatorSpecs Exponent { get; private set; }

        public static SteOperatorSpecs Multiply { get; private set; }

        public static SteOperatorSpecs Divide { get; private set; }

        public static SteOperatorSpecs Add { get; private set; }

        public static SteOperatorSpecs Subtract { get; private set; }

        public static SteOperatorSpecs Less { get; private set; }

        public static SteOperatorSpecs More { get; private set; }

        public static SteOperatorSpecs LessOrEqual { get; private set; }

        public static SteOperatorSpecs MoreOrEqual { get; private set; }

        public static SteOperatorSpecs Equal { get; private set; }

        public static SteOperatorSpecs NotEqual { get; private set; }

        public static SteOperatorSpecs Concat { get; private set; }

        public static SteOperatorSpecs Range { get; private set; }

        public static SteOperatorSpecs RangeUnion { get; private set; }

        public static SteOperatorSpecs RangeIntersection { get; private set; }


        static Operators()
        {
            var precedence = 0;

            //Lowest precedence
            Equal =
                new SteOperatorSpecs("=", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            NotEqual =
                new SteOperatorSpecs("<>", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            Less =
                new SteOperatorSpecs("<", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            More =
                new SteOperatorSpecs(">", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            LessOrEqual =
                new SteOperatorSpecs("<=", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            MoreOrEqual =
                new SteOperatorSpecs(">=", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            precedence++;


            Concat =
                new SteOperatorSpecs("&", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            precedence++;


            Add =
                new SteOperatorSpecs("+", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            Subtract =
                new SteOperatorSpecs("-", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            precedence++;


            Multiply =
                new SteOperatorSpecs("*", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            Divide =
                new SteOperatorSpecs("/", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            precedence++;


            Exponent =
                new SteOperatorSpecs("^", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            precedence++;


            Percent =
                new SteOperatorSpecs("%", precedence, SteOperatorPosition.Suffix, SteOperatorAssociation.None);
            precedence++;


            UnaryMinus =
                new SteOperatorSpecs("-", precedence, SteOperatorPosition.Prefix, SteOperatorAssociation.None);
            precedence++;


            //Highest precedence
            Range =
                new SteOperatorSpecs(":", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            RangeUnion =
                new SteOperatorSpecs(",", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            RangeIntersection =
                new SteOperatorSpecs(" ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
        }
    }


    public static CclLanguageInfo Excel2007Info { get; private set; }


    static CclExcelUtils()
    {
        Excel2007Info = new CclLanguageInfo("Excel 2007 Formulas", "2007", "Excel 2007");
    }


    public static CclExcelCodeGenerator ExcelCodeComposer()
    {
        return new CclExcelCodeGenerator();
    }

    public static CclExcelSyntaxFactory ExcelSyntaxFactory()
    {
        return new CclExcelSyntaxFactory();
    }
}