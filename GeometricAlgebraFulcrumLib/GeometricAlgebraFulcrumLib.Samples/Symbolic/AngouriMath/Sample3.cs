using System;
using System.Linq;
using AngouriMath;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Algebra.LinearMaps;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Samples.Symbolic.AngouriMath
{
    public static class Sample3
    {
        public static uint VSpaceDimension 
            => Processor.VSpaceDimension;

        public static ulong GaSpaceDimension 
            => Processor.GaSpaceDimension;

        public static IGaProcessor<Entity> Processor { get; }
            = AngouriMathScalarProcessor.DefaultProcessor.CreateGaEuclideanProcessor(3);

        public static AngouriMathTextComposer TextComposer { get; }
            = AngouriMathTextComposer.DefaultComposer;

        public static AngouriMathLaTeXComposer LaTeXComposer { get; }
            = AngouriMathLaTeXComposer.DefaultComposer;


        public static void Execute1()
        {
            //var e1 = Processor.CreateBasisVector(0);
            var u = Processor.CreateGaVectorStorage(VSpaceDimension, i => $"u_{i + 1}");
            var v = Processor.CreateGaVectorStorage(VSpaceDimension, i => $"v_{i + 1}");

            //var unitLengthAssumption1 = Mfs.Equal[Processor.ENormSquared(u), Expr.INT_ONE].Evaluate();
            //var unitLengthAssumption2 = Mfs.Equal[Processor.ENormSquared(v), Expr.INT_ONE].Evaluate();
            //var unitLengthAssumption = Mfs.And[unitLengthAssumption1, unitLengthAssumption2];

            var rotationMatrix1 = 
                Processor.CreateRotationMatrixFromE1(u, (int) VSpaceDimension).Transpose();

            var rotationMatrix2 = 
                Processor.CreateRotationMatrixFromE1(v, (int) VSpaceDimension);

            var rotationMatrix = 
                Processor.MatrixTimes(rotationMatrix2, rotationMatrix1);

            var rotationMatrixDet1 =
                rotationMatrix1.MatrixDeterminant();

            var rotationMatrixDet2 =
                rotationMatrix2.MatrixDeterminant();

            var rotationMatrixDet =
                rotationMatrix.MatrixDeterminant();

            var v1 = 
                Processor.MapVector(rotationMatrix.CreateEvenGridDenseArray(), u);

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

            Console.WriteLine($@"mapped u = {LaTeXComposer.GetMultivectorText(v1)}");
            Console.WriteLine();
        }

        public static void Execute2()
        {
            var u = Processor.CreateGaVectorStorage(VSpaceDimension, i => $"u_{i + 1}");
            var v = Processor.CreateGaVectorStorage(VSpaceDimension, i => $"v_{i + 1}");

            //var unitLengthAssumption1 = Mfs.Equal[Processor.ENormSquared(u), Expr.INT_ONE].Evaluate();
            //var unitLengthAssumption2 = Mfs.Equal[Processor.ENormSquared(v), Expr.INT_ONE].Evaluate();
            //var unitLengthAssumption = Mfs.And[unitLengthAssumption1, unitLengthAssumption2];

            var rotor1 =
                Processor.CreateEuclideanRotor(u, v);

            var rotor2 =
                rotor1.GetReverseRotor();

            var rotorMv1 = rotor1.Multivector;
            var rotorMv2 = rotor2.Multivector;
            
            //var rotorMatrix =
            //    rotor.GetVectorsMappingArray(
            //        (int) VSpaceDimension,
            //        (int) VSpaceDimension
            //    ).SimplifyScalars(unitLengthAssumption);

            //var rotorMatrixDet =
            //    Mfs.Det[rotorMatrix].FullSimplify(unitLengthAssumption);

            var v1 =
                rotor1.MapVector(u); //.SimplifyScalars(unitLengthAssumption);

            var u1 = 
                rotor2.MapVector(v); //.SimplifyScalars(unitLengthAssumption);

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
            var e1 = Processor.CreateGaVectorStorage(0);
            var u = Processor.CreateGaVectorStorage(VSpaceDimension, i => $"u_{i + 1}");
            var v = Processor.CreateGaVectorStorage(VSpaceDimension, i => $"v_{i + 1}");

            //var unitLengthAssumption1 = Mfs.Equal[Processor.ENormSquared(u), Expr.INT_ONE].Evaluate();
            //var unitLengthAssumption2 = Mfs.Equal[Processor.ENormSquared(v), Expr.INT_ONE].Evaluate();
            //var unitLengthAssumption = Mfs.And[unitLengthAssumption1, unitLengthAssumption2];

            var rotor1 =
                Processor.CreateEuclideanRotor(u, e1);

            var rotor2 =
                Processor.CreateEuclideanRotor(e1, v);

            var rotorMv = rotor2.Multivector;
            var rotorMvReverse = Processor.Reverse(rotorMv);

            //var u1 =
            //    rotorMv.EGp(e1).FullSimplifyScalars(unitLengthAssumption2);

            var indicesArray1 =
                Processor
                    .BasisBladeIDsOfGrades(1, 3)
                    .Select(i => (int)i)
                    .ToArray();

            var indicesArray2 =
                Processor
                    .BasisBladeIDsOfGrade(1)
                    .Select(i => (int)i)
                    .ToArray();

            var matrix1 =
                Processor
                    .CreateStoredUnilinearMap(
                        VSpaceDimension,
                        i => Processor.EGp(rotorMv, Processor.CreateKVectorStorage(i))
                    )
                    .GetMultivectorsMappingArray(
                        (int)GaSpaceDimension,
                        (int)GaSpaceDimension
                    )
                    .GetSubArray(indicesArray1, indicesArray1);

            var matrix2 =
                Processor
                    .CreateStoredUnilinearMap(
                        VSpaceDimension,
                        i => Processor.EGp(Processor.CreateKVectorStorage(i), rotorMvReverse)
                    )
                    .GetMultivectorsMappingArray(
                        (int)GaSpaceDimension,
                        (int)GaSpaceDimension
                    )
                    .GetSubArray(indicesArray1, indicesArray1);

            //var det1 = 
            //    Mfs.Det[matrix1.ToArrayExpr()].FullSimplify(unitLengthAssumptionExpr2);

            //var det2 = 
            //    Mfs.Det[matrix2.ToArrayExpr()].FullSimplify(unitLengthAssumptionExpr2);

            var rotorMatrix =
                rotor2.GetVectorsMappingArray(
                    (int)VSpaceDimension,
                    (int)VSpaceDimension
                );

            var matrixDot =
                (Entity.Matrix) (matrix2.ArrayToMatrixExpr() * matrix1.ArrayToMatrixExpr());

            var matrixDotDet =
                MathS.Det(matrixDot);

            var v1 = Processor.MapVector(rotorMatrix, e1);
            var v2 = Processor.MapVector(rotorMatrix.Transpose(), v);

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
            Console.WriteLine($@"rotor matrix * e1 = {LaTeXComposer.GetMultivectorText(v1)}");
            Console.WriteLine($@"rotor matrix transpose * v = {LaTeXComposer.GetMultivectorText(v2)}");
            Console.WriteLine();

        }
    }
}