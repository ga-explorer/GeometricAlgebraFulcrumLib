using AngouriMath;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Processors;
using GeometricAlgebraFulcrumLib.MetaProgramming.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;

//using GeometricAlgebraFulcrumLib.Text;

namespace GeometricAlgebraFulcrumLib.Samples.Symbolic.Algebra.AngouriMath;

public static class Sample3
{
    public static int VSpaceDimensions
        => 3;

    public static ulong GaSpaceDimensions
        => 1UL << VSpaceDimensions;

    public static IScalarProcessor<Entity> ScalarProcessor
        => ScalarProcessorOfAngouriMathEntity.Instance;

    public static RGaProcessor<Entity> GeometricProcessor { get; }
        = ScalarProcessor.CreateEuclideanRGaProcessor();

    public static TextComposerEntity TextComposer { get; }
        = TextComposerEntity.DefaultComposer;

    public static LaTeXAngouriMathComposer LaTeXComposer { get; }
        = LaTeXAngouriMathComposer.DefaultComposer;


    public static void Execute1()
    {
        //var e1 = GeometricProcessor.CreateBasisVector(0);
        var u = ScalarProcessor.CreateLinVector(VSpaceDimensions, i => $"u_{i + 1}");
        var v = ScalarProcessor.CreateLinVector(VSpaceDimensions, i => $"v_{i + 1}");

        //var unitLengthAssumption1 = Mfs.Equal[GeometricProcessor.ENormSquared(u), Expr.INT_ONE].Evaluate();
        //var unitLengthAssumption2 = Mfs.Equal[GeometricProcessor.ENormSquared(v), Expr.INT_ONE].Evaluate();
        //var unitLengthAssumption = Mfs.And[unitLengthAssumption1, unitLengthAssumption2];

        var rotationMap1 =
            ScalarProcessor.CreateVectorToBasisRotationMap(u, 0, VSpaceDimensions);

        var rotationMap2 =
            ScalarProcessor.CreateBasisToVectorRotationMap(0, v, VSpaceDimensions);

        var rotationMap =
            rotationMap2.Map(rotationMap1);


        var rotationMatrix1 =
            rotationMap1.ToArray(VSpaceDimensions);

        var rotationMatrix2 =
            rotationMap2.ToArray(VSpaceDimensions);

        var rotationMatrix =
            rotationMap.ToArray(VSpaceDimensions);

        var rotationMatrixDet1 =
            rotationMatrix1.MatrixDeterminant();

        var rotationMatrixDet2 =
            rotationMatrix2.MatrixDeterminant();

        var rotationMatrixDet =
            rotationMatrix.MatrixDeterminant();

        var v1 =
            rotationMap.Map(u);

        Console.WriteLine($@"rotor matrix 1 = {LaTeXComposer.GetArrayDisplayEquationText(rotationMatrix1)}");
        Console.WriteLine();

        Console.WriteLine($@"rotor matrix 2 = {LaTeXComposer.GetArrayDisplayEquationText(rotationMatrix2)}");
        Console.WriteLine();

        Console.WriteLine($@"rotor matrix = {LaTeXComposer.GetArrayDisplayEquationText(rotationMatrix)}");
        Console.WriteLine();

        Console.WriteLine($@"rotor matrix determinant 1 = {LaTeXComposer.GetScalarText(rotationMatrixDet1)}");
        Console.WriteLine();

        Console.WriteLine($@"rotor matrix determinant 2 = {LaTeXComposer.GetScalarText(rotationMatrixDet2)}");
        Console.WriteLine();

        Console.WriteLine($@"rotor matrix determinant = {LaTeXComposer.GetScalarText(rotationMatrixDet)}");
        Console.WriteLine();

        Console.WriteLine($@"mapped u = {LaTeXComposer.GetVectorText(v1)}");
        Console.WriteLine();
    }

    public static void Execute2()
    {
        var u = GeometricProcessor.Vector(VSpaceDimensions, i => $"u_{i + 1}");
        var v = GeometricProcessor.Vector(VSpaceDimensions, i => $"v_{i + 1}");

        //var unitLengthAssumption1 = Mfs.Equal[GeometricProcessor.ENormSquared(u), Expr.INT_ONE].Evaluate();
        //var unitLengthAssumption2 = Mfs.Equal[GeometricProcessor.ENormSquared(v), Expr.INT_ONE].Evaluate();
        //var unitLengthAssumption = Mfs.And[unitLengthAssumption1, unitLengthAssumption2];

        var rotor1 =
            u.CreatePureRotor(v);

        var rotor2 =
            rotor1.GetPureRotorInverse();

        var rotorMv1 = rotor1.Multivector;
        var rotorMv2 = rotor2.Multivector;

        //var rotorMatrix =
        //    rotor.GetVectorsMappingArray(
        //        (int) VSpaceDimensions,
        //        (int) VSpaceDimensions
        //    ).SimplifyScalars(unitLengthAssumption);

        //var rotorMatrixDet =
        //    Mfs.Det[rotorMatrix].FullSimplify(unitLengthAssumption);

        var v1 =
            rotor1.OmMap(u); //.SimplifyScalars(unitLengthAssumption);

        var u1 =
            rotor2.OmMap(v); //.SimplifyScalars(unitLengthAssumption);

        // Display a LaTeX representation of the vectors and their outer product
        Console.WriteLine($@"\boldsymbol{{u}} = {LaTeXComposer.GetMultivectorText(u)}");
        Console.WriteLine($@"\boldsymbol{{v}} = {LaTeXComposer.GetMultivectorText(v)}");
        Console.WriteLine($@"rotor = {LaTeXComposer.GetMultivectorText(rotorMv1)}");
        //Console.WriteLine($@"rotor matrix = {LaTeXComposer.GetArrayDisplayEquationText(rotorMatrix)}");
        //Console.WriteLine($@"det(matrix2 * matrix1) = {LaTeXComposer.GetScalarText(rotorMatrixDet)}");
        Console.WriteLine($@"rotor matrix * u = {LaTeXComposer.GetMultivectorText(v1)}");
        Console.WriteLine($@"rotor matrix transpose * v = {LaTeXComposer.GetMultivectorText(u1)}");
        Console.WriteLine();
    }

    public static void Execute3()
    {
        var e1 = GeometricProcessor.VectorTerm(0);
        var u = GeometricProcessor.Vector(VSpaceDimensions, i => $"u_{i + 1}");
        var v = GeometricProcessor.Vector(VSpaceDimensions, i => $"v_{i + 1}");

        //var unitLengthAssumption1 = Mfs.Equal[GeometricProcessor.ENormSquared(u), Expr.INT_ONE].Evaluate();
        //var unitLengthAssumption2 = Mfs.Equal[GeometricProcessor.ENormSquared(v), Expr.INT_ONE].Evaluate();
        //var unitLengthAssumption = Mfs.And[unitLengthAssumption1, unitLengthAssumption2];

        var rotor1 =
            u.CreatePureRotor(e1);

        var rotor2 =
            e1.CreatePureRotor(v);

        var rotorMv = rotor2.Multivector;
        var rotorMvReverse = rotor2.MultivectorReverse;

        //var u1 =
        //    rotorMv.EGp(e1).FullSimplifyScalars(unitLengthAssumption2);

        var indicesArray1 =
            VSpaceDimensions
                .BasisBladeIDsOfGrades(1, 3)
                .Select(i => (int)i)
                .ToArray();

        var indicesArray2 =
            VSpaceDimensions
                .BasisBladeIDsOfGrade(1)
                .Select(i => (int)i)
                .ToArray();

        var matrix1 =
            ScalarProcessor.CreateLinUnilinearMap(
                    VSpaceDimensions,
                    i =>
                        rotorMv.EGp(
                            GeometricProcessor.KVectorTerm((ulong)i)
                        ).MultivectorToLinVector()
                )
                .ToArray((int)GaSpaceDimensions)
                .GetShallowCopy(indicesArray1, indicesArray1);

        var matrix2 =
            ScalarProcessor.CreateLinUnilinearMap(
                    VSpaceDimensions,
                    i => GeometricProcessor.KVectorTerm((ulong)i).EGp(rotorMvReverse).MultivectorToLinVector()
                )
                .ToArray((int)GaSpaceDimensions)
                .GetShallowCopy(indicesArray1, indicesArray1);

        //var det1 = 
        //    Mfs.Det[matrix1.ToArrayExpr()].FullSimplify(unitLengthAssumptionExpr2);

        //var det2 = 
        //    Mfs.Det[matrix2.ToArrayExpr()].FullSimplify(unitLengthAssumptionExpr2);

        var rotorMatrix =
            rotor2.GetVectorMapPart(VSpaceDimensions).ToArray(VSpaceDimensions);

        var matrixDot =
            (Entity.Matrix)(matrix2.ArrayToMatrixExpr() * matrix1.ArrayToMatrixExpr());

        var matrixDotDet =
            MathS.Det(matrixDot);

        var v1 = rotorMatrix.MapVector(e1.ToLinVector());
        var v2 = rotorMatrix.Transpose().MapVector(v.ToLinVector());

        // Display a LaTeX representation of the vectors and their outer product
        Console.WriteLine($@"\boldsymbol{{u}} = {LaTeXComposer.GetMultivectorText(u)}");
        Console.WriteLine($@"\boldsymbol{{v}} = {LaTeXComposer.GetMultivectorText(v)}");
        Console.WriteLine($@"rotor = {LaTeXComposer.GetMultivectorText(rotorMv)}");
        //Console.WriteLine($@"rotor gp \boldsymbol{{e2}} = {LaTeXComposer.GetMultivectorText(u1)}");
        Console.WriteLine($@"rotor matrix = {LaTeXComposer.GetArrayDisplayEquationText(rotorMatrix)}");
        Console.WriteLine($@"matrix1 = {LaTeXComposer.GetArrayDisplayEquationText(matrix1)}");
        Console.WriteLine($@"matrix2 = {LaTeXComposer.GetArrayDisplayEquationText(matrix2)}");
        Console.WriteLine($@"matrix2 * matrix1 = {LaTeXComposer.GetScalarText(matrixDot)}");
        Console.WriteLine($@"det(matrix2 * matrix1) = {LaTeXComposer.GetScalarText(matrixDotDet)}");
        //Console.WriteLine($@"det(matrix1) = {LaTeXComposer.GetScalarText(det1)}");
        //Console.WriteLine($@"det(matrix2) = {LaTeXComposer.GetScalarText(det2)}");
        Console.WriteLine($@"rotor matrix * e1 = {LaTeXComposer.GetVectorText(v1)}");
        Console.WriteLine($@"rotor matrix transpose * v = {LaTeXComposer.GetVectorText(v2)}");
        Console.WriteLine();

    }
}