using System;
using System.Linq;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.Mathematica.Text;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Samples.Symbolic.Mathematica
{
    public static class Sample2
    {
        public static uint VSpaceDimension 
            => GeometricProcessor.VSpaceDimension;

        public static ulong GaSpaceDimension 
            => GeometricProcessor.GaSpaceDimension;

        public static IGeometricAlgebraProcessor<Expr> GeometricProcessor { get; }
            = ScalarAlgebraMathematicaProcessor.DefaultProcessor.CreateGeometricAlgebraEuclideanProcessor(3);

        public static MathematicaTextComposer TextComposer { get; }
            = MathematicaTextComposer.DefaultComposer;

        public static MathematicaLaTeXComposer LaTeXComposer { get; }
            = MathematicaLaTeXComposer.DefaultComposer;


        public static void Execute1()
        {
            //var e1 = GeometricProcessor.CreateBasisVector(0);
            var u = GeometricProcessor.CreateVectorStorageFromText(VSpaceDimension, i => $"Subscript[u,{i + 1}]");
            var v = GeometricProcessor.CreateVectorStorageFromText(VSpaceDimension, i => $"Subscript[v,{i + 1}]");

            var unitLengthAssumption1 = Mfs.Equal[GeometricProcessor.ENormSquared(u), Expr.INT_ONE].Evaluate();
            var unitLengthAssumption2 = Mfs.Equal[GeometricProcessor.ENormSquared(v), Expr.INT_ONE].Evaluate();
            var unitLengthAssumption = Mfs.And[unitLengthAssumption1, unitLengthAssumption2];

            var rotationMatrix1 = 
                GeometricProcessor.CreateRotationMatrixFromE1(u, (int) VSpaceDimension).Transpose().FullSimplifyScalars(unitLengthAssumption1);

            var rotationMatrix2 = 
                GeometricProcessor.CreateRotationMatrixFromE1(v, (int) VSpaceDimension).FullSimplifyScalars(unitLengthAssumption2);

            var rotationMatrix = 
                GeometricProcessor.MatrixProduct(rotationMatrix2, rotationMatrix1).FullSimplifyScalars(unitLengthAssumption);

            var rotationMatrixDet1 =
                rotationMatrix1.MatrixDeterminant().FullSimplify(unitLengthAssumption1);

            var rotationMatrixDet2 =
                rotationMatrix2.MatrixDeterminant().FullSimplify(unitLengthAssumption2);

            var rotationMatrixDet =
                rotationMatrix.MatrixDeterminant().FullSimplify(unitLengthAssumption);

            var v1 = 
                GeometricProcessor.MapVector(rotationMatrix.CreateLinMatrixDenseStorage(), u).FullSimplifyScalars(unitLengthAssumption);

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
            var u = GeometricProcessor.CreateVectorStorageFromText(VSpaceDimension, i => $"Subscript[u,{i + 1}]");
            var v = GeometricProcessor.CreateVectorStorageFromText(VSpaceDimension, i => $"Subscript[v,{i + 1}]");

            var unitLengthAssumption1 = Mfs.Equal[GeometricProcessor.ENormSquared(u), Expr.INT_ONE].Evaluate();
            var unitLengthAssumption2 = Mfs.Equal[GeometricProcessor.ENormSquared(v), Expr.INT_ONE].Evaluate();
            var unitLengthAssumption = Mfs.And[unitLengthAssumption1, unitLengthAssumption2];

            var rotor1 =
                GeometricProcessor.CreateEuclideanRotor(u, v);

            var rotor2 =
                rotor1.GetPureRotorInverse();

            var rotorMv1 = rotor1.Multivector.SimplifyScalars(unitLengthAssumption);
            var rotorMv2 = rotor2.Multivector.SimplifyScalars(unitLengthAssumption);
            
            //var rotorMatrix =
            //    rotor.GetVectorsMappingArray(
            //        (int) VSpaceDimension,
            //        (int) VSpaceDimension
            //    ).SimplifyScalars(unitLengthAssumption);

            //var rotorMatrixDet =
            //    Mfs.Det[rotorMatrix].FullSimplify(unitLengthAssumption);

            var v1 =
                rotor1.OmMapVector(u); //.SimplifyScalars(unitLengthAssumption);

            var u1 = 
                rotor2.OmMapVector(v); //.SimplifyScalars(unitLengthAssumption);

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
            var e1 = GeometricProcessor.CreateVectorBasisStorage(0);
            var u = GeometricProcessor.CreateVectorStorageFromText(VSpaceDimension, i => $"Subscript[u,{i + 1}]");
            var v = GeometricProcessor.CreateVectorStorageFromText(VSpaceDimension, i => $"Subscript[v,{i + 1}]");

            var unitLengthAssumption1 = Mfs.Equal[GeometricProcessor.ENormSquared(u), Expr.INT_ONE].Evaluate();
            var unitLengthAssumption2 = Mfs.Equal[GeometricProcessor.ENormSquared(v), Expr.INT_ONE].Evaluate();
            var unitLengthAssumption = Mfs.And[unitLengthAssumption1, unitLengthAssumption2];

            var rotor1 =
                GeometricProcessor.CreateEuclideanRotor(u, e1);

            var rotor2 =
                GeometricProcessor.CreateEuclideanRotor(e1, v);

            var rotorMv = rotor2.Multivector.SimplifyScalars(unitLengthAssumption2);
            var rotorMvReverse = GeometricProcessor.Reverse(rotorMv);

            //var u1 =
            //    rotorMv.EGp(e1).FullSimplifyScalars(unitLengthAssumption2);

            var indicesArray1 =
                GeometricProcessor
                    .BasisBladeIDsOfGrades(1, 3)
                    .Select(i => (int)i)
                    .ToArray();

            var indicesArray2 =
                GeometricProcessor
                    .BasisBladeIDsOfGrade(1)
                    .Select(i => (int)i)
                    .ToArray();

            var matrix1 =
                GeometricProcessor
                    .CreateSparseUnilinearMap(
                        i => GeometricProcessor.EGp(rotorMv, GeometricProcessor.CreateKVectorBasisStorage(i))
                    )
                    .GetMultivectorsMappingArray(
                        (int)GaSpaceDimension,
                        (int)GaSpaceDimension
                    )
                    .GetSubArray(indicesArray1, indicesArray1)
                    .SimplifyScalars(unitLengthAssumption2);

            var matrix2 =
                GeometricProcessor
                    .CreateSparseUnilinearMap(
                        i => GeometricProcessor.EGp(GeometricProcessor.CreateKVectorBasisStorage(i), rotorMvReverse)
                    )
                    .GetMultivectorsMappingArray(
                        (int)GaSpaceDimension,
                        (int)GaSpaceDimension
                    )
                    .GetSubArray(indicesArray1, indicesArray1)
                    .SimplifyScalars(unitLengthAssumption2);

            //var det1 = 
            //    Mfs.Det[matrix1.ToArrayExpr()].FullSimplify(unitLengthAssumptionExpr2);

            //var det2 = 
            //    Mfs.Det[matrix2.ToArrayExpr()].FullSimplify(unitLengthAssumptionExpr2);

            var rotorMatrix =
                rotor2.GetVectorOmMappingMatrix(
                    (int)VSpaceDimension,
                    (int)VSpaceDimension
                ).SimplifyScalars(unitLengthAssumption2);

            var matrixDot =
                Mfs.Dot[matrix2.ArrayToMatrixExpr(), matrix1.ArrayToMatrixExpr()].FullSimplify(unitLengthAssumption2);

            var matrixDotDet =
                Mfs.Det[matrixDot].FullSimplify(unitLengthAssumption2);

            var v1 = GeometricProcessor.MapVector(rotorMatrix, e1).FullSimplifyScalars(unitLengthAssumption2);
            var v2 = GeometricProcessor.MapVector(rotorMatrix.GetTranspose(), v).FullSimplifyScalars(unitLengthAssumption2);

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