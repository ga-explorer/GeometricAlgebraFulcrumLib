using System.Text;
using GeometricAlgebraFulcrumLib.Symbolic.Processors;
using GeometricAlgebraFulcrumLib.Text;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Symbolic.Text
{
    public sealed class GaTextComposerMathematicaExpr
        : GaTextComposer<Expr>
    {
        public static GaTextComposerMathematicaExpr DefaultComposer { get; }
            = new();


        public GaTextComposerMathematicaExpr() 
            : base(GaScalarProcessorMathematicaExpr.DefaultProcessor)
        {
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
}