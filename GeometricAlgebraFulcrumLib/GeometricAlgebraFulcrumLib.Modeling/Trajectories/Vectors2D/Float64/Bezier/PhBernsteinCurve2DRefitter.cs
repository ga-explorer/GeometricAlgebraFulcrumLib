using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Optimization;
using System.Diagnostics;
using System.Numerics;
using GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.Bernstein;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier;

public static class PhBernsteinCurve2DRefitter
{
    public class RefitResult
    {
        public required PhBernsteinCurve2D.GeneratorData Generators { get; init; }

        public bool Converged { get; init; }

        public int Iterations { get; init; }

        public double FinalPositionResidual { get; init; }

        public double FinalTangentResidual { get; init; }

        public double FinalAccelerationResidual { get; init; }

        public double FinalAreaResidual { get; set; }

        public override string ToString()
            => $"Converged={Converged}, Iter={Iterations}, PosRes={FinalPositionResidual:E3}, TanRes={FinalTangentResidual:E3}, AccRes={FinalAccelerationResidual:E3}, AreaRes={FinalAreaResidual:E3}";
    }

    public static RefitResult RefitStartTangentAccelerationAndEndpoint(
        PhBernsteinCurve2D.GeneratorData generators,
        LinFloat64Vector2D p0,
        LinFloat64Vector2D p1,
        LinFloat64Vector2D s0, // start tangent (derivative)
        LinFloat64Vector2D a0, // start acceleration
        double endpointTolerance = 1e-7,
        double tangentTolerance = 1e-7,
        double accelTolerance = 1e-6,
        int maxIterations = 500
    )
    {
        var uGen = generators.U;
        var vGen = generators.V;
        var wGen = generators.W;

        var paramIndices = new List<(char PolyType, int Index)>();
        for (var i = 1; i < uGen.Degree; i++) paramIndices.Add(('u', i));
        for (var i = 1; i < vGen.Degree; i++) paramIndices.Add(('v', i));
        for (var i = 1; i < wGen.Degree; i++) paramIndices.Add(('w', i));

        var initialParams = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(paramIndices.Count, i =>
        {
            var (polyType, idx) = paramIndices[i];
            return polyType switch
            {
                'u' => uGen.Coeffs[idx],
                'v' => vGen.Coeffs[idx],
                'w' => wGen.Coeffs[idx],
                _ => 0.0
            };
        });

        var xData = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(6);
        var yData = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(6);

        var objective = ObjectiveFunction.NonlinearModel(
            ResidualModel, ResidualJacobian, xData, yData
        );

        var solver = new LevenbergMarquardtMinimizer(
            gradientTolerance: 1e-12,
            functionTolerance: 1e-12,
            maximumIterations: maxIterations
        );

        var result = solver.FindMinimum(objective, initialParams);
        var bestParams = result.MinimizingPoint;

        // build final coefficients
        var uCoeffs = uGen.Coeffs.ToArray();
        var vCoeffs = vGen.Coeffs.ToArray();
        var wCoeffs = wGen.Coeffs.ToArray();

        for (var p = 0; p < paramIndices.Count; p++)
        {
            var (polyType, idx) = paramIndices[p];
            var val = bestParams[p];
            switch (polyType)
            {
                case 'u': uCoeffs[idx] = val; break;
                case 'v': vCoeffs[idx] = val; break;
                case 'w': wCoeffs[idx] = val; break;
            }
        }

        var uFinal = BernsteinPolynomial.Create(uCoeffs);
        var vFinal = BernsteinPolynomial.Create(vCoeffs);
        var wFinal = BernsteinPolynomial.Create(wCoeffs);

        var residuals = ResidualModel(bestParams, xData);

        return new RefitResult
        {
            Generators = new PhBernsteinCurve2D.GeneratorData
            {
                U = uFinal,
                V = vFinal,
                W = wFinal,
                P0 = p0,
                S0 = generators.S0
            },
            Converged = result.ReasonForExit == ExitCondition.Converged,
            Iterations = result.Iterations,
            FinalPositionResidual = Math.Sqrt(residuals[0] * residuals[0] + residuals[1] * residuals[1]),
            FinalTangentResidual = Math.Sqrt(residuals[2] * residuals[2] + residuals[3] * residuals[3]),
            FinalAccelerationResidual = Math.Sqrt(residuals[4] * residuals[4] + residuals[5] * residuals[5])
        };

        // ==========================
        // Local Helper Functions
        // ==========================

        MathNet.Numerics.LinearAlgebra.Vector<double> ResidualModel(
            MathNet.Numerics.LinearAlgebra.Vector<double> p,
            MathNet.Numerics.LinearAlgebra.Vector<double> _)
        {
            var uC = (double[])uGen.Coeffs.ToArray();
            var vC = (double[])vGen.Coeffs.ToArray();
            var wC = (double[])wGen.Coeffs.ToArray();

            for (var i = 0; i < paramIndices.Count; i++)
            {
                var (polyType, idx) = paramIndices[i];
                var val = p[i];
                switch (polyType)
                {
                    case 'u': uC[idx] = val; break;
                    case 'v': vC[idx] = val; break;
                    case 'w': wC[idx] = val; break;
                }
            }

            var u = BernsteinPolynomial.Create(uC);
            var v = BernsteinPolynomial.Create(vC);
            var w = BernsteinPolynomial.Create(wC);

            // PH derivatives
            var xPrime = w * (u.Square() - v.Square());
            var yPrime = w * (u * v * 2.0);

            var xSecond = xPrime.Derivative();
            var ySecond = yPrime.Derivative();

            var xPoly = xPrime.Integral().Add(p0.X);
            var yPoly = yPrime.Integral().Add(p0.Y);

            var p1Est = LinFloat64Vector2D.Create(xPoly.GetValue(1.0), yPoly.GetValue(1.0));
            var s0Est = LinFloat64Vector2D.Create(xPrime.GetValue(0.0), yPrime.GetValue(0.0));
            var a0Est = LinFloat64Vector2D.Create(xSecond.GetValue(0.0), ySecond.GetValue(0.0));

            var r = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(6);
            r[0] = p1Est.X - p1.X;
            r[1] = p1Est.Y - p1.Y;
            r[2] = s0Est.X - s0.X;
            r[3] = s0Est.Y - s0.Y;
            r[4] = a0Est.X - a0.X;
            r[5] = a0Est.Y - a0.Y;

            return r;
        }

        Matrix<double> ResidualJacobian(MathNet.Numerics.LinearAlgebra.Vector<double> p, MathNet.Numerics.LinearAlgebra.Vector<double> x)
        {
            const double eps = 1e-8;
            var n = p.Count;
            var f0 = ResidualModel(p, x);
            var J = Matrix<double>.Build.Dense(6, n);

            for (var j = 0; j < n; j++)
            {
                var pPert = p.Clone();
                var dp = Math.Max(Math.Abs(p[j]) * eps, eps);
                pPert[j] += dp;
                var f1 = ResidualModel(pPert, x);
                for (var i = 0; i < 6; i++)
                    J[i, j] = (f1[i] - f0[i]) / dp;
            }
            return J;
        }
    }





    public static RefitResult RefitEndpointTangentTransformGeneralNumerical(
        PhBernsteinCurve2D.GeneratorData generators,
        LinFloat64Vector2D p0,
        LinFloat64Vector2D p1,
        double scalingFactor,
        double rotateAngleTheta,
        bool adjustAllInteriorCoeffs = true,
        double endpointTolerance = 1e-7,
        int maxRefinementIterations = 8,
        int lmMaxIterations = 500
    )
    {
        if (scalingFactor <= 0)
            throw new ArgumentException("Scaling factor must be positive.", nameof(scalingFactor));

        // Enhanced initialization with better starting point
        var sqrtS = Math.Sqrt(scalingFactor);
        var halfAngle = rotateAngleTheta / 2.0;
        var complexRotHalf = Complex.FromPolarCoordinates(sqrtS, halfAngle);

        var uGen = generators.U;
        var vGen = generators.V;
        var wGen = generators.W;

        // Transformed endpoint values for u,v (complex multiply)
        var z0 = GetZ0(uGen, vGen);
        var zN = GetZN(uGen, vGen);
        var z0P = z0 * complexRotHalf;
        var zNP = zN * complexRotHalf;

        // Enhanced W polynomial endpoint handling
        var w0P = generators.W.Coeffs[0];
        var wNP = generators.W.Coeffs[wGen.Degree];

        var uDegree = uGen.Degree;
        var vDegree = vGen.Degree;
        var wDegree = wGen.Degree;

        var uTarget = new double[uDegree + 1];
        var vTarget = new double[vDegree + 1];
        var wTarget = new double[wDegree + 1];

        // Set transformed endpoints
        uTarget[0] = z0P.Real;
        vTarget[0] = z0P.Imaginary;
        uTarget[uDegree] = zNP.Real;
        vTarget[vDegree] = zNP.Imaginary;

        wTarget[0] = w0P;
        wTarget[wDegree] = wNP;

        // Enhanced parameter selection with degree validation
        var paramIndices = new List<(char PolyType, int Index)>();

        if (adjustAllInteriorCoeffs)
        {
            for (var i = 1; i < uDegree; i++) paramIndices.Add(('u', i));
            for (var i = 1; i < vDegree; i++) paramIndices.Add(('v', i));
            for (var i = 1; i < wDegree; i++) paramIndices.Add(('w', i));
        }
        else
        {
            // Smart parameter selection based on polynomial degrees
            // Prioritize higher degree polynomials for better control
            var maxDegree = Math.Max(Math.Max(uDegree, vDegree), wDegree);
            for (var i = 1; i < uDegree; i++)
                if (uDegree >= maxDegree - 1) paramIndices.Add(('u', i));
            for (var i = 1; i < vDegree; i++)
                if (vDegree >= maxDegree - 1) paramIndices.Add(('v', i));
            for (var i = 1; i < wDegree; i++)
                paramIndices.Add(('w', i)); // Always include W coefficients
        }

        // Enhanced initial parameter estimation
        var initialParams = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(paramIndices.Count);
        for (var p = 0; p < paramIndices.Count; p++)
        {
            var (polyType, idx) = paramIndices[p];
            var currentValue = polyType switch
            {
                'u' => uGen.Coeffs[idx],
                'v' => vGen.Coeffs[idx],
                'w' => wGen.Coeffs[idx],
                _ => throw new InvalidOperationException($"Unknown polynomial type: {polyType}")
            };

            // Apply intelligent scaling to initial parameters
            var scaledValue = polyType switch
            {
                'u' or 'v' => currentValue * sqrtS, // Scale U,V by sqrt(scale)
                'w' => currentValue * scalingFactor, // Scale W by full scale
                _ => currentValue
            };

            initialParams[p] = scaledValue;
        }

        var current = initialParams.Clone();
        var converged = false;
        var finalPosRes = double.PositiveInfinity;
        var iterCount = 0;

        var xData = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(2);
        var yData = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(2);

        // Enhanced optimization loop with progress tracking
        for (iterCount = 1; iterCount <= maxRefinementIterations; iterCount++)
        {
            try
            {
                var obj = ObjectiveFunction.NonlinearModel(
                    EndpointResidualModel,
                    EndpointResidualJacobian,
                    xData,
                    yData
                );

                var solver = new LevenbergMarquardtMinimizer(
                    gradientTolerance: 1e-12,
                    functionTolerance: 1e-12,
                    maximumIterations: lmMaxIterations
                );

                var result = solver.FindMinimum(obj, current);
                current = result.MinimizingPoint.Clone();

                var resVec = EndpointResidualModel(current, xData);
                finalPosRes = resVec.L2Norm();

                if (finalPosRes <= endpointTolerance)
                {
                    converged = true;
                    break;
                }

                // Early termination if no significant improvement
                if (iterCount > 1 && finalPosRes > 0.9 * finalPosRes)
                    break;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Refinement iteration {iterCount} failed: {ex.Message}");
                break;
            }
        }

        // Reconstruct final polynomials with validation
        var uFinalCoeffs = (double[])uTarget.Clone();
        var vFinalCoeffs = (double[])vTarget.Clone();
        var wFinalCoeffs = (double[])wTarget.Clone();

        for (var p = 0; p < paramIndices.Count; p++)
        {
            var (polyType, idx) = paramIndices[p];
            var val = current[p];

            // Apply bounds checking for stability
            var boundedVal = polyType switch
            {
                'u' or 'v' => Math.Clamp(val, -10.0, 10.0), // Reasonable bounds for U,V
                'w' => Math.Max(val, 0.01), // W must be positive
                _ => val
            };

            switch (polyType)
            {
                case 'u': uFinalCoeffs[idx] = boundedVal; break;
                case 'v': vFinalCoeffs[idx] = boundedVal; break;
                case 'w': wFinalCoeffs[idx] = boundedVal; break;
            }
        }

        var uFinal = BernsteinPolynomial.Create(uFinalCoeffs);
        var vFinal = BernsteinPolynomial.Create(vFinalCoeffs);
        var wFinal = BernsteinPolynomial.Create(wFinalCoeffs);

        return new RefitResult
        {
            Generators = new PhBernsteinCurve2D.GeneratorData()
            {
                U = uFinal,
                V = vFinal,
                W = wFinal,
                P0 = p0,
                S0 = generators.S0
            },
            Converged = converged,
            Iterations = iterCount,
            FinalPositionResidual = finalPosRes,
            FinalTangentResidual = 0, // Not computed in this method
            FinalAccelerationResidual = 0
        };

        // Local helper functions
        Complex GetZ0(BernsteinPolynomial u, BernsteinPolynomial v)
            => new Complex(u.Coeffs[0], v.Coeffs[0]);

        Complex GetZN(BernsteinPolynomial u, BernsteinPolynomial v)
        {
            var uN = u.Coeffs[u.Degree];
            var vN = v.Coeffs[v.Degree];
            return new Complex(uN, vN);
        }

        MathNet.Numerics.LinearAlgebra.Vector<double> EndpointResidualModel(MathNet.Numerics.LinearAlgebra.Vector<double> paramVec, MathNet.Numerics.LinearAlgebra.Vector<double> x)
        {
            var uCoeffs = (double[])uTarget.Clone();
            var vCoeffs = (double[])vTarget.Clone();
            var wCoeffs = (double[])wTarget.Clone();

            for (var p = 0; p < paramIndices.Count; p++)
            {
                var (polyType, idx) = paramIndices[p];
                var val = paramVec[p];
                switch (polyType)
                {
                    case 'u': uCoeffs[idx] = val; break;
                    case 'v': vCoeffs[idx] = val; break;
                    case 'w': wCoeffs[idx] = val; break;
                }
            }

            var uNew = BernsteinPolynomial.Create(uCoeffs);
            var vNew = BernsteinPolynomial.Create(vCoeffs);
            var wNew = BernsteinPolynomial.Create(wCoeffs);

            var xPrime = wNew * (uNew.Square() - vNew.Square());
            var yPrime = (wNew * (uNew * vNew)).Times(2.0);

            var xPoly = xPrime.Integral().Add(p0.X);
            var yPoly = yPrime.Integral().Add(p0.Y);

            var p1EstX = xPoly.GetValue(1.0);
            var p1EstY = yPoly.GetValue(1.0);

            var r = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(2);
            r[0] = p1EstX - p1.X;
            r[1] = p1EstY - p1.Y;
            return r;
        }

        Matrix<double> EndpointResidualJacobian(MathNet.Numerics.LinearAlgebra.Vector<double> p, MathNet.Numerics.LinearAlgebra.Vector<double> x)
        {
            const int nResiduals = 2;
            const double eps = 1e-8;
            var nParams = p.Count;
            var jac = Matrix<double>.Build.Dense(nResiduals, nParams);

            var f0 = EndpointResidualModel(p, x);
            for (var j = 0; j < nParams; j++)
            {
                var pPert = p.Clone();
                var dp = Math.Max(Math.Abs(p[j]) * eps, eps);
                pPert[j] += dp;
                var f1 = EndpointResidualModel(pPert, x);

                for (var i = 0; i < nResiduals; i++)
                    jac[i, j] = (f1[i] - f0[i]) / dp;
            }

            return jac;
        }
    }

    public static RefitResult RefitEndpointTangentAccelerationTransformGeneralNumerical(
        PhBernsteinCurve2D.GeneratorData generators,
        LinFloat64Vector2D p0, // target start point (after map)
        LinFloat64Vector2D p1, // target end point (after map)
        double scalingFactor,
        double rotateAngleTheta,
        LinFloat64Vector2D desiredT0, // desired tangent at t=0 after mapping
        LinFloat64Vector2D desiredT1, // desired tangent at t=1 after mapping
        LinFloat64Vector2D desiredA0, // desired acceleration at t=0 after mapping
        LinFloat64Vector2D desiredA1, // desired acceleration at t=1 after mapping
        bool adjustAllInteriorCoeffs = true,
        double endpointTolerance = 1e-7,
        int maxRefinementIterations = 8,
        int lmMaxIterations = 500
    )
    {
        if (scalingFactor <= 0)
            throw new ArgumentException("scalingFactor must be positive.");

        // compute complex half-rotation/scale factor (applies to z = u + i v)
        var sqrtS = Math.Sqrt(scalingFactor);
        var halfAngle = rotateAngleTheta / 2.0;
        var complexRotHalf = Complex.FromPolarCoordinates(sqrtS, halfAngle);

        var uGen = generators.U;
        var vGen = generators.V;
        var wGen = generators.W;

        var z0 = GetZ0(uGen, vGen);
        var zN = GetZN(uGen, vGen);
        var z0P = z0 * complexRotHalf;
        var zNP = zN * complexRotHalf;

        // w endpoints (kept as-is scaled by scale? previous code kept the same)
        var w0P = wGen.Coeffs[0];
        var wNP = wGen.Coeffs[wGen.Degree];

        // Prepare coefficient arrays with fixed endpoints
        var uDegree = uGen.Degree;
        var vDegree = vGen.Degree;
        var wDegree = wGen.Degree;

        var uTarget = new double[uDegree + 1];
        var vTarget = new double[vDegree + 1];
        var wTarget = new double[wDegree + 1];

        uTarget[0] = z0P.Real;
        vTarget[0] = z0P.Imaginary;
        uTarget[uDegree] = zNP.Real;
        vTarget[vDegree] = zNP.Imaginary;

        wTarget[0] = w0P;
        wTarget[wDegree] = wNP;

        // assemble paramIndices like other refitters (interior coefficients)
        var paramIndices = new List<(char poly, int idx)>();
        if (adjustAllInteriorCoeffs)
        {
            for (var i = 1; i <= Math.Max(0, uDegree - 1); i++) paramIndices.Add(('u', i));
            for (var i = 1; i <= Math.Max(0, vDegree - 1); i++) paramIndices.Add(('v', i));
            for (var i = 1; i <= Math.Max(0, wDegree - 1); i++) paramIndices.Add(('w', i));
        }
        else
        {
            // same default behavior (you can refine selection)
            for (var i = 1; i <= Math.Max(0, uDegree - 1); i++) paramIndices.Add(('u', i));
            for (var i = 1; i <= Math.Max(0, vDegree - 1); i++) paramIndices.Add(('v', i));
            for (var i = 1; i <= Math.Max(0, wDegree - 1); i++) paramIndices.Add(('w', i));
        }

        // build initial parameter vector from current interior coefficients
        var initialParams = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(paramIndices.Count);
        for (var p = 0; p < paramIndices.Count; p++)
        {
            var (poly, idx) = paramIndices[p];
            initialParams[p] = poly switch
            {
                'u' => uGen.Coeffs[idx],
                'v' => vGen.Coeffs[idx],
                'w' => wGen.Coeffs[idx],
                _ => throw new InvalidOperationException()
            };
        }

        var current = initialParams.Clone();
        var converged = false;
        var finalRes = double.PositiveInfinity;
        var iterCount = 0;

        // LM outer loop
        var xData = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(10); // dummy
        for (iterCount = 1; iterCount <= maxRefinementIterations; iterCount++)
        {
            // objective with numerical jacobian wrapper
            var obj = ObjectiveFunction.NonlinearModel(
                EndpointResidualModel,
                EndpointResidualJacobian,
                xData,
                xData
            );

            var solver = new LevenbergMarquardtMinimizer(
                gradientTolerance: 1e-14,
                functionTolerance: 1e-14,
                maximumIterations: lmMaxIterations
            );

            NonlinearMinimizationResult result;
            try
            {
                result = solver.FindMinimum(obj, current);
            }
            catch
            {
                break;
            }

            current = result.MinimizingPoint.Clone();

            var resVec = EndpointResidualModel(current, xData);
            finalRes = resVec.L2Norm();

            if (finalRes <= endpointTolerance)
            {
                converged = true;
                break;
            }
        }

        // Reconstruct final polynomials
        var uFinalCoeffs = (double[])uTarget.Clone();
        var vFinalCoeffs = (double[])vTarget.Clone();
        var wFinalCoeffs = (double[])wTarget.Clone();

        for (var p = 0; p < paramIndices.Count; p++)
        {
            var (poly, idx) = paramIndices[p];
            var val = current[p];
            switch (poly)
            {
                case 'u': uFinalCoeffs[idx] = val; break;
                case 'v': vFinalCoeffs[idx] = val; break;
                case 'w': wFinalCoeffs[idx] = val; break;
            }
        }

        var uFinal = BernsteinPolynomial.Create(uFinalCoeffs);
        var vFinal = BernsteinPolynomial.Create(vFinalCoeffs);
        var wFinal = BernsteinPolynomial.Create(wFinalCoeffs);

        return new RefitResult
        {
            Generators = new PhBernsteinCurve2D.GeneratorData()
            {
                U = uFinal,
                V = vFinal,
                W = wFinal,
                P0 = p0,
                S0 = generators.S0
            },
            Converged = converged,
            Iterations = iterCount,
            FinalPositionResidual = finalRes
        };

        // Transform endpoint complex z values using half-rotation-scale
        Complex GetZ0(BernsteinPolynomial u, BernsteinPolynomial v)
            => new Complex(u.Coeffs[0], v.Coeffs[0]);

        Complex GetZN(BernsteinPolynomial u, BernsteinPolynomial v)
            => new Complex(u.Coeffs[u.Degree], v.Coeffs[v.Degree]);

        // -----------------------
        // Local helper models
        // -----------------------
        // Residual model: returns 10-vector
        // order: [posDiff.x, posDiff.y,
        //         t0Diff.x, t0Diff.y,
        //         t1Diff.x, t1Diff.y,
        //         a0Diff.x, a0Diff.y,
        //         a1Diff.x, a1Diff.y]
        MathNet.Numerics.LinearAlgebra.Vector<double> EndpointResidualModel(MathNet.Numerics.LinearAlgebra.Vector<double> paramVec, MathNet.Numerics.LinearAlgebra.Vector<double> _)
        {
            // Reconstruct full coefficient arrays
            var uCoeffs = (double[])uTarget.Clone();
            var vCoeffs = (double[])vTarget.Clone();
            var wCoeffs = (double[])wTarget.Clone();

            for (var p = 0; p < paramIndices.Count; p++)
            {
                var (poly, idx) = paramIndices[p];
                var val = paramVec[p];
                switch (poly)
                {
                    case 'u': uCoeffs[idx] = val; break;
                    case 'v': vCoeffs[idx] = val; break;
                    case 'w': wCoeffs[idx] = val; break;
                }
            }

            var uNew = BernsteinPolynomial.Create(uCoeffs);
            var vNew = BernsteinPolynomial.Create(vCoeffs);
            var wNew = BernsteinPolynomial.Create(wCoeffs);

            // x', y'
            var xPrime = wNew * (uNew.Square() - vNew.Square());
            var yPrime = (wNew * (uNew * vNew)).Times(2.0);

            // positions: integrate and add p0
            var xPoly = xPrime.Integral().Add(p0.X);
            var yPoly = yPrime.Integral().Add(p0.Y);

            var p1EstX = xPoly.GetValue(1.0);
            var p1EstY = yPoly.GetValue(1.0);

            // tangents: evaluate x', y' at endpoints
            var t0x = xPrime.GetValue(0.0);
            var t0y = yPrime.GetValue(0.0);
            var t1x = xPrime.GetValue(1.0);
            var t1y = yPrime.GetValue(1.0);

            // accelerations: derivatives of x', y' (i.e. x'', y'')
            var xPrimeDt = xPrime.Derivative();
            var yPrimeDt = yPrime.Derivative();

            var a0x = xPrimeDt.GetValue(0.0);
            var a0y = yPrimeDt.GetValue(0.0);
            var a1x = xPrimeDt.GetValue(1.0);
            var a1y = yPrimeDt.GetValue(1.0);

            var r = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(10);

            // position residuals (end point p1)
            r[0] = p1EstX - p1.X;
            r[1] = p1EstY - p1.Y;

            // tangent residuals (we expect tangents to match desiredT0/T1)
            r[2] = t0x - desiredT0.X;
            r[3] = t0y - desiredT0.Y;

            r[4] = t1x - desiredT1.X;
            r[5] = t1y - desiredT1.Y;

            // acceleration residuals
            r[6] = a0x - desiredA0.X;
            r[7] = a0y - desiredA0.Y;

            r[8] = a1x - desiredA1.X;
            r[9] = a1y - desiredA1.Y;

            return r;
        }

        // Numerical Jacobian by finite differences (10 x nParams)
        Matrix<double> EndpointResidualJacobian(MathNet.Numerics.LinearAlgebra.Vector<double> p, MathNet.Numerics.LinearAlgebra.Vector<double> _)
        {
            const int nResiduals = 10;
            const double eps = 1e-8;
            var nParams = p.Count;
            var jac = Matrix<double>.Build.Dense(nResiduals, nParams);

            var f0 = EndpointResidualModel(p, null);
            for (var j = 0; j < nParams; j++)
            {
                var pPert = p.Clone();
                var dp = Math.Abs(p[j]) * eps + eps;
                pPert[j] += dp;
                var f1 = EndpointResidualModel(pPert, null);

                for (var i = 0; i < nResiduals; i++)
                    jac[i, j] = (f1[i] - f0[i]) / dp;
            }

            return jac;
        }
    }
    
    public static RefitResult RefitEndpointTangentAccelerationTransformGeneralNumerical(
        PhBernsteinCurve2D.GeneratorData generators,
        LinFloat64Vector2D p0, // target start point (after map)
        LinFloat64Vector2D p1, // target end point (after map)
        LinFloat64Vector2D desiredT0, // desired tangent at t=0 after mapping
        LinFloat64Vector2D desiredT1, // desired tangent at t=1 after mapping
        LinFloat64Vector2D desiredA0, // desired acceleration at t=0 after mapping
        bool adjustAllInteriorCoeffs = true,
        double endpointTolerance = 1e-7,
        int maxRefinementIterations = 8,
        int lmMaxIterations = 500
    )
    {
        // compute complex half-rotation/scale factor (applies to z = u + i v)
        var uGen = generators.U;
        var vGen = generators.V;
        var wGen = generators.W;

        var z0 = GetZ0(uGen, vGen);
        var zN = GetZN(uGen, vGen);
        var z0P = z0;
        var zNP = zN;

        // w endpoints (kept as-is scaled by scale? previous code kept the same)
        var w0P = wGen.Coeffs[0];
        var wNP = wGen.Coeffs[wGen.Degree];

        // Prepare coefficient arrays with fixed endpoints
        var uDegree = uGen.Degree;
        var vDegree = vGen.Degree;
        var wDegree = wGen.Degree;

        var uTarget = new double[uDegree + 1];
        var vTarget = new double[vDegree + 1];
        var wTarget = new double[wDegree + 1];

        uTarget[0] = z0P.Real;
        vTarget[0] = z0P.Imaginary;
        uTarget[uDegree] = zNP.Real;
        vTarget[vDegree] = zNP.Imaginary;

        wTarget[0] = w0P;
        wTarget[wDegree] = wNP;

        // assemble paramIndices like other refitters (interior coefficients)
        var paramIndices = new List<(char poly, int idx)>();
        if (adjustAllInteriorCoeffs)
        {
            for (var i = 1; i <= Math.Max(0, uDegree - 1); i++) paramIndices.Add(('u', i));
            for (var i = 1; i <= Math.Max(0, vDegree - 1); i++) paramIndices.Add(('v', i));
            for (var i = 1; i <= Math.Max(0, wDegree - 1); i++) paramIndices.Add(('w', i));
        }
        else
        {
            // same default behavior (you can refine selection)
            for (var i = 1; i <= Math.Max(0, uDegree - 1); i++) paramIndices.Add(('u', i));
            for (var i = 1; i <= Math.Max(0, vDegree - 1); i++) paramIndices.Add(('v', i));
            for (var i = 1; i <= Math.Max(0, wDegree - 1); i++) paramIndices.Add(('w', i));
        }

        // build initial parameter vector from current interior coefficients
        var initialParams = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(paramIndices.Count);
        for (var p = 0; p < paramIndices.Count; p++)
        {
            var (poly, idx) = paramIndices[p];
            initialParams[p] = poly switch
            {
                'u' => uGen.Coeffs[idx],
                'v' => vGen.Coeffs[idx],
                'w' => wGen.Coeffs[idx],
                _ => throw new InvalidOperationException()
            };
        }

        var current = initialParams.Clone();
        var converged = false;
        var finalRes = double.PositiveInfinity;
        var iterCount = 0;

        // LM outer loop
        var xData = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(8); // dummy
        for (iterCount = 1; iterCount <= maxRefinementIterations; iterCount++)
        {
            // objective with numerical jacobian wrapper
            var obj = ObjectiveFunction.NonlinearModel(
                EndpointResidualModel,
                EndpointResidualJacobian,
                xData,
                xData
            );

            var solver = new LevenbergMarquardtMinimizer(
                gradientTolerance: 1e-14,
                functionTolerance: 1e-14,
                maximumIterations: lmMaxIterations
            );

            NonlinearMinimizationResult result;
            try
            {
                result = solver.FindMinimum(obj, current);
            }
            catch
            {
                break;
            }

            current = result.MinimizingPoint.Clone();

            var resVec = EndpointResidualModel(current, xData);
            finalRes = resVec.L2Norm();

            if (finalRes <= endpointTolerance)
            {
                converged = true;
                break;
            }
        }

        // Reconstruct final polynomials
        var uFinalCoeffs = (double[])uTarget.Clone();
        var vFinalCoeffs = (double[])vTarget.Clone();
        var wFinalCoeffs = (double[])wTarget.Clone();

        for (var p = 0; p < paramIndices.Count; p++)
        {
            var (poly, idx) = paramIndices[p];
            var val = current[p];
            switch (poly)
            {
                case 'u': uFinalCoeffs[idx] = val; break;
                case 'v': vFinalCoeffs[idx] = val; break;
                case 'w': wFinalCoeffs[idx] = val; break;
            }
        }

        var uFinal = BernsteinPolynomial.Create(uFinalCoeffs);
        var vFinal = BernsteinPolynomial.Create(vFinalCoeffs);
        var wFinal = BernsteinPolynomial.Create(wFinalCoeffs);

        return new RefitResult
        {
            Generators = new PhBernsteinCurve2D.GeneratorData()
            {
                U = uFinal,
                V = vFinal,
                W = wFinal,
                P0 = p0,
                S0 = generators.S0
            },
            Converged = converged,
            Iterations = iterCount,
            FinalPositionResidual = finalRes
        };

        // Transform endpoint complex z values using half-rotation-scale
        Complex GetZ0(BernsteinPolynomial u, BernsteinPolynomial v)
            => new Complex(u.Coeffs[0], v.Coeffs[0]);

        Complex GetZN(BernsteinPolynomial u, BernsteinPolynomial v)
            => new Complex(u.Coeffs[u.Degree], v.Coeffs[v.Degree]);

        // -----------------------
        // Local helper models
        // -----------------------
        // Residual model: returns 10-vector
        // order: [posDiff.x, posDiff.y,
        //         t0Diff.x, t0Diff.y,
        //         t1Diff.x, t1Diff.y,
        //         a0Diff.x, a0Diff.y,
        //         a1Diff.x, a1Diff.y]
        MathNet.Numerics.LinearAlgebra.Vector<double> EndpointResidualModel(MathNet.Numerics.LinearAlgebra.Vector<double> paramVec, MathNet.Numerics.LinearAlgebra.Vector<double> _)
        {
            // Reconstruct full coefficient arrays
            var uCoeffs = (double[])uTarget.Clone();
            var vCoeffs = (double[])vTarget.Clone();
            var wCoeffs = (double[])wTarget.Clone();

            for (var p = 0; p < paramIndices.Count; p++)
            {
                var (poly, idx) = paramIndices[p];
                var val = paramVec[p];
                switch (poly)
                {
                    case 'u': uCoeffs[idx] = val; break;
                    case 'v': vCoeffs[idx] = val; break;
                    case 'w': wCoeffs[idx] = val; break;
                }
            }

            var uNew = BernsteinPolynomial.Create(uCoeffs);
            var vNew = BernsteinPolynomial.Create(vCoeffs);
            var wNew = BernsteinPolynomial.Create(wCoeffs);

            // x', y'
            var xPrime = wNew * (uNew.Square() - vNew.Square());
            var yPrime = (wNew * (uNew * vNew)).Times(2.0);

            // positions: integrate and add p0
            var xPoly = xPrime.Integral().Add(p0.X);
            var yPoly = yPrime.Integral().Add(p0.Y);

            var p1EstX = xPoly.GetValue(1.0);
            var p1EstY = yPoly.GetValue(1.0);

            // tangents: evaluate x', y' at endpoints
            var t0x = xPrime.GetValue(0.0);
            var t0y = yPrime.GetValue(0.0);
            var t1x = xPrime.GetValue(1.0);
            var t1y = yPrime.GetValue(1.0);

            // accelerations: derivatives of x', y' (i.e. x'', y'')
            var xPrimeDt = xPrime.Derivative();
            var yPrimeDt = yPrime.Derivative();

            var a0x = xPrimeDt.GetValue(0.0);
            var a0y = yPrimeDt.GetValue(0.0);

            var r = 
                MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(8);

            // position residuals (end point p1)
            r[0] = p1EstX - p1.X;
            r[1] = p1EstY - p1.Y;

            // tangent residuals (we expect tangents to match desiredT0/T1)
            r[2] = t0x - desiredT0.X;
            r[3] = t0y - desiredT0.Y;

            r[4] = t1x - desiredT1.X;
            r[5] = t1y - desiredT1.Y;

            // acceleration residuals
            r[6] = a0x - desiredA0.X;
            r[7] = a0y - desiredA0.Y;

            return r;
        }

        // Numerical Jacobian by finite differences (10 x nParams)
        Matrix<double> EndpointResidualJacobian(MathNet.Numerics.LinearAlgebra.Vector<double> p, MathNet.Numerics.LinearAlgebra.Vector<double> _)
        {
            const int nResiduals = 8;
            const double eps = 1e-8;
            var nParams = p.Count;
            var jac = Matrix<double>.Build.Dense(nResiduals, nParams);

            var f0 = EndpointResidualModel(p, null);
            for (var j = 0; j < nParams; j++)
            {
                var pPert = p.Clone();
                var dp = Math.Abs(p[j]) * eps + eps;
                pPert[j] += dp;
                var f1 = EndpointResidualModel(pPert, null);

                for (var i = 0; i < nResiduals; i++)
                    jac[i, j] = (f1[i] - f0[i]) / dp;
            }

            return jac;
        }
    }

    
    public static RefitResult RefitToPoints(
        PhBernsteinCurve2D.GeneratorData generators,
        LinFloat64Vector2D p0, // target start point
        LinFloat64Vector2D p1, // target end point
        LinFloat64Vector2D pMid, // target mid-point
        LinFloat64Vector2D desiredT0, // desired tangent at t=0 after mapping
        LinFloat64Vector2D desiredT1, // desired tangent at t=1 after mapping
        LinFloat64Vector2D desiredA0, // desired acceleration at t=0 after mapping
        LinFloat64Vector2D desiredA1, // desired acceleration at t=1 after mapping
        double endpointTolerance = 1e-7,
        int maxRefinementIterations = 8,
        int lmMaxIterations = 500
    )
    {
        var uGen = generators.U;
        var vGen = generators.V;
        var wGen = generators.W;

        // Prepare coefficient arrays with fixed endpoints
        var uDegree = uGen.Degree;
        var vDegree = vGen.Degree;
        var wDegree = wGen.Degree;

        var uTarget = new double[uDegree + 1];
        var vTarget = new double[vDegree + 1];
        var wTarget = new double[wDegree + 1];
        
        uTarget[0] = uGen.Coeffs[0];
        vTarget[0] = vGen.Coeffs[0];
        wTarget[0] = wGen.Coeffs[0];

        uTarget[uDegree] = uGen.Coeffs[uDegree];
        vTarget[vDegree] = vGen.Coeffs[vDegree];
        wTarget[wDegree] = wGen.Coeffs[wDegree];

        // assemble paramIndices like other refitters (interior coefficients)
        var paramIndices = new List<(char poly, int idx)>();
        
        for (var i = 1; i <= Math.Max(0, uDegree - 1); i++) 
            paramIndices.Add(('u', i));

        for (var i = 1; i <= Math.Max(0, vDegree - 1); i++) 
            paramIndices.Add(('v', i));

        for (var i = 1; i <= Math.Max(0, wDegree - 1); i++) 
            paramIndices.Add(('w', i));
        

        // build initial parameter vector from current interior coefficients
        var initialParams = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(paramIndices.Count);
        for (var p = 0; p < paramIndices.Count; p++)
        {
            var (poly, idx) = paramIndices[p];
            initialParams[p] = poly switch
            {
                'u' => uGen.Coeffs[idx],
                'v' => vGen.Coeffs[idx],
                'w' => wGen.Coeffs[idx],
                _ => throw new InvalidOperationException()
            };
        }

        var current = initialParams.Clone();
        var converged = false;
        var finalRes = double.PositiveInfinity;
        var iterCount = 0;

        // LM outer loop
        var xData = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(12); // dummy
        for (iterCount = 1; iterCount <= maxRefinementIterations; iterCount++)
        {
            // objective with numerical jacobian wrapper
            var obj = ObjectiveFunction.NonlinearModel(
                EndpointResidualModel,
                EndpointResidualJacobian,
                xData,
                xData
            );

            var solver = new LevenbergMarquardtMinimizer(
                gradientTolerance: 1e-14,
                functionTolerance: 1e-14,
                maximumIterations: lmMaxIterations
            );

            NonlinearMinimizationResult result;
            try
            {
                result = solver.FindMinimum(obj, current);
            }
            catch
            {
                break;
            }

            current = result.MinimizingPoint.Clone();

            var resVec = EndpointResidualModel(current, xData);
            finalRes = resVec.L2Norm();

            if (finalRes <= endpointTolerance)
            {
                converged = true;
                break;
            }
        }

        // Reconstruct final polynomials
        var uFinalCoeffs = (double[])uTarget.Clone();
        var vFinalCoeffs = (double[])vTarget.Clone();
        var wFinalCoeffs = (double[])wTarget.Clone();

        for (var p = 0; p < paramIndices.Count; p++)
        {
            var (poly, idx) = paramIndices[p];
            var val = current[p];
            switch (poly)
            {
                case 'u': uFinalCoeffs[idx] = val; break;
                case 'v': vFinalCoeffs[idx] = val; break;
                case 'w': wFinalCoeffs[idx] = val; break;
            }
        }

        var uFinal = BernsteinPolynomial.Create(uFinalCoeffs);
        var vFinal = BernsteinPolynomial.Create(vFinalCoeffs);
        var wFinal = BernsteinPolynomial.Create(wFinalCoeffs);

        return new RefitResult
        {
            Generators = new PhBernsteinCurve2D.GeneratorData()
            {
                U = uFinal,
                V = vFinal,
                W = wFinal,
                P0 = p0,
                S0 = generators.S0
            },
            Converged = converged,
            Iterations = iterCount,
            FinalPositionResidual = finalRes
        };

        // -----------------------
        // Local helper models
        // -----------------------
        // Residual model: returns 10-vector
        // order: [posDiff.x, posDiff.y,
        //         t0Diff.x, t0Diff.y,
        //         t1Diff.x, t1Diff.y,
        //         a0Diff.x, a0Diff.y,
        //         a1Diff.x, a1Diff.y]
        MathNet.Numerics.LinearAlgebra.Vector<double> EndpointResidualModel(MathNet.Numerics.LinearAlgebra.Vector<double> paramVec, MathNet.Numerics.LinearAlgebra.Vector<double> _)
        {
            // Reconstruct full coefficient arrays
            var uCoeffs = (double[])uTarget.Clone();
            var vCoeffs = (double[])vTarget.Clone();
            var wCoeffs = (double[])wTarget.Clone();

            for (var p = 0; p < paramIndices.Count; p++)
            {
                var (poly, idx) = paramIndices[p];
                var val = paramVec[p];
                switch (poly)
                {
                    case 'u': uCoeffs[idx] = val; break;
                    case 'v': vCoeffs[idx] = val; break;
                    case 'w': wCoeffs[idx] = val; break;
                }
            }

            var uNew = BernsteinPolynomial.Create(uCoeffs);
            var vNew = BernsteinPolynomial.Create(vCoeffs);
            var wNew = BernsteinPolynomial.Create(wCoeffs);

            var curveNew = new PhBernsteinCurve2D(
                new PhBernsteinCurve2D.GeneratorData()
                {
                    U = uNew, V = vNew, W = wNew, P0 = p0, S0 = 0
                }, 
                false
            );

            var (pMidEstX, pMidEstY) = curveNew.GetPoint(0.5);
            var (p1EstX, p1EstY) = curveNew.GetPoint(1.0);
            
            var (t0x, t0y) = curveNew.GetDerivative1Value(0.0);
            var (t1x, t1y) = curveNew.GetDerivative1Value(1.0);
            
            var (a0x, a0y) = curveNew.GetDerivative2Value(0.0);
            var (a1x, a1y) = curveNew.GetDerivative2Value(1.0);

            var r = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(12);

            // position residuals (end point p1)
            r[0] = pMidEstX - pMid.X;
            r[1] = pMidEstY - pMid.Y;

            r[2] = p1EstX - p1.X;
            r[3] = p1EstY - p1.Y;

            // tangent residuals (we expect tangents to match desiredT0/T1)
            r[4] = t0x - desiredT0.X;
            r[5] = t0y - desiredT0.Y;

            r[6] = t1x - desiredT1.X;
            r[7] = t1y - desiredT1.Y;

            // acceleration residuals
            r[8] = a0x - desiredA0.X;
            r[9] = a0y - desiredA0.Y;

            r[10] = a1x - desiredA1.X;
            r[11] = a1y - desiredA1.Y;

            return r;
        }

        // Numerical Jacobian by finite differences (10 x nParams)
        Matrix<double> EndpointResidualJacobian(MathNet.Numerics.LinearAlgebra.Vector<double> p, MathNet.Numerics.LinearAlgebra.Vector<double> _)
        {
            const int nResiduals = 12;
            const double eps = 1e-8;
            var nParams = p.Count;
            var jac = Matrix<double>.Build.Dense(nResiduals, nParams);

            var f0 = EndpointResidualModel(p, null);
            for (var j = 0; j < nParams; j++)
            {
                var pPert = p.Clone();
                var dp = Math.Abs(p[j]) * eps + eps;
                pPert[j] += dp;
                var f1 = EndpointResidualModel(pPert, null);

                for (var i = 0; i < nResiduals; i++)
                    jac[i, j] = (f1[i] - f0[i]) / dp;
            }

            return jac;
        }
    }
    
    public static RefitResult RefitToPolyline(
        this PhBernsteinCurve2D curve,
        Float64Polyline2D polyLine,
        double bezierArea,
        double maxPositionError = 1e-5,
        double maxAreaError = 1e-3,
        int maxRefinementIterations = 8,
        int lmMaxIterations = 500
    )
    {
        var n = polyLine.Count;
        var point0 = polyLine.GetVertexPoint(0);
        var point1 = polyLine.GetVertexPoint(n - 1);
        var tangent0 = polyLine.GetVertexTangentVector(0);
        var tangent1 = polyLine.GetVertexTangentVector(n - 1);
        var s0 = curve.Generators.S0;

        var (_, phCurve, _) = curve.SetEndPoints(
            point0, 
            point1,
            tangent0, 
            tangent1
        );

        // compute complex half-rotation/scale factor (applies to z = u + i v)
        var uGen = phCurve.Generators.U;
        var vGen = phCurve.Generators.V;
        var wGen = phCurve.Generators.W;

        // Prepare coefficient arrays with fixed endpoints
        var uDegree = uGen.Degree;
        var vDegree = vGen.Degree;
        var wDegree = wGen.Degree;
        
        var uTarget = new double[uDegree + 1];
        var vTarget = new double[vDegree + 1];
        var wTarget = new double[wDegree + 1];

        // assemble paramIndices like other refitters (interior coefficients)
        var paramIndices = new List<(char poly, int idx)>();
        
        for (var i = 0; i <= uDegree; i++) 
            paramIndices.Add(('u', i));

        for (var i = 0; i <= vDegree; i++) 
            paramIndices.Add(('v', i));

        for (var i = 0; i <= wDegree; i++) 
            paramIndices.Add(('w', i));

        // build initial parameter vector from current interior coefficients
        var initialParams = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(paramIndices.Count);
        for (var p = 0; p < paramIndices.Count; p++)
        {
            var (poly, idx) = paramIndices[p];
            initialParams[p] = poly switch
            {
                'u' => uGen.Coeffs[idx],
                'v' => vGen.Coeffs[idx],
                'w' => wGen.Coeffs[idx],
                _ => throw new InvalidOperationException()
            };
        }

        var current = initialParams.Clone();
        var converged = false;
        var finalPositionRes = double.PositiveInfinity;
        var finalAreaRes = double.PositiveInfinity;
        var iterCount = 0;

        // LM outer loop
        var xData = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(7); // dummy
        for (iterCount = 1; iterCount <= maxRefinementIterations; iterCount++)
        {
            // objective with numerical jacobian wrapper
            var obj = ObjectiveFunction.NonlinearModel(
                EndpointResidualModel,
                EndpointResidualJacobian,
                xData,
                xData
            );

            var solver = new LevenbergMarquardtMinimizer(
                gradientTolerance: 1e-14,
                functionTolerance: 1e-14,
                maximumIterations: lmMaxIterations
            );

            NonlinearMinimizationResult result;
            try
            {
                result = solver.FindMinimum(obj, current);
            }
            catch
            {
                break;
            }

            current = result.MinimizingPoint.Clone();

            var resVec = EndpointResidualModel(current, xData);
            finalPositionRes = resVec.SubVector(0, 2).L2Norm();
            finalAreaRes = Math.Abs(resVec[2]);

            if (finalPositionRes <= maxPositionError && finalAreaRes <= maxAreaError)
            {
                converged = true;
                break;
            }
        }

        // Reconstruct final polynomials
        var uFinalCoeffs = (double[])uTarget.Clone();
        var vFinalCoeffs = (double[])vTarget.Clone();
        var wFinalCoeffs = (double[])wTarget.Clone();

        for (var p = 0; p < paramIndices.Count; p++)
        {
            var (poly, idx) = paramIndices[p];
            var val = current[p];
            switch (poly)
            {
                case 'u': uFinalCoeffs[idx] = val; break;
                case 'v': vFinalCoeffs[idx] = val; break;
                case 'w': wFinalCoeffs[idx] = val; break;
            }
        }

        var uFinal = BernsteinPolynomial.Create(uFinalCoeffs);
        var vFinal = BernsteinPolynomial.Create(vFinalCoeffs);
        var wFinal = BernsteinPolynomial.Create(wFinalCoeffs);

        return new RefitResult
        {
            Generators = new PhBernsteinCurve2D.GeneratorData()
            {
                U = uFinal,
                V = vFinal,
                W = wFinal,
                P0 = point0,
                S0 = s0
            },
            Converged = converged,
            Iterations = iterCount,
            FinalPositionResidual = finalPositionRes,
            FinalAreaResidual = finalAreaRes
        };

        // -----------------------
        // Local helper models
        // -----------------------
        // Residual model: returns 3-vector
        // order: [posDiff.x, posDiff.y, area]
        MathNet.Numerics.LinearAlgebra.Vector<double> EndpointResidualModel(MathNet.Numerics.LinearAlgebra.Vector<double> paramVec, MathNet.Numerics.LinearAlgebra.Vector<double> _)
        {
            // Reconstruct full coefficient arrays
            var uCoeffs = (double[])uTarget.Clone();
            var vCoeffs = (double[])vTarget.Clone();
            var wCoeffs = (double[])wTarget.Clone();

            for (var p = 0; p < paramIndices.Count; p++)
            {
                var (poly, idx) = paramIndices[p];
                var val = paramVec[p];
                switch (poly)
                {
                    case 'u': uCoeffs[idx] = val; break;
                    case 'v': vCoeffs[idx] = val; break;
                    case 'w': wCoeffs[idx] = val; break;
                }
            }

            var uNew = BernsteinPolynomial.Create(uCoeffs);
            var vNew = BernsteinPolynomial.Create(vCoeffs);
            var wNew = BernsteinPolynomial.Create(wCoeffs);

            var curveNew = new PhBernsteinCurve2D(
                new PhBernsteinCurve2D.GeneratorData
                {
                    U = uNew, V = vNew, W = wNew, P0 = point0, S0 = s0
                }, 
                false
            );

            // positions
            var (p1EstX, p1EstY) = curveNew.GetPoint(1.0);
            var (t0EstX, t0EstY) = curveNew.GetTangentVector(0.0);
            var (t1EstX, t1EstY) = curveNew.GetTangentVector(1.0);

            // area
            var area = 
                bezierArea -
                curveNew.ToAdaptivePolyline().GetArea(polyLine, 1e-5);

            var r = 
                MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(7);

            // position residuals (end point p1)
            r[0] = p1EstX - point1.X;
            r[1] = p1EstY - point1.Y;

            // tangent residuals (end point p0)
            r[2] = t0EstX - tangent0.X;
            r[3] = t0EstY - tangent0.Y;

            // tangent residuals (end point p1)
            r[4] = t1EstX - tangent1.X;
            r[5] = t1EstY - tangent1.Y;

            // acceleration residuals
            r[6] = area;

            return r;
        }

        // Numerical Jacobian by finite differences (3 x nParams)
        Matrix<double> EndpointResidualJacobian(MathNet.Numerics.LinearAlgebra.Vector<double> p, MathNet.Numerics.LinearAlgebra.Vector<double> _)
        {
            const int nResiduals = 7;
            const double eps = 1e-8;
            var nParams = p.Count;
            var jac = Matrix<double>.Build.Dense(nResiduals, nParams);

            var f0 = EndpointResidualModel(p, null);
            for (var j = 0; j < nParams; j++)
            {
                var pPert = p.Clone();
                var dp = Math.Abs(p[j]) * eps + eps;
                pPert[j] += dp;
                var f1 = EndpointResidualModel(pPert, null);

                for (var i = 0; i < nResiduals; i++)
                    jac[i, j] = (f1[i] - f0[i]) / dp;
            }

            return jac;
        }
    }

    //public static RefitResult RefitEndpointTangentAccelerationTransformGeneralNumerical(
    //    PhBernsteinCurve2D.GeneratorData generators,
    //    LinFloat64Vector2D p0,
    //    LinFloat64Vector2D p1,
    //    double scalingFactor,
    //    double rotateAngleTheta,
    //    LinFloat64Vector2D desiredT0,
    //    LinFloat64Vector2D desiredT1,
    //    LinFloat64Vector2D desiredA0,
    //    LinFloat64Vector2D desiredA1,
    //    bool adjustAllInteriorCoeffs = true,
    //    double endpointTolerance = 1e-8, // Tighter tolerance
    //    int maxRefinementIterations = 15,
    //    int lmMaxIterations = 1000
    //)
    //{
    //    if (scalingFactor <= 0)
    //        throw new ArgumentException("Scaling factor must be positive.", nameof(scalingFactor));

    //    // Enhanced initialization with better scaling
    //    var sqrtS = Math.Sqrt(scalingFactor);
    //    var halfAngle = rotateAngleTheta / 2.0;
    //    var complexRotHalf = Complex.FromPolarCoordinates(sqrtS, halfAngle);

    //    var uGen = generators.U;
    //    var vGen = generators.V;
    //    var wGen = generators.W;

    //    // Enhanced degree validation
    //    if (uGen.Degree < 3 || vGen.Degree < 3 || wGen.Degree < 2)
    //        throw new InvalidOperationException(
    //            "U and V polynomials must have degree ≥ 3, W must have degree ≥ 2 for acceleration constraints");

    //    var z0 = GetZ0(uGen, vGen);
    //    var zN = GetZN(uGen, vGen);
    //    var z0P = z0 * complexRotHalf;
    //    var zNP = zN * complexRotHalf;

    //    var w0P = wGen.Coeffs[0];
    //    var wNP = wGen.Coeffs[wGen.Degree];

    //    var uDegree = uGen.Degree;
    //    var vDegree = vGen.Degree;
    //    var wDegree = wGen.Degree;

    //    var uTarget = new double[uDegree + 1];
    //    var vTarget = new double[vDegree + 1];
    //    var wTarget = new double[wDegree + 1];

    //    uTarget[0] = z0P.Real;
    //    vTarget[0] = z0P.Imaginary;
    //    uTarget[uDegree] = zNP.Real;
    //    vTarget[vDegree] = zNP.Imaginary;

    //    wTarget[0] = w0P;
    //    wTarget[wDegree] = wNP;

    //    // IMPROVED: Better parameter selection for acceleration constraints
    //    var paramIndices = new List<(char PolyType, int Index)>();

    //    if (adjustAllInteriorCoeffs)
    //    {
    //        // Include all interior coefficients but prioritize acceleration-sensitive ones
    //        for (var i = 1; i < uDegree; i++)
    //            paramIndices.Add(('u', i));
    //        for (var i = 1; i < vDegree; i++)
    //            paramIndices.Add(('v', i));
    //        for (var i = 1; i < wDegree; i++)
    //            paramIndices.Add(('w', i));
    //    }
    //    else
    //    {
    //        // SMART SELECTION: Focus on coefficients that most affect acceleration
    //        // For acceleration, we need control over the first 3 coefficients (0,1,2) and last 3 coefficients

    //        // U polynomial: coefficients 0,1,2 affect initial acceleration
    //        for (var i = 1; i <= Math.Min(2, uDegree - 1); i++)
    //            paramIndices.Add(('u', i));
    //        // U polynomial: coefficients near the end affect final acceleration  
    //        for (var i = Math.Max(1, uDegree - 2); i < uDegree; i++)
    //            if (!paramIndices.Any(p => p.PolyType == 'u' && p.Index == i))
    //                paramIndices.Add(('u', i));

    //        // V polynomial: same strategy
    //        for (var i = 1; i <= Math.Min(2, vDegree - 1); i++)
    //            paramIndices.Add(('v', i));
    //        for (var i = Math.Max(1, vDegree - 2); i < vDegree; i++)
    //            if (!paramIndices.Any(p => p.PolyType == 'v' && p.Index == i))
    //                paramIndices.Add(('v', i));

    //        // W polynomial: critical for acceleration throughout
    //        for (var i = 1; i < wDegree; i++)
    //            paramIndices.Add(('w', i));
    //    }

    //    // IMPROVED: Better initial parameter estimation with acceleration awareness
    //    var initialParams = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(paramIndices.Count);
    //    for (var p = 0; p < paramIndices.Count; p++)
    //    {
    //        var (polyType, idx) = paramIndices[p];
    //        var currentValue = polyType switch
    //        {
    //            'u' => uGen.Coeffs[idx],
    //            'v' => vGen.Coeffs[idx],
    //            'w' => wGen.Coeffs[idx],
    //            _ => throw new InvalidOperationException()
    //        };

    //        // Enhanced scaling strategy
    //        var scaledValue = polyType switch
    //        {
    //            'u' or 'v' => currentValue * sqrtS,
    //            'w' => currentValue * scalingFactor, // W scales with full scaling factor
    //            _ => currentValue
    //        };

    //        initialParams[p] = scaledValue;
    //    }

    //    var current = initialParams.Clone();
    //    var converged = false;
    //    var finalRes = double.PositiveInfinity;
    //    var iterCount = 0;

    //    // IMPROVED: Use adaptive weights based on constraint importance
    //    var xData = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(10);

    //    for (iterCount = 1; iterCount <= maxRefinementIterations; iterCount++)
    //    {
    //        try
    //        {
    //            var obj = ObjectiveFunction.NonlinearModel(
    //                EndpointResidualModel,
    //                EndpointResidualJacobian,
    //                xData,
    //                xData
    //            );

    //            // IMPROVED: Adaptive tolerance based on iteration
    //            var currentTolerance = Math.Max(endpointTolerance, finalRes * 0.1);

    //            var solver = new LevenbergMarquardtMinimizer(
    //                gradientTolerance: currentTolerance * 0.1,
    //                functionTolerance: currentTolerance,
    //                maximumIterations: lmMaxIterations
    //            );

    //            var result = solver.FindMinimum(obj, current);
    //            current = result.MinimizingPoint.Clone();

    //            var resVec = EndpointResidualModel(current, xData);
    //            finalRes = resVec.L2Norm();

    //            if (finalRes <= endpointTolerance)
    //            {
    //                converged = true;
    //                break;
    //            }

    //            // IMPROVED: Early termination if stuck
    //            if (iterCount > 3 && finalRes > 0.95 * finalRes)
    //            {
    //                Debug.WriteLine($"Terminating early - insufficient improvement at iteration {iterCount}");
    //                break;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Debug.WriteLine($"Acceleration refinement iteration {iterCount} failed: {ex.Message}");
    //            break;
    //        }
    //    }

    //    // Reconstruct final polynomials with enhanced validation
    //    var uFinalCoeffs = (double[])uTarget.Clone();
    //    var vFinalCoeffs = (double[])vTarget.Clone();
    //    var wFinalCoeffs = (double[])wTarget.Clone();

    //    for (var p = 0; p < paramIndices.Count; p++)
    //    {
    //        var (polyType, idx) = paramIndices[p];
    //        var val = current[p];

    //        // IMPROVED: Tighter bounds for stability
    //        var boundedVal = polyType switch
    //        {
    //            'u' or 'v' => Math.Clamp(val, -3.0, 3.0), // Tighter bounds for U,V
    //            'w' => Math.Max(val, 0.005), // Stricter positivity for W with higher minimum
    //            _ => val
    //        };

    //        switch (polyType)
    //        {
    //            case 'u': uFinalCoeffs[idx] = boundedVal; break;
    //            case 'v': vFinalCoeffs[idx] = boundedVal; break;
    //            case 'w': wFinalCoeffs[idx] = boundedVal; break;
    //        }
    //    }

    //    var uFinal = BernsteinPolynomial.Create(uFinalCoeffs);
    //    var vFinal = BernsteinPolynomial.Create(vFinalCoeffs);
    //    var wFinal = BernsteinPolynomial.Create(wFinalCoeffs);

    //    // Compute final residuals for diagnostics
    //    var finalCurve = new PhBernsteinCurve2D.GeneratorData
    //    {
    //        U = uFinal,
    //        V = vFinal,
    //        W = wFinal,
    //        P0 = p0,
    //        S0 = generators.S0
    //    };

    //    var (posRes, tanRes, accRes) = ComputeConstraintResiduals(finalCurve, p1, desiredT0, desiredT1, desiredA0, desiredA1);

    //    return new RefitResult
    //    {
    //        Generators = finalCurve,
    //        Converged = converged,
    //        Iterations = iterCount,
    //        FinalPositionResidual = posRes,
    //        FinalTangentResidual = tanRes,
    //        FinalAccelerationResidual = accRes
    //    };

    //    // Local helper functions
    //    Complex GetZ0(BernsteinPolynomial u, BernsteinPolynomial v)
    //        => new Complex(u.Coeffs[0], v.Coeffs[0]);

    //    Complex GetZN(BernsteinPolynomial u, BernsteinPolynomial v)
    //        => new Complex(u.Coeffs[u.Degree], v.Coeffs[v.Degree]);

    //    MathNet.Numerics.LinearAlgebra.Vector<double> EndpointResidualModel(
    //        MathNet.Numerics.LinearAlgebra.Vector<double> paramVec,
    //        MathNet.Numerics.LinearAlgebra.Vector<double> _)
    //    {
    //        var uCoeffs = (double[])uTarget.Clone();
    //        var vCoeffs = (double[])vTarget.Clone();
    //        var wCoeffs = (double[])wTarget.Clone();

    //        for (var p = 0; p < paramIndices.Count; p++)
    //        {
    //            var (polyType, idx) = paramIndices[p];
    //            var val = paramVec[p];
    //            switch (polyType)
    //            {
    //                case 'u': uCoeffs[idx] = val; break;
    //                case 'v': vCoeffs[idx] = val; break;
    //                case 'w': wCoeffs[idx] = val; break;
    //            }
    //        }

    //        var uNew = BernsteinPolynomial.Create(uCoeffs);
    //        var vNew = BernsteinPolynomial.Create(vCoeffs);
    //        var wNew = BernsteinPolynomial.Create(wCoeffs);

    //        // Compute curve properties
    //        var xPrime = wNew * (uNew.Square() - vNew.Square());
    //        var yPrime = (wNew * (uNew * vNew)).Times(2.0);

    //        var xPoly = xPrime.Integral().Add(p0.X);
    //        var yPoly = yPrime.Integral().Add(p0.Y);

    //        var p1EstX = xPoly.EvaluateAt(1.0);
    //        var p1EstY = yPoly.EvaluateAt(1.0);

    //        // Tangents
    //        var t0x = xPrime.EvaluateAt(0.0);
    //        var t0y = yPrime.EvaluateAt(0.0);
    //        var t1x = xPrime.EvaluateAt(1.0);
    //        var t1y = yPrime.EvaluateAt(1.0);

    //        // Accelerations - IMPROVED: More stable computation
    //        var xPrimeDt = xPrime.Derivative();
    //        var yPrimeDt = yPrime.Derivative();

    //        var a0x = xPrimeDt.EvaluateAt(0.0);
    //        var a0y = yPrimeDt.EvaluateAt(0.0);
    //        var a1x = xPrimeDt.EvaluateAt(1.0);
    //        var a1y = yPrimeDt.EvaluateAt(1.0);

    //        // IMPROVED: Adaptive weighting based on constraint importance
    //        // Position is most critical, but acceleration needs stronger weighting
    //        var positionWeight = 1.0;
    //        var tangentWeight = 0.8;
    //        var accelerationWeight = 0.6; // Increased from 0.1 to 0.6

    //        // IMPROVED: Normalize residuals by expected scales
    //        var positionScale = Math.Max(1.0, (p1 - p0).L2Norm().ScalarValue);
    //        var tangentScale = Math.Max(1.0, Math.Max(desiredT0.L2Norm().ScalarValue, desiredT1.L2Norm().ScalarValue));
    //        var accelerationScale = Math.Max(1.0, Math.Max(desiredA0.L2Norm().ScalarValue, desiredA1.L2Norm().ScalarValue));

    //        var r = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(10);

    //        // Position residuals (normalized)
    //        r[0] = (positionWeight / positionScale) * (p1EstX - p1.X);
    //        r[1] = (positionWeight / positionScale) * (p1EstY - p1.Y);

    //        // Tangent residuals (normalized)
    //        r[2] = (tangentWeight / tangentScale) * (t0x - desiredT0.X);
    //        r[3] = (tangentWeight / tangentScale) * (t0y - desiredT0.Y);
    //        r[4] = (tangentWeight / tangentScale) * (t1x - desiredT1.X);
    //        r[5] = (tangentWeight / tangentScale) * (t1y - desiredT1.Y);

    //        // Acceleration residuals (normalized) - IMPROVED: Higher weighting
    //        r[6] = (accelerationWeight / accelerationScale) * (a0x - desiredA0.X);
    //        r[7] = (accelerationWeight / accelerationScale) * (a0y - desiredA0.Y);
    //        r[8] = (accelerationWeight / accelerationScale) * (a1x - desiredA1.X);
    //        r[9] = (accelerationWeight / accelerationScale) * (a1y - desiredA1.Y);

    //        return r;
    //    }

    //    Matrix<double> EndpointResidualJacobian(
    //        MathNet.Numerics.LinearAlgebra.Vector<double> p,
    //        MathNet.Numerics.LinearAlgebra.Vector<double> _)
    //    {
    //        const int nResiduals = 10;
    //        // IMPROVED: Adaptive epsilon for finite differences
    //        var baseEpsilon = 1e-8;
    //        var nParams = p.Count;
    //        var jac = Matrix<double>.Build.Dense(nResiduals, nParams);

    //        var f0 = EndpointResidualModel(p, null);

    //        for (var j = 0; j < nParams; j++)
    //        {
    //            // IMPROVED: Better epsilon selection for numerical stability
    //            var epsilon = Math.Max(Math.Abs(p[j]) * baseEpsilon, baseEpsilon);
    //            var pPert = p.Clone();
    //            pPert[j] += epsilon;
    //            var f1 = EndpointResidualModel(pPert, null);

    //            for (var i = 0; i < nResiduals; i++)
    //                jac[i, j] = (f1[i] - f0[i]) / epsilon;
    //        }

    //        return jac;
    //    }
    //}

    //// Enhanced version for tangent and acceleration constraints
    //public static RefitResult RefitEndpointTangentAccelerationTransformGeneralNumerical(
    //    PhBernsteinCurve2D.GeneratorData generators,
    //    LinFloat64Vector2D p0,
    //    LinFloat64Vector2D p1,
    //    double scalingFactor,
    //    double rotateAngleTheta,
    //    LinFloat64Vector2D desiredT0,
    //    LinFloat64Vector2D desiredT1,
    //    LinFloat64Vector2D desiredA0,
    //    LinFloat64Vector2D desiredA1,
    //    bool adjustAllInteriorCoeffs = true,
    //    double endpointTolerance = 1e-6, // Tighter tolerance for more constraints
    //    int maxRefinementIterations = 12, // More iterations for harder problem
    //    int lmMaxIterations = 500
    //)
    //{
    //    if (scalingFactor <= 0)
    //        throw new ArgumentException("Scaling factor must be positive.", nameof(scalingFactor));

    //    // Enhanced initialization with constraint-aware parameter selection
    //    var sqrtS = Math.Sqrt(scalingFactor);
    //    var halfAngle = rotateAngleTheta / 2.0;
    //    var complexRotHalf = Complex.FromPolarCoordinates(sqrtS, halfAngle);

    //    var uGen = generators.U;
    //    var vGen = generators.V;
    //    var wGen = generators.W;

    //    // Validate polynomial degrees for acceleration constraints
    //    if (uGen.Degree < 3 || vGen.Degree < 3)
    //        throw new InvalidOperationException("U and V polynomials must have degree ≥ 3 for acceleration constraints");

    //    var z0 = GetZ0(uGen, vGen);
    //    var zN = GetZN(uGen, vGen);
    //    var z0P = z0 * complexRotHalf;
    //    var zNP = zN * complexRotHalf;

    //    var w0P = wGen.Coeffs[0];
    //    var wNP = wGen.Coeffs[wGen.Degree];

    //    var uDegree = uGen.Degree;
    //    var vDegree = vGen.Degree;
    //    var wDegree = wGen.Degree;

    //    var uTarget = new double[uDegree + 1];
    //    var vTarget = new double[vDegree + 1];
    //    var wTarget = new double[wDegree + 1];

    //    uTarget[0] = z0P.Real;
    //    vTarget[0] = z0P.Imaginary;
    //    uTarget[uDegree] = zNP.Real;
    //    vTarget[vDegree] = zNP.Imaginary;

    //    wTarget[0] = w0P;
    //    wTarget[wDegree] = wNP;

    //    // Enhanced parameter selection for acceleration constraints
    //    var paramIndices = new List<(char PolyType, int Index)>();

    //    // For acceleration constraints, we need more degrees of freedom
    //    if (adjustAllInteriorCoeffs)
    //    {
    //        for (var i = 1; i < uDegree; i++) paramIndices.Add(('u', i));
    //        for (var i = 1; i < vDegree; i++) paramIndices.Add(('v', i));
    //        for (var i = 1; i < wDegree; i++) paramIndices.Add(('w', i));
    //    }
    //    else
    //    {
    //        // Smart selection: include coefficients that most affect acceleration
    //        for (var i = 1; i <= 2 && i < uDegree; i++) paramIndices.Add(('u', i));
    //        for (var i = 1; i <= 2 && i < vDegree; i++) paramIndices.Add(('v', i));
    //        for (var i = 1; i < wDegree; i++) paramIndices.Add(('w', i));
    //        for (var i = uDegree - 2; i < uDegree && i > 2; i++) paramIndices.Add(('u', i));
    //        for (var i = vDegree - 2; i < vDegree && i > 2; i++) paramIndices.Add(('v', i));
    //    }

    //    // Enhanced initial parameter estimation with constraint awareness
    //    var initialParams = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(paramIndices.Count);
    //    for (var p = 0; p < paramIndices.Count; p++)
    //    {
    //        var (polyType, idx) = paramIndices[p];
    //        var currentValue = polyType switch
    //        {
    //            'u' => uGen.Coeffs[idx],
    //            'v' => vGen.Coeffs[idx],
    //            'w' => wGen.Coeffs[idx],
    //            _ => throw new InvalidOperationException()
    //        };

    //        // Apply constraint-aware scaling
    //        var scaledValue = polyType switch
    //        {
    //            'u' or 'v' => currentValue * sqrtS,
    //            'w' => currentValue * scalingFactor,
    //            _ => currentValue
    //        };

    //        initialParams[p] = scaledValue;
    //    }

    //    var current = initialParams.Clone();
    //    var converged = false;
    //    var finalRes = double.PositiveInfinity;
    //    var iterCount = 0;

    //    // Enhanced optimization with weighted residuals
    //    var xData = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(10);

    //    for (iterCount = 1; iterCount <= maxRefinementIterations; iterCount++)
    //    {
    //        try
    //        {
    //            var obj = ObjectiveFunction.NonlinearModel(
    //                EndpointResidualModel,
    //                EndpointResidualJacobian,
    //                xData,
    //                xData
    //            );

    //            var solver = new LevenbergMarquardtMinimizer(
    //                gradientTolerance: 1e-12,
    //                functionTolerance: 1e-12,
    //                maximumIterations: lmMaxIterations
    //            );

    //            var result = solver.FindMinimum(obj, current);
    //            current = result.MinimizingPoint.Clone();

    //            var resVec = EndpointResidualModel(current, xData);
    //            finalRes = resVec.L2Norm();

    //            if (finalRes <= endpointTolerance)
    //            {
    //                converged = true;
    //                break;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Debug.WriteLine($"Acceleration refinement iteration {iterCount} failed: {ex.Message}");
    //            break;
    //        }
    //    }

    //    // Reconstruct final polynomials with enhanced validation
    //    var uFinalCoeffs = (double[])uTarget.Clone();
    //    var vFinalCoeffs = (double[])vTarget.Clone();
    //    var wFinalCoeffs = (double[])wTarget.Clone();

    //    for (var p = 0; p < paramIndices.Count; p++)
    //    {
    //        var (polyType, idx) = paramIndices[p];
    //        var val = current[p];

    //        var boundedVal = polyType switch
    //        {
    //            'u' or 'v' => Math.Clamp(val, -5.0, 5.0), // Tighter bounds for stability
    //            'w' => Math.Max(val, 0.001), // Stricter positivity for W
    //            _ => val
    //        };

    //        switch (polyType)
    //        {
    //            case 'u': uFinalCoeffs[idx] = boundedVal; break;
    //            case 'v': vFinalCoeffs[idx] = boundedVal; break;
    //            case 'w': wFinalCoeffs[idx] = boundedVal; break;
    //        }
    //    }

    //    var uFinal = BernsteinPolynomial.Create(uFinalCoeffs);
    //    var vFinal = BernsteinPolynomial.Create(vFinalCoeffs);
    //    var wFinal = BernsteinPolynomial.Create(wFinalCoeffs);

    //    // Compute final residuals for diagnostics
    //    var finalCurve = new PhBernsteinCurve2D.GeneratorData
    //    {
    //        U = uFinal, V = vFinal, W = wFinal, P0 = p0, S0 = generators.S0
    //    };

    //    var (posRes, tanRes, accRes) = ComputeConstraintResiduals(finalCurve, p1, desiredT0, desiredT1, desiredA0, desiredA1);

    //    return new RefitResult
    //    {
    //        Generators = finalCurve,
    //        Converged = converged,
    //        Iterations = iterCount,
    //        FinalPositionResidual = posRes,
    //        FinalTangentResidual = tanRes,
    //        FinalAccelerationResidual = accRes
    //    };

    //    // Local helper functions
    //    Complex GetZ0(BernsteinPolynomial u, BernsteinPolynomial v)
    //        => new Complex(u.Coeffs[0], v.Coeffs[0]);

    //    Complex GetZN(BernsteinPolynomial u, BernsteinPolynomial v)
    //        => new Complex(u.Coeffs[u.Degree], v.Coeffs[v.Degree]);

    //    MathNet.Numerics.LinearAlgebra.Vector<double> EndpointResidualModel(MathNet.Numerics.LinearAlgebra.Vector<double> paramVec, MathNet.Numerics.LinearAlgebra.Vector<double> _)
    //    {
    //        var uCoeffs = (double[])uTarget.Clone();
    //        var vCoeffs = (double[])vTarget.Clone();
    //        var wCoeffs = (double[])wTarget.Clone();

    //        for (var p = 0; p < paramIndices.Count; p++)
    //        {
    //            var (polyType, idx) = paramIndices[p];
    //            var val = paramVec[p];
    //            switch (polyType)
    //            {
    //                case 'u': uCoeffs[idx] = val; break;
    //                case 'v': vCoeffs[idx] = val; break;
    //                case 'w': wCoeffs[idx] = val; break;
    //            }
    //        }

    //        var uNew = BernsteinPolynomial.Create(uCoeffs);
    //        var vNew = BernsteinPolynomial.Create(vCoeffs);
    //        var wNew = BernsteinPolynomial.Create(wCoeffs);

    //        // Compute curve properties
    //        var xPrime = wNew * (uNew.Square() - vNew.Square());
    //        var yPrime = (wNew * (uNew * vNew)).Times(2.0);

    //        var xPoly = xPrime.Integral().Add(p0.X);
    //        var yPoly = yPrime.Integral().Add(p0.Y);

    //        var p1EstX = xPoly.EvaluateAt(1.0);
    //        var p1EstY = yPoly.EvaluateAt(1.0);

    //        // Tangents
    //        var t0x = xPrime.EvaluateAt(0.0);
    //        var t0y = yPrime.EvaluateAt(0.0);
    //        var t1x = xPrime.EvaluateAt(1.0);
    //        var t1y = yPrime.EvaluateAt(1.0);

    //        // Accelerations
    //        var xPrimeDt = xPrime.Derivative();
    //        var yPrimeDt = yPrime.Derivative();

    //        var a0x = xPrimeDt.EvaluateAt(0.0);
    //        var a0y = yPrimeDt.EvaluateAt(0.0);
    //        var a1x = xPrimeDt.EvaluateAt(1.0);
    //        var a1y = yPrimeDt.EvaluateAt(1.0);

    //        // Weighted residuals - position constraints are most important
    //        var r = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(10);
    //        var positionWeight = 1.0;
    //        var tangentWeight = 0.5;
    //        var accelerationWeight = 0.1;

    //        // Position residuals
    //        r[0] = positionWeight * (p1EstX - p1.X);
    //        r[1] = positionWeight * (p1EstY - p1.Y);

    //        // Tangent residuals
    //        r[2] = tangentWeight * (t0x - desiredT0.X);
    //        r[3] = tangentWeight * (t0y - desiredT0.Y);
    //        r[4] = tangentWeight * (t1x - desiredT1.X);
    //        r[5] = tangentWeight * (t1y - desiredT1.Y);

    //        // Acceleration residuals
    //        r[6] = accelerationWeight * (a0x - desiredA0.X);
    //        r[7] = accelerationWeight * (a0y - desiredA0.Y);
    //        r[8] = accelerationWeight * (a1x - desiredA1.X);
    //        r[9] = accelerationWeight * (a1y - desiredA1.Y);

    //        return r;
    //    }

    //    Matrix<double> EndpointResidualJacobian(MathNet.Numerics.LinearAlgebra.Vector<double> p, MathNet.Numerics.LinearAlgebra.Vector<double> _)
    //    {
    //        const int nResiduals = 10;
    //        const double eps = 1e-8;
    //        var nParams = p.Count;
    //        var jac = Matrix<double>.Build.Dense(nResiduals, nParams);

    //        var f0 = EndpointResidualModel(p, null);
    //        for (var j = 0; j < nParams; j++)
    //        {
    //            var pPert = p.Clone();
    //            var dp = Math.Max(Math.Abs(p[j]) * eps, eps);
    //            pPert[j] += dp;
    //            var f1 = EndpointResidualModel(pPert, null);

    //            for (var i = 0; i < nResiduals; i++)
    //                jac[i, j] = (f1[i] - f0[i]) / dp;
    //        }

    //        return jac;
    //    }
    //}

    // Helper method to compute constraint residuals for diagnostics
    private static (double PositionResidual, double TangentResidual, double AccelerationResidual)
        ComputeConstraintResiduals(
            PhBernsteinCurve2D.GeneratorData generators,
            LinFloat64Vector2D p1,
            LinFloat64Vector2D desiredT0,
            LinFloat64Vector2D desiredT1,
            LinFloat64Vector2D desiredA0,
            LinFloat64Vector2D desiredA1)
    {
        var u = generators.U;
        var v = generators.V;
        var w = generators.W;

        var xPrime = w * (u.Square() - v.Square());
        var yPrime = (w * (u * v)).Times(2.0);

        var xPoly = xPrime.Integral().Add(generators.P0.X);
        var yPoly = yPrime.Integral().Add(generators.P0.Y);

        // Position at t=1
        var p1Est = LinFloat64Vector2D.Create(xPoly.GetValue(1.0), yPoly.GetValue(1.0));
        var posResidual = (p1Est - p1).L2Norm();

        // Tangents
        var t0 = LinFloat64Vector2D.Create(xPrime.GetValue(0.0), yPrime.GetValue(0.0));
        var t1 = LinFloat64Vector2D.Create(xPrime.GetValue(1.0), yPrime.GetValue(1.0));
        var tanResidual = Math.Max(
            (t0 - desiredT0).L2Norm(),
            (t1 - desiredT1).L2Norm()
        );

        // Accelerations
        var xPrimeDt = xPrime.Derivative();
        var yPrimeDt = yPrime.Derivative();
        var a0 = LinFloat64Vector2D.Create(xPrimeDt.GetValue(0.0), yPrimeDt.GetValue(0.0));
        var a1 = LinFloat64Vector2D.Create(xPrimeDt.GetValue(1.0), yPrimeDt.GetValue(1.0));
        var accResidual = Math.Max(
            (a0 - desiredA0).L2Norm(),
            (a1 - desiredA1).L2Norm()
        );

        return (posResidual, tanResidual, accResidual);
    }


    
    public static RefitResult RefitEndpointTangentAccelerationTransformGeneralNumerical1(
        PhBernsteinCurve2D.GeneratorData generators,
        LinFloat64Vector2D p0, // target start point (after map)
        LinFloat64Vector2D p1, // target end point (after map)
        LinFloat64Vector2D desiredT0, // desired tangent at t=0 after mapping
        LinFloat64Vector2D desiredA0, // desired acceleration at t=0 after mapping
        bool adjustAllInteriorCoeffs = true,
        double endpointTolerance = 1e-7,
        int maxRefinementIterations = 8,
        int lmMaxIterations = 500
    )
    {
        var uGen = generators.U;
        var vGen = generators.V;
        var wGen = generators.W;

        var z0 = GetZ0(uGen, vGen);
        var zN = GetZN(uGen, vGen);
        var z0P = z0;
        var zNP = zN;

        // w endpoints (kept as-is scaled by scale? previous code kept the same)
        var w0P = wGen.Coeffs[0];
        var wNP = wGen.Coeffs[wGen.Degree];

        // Prepare coefficient arrays with fixed endpoints
        var uDegree = uGen.Degree;
        var vDegree = vGen.Degree;
        var wDegree = wGen.Degree;

        var uTarget = new double[uDegree + 1];
        var vTarget = new double[vDegree + 1];
        var wTarget = new double[wDegree + 1];

        uTarget[0] = z0P.Real;
        vTarget[0] = z0P.Imaginary;
        uTarget[uDegree] = zNP.Real;
        vTarget[vDegree] = zNP.Imaginary;

        wTarget[0] = w0P;
        wTarget[wDegree] = wNP;

        // assemble paramIndices like other refitters (interior coefficients)
        var paramIndices = new List<(char poly, int idx)>();
        if (adjustAllInteriorCoeffs)
        {
            for (var i = 1; i <= Math.Max(0, uDegree - 1); i++) paramIndices.Add(('u', i));
            for (var i = 1; i <= Math.Max(0, vDegree - 1); i++) paramIndices.Add(('v', i));
            for (var i = 1; i <= Math.Max(0, wDegree - 1); i++) paramIndices.Add(('w', i));
        }
        else
        {
            // same default behavior (you can refine selection)
            for (var i = 1; i <= Math.Max(0, uDegree - 1); i++) paramIndices.Add(('u', i));
            for (var i = 1; i <= Math.Max(0, vDegree - 1); i++) paramIndices.Add(('v', i));
            for (var i = 1; i <= Math.Max(0, wDegree - 1); i++) paramIndices.Add(('w', i));
        }

        // build initial parameter vector from current interior coefficients
        var initialParams = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(paramIndices.Count);
        for (var p = 0; p < paramIndices.Count; p++)
        {
            var (poly, idx) = paramIndices[p];
            initialParams[p] = poly switch
            {
                'u' => uGen.Coeffs[idx],
                'v' => vGen.Coeffs[idx],
                'w' => wGen.Coeffs[idx],
                _ => throw new InvalidOperationException()
            };
        }

        var current = initialParams.Clone();
        var converged = false;
        var finalRes = double.PositiveInfinity;
        var iterCount = 0;

        // LM outer loop
        var xData = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(6); // dummy
        for (iterCount = 1; iterCount <= maxRefinementIterations; iterCount++)
        {
            // objective with numerical jacobian wrapper
            var obj = ObjectiveFunction.NonlinearModel(
                EndpointResidualModel,
                EndpointResidualJacobian,
                xData,
                xData
            );

            var solver = new LevenbergMarquardtMinimizer(
                gradientTolerance: 1e-14,
                functionTolerance: 1e-14,
                maximumIterations: lmMaxIterations
            );

            NonlinearMinimizationResult result;
            try
            {
                result = solver.FindMinimum(obj, current);
            }
            catch
            {
                break;
            }

            current = result.MinimizingPoint.Clone();

            var resVec = EndpointResidualModel(current, xData);
            finalRes = resVec.L2Norm();

            if (finalRes <= endpointTolerance)
            {
                converged = true;
                break;
            }
        }

        // Reconstruct final polynomials
        var uFinalCoeffs = (double[])uTarget.Clone();
        var vFinalCoeffs = (double[])vTarget.Clone();
        var wFinalCoeffs = (double[])wTarget.Clone();

        for (var p = 0; p < paramIndices.Count; p++)
        {
            var (poly, idx) = paramIndices[p];
            var val = current[p];
            switch (poly)
            {
                case 'u': uFinalCoeffs[idx] = val; break;
                case 'v': vFinalCoeffs[idx] = val; break;
                case 'w': wFinalCoeffs[idx] = val; break;
            }
        }

        var uFinal = BernsteinPolynomial.Create(uFinalCoeffs);
        var vFinal = BernsteinPolynomial.Create(vFinalCoeffs);
        var wFinal = BernsteinPolynomial.Create(wFinalCoeffs);

        return new RefitResult
        {
            Generators = new PhBernsteinCurve2D.GeneratorData()
            {
                U = uFinal,
                V = vFinal,
                W = wFinal,
                P0 = p0,
                S0 = generators.S0
            },
            Converged = converged,
            Iterations = iterCount,
            FinalPositionResidual = finalRes
        };

        // Transform endpoint complex z values using half-rotation-scale
        Complex GetZ0(BernsteinPolynomial u, BernsteinPolynomial v)
            => new Complex(u.Coeffs[0], v.Coeffs[0]);

        Complex GetZN(BernsteinPolynomial u, BernsteinPolynomial v)
            => new Complex(u.Coeffs[u.Degree], v.Coeffs[v.Degree]);

        // -----------------------
        // Local helper models
        // -----------------------
        // Residual model: returns 10-vector
        // order: [posDiff.x, posDiff.y,
        //         t0Diff.x, t0Diff.y,
        //         t1Diff.x, t1Diff.y,
        //         a0Diff.x, a0Diff.y,
        //         a1Diff.x, a1Diff.y]
        MathNet.Numerics.LinearAlgebra.Vector<double> EndpointResidualModel(MathNet.Numerics.LinearAlgebra.Vector<double> paramVec, MathNet.Numerics.LinearAlgebra.Vector<double> _)
        {
            // Reconstruct full coefficient arrays
            var uCoeffs = (double[])uTarget.Clone();
            var vCoeffs = (double[])vTarget.Clone();
            var wCoeffs = (double[])wTarget.Clone();

            for (var p = 0; p < paramIndices.Count; p++)
            {
                var (poly, idx) = paramIndices[p];
                var val = paramVec[p];
                switch (poly)
                {
                    case 'u': uCoeffs[idx] = val; break;
                    case 'v': vCoeffs[idx] = val; break;
                    case 'w': wCoeffs[idx] = val; break;
                }
            }

            var uNew = BernsteinPolynomial.Create(uCoeffs);
            var vNew = BernsteinPolynomial.Create(vCoeffs);
            var wNew = BernsteinPolynomial.Create(wCoeffs);

            // x', y'
            var xPrime = wNew * (uNew.Square() - vNew.Square());
            var yPrime = (wNew * (uNew * vNew)).Times(2.0);

            // positions: integrate and add p0
            var xPoly = xPrime.Integral().Add(p0.X);
            var yPoly = yPrime.Integral().Add(p0.Y);

            var p1EstX = xPoly.GetValue(1.0);
            var p1EstY = yPoly.GetValue(1.0);

            // tangents: evaluate x', y' at endpoints
            var t0x = xPrime.GetValue(0.0);
            var t0y = yPrime.GetValue(0.0);

            // accelerations: derivatives of x', y' (i.e. x'', y'')
            var xPrimeDt = xPrime.Derivative();
            var yPrimeDt = yPrime.Derivative();

            var a0x = xPrimeDt.GetValue(0.0);
            var a0y = yPrimeDt.GetValue(0.0);

            var r = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(6);

            // position residuals (end point p1)
            r[0] = p1EstX - p1.X;
            r[1] = p1EstY - p1.Y;

            // tangent residuals (we expect tangents to match desiredT0/T1)
            r[2] = t0x - desiredT0.X;
            r[3] = t0y - desiredT0.Y;

            // acceleration residuals
            r[4] = a0x - desiredA0.X;
            r[5] = a0y - desiredA0.Y;

            return r;
        }

        // Numerical Jacobian by finite differences (10 x nParams)
        Matrix<double> EndpointResidualJacobian(MathNet.Numerics.LinearAlgebra.Vector<double> p, MathNet.Numerics.LinearAlgebra.Vector<double> _)
        {
            const int nResiduals = 6;
            const double eps = 1e-8;
            var nParams = p.Count;
            var jac = Matrix<double>.Build.Dense(nResiduals, nParams);

            var f0 = EndpointResidualModel(p, null);
            for (var j = 0; j < nParams; j++)
            {
                var pPert = p.Clone();
                var dp = Math.Abs(p[j]) * eps + eps;
                pPert[j] += dp;
                var f1 = EndpointResidualModel(pPert, null);

                for (var i = 0; i < nResiduals; i++)
                    jac[i, j] = (f1[i] - f0[i]) / dp;
            }

            return jac;
        }
    }

}



//using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
//using MathNet.Numerics.LinearAlgebra;
//using MathNet.Numerics.Optimization;
//using System.Numerics;

//namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier;

//public static class PhBernsteinCurve2DRefitter
//{
//    public class RefitResult
//    {
//        public required PhBernsteinCurve2D.GeneratorData Generators { get; init; }

//        public bool Converged { get; init; }

//        public int Iterations { get; init; }

//        public double FinalPositionResidual { get; init; }

//        public override string ToString()
//            => $"Converged={Converged}, Iter={Iterations}, PosRes={FinalPositionResidual:E3}";
//    }

//    //public static RefitResult RefitEndpointTangentTransformIterative(
//    //    GeneratorData generators,
//    //    LinFloat64Vector2D p0,
//    //    LinFloat64Vector2D p1,
//    //    double scalingFactor,
//    //    double rotateAngleTheta,
//    //    double endpointTolerance = 1e-7,
//    //    int maxRefinementIterations = 8,
//    //    int lmMaxIterations = 200
//    //)
//    //{
//    //    if (generators.U.Degree != 2 || generators.V.Degree != 2 || generators.W.Degree != 2)
//    //        throw new ArgumentException("u, v, w must be quadratic Bernstein polynomials.");

//    //    if (scalingFactor <= 0)
//    //        throw new ArgumentException("scaleFactor_s must be positive.");

//    //    // --- 1) Compute rotated/scaled endpoint complex generators ---
//    //    var sqrtS = Math.Sqrt(scalingFactor);
//    //    var halfAngle = rotateAngleTheta / 2.0;
//    //    var complexRotHalf = Complex.FromPolarCoordinates(sqrtS, halfAngle);

//    //    var z0 = new Complex(generators.U.Coeffs[0], generators.V.Coeffs[0]);
//    //    var z2 = new Complex(generators.U.Coeffs[2], generators.V.Coeffs[2]);
//    //    var z0P = z0 * complexRotHalf;
//    //    var z2P = z2 * complexRotHalf;

//    //    double u0P = z0P.Real, v0P = z0P.Imaginary;
//    //    double u2P = z2P.Real, v2P = z2P.Imaginary;

//    //    var w0P = generators.W.Coeffs[0];
//    //    var w2P = generators.W.Coeffs[2];

//    //    // Initial interior coefficients
//    //    var current = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.DenseOfArray(new[]
//    //    {
//    //        generators.U.Coeffs[1],
//    //        generators.V.Coeffs[1],
//    //        generators.W.Coeffs[1]
//    //    });

//    //    var converged = false;
//    //    var finalPosRes = double.PositiveInfinity;
//    //    int iterCount;

//    //    // Dummy data vectors for MathNet
//    //    var xData = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(2);
//    //    var yData = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(2);

//    //    // --- 2) Iterative refinement loop ---
//    //    for (iterCount = 1; iterCount <= maxRefinementIterations; iterCount++)
//    //    {
//    //        var obj = ObjectiveFunction.NonlinearModel(
//    //            EndpointResidualModel,
//    //            EndpointResidualJacobian,
//    //            xData,
//    //            yData
//    //        );

//    //        var solver = new LevenbergMarquardtMinimizer(
//    //            gradientTolerance: 1e-14,
//    //            functionTolerance: 1e-14,
//    //            maximumIterations: lmMaxIterations
//    //        );

//    //        NonlinearMinimizationResult result;
//    //        try
//    //        {
//    //            result = solver.FindMinimum(obj, current);
//    //        }
//    //        catch
//    //        {
//    //            break;
//    //        }

//    //        current = result.MinimizingPoint.Clone();

//    //        var resVec = EndpointResidualModel(current, xData);
//    //        finalPosRes = resVec.L2Norm();

//    //        if (finalPosRes <= endpointTolerance)
//    //        {
//    //            converged = true;
//    //            break;
//    //        }
//    //    }

//    //    var uFinal = BernsteinPolynomial.Create(u0P, current[0], u2P);
//    //    var vFinal = BernsteinPolynomial.Create(v0P, current[1], v2P);
//    //    var wFinal = BernsteinPolynomial.Create(w0P, current[2], w2P);

//    //    return new RefitResult
//    //    {
//    //        Generators = new GeneratorData()
//    //        {
//    //            U = uFinal,
//    //            V = vFinal,
//    //            W = wFinal,
//    //            P0 = p0,
//    //            S0 = generators.S0
//    //        },
//    //        Converged = converged,
//    //        Iterations = iterCount,
//    //        FinalPositionResidual = finalPosRes
//    //    };

//    //    Matrix<double> EndpointResidualJacobian(MathNet.Numerics.LinearAlgebra.Vector<double> p, MathNet.Numerics.LinearAlgebra.Vector<double> x)
//    //    {
//    //        const int nResiduals = 2;
//    //        const double eps = 1e-8;

//    //        var nParams = p.Count;
//    //        var jacobian = Matrix<double>.Build.Dense(nResiduals, nParams);

//    //        var f0 = EndpointResidualModel(p, x);
//    //        for (var j = 0; j < nParams; j++)
//    //        {
//    //            var pPert = p.Clone();
//    //            var dp = Math.Abs(p[j]) * eps + eps;
//    //            pPert[j] += dp;
//    //            var f1 = EndpointResidualModel(pPert, x);

//    //            for (var i = 0; i < nResiduals; i++)
//    //                jacobian[i, j] = (f1[i] - f0[i]) / dp;
//    //        }

//    //        return jacobian;
//    //    }

//    //    // --- Helper model and Jacobian ---
//    //    MathNet.Numerics.LinearAlgebra.Vector<double> EndpointResidualModel(MathNet.Numerics.LinearAlgebra.Vector<double> p, MathNet.Numerics.LinearAlgebra.Vector<double> x)
//    //    {
//    //        var u1C = p[0];
//    //        var v1C = p[1];
//    //        var w1C = p[2];

//    //        var uNew = BernsteinPolynomial.Create(u0P, u1C, u2P);
//    //        var vNew = BernsteinPolynomial.Create(v0P, v1C, v2P);
//    //        var wNew = BernsteinPolynomial.Create(w0P, w1C, w2P);

//    //        var xPrime = wNew * (uNew.Square() - vNew.Square());
//    //        var yPrime = 2 * wNew * uNew * vNew;

//    //        var xPoly = xPrime.Integral().Add(p0.X);
//    //        var yPoly = yPrime.Integral().Add(p0.Y);

//    //        var p1Est = LinFloat64Vector2D.Create(xPoly.EvaluateAt(1.0), yPoly.EvaluateAt(1.0));

//    //        var r = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(2);
//    //        r[0] = p1Est.X - p1.X;
//    //        r[1] = p1Est.Y - p1.Y;
//    //        return r;
//    //    }
//    //}

//    public static RefitResult RefitEndpointTangentTransformGeneralNumerical(
//        PhBernsteinCurve2D.GeneratorData generators,
//        LinFloat64Vector2D p0,
//        LinFloat64Vector2D p1,
//        double scalingFactor,
//        double rotateAngleTheta,
//        bool adjustAllInteriorCoeffs = true, // default: adjust every interior coeff of u,v,w
//        double endpointTolerance = 1e-7,
//        int maxRefinementIterations = 8,
//        int lmMaxIterations = 500
//    )
//    {
//        if (scalingFactor <= 0)
//            throw new ArgumentException("scalingFactor must be positive.");

//        // --- compute complex half-rotation/scale factor ---
//        var sqrtS = Math.Sqrt(scalingFactor);
//        var halfAngle = rotateAngleTheta / 2.0;
//        var complexRotHalf = Complex.FromPolarCoordinates(sqrtS, halfAngle);

//        var uGen = generators.U;
//        var vGen = generators.V;
//        var wGen = generators.W;

//        // Transformed endpoint values for u,v (complex multiply)
//        var z0 = GetZ0(uGen, vGen);
//        var zN = GetZN(uGen, vGen);
//        var z0P = z0 * complexRotHalf;
//        var zNP = zN * complexRotHalf;

//        // Transformed endpoint values for w: scale endpoints by s
//        var w0P = wGen.Coeffs[0]; // * scalingFactor;
//        var wNP = wGen.Coeffs[wGen.Degree]; // * scalingFactor;

//        // Prepare endpoint coefficient arrays for u,v,w after transformation
//        // We'll keep interior coefficients as variables
//        var uDegree = uGen.Degree;
//        var vDegree = vGen.Degree;
//        var wDegree = wGen.Degree;

//        // Build coefficient arrays placeholders
//        var uTarget = new double[uDegree + 1];
//        var vTarget = new double[vDegree + 1];
//        var wTarget = new double[wDegree + 1];

//        // Set transformed endpoints (index 0 and last)
//        uTarget[0] = z0P.Real;
//        vTarget[0] = z0P.Imaginary;
//        uTarget[uDegree] = zNP.Real;
//        vTarget[vDegree] = zNP.Imaginary;

//        wTarget[0] = w0P;
//        wTarget[wDegree] = wNP;

//        // Collect indices/positions of interior coefficients that will be parameters
//        // For generality we will collect interior coeffs in this order:
//        // [u idx=1..uDegree-1], [v idx=1..vDegree-1], [w idx=1..wDegree-1]
//        var paramIndices = new List<(char poly, int idx)>(); // poly: 'u','v','w'

//        if (adjustAllInteriorCoeffs)
//        {
//            for (var i = 1; i <= Math.Max(0, uDegree - 1); i++)
//                paramIndices.Add(('u', i));

//            for (var i = 1; i <= Math.Max(0, vDegree - 1); i++)
//                paramIndices.Add(('v', i));

//            for (var i = 1; i <= Math.Max(0, wDegree - 1); i++)
//                paramIndices.Add(('w', i));
//        }
//        else
//        {
//            // If not adjusting all, you can implement custom selection here.
//            // For simplicity default to same behavior as adjustAllInteriorCoeffs.
//            for (var i = 1; i <= Math.Max(0, uDegree - 1); i++)
//                paramIndices.Add(('u', i));

//            for (var i = 1; i <= Math.Max(0, vDegree - 1); i++)
//                paramIndices.Add(('v', i));

//            for (var i = 1; i <= Math.Max(0, wDegree - 1); i++)
//                paramIndices.Add(('w', i));
//        }

//        // Build initial parameter vector from existing generator interior coefficients
//        var initialParams =
//            MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(paramIndices.Count);

//        for (var p = 0; p < paramIndices.Count; p++)
//        {
//            var (poly, idx) = paramIndices[p];
//            switch (poly)
//            {
//                case 'u':
//                    initialParams[p] = uGen.Coeffs[idx];
//                    break;
//                case 'v':
//                    initialParams[p] = vGen.Coeffs[idx];
//                    break;
//                case 'w':
//                    initialParams[p] = wGen.Coeffs[idx];
//                    break;
//                default:
//                    throw new InvalidOperationException();
//            }
//        }

//        // LM iterative refinement (outer loop similar to your quadratic code)
//        var current = initialParams.Clone();
//        var converged = false;
//        var finalPosRes = double.PositiveInfinity;
//        var iterCount = 0;

//        // dummy data for MathNet model (unused, but API requires vectors)
//        var xData = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(2);
//        var yData = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(2);

//        for (iterCount = 1; iterCount <= maxRefinementIterations; iterCount++)
//        {
//            var obj = ObjectiveFunction.NonlinearModel(
//                EndpointResidualModel,
//                EndpointResidualJacobian,
//                xData,
//                yData
//            );

//            var solver = new LevenbergMarquardtMinimizer(
//                gradientTolerance: 1e-14,
//                functionTolerance: 1e-14,
//                maximumIterations: lmMaxIterations
//            );

//            NonlinearMinimizationResult result;
//            try
//            {
//                result = solver.FindMinimum(obj, current);
//            }
//            catch
//            {
//                break;
//            }

//            current = result.MinimizingPoint.Clone();

//            var resVec = EndpointResidualModel(current, xData);
//            finalPosRes = resVec.L2Norm();

//            if (finalPosRes <= endpointTolerance)
//            {
//                converged = true;
//                break;
//            }
//        }

//        // Reconstruct final polynomials (uFinal,vFinal,wFinal)
//        var uFinalCoeffs = (double[])uTarget.Clone();
//        var vFinalCoeffs = (double[])vTarget.Clone();
//        var wFinalCoeffs = (double[])wTarget.Clone();

//        for (var p = 0; p < paramIndices.Count; p++)
//        {
//            var (poly, idx) = paramIndices[p];
//            var val = current[p];
//            switch (poly)
//            {
//                case 'u': uFinalCoeffs[idx] = val; break;
//                case 'v': vFinalCoeffs[idx] = val; break;
//                case 'w': wFinalCoeffs[idx] = val; break;
//            }
//        }

//        var uFinal = BernsteinPolynomial.Create(uFinalCoeffs);
//        var vFinal = BernsteinPolynomial.Create(vFinalCoeffs);
//        var wFinal = BernsteinPolynomial.Create(wFinalCoeffs);

//        return new RefitResult
//        {
//            Generators = new PhBernsteinCurve2D.GeneratorData()
//            {
//                U = uFinal,
//                V = vFinal,
//                W = wFinal,
//                P0 = p0,
//                S0 = generators.S0 // s0 might need scaling if you want to change it; kept as original
//            },
//            Converged = converged,
//            Iterations = iterCount,
//            FinalPositionResidual = finalPosRes
//        };

//        // Helper to get endpoints safely (use degree 0 coefficients if degree==0)
//        Complex GetZ0(BernsteinPolynomial u, BernsteinPolynomial v)
//            => new Complex(u.Coeffs[0], v.Coeffs[0]);

//        Complex GetZN(BernsteinPolynomial u, BernsteinPolynomial v)
//        {
//            var uN = u.Coeffs[u.Degree];
//            var vN = v.Coeffs[v.Degree];
//            return new Complex(uN, vN);
//        }

//        // LM solver will call these local helpers:
//        MathNet.Numerics.LinearAlgebra.Vector<double> EndpointResidualModel(MathNet.Numerics.LinearAlgebra.Vector<double> paramVec, MathNet.Numerics.LinearAlgebra.Vector<double> x)
//        {
//            // Reconstruct uCoeffs, vCoeffs, wCoeffs arrays using endpoints + current params
//            var uCoeffs = (double[])uTarget.Clone();
//            var vCoeffs = (double[])vTarget.Clone();
//            var wCoeffs = (double[])wTarget.Clone();

//            for (var p = 0; p < paramIndices.Count; p++)
//            {
//                var (poly, idx) = paramIndices[p];
//                var val = paramVec[p];
//                switch (poly)
//                {
//                    case 'u': uCoeffs[idx] = val; break;
//                    case 'v': vCoeffs[idx] = val; break;
//                    case 'w': wCoeffs[idx] = val; break;
//                }
//            }

//            var uNew = BernsteinPolynomial.Create(uCoeffs);
//            var vNew = BernsteinPolynomial.Create(vCoeffs);
//            var wNew = BernsteinPolynomial.Create(wCoeffs);

//            // Compute derivatives x'(t), y'(t):
//            // x' = w * (u^2 - v^2)
//            // y' = 2 * w * u * v
//            var xPrime = wNew * (uNew.Square() - vNew.Square());
//            var yPrime = (wNew * (uNew * vNew)).Times(2.0);

//            // Integrate and add p0
//            var xPoly = xPrime.Integral().Add(p0.X);
//            var yPoly = yPrime.Integral().Add(p0.Y);

//            // Evaluate at t=1 for endpoint
//            var p1EstX = xPoly.EvaluateAt(1.0);
//            var p1EstY = yPoly.EvaluateAt(1.0);

//            var r = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(2);
//            r[0] = p1EstX - p1.X;
//            r[1] = p1EstY - p1.Y;
//            return r;
//        }

//        Matrix<double> EndpointResidualJacobian(MathNet.Numerics.LinearAlgebra.Vector<double> p, MathNet.Numerics.LinearAlgebra.Vector<double> x)
//        {
//            const int nResiduals = 2;
//            const double eps = 1e-8;
//            var nParams = p.Count;
//            var jac = Matrix<double>.Build.Dense(nResiduals, nParams);

//            var f0 = EndpointResidualModel(p, x);
//            for (var j = 0; j < nParams; j++)
//            {
//                var pPert = p.Clone();
//                var dp = Math.Abs(p[j]) * eps + eps;
//                pPert[j] += dp;
//                var f1 = EndpointResidualModel(pPert, x);

//                for (var i = 0; i < nResiduals; i++)
//                    jac[i, j] = (f1[i] - f0[i]) / dp;
//            }

//            return jac;
//        }
//    }

//    public static RefitResult RefitEndpointTangentTransformGeneralAnalytical(
//        PhBernsteinCurve2D.GeneratorData generators,
//        LinFloat64Vector2D p0,
//        LinFloat64Vector2D p1,
//        double scalingFactor,
//        double rotateAngleTheta,
//        bool adjustAllInteriorCoeffs = true, // default: adjust every interior coeff of u,v,w
//        double endpointTolerance = 1e-7,
//        int maxRefinementIterations = 8,
//        int lmMaxIterations = 500
//    )
//    {
//        if (scalingFactor <= 0)
//            throw new ArgumentException("scalingFactor must be positive.");

//        // --- compute complex half-rotation/scale factor ---
//        var sqrtS = Math.Sqrt(scalingFactor);
//        var halfAngle = rotateAngleTheta / 2.0;
//        var complexRotHalf = Complex.FromPolarCoordinates(sqrtS, halfAngle);

//        var uGen = generators.U;
//        var vGen = generators.V;
//        var wGen = generators.W;

//        // Transformed endpoint values for u,v (complex multiply)
//        var z0 = GetZ0(uGen, vGen);
//        var zN = GetZN(uGen, vGen);
//        var z0P = z0 * complexRotHalf;
//        var zNp = zN * complexRotHalf;

//        // Transformed endpoint values for w: scale endpoints by s
//        var w0P = wGen.Coeffs[0];// * scalingFactor;
//        var wNp = wGen.Coeffs[wGen.Degree];// * scalingFactor;

//        // Prepare endpoint coefficient arrays for u,v,w after transformation
//        var uDegree = uGen.Degree;
//        var vDegree = vGen.Degree;
//        var wDegree = wGen.Degree;

//        var uTarget = new double[uDegree + 1];
//        var vTarget = new double[vDegree + 1];
//        var wTarget = new double[wDegree + 1];

//        // Set transformed endpoints (index 0 and last)
//        uTarget[0] = z0P.Real;
//        vTarget[0] = z0P.Imaginary;
//        uTarget[uDegree] = zNp.Real;
//        vTarget[vDegree] = zNp.Imaginary;

//        wTarget[0] = w0P;
//        wTarget[wDegree] = wNp;

//        // Collect indices/positions of interior coefficients that will be parameters
//        var paramIndices = new List<(char poly, int idx)>(); // poly: 'u','v','w'

//        if (adjustAllInteriorCoeffs)
//        {
//            for (var i = 1; i <= Math.Max(0, uDegree - 1); i++)
//                paramIndices.Add(('u', i));

//            for (var i = 1; i <= Math.Max(0, vDegree - 1); i++)
//                paramIndices.Add(('v', i));

//            for (var i = 1; i <= Math.Max(0, wDegree - 1); i++)
//                paramIndices.Add(('w', i));
//        }
//        else
//        {
//            // Default: same as above (you may change to a custom selection)
//            for (var i = 1; i <= Math.Max(0, uDegree - 1); i++)
//                paramIndices.Add(('u', i));

//            for (var i = 1; i <= Math.Max(0, vDegree - 1); i++)
//                paramIndices.Add(('v', i));

//            for (var i = 1; i <= Math.Max(0, wDegree - 1); i++)
//                paramIndices.Add(('w', i));
//        }

//        // Build initial parameter vector from existing generator interior coefficients
//        var initialParams = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(paramIndices.Count);
//        for (var p = 0; p < paramIndices.Count; p++)
//        {
//            var (poly, idx) = paramIndices[p];
//            initialParams[p] = poly switch
//            {
//                'u' => uGen.Coeffs[idx],
//                'v' => vGen.Coeffs[idx],
//                'w' => wGen.Coeffs[idx],
//                _ => throw new InvalidOperationException()
//            };
//        }

//        // ---------------------------
//        // Local functions for analytic Jacobian
//        // ---------------------------

//        // ---------------------------
//        // LM outer loop (same pattern as before)
//        // ---------------------------
//        var current = initialParams.Clone();
//        var converged = false;
//        var finalPosRes = double.PositiveInfinity;
//        var iterCount = 0;

//        var xData = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(2);
//        var yData = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(2);

//        for (iterCount = 1; iterCount <= maxRefinementIterations; iterCount++)
//        {
//            var obj = ObjectiveFunction.NonlinearModel(
//                EndpointResidualModel,
//                EndpointResidualJacobian,
//                xData,
//                yData
//            );

//            var solver = new LevenbergMarquardtMinimizer(
//                gradientTolerance: 1e-14,
//                functionTolerance: 1e-14,
//                maximumIterations: lmMaxIterations
//            );

//            NonlinearMinimizationResult result;
//            try
//            {
//                result = solver.FindMinimum(obj, current);
//            }
//            catch
//            {
//                break;
//            }

//            current = result.MinimizingPoint.Clone();

//            var resVec = EndpointResidualModel(current, xData);
//            finalPosRes = resVec.L2Norm();

//            if (finalPosRes <= endpointTolerance)
//            {
//                converged = true;
//                break;
//            }
//        }

//        // Reconstruct final polynomials
//        var uFinalCoeffs = (double[])uTarget.Clone();
//        var vFinalCoeffs = (double[])vTarget.Clone();
//        var wFinalCoeffs = (double[])wTarget.Clone();

//        for (var p = 0; p < paramIndices.Count; p++)
//        {
//            var (poly, idx) = paramIndices[p];
//            var val = current[p];
//            switch (poly)
//            {
//                case 'u': uFinalCoeffs[idx] = val; break;
//                case 'v': vFinalCoeffs[idx] = val; break;
//                case 'w': wFinalCoeffs[idx] = val; break;
//            }
//        }

//        var uFinal = BernsteinPolynomial.Create(uFinalCoeffs);
//        var vFinal = BernsteinPolynomial.Create(vFinalCoeffs);
//        var wFinal = BernsteinPolynomial.Create(wFinalCoeffs);

//        return new RefitResult
//        {
//            Generators = new PhBernsteinCurve2D.GeneratorData()
//            {
//                U = uFinal,
//                V = vFinal,
//                W = wFinal,
//                P0 = p0,
//                S0 = generators.S0
//            },
//            Converged = converged,
//            Iterations = iterCount,
//            FinalPositionResidual = finalPosRes
//        };

//        // Helper to get endpoints safely (use degree 0 coefficients if degree==0)
//        Complex GetZ0(BernsteinPolynomial u, BernsteinPolynomial v)
//            => new Complex(u.Coeffs[0], v.Coeffs[0]);

//        Complex GetZN(BernsteinPolynomial u, BernsteinPolynomial v)
//        {
//            var uN = u.Coeffs[u.Degree];
//            var vN = v.Coeffs[v.Degree];
//            return new Complex(uN, vN);
//        }

//        // Binomial coefficient (multiplicative, stable for moderate n)
//        double Binomial(int n, int k)
//        {
//            if (k < 0 || k > n) return 0.0;
//            if (k == 0 || k == n) return 1.0;
//            k = Math.Min(k, n - k);
//            double num = 1.0, den = 1.0;
//            for (var i = 1; i <= k; i++)
//            {
//                num *= (n - (k - i));
//                den *= i;
//            }
//            return num / den;
//        }

//        double[,] BuildAlpha(int n)
//        {
//            var alpha = new double[n + 1, n + 1];
//            for (var i = 0; i <= n; i++)
//            {
//                var binni = Binomial(n, i);
//                for (var j = 0; j <= n; j++)
//                {
//                    var binnj = Binomial(n, j);
//                    var denom = Binomial(2 * n, i + j);
//                    alpha[i, j] = denom == 0.0 ? 0.0 : (binni * binnj) / denom;
//                }
//            }
//            return alpha;
//        }

//        double[,] BuildBeta(int m)
//        {
//            var beta = new double[m + 1, m + 1];
//            for (var i = 0; i <= m; i++)
//            {
//                var binni = Binomial(m, i);
//                for (var j = 0; j <= m; j++)
//                {
//                    var binnj = Binomial(m, j);
//                    var denom = Binomial(2 * m, i + j);
//                    beta[i, j] = denom == 0.0 ? 0.0 : (binni * binnj) / denom;
//                }
//            }
//            return beta;
//        }

//        double[,] BuildGamma(int n, int m)
//        {
//            var gamma = new double[n + 1, m + 1];
//            for (var i = 0; i <= n; i++)
//            {
//                var bini = Binomial(n, i);
//                for (var j = 0; j <= m; j++)
//                {
//                    var binj = Binomial(m, j);
//                    var denom = Binomial(n + m, i + j);
//                    gamma[i, j] = denom == 0.0 ? 0.0 : (bini * binj) / denom;
//                }
//            }
//            return gamma;
//        }

//        double[] ProductCoeffsSame(double[] coeffA)
//        {
//            var n = coeffA.Length - 1;
//            var deg = 2 * n;
//            var a = new double[deg + 1];
//            var alpha = BuildAlpha(n);
//            for (var r = 0; r <= deg; r++)
//            {
//                var sum = 0.0;
//                var iMin = Math.Max(0, r - n);
//                var iMax = Math.Min(n, r);
//                for (var i = iMin; i <= iMax; i++)
//                {
//                    var j = r - i;
//                    sum += coeffA[i] * coeffA[j] * alpha[i, j];
//                }
//                a[r] = sum;
//            }
//            return a;
//        }

//        double[] ProductCoeffsCross(double[] coeffU, double[] coeffV)
//        {
//            var n = coeffU.Length - 1;
//            var m = coeffV.Length - 1;
//            var deg = n + m;
//            var c = new double[deg + 1];
//            var gamma = BuildGamma(n, m);
//            for (var r = 0; r <= deg; r++)
//            {
//                var sum = 0.0;
//                var iMin = Math.Max(0, r - m);
//                var iMax = Math.Min(n, r);
//                for (var i = iMin; i <= iMax; i++)
//                {
//                    var j = r - i;
//                    sum += coeffU[i] * coeffV[j] * gamma[i, j];
//                }
//                c[r] = sum;
//            }
//            return c;
//        }

//        // Build the analytic Jacobian 2 x nParams using current full coefficients arrays
//        Matrix<double> BuildAnalyticJacobian(
//            double[] uCoeffs,
//            double[] vCoeffs,
//            double[] wCoeffs,
//            List<(char poly, int idx)> paramIdx
//        )
//        {
//            var n = uCoeffs.Length - 1;
//            var m = vCoeffs.Length - 1;
//            var p = wCoeffs.Length - 1;

//            var Nx = p + 2 * n;
//            var Ny = p + n + m;

//            var a = ProductCoeffsSame(uCoeffs); // u^2 coefficients
//            var b = ProductCoeffsSame(vCoeffs); // v^2 coefficients
//            var c = ProductCoeffsCross(uCoeffs, vCoeffs); // u*v coeffs

//            var S_w = 0.0;
//            foreach (var wk in wCoeffs)
//                S_w += wk;

//            var S_a = 0.0;
//            for (var r = 0; r < a.Length; r++)
//                S_a += a[r];

//            var S_b = 0.0;
//            for (var r = 0; r < b.Length; r++)
//                S_b += b[r];

//            var S_c = 0.0;
//            for (var r = 0; r < c.Length; r++)
//                S_c += c[r];

//            var sU2MinusV2 = S_a - S_b;

//            var alpha = BuildAlpha(n);
//            var beta = BuildBeta(m);
//            var gamma = BuildGamma(n, m);

//            var nParams = paramIdx.Count;
//            var jac = Matrix<double>.Build.Dense(2, nParams);

//            for (var pIndex = 0; pIndex < nParams; pIndex++)
//            {
//                var (poly, idx) = paramIdx[pIndex];
//                var dRx = 0.0;
//                var dRy = 0.0;

//                if (poly == 'u')
//                {
//                    var innerAlphaSum = 0.0;
//                    for (var j = 0; j <= n; j++)
//                        innerAlphaSum += uCoeffs[j] * alpha[idx, j];
//                    dRx = (S_w / (Nx + 1.0)) * 2.0 * innerAlphaSum;

//                    var innerGammaSum = 0.0;
//                    for (var j = 0; j <= m; j++)
//                        innerGammaSum += vCoeffs[j] * gamma[idx, j];
//                    dRy = (2.0 * S_w / (Ny + 1.0)) * innerGammaSum;
//                }
//                else if (poly == 'v')
//                {
//                    var innerBetaSum = 0.0;
//                    for (var j = 0; j <= m; j++)
//                        innerBetaSum += vCoeffs[j] * beta[idx, j];
//                    dRx = -(S_w / (Nx + 1.0)) * 2.0 * innerBetaSum;

//                    var innerGammaSum = 0.0;
//                    for (var i = 0; i <= n; i++)
//                        innerGammaSum += uCoeffs[i] * gamma[i, idx];
//                    dRy = (2.0 * S_w / (Ny + 1.0)) * innerGammaSum;
//                }
//                else if (poly == 'w')
//                {
//                    dRx = sU2MinusV2 / (Nx + 1.0);
//                    dRy = 2.0 * S_c / (Ny + 1.0);
//                }
//                else
//                {
//                    throw new ArgumentException("Unknown poly type in paramIndices. Use 'u','v' or 'w'.");
//                }

//                jac[0, pIndex] = dRx;
//                jac[1, pIndex] = dRy;
//            }

//            return jac;
//        }

//        // Residual model (unchanged)
//        MathNet.Numerics.LinearAlgebra.Vector<double> EndpointResidualModel(
//            MathNet.Numerics.LinearAlgebra.Vector<double> paramVec,
//            MathNet.Numerics.LinearAlgebra.Vector<double> x
//        )
//        {
//            // Reconstruct full coefficients
//            var uCoeffs = (double[])uTarget.Clone();
//            var vCoeffs = (double[])vTarget.Clone();
//            var wCoeffs = (double[])wTarget.Clone();

//            for (var p = 0; p < paramIndices.Count; p++)
//            {
//                var (poly, idx) = paramIndices[p];
//                var val = paramVec[p];
//                switch (poly)
//                {
//                    case 'u': uCoeffs[idx] = val; break;
//                    case 'v': vCoeffs[idx] = val; break;
//                    case 'w': wCoeffs[idx] = val; break;
//                }
//            }

//            var uNew = BernsteinPolynomial.Create(uCoeffs);
//            var vNew = BernsteinPolynomial.Create(vCoeffs);
//            var wNew = BernsteinPolynomial.Create(wCoeffs);

//            var xPrime = wNew * (uNew.Square() - vNew.Square());
//            var yPrime = (wNew * (uNew * vNew)).Times(2.0);

//            var xPoly = xPrime.Integral().Add(p0.X);
//            var yPoly = yPrime.Integral().Add(p0.Y);

//            var p1EstX = xPoly.EvaluateAt(1.0);
//            var p1EstY = yPoly.EvaluateAt(1.0);

//            var r = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(2);
//            r[0] = p1EstX - p1.X;
//            r[1] = p1EstY - p1.Y;
//            return r;
//        }

//        // New analytic Jacobian wrapper that reconstructs full coeff arrays then calls BuildAnalyticJacobian
//        Matrix<double> EndpointResidualJacobian(
//            MathNet.Numerics.LinearAlgebra.Vector<double> paramVec,
//            MathNet.Numerics.LinearAlgebra.Vector<double> x
//        )
//        {
//            var uCoeffs = (double[])uTarget.Clone();
//            var vCoeffs = (double[])vTarget.Clone();
//            var wCoeffs = (double[])wTarget.Clone();

//            for (var p = 0; p < paramIndices.Count; p++)
//            {
//                var (poly, idx) = paramIndices[p];
//                var val = paramVec[p];
//                switch (poly)
//                {
//                    case 'u': uCoeffs[idx] = val; break;
//                    case 'v': vCoeffs[idx] = val; break;
//                    case 'w': wCoeffs[idx] = val; break;
//                }
//            }

//            return BuildAnalyticJacobian(uCoeffs, vCoeffs, wCoeffs, paramIndices);
//        }
//    }

//    public static RefitResult RefitEndpointTangentAccelerationTransformGeneralNumerical(
//        PhBernsteinCurve2D.GeneratorData generators,
//        LinFloat64Vector2D p0, // target start point (after map)
//        LinFloat64Vector2D p1, // target end point (after map)
//        double scalingFactor,
//        double rotateAngleTheta,
//        LinFloat64Vector2D desiredT0, // desired tangent at t=0 after mapping
//        LinFloat64Vector2D desiredT1, // desired tangent at t=1 after mapping
//        LinFloat64Vector2D desiredA0, // desired acceleration at t=0 after mapping
//        LinFloat64Vector2D desiredA1, // desired acceleration at t=1 after mapping
//        bool adjustAllInteriorCoeffs = true,
//        double endpointTolerance = 1e-7,
//        int maxRefinementIterations = 8,
//        int lmMaxIterations = 500
//    )
//    {
//        if (scalingFactor <= 0)
//            throw new ArgumentException("scalingFactor must be positive.");

//        // compute complex half-rotation/scale factor (applies to z = u + i v)
//        var sqrtS = Math.Sqrt(scalingFactor);
//        var halfAngle = rotateAngleTheta / 2.0;
//        var complexRotHalf = Complex.FromPolarCoordinates(sqrtS, halfAngle);

//        var uGen = generators.U;
//        var vGen = generators.V;
//        var wGen = generators.W;

//        var z0 = GetZ0(uGen, vGen);
//        var zN = GetZN(uGen, vGen);
//        var z0P = z0 * complexRotHalf;
//        var zNP = zN * complexRotHalf;

//        // w endpoints (kept as-is scaled by scale? previous code kept the same)
//        var w0P = wGen.Coeffs[0];
//        var wNP = wGen.Coeffs[wGen.Degree];

//        // Prepare coefficient arrays with fixed endpoints
//        var uDegree = uGen.Degree;
//        var vDegree = vGen.Degree;
//        var wDegree = wGen.Degree;

//        var uTarget = new double[uDegree + 1];
//        var vTarget = new double[vDegree + 1];
//        var wTarget = new double[wDegree + 1];

//        uTarget[0] = z0P.Real;
//        vTarget[0] = z0P.Imaginary;
//        uTarget[uDegree] = zNP.Real;
//        vTarget[vDegree] = zNP.Imaginary;

//        wTarget[0] = w0P;
//        wTarget[wDegree] = wNP;

//        // assemble paramIndices like other refitters (interior coefficients)
//        var paramIndices = new List<(char poly, int idx)>();
//        if (adjustAllInteriorCoeffs)
//        {
//            for (var i = 1; i <= Math.Max(0, uDegree - 1); i++) paramIndices.Add(('u', i));
//            for (var i = 1; i <= Math.Max(0, vDegree - 1); i++) paramIndices.Add(('v', i));
//            for (var i = 1; i <= Math.Max(0, wDegree - 1); i++) paramIndices.Add(('w', i));
//        }
//        else
//        {
//            // same default behavior (you can refine selection)
//            for (var i = 1; i <= Math.Max(0, uDegree - 1); i++) paramIndices.Add(('u', i));
//            for (var i = 1; i <= Math.Max(0, vDegree - 1); i++) paramIndices.Add(('v', i));
//            for (var i = 1; i <= Math.Max(0, wDegree - 1); i++) paramIndices.Add(('w', i));
//        }

//        // build initial parameter vector from current interior coefficients
//        var initialParams = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(paramIndices.Count);
//        for (var p = 0; p < paramIndices.Count; p++)
//        {
//            var (poly, idx) = paramIndices[p];
//            initialParams[p] = poly switch
//            {
//                'u' => uGen.Coeffs[idx],
//                'v' => vGen.Coeffs[idx],
//                'w' => wGen.Coeffs[idx],
//                _ => throw new InvalidOperationException()
//            };
//        }

//        var current = initialParams.Clone();
//        var converged = false;
//        var finalRes = double.PositiveInfinity;
//        var iterCount = 0;

//        // LM outer loop
//        var xData = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(10); // dummy
//        for (iterCount = 1; iterCount <= maxRefinementIterations; iterCount++)
//        {
//            // objective with numerical jacobian wrapper
//            var obj = ObjectiveFunction.NonlinearModel(
//                EndpointResidualModel,
//                EndpointResidualJacobian,
//                xData,
//                xData
//            );

//            var solver = new LevenbergMarquardtMinimizer(
//                gradientTolerance: 1e-14,
//                functionTolerance: 1e-14,
//                maximumIterations: lmMaxIterations
//            );

//            NonlinearMinimizationResult result;
//            try
//            {
//                result = solver.FindMinimum(obj, current);
//            }
//            catch
//            {
//                break;
//            }

//            current = result.MinimizingPoint.Clone();

//            var resVec = EndpointResidualModel(current, xData);
//            finalRes = resVec.L2Norm();

//            if (finalRes <= endpointTolerance)
//            {
//                converged = true;
//                break;
//            }
//        }

//        // Reconstruct final polynomials
//        var uFinalCoeffs = (double[])uTarget.Clone();
//        var vFinalCoeffs = (double[])vTarget.Clone();
//        var wFinalCoeffs = (double[])wTarget.Clone();

//        for (var p = 0; p < paramIndices.Count; p++)
//        {
//            var (poly, idx) = paramIndices[p];
//            var val = current[p];
//            switch (poly)
//            {
//                case 'u': uFinalCoeffs[idx] = val; break;
//                case 'v': vFinalCoeffs[idx] = val; break;
//                case 'w': wFinalCoeffs[idx] = val; break;
//            }
//        }

//        var uFinal = BernsteinPolynomial.Create(uFinalCoeffs);
//        var vFinal = BernsteinPolynomial.Create(vFinalCoeffs);
//        var wFinal = BernsteinPolynomial.Create(wFinalCoeffs);

//        return new RefitResult
//        {
//            Generators = new PhBernsteinCurve2D.GeneratorData()
//            {
//                U = uFinal,
//                V = vFinal,
//                W = wFinal,
//                P0 = p0,
//                S0 = generators.S0
//            },
//            Converged = converged,
//            Iterations = iterCount,
//            FinalPositionResidual = finalRes
//        };

//        // Transform endpoint complex z values using half-rotation-scale
//        Complex GetZ0(BernsteinPolynomial u, BernsteinPolynomial v)
//            => new Complex(u.Coeffs[0], v.Coeffs[0]);

//        Complex GetZN(BernsteinPolynomial u, BernsteinPolynomial v)
//            => new Complex(u.Coeffs[u.Degree], v.Coeffs[v.Degree]);

//        // -----------------------
//        // Local helper models
//        // -----------------------
//        // Residual model: returns 10-vector
//        // order: [posDiff.x, posDiff.y,
//        //         t0Diff.x, t0Diff.y,
//        //         t1Diff.x, t1Diff.y,
//        //         a0Diff.x, a0Diff.y,
//        //         a1Diff.x, a1Diff.y]
//        MathNet.Numerics.LinearAlgebra.Vector<double> EndpointResidualModel(MathNet.Numerics.LinearAlgebra.Vector<double> paramVec, MathNet.Numerics.LinearAlgebra.Vector<double> _)
//        {
//            // Reconstruct full coefficient arrays
//            var uCoeffs = (double[])uTarget.Clone();
//            var vCoeffs = (double[])vTarget.Clone();
//            var wCoeffs = (double[])wTarget.Clone();

//            for (var p = 0; p < paramIndices.Count; p++)
//            {
//                var (poly, idx) = paramIndices[p];
//                var val = paramVec[p];
//                switch (poly)
//                {
//                    case 'u': uCoeffs[idx] = val; break;
//                    case 'v': vCoeffs[idx] = val; break;
//                    case 'w': wCoeffs[idx] = val; break;
//                }
//            }

//            var uNew = BernsteinPolynomial.Create(uCoeffs);
//            var vNew = BernsteinPolynomial.Create(vCoeffs);
//            var wNew = BernsteinPolynomial.Create(wCoeffs);

//            // x', y'
//            var xPrime = wNew * (uNew.Square() - vNew.Square());
//            var yPrime = (wNew * (uNew * vNew)).Times(2.0);

//            // positions: integrate and add p0
//            var xPoly = xPrime.Integral().Add(p0.X);
//            var yPoly = yPrime.Integral().Add(p0.Y);

//            var p1EstX = xPoly.EvaluateAt(1.0);
//            var p1EstY = yPoly.EvaluateAt(1.0);

//            // tangents: evaluate x', y' at endpoints
//            var t0x = xPrime.EvaluateAt(0.0);
//            var t0y = yPrime.EvaluateAt(0.0);
//            var t1x = xPrime.EvaluateAt(1.0);
//            var t1y = yPrime.EvaluateAt(1.0);

//            // accelerations: derivatives of x', y' (i.e. x'', y'')
//            var xPrimeDt = xPrime.Derivative();
//            var yPrimeDt = yPrime.Derivative();

//            var a0x = xPrimeDt.EvaluateAt(0.0);
//            var a0y = yPrimeDt.EvaluateAt(0.0);
//            var a1x = xPrimeDt.EvaluateAt(1.0);
//            var a1y = yPrimeDt.EvaluateAt(1.0);

//            var r = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(10);

//            // position residuals (end point p1)
//            r[0] = p1EstX - p1.X;
//            r[1] = p1EstY - p1.Y;

//            // tangent residuals (we expect tangents to match desiredT0/T1)
//            r[2] = t0x - desiredT0.X;
//            r[3] = t0y - desiredT0.Y;

//            r[4] = t1x - desiredT1.X;
//            r[5] = t1y - desiredT1.Y;

//            // acceleration residuals
//            r[6] = a0x - desiredA0.X;
//            r[7] = a0y - desiredA0.Y;

//            r[8] = a1x - desiredA1.X;
//            r[9] = a1y - desiredA1.Y;

//            return r;
//        }

//        // Numerical Jacobian by finite differences (10 x nParams)
//        Matrix<double> EndpointResidualJacobian(MathNet.Numerics.LinearAlgebra.Vector<double> p, MathNet.Numerics.LinearAlgebra.Vector<double> _)
//        {
//            const int nResiduals = 10;
//            const double eps = 1e-8;
//            var nParams = p.Count;
//            var jac = Matrix<double>.Build.Dense(nResiduals, nParams);

//            var f0 = EndpointResidualModel(p, null);
//            for (var j = 0; j < nParams; j++)
//            {
//                var pPert = p.Clone();
//                var dp = Math.Abs(p[j]) * eps + eps;
//                pPert[j] += dp;
//                var f1 = EndpointResidualModel(pPert, null);

//                for (var i = 0; i < nResiduals; i++)
//                    jac[i, j] = (f1[i] - f0[i]) / dp;
//            }

//            return jac;
//        }
//    }

//}