using System;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.Processors;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Samples.EuclideanGeometry
{
    public static class Euclidean3DSymbolicSample
    {
        public static void Execute()
        {
            var n = 3U;
            var processor = 
                ScalarAlgebraMathematicaProcessor.DefaultProcessor.CreateGeometricAlgebraEuclideanProcessor(n);

            var v = MathematicaUtils.CreateVector(
                "Subscript[v,1]", "Subscript[v,2]", "Subscript[v,3]"
            );

            var u = MathematicaUtils.CreateVector(
                "Subscript[u,1]", "Subscript[u,2]", "Subscript[u,3]"
            );


            var rotor = 
                processor.CreatePureRotor(v, u);

            var rotorMv = rotor.Multivector;
            var rotorMvReverse = rotor.Multivector.Reverse();

            var unitLengthAssumptionExpr =
                Mfs.And[
                    Mfs.Equal[v.ENormSquared().ScalarValue, Expr.INT_ONE],
                    Mfs.Equal[u.ENormSquared().ScalarValue, Expr.INT_ONE]
                ];

            var rotorMatrix =
                rotor
                    .GetMatrix((int) n, (int) n)
                    .Simplify(unitLengthAssumptionExpr);

            var rotorMatrix1 =
                MathematicaUtils.CreateVectorsLinearMap(
                        (int) n,
                        basisVector =>
                            rotorMv.EGp(basisVector.CreateVector(processor)).GetVectorPart().VectorStorage
                    )
                    .GetMatrix((int) n, (int) n)
                    .Simplify(unitLengthAssumptionExpr);

            var rotorMatrix2 =
                MathematicaUtils.CreateVectorsLinearMap(
                        (int) n,
                        basisVector =>
                            basisVector.CreateVector(processor).EGp(rotorMvReverse).GetVectorPart().VectorStorage
                    )
                    .GetMatrix((int) n, (int) n)
                    .Simplify(unitLengthAssumptionExpr);

            var rotorMatrix21 = 
                rotorMatrix2
                    .MatrixProduct(rotorMatrix1)
                    .Simplify(unitLengthAssumptionExpr);

            var vMatrix = v.VectorStorage.VectorToColumnVectorMatrix(n);
            var uMatrix = u.VectorStorage.VectorToColumnVectorMatrix(n);
            var u1 = Mfs.Expand[rotor.OmMap(v).VectorStorage.VectorToColumnVectorMatrix(n)].Evaluate();
            var u2 = Mfs.Expand[rotorMatrix.MatrixProduct(vMatrix)].Evaluate();
            var u3 = Mfs.Expand[rotorMatrix21.MatrixProduct(vMatrix)].Evaluate();
            var u4 = Mfs.Expand[rotorMatrix2.MatrixProduct(rotorMatrix1.MatrixProduct(vMatrix))].Evaluate();

            Console.WriteLine("Rotor Matrix:");
            Console.WriteLine(rotorMatrix.ToString());
            Console.WriteLine();

            Console.WriteLine("Rotor Matrix1:");
            Console.WriteLine(rotorMatrix1.ToString());
            Console.WriteLine();

            Console.WriteLine("Rotor Matrix2:");
            Console.WriteLine(rotorMatrix2.ToString());
            Console.WriteLine();

            Console.WriteLine("Rotor Matrix2 * Matrix1:");
            Console.WriteLine(rotorMatrix21.ToString());
            Console.WriteLine();

            Console.WriteLine("v Vector Matrix:");
            Console.WriteLine(vMatrix.ToString());
            Console.WriteLine();

            Console.WriteLine("u Vector Matrix:");
            Console.WriteLine(uMatrix.ToString());
            Console.WriteLine();

            Console.WriteLine("Rotated Vector 1:");
            Console.WriteLine(u1.ToString());
            Console.WriteLine();

            Console.WriteLine("Rotated Vector 2:");
            Console.WriteLine(u2.ToString());
            Console.WriteLine();

            Console.WriteLine("Rotated Vector 3:");
            Console.WriteLine(u3.ToString());
            Console.WriteLine();

            Console.WriteLine("Rotated Vector 4:");
            Console.WriteLine(u4.ToString());
            Console.WriteLine();
        }
    }
}
