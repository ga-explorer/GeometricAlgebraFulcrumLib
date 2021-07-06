using System;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.Geometry.Euclidean;
using GeometricAlgebraFulcrumLib.Symbolic;
using GeometricAlgebraFulcrumLib.Symbolic.Mathematica;
using GeometricAlgebraFulcrumLib.Symbolic.Mathematica.ExprFactory;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Samples.EuclideanGeometry
{
    public static class Euclidean3DSymbolicSample
    {
        public static void Execute()
        {
            var n = 3;

            var v = GaSymbolicUtils.CreateVector(
                "Subscript[v,1]", "Subscript[v,2]", "Subscript[v,3]"
            );

            var u = GaSymbolicUtils.CreateVector(
                "Subscript[u,1]", "Subscript[u,2]", "Subscript[u,3]"
            );


            var rotor = 
                GaEuclideanSimpleRotor<Expr>.Create(v, u);

            var rotorMv = rotor.Storage;
            var rotorMvReverse = rotor.Storage.GetReverse();

            var unitLengthAssumptionExpr =
                Mfs.And[
                    Mfs.Equal[v.ENormSquared(), Expr.INT_ONE],
                    Mfs.Equal[u.ENormSquared(), Expr.INT_ONE]
                ];

            var rotorMatrix =
                rotor
                    .GetMatrix(n, n)
                    .Simplify(unitLengthAssumptionExpr);

            var rotorMatrix1 =
                GaSymbolicUtils.CreateVectorsLinearMap(
                        n,
                        basisVector =>
                            rotorMv.EGp(basisVector).GetVectorPart()
                    )
                    .GetMatrix(n, n)
                    .Simplify(unitLengthAssumptionExpr);

            var rotorMatrix2 =
                GaSymbolicUtils.CreateVectorsLinearMap(
                        n,
                        basisVector =>
                            basisVector.EGp(rotorMvReverse).GetVectorPart()
                    )
                    .GetMatrix(n, n)
                    .Simplify(unitLengthAssumptionExpr);

            var rotorMatrix21 = 
                rotorMatrix2
                    .MatrixProduct(rotorMatrix1)
                    .Simplify(unitLengthAssumptionExpr);

            var vMatrix = v.VectorToColumnVectorMatrix(n);
            var uMatrix = u.VectorToColumnVectorMatrix(n);
            var u1 = Mfs.Expand[rotor.MapVector(v).VectorToColumnVectorMatrix(n)].Evaluate();
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
