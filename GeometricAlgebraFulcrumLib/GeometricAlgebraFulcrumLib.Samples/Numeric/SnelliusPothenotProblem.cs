using System;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Samples.Numeric;

/// <summary>
/// https://en.wikipedia.org/wiki/Snellius%E2%80%93Pothenot_problem
/// </summary>
public static class SnelliusPothenotProblem
{
    public static void Execute()
    {
        var pointA = Float64Vector2D.Create(-4, 3);
        var pointB = Float64Vector2D.Create(0, 5);
        var pointC = Float64Vector2D.Create(7, 4);
        var pointP = Float64Vector2D.Create(1, 0);

        var (angleAlpha, angleBeta, originIndex) = 
            GetAngles(pointA, pointB, pointC, pointP);

        var initialSolution = 
            Solve(pointA, pointB, pointC, originIndex, angleAlpha, angleBeta);
            
        var time1 = DateTime.Now;
        for (var i = 0; i < 1000000; i++)
        {
            var (solutionFound, point) = 
                Solve(pointA, pointB, pointC, originIndex, angleAlpha, angleBeta);
        }
        var time2 = DateTime.Now;

        Console.WriteLine(time2 - time1);
    }

        
    public static Tuple<Float64PlanarAngle, Float64PlanarAngle, int> GetAngles(Float64Vector2D pointA, Float64Vector2D pointB, Float64Vector2D pointC, Float64Vector2D pointP)
    {
        var ua = pointA - pointP;
        var ub = pointB - pointP;
        var uc = pointC - pointP;

        //Angles at which P sees the beacons
        var theta_ua = ua.GetPolarAngle();
        var theta_ub = ub.GetPolarAngle();
        var theta_uc = uc.GetPolarAngle();

        var alpha = theta_ua - theta_ub;
        var beta = theta_ub - theta_uc;
        var originIndex = 2; //2 = B

        // If beta or alpha are null we must change the origin
        if (beta.IsNearZero())
        {
            alpha = theta_ub - theta_ua;
            beta = theta_ua - theta_uc;
            originIndex = 1; //1 = A
        }
        else if (alpha.IsNearZero())
        {
            alpha = theta_ua - theta_uc;
            beta = theta_uc - theta_ub;
            originIndex = 3; //3 = C
        }
            
        //Console.WriteLine();
        //Console.WriteLine($"Angles from P  -----> A:{theta_ua*180/Math.PI}, B:{theta_ub*180/Math.PI}, C:{theta_uc*180/Math.PI}");
        //Console.WriteLine($"alpha: {alpha*180/Math.PI}");
        //Console.WriteLine($"beta: {beta*180/Math.PI}");
        //Console.WriteLine($"origin: {origin}");
        //Console.WriteLine();

        return new Tuple<Float64PlanarAngle, Float64PlanarAngle, int>(
            alpha,
            beta,
            originIndex
        );
    }

    public static Tuple<bool, Float64Vector2D> Solve(Float64Vector2D pointA, Float64Vector2D pointB, Float64Vector2D pointC, int originIndex, Float64PlanarAngle angleAlpha, Float64PlanarAngle angleBeta)
    {
        //select the central beacon depending on the origin
        Float64Vector2D center, v1, v2;

        if (originIndex == 1)
        {
            center = pointA;
            v1 = pointB - pointA;
            v2 = pointC - pointA;
        }
        else if (originIndex == 2)
        {
            center = pointB;
            v1 = pointA - pointB;
            v2 = pointC - pointB;
        }
        else
        {
            center = pointC;
            v1 = pointA - pointC;
            v2 = pointB - pointC;
        }
            
        var e12 = Float64Bivector2D.E12;

        // Calculate the vector p
        var d1 = v1 + (v1 / angleAlpha.Tan()).Gp(e12);
        var d2 = v2 - (v2 / angleBeta.Tan()).Gp(e12);

        var d = d2 - d1;

        if (d.IsNearZero())
            return new Tuple<bool, Float64Vector2D>(false, Float64Vector2D.Zero);

        var dInv = d1.Inverse();
            
        var p = d1.Op(d).Gp(dInv);
            
        return new Tuple<bool, Float64Vector2D>(true, center + p);
    }
}