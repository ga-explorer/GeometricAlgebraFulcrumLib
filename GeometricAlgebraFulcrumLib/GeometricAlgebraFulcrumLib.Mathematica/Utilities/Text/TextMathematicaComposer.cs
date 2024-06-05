using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.Utilities.Text;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra.Scalars;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Mathematica.Utilities.Text;

public sealed class TextMathematicaComposer
    : TextComposer<Expr>
{
    public static TextMathematicaComposer DefaultComposer { get; }
        = new();


    private TextMathematicaComposer()
        : base(ScalarProcessorOfWolframExpr.Instance)
    {
    }


    public override string GetAngleText(LinFloat64Angle angle)
    {
        return $"{angle.DegreesValue} Degree";
    }

    public override string GetScalarText(Expr scalar)
    {
        return scalar.ToString();
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