using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.SymbolicApplications.Samples.Mathematica
{
    public static class Sample2
    {
        public static int VSpaceDimensions 
            => 3;

        public static ulong GaSpaceDimensions 
            => 1UL << VSpaceDimensions;

        public static IScalarProcessor<Expr> ScalarProcessor
            => ScalarProcessorExpr.DefaultProcessor;

        public static RGaProcessor<Expr> GeometricProcessor { get; }
            = ScalarProcessor.CreateEuclideanRGaProcessor();

        public static TextComposerExpr TextComposer { get; }
            = TextComposerExpr.DefaultComposer;

        public static LaTeXComposerExpr LaTeXComposer { get; }
            = LaTeXComposerExpr.DefaultComposer;


        public static void Execute1()
        {
            var basisVectorIndex = 1;
            var u = ScalarProcessor.CreateLinVector(VSpaceDimensions, i => $"Subscript[u,{i + 1}]");
            var v = ScalarProcessor.CreateLinVector(VSpaceDimensions, i => $"Subscript[v,{i + 1}]");

            var unitLengthAssumption1 =
                Mfs.And[
                    Mfs.Element[Mfs.List[u.Scalars.Cast<object>().ToArray()], Mfs.Ball[Array.Empty<object>()]],
                    Mfs.Equal[u.ENormSquared(), Expr.INT_ONE]
                ].Evaluate();

            var unitLengthAssumption2 = 
                Mfs.And[
                    Mfs.Element[Mfs.List[v.Scalars.Cast<object>().ToArray()], Mfs.Ball[Array.Empty<object>()]],
                    Mfs.Equal[v.ENormSquared(), Expr.INT_ONE]
                ].Evaluate();

            var unitLengthAssumption = 
                Mfs.And[unitLengthAssumption1, unitLengthAssumption2].Evaluate();

            var rotationMatrix1 = 
                ScalarProcessor
                    .CreateVectorToBasisRotationMap(u, basisVectorIndex, VSpaceDimensions)
                    .ToArray(VSpaceDimensions)
                    .FullSimplifyScalars(unitLengthAssumption1);

            var rotationMatrix2 = 
                ScalarProcessor
                    .CreateBasisToVectorRotationMap(basisVectorIndex, v, VSpaceDimensions)
                    .ToArray(VSpaceDimensions)
                    .FullSimplifyScalars(unitLengthAssumption2);

            var rotationMatrix = 
                ScalarProcessor.MatrixProduct(rotationMatrix2, rotationMatrix1).FullSimplifyScalars(unitLengthAssumption);

            var rotationMatrixDet1 =
                rotationMatrix1.MatrixDeterminant().FullSimplify(unitLengthAssumption1);

            var rotationMatrixDet2 =
                rotationMatrix2.MatrixDeterminant().FullSimplify(unitLengthAssumption2);

            var rotationMatrixDet =
                rotationMatrix.MatrixDeterminant().FullSimplify(unitLengthAssumption);

            var u1 = 
                ScalarProcessor.MapVector(rotationMatrix1, u).FullSimplifyScalars(unitLengthAssumption1);

            var v1 = 
                ScalarProcessor.MapVector(rotationMatrix, u).FullSimplifyScalars(unitLengthAssumption);

            Console.WriteLine($@"rotor matrix 1 = {LaTeXComposer.GetArrayDisplayEquationText(rotationMatrix1)}");
            Console.WriteLine();

            Console.WriteLine($@"mapped u by matrix 1 = {LaTeXComposer.GetVectorText(u1)}");
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

            Console.WriteLine($@"mapped u = {TextComposer.GetVectorText(v1)}");
            Console.WriteLine();

            Console.WriteLine($@"mapped u = {LaTeXComposer.GetVectorText(v1)}");
            Console.WriteLine();
        }

        public static void Execute2()
        {
            var u = GeometricProcessor.CreateVector(VSpaceDimensions, i => $"Subscript[u,{i + 1}]");
            var v = GeometricProcessor.CreateVector(VSpaceDimensions, i => $"Subscript[v,{i + 1}]");

            var unitLengthAssumption1 =
                Mfs.And[
                    Mfs.Element[Mfs.List[u.Scalars.Cast<object>().ToArray()], Mfs.Ball[Array.Empty<object>()]],
                    Mfs.Equal[u.ENormSquared().ScalarValue, Expr.INT_ONE]
                ].Evaluate();

            var unitLengthAssumption2 = 
                Mfs.And[
                    Mfs.Element[Mfs.List[v.Scalars.Cast<object>().ToArray()], Mfs.Ball[Array.Empty<object>()]],
                    Mfs.Equal[v.ENormSquared().ScalarValue, Expr.INT_ONE]
                ].Evaluate();

            var unitLengthAssumption = 
                Mfs.And[unitLengthAssumption1, unitLengthAssumption2].Evaluate();

            var rotor1 =
                u.CreatePureRotor(v);

            var rotor2 =
                rotor1.GetPureRotorInverse();

            var rotorMv1 = rotor1.Multivector.SimplifyScalars(unitLengthAssumption);
            var rotorMv2 = rotor2.Multivector.SimplifyScalars(unitLengthAssumption);
            
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
            var e1 = GeometricProcessor.CreateVector(0, Expr.INT_MINUSONE);
            var u = GeometricProcessor.CreateVector(VSpaceDimensions, i => $"Subscript[u,{i + 1}]");
            var v = GeometricProcessor.CreateVector(VSpaceDimensions, i => $"Subscript[v,{i + 1}]");

            var unitLengthAssumption1 =
                Mfs.And[
                    Mfs.Element[Mfs.List[u.Scalars.Cast<object>().ToArray()], Mfs.Ball[Array.Empty<object>()]],
                    Mfs.Equal[u.ENormSquared().ScalarValue, Expr.INT_ONE]
                ].Evaluate();

            var unitLengthAssumption2 = 
                Mfs.And[
                    Mfs.Element[Mfs.List[v.Scalars.Cast<object>().ToArray()], Mfs.Ball[Array.Empty<object>()]],
                    Mfs.Equal[v.ENormSquared().ScalarValue, Expr.INT_ONE]
                ].Evaluate();

            var unitLengthAssumption = 
                Mfs.And[unitLengthAssumption1, unitLengthAssumption2].Evaluate();

            var rotor1 =
                u.CreatePureRotor(e1);

            var rotor2 =
                e1.CreatePureRotor(v);

            var rotorMv = rotor2.Multivector.SimplifyScalars(unitLengthAssumption2);
            var rotorMvReverse = rotorMv.Reverse();

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
                ScalarProcessor
                    .CreateLinUnilinearMap(
                        VSpaceDimensions,
                        i => rotorMv.EGp(GeometricProcessor.CreateKVector((ulong) i)).MultivectorToLinVector()
                    )
                    .ToArray((int)GaSpaceDimensions)
                    .GetShallowCopy(indicesArray1, indicesArray1)
                    .SimplifyScalars(unitLengthAssumption2);

            var matrix2 =
                ScalarProcessor
                    .CreateLinUnilinearMap(
                        VSpaceDimensions,
                        i => GeometricProcessor.CreateKVector((ulong)i).EGp(rotorMvReverse).MultivectorToLinVector()
                    )
                    .ToArray((int)GaSpaceDimensions)
                    .GetShallowCopy(indicesArray1, indicesArray1)
                    .SimplifyScalars(unitLengthAssumption2);

            //var det1 = 
            //    Mfs.Det[matrix1.ToArrayExpr()].FullSimplify(unitLengthAssumptionExpr2);

            //var det2 = 
            //    Mfs.Det[matrix2.ToArrayExpr()].FullSimplify(unitLengthAssumptionExpr2);

            var rotorMatrix =
                rotor2.GetVectorMapPart(VSpaceDimensions).ToArray(VSpaceDimensions).SimplifyScalars(unitLengthAssumption2);

            var matrixDot =
                Mfs.Dot[matrix2.ToMatrixExpr(), matrix1.ToMatrixExpr()].FullSimplify(unitLengthAssumption2);

            var matrixDotDet =
                Mfs.Det[matrixDot].FullSimplify(unitLengthAssumption2);

            var v1 = rotorMatrix.MapVector(e1.ToLinVector()).FullSimplifyScalars(unitLengthAssumption2);
            var v2 = rotorMatrix.Transpose().MapVector(v.ToLinVector()).FullSimplifyScalars(unitLengthAssumption2);

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
}