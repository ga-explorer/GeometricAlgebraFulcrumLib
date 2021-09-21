using System.Text;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
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