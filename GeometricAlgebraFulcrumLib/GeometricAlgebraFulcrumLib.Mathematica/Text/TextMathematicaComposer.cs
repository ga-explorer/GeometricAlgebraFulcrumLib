using System.Text;
using NumericalGeometryLib.BasicMath;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.Text;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Mathematica.Text
{
    public sealed class TextMathematicaComposer
        : TextComposerBase<Expr>
    {
        public static TextMathematicaComposer DefaultComposer { get; }
            = new();


        private TextMathematicaComposer() 
            : base(ScalarAlgebraMathematicaProcessor.DefaultProcessor)
        {
        }


        public override string GetAngleText(PlanarAngle angle)
        {
            return $"{angle.Degrees} Degree";
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