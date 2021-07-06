using GeometricAlgebraFulcrumLib.Symbolic.Mathematica.Expression;

namespace GeometricAlgebraFulcrumLib.Symbolic.Mathematica
{
    public interface ISymbolicMatrix : ISymbolicObject
    {
        int RowCount { get; }

        int ColumnCount { get; }


        bool IsZero();

        bool IsIdentity();

        bool IsDiagonal();

        bool IsSymmetric();

        bool IsOrthogonal();

        bool IsInvertable();

        bool IsFullMatrix();

        bool IsSparseMatrix();

        bool IsSquare();

        bool IsRowVector();

        bool IsColumnVector();


        MathematicaScalar this[int row, int column] { get; }

        ISymbolicVector GetRow(int row);

        ISymbolicVector GetColumn(int column);

        ISymbolicVector GetDiagonal();


        ISymbolicVector Times(ISymbolicVector v);

        ISymbolicMatrix Transpose();

        ISymbolicMatrix Inverse();

        ISymbolicMatrix InverseTranspose();


        MathematicaMatrix ToMathematicaMatrix();

        MathematicaMatrix ToMathematicaFullMatrix();

        MathematicaMatrix ToMathematicaSparseMatrix();
    }
}
