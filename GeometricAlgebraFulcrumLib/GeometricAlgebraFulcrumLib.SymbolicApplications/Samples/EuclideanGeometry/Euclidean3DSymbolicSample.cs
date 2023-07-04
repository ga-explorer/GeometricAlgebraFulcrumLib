using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic.Matrices;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.SymbolicApplications.Samples.EuclideanGeometry
{
    public static class Euclidean3DSymbolicSample
    {
        public static void Execute()
        {
            var n = 3;

            var scalarProcessor = 
                ScalarProcessorExpr.DefaultProcessor;

            var processor = 
                XGaProcessor<Expr>.CreateEuclidean(scalarProcessor);

            var v = processor.CreateVector(
                "Subscript[v,1]", "Subscript[v,2]", "Subscript[v,3]"
            );

            var u = processor.CreateVector(
                "Subscript[u,1]", "Subscript[u,2]", "Subscript[u,3]"
            );


            var rotor = 
                v.CreatePureRotor(u);

            var rotorMv = rotor.Multivector;
            var rotorMvReverse = rotor.Multivector.Reverse();

            var unitLengthAssumptionExpr =
                Mfs.And[
                    Mfs.Equal[v.ENormSquared().ScalarValue, Expr.INT_ONE],
                    Mfs.Equal[u.ENormSquared().ScalarValue, Expr.INT_ONE]
                ];

            var rotorMatrix =
                rotor
                    .GetMultivectorMapArray(n, n)
                    .SimplifyScalars(unitLengthAssumptionExpr);

            var rotorMatrix1 =
                scalarProcessor.CreateLinUnilinearMap(
                        n,
                        (int index) =>
                            rotorMv.EGp(processor.CreateVector(index)).GetVectorPart().ToLinVector()
                    )
                    .ToArray(n, n)
                    .SimplifyScalars(unitLengthAssumptionExpr);

            var rotorMatrix2 =
                scalarProcessor.CreateLinUnilinearMap(
                        n,
                        (int index) =>
                            processor.CreateVector(index).EGp(rotorMvReverse).GetVectorPart().ToLinVector()
                    )
                    .ToArray(n, n)
                    .SimplifyScalars(unitLengthAssumptionExpr);

            var rotorMatrix21 = 
                scalarProcessor.Times(
                    rotorMatrix2, 
                    rotorMatrix1
                ).SimplifyScalars(unitLengthAssumptionExpr);

            var vMatrix = v.VectorToColumnArray2D(n);
            var uMatrix = u.VectorToColumnArray2D(n);
            var u1 = Mfs.Expand[rotor.OmMap(v).ToColumnVectorMatrixExpr(n)].Evaluate();
            var u2 = Mfs.Expand[scalarProcessor.Times(rotorMatrix, vMatrix)].Evaluate();
            var u3 = Mfs.Expand[scalarProcessor.Times(rotorMatrix21, vMatrix)].Evaluate();
            var u4 = Mfs.Expand[scalarProcessor.Times(rotorMatrix2, rotorMatrix1, vMatrix)].Evaluate();

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
