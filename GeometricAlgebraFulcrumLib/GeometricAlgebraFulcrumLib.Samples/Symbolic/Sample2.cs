using System;
using System.Linq;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Geometry.Euclidean;
using GeometricAlgebraFulcrumLib.Processing;
using GeometricAlgebraFulcrumLib.Processing.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Symbolic;
using GeometricAlgebraFulcrumLib.Symbolic.Mathematica;
using GeometricAlgebraFulcrumLib.Symbolic.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.Symbolic.Processors;
using GeometricAlgebraFulcrumLib.Symbolic.Text;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Samples.Symbolic
{
    public static class Sample2
    {
        public static uint VSpaceDimension 
            => Processor.VSpaceDimension;

        public static ulong GaSpaceDimension 
            => Processor.GaSpaceDimension;

        public static IGaProcessor<Expr> Processor { get; }
            = GaScalarProcessorMathematicaExpr.DefaultProcessor.CreateEuclideanProcessor(3);

        public static GaTextComposerMathematicaExpr TextComposer { get; }
            = GaTextComposerMathematicaExpr.DefaultComposer;

        public static GaLaTeXComposerMathematicaExpr LaTeXComposer { get; }
            = GaLaTeXComposerMathematicaExpr.DefaultComposer;


        public static void Execute1()
        {
            //var e1 = Processor.CreateBasisVector(0);
            var u = Processor.CreateVector(VSpaceDimension, i => $"Subscript[u,{i + 1}]".ToExpr());
            var v = Processor.CreateVector(VSpaceDimension, i => $"Subscript[v,{i + 1}]".ToExpr());

            var unitLengthAssumption1 = Mfs.Equal[u.ENormSquared(), Expr.INT_ONE].Evaluate();
            var unitLengthAssumption2 = Mfs.Equal[v.ENormSquared(), Expr.INT_ONE].Evaluate();
            var unitLengthAssumption = Mfs.And[unitLengthAssumption1, unitLengthAssumption2];

            var rotationMatrix1 = 
                u.CreateSimpleEuclideanRotationMatrix((int) VSpaceDimension).Transpose().FullSimplifyScalars(unitLengthAssumption1);

            var rotationMatrix2 = 
                v.CreateSimpleEuclideanRotationMatrix((int) VSpaceDimension).FullSimplifyScalars(unitLengthAssumption2);

            var rotationMatrix = 
                Processor.MatrixTimes(rotationMatrix2, rotationMatrix1).FullSimplifyScalars(unitLengthAssumption);

            var rotationMatrixDet1 =
                rotationMatrix1.MatrixDeterminant().FullSimplify(unitLengthAssumption1);

            var rotationMatrixDet2 =
                rotationMatrix2.MatrixDeterminant().FullSimplify(unitLengthAssumption2);

            var rotationMatrixDet =
                rotationMatrix.MatrixDeterminant().FullSimplify(unitLengthAssumption);

            var v1 = 
                rotationMatrix.MapVector(u).FullSimplifyScalars(unitLengthAssumption);

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
            var u = Processor.CreateVector(VSpaceDimension, i => $"Subscript[u,{i + 1}]".ToExpr());
            var v = Processor.CreateVector(VSpaceDimension, i => $"Subscript[v,{i + 1}]".ToExpr());

            var unitLengthAssumption1 = Mfs.Equal[u.ENormSquared(), Expr.INT_ONE].Evaluate();
            var unitLengthAssumption2 = Mfs.Equal[v.ENormSquared(), Expr.INT_ONE].Evaluate();
            var unitLengthAssumption = Mfs.And[unitLengthAssumption1, unitLengthAssumption2];

            var rotor1 =
                GaEuclideanSimpleRotor<Expr>.Create(Processor, u, v);

            var rotor2 =
                GaEuclideanSimpleRotor<Expr>.Create(Processor, rotor1.Rotor.GetReverse());

            var rotorMv1 = rotor1.Rotor.SimplifyScalars(unitLengthAssumption);
            var rotorMv2 = rotor2.Rotor.SimplifyScalars(unitLengthAssumption);
            
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
            var e1 = Processor.CreateBasisVector(0);
            var u = Processor.CreateVector(VSpaceDimension, i => $"Subscript[u,{i + 1}]".ToExpr());
            var v = Processor.CreateVector(VSpaceDimension, i => $"Subscript[v,{i + 1}]".ToExpr());

            var unitLengthAssumption1 = Mfs.Equal[u.ENormSquared(), Expr.INT_ONE].Evaluate();
            var unitLengthAssumption2 = Mfs.Equal[v.ENormSquared(), Expr.INT_ONE].Evaluate();
            var unitLengthAssumption = Mfs.And[unitLengthAssumption1, unitLengthAssumption2];

            var rotor1 =
                GaEuclideanSimpleRotor<Expr>.Create(Processor, u, e1);

            var rotor2 =
                GaEuclideanSimpleRotor<Expr>.Create(Processor, e1, v);

            var rotorMv = rotor2.Rotor.SimplifyScalars(unitLengthAssumption2);
            var rotorMvReverse = rotorMv.GetReverse();

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
                        i => rotorMv.EGp(Processor.CreateBasisBlade(i))
                    )
                    .GetMultivectorsMappingArray(
                        (int)GaSpaceDimension,
                        (int)GaSpaceDimension
                    )
                    .GetSubArray(indicesArray1, indicesArray1)
                    .SimplifyScalars(unitLengthAssumption2);

            var matrix2 =
                Processor
                    .CreateStoredUnilinearMap(
                        VSpaceDimension,
                        i => Processor.CreateBasisBlade(i).EGp(rotorMvReverse)
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
                rotor2.GetVectorsMappingArray(
                    (int)VSpaceDimension,
                    (int)VSpaceDimension
                ).SimplifyScalars(unitLengthAssumption2);

            var matrixDot =
                Mfs.Dot[matrix2.ToArrayExpr(), matrix1.ToArrayExpr()].FullSimplify(unitLengthAssumption2);

            var matrixDotDet =
                Mfs.Det[matrixDot].FullSimplify(unitLengthAssumption2);

            var v1 = rotorMatrix.MapVector(e1).FullSimplifyScalars(unitLengthAssumption2);
            var v2 = rotorMatrix.Transpose().MapVector(v).FullSimplifyScalars(unitLengthAssumption2);

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