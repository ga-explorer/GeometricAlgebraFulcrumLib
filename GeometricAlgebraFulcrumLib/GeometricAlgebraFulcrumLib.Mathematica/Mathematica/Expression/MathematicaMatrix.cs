using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Mathematica.Mathematica.Expression;

public class MathematicaMatrix : MathematicaExpression, ISymbolicMatrix
{
    public enum EigenVectorsSpecs
    {
        InMatrixRows = 0,
        InMatrixColumns = 1,
        OrthogonalInMatrixRows = 2,
        OrthogonalInMatrixColumns = 3
    }


    public static MathematicaMatrix CreateFullMatrix(MathematicaInterface parentCas, MathematicaScalar[,] matrix)
    {
        var rows = new object[matrix.GetUpperBound(0) + 1];

        for (var i = 0; i <= matrix.GetUpperBound(0); i++)
        {
            var items = new object[matrix.GetUpperBound(1) + 1];

            for (var j = 0; j <= matrix.GetUpperBound(1); j++)
                items[j] = matrix[i, j]?.Expression ?? Expr.INT_ZERO;

            rows[i] = Mfs.List[items];
        }

        var mathExpr = parentCas[Mfs.List[rows]];

        return new MathematicaMatrix(parentCas, mathExpr);
    }

    public static MathematicaMatrix CreateFullMatrix(MathematicaInterface parentCas, Expr[,] matrix)
    {
        var rows = new object[matrix.GetUpperBound(0) + 1];

        for (var i = 0; i <= matrix.GetUpperBound(0); i++)
        {
            var items = new object[matrix.GetUpperBound(1) + 1];

            for (var j = 0; j <= matrix.GetUpperBound(1); j++)
                items[j] = matrix[i, j] ?? Expr.INT_ZERO;

            rows[i] = Mfs.List[items];
        }

        var mathExpr = parentCas[Mfs.List[rows]];

        return new MathematicaMatrix(parentCas, mathExpr);
    }

    public static MathematicaMatrix CreateFullMatrixFromColumns(MathematicaInterface parentCas, IEnumerable<Expr[]> matrixColumns)
    {
        var exprList = matrixColumns
            .Select(column => Mfs.List[column.Cast<object>().ToArray()])
            .ToList();

        var mathExpr = parentCas[Mfs.Transpose[Mfs.List[exprList]]];

        return new MathematicaMatrix(parentCas, mathExpr);
    }

    public static MathematicaMatrix CreateIdentity(MathematicaInterface parentCas, int size, bool asFullMatrix = true)
    {
        var mathExpr = asFullMatrix 
            ? parentCas[Mfs.IdentityMatrix[size.ToExpr()]] 
            : parentCas[Mfs.SparseArray[Mfs.IdentityMatrix[size.ToExpr()]]];

        return new MathematicaMatrix(parentCas, mathExpr);
    }

    public static MathematicaMatrix CreateDiagonal(ISymbolicVector vector, bool asFullMatrix = true)
    {
        var mathExpr = asFullMatrix 
            ? vector.CasInterface[Mfs.DiagonalMatrix[vector.ToMathematicaVector().Expression]] 
            : vector.CasInterface[Mfs.SparseArray[Mfs.DiagonalMatrix[vector.ToMathematicaVector().Expression]]];

        return new MathematicaMatrix(vector.CasInterface, mathExpr);
    }

    public static MathematicaMatrix CreateFullDiagonalMatrix(MathematicaInterface parentCas, params MathematicaScalar[] scalarsList)
    {
        var vector = MathematicaVector.CreateFullVector(parentCas, scalarsList);

        return CreateDiagonal(vector);
    }

    public static MathematicaMatrix CreateFullDiagonalMatrix(MathematicaInterface parentCas, IEnumerable<MathematicaScalar> scalarsList)
    {
        var vector = MathematicaVector.CreateFullVector(parentCas, scalarsList);

        return CreateDiagonal(vector);
    }

    public static MathematicaMatrix CreateRowVector(ISymbolicVector vector, bool asFullMatrix = true)
    {
        var mathExpr = asFullMatrix 
            ? vector.CasInterface[Mfs.List[vector.ToMathematicaFullVector().Expression]] 
            : vector.CasInterface[Mfs.SparseArray[Mfs.List[vector.ToMathematicaFullVector().Expression]]];

        return new MathematicaMatrix(vector.CasInterface, mathExpr);
    }

    public static MathematicaMatrix CreateColumnVector(ISymbolicVector vector, bool asFullMatrix = true)
    {
        var mathExpr = 
            asFullMatrix 
                ? vector.CasInterface[Mfs.Transpose[Mfs.List[vector.ToMathematicaFullVector().Expression]]] 
                : vector.CasInterface[Mfs.Transpose[Mfs.SparseArray[Mfs.List[vector.ToMathematicaFullVector().Expression]]]];

        return new MathematicaMatrix(vector.CasInterface, mathExpr);
    }

    public static MathematicaMatrix CreateConstant(MathematicaScalar scalar, int rows, int columns)
    {
        var mathExpr = scalar.CasInterface[Mfs.ConstantArray[scalar.Expression, Mfs.List[rows.ToExpr(), columns.ToExpr()]]];

        return new MathematicaMatrix(scalar.CasInterface, mathExpr);
    }

    public new static MathematicaMatrix Create(MathematicaInterface parentCas, string mathExprText)
    {
        return new MathematicaMatrix(parentCas, parentCas[mathExprText]);
    }

    public new static MathematicaMatrix Create(MathematicaInterface parentCas, Expr mathExpr)
    {
        return new MathematicaMatrix(parentCas, mathExpr);
    }


    public static MathematicaMatrix operator -(MathematicaMatrix matrix1)
    {
        var e = matrix1.CasInterface[Mfs.Minus[matrix1.Expression]];

        return new MathematicaMatrix(matrix1.CasInterface, e);
    }

    public static MathematicaMatrix operator +(MathematicaMatrix matrix1, MathematicaMatrix matrix2)
    {
        var e = matrix1.CasInterface[Mfs.Plus[matrix1.Expression, matrix2.Expression]];

        return new MathematicaMatrix(matrix1.CasInterface, e);
    }

    public static MathematicaMatrix operator -(MathematicaMatrix matrix1, MathematicaMatrix matrix2)
    {
        var e = matrix1.CasInterface[Mfs.Subtract[matrix1.Expression, matrix2.Expression]];

        return new MathematicaMatrix(matrix1.CasInterface, e);
    }

    public static MathematicaMatrix operator *(MathematicaMatrix matrix1, MathematicaMatrix matrix2)
    {
        var e = matrix1.CasInterface[Mfs.Dot[matrix1.Expression, matrix2.Expression]];

        return new MathematicaMatrix(matrix1.CasInterface, e);
    }

    public static MathematicaMatrix operator *(MathematicaMatrix matrix1, MathematicaScalar scalar2)
    {
        var e = matrix1.CasInterface[Mfs.Times[matrix1.Expression, scalar2.Expression]];

        return new MathematicaMatrix(matrix1.CasInterface, e);
    }

    public static MathematicaMatrix operator *(MathematicaScalar scalar1, MathematicaMatrix matrix2)
    {
        var e = matrix2.CasInterface[Mfs.Times[scalar1.Expression, matrix2.Expression]];

        return new MathematicaMatrix(matrix2.CasInterface, e);
    }

    public static MathematicaMatrix operator /(MathematicaMatrix matrix1, MathematicaScalar scalar2)
    {
        var e = matrix1.CasInterface[Mfs.Divide[matrix1.Expression, scalar2.Expression]];

        return new MathematicaMatrix(matrix1.CasInterface, e);
    }


    public int RowCount { get; }

    public int ColumnCount { get; }

    public MathematicaScalar this[int row, int column] => 
        MathematicaScalar.Create(
            CasInterface, 
            IsFullMatrix() 
                ? Expression.Args[row].Args[column] 
                : CasInterface[Mfs.Part[Expression, (row + 1).ToExpr(), (column + 1).ToExpr()]]
        );


    private MathematicaMatrix(MathematicaInterface parentCas, Expr mathExpr)
        : base(parentCas, mathExpr)
    {
        var dimensions = CasInterface[Mfs.Dimensions[mathExpr]];

        RowCount = int.Parse(dimensions.Args[0].ToString());

        ColumnCount = int.Parse(dimensions.Args[1].ToString());
    }


    public ISymbolicMatrix Transpose()
    {
        var e = CasInterface[Mfs.Transpose[Expression]];

        return Create(CasInterface, e);
    }

    public ISymbolicMatrix Inverse()
    {
        var e = CasInterface[Mfs.Inverse[Expression]];

        return Create(CasInterface, e);
    }

    public ISymbolicMatrix InverseTranspose()
    {
        var e = CasInterface[Mfs.Transpose[Mfs.Inverse[Expression]]];

        return Create(CasInterface, e);
    }

    public ISymbolicVector Times(ISymbolicVector v)
    {
        var e = CasInterface[Mfs.Dot[Expression, v.ToMathematicaVector().Expression]];

        return MathematicaVector.Create(CasInterface, e);
    }

    public ISymbolicVector GetRow(int index)
    {
        var e = CasInterface[Mfs.Part[Expression, (index + 1).ToExpr(), OptionSymbols.All]];

        return MathematicaVector.Create(CasInterface, e);
    }

    public ISymbolicVector GetColumn(int index)
    {
        var e = CasInterface[Mfs.Part[Expression, OptionSymbols.All, (index + 1).ToExpr()]];

        return MathematicaVector.Create(CasInterface, e);
    }

    public ISymbolicVector GetDiagonal()
    {
        var e = CasInterface[Mfs.Diagonal[Expression]];

        return MathematicaVector.Create(CasInterface, e);
    }

    //public MathematicaMatrix SetRow(int index, VectorBase vector);

    //public MathematicaMatrix SetColumn(int index, VectorBase vector);

    //public MathematicaMatrix SetDiagonal(VectorBase vector);

    //public MathematicaMatrix AppendRow(VectorBase vector);

    //public MathematicaMatrix AppendColumn(VectorBase vector);

    public MathematicaVector EigenValues_InVector()
    {
        return MathematicaVector.Create(CasInterface, CasInterface[Mfs.Eigenvalues[Expression]]);
    }

    public MathematicaMatrix EigenValues_InDiagonalMatrix()
    {
        return Create(CasInterface, CasInterface[Mfs.DiagonalMatrix[Mfs.Eigenvalues[Expression]]]);
    }

    public MathematicaMatrix EigenVectors(EigenVectorsSpecs specs)
    {
        Expr vectorsExpr;

        switch (specs)
        {
            case EigenVectorsSpecs.InMatrixRows:
                vectorsExpr = Mfs.Eigenvectors[Expression];
                break;

            case EigenVectorsSpecs.InMatrixColumns:
                vectorsExpr = Mfs.Transpose[Mfs.Eigenvectors[Expression]];
                break;

            case EigenVectorsSpecs.OrthogonalInMatrixRows:
                vectorsExpr = Mfs.Orthogonalize[Mfs.Eigenvectors[Expression]];
                break;

            default:
                vectorsExpr = Mfs.Transpose[Mfs.Orthogonalize[Mfs.Eigenvectors[Expression]]];
                break;
        }

        return new MathematicaMatrix(CasInterface, CasInterface[vectorsExpr]);
    }

    public bool EigenSystem(EigenVectorsSpecs specs, out MathematicaVector values, out MathematicaMatrix vectors)
    {
        var sysExpr = CasInterface[Mfs.Eigensystem[Expression]];

        values = MathematicaVector.Create(CasInterface, sysExpr.Args[0]);

        Expr vectorsExpr;

        switch (specs)
        {
            case EigenVectorsSpecs.InMatrixRows:
                vectorsExpr = sysExpr.Args[1];
                break;

            case EigenVectorsSpecs.InMatrixColumns:
                vectorsExpr = CasInterface[Mfs.Transpose[sysExpr.Args[1]]];
                break;

            case EigenVectorsSpecs.OrthogonalInMatrixRows:
                vectorsExpr = CasInterface[Mfs.Orthogonalize[sysExpr.Args[1]]];
                break;

            default:
                vectorsExpr = CasInterface[Mfs.Transpose[Mfs.Orthogonalize[sysExpr.Args[1]]]];
                break;
        }

        vectors = Create(CasInterface, vectorsExpr);

        //TODO: Test if the eigen system is OK
        return true;
    }

    public bool EigenSystem(EigenVectorsSpecs specs, out MathematicaMatrix values, out MathematicaMatrix vectors)
    {
        var sysExpr = CasInterface[Mfs.Eigensystem[Expression]];

        values = Create(CasInterface, CasInterface[Mfs.DiagonalMatrix[sysExpr.Args[0]]]);

        Expr vectorsExpr;

        switch (specs)
        {
            case EigenVectorsSpecs.InMatrixRows:
                vectorsExpr = Mfs.Eigenvectors[sysExpr.Args[1]];
                break;

            case EigenVectorsSpecs.InMatrixColumns:
                vectorsExpr = CasInterface[Mfs.Transpose[Mfs.Eigenvectors[sysExpr.Args[1]]]];
                break;

            case EigenVectorsSpecs.OrthogonalInMatrixRows:
                vectorsExpr = CasInterface[Mfs.Orthogonalize[Mfs.Eigenvectors[sysExpr.Args[1]]]];
                break;

            default:
                vectorsExpr = CasInterface[Mfs.Transpose[Mfs.Orthogonalize[Mfs.Eigenvectors[sysExpr.Args[1]]]]];
                break;

        }

        vectors = Create(CasInterface, vectorsExpr);

        //TODO: Test if the eigen system is OK
        return true;
    }


    public MathematicaMatrix ToMathematicaMatrix()
    {
        return this;
    }

    public MathematicaMatrix ToMathematicaFullMatrix()
    {
        if (IsFullMatrix())
            return this;

        var e = CasInterface[Mfs.Normal[Expression]];

        return new MathematicaMatrix(CasInterface, e);
    }

    public MathematicaMatrix ToMathematicaSparseMatrix()
    {
        if (IsSparseMatrix())
            return this;

        var e = CasInterface[Mfs.SparseArray[Expression]];

        return new MathematicaMatrix(CasInterface, e);
    }


    public bool IsSquare()
    {
        return (RowCount == ColumnCount);
    }

    public bool IsRowVector()
    {
        return (RowCount == 1);
    }

    public bool IsColumnVector()
    {
        return (ColumnCount == 1);
    }

    public bool IsZero()
    {
        var e = Mfs.Apply[Mfs.And.MathExpr, Mfs.Flatten[Mfs.PossibleZeroQ[Expression]]];

        return CasInterface.EvalTrueQ(e);
    }

    public bool IsIdentity()
    {
        if (!IsSquare())
            return false;

        var e = Mfs.Equal[Expression, Mfs.IdentityMatrix[Mfs.Dimensions[Expression]]];

        return CasInterface.EvalTrueQ(e);
    }

    public bool IsDiagonal()
    {
        if (!IsSquare())
            return false;

        var e = Mfs.Equal[Expression, Mfs.DiagonalMatrix[Mfs.Diagonal[Expression]]];

        return CasInterface.EvalTrueQ(e);
    }

    public bool IsSymmetric()
    {
        return IsSquare() && CasInterface.EvalTrueQ(Mfs.SymmetricMatrixQ[Expression]);
    }

    public bool IsOrthogonal()
    {
        if (!IsSquare())
            return false;

        var e = Mfs.Equal[Mfs.Dot[Expression, Mfs.Transpose[Expression]], Mfs.IdentityMatrix[Mfs.Dimensions[Expression]]];

        return CasInterface.EvalTrueQ(e);
    }

    public bool IsInvertable()
    {
        return IsSquare() && CasInterface.EvalFalseQ(Mfs.PossibleZeroQ[Mfs.Det[Expression]]);
    }

    public bool IsFullMatrix()
    {
        return Expression.ListQ();
    }

    public bool IsSparseMatrix()
    {
        return Expression.Head.ToString() == Mfs.SparseArray.ToString();
    }
}