using System.Text;

namespace GraphicsComposerLib.Rendering.Svg.Transforms
{
    public sealed class SvgTransformMatrix : SvgTransform
    {
        public static SvgTransformMatrix Create(double m00, double m01, double m02, double m10, double m11, double m12)
        {
            return new SvgTransformMatrix()
            {
                [0, 0] = m00,
                [0, 1] = m01,
                [0, 2] = m02,
                [1, 0] = m10,
                [1, 1] = m11,
                [1, 2] = m12
            };
        }

        public static SvgTransformMatrix Create(double[,] matrix)
        {
            return new SvgTransformMatrix()
            {
                [0, 0] = matrix[0, 0],
                [0, 1] = matrix[0, 1],
                [0, 2] = matrix[0, 2],
                [1, 0] = matrix[1, 0],
                [1, 1] = matrix[1, 1],
                [1, 2] = matrix[1, 2]
            };
        }


        private readonly double[,] _matrix = new double[2, 3];


        public double this[int row, int col]
        {
            get => _matrix[row, col];
            set => _matrix[row, col] = value;
        }


        public override string ValueText
            => new StringBuilder()
                .Append("matrix(")
                .Append(_matrix[0, 0].ToString("G")).Append(", ")
                .Append(_matrix[1, 0].ToString("G")).Append(", ")
                .Append(_matrix[0, 1].ToString("G")).Append(", ")
                .Append(_matrix[1, 1].ToString("G")).Append(", ")
                .Append(_matrix[0, 2].ToString("G")).Append(", ")
                .Append(_matrix[1, 2].ToString("G"))
                .Append(")")
                .ToString();


        private SvgTransformMatrix()
        {
        }
    }
}
