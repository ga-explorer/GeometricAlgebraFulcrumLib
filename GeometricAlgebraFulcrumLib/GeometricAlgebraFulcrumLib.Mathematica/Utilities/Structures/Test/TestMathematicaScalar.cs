using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures.Expression;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures.ExprFactory;

namespace GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures.Test;

public static class TestMathematicaScalar
{
    public static MathematicaInterface Cas => TestUtils.Cas;

    public static void ConstructionTest()
    {
        TestUtils.AddTestStartingMessage("MathematicaScalar Creation Test Started.");

        var s = MathematicaScalar.CreateRational(Cas, 3, 5);
        TestUtils.AddTest("Try create scalar from rational 3/5 ... ", s);

        s = MathematicaScalar.CreateSymbol(Cas, "x3");
        TestUtils.AddTest("Try create scalar from symbol name ... ", s);

        s = MathematicaScalar.Create(Cas, 5);
        TestUtils.AddTest("Try create scalar from integer 5 ... ", s);

        s = MathematicaScalar.Create(Cas, 5.0f);
        TestUtils.AddTest("Try create scalar from float 5.0f ... ", s);

        s = MathematicaScalar.Create(Cas, 5.0d);
        TestUtils.AddTest("Try create scalar from double 5.0d ... ", s);

        var e = Mfs.Plus["v1".ToSymbolExpr(), Mfs.Power[5.ToExpr(), "n".ToSymbolExpr()]];
        s = MathematicaScalar.Create(Cas, e);
        TestUtils.AddTest("Try create scalar from expression object ... ", s);

        s = MathematicaScalar.Create(Cas, @"Pi + 5 / 3");
        TestUtils.AddTest("Try create scalar from expression text \"Pi + 5 / 3\" ... ", s);

        TestUtils.AddTestCompletionMessage("MathematicaScalar Creation Test Completed.");
    }

    public static void BasicOpsTest()
    {
        //Expr e;
        var s1 = MathematicaScalar.CreateRational(Cas, -3, 5);
        var s2 = MathematicaScalar.CreateRational(Cas, 9, 5);
        var s3 = MathematicaScalar.Create(Cas, "Pi");

        TestUtils.AddTestStartingMessage("MathematicaScalar Basic Operations Test Started.");

        TestUtils.AddTest("Try negate rational -3/5 ... ", -s1);
        TestUtils.AddTest("Try add rationals -3/5 and 9/5 ... ", s1 + s2);
        TestUtils.AddTest("Try subtract rationals -3/5 and 9/5 ... ", s1 - s2);
        TestUtils.AddTest("Try multiply rationals -3/5 and 9/5 ... ", s1 * s2);
        TestUtils.AddTest("Try divide rationals -3/5 and 9/5 ... ", s1 / s2);
        TestUtils.AddTest("Try rise rational 9/5 to the power of Pi ... ", s2 ^ s3);

        TestUtils.AddTest("Try apply Abs to rational -3/5 ... ", s1.Abs());
        TestUtils.AddTest("Try apply Sqrt to rational -3/5 ... ", s1.Sqrt());
        TestUtils.AddTest("Try apply Sin to rational 9/5 ... ", s2.Sin());
        TestUtils.AddTest("Try apply Cos to rational 9/5 ... ", s2.Cos());
        TestUtils.AddTest("Try apply Tan to rational 9/5 ... ", s2.Tan());
        TestUtils.AddTest("Try apply Sinh to rational 9/5 ... ", s2.Sinh());
        TestUtils.AddTest("Try apply Cosh to rational 9/5 ... ", s2.Cosh());
        TestUtils.AddTest("Try apply Tanh to rational 9/5 ... ", s2.Tanh());
        TestUtils.AddTest("Try apply Log to rational 9/5 ... ", s2.Log());
        TestUtils.AddTest("Try apply Log10 to rational 9/5 ... ", s2.Log10());
        TestUtils.AddTest("Try apply Log2 to rational 9/5 ... ", s2.Log2());
        TestUtils.AddTest("Try apply Exp to rational 9/5 ... ", s2.Exp());

        var s = MathematicaScalar.Create(Cas, @"3 x ^ 2 + 2 Pi ^ x - Sin[x]");
        var d = MathematicaScalar.CreateSymbol(Cas, "x");
        TestUtils.AddTest("Try differentiate 3 x ^ 2 + 2 Pi ^ x - Sin[x] w.r.t. x ... ", s.DiffBy(d));

        TestUtils.AddTestCompletionMessage("MathematicaScalar Basic Operations Test Completed.");
    }

    public static void IsOpsTest()
    {
        TestUtils.AddTestStartingMessage("MathematicaScalar 'Is' Operations Test Started.");

        var s1 = MathematicaScalar.Create(Cas, 0.0f);
        TestUtils.AddTest("Try apply IsPossibleZero() to 0.0f ... ", s1.IsPossibleZero());

        s1 = MathematicaScalar.CreateSymbol(Cas, "Pi");
        TestUtils.AddTest("Try apply IsPossibleZero() to Pi ... ", s1.IsPossibleZero());

        s1 = MathematicaScalar.Create(Cas, @"x - x");
        TestUtils.AddTest("Try apply IsPossibleZero() to (x - x) ... ", s1.IsPossibleZero());

        s1 = MathematicaScalar.Create(Cas, @"3 x ^ 2 + 2 Pi ^ x - Sin[x]");
        TestUtils.AddTest("Try apply IsPossibleZero() to 3 x ^ 2 + 2 Pi ^ x - Sin[x] ... ", s1.IsPossibleZero());


        s1 = MathematicaScalar.Create(Cas, 0.0f);
        TestUtils.AddTest("Try apply IsEqualZero() to 0.0f ... ", s1.IsEqualZero());

        s1 = MathematicaScalar.CreateSymbol(Cas, "Pi");
        TestUtils.AddTest("Try apply IsEqualZero() to Pi ... ", s1.IsEqualZero());

        s1 = MathematicaScalar.Create(Cas, @"x - x");
        TestUtils.AddTest("Try apply IsEqualZero() to (x - x) ... ", s1.IsEqualZero());

        s1 = MathematicaScalar.Create(Cas, @"3 x ^ 2 + 2 Pi ^ x - Sin[x]");
        TestUtils.AddTest("Try apply IsEqualZero() to 3 x ^ 2 + 2 Pi ^ x - Sin[x] ... ", s1.IsEqualZero());


        s1 = MathematicaScalar.Create(Cas, 5.0f);
        var s2 = MathematicaScalar.Create(Cas, 5);
        TestUtils.AddTest("Try apply IsPossibleScalar() to 5.0f with 5 ... ", s1.IsPossibleScalar(s2));

        s1 = MathematicaScalar.Create(Cas, 5.1f);
        s2 = MathematicaScalar.Create(Cas, 5);
        TestUtils.AddTest("Try apply IsPossibleScalar() to 5.1f with 5 ... ", s1.IsPossibleScalar(s2));

        s1 = MathematicaScalar.Create(Cas, 1.0f);
        s2 = MathematicaScalar.Create(Cas, "Sin[Pi]");
        TestUtils.AddTest("Try apply IsPossibleScalar() to 1.0f with Sin[Pi] ... ", s1.IsPossibleScalar(s2));

        s1 = MathematicaScalar.Create(Cas, -1.0f);
        s2 = MathematicaScalar.Create(Cas, "Cos[Pi]");
        TestUtils.AddTest("Try apply IsPossibleScalar() to -1.0f with Cos[Pi] ... ", s1.IsPossibleScalar(s2));


        s1 = MathematicaScalar.Create(Cas, 5.0f);
        s2 = MathematicaScalar.Create(Cas, 5);
        TestUtils.AddTest("Try apply IsEqualScalar() to 5.0f with 5 ... ", s1.IsEqualScalar(s2));

        s1 = MathematicaScalar.Create(Cas, 5.1f);
        s2 = MathematicaScalar.Create(Cas, 5);
        TestUtils.AddTest("Try apply IsEqualScalar() to 5.1f with 5 ... ", s1.IsEqualScalar(s2));

        s1 = MathematicaScalar.Create(Cas, 1.0f);
        s2 = MathematicaScalar.Create(Cas, "Sin[Pi]");
        TestUtils.AddTest("Try apply IsEqualScalar() to 1.0f with Sin[Pi] ... ", s1.IsEqualScalar(s2));

        s1 = MathematicaScalar.Create(Cas, -1.0f);
        s2 = MathematicaScalar.Create(Cas, "Cos[Pi]");
        TestUtils.AddTest("Try apply IsEqualScalar() to -1.0f with Cos[Pi] ... ", s1.IsEqualScalar(s2));


        s1 = MathematicaScalar.Create(Cas, -1.0f);
        TestUtils.AddTest("Try apply IsConstant() to -1.0f ... ", s1.IsConstant());

        s1 = MathematicaScalar.Create(Cas, "Sin[Pi]");
        TestUtils.AddTest("Try apply IsConstant() to Sin[Pi] ... ", s1.IsConstant());

        s1 = MathematicaScalar.Create(Cas, "Sin[x]");
        TestUtils.AddTest("Try apply IsConstant() to Sin[x] ... ", s1.IsConstant());

        s1 = MathematicaScalar.Create(Cas, "Sin[x - x]");
        TestUtils.AddTest("Try apply IsConstant() to Sin[x - x] ... ", s1.IsConstant());


        s1 = MathematicaScalar.Create(Cas, 0.0f);
        TestUtils.AddTest("Try apply IsNonZeroRealConstant() to 0.0f ... ", s1.IsNonZeroRealConstant());

        s1 = MathematicaScalar.Create(Cas, -1.0f);
        TestUtils.AddTest("Try apply IsNonZeroRealConstant() to -1.0f ... ", s1.IsNonZeroRealConstant());

        s1 = MathematicaScalar.Create(Cas, "Sin[Pi]");
        TestUtils.AddTest("Try apply IsNonZeroRealConstant() to Sin[Pi] ... ", s1.IsNonZeroRealConstant());

        s1 = MathematicaScalar.Create(Cas, "Sin[x]");
        TestUtils.AddTest("Try apply IsNonZeroRealConstant() to Sin[x] ... ", s1.IsNonZeroRealConstant());

        s1 = MathematicaScalar.Create(Cas, "Sin[x - x]");
        TestUtils.AddTest("Try apply IsNonZeroRealConstant() to Sin[x - x] ... ", s1.IsNonZeroRealConstant());

        s1 = MathematicaScalar.Create(Cas, "3 + i * Sin[Pi / 2]");
        TestUtils.AddTest("Try apply IsNonZeroRealConstant() to 3 + i * Sin[Pi / 2] ... ", s1.IsNonZeroRealConstant());


        TestUtils.AddTestCompletionMessage("MathematicaScalar 'Is' Operations Test Completed.");
    }
}