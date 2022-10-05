using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CodeComposerLib.Languages.CSharp;
using CodeComposerLib.SyntaxTree.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Composite;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.HeadSpecs;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Numbers;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Languages.CSharp;

public sealed class MetaExpressionToCSharpScalarProcessorConverter : 
    MetaExpressionToLanguageConverterBase
{
    public static MetaExpressionToCSharpScalarProcessorConverter DefaultConverter { get; }
        = new MetaExpressionToCSharpScalarProcessorConverter("sp");

    public static MetaExpressionToCSharpScalarProcessorConverter Create(string scalarProcessorVariableName)
    {
        return new MetaExpressionToCSharpScalarProcessorConverter(scalarProcessorVariableName);
    }


    public string ScalarProcessorVariableName { get; }


    private MetaExpressionToCSharpScalarProcessorConverter([NotNull] string scalarProcessorVariableName)
        : base(CclCSharpUtils.CSharp4Info)
    {
        ScalarProcessorVariableName = scalarProcessorVariableName;
    }
        

    public override SteExpression Visit(IMetaExpressionFunction functionExpr)
    {
        var functionName = 
            functionExpr.HeadSpecs.HeadText;

        var argumentsArray =
            functionExpr.Arguments.Select(Convert).ToArray();

        if (argumentsArray.Length == 0)
            return SteExpression.CreateFunction(functionName);

        switch (functionName)
        {
            case "Plus":
                return SteExpression.CreateFunction($"{ScalarProcessorVariableName}.Add", argumentsArray);

            case "Minus":
                return SteExpression.CreateFunction($"{ScalarProcessorVariableName}.Negative", argumentsArray);

            case "Subtract":
                return SteExpression.CreateFunction($"{ScalarProcessorVariableName}.Subtract", argumentsArray);

            case "Times":
                if (argumentsArray[0].ToString() == "-1" && argumentsArray.Length == 2)
                    return SteExpression.CreateFunction($"{ScalarProcessorVariableName}.Negative", argumentsArray[1]);

                return SteExpression.CreateFunction($"{ScalarProcessorVariableName}.Times", argumentsArray);
                
                //    return SteExpression.CreateOperator(
                //        CclCSharpUtils.Operators.UnaryMinus, argumentsArray[1]
                //    );

                //return SteExpression.CreateOperator(
                //    CclCSharpUtils.Operators.Multiply, argumentsArray
                //);

            case "Divide":
                return SteExpression.CreateFunction($"{ScalarProcessorVariableName}.Divide", argumentsArray);

            case "Power":
                return SteExpression.CreateFunction($"{ScalarProcessorVariableName}.Power", argumentsArray);
                //return argumentsArray[1].ToString() switch
                //{
                //    "-1" => SteExpression.CreateOperator(CclCSharpUtils.Operators.Divide,
                //        SteExpression.CreateLiteralNumber(1), argumentsArray[0]),
                //    "2" => SteExpression.CreateOperator(CclCSharpUtils.Operators.Multiply, argumentsArray[0],
                //        argumentsArray[0]),
                //    "3" => SteExpression.CreateOperator(CclCSharpUtils.Operators.Multiply, argumentsArray[0],
                //        argumentsArray[0], argumentsArray[0]),
                //    _ => SteExpression.CreateFunction("Math.Pow", argumentsArray)
                //};

            case "Abs":
                return SteExpression.CreateFunction($"{ScalarProcessorVariableName}.Abs", argumentsArray);

            case "Exp":
                return SteExpression.CreateFunction($"{ScalarProcessorVariableName}.Exp", argumentsArray);

            case "Sin":
                return SteExpression.CreateFunction($"{ScalarProcessorVariableName}.Sin", argumentsArray);

            case "Cos":
                return SteExpression.CreateFunction($"{ScalarProcessorVariableName}.Cos", argumentsArray);

            case "Tan":
                return SteExpression.CreateFunction($"{ScalarProcessorVariableName}.Tan", argumentsArray);

            case "ArcSin":
                return SteExpression.CreateFunction($"{ScalarProcessorVariableName}.ArcSin", argumentsArray);

            case "ArcCos":
                return SteExpression.CreateFunction($"{ScalarProcessorVariableName}.ArcCos", argumentsArray);

            case "ArcTan":
                return SteExpression.CreateFunction(
                    argumentsArray.Length == 1 ? $"{ScalarProcessorVariableName}.ArcTan" : "Math.ArcTan2",
                    argumentsArray
                );

            case "Sinh":
                return SteExpression.CreateFunction($"{ScalarProcessorVariableName}.Sinh", argumentsArray);

            case "Cosh":
                return SteExpression.CreateFunction($"{ScalarProcessorVariableName}.Cosh", argumentsArray);

            case "Tanh":
                return SteExpression.CreateFunction($"{ScalarProcessorVariableName}.Tanh", argumentsArray);

            case "Log":
                return SteExpression.CreateFunction(
                    $"{ScalarProcessorVariableName}.Log", 
                    argumentsArray.Length == 1 ? argumentsArray : argumentsArray.Reverse()
                );

            case "Log10":
                return SteExpression.CreateFunction($"{ScalarProcessorVariableName}.Log10", argumentsArray);

            case "Sqrt":
                return SteExpression.CreateFunction($"{ScalarProcessorVariableName}.Sqrt", argumentsArray);

            case "Floor":
                return SteExpression.CreateFunction(
                    argumentsArray.Length == 1 ? $"{ScalarProcessorVariableName}.Floor" : "MathHelper.Floor",
                    argumentsArray
                );

            case "Ceiling":
                return SteExpression.CreateFunction(
                    argumentsArray.Length == 1 ? $"{ScalarProcessorVariableName}.Ceiling" : "MathHelper.Ceiling",
                    argumentsArray
                );

            case "Round":
                return SteExpression.CreateFunction(
                    argumentsArray.Length == 1 ? $"{ScalarProcessorVariableName}.Round" : "MathHelper.Round",
                    argumentsArray
                );

            case "Min":
                return SteExpression.CreateFunction(
                    argumentsArray.Length == 1 ? $"{ScalarProcessorVariableName}.Min" : "MathHelper.Min",
                    argumentsArray
                );

            case "Max":
                return SteExpression.CreateFunction(
                    argumentsArray.Length == 1 ? $"{ScalarProcessorVariableName}.Max" : "MathHelper.Max",
                    argumentsArray
                );

            case "Sign":
                return SteExpression.CreateFunction($"{ScalarProcessorVariableName}.Sign", argumentsArray);

            case "IntegerPart":
                return SteExpression.CreateFunction($"{ScalarProcessorVariableName}.Truncate", argumentsArray);
        }

        return SteExpression.CreateFunction($"{ScalarProcessorVariableName}." + functionName, argumentsArray);
    }
    
    private SteExpression Visit(int value)
    {
        return value switch
        {
            0 => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarZero"
            ),

            1 => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarOne"
            ),

            -1 => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarMinusOne"
            ),

            2 => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarTwo"
            ),

            -2 => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarMinusTwo"
            ),

            10 => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarTen"
            ),

            -10 => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarMinusTen"
            ),

            _ => SteExpression.CreateFunction(
                $"{ScalarProcessorVariableName}.GetScalarFromNumber",
                value
            )
        };
    }

    private SteExpression Visit(uint value)
    {
        return value switch
        {
            0U => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarZero"
            ),

            1U => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarOne"
            ),

            2U => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarTwo"
            ),

            10U => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarTen"
            ),

            _ => SteExpression.CreateFunction(
                $"{ScalarProcessorVariableName}.GetScalarFromNumber",
                value
            )
        };
    }
    
    private SteExpression Visit(long value)
    {
        return value switch
        {
            0L => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarZero"
            ),

            1L => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarOne"
            ),

            -1L => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarMinusOne"
            ),

            2L => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarTwo"
            ),

            -2L => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarMinusTwo"
            ),

            10L => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarTen"
            ),

            -10L => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarMinusTen"
            ),

            _ => SteExpression.CreateFunction(
                $"{ScalarProcessorVariableName}.GetScalarFromNumber",
                value
            )
        };
    }

    private SteExpression Visit(ulong value)
    {
        return value switch
        {
            0UL => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarZero"
            ),

            1UL => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarOne"
            ),

            2UL => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarTwo"
            ),

            10UL => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarTen"
            ),

            _ => SteExpression.CreateFunction(
                $"{ScalarProcessorVariableName}.GetScalarFromNumber",
                value
            )
        };
    }

    private SteExpression Visit(float value)
    {
        return value switch
        {
            0f => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarZero"
            ),

            1f => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarOne"
            ),

            -1f => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarMinusOne"
            ),

            2f => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarTwo"
            ),

            -2f => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarMinusTwo"
            ),

            10f => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarTen"
            ),

            -10f => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarMinusTen"
            ),

            _ => SteExpression.CreateFunction(
                $"{ScalarProcessorVariableName}.GetScalarFromNumber",
                value
            )
        };
    }
    
    private SteExpression Visit(double value)
    {
        return value switch
        {
            0d => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarZero"
            ),

            1d => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarOne"
            ),

            -1d => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarMinusOne"
            ),

            2d => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarTwo"
            ),

            -2d => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarMinusTwo"
            ),

            10d => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarTen"
            ),

            -10d => SteExpression.CreateSymbolicNumber(
                $"{ScalarProcessorVariableName}.ScalarMinusTen"
            ),

            _ => SteExpression.CreateFunction(
                $"{ScalarProcessorVariableName}.GetScalarFromNumber",
                value
            )
        };
    }

    public override SteExpression Visit(IMetaExpressionNumber numberExpr)
    {
        return numberExpr.HeadSpecs switch
        {
            MetaExpressionHeadSpecsNumberInt32 int32HeadSpecs =>
                Visit(int32HeadSpecs.NumberInt32Value),

            MetaExpressionHeadSpecsNumberUInt32 uint32HeadSpecs =>
                Visit(uint32HeadSpecs.NumberUInt32Value),

            MetaExpressionHeadSpecsNumberInt64 int64HeadSpecs =>
                Visit(int64HeadSpecs.NumberInt64Value),

            MetaExpressionHeadSpecsNumberUInt64 uint64HeadSpecs =>
                Visit(uint64HeadSpecs.NumberUInt64Value),

            MetaExpressionHeadSpecsNumberFloat32 float32HeadSpecs =>
                Visit(float32HeadSpecs.NumberFloat32Value),

            MetaExpressionHeadSpecsNumberFloat64 float64HeadSpecs =>
                Visit(float64HeadSpecs.NumberFloat64Value),

            MetaExpressionHeadSpecsNumberRational rationalHeadSpecs => 
                SteExpression.CreateFunction(
                    $"{ScalarProcessorVariableName}.GetScalarFromRational", 
                    SteExpression.CreateLiteralNumber(rationalHeadSpecs.Numerator), 
                    SteExpression.CreateLiteralNumber(rationalHeadSpecs.Denominator)
                ),

            MetaExpressionHeadSpecsNumberSymbolic symbolicHeadSpecs => 
                symbolicHeadSpecs.HeadText switch
                {
                    "Pi" => SteExpression.CreateSymbolicNumber(
                        $"{ScalarProcessorVariableName}.ScalarPi"
                    ),

                    "E" => SteExpression.CreateSymbolicNumber(
                        $"{ScalarProcessorVariableName}.ScalarE"
                    ),

                    _ => numberExpr.ToSimpleTextExpression()
                },

            _ => 
                numberExpr.ToSimpleTextExpression()
        };
    }
}