using GeometricAlgebraFulcrumLib.Symbolic.Mathematica.Expression;
using GeometricAlgebraFulcrumLib.Symbolic.Mathematica.ExprFactory;

namespace GeometricAlgebraFulcrumLib.Symbolic.Mathematica.Test
{
    public static class TestMathematicaVector
    {
        public static MathematicaInterface Cas => TestUtils.Cas;

        public static void ConstructionTest()
        {
            TestUtils.AddTestStartingMessage("MathematicaVector Creation Test Started.");

            var s = MathematicaVector.CreateZero(Cas, 3);
            TestUtils.AddTest("Try create 3D zero vector ... ", s);

            var scalarsList = new[] { Cas.Constants.MinusOne, Cas.Constants.Pi, Cas.Constants.Zero };
            s = MathematicaVector.CreateFullVector(Cas, scalarsList);
            TestUtils.AddTest("Try create full vector from list of scalars -1, Pi, 0 ... ", s);

            s = MathematicaVector.CreateFullVector(Cas, Cas.Constants.MinusOne, Cas.Constants.Pi, Cas.Constants.Zero);
            TestUtils.AddTest("Try create full vector from param array of scalars -1, Pi, 0 ... ", s);

            s = MathematicaVector.Create(Cas.Constants.TwoPi, 3);
            TestUtils.AddTest("Try create 3D vector with constant entries of 2 Pi ... ", s);

            var e = Cas[Mfs.List["v1".ToSymbolExpr(), Mfs.Power[5.ToExpr(), 2.ToExpr()], "n".ToSymbolExpr()]];
            s = MathematicaVector.Create(Cas, e);
            TestUtils.AddTest("Try create vector from expression object ... ", s);

            s = MathematicaVector.Create(Cas, @"List[Pi, 5 / 3, -2.7]");
            TestUtils.AddTest("Try create vector from expression text \"List[Pi, 5 / 3, -2.7]\" ... ", s);

            TestUtils.AddTestCompletionMessage("MathematicaVector Creation Test Completed.");
        }

        public static void BasicOpsTest()
        {
            var v1 = MathematicaVector.CreateFullVector(Cas, Cas.Constants.Zero, Cas.Constants.One, Cas.Constants.TwoPi);
            var v2 = MathematicaVector.Create(Cas, @"{-1, x, Sin[t]}");
            var sv3 = MathematicaVector.Create(Cas, "SparseArray[{Rule[1, Pi], Rule[5, -1]}]");
            var s = MathematicaScalar.Create(Cas, "x");

            TestUtils.AddTestStartingMessage("MathematicaVector Basic Operations Test Started.");

            TestUtils.AddTest("Try get size of full vector {-1, x, Sin[t]} ... ", v2.Size);
            TestUtils.AddTest("Try get 1st component of full vector {-1, x, Sin[t]} ... ", v2[0]);
            TestUtils.AddTest("Try get 2nd component of full vector {-1, x, Sin[t]} ... ", v2[1]);
            TestUtils.AddTest("Try get 3rd component of full vector {-1, x, Sin[t]} ... ", v2[2]);

            TestUtils.AddTest("Try get size of sparse vector {Pi, 0, 0, 0, -1} ... ", sv3.Size);
            TestUtils.AddTest("Try get 1st component of sparse vector {Pi, 0, 0, 0, -1} ... ", sv3[0]);
            TestUtils.AddTest("Try get 3rd component of sparse vector {Pi, 0, 0, 0, -1} ... ", sv3[2]);
            TestUtils.AddTest("Try get 5th component of sparse vector {Pi, 0, 0, 0, -1} ... ", sv3[4]);

            TestUtils.AddTest("Try list components of full vector {-1, x, Sin[t]}", "");
            var i = 0;
            foreach (var scalar in v2)
            {
                TestUtils.AddTest("   Component " + i + " ... ", scalar);
                i++;
            }

            TestUtils.AddTest("Try list components of sparse vector {Pi, 0, 0, 0, -1}", "");
            i = 0;
            foreach (var scalar in sv3)
            {
                TestUtils.AddTest("   Component " + i + " ... ", scalar);
                i++;
            }

            TestUtils.AddTest("Try negate vector {-1, x, Sin[t]} ... ", -v2);
            TestUtils.AddTest("Try add vectors {0, 1, 2 Pi} and {-1, x, Sin[t]} ... ", v1 + v2);
            TestUtils.AddTest("Try subtract vectors {0, 1, 2 Pi} and {-1, x, Sin[t]} ... ", v1 - v2);
            TestUtils.AddTest("Try find dot product of {0, 1, 2 Pi} and {-1, x, Sin[t]} ... ", v1 * v2);
            TestUtils.AddTest("Try find product of vector {-1, x, Sin[t]} with scalar x ... ", v2 * s);
            TestUtils.AddTest("Try find product of scalar x with vector {-1, x, Sin[t]} ... ", s * v2);
            TestUtils.AddTest("Try divide vector {-1, x, Sin[t]} by scalar x ... ", v2 / s);

            TestUtils.AddTest("Try apply Norm to vector {0, 1, 2 Pi} ... ", v1.Norm());
            TestUtils.AddTest("Try apply Norm2 to vector {0, 1, 2 Pi} ... ", v1.Norm2());

            var m = MathematicaMatrix.CreateFullDiagonalMatrix(Cas, Cas.Constants.One, Cas.Constants.Pi, Cas.Constants.MinusOne);
            TestUtils.AddTest("Try find product of vector {-1, x, Sin[t]} with matrix DiagonalMatrix[{1, Pi, -1}] ... ", v2.Times(m));

            TestUtils.AddTest("Try apply ToMathematicaVector() to full vector {0, 1, 2 Pi} ... ", v1.ToMathematicaVector());
            TestUtils.AddTest("Try apply ToMathematicaFullVector() to full vector {0, 1, 2 Pi} ... ", v1.ToMathematicaFullVector());
            TestUtils.AddTest("Try apply ToMathematicaSparseVector() to full vector {0, 1, 2 Pi} ... ", v1.ToMathematicaSparseVector());

            TestUtils.AddTest("Try apply ToMathematicaVector() to sparse vector {Pi, 0, 0, 0, -1} ... ", sv3.ToMathematicaVector());
            TestUtils.AddTest("Try apply ToMathematicaFullVector() to sparse vector {Pi, 0, 0, 0, -1} ... ", sv3.ToMathematicaFullVector());
            TestUtils.AddTest("Try apply ToMathematicaSparseVector() to sparse vector {Pi, 0, 0, 0, -1} ... ", sv3.ToMathematicaSparseVector());

            TestUtils.AddTestCompletionMessage("MathematicaVector Basic Operations Test Completed.");
        }

        public static void IsOpsTest()
        {
            var v1 = MathematicaVector.CreateFullVector(Cas, Cas.Constants.Zero, Cas.Constants.One, Cas.Constants.TwoPi);
            var v2 = MathematicaVector.Create(Cas, "SparseArray[{Rule[1, Pi], Rule[5, -1]}]");

            TestUtils.AddTestStartingMessage("MathematicaVector 'Is' Operations Test Started.");

            TestUtils.AddTest("Try apply IsFullVector() to full vector {0, 1, 2 Pi} ... ", v1.IsFullVector());

            TestUtils.AddTest("Try apply IsSparseVector() to full vector {0, 1, 2 Pi} ... ", v1.IsSparseVector());

            TestUtils.AddTest("Try apply IsFullVector() to sparse vector {Pi, 0, 0, 0, -1} ... ", v2.IsFullVector());

            TestUtils.AddTest("Try apply IsSparseVector() to sparse vector {Pi, 0, 0, 0, -1} ... ", v2.IsSparseVector());

            TestUtils.AddTestCompletionMessage("MathematicaVector 'Is' Operations Test Completed.");
        }
    }
}
