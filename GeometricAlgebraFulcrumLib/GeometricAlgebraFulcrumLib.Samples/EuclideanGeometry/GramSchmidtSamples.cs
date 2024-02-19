using System;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Frames;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Frames;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Samples.EuclideanGeometry;

public static class GramSchmidtSamples
{
    public static void Example1()
    {
        // 3 Orthogonal vectors
        //var v1 = new Float64Tuple3D(1, -1, 2);
        //var v2 = v1.GetNormal();
        //var v3 = v1.VectorCross(v2);

        // 2 non-orthogonal vectors and their cross product
        //var v1 = new Float64Tuple3D(1, -1, 2);
        //var v2 = new Float64Tuple3D(-1, -1, 1.5);
        //var v3 = v1.VectorCross(v2);
            
        // 3 non-orthogonal vectors
        //var v1 = new Float64Tuple3D(1, -1, 2);
        //var v2 = new Float64Tuple3D(-1, -1, 1.5);
        //var v3 = new Float64Tuple3D(2, 1, 1.5);

        // 2 Orthogonal vectors and one parallel to 1st vector
        //var v1 = new Float64Tuple3D(1, -1, 2);
        //var v2 = v1.GetNormal();
        //var v3 = 2.5 * v1;
            
        // 2 non-orthogonal vectors and one parallel to 2nd vector
        var v1 = Float64Vector3D.Create(1, -1, 2);
        var v2 = Float64Vector3D.Create(-1, -1, 1.5);
        var v3 = 2.5 * v2;

        var u1 = v1;
        var u2 = v2 - v2.ProjectOnVector(u1);
        var u3 = v3 - v3.ProjectOnVector(u2) - v3.ProjectOnVector(u1);

        var u1Norm = u1.ENorm();
        var u2Norm = u2.ENorm();
        var u3Norm = u3.ENorm();

        var e1 = u1.ToUnitVector();
        var e2 = u2.ToUnitVector();
        var e3 = u3.ToUnitVector();

        Console.WriteLine($"u1 = {u1}");
        Console.WriteLine($"u2 = {u2}");
        Console.WriteLine($"u3 = {u3}");
        Console.WriteLine();

        Console.WriteLine($"u1Norm = {u1Norm:G}");
        Console.WriteLine($"u2Norm = {u2Norm:G}");
        Console.WriteLine($"u3Norm = {u3Norm:G}");
        Console.WriteLine();

        Console.WriteLine($"e1 = {e1}");
        Console.WriteLine($"e2 = {e2}");
        Console.WriteLine($"e3 = {e3}");
        Console.WriteLine();

        var gsFrame = Float64GramSchmidtFrame3D.Create(v1, v2, v3);

        var vDsMatrix = Matrix<double>.Build.DenseOfArray(
            new double[,]
            {
                { v1.X, v2.X, v3.X },
                { v1.Y, v2.Y, v3.Y },
                { v1.Z, v2.Z, v3.Z }
            }
        );

        var gramSchmidt = vDsMatrix.GramSchmidt();
            
        Console.WriteLine("Q:");
        Console.WriteLine(gramSchmidt.Q);
        Console.WriteLine();

        Console.WriteLine("R:");
        Console.WriteLine(gramSchmidt.R);
        Console.WriteLine();

        Console.WriteLine($"Det(Q) = {gramSchmidt.Q.Determinant():G}");
        Console.WriteLine();
    }

    public static void Example2()
    {
        // 3 Orthogonal vectors
        //var v1 = new Float64Tuple3D(1, -1, 2);
        //var v2 = v1.GetNormal();
        //var v3 = v1.VectorCross(v2);

        // 2 non-orthogonal vectors and their cross product
        //var v1 = new Float64Tuple3D(1, -1, 2);
        //var v2 = new Float64Tuple3D(-1, -1, 1.5);
        //var v3 = v1.VectorCross(v2);
            
        // 3 non-orthogonal vectors
        var v1 = Float64Vector3D.Create(1, -1, 2);
        var v2 = Float64Vector3D.Create(-1, -1, 1.5);
        var v3 = Float64Vector3D.Create(2, 1, 1.5);

        // 2 Orthogonal vectors and one parallel to 1st vector
        //var v1 = new Float64Tuple3D(1, -1, 2);
        //var v2 = v1.GetNormal();
        //var v3 = 2.5 * v1;
            
        // 2 non-orthogonal vectors and one parallel to 2nd vector
        //var v1 = new Float64Tuple3D(1, -1, 2);
        //var v2 = new Float64Tuple3D(-1, -1, 1.5);
        //var v3 = 2.5 * v2;

        var u1 = v1;
        var u2 = v2 - v2.ProjectOnVector(u1);
        var u3 = v3 - v3.ProjectOnVector(u2) - v3.ProjectOnVector(u1);

        var u1Norm = u1.ENorm();
        var u2Norm = u2.ENorm();
        var u3Norm = u3.ENorm();

        var e1 = u1.ToUnitVector();
        var e2 = u2.ToUnitVector();
        var e3 = u3.ToUnitVector();

        Console.WriteLine($"u1 = {u1}");
        Console.WriteLine($"u2 = {u2}");
        Console.WriteLine($"u3 = {u3}");
        Console.WriteLine();

        Console.WriteLine($"u1Norm = {u1Norm:G}");
        Console.WriteLine($"u2Norm = {u2Norm:G}");
        Console.WriteLine($"u3Norm = {u3Norm:G}");
        Console.WriteLine();

        Console.WriteLine($"e1 = {e1}");
        Console.WriteLine($"e2 = {e2}");
        Console.WriteLine($"e3 = {e3}");
        Console.WriteLine();

        var gsFrame = Float64GramSchmidtFrame3D.Create(v1, v2, v3);
            
        Console.WriteLine($"u1 = {gsFrame.GetDirection1()}");
        Console.WriteLine($"u2 = {gsFrame.GetDirection2()}");
        Console.WriteLine($"u3 = {gsFrame.GetDirection3()}");
        Console.WriteLine();

        Console.WriteLine($"u1Norm = {gsFrame.Direction1Norm:G}");
        Console.WriteLine($"u2Norm = {gsFrame.Direction2Norm:G}");
        Console.WriteLine($"u3Norm = {gsFrame.Direction3Norm:G}");
        Console.WriteLine();

        Console.WriteLine($"e1 = {gsFrame.UnitDirection1}");
        Console.WriteLine($"e2 = {gsFrame.UnitDirection2}");
        Console.WriteLine($"e3 = {gsFrame.UnitDirection3}");
        Console.WriteLine();
            
        Console.WriteLine($"kappa1 = {gsFrame.GetCurvature12():G}");
        Console.WriteLine($"kappa2 = {gsFrame.GetCurvature23():G}");
        Console.WriteLine();

        Console.WriteLine($"DB1 = {gsFrame.GetDarbouxBlade12()}");
        Console.WriteLine($"DB2 = {gsFrame.GetDarbouxBlade23()}");
        Console.WriteLine($"Darboux Bivector = {gsFrame.GetDarbouxBivector()}");
        Console.WriteLine();
    }
        
    public static void Example3()
    {
        var metric = XGaFloat64Processor.Euclidean;

        // 3 Orthogonal vectors
        //var v1 = new Float64Tuple3D(1, -1, 2);
        //var v2 = v1.GetNormal();
        //var v3 = v1.VectorCross(v2);

        // 2 non-orthogonal vectors and their cross product
        //var v1 = new Float64Tuple3D(1, -1, 2);
        //var v2 = new Float64Tuple3D(-1, -1, 1.5);
        //var v3 = v1.VectorCross(v2);
            
        // 3 non-orthogonal vectors
        var v1 = Float64Vector3D.Create(1, -1, 2);
        var v2 = Float64Vector3D.Create(-1, -1, 1.5);
        var v3 = Float64Vector3D.Create(2, 1, 1.5);

        // 2 Orthogonal vectors and one parallel to 1st vector
        //var v1 = new Float64Tuple3D(1, -1, 2);
        //var v2 = v1.GetNormal();
        //var v3 = 2.5 * v1;
            
        // 2 non-orthogonal vectors and one parallel to 2nd vector
        //var v1 = new Float64Tuple3D(1, -1, 2);
        //var v2 = new Float64Tuple3D(-1, -1, 1.5);
        //var v3 = 2.5 * v2;

        var u1 = v1;
        var u2 = v2 - v2.ProjectOnVector(u1);
        var u3 = v3 - v3.ProjectOnVector(u2) - v3.ProjectOnVector(u1);

        var u1Norm = u1.ENorm();
        var u2Norm = u2.ENorm();
        var u3Norm = u3.ENorm();

        var e1 = u1.ToUnitVector();
        var e2 = u2.ToUnitVector();
        var e3 = u3.ToUnitVector();

        Console.WriteLine($"u1 = {u1}");
        Console.WriteLine($"u2 = {u2}");
        Console.WriteLine($"u3 = {u3}");
        Console.WriteLine();

        Console.WriteLine($"u1Norm = {u1Norm:G}");
        Console.WriteLine($"u2Norm = {u2Norm:G}");
        Console.WriteLine($"u3Norm = {u3Norm:G}");
        Console.WriteLine();

        Console.WriteLine($"e1 = {e1}");
        Console.WriteLine($"e2 = {e2}");
        Console.WriteLine($"e3 = {e3}");
        Console.WriteLine();

        var gsFrame = XGaFloat64GramSchmidtFrame.Create(
            v1.ToXGaFloat64Vector(), 
            v2.ToXGaFloat64Vector(), 
            v3.ToXGaFloat64Vector()
        );
            
        Console.WriteLine($"u1 = {gsFrame.GetDirection(0)}");
        Console.WriteLine($"u2 = {gsFrame.GetDirection(1)}");
        Console.WriteLine($"u3 = {gsFrame.GetDirection(2)}");
        Console.WriteLine();

        Console.WriteLine($"u1Norm = {gsFrame.DirectionNorms[0]:G}");
        Console.WriteLine($"u2Norm = {gsFrame.DirectionNorms[1]:G}");
        Console.WriteLine($"u3Norm = {gsFrame.DirectionNorms[2]:G}");
        Console.WriteLine();

        Console.WriteLine($"e1 = {gsFrame.UnitDirections[0]}");
        Console.WriteLine($"e2 = {gsFrame.UnitDirections[1]}");
        Console.WriteLine($"e3 = {gsFrame.UnitDirections[2]}");
        Console.WriteLine();
            
        Console.WriteLine($"kappa1 = {gsFrame.GetCurvature(0):G}");
        Console.WriteLine($"kappa2 = {gsFrame.GetCurvature(1):G}");
        Console.WriteLine();

        //var b1 = metric.CreateVector(gsFrame.UnitDirections[0]);
        //var b2 = metric.CreateVector(gsFrame.UnitDirections[1]);
        //foreach (var term in b1.GetOpTerms(b2))
        //{
        //    Console.WriteLine(term);
        //}

        Console.WriteLine($"DB1 = {gsFrame.GetDarbouxBlade(0)}");
        Console.WriteLine($"DB2 = {gsFrame.GetDarbouxBlade(1)}");
        Console.WriteLine($"Darboux Bivector = {gsFrame.GetDarbouxBivector()}");
        Console.WriteLine();
    }

        
    public static void Example4()
    {
        var processor =
            RGaFloat64Processor.Euclidean;

        // 3 Orthogonal vectors
        //var v1 = new Float64Tuple3D(1, -1, 2);
        //var v2 = v1.GetNormal();
        //var v3 = v1.VectorCross(v2);

        // 2 non-orthogonal vectors and their cross product
        //var v1 = new Float64Tuple3D(1, -1, 2);
        //var v2 = new Float64Tuple3D(-1, -1, 1.5);
        //var v3 = v1.VectorCross(v2);
            
        // 3 non-orthogonal vectors
        var v1 = processor.CreateVector(1, -1, 2);
        var v2 = processor.CreateVector(-1, -1, 1.5);
        var v3 = processor.CreateVector(2, 1, 1.5);

        // 2 Orthogonal vectors and one parallel to 1st vector
        //var v1 = new Float64Tuple3D(1, -1, 2);
        //var v2 = v1.GetNormal();
        //var v3 = 2.5 * v1;
            
        // 2 non-orthogonal vectors and one parallel to 2nd vector
        //var v1 = new Float64Tuple3D(1, -1, 2);
        //var v2 = new Float64Tuple3D(-1, -1, 1.5);
        //var v3 = 2.5 * v2;

        var u1 = v1;
        var u2 = v2 - v2.ProjectOnVector(u1);
        var u3 = v3 - v3.ProjectOnVector(u2) - v3.ProjectOnVector(u1);

        var u1Norm = u1.Norm().ScalarValue();
        var u2Norm = u2.Norm().ScalarValue();
        var u3Norm = u3.Norm().ScalarValue();

        var e1 = u1.DivideByENorm();
        var e2 = u2.DivideByENorm();
        var e3 = u3.DivideByENorm();

        Console.WriteLine($"u1 = {u1}");
        Console.WriteLine($"u2 = {u2}");
        Console.WriteLine($"u3 = {u3}");
        Console.WriteLine();

        Console.WriteLine($"u1Norm = {u1Norm:G}");
        Console.WriteLine($"u2Norm = {u2Norm:G}");
        Console.WriteLine($"u3Norm = {u3Norm:G}");
        Console.WriteLine();

        Console.WriteLine($"e1 = {e1}");
        Console.WriteLine($"e2 = {e2}");
        Console.WriteLine($"e3 = {e3}");
        Console.WriteLine();

        var gsFrame = 
            RGaFloat64GramSchmidtFrame.Create(v1, v2, v3);
            
        Console.WriteLine($"u1 = {gsFrame.GetDirection(0)}");
        Console.WriteLine($"u2 = {gsFrame.GetDirection(1)}");
        Console.WriteLine($"u3 = {gsFrame.GetDirection(2)}");
        Console.WriteLine();

        Console.WriteLine($"u1Norm = {gsFrame.DirectionNorms[0]:G}");
        Console.WriteLine($"u2Norm = {gsFrame.DirectionNorms[1]:G}");
        Console.WriteLine($"u3Norm = {gsFrame.DirectionNorms[2]:G}");
        Console.WriteLine();

        Console.WriteLine($"e1 = {gsFrame.UnitDirections[0]}");
        Console.WriteLine($"e2 = {gsFrame.UnitDirections[1]}");
        Console.WriteLine($"e3 = {gsFrame.UnitDirections[2]}");
        Console.WriteLine();
            
        Console.WriteLine($"kappa1 = {gsFrame.GetCurvature(0):G}");
        Console.WriteLine($"kappa2 = {gsFrame.GetCurvature(1):G}");
        Console.WriteLine();
            
        Console.WriteLine($"DB1 = {gsFrame.GetDarbouxBlade(0)}");
        Console.WriteLine($"DB2 = {gsFrame.GetDarbouxBlade(1)}");
        Console.WriteLine($"Darboux Bivector = {gsFrame.GetDarbouxBivector()}");
        Console.WriteLine();
    }
}