using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.Expression;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory;

namespace GeometricAlgebraFulcrumLib.Mathematica.Mathematica.Test
{
    public static class TestMathematicaMatrix
    {
        public static MathematicaInterface Cas => TestUtils.Cas;

        public static void ConstructionTest()
        {
            TestUtils.AddTestStartingMessage("MathematicaMatrix Creation Test Started.");

            var scalarsArray = new MathematicaScalar[2, 2];
            scalarsArray[0, 0] = MathematicaScalar.Create(Cas, 2);
            scalarsArray[0, 1] = MathematicaScalar.Create(Cas, -2);
            scalarsArray[1, 0] = MathematicaScalar.Create(Cas, 3);
            scalarsArray[1, 1] = MathematicaScalar.Create(Cas, 1);

            var s = MathematicaMatrix.CreateFullMatrix(Cas, scalarsArray);
            TestUtils.AddTest("Try create full matrix from array of scalars ... ", s);

            s = MathematicaMatrix.CreateIdentity(Cas, 3);
            TestUtils.AddTest("Try create full 3x3 identity matrix ... ", s);

            s = MathematicaMatrix.CreateIdentity(Cas, 3, false);
            TestUtils.AddTest("Try create sparse 3x3 identity matrix ... ", s);

            var v = MathematicaVector.CreateFullVector(Cas, Cas.Constants.Zero, Cas.Constants.TwoPi, Cas.Constants.One);
            s = MathematicaMatrix.CreateDiagonal(v);
            TestUtils.AddTest("Try create full 3x3 diagonal matrix from vector {0, 2 Pi, 1} ... ", s);

            s = MathematicaMatrix.CreateDiagonal(v, false);
            TestUtils.AddTest("Try create sparse 3x3 diagonal matrix from vector {0, 2 Pi, 1} ... ", s);

            s = MathematicaMatrix.CreateRowVector(v);
            TestUtils.AddTest("Try create full row vector matrix from vector {0, 2 Pi, 1} ... ", s);

            s = MathematicaMatrix.CreateRowVector(v, false);
            TestUtils.AddTest("Try create sparse row vector matrix from vector {0, 2 Pi, 1} ... ", s);

            s = MathematicaMatrix.CreateColumnVector(v);
            TestUtils.AddTest("Try create full row vector matrix from vector {0, 2 Pi, 1} ... ", s);

            s = MathematicaMatrix.CreateColumnVector(v, false);
            TestUtils.AddTest("Try create sparse row vector matrix from vector {0, 2 Pi, 1} ... ", s);

            s = MathematicaMatrix.CreateConstant(Cas.Constants.Pi, 2, 3);
            TestUtils.AddTest("Try create 2x3 matrix with constant elements of Pi ... ", s);

            s = MathematicaMatrix.Create(Cas, @"{{1, x}, {2, y}, {3, z}}");
            TestUtils.AddTest("Try create matrix from expression text \"{{1, x}, {2, y}, {3, z}}\" ... ", s);

            TestUtils.AddTestCompletionMessage("MathematicaMatrix Creation Test Completed.");
        }

        public static void BasicOpsTest()
        {
            var s = MathematicaScalar.Create(Cas, -2);
            var v = MathematicaVector.CreateFullVector(Cas, Cas.Constants.One, Cas.Constants.Pi, Cas.Constants.MinusOne);
            var m1 = MathematicaMatrix.Create(Cas, "RandomInteger[{-5, 5}, {3, 4}]");
            var m2 = MathematicaMatrix.CreateFullDiagonalMatrix(Cas, Cas.Constants.One, Cas.Constants.Pi, Cas.Constants.MinusOne);
            var sm3 = MathematicaMatrix.Create(Cas, "SparseArray[{{1, 0, 0, 0}, {0, 2, 0, 4}, {0, 0, 0, 3}}]");
            var sm4 = m2.ToMathematicaSparseMatrix();

            TestUtils.AddTestStartingMessage("MathematicaMatrix Basic Operations Test Started.");

            TestUtils.AddTest("Try get rows of full 3x4 matrix ... ", m1.RowCount);
            TestUtils.AddTest("Try get columns of full 3x4 matrix ... ", m1.ColumnCount);

            TestUtils.AddTest("Try get rows of sparse 3x4 matrix ... ", sm3.RowCount);
            TestUtils.AddTest("Try get columns of sparse 3x4 matrix ... ", sm3.ColumnCount);

            for (var i = 0; i < m1.RowCount; i++)
                for (var j = 0; j < m1.ColumnCount; j++)
                    TestUtils.AddTest("Try get component (" + i + ", " + j + ") of full 3x4 matrix " + m1.ExpressionText + " ... ", m1[i, j]);

            for (var i = 0; i < sm3.RowCount; i++)
                for (var j = 0; j < sm3.ColumnCount; j++)
                    TestUtils.AddTest("Try get component (" + i + ", " + j + ") of sparse 3x4 matrix " + sm3.ToMathematicaFullMatrix().ExpressionText + " ... ", sm3[i, j]);

            for (var i = 0; i < m1.RowCount; i++)
                TestUtils.AddTest("Try get row " + i + " of full 3x4 matrix " + m1.ExpressionText + " ... ", m1.GetRow(i));

            for (var i = 0; i < m1.ColumnCount; i++)
                TestUtils.AddTest("Try get column " + i + " of full 3x4 matrix " + m1.ExpressionText + " ... ", m1.GetColumn(i));

            for (var i = 0; i < sm3.RowCount; i++)
                TestUtils.AddTest("Try get row " + i + " of sparse 3x4 matrix " + sm3.ToMathematicaFullMatrix().ExpressionText + " ... ", sm3.GetRow(i));

            for (var i = 0; i < sm3.ColumnCount; i++)
                TestUtils.AddTest("Try get column " + i + " of sparse 3x4 matrix " + sm3.ToMathematicaFullMatrix().ExpressionText + " ... ", sm3.GetColumn(i));

            TestUtils.AddTest("Try get diagonal of full matrix " + m2.ExpressionText + " ... ", m2.GetDiagonal());
            TestUtils.AddTest("Try get diagonal of sparse matrix " + sm4.ToMathematicaFullMatrix().ExpressionText + " ... ", sm4.GetDiagonal());
            TestUtils.AddTest("Try negate matrix " + m2.ExpressionText + " ... ", -m2);
            TestUtils.AddTest("Try add matrix " + m1.ExpressionText + " and " + sm3.ToMathematicaFullMatrix().ExpressionText + " ... ", m1 + sm3);
            TestUtils.AddTest("Try subtract matrix " + m1.ExpressionText + " and " + sm3.ToMathematicaFullMatrix().ExpressionText + " ... ", m1 - sm3);
            TestUtils.AddTest("Try multiply matrix " + m2.ExpressionText + " and " + m1.ToMathematicaFullMatrix().ExpressionText + " ... ", m2 * m1);
            TestUtils.AddTest("Try multiply matrix " + m2.ExpressionText + " and scalar -2 ... ", m2 * s);
            TestUtils.AddTest("Try multiply scalar -2 and matrix " + m2.ExpressionText + " ... ", s * m2);
            TestUtils.AddTest("Try divide matrix " + m2.ExpressionText + " by scalar -2 ... ", m2 / s);
            TestUtils.AddTest("Try multiply matrix " + m2.ExpressionText + " and vector " + v.ExpressionText + " ... ", m2.Times(v));
            TestUtils.AddTest("Try multiply vector " + v.ExpressionText + " and matrix " + m2.ExpressionText + " ... ", v.Times(m2));
            TestUtils.AddTest("Try transpose matrix " + m2.ExpressionText + " ... ", m2.Transpose());
            TestUtils.AddTest("Try inverse matrix " + m2.ExpressionText + " ... ", m2.Inverse());
            TestUtils.AddTest("Try inverse transpose matrix " + m2.ExpressionText + " ... ", m2.InverseTranspose());

            m2 = MathematicaMatrix.Create(Cas, "{{1, -1, 0}, {-1, 2, -1}, {0, -1, 1}}");
            TestUtils.AddTest("Try get eigen values (in a vector) of matrix " + m2.ExpressionText + " ... ", m2.EigenValues_InVector());
            TestUtils.AddTest("Try get eigen values (in a matrix) of matrix " + m2.ExpressionText + " ... ", m2.EigenValues_InDiagonalMatrix());
            TestUtils.AddTest("Try get eigen vectors (as rows of a matrix) of matrix " + m2.ExpressionText + " ... ", m2.EigenVectors(MathematicaMatrix.EigenVectorsSpecs.InMatrixRows));
            TestUtils.AddTest("Try get eigen vectors (as columns of a matrix) of matrix " + m2.ExpressionText + " ... ", m2.EigenVectors(MathematicaMatrix.EigenVectorsSpecs.InMatrixColumns));
            TestUtils.AddTest("Try get eigen vectors (as orthonormal rows of a matrix) of matrix " + m2.ExpressionText + " ... ", m2.EigenVectors(MathematicaMatrix.EigenVectorsSpecs.OrthogonalInMatrixRows));
            TestUtils.AddTest("Try get eigen vectors (as orthonormal columns of a matrix) of matrix " + m2.ExpressionText + " ... ", m2.EigenVectors(MathematicaMatrix.EigenVectorsSpecs.OrthogonalInMatrixColumns));

            m2.EigenSystem(MathematicaMatrix.EigenVectorsSpecs.InMatrixRows, out MathematicaVector eval1, out var evec);
            TestUtils.AddTest("Try get eigen system values (in a vector) of matrix " + m2.ExpressionText + " ... ", eval1);

            m2.EigenSystem(MathematicaMatrix.EigenVectorsSpecs.InMatrixRows, out MathematicaMatrix eval2, out evec);
            TestUtils.AddTest("Try get eigen system values (in a matrix) of matrix " + m2.ExpressionText + " ... ", eval2);

            m2.EigenSystem(MathematicaMatrix.EigenVectorsSpecs.InMatrixRows, out eval1, out evec);
            TestUtils.AddTest("Try get eigen system vectors (as rows of a matrix) of matrix " + m2.ExpressionText + " ... ", evec);

            m2.EigenSystem(MathematicaMatrix.EigenVectorsSpecs.InMatrixColumns, out eval1, out evec);
            TestUtils.AddTest("Try get eigen system vectors (as columns of a matrix) of matrix " + m2.ExpressionText + " ... ", evec);

            m2.EigenSystem(MathematicaMatrix.EigenVectorsSpecs.OrthogonalInMatrixRows, out eval1, out evec);
            TestUtils.AddTest("Try get eigen system vectors (as orthonormal rows of a matrix) of matrix " + m2.ExpressionText + " ... ", evec);

            m2.EigenSystem(MathematicaMatrix.EigenVectorsSpecs.OrthogonalInMatrixColumns, out eval1, out evec);
            TestUtils.AddTest("Try get eigen system vectors (as orthonormal columns of a matrix) of matrix " + m2.ExpressionText + " ... ", evec);

            TestUtils.AddTestCompletionMessage("MathematicaMatrix Basic Operations Test Completed.");
        }

        public static void IsOpsTest()
        {
            TestUtils.AddTestStartingMessage("MathematicaMatrix 'Is' Operations Test Started.");

            var m1 = MathematicaMatrix.CreateFullDiagonalMatrix(Cas, Cas.Constants.Zero, Cas.Constants.One, Cas.Constants.TwoPi);
            TestUtils.AddTest("Try apply IsZero() to full diagonal matrix {0, 1, 2 Pi} ... ", m1.IsZero());

            var sm1 = m1.ToMathematicaSparseMatrix();
            TestUtils.AddTest("Try apply IsZero() to sparse diagonal matrix {0, 1, 2 Pi} ... ", sm1.IsZero());

            m1 = MathematicaMatrix.CreateFullDiagonalMatrix(Cas, Cas.Constants.Zero, Cas.Constants.Zero, Cas.Constants.Zero);
            TestUtils.AddTest("Try apply IsZero() to full diagonal matrix {0, 0, 0} ... ", m1.IsZero());

            sm1 = m1.ToMathematicaSparseMatrix();
            TestUtils.AddTest("Try apply IsZero() to sparse diagonal matrix {0, 0, 0} ... ", sm1.IsZero());


            m1 = MathematicaMatrix.CreateFullDiagonalMatrix(Cas, Cas.Constants.Zero, Cas.Constants.One, Cas.Constants.TwoPi);
            TestUtils.AddTest("Try apply IsIdentity() to full diagonal matrix {0, 1, 2 Pi} ... ", m1.IsIdentity());

            sm1 = m1.ToMathematicaSparseMatrix();
            TestUtils.AddTest("Try apply IsIdentity() to sparse diagonal matrix {0, 1, 2 Pi} ... ", sm1.IsIdentity());

            m1 = MathematicaMatrix.CreateIdentity(Cas, 4);
            TestUtils.AddTest("Try apply IsIdentity() to full 4x4 Identity matrix ... ", m1.IsIdentity());

            sm1 = m1.ToMathematicaSparseMatrix();
            TestUtils.AddTest("Try apply IsIdentity() to sparse 4x4 Identity matrix ... ", sm1.IsIdentity());


            m1 = MathematicaMatrix.CreateFullDiagonalMatrix(Cas, Cas.Constants.Zero, Cas.Constants.One, Cas.Constants.TwoPi);
            TestUtils.AddTest("Try apply IsDiagonal() to full diagonal matrix {0, 1, 2 Pi} ... ", m1.IsDiagonal());

            sm1 = m1.ToMathematicaSparseMatrix();
            TestUtils.AddTest("Try apply IsDiagonal() to sparse diagonal matrix {0, 1, 2 Pi} ... ", sm1.IsDiagonal());

            m1 = MathematicaMatrix.CreateConstant(Cas.Constants.Pi, 5, 5);
            TestUtils.AddTest("Try apply IsDiagonal() to full 5x5 matrix with non-zero elements ... ", m1.IsDiagonal());

            sm1 = MathematicaMatrix.Create(Cas, "SparseArray[RandomInteger[1, {5, 5}]]");
            TestUtils.AddTest("Try apply IsDiagonal() to sparse 5x5 matrix " + sm1.ToMathematicaFullMatrix().ExpressionText + " ... ", sm1.IsDiagonal());


            m1 = MathematicaMatrix.Create(Cas, "RandomInteger[3, {5, 5}]");
            m1 = m1 * m1.Transpose().ToMathematicaMatrix();
            TestUtils.AddTest("Try apply IsSymmetric() to full 5x5 symmetric matrix " + m1.ExpressionText + " ... ", m1.IsSymmetric());

            TestUtils.AddTest("Try apply IsSymmetric() to sparse 5x5 matrix " + sm1.ToMathematicaFullMatrix().ExpressionText + " ... ", sm1.IsSymmetric());


            m1 = MathematicaMatrix.Create(Cas, "RandomInteger[{-10, 10}, {5, 5}]");
            TestUtils.AddTest("Try apply IsInvertable() to full 5x5 matrix " + m1.ExpressionText + " ... ", m1.IsInvertable());

            sm1 = MathematicaMatrix.Create(Cas, "SparseArray[RandomInteger[1, {5, 5}]]");
            TestUtils.AddTest("Try apply IsInvertable() to sparse 5x5 matrix " + sm1.ToMathematicaFullMatrix().ExpressionText + " ... ", sm1.IsInvertable());


            m1 = MathematicaMatrix.Create(Cas, "RandomInteger[3, {5, 5}]");
            m1 = m1 * m1.Transpose().ToMathematicaMatrix();
            TestUtils.AddTest("Try apply IsOrthogonal() to full 5x5 symmetric matrix " + m1.ExpressionText + " ... ", m1.IsOrthogonal());

            TestUtils.AddTest("Try apply IsOrthogonal() to sparse 5x5 matrix " + sm1.ToMathematicaFullMatrix().ExpressionText + " ... ", sm1.IsOrthogonal());

            m1 = MathematicaMatrix.Create(Cas, Cas[Mfs.Orthogonalize[m1.Expression]]);
            TestUtils.AddTest("Try apply IsOrthogonal() to full 5x5 symmetric matrix " + m1.ExpressionText + " ... ", m1.IsOrthogonal());

            sm1 = MathematicaMatrix.Create(Cas, Cas[Mfs.SparseArray[Mfs.Orthogonalize[sm1.Expression]]]);
            TestUtils.AddTest("Try apply IsOrthogonal() to sparse 5x5 matrix " + sm1.ToMathematicaFullMatrix().ExpressionText + " ... ", sm1.IsOrthogonal());


            TestUtils.AddTestCompletionMessage("MathematicaMatrix 'Is' Operations Test Completed.");
        }
    }
}
