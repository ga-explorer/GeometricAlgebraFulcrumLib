using CodeComposerLib.SyntaxTree.Expressions;

namespace CodeComposerLib.Languages.CSharp;

public static class CclCSharpUtils
{
    /// <summary>
    /// Most important C# operators
    /// </summary>
    public static class Operators
    {
        public static SteOperatorSpecs MemberAccess { get; }

        public static SteOperatorSpecs ReadThenIncrement { get; }

        public static SteOperatorSpecs ReadThenDecrement { get; }

        public static SteOperatorSpecs UnaryPlus { get; }

        public static SteOperatorSpecs UnaryMinus { get; }

        public static SteOperatorSpecs IncrementThenRead { get; }

        public static SteOperatorSpecs DecrementThenRead { get; }

        public static SteOperatorSpecs BitwiseNot { get; }

        public static SteOperatorSpecs LogicalNot { get; }

        public static SteOperatorSpecs Multiply { get; }

        public static SteOperatorSpecs Divide { get; }

        public static SteOperatorSpecs Remainder { get; }

        public static SteOperatorSpecs Add { get; }

        public static SteOperatorSpecs Subtract { get; }

        public static SteOperatorSpecs ShiftLeft { get; }

        public static SteOperatorSpecs ShiftRight { get; }

        public static SteOperatorSpecs Less { get; }

        public static SteOperatorSpecs More { get; }

        public static SteOperatorSpecs LessOrEqual { get; }

        public static SteOperatorSpecs MoreOrEqual { get; }

        public static SteOperatorSpecs IsOfType { get; }

        public static SteOperatorSpecs AsType { get; }

        public static SteOperatorSpecs Equal { get; }

        public static SteOperatorSpecs NotEqual { get; }

        public static SteOperatorSpecs BitwiseAnd { get; }

        public static SteOperatorSpecs LogicalAnd { get; }

        public static SteOperatorSpecs BitwiseXor { get; }

        public static SteOperatorSpecs LogicalXor { get; }

        public static SteOperatorSpecs BitwiseOr { get; }

        public static SteOperatorSpecs LogicalOr { get; }

        public static SteOperatorSpecs ConditionalAnd { get; }

        public static SteOperatorSpecs ConditionalOr { get; }

        public static SteOperatorSpecs NullCoalescing { get; }

        public static SteOperatorSpecs Conditional { get; }

        public static SteOperatorSpecs Assign { get; }

        public static SteOperatorSpecs AddThenAssign { get; }

        public static SteOperatorSpecs SubtractThenAssign { get; }

        public static SteOperatorSpecs MultiplyThenAssign { get; }

        public static SteOperatorSpecs DivideThenAssign { get; }

        public static SteOperatorSpecs RemainderThenAssign { get; }

        public static SteOperatorSpecs BitwiseAndThenAssign { get; }

        public static SteOperatorSpecs BitwiseXorThenAssign { get; }

        public static SteOperatorSpecs BitwiseOrThenAssign { get; }

        public static SteOperatorSpecs ShiftLeftThenAssign { get; }

        public static SteOperatorSpecs ShiftRightThenAssign { get; }


        static Operators()
        {
            var precedence = 0;

            //Lowest precedence
            Assign =
                new SteOperatorSpecs(" = ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Right);
            AddThenAssign =
                new SteOperatorSpecs(" += ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Right);
            SubtractThenAssign =
                new SteOperatorSpecs(" -= ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Right);
            MultiplyThenAssign =
                new SteOperatorSpecs(" *= ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Right);
            DivideThenAssign =
                new SteOperatorSpecs(" /= ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Right);
            RemainderThenAssign =
                new SteOperatorSpecs(" %= ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Right);
            BitwiseAndThenAssign =
                new SteOperatorSpecs(" &= ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Right);
            BitwiseXorThenAssign =
                new SteOperatorSpecs(" ^= ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Right);
            BitwiseOrThenAssign =
                new SteOperatorSpecs(" |= ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Right);
            ShiftLeftThenAssign =
                new SteOperatorSpecs(" <<= ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Right);
            ShiftRightThenAssign =
                new SteOperatorSpecs(" >>= ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Right);
            precedence++;

            Conditional =
                new SteOperatorSpecs(" ?: ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Right);
            precedence++;

            NullCoalescing =
                new SteOperatorSpecs(" ?? ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            precedence++;

            ConditionalOr =
                new SteOperatorSpecs(" || ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            precedence++;

            ConditionalAnd =
                new SteOperatorSpecs(" && ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            precedence++;

            BitwiseOr =
                new SteOperatorSpecs(" | ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            LogicalOr =
                new SteOperatorSpecs(" | ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            precedence++;

            BitwiseXor =
                new SteOperatorSpecs(" ^ ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            LogicalXor =
                new SteOperatorSpecs(" ^ ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            precedence++;

            BitwiseAnd =
                new SteOperatorSpecs(" & ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            LogicalAnd =
                new SteOperatorSpecs(" & ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            precedence++;

            Equal =
                new SteOperatorSpecs(" == ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            NotEqual =
                new SteOperatorSpecs(" != ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            precedence++;

            Less =
                new SteOperatorSpecs(" < ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            More =
                new SteOperatorSpecs(" > ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            LessOrEqual =
                new SteOperatorSpecs(" <= ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            MoreOrEqual =
                new SteOperatorSpecs(" >= ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            IsOfType =
                new SteOperatorSpecs(" is ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            AsType =
                new SteOperatorSpecs(" as ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            precedence++;

            ShiftLeft =
                new SteOperatorSpecs(" << ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            ShiftRight =
                new SteOperatorSpecs(" >> ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            precedence++;

            Add =
                new SteOperatorSpecs(" + ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            Subtract =
                new SteOperatorSpecs(" - ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            precedence++;

            Multiply =
                new SteOperatorSpecs(" * ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            Divide =
                new SteOperatorSpecs(" / ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            Remainder =
                new SteOperatorSpecs(" % ", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            precedence++;

            UnaryPlus =
                new SteOperatorSpecs("+", precedence, SteOperatorPosition.Prefix, SteOperatorAssociation.None);
            UnaryMinus =
                new SteOperatorSpecs("-", precedence, SteOperatorPosition.Prefix, SteOperatorAssociation.None);
            IncrementThenRead =
                new SteOperatorSpecs("++", precedence, SteOperatorPosition.Prefix, SteOperatorAssociation.None);
            DecrementThenRead =
                new SteOperatorSpecs("--", precedence, SteOperatorPosition.Prefix, SteOperatorAssociation.None);
            BitwiseNot =
                new SteOperatorSpecs("~", precedence, SteOperatorPosition.Prefix, SteOperatorAssociation.None);
            LogicalNot =
                new SteOperatorSpecs("!", precedence, SteOperatorPosition.Prefix, SteOperatorAssociation.None);
            precedence++;

            //Highest precedence
            MemberAccess =
                new SteOperatorSpecs(".", precedence, SteOperatorPosition.Infix, SteOperatorAssociation.Left);
            ReadThenIncrement =
                new SteOperatorSpecs("++", precedence, SteOperatorPosition.Suffix, SteOperatorAssociation.None);
            ReadThenDecrement =
                new SteOperatorSpecs("--", precedence, SteOperatorPosition.Suffix, SteOperatorAssociation.None);
        }
    }

        
    public static CclLanguageInfo CSharp4Info { get; }


    static CclCSharpUtils()
    {
        CSharp4Info = new CclLanguageInfo("CSharp", "4.0", "C# 4.0");
    }


    public static CclCSharpCodeGenerator CSharp4CodeComposer()
    {
        return new CclCSharpCodeGenerator();
    }

    public static CclCSharpSyntaxFactory CSharp4SyntaxFactory()
    {
        return new CclCSharpSyntaxFactory();
    }

    //public static class Modifiers
    //{
    //    public static string[] Public { get; private set; }

    //    public static string[] PublicStatic { get; private set; }

    //    //public static string[] 

            
    //    static Modifiers()
    //    {
    //        Public = new[]
    //        {
    //            TccModifierNames.PublicModifier, TccModifierNames.
    //        };

    //        PublicStatic = new[]
    //        {
    //            TccModifierNames.PublicModifier,
    //            TccModifierNames.StaticModifier
    //        };
    //    }
    //}
}