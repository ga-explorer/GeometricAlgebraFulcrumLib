using CodeComposerLib.SyntaxTree.Expressions;

namespace CodeComposerLib.Languages.CSharp
{
    public static class CSharpUtils
    {
        /// <summary>
        /// Most important C# operators
        /// </summary>
        public static class Operators
        {
            public static SteOperatorSpecs MemberAccess { get; private set; }

            public static SteOperatorSpecs ReadThenIncrement { get; private set; }

            public static SteOperatorSpecs ReadThenDecrement { get; private set; }

            public static SteOperatorSpecs UnaryPlus { get; private set; }

            public static SteOperatorSpecs UnaryMinus { get; private set; }

            public static SteOperatorSpecs IncrementThenRead { get; private set; }

            public static SteOperatorSpecs DecrementThenRead { get; private set; }

            public static SteOperatorSpecs BitwiseNot { get; private set; }

            public static SteOperatorSpecs LogicalNot { get; private set; }

            public static SteOperatorSpecs Multiply { get; private set; }

            public static SteOperatorSpecs Divide { get; private set; }

            public static SteOperatorSpecs Remainder { get; private set; }

            public static SteOperatorSpecs Add { get; private set; }

            public static SteOperatorSpecs Subtract { get; private set; }

            public static SteOperatorSpecs ShiftLeft { get; private set; }

            public static SteOperatorSpecs ShiftRight { get; private set; }

            public static SteOperatorSpecs Less { get; private set; }

            public static SteOperatorSpecs More { get; private set; }

            public static SteOperatorSpecs LessOrEqual { get; private set; }

            public static SteOperatorSpecs MoreOrEqual { get; private set; }

            public static SteOperatorSpecs IsOfType { get; private set; }

            public static SteOperatorSpecs AsType { get; private set; }

            public static SteOperatorSpecs Equal { get; private set; }

            public static SteOperatorSpecs NotEqual { get; private set; }

            public static SteOperatorSpecs BitwiseAnd { get; private set; }

            public static SteOperatorSpecs LogicalAnd { get; private set; }

            public static SteOperatorSpecs BitwiseXor { get; private set; }

            public static SteOperatorSpecs LogicalXor { get; private set; }

            public static SteOperatorSpecs BitwiseOr { get; private set; }

            public static SteOperatorSpecs LogicalOr { get; private set; }

            public static SteOperatorSpecs ConditionalAnd { get; private set; }

            public static SteOperatorSpecs ConditionalOr { get; private set; }

            public static SteOperatorSpecs NullCoalescing { get; private set; }

            public static SteOperatorSpecs Conditional { get; private set; }

            public static SteOperatorSpecs Assign { get; private set; }

            public static SteOperatorSpecs AddThenAssign { get; private set; }

            public static SteOperatorSpecs SubtractThenAssign { get; private set; }

            public static SteOperatorSpecs MultiplyThenAssign { get; private set; }

            public static SteOperatorSpecs DivideThenAssign { get; private set; }

            public static SteOperatorSpecs RemainderThenAssign { get; private set; }

            public static SteOperatorSpecs BitwiseAndThenAssign { get; private set; }

            public static SteOperatorSpecs BitwiseXorThenAssign { get; private set; }

            public static SteOperatorSpecs BitwiseOrThenAssign { get; private set; }

            public static SteOperatorSpecs ShiftLeftThenAssign { get; private set; }

            public static SteOperatorSpecs ShiftRightThenAssign { get; private set; }


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

        
        public static LanguageInfo CSharp4Info { get; private set; }


        static CSharpUtils()
        {
            CSharp4Info = new LanguageInfo("CSharp", "4.0", "C# 4.0");
        }


        public static CSharpCodeGenerator CSharp4CodeGenerator()
        {
            return new CSharpCodeGenerator();
        }

        public static CSharpSyntaxFactory CSharp4SyntaxFactory()
        {
            return new CSharpSyntaxFactory();
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
}
