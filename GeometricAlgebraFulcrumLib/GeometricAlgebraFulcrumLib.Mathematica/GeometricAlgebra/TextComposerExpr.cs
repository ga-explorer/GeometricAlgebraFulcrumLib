using System.Text;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.MathBase.Text;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Mathematica.GeometricAlgebra;

public sealed class TextComposerExpr
    : TextComposer<Expr>
{
    public static TextComposerExpr DefaultComposer { get; }
        = new();


    private TextComposerExpr() 
        : base(ScalarProcessorOfWolframExpr.DefaultProcessor)
    {
    }


    public override string GetAngleText(Float64PlanarAngle angle)
    {
        return $"{angle.Degrees} Degree";
    }

    public override string GetScalarText(Expr scalar)
    {
        return scalar.ToString();
    }
        
    public override string GetArrayText(double[,] array)
    {
        var composer = new StringBuilder();

        composer.Append("List[");

        var rowsCount = array.GetLength(0);
        var colsCount = array.GetLength(1);

        for (var i = 0; i < rowsCount; i++)
        {
            if (i > 0) composer.Append(", ");

            composer.Append("List[");

            for (var j = 0; j < colsCount; j++)
            {
                if (j > 0) composer.Append(", ");

                composer.Append(GetScalarText(array[i, j]));
            }

            composer.Append("]");
        }

        composer.Append("]");

        return composer.ToString();
    }

    public override string GetArrayText(Expr[,] array)
    {
        var composer = new StringBuilder();

        composer.Append("List[");

        var rowsCount = array.GetLength(0);
        var colsCount = array.GetLength(1);

        for (var i = 0; i < rowsCount; i++)
        {
            if (i > 0) composer.Append(", ");

            composer.Append("List[");

            for (var j = 0; j < colsCount; j++)
            {
                if (j > 0) composer.Append(", ");

                composer.Append(GetScalarText(array[i, j]));
            }

            composer.Append("]");
        }

        composer.Append("]");

        return composer.ToString();
    }
}