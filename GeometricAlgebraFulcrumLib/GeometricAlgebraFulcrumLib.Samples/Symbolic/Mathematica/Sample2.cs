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

        public static IGeometricAlgebraEuclideanProcessor<Expr> GeometricProcessor { get; }
            = ScalarAlgebraMathematicaProcessor.DefaultProcessor.CreateGeometricAlgebraEuclideanProcessor(3);

        public static TextMathematicaComposer TextComposer { get; }
            = TextMathematicaComposer.DefaultComposer;

        public static LaTeXMathematicaComposer LaTeXComposer { get; }
            = LaTeXMathematicaComposer.DefaultComposer;


        public static void Execute1()
        {
            var basisVectorIndex = 1ul;
            var u = GeometricProcessor.CreateVectorFromText(VSpaceDimension, i => $"Subscript[u,{i + 1}]");
            var v = GeometricProcessor.CreateVectorFromText(VSpaceDimension, i => $"Subscript[v,{i + 1}]");

            var unitLengthAssumption1 =
                Mfs.And[
                    Mfs.Element[Mfs.List[u.GetScalars().Cast<object>().ToArray()], Mfs.Ball[Array.Empty<object>()]],
                    Mfs.Equal[u.ENormSquared(), Expr.INT_ONE]
                ].Evaluate();

            var unitLengthAssumption2 = 
                Mfs.And[
                    Mfs.Element[Mfs.List[v.GetScalars().Cast<object>().ToArray()], Mfs.Ball[Array.Empty<object>()]],
                    Mfs.Equal[v.ENormSquared(), Expr.INT_ONE]
                ].Evaluate();

            var unitLengthAssumption = 
                Mfs.And[unitLengthAssumption1, unitLengthAssumption2].Evaluate();

            var rotationMatrix1 = 
                u.CreateVectorToBasisRotationMatrix(basisVectorIndex, (int) VSpaceDimension).FullSimplifyScalars(unitLengthAssumption1);

            var rotationMatrix2 = 
                v.CreateBasisToVectorRotationMatrix(basisVectorIndex, (int) VSpaceDimension).FullSimplifyScalars(unitLengthAssumption2);

            var rotationMatrix = 
                GeometricProcessor.MatrixProduct(rotationMatrix2, rotationMatrix1).FullSimplifyScalars(unitLengthAssumption);

            var rotationMatrixDet1 =
                rotationMatrix1.MatrixDeterminant().FullSimplify(unitLengthAssumption1);

            var rotationMatrixDet2 =
                rotationMatrix2.MatrixDeterminant().FullSimplify(unitLengthAssumption2);

            var rotationMatrixDet =
                rotationMatrix.MatrixDeterminant().FullSimplify(unitLengthAssumption);

            var u1 = 
                u.MapUsing(rotationMatrix1).FullSimplifyScalars(unitLengthAssumption1);

            var v1 = 
                u.MapUsing(rotationMatrix).FullSimplifyScalars(unitLengthAssumption);

            Console.WriteLine($@"rotor matrix 1 = {LaTeXComposer.GetArrayDisplayEquationText(rotationMatrix1)}");
            Console.WriteLine();

            Console.WriteLine($@"mapped u by matrix 1 = {LaTeXComposer.GetMultivectorText(u1)}");
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

            Console.WriteLine($@"mapped u = {TextComposer.GetMultivectorText(v1)}");
            Console.WriteLine();

            Console.WriteLine($@"mapped u = {LaTeXComposer.GetMultivectorText(v1)}");
            Console.WriteLine();
        }

        public static void Execute2()
        {
            var u = GeometricProcessor.CreateVectorFromText(VSpaceDimension, i => $"Subscript[u,{i + 1}]");
            var v = GeometricProcessor.CreateVectorFromText(VSpaceDimension, i => $"Subscript[v,{i + 1}]");

            var unitLengthAssumption1 =
                Mfs.And[
                    Mfs.Element[Mfs.List[u.GetScalars().Cast<object>().ToArray()], Mfs.Ball[Array.Empty<object>()]],
                    Mfs.Equal[u.ENormSquared().ScalarValue, Expr.INT_ONE]
                ].Evaluate();

            var unitLengthAssumption2 = 
                Mfs.And[
                    Mfs.Element[Mfs.List[v.GetScalars().Cast<object>().ToArray()], Mfs.Ball[Array.Empty<object>()]],
                    Mfs.Equal[v.ENormSquared().ScalarValue, Expr.INT_ONE]
                ].Evaluate();

            var unitLengthAssumption = 
                Mfs.And[unitLengthAssumption1, unitLengthAssumption2].Evaluate();

            var rotor1 =
                GeometricProcessor.CreatePureRotor(u, v);

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
            var e1 = GeometricProcessor.CreateVectorTerm(0, Expr.INT_MINUSONE);
            var u = GeometricProcessor.CreateVectorFromText(VSpaceDimension, i => $"Subscript[u,{i + 1}]");
            var v = GeometricProcessor.CreateVectorFromText(VSpaceDimension, i => $"Subscript[v,{i + 1}]");

            var unitLengthAssumption1 =
                Mfs.And[
                    Mfs.Element[Mfs.List[u.GetScalars().Cast<object>().ToArray()], Mfs.Ball[Array.Empty<object>()]],
                    Mfs.Equal[u.ENormSquared().ScalarValue, Expr.INT_ONE]
                ].Evaluate();

            var unitLengthAssumption2 = 
                Mfs.And[
                    Mfs.Element[Mfs.List[v.GetScalars().Cast<object>().ToArray()], Mfs.Ball[Array.Empty<object>()]],
                    Mfs.Equal[v.ENormSquared().ScalarValue, Expr.INT_ONE]
                ].Evaluate();

            var unitLengthAssumption = 
                Mfs.And[unitLengthAssumption1, unitLengthAssumption2].Evaluate();

            var rotor1 =
                GeometricProcessor.CreatePureRotor(u, e1);

            var rotor2 =
                GeometricProcessor.CreatePureRotor(e1, v);

            var rotorMv = rotor2.Multivector.SimplifyScalars(unitLengthAssumption2);
            var rotorMvReverse = rotorMv.Reverse();

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
                        i => rotorMv.EGp(GeometricProcessor.CreateKVectorBasis(i)).MultivectorStorage
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
                        i => GeometricProcessor.CreateKVectorBasis(i).EGp(rotorMvReverse).MultivectorStorage
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

            var v1 = rotorMatrix.Map(e1).FullSimplifyScalars(unitLengthAssumption2);
            var v2 = rotorMatrix.GetTranspose().Map(v).FullSimplifyScalars(unitLengthAssumption2);

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