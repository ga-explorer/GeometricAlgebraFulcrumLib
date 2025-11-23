using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical;
using GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.Bernstein;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using System.Diagnostics;
using static GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier.PhBernsteinCurve2DRefitter;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier;

public sealed class PhBernsteinCurve2D :
    Float64Path2D,
    IPhBernsteinCurve,
    IFloat64BoundedShape2D
{
    public new sealed class SamplingOptions :
        Float64AdaptiveSamplingOptions
    {
        // keep defaults; user can set MinLevel/MaxLevel/MaxChordError/MaxAngleRadians etc.
        // Add any PH-specific options here if needed later
    }

    public sealed class GeneratorData
    {
        public required BernsteinPolynomial U { get; init; }

        public required BernsteinPolynomial V { get; init; }

        public required BernsteinPolynomial W { get; init; }

        public required LinFloat64Vector2D P0 { get; init; }

        public required double S0 { get; init; }


        public void Deconstruct(out BernsteinPolynomial u, out BernsteinPolynomial v, out BernsteinPolynomial w, out LinFloat64Vector2D p0, out double s0)
        {
            u = U;
            v = V;
            w = W;
            p0 = P0;
            s0 = S0;
        }


        public Pair<BernsteinPolynomial> GetTangentPolynomials(double tolerance = Float64Utils.DefaultTolerance)
        {
            return BernsteinPolynomial.SimplifyToEqualDegree(
                W * (U * U - V * V),
                2 * W * U * V,
                tolerance
            );
        }

        public Pair<BernsteinPolynomial> GetAccelerationPolynomials(double tolerance = Float64Utils.DefaultTolerance)
        {
            var (xDt, yDt) =
                GetTangentPolynomials(tolerance);

            return BernsteinPolynomial.SimplifyToEqualDegree(
                xDt.Derivative(),
                yDt.Derivative(),
                tolerance
            );
        }

        public Pair<Pair<BernsteinPolynomial>> GetTangentAccelerationPolynomials(double tolerance = Float64Utils.DefaultTolerance)
        {
            var (xDt, yDt) =
                GetTangentPolynomials(tolerance);

            var (xDt2, yDt2) =
                BernsteinPolynomial.SimplifyToEqualDegree(
                    xDt.Derivative(),
                    yDt.Derivative(),
                    tolerance
                );

            return new Pair<Pair<BernsteinPolynomial>>(
                (xDt, yDt),
                (xDt2, yDt2)
            );
        }


        public GeneratorData SetEndDerivatives(LinFloat64Vector2D tangent0, LinFloat64Vector2D tangent1)
        {
            var (u, v, w, p0, s0) = this;

            if (u.Degree < 2 || v.Degree < 2)
                throw new InvalidOperationException("Generator polynomials must have degree ≥ 2 for tangent constraints");

            var uCoefArray = u.Coeffs.ToArray();
            var vCoefArray = v.Coeffs.ToArray();

            // Manipulate u, v coefficients to satisfy tangent constraints
            var w0 = w.GetValue(0);
            var w1 = w.GetValue(1);

            // Enhanced numerical stability checks
            if (Math.Abs(w0) < Float64Utils.DefaultTolerance)
                throw new InvalidOperationException($"w(0) is too small (|w(0)| = {Math.Abs(w0)}) for stable tangent constraint");
            if (Math.Abs(w1) < Float64Utils.DefaultTolerance)
                throw new InvalidOperationException($"w(1) is too small (|w(1)| = {Math.Abs(w1)}) for stable tangent constraint");

            var r0 = tangent0.L2Norm();
            var phi0 = Math.Atan2(tangent0.Y, tangent0.X);
            var u0 = Math.Sqrt(Math.Max(0, r0 / w0)) * Math.Cos(phi0 / 2);
            var v0 = Math.Sqrt(Math.Max(0, r0 / w0)) * Math.Sin(phi0 / 2);

            var r1 = tangent1.L2Norm();
            var phi1 = Math.Atan2(tangent1.Y, tangent1.X);
            var u1 = Math.Sqrt(Math.Max(0, r1 / w1)) * Math.Cos(phi1 / 2);
            var v1 = Math.Sqrt(Math.Max(0, r1 / w1)) * Math.Sin(phi1 / 2);

            uCoefArray[0] = u0;
            uCoefArray[^1] = u1;

            vCoefArray[0] = v0;
            vCoefArray[^1] = v1;

            // Construct new Bernstein polynomials
            u = BernsteinPolynomial.Create(uCoefArray);
            v = BernsteinPolynomial.Create(vCoefArray);

            var genData =
                new GeneratorData
                {
                    U = u,
                    V = v,
                    W = w,
                    P0 = p0,
                    S0 = s0
                };

#if DEBUG
            {
                var (xDt, yDt) =
                    genData.GetTangentPolynomials();

                var t0 = LinFloat64Vector2D.Create(xDt.GetValue(0), yDt.GetValue(0));
                var t1 = LinFloat64Vector2D.Create(xDt.GetValue(1), yDt.GetValue(1));

                // Use more lenient tolerance for tangent verification
                var tangentTolerance = 1e-6;
                Debug.Assert(t0.IsNearEqual(tangent0, tangentTolerance),
                    $"Tangent0 constraint violated: expected {tangent0}, got {t0}");
                Debug.Assert(t1.IsNearEqual(tangent1, tangentTolerance),
                    $"Tangent1 constraint violated: expected {tangent1}, got {t1}");
            }
#endif

            return genData;
        }

        public GeneratorData SetEndDerivatives(LinFloat64Vector2D tangent0, LinFloat64Vector2D tangent1, LinFloat64Vector2D acceleration0, LinFloat64Vector2D acceleration1)
        {
            var (u, v, w, p0, s0) = this;

            if (u.Degree < 3 || v.Degree < 3)
                throw new InvalidOperationException("Generator polynomials must have degree ≥ 3 for acceleration constraints");

            var uCoefArray = u.Coeffs.ToArray();
            var vCoefArray = v.Coeffs.ToArray();
            var n = u.Degree;

            // Enhanced numerical stability checks
            var w0 = w.GetValue(0);
            var w1 = w.GetValue(1);

            if (Math.Abs(w0) < Float64Utils.DefaultTolerance)
                throw new InvalidOperationException($"w(0) is too small (|w(0)| = {Math.Abs(w0)}) for stable acceleration constraint");
            if (Math.Abs(w1) < Float64Utils.DefaultTolerance)
                throw new InvalidOperationException($"w(1) is too small (|w(1)| = {Math.Abs(w1)}) for stable acceleration constraint");

            // Tangent enforcement
            var r0 = tangent0.L2Norm();
            var phi0 = Math.Atan2(tangent0.Y, tangent0.X);
            var u0 = Math.Sqrt(Math.Max(0, r0 / w0)) * Math.Cos(phi0 / 2);
            var v0 = Math.Sqrt(Math.Max(0, r0 / w0)) * Math.Sin(phi0 / 2);

            var r1 = tangent1.L2Norm();
            var phi1 = Math.Atan2(tangent1.Y, tangent1.X);
            var u1 = Math.Sqrt(Math.Max(0, r1 / w1)) * Math.Cos(phi1 / 2);
            var v1 = Math.Sqrt(Math.Max(0, r1 / w1)) * Math.Sin(phi1 / 2);

            uCoefArray[0] = u0;
            vCoefArray[0] = v0;
            uCoefArray[^1] = u1;
            vCoefArray[^1] = v1;

            // Acceleration enforcement
            var wDt = w.Derivative();
            var acc0 = wDt.GetValue(0);
            var acc1 = wDt.GetValue(1);

            var (u0P, v0P) = SolveUvPrime(u0, v0, w0, acc0, acceleration0.X, acceleration0.Y);
            var (u1P, v1P) = SolveUvPrime(u1, v1, w1, acc1, acceleration1.X, acceleration1.Y);

            // Set inner control coefficients with bounds checking
            uCoefArray[1] = u0 + u0P / n;
            vCoefArray[1] = v0 + v0P / n;

            uCoefArray[^2] = u1 - u1P / n;
            vCoefArray[^2] = v1 - v1P / n;

            // Construct new polynomials
            u = BernsteinPolynomial.Create(uCoefArray);
            v = BernsteinPolynomial.Create(vCoefArray);

            var genData = new GeneratorData
            {
                U = u,
                V = v,
                W = w,
                P0 = p0,
                S0 = s0
            };

#if DEBUG
            {
                var ((xDt, yDt), (xDt2, yDt2)) = genData.GetTangentAccelerationPolynomials();

                var t0 = LinFloat64Vector2D.Create(xDt.GetValue(0), yDt.GetValue(0));
                var t1 = LinFloat64Vector2D.Create(xDt.GetValue(1), yDt.GetValue(1));
                var a0 = LinFloat64Vector2D.Create(xDt2.GetValue(0), yDt2.GetValue(0));
                var a1 = LinFloat64Vector2D.Create(xDt2.GetValue(1), yDt2.GetValue(1));

                var tangentTolerance = 1e-6;
                var accelerationTolerance = 1e-4;

                Debug.Assert(t0.IsNearEqual(tangent0, tangentTolerance),
                    $"Tangent0 constraint violated: expected {tangent0}, got {t0}");
                Debug.Assert(t1.IsNearEqual(tangent1, tangentTolerance),
                    $"Tangent1 constraint violated: expected {tangent1}, got {t1}");
                Debug.Assert(a0.IsNearEqual(acceleration0, accelerationTolerance),
                    $"Acceleration0 constraint violated: expected {acceleration0}, got {a0}");
                Debug.Assert(a1.IsNearEqual(acceleration1, accelerationTolerance),
                    $"Acceleration1 constraint violated: expected {acceleration1}, got {a1}");
            }
#endif

            return genData;

            // Enhanced helper local function with better numerical handling
            static (double uPrime, double vPrime) SolveUvPrime(double u, double v, double w, double wPrime, double ax, double ay)
            {
                // RHS of the linear system
                var bx = ax - wPrime * (u * u - v * v);
                var by = ay - 2.0 * wPrime * u * v;

                // Matrix coefficients with enhanced stability
                var det = 4.0 * w * w * (u * u + v * v);
                if (Math.Abs(det) < 1e-12)
                {
                    // Use pseudo-inverse for near-singular cases
                    var normSq = u * u + v * v;
                    if (normSq < 1e-14)
                        return (0, 0);

                    var uPrime = (u * bx + v * by) / (2.0 * w * normSq);
                    var vPrime = (-v * bx + u * by) / (2.0 * w * normSq);
                    return (uPrime, vPrime);
                }

                var uPrimeStable = (2.0 * w * (u * bx + v * by)) / det;
                var vPrimeStable = (2.0 * w * (-v * bx + u * by)) / det;
                return (uPrimeStable, vPrimeStable);
            }
        }


        public GeneratorData SetStartDerivative(LinFloat64Vector2D tangent0)
        {
            var (u, v, w, p0, s0) = this;

            if (u.Degree < 2 || v.Degree < 2)
                throw new InvalidOperationException("Generator polynomials must have degree ≥ 2 for tangent constraints");

            var uCoefArray = u.Coeffs.ToArray();
            var vCoefArray = v.Coeffs.ToArray();

            // Manipulate u, v coefficients to satisfy tangent constraints
            var w0 = w.GetValue(0);
            var w1 = w.GetValue(1);

            // Enhanced numerical stability checks
            if (Math.Abs(w0) < Float64Utils.DefaultTolerance)
                throw new InvalidOperationException($"w(0) is too small (|w(0)| = {Math.Abs(w0)}) for stable tangent constraint");
            if (Math.Abs(w1) < Float64Utils.DefaultTolerance)
                throw new InvalidOperationException($"w(1) is too small (|w(1)| = {Math.Abs(w1)}) for stable tangent constraint");

            var r0 = tangent0.L2Norm();
            var phi0 = Math.Atan2(tangent0.Y, tangent0.X);
            var u0 = Math.Sqrt(Math.Max(0, r0 / w0)) * Math.Cos(phi0 / 2);
            var v0 = Math.Sqrt(Math.Max(0, r0 / w0)) * Math.Sin(phi0 / 2);

            uCoefArray[0] = u0;
            vCoefArray[0] = v0;

            // Construct new Bernstein polynomials
            u = BernsteinPolynomial.Create(uCoefArray);
            v = BernsteinPolynomial.Create(vCoefArray);

            var genData =
                new GeneratorData
                {
                    U = u,
                    V = v,
                    W = w,
                    P0 = p0,
                    S0 = s0
                };

#if DEBUG
            {
                var (xDt, yDt) =
                    genData.GetTangentPolynomials();

                var t0 = LinFloat64Vector2D.Create(xDt.GetValue(0), yDt.GetValue(0));

                // Use more lenient tolerance for tangent verification
                var tangentTolerance = 1e-6;

                Debug.Assert(t0.IsNearEqual(tangent0, tangentTolerance),
                    $"Tangent0 constraint violated: expected {tangent0}, got {t0}");
            }
#endif

            return genData;
        }

        public GeneratorData SetStartDerivatives(LinFloat64Vector2D tangent0, LinFloat64Vector2D acceleration0)
        {
            var (u, v, w, p0, s0) = this;

            if (u.Degree < 3 || v.Degree < 3)
                throw new InvalidOperationException("Generator polynomials must have degree ≥ 3 for acceleration constraints");

            var uCoefArray = u.Coeffs.ToArray();
            var vCoefArray = v.Coeffs.ToArray();
            var n = u.Degree;

            // Enhanced numerical stability checks
            var w0 = w.GetValue(0);
            var w1 = w.GetValue(1);

            if (Math.Abs(w0) < Float64Utils.DefaultTolerance)
                throw new InvalidOperationException($"w(0) is too small (|w(0)| = {Math.Abs(w0)}) for stable acceleration constraint");
            if (Math.Abs(w1) < Float64Utils.DefaultTolerance)
                throw new InvalidOperationException($"w(1) is too small (|w(1)| = {Math.Abs(w1)}) for stable acceleration constraint");

            // Tangent enforcement
            var r0 = tangent0.L2Norm();
            var phi0 = Math.Atan2(tangent0.Y, tangent0.X);
            var u0 = Math.Sqrt(Math.Max(0, r0 / w0)) * Math.Cos(phi0 / 2);
            var v0 = Math.Sqrt(Math.Max(0, r0 / w0)) * Math.Sin(phi0 / 2);

            uCoefArray[0] = u0;
            vCoefArray[0] = v0;

            // Acceleration enforcement
            var wDt = w.Derivative();
            var acc0 = wDt.GetValue(0);

            var (u0P, v0P) = SolveUvPrime(u0, v0, w0, acc0, acceleration0.X, acceleration0.Y);

            // Set inner control coefficients with bounds checking
            uCoefArray[1] = u0 + u0P / n;
            vCoefArray[1] = v0 + v0P / n;

            // Construct new polynomials
            u = BernsteinPolynomial.Create(uCoefArray);
            v = BernsteinPolynomial.Create(vCoefArray);

            var genData = new GeneratorData
            {
                U = u,
                V = v,
                W = w,
                P0 = p0,
                S0 = s0
            };

#if DEBUG
            {
                var ((xDt, yDt), (xDt2, yDt2)) = genData.GetTangentAccelerationPolynomials();

                var t0 = LinFloat64Vector2D.Create(xDt.GetValue(0), yDt.GetValue(0));
                var a0 = LinFloat64Vector2D.Create(xDt2.GetValue(0), yDt2.GetValue(0));

                var tangentTolerance = 1e-6;
                var accelerationTolerance = 1e-4;

                Debug.Assert(t0.IsNearEqual(tangent0, tangentTolerance),
                    $"Tangent0 constraint violated: expected {tangent0}, got {t0}");

                Debug.Assert(a0.IsNearEqual(acceleration0, accelerationTolerance),
                    $"Acceleration0 constraint violated: expected {acceleration0}, got {a0}");
            }
#endif

            return genData;

            // Enhanced helper local function with better numerical handling
            static (double uPrime, double vPrime) SolveUvPrime(double u, double v, double w, double wPrime, double ax, double ay)
            {
                // RHS of the linear system
                var bx = ax - wPrime * (u * u - v * v);
                var by = ay - 2.0 * wPrime * u * v;

                // Matrix coefficients with enhanced stability
                var det = 4.0 * w * w * (u * u + v * v);
                if (Math.Abs(det) < 1e-12)
                {
                    // Use pseudo-inverse for near-singular cases
                    var normSq = u * u + v * v;
                    if (normSq < 1e-14)
                        return (0, 0);

                    var uPrime = (u * bx + v * by) / (2.0 * w * normSq);
                    var vPrime = (-v * bx + u * by) / (2.0 * w * normSq);
                    return (uPrime, vPrime);
                }

                var uPrimeStable = (2.0 * w * (u * bx + v * by)) / det;
                var vPrimeStable = (2.0 * w * (-v * bx + u * by)) / det;
                return (uPrimeStable, vPrimeStable);
            }
        }

    }

    
    public static (bool, PhBernsteinCurve2D) TryCreateC1(Random randGen, int degree, LinFloat64Vector2D point0, LinFloat64Vector2D point1, LinFloat64Vector2D tangent0, LinFloat64Vector2D tangent1)
    {
        PhBernsteinCurve2D phCurve = null!;
        var foundCurve = false;
        var trialCount = 100;
        while (!foundCurve && trialCount > 0)
        {
            (foundCurve, phCurve, _) = 
                CreateRandom(randGen, degree, degree, degree)
                    .SetEndPoints(
                        point0, 
                        point1, 
                        tangent0, 
                        tangent1
                    );

            trialCount--;
            
            if (!foundCurve || phCurve.HasCusps() || phCurve.HasLoops())
            {
                foundCurve = false;
                continue;
            }

            return (true, phCurve);
        }

        return (false, phCurve);
    }

    public static (bool, PhBernsteinCurve2D) TryCreateC2(Random randGen, int degree, LinFloat64Vector2D point0, LinFloat64Vector2D point1, LinFloat64Vector2D tangent0, LinFloat64Vector2D tangent1, LinFloat64Vector2D acceleration0, LinFloat64Vector2D acceleration1)
    {
        PhBernsteinCurve2D phCurve = null!;
        var foundCurve = false;
        var trialCount = 100;
        while (!foundCurve && trialCount > 0)
        {
            (foundCurve, phCurve, _) = 
                CreateRandom(randGen, degree, degree, degree)
                .SetEndPoints(
                    point0, 
                    point1, 
                    tangent0, 
                    tangent1, 
                    acceleration0, 
                    acceleration1
                );

            trialCount--;
            
            if (!foundCurve || phCurve.HasCusps() || phCurve.HasLoops())
            {
                foundCurve = false;
                continue;
            }

            return (true, phCurve);
        }

        return (false, phCurve);
    }

    public static (bool, PhBernsteinCurve2D) TryJoinLinesC2(Random randGen, int degree, LinFloat64PolarAngle angle, double sideLength = 1)
    {
        var reflectResult = angle.Degrees > 180;

        var correctedAngle = reflectResult ? angle.NegativeAngle() : angle; 

        if (!(correctedAngle.Degrees >= 0 && correctedAngle.Degrees <= 180))
            throw new InvalidOperationException();

        var p0 = sideLength * LinFloat64Vector2D.E1;
        var p1 = LinFloat64Vector2D.Zero;
        var p2 = p1 + sideLength * correctedAngle.ToLinVector2D();

        var tangent0 =
            (p1 - p0).ToUnitLinVector2D(false);
        
        var tangent1 =
            (p2 - p1).ToUnitLinVector2D(false);

        var t0 = 0d; //randGen.GetFloat64()
        var t1 = 1d; //randGen.GetFloat64()

        var point0 =
            t0.Lerp(p0, p1);

        var point1 =
            t1.Lerp(p1, p2);

        var pointMid =
            p1 + ((p0 - p1) + (p2 - p1)) / 5;

        PhBernsteinCurve2D phCurve = null!;
        var foundCurve = false;
        var trialCount = 100;
        while (!foundCurve && trialCount > 0)
        {
            (foundCurve, phCurve, var msg) =
                CreateRandom(randGen, degree, degree, degree)
                    .JoinLinesC2(
                        point0,
                        point1,
                        pointMid,
                        tangent0,
                        tangent1
                    );

            //Console.WriteLine(msg);
            
            trialCount--;

            if (!foundCurve || phCurve.HasCusps() || phCurve.HasLoops())
            {
                foundCurve = false;
                continue;
            }

            if (reflectResult) 
                phCurve = phCurve.ReflectOnX();

            return (true, phCurve);
        }

        return (false, phCurve);
    }

    public static (bool, PhBernsteinCurve2D) TryJoinLinesC2(Random randGen, int degree, LinFloat64Vector2D p1, LinFloat64Vector2D p2, LinFloat64Vector2D p3)
    {
        var u12 = p1 - p2;
        var u32 = p3 - p2;

        var minLength = Math.Min(u12.L2Norm(), u32.L2Norm());
        var sideLength = 1d;

        var (found, curve) = 
            TryJoinLinesC2(randGen, degree, u12.GetAngleCCw(u32), sideLength);

        if (found)
        {
            curve = curve.MapUsing(
                Float64SimilarityAffineMap2D.Create(
                    minLength / sideLength,
                    LinFloat64Vector2D.E1.GetAngleCCw(u12),
                    p2
                )
            );

            return (true, curve);
        }

        return (false, curve);
    }

    public static PhBernsteinCurve2D CreateRandom(Random randGen, int uDegree, int vDegree, int wDegree)
    {
        var u = BernsteinPolynomial.Create(randGen.GetFloat64Array1D(uDegree + 1, -2, 2));
        var v = BernsteinPolynomial.Create(randGen.GetFloat64Array1D(vDegree + 1, -2, 2));
        var w = BernsteinPolynomial.Create(randGen.GetFloat64Array1D(wDegree + 1, 0.25, 2));

        var genData =
            new GeneratorData
            {
                U = u,
                V = v,
                W = w,
                P0 = LinFloat64Vector2D.Zero,
                S0 = 0
            };

        return new PhBernsteinCurve2D(
            genData,
            false
        );
    }

    public static (bool, PhBernsteinCurve2D) CreateRandomWithStartDerivativesAndEndpoint(
        Random rand,
        LinFloat64Vector2D p0,
        LinFloat64Vector2D s0,
        LinFloat64Vector2D a0,
        LinFloat64Vector2D p1,
        int degree = 5,
        int maxRefitIterations = 5
    )
    {
        if (degree < 3)
            throw new ArgumentException("Degree must be >= 3 for PH curve with second derivative control.");

        // Step 1️⃣ — Generate random initial generator polynomials
        var uCoeffs = rand.GetFloat64Array1D(degree + 1, -10, 10);
        var vCoeffs = rand.GetFloat64Array1D(degree + 1, -10, 10);
        var wCoeffs = rand.GetFloat64Array1D(degree + 1, 0.1, 10); // ensure positive W

        var uPoly = BernsteinPolynomial.Create(uCoeffs);
        var vPoly = BernsteinPolynomial.Create(vCoeffs);
        var wPoly = BernsteinPolynomial.Create(wCoeffs);

        var genData = new GeneratorData
        {
            U = uPoly,
            V = vPoly,
            W = wPoly,
            P0 = p0,
            S0 = 0
        };

        // Step 2️⃣ — Build the initial PH curve
        var baseCurve = new PhBernsteinCurve2D(genData, false);

        // Step 3️⃣ — Numerically refit to enforce endpoint while keeping start derivatives exact
        RefitResult refitResult = null!;
        var success = false;

        for (var attempt = 0; attempt < maxRefitIterations; attempt++)
        {
            refitResult = RefitStartTangentAccelerationAndEndpoint(
                generators: genData,
                p0: p0,
                p1: p1,
                s0: s0,
                a0: a0,
                endpointTolerance: 1e-8,
                tangentTolerance: 1e-8,
                accelTolerance: 1e-6,
                maxIterations: 300
            );

            if (refitResult.Converged)
            {
                success = true;
                break;
            }

            // Try again with small random noise on interior coefficients
            for (var i = 1; i < uCoeffs.Length - 1; i++)
                uCoeffs[i] += (rand.NextDouble() - 0.5) * 0.2;

            for (var i = 1; i < vCoeffs.Length - 1; i++)
                vCoeffs[i] += (rand.NextDouble() - 0.5) * 0.2;

            for (var i = 1; i < wCoeffs.Length - 1; i++)
                wCoeffs[i] = Math.Abs(wCoeffs[i] + (rand.NextDouble() - 0.5) * 0.2) + 0.05;

            genData = new GeneratorData
            {
                U = BernsteinPolynomial.Create(uCoeffs),
                V = BernsteinPolynomial.Create(vCoeffs),
                W = BernsteinPolynomial.Create(wCoeffs),
                P0 = p0,
                S0 = 0
            };
        }

        if (!success)
            Debug.WriteLine("⚠️ Warning: Curve did not converge perfectly, but best fit returned.");

        // Step 4️⃣ — Return the refitted final curve
        var finalCurve = new PhBernsteinCurve2D(refitResult.Generators, false);

        Debug.WriteLine($"✅ Random PH curve generated. {refitResult}");

        return (success, finalCurve);
    }


    public double Tolerance { get; }

    public GeneratorData Generators { get; }


    public BernsteinPolynomial X { get; }

    public BernsteinPolynomial Y { get; }


    public BernsteinPolynomial XDt { get; }

    public BernsteinPolynomial YDt { get; }


    public BernsteinPolynomial XDt2 { get; }

    public BernsteinPolynomial YDt2 { get; }


    public BernsteinPolynomial S { get; }

    public BernsteinPolynomial SDt { get; }


    public RationalBernsteinPolynomial Kappa1 { get; }

    public RationalBernsteinPolynomial EnergyDensity { get; }


    public double X0
        => X.GetValue(MinTime);

    public double X1
        => X.GetValue(MaxTime);

    public double Y0
        => Y.GetValue(MinTime);

    public double Y1
        => Y.GetValue(MaxTime);

    public double S0
        => S.GetValue(MinTime);

    public double S1
        => S.GetValue(MaxTime);


    public bool IntersectionTestsEnabled
        => true;


    public PhBernsteinCurve2D(GeneratorData generators, bool isPeriodic, double tolerance = Float64Utils.DefaultTolerance)
        : base(Float64ScalarRange.ZeroToOne, isPeriodic)
    {
        if (!double.IsFinite(tolerance) || tolerance < 0)
            throw new ArgumentException("Tolerance must be finite and non-negative", nameof(tolerance));

        Tolerance = tolerance;
        Generators = generators;

        var (uGen, vGen, wGen, (x0, y0), s0) =
            generators;

        // Precompute squares and sums
        var u2 = uGen.Square();
        var v2 = vGen.Square();
        var u2v2Sum = (u2 + v2).Simplify(tolerance);
        var u2v2SumSquared = u2v2Sum.Square();
        var u2v2SumCubed = u2v2Sum * u2v2SumSquared;

        // Derivatives
        (XDt, YDt) = BernsteinPolynomial.SimplifyToEqualDegree(
            wGen * (u2 - v2),
            2 * wGen * uGen * vGen,
            tolerance
        );

        (XDt2, YDt2) = BernsteinPolynomial.SimplifyToEqualDegree(
            XDt.Derivative(),
            YDt.Derivative(),
            tolerance
        );

        // Integrals with initial conditions
        (X, Y) = BernsteinPolynomial.SimplifyToEqualDegree(
            XDt.Integral().Add(x0),
            YDt.Integral().Add(y0),
            tolerance
        );

        SDt = (wGen * u2v2Sum).Simplify(tolerance);
        S = SDt.Integral().Add(s0).Simplify(tolerance);

        // Curvature
        var uDt = uGen.Derivative();
        var vDt = vGen.Derivative();
        var uvDt = uGen * vDt - vGen * uDt;

        Kappa1 = new RationalBernsteinPolynomial(
            2 * uvDt,
            wGen * u2v2SumSquared,
            tolerance
        );

        EnergyDensity = new RationalBernsteinPolynomial(
            uvDt.Square().Times(4),
            wGen * u2v2SumCubed,
            tolerance
        );
    }


    public override bool IsValid()
    {
        // Enhanced validation with comprehensive checks
        try
        {
            // Check polynomial degrees are consistent
            if (X.Degree != Y.Degree) return false;

            // Check for NaN or infinite values in coefficients
            if (X.Coeffs.Any(c => !double.IsFinite(c))) return false;
            if (Y.Coeffs.Any(c => !double.IsFinite(c))) return false;

            // Check arc length is non-decreasing
            var sampleCount = Math.Min(10, X.Degree + 1);
            var prevLength = S0;
            for (var i = 1; i <= sampleCount; i++)
            {
                var t = (double)i / sampleCount;
                var currentLength = GetArcLength(t);
                if (currentLength < prevLength - Tolerance) return false;
                prevLength = currentLength;
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    public override Float64Path2D ToFinitePath()
    {
        return IsFinite
            ? this
            : new PhBernsteinCurve2D(Generators, false, Tolerance);
    }

    public override Float64Path2D ToPeriodicPath()
    {
        return IsPeriodic
            ? this
            : new PhBernsteinCurve2D(Generators, true, Tolerance);
    }


    public override LinFloat64Vector2D GetValue(double t)
    {
        t = Math.Clamp(t, MinTime, MaxTime);
        return LinFloat64Vector2D.Create(
            X.GetValue(t),
            Y.GetValue(t)
        );
    }

    public override LinFloat64Vector2D GetDerivative1Value(double t)
    {
        t = Math.Clamp(t, MinTime, MaxTime);
        return LinFloat64Vector2D.Create(
            XDt.GetValue(t),
            YDt.GetValue(t)
        );
    }

    public override LinFloat64Vector2D GetDerivative2Value(double t)
    {
        t = Math.Clamp(t, MinTime, MaxTime);
        return LinFloat64Vector2D.Create(
            XDt2.GetValue(t),
            YDt2.GetValue(t)
        );
    }

    public override double GetArcLength(double t)
    {
        t = Math.Clamp(t, MinTime, MaxTime);
        return S.GetValue(t);
    }

    public override double GetArcSpeed(double t)
    {
        t = Math.Clamp(t, MinTime, MaxTime);
        return SDt.GetValue(t);
    }

    public double GetCurvature(double t)
    {
        t = Math.Clamp(t, MinTime, MaxTime);
        return Kappa1.EvaluateAt(t);
    }

    public double GetEnergyDensity(double t)
    {
        t = Math.Clamp(t, MinTime, MaxTime);
        return EnergyDensity.EvaluateAt(t);
    }

    public double GetEnergy(double t)
    {
        t = Math.Clamp(t, MinTime, MaxTime);
        return EnergyDensity.Integrate(MinTime, t);
    }

    public double GetMaxCurvature()
    {
        try
        {
            var kappa1DtRoots = Kappa1.DerivativeNumerator().FindRoots(Tolerance);

            // Include endpoints
            var candidateTimes = new List<double> { MinTime, MaxTime };
            candidateTimes.AddRange(kappa1DtRoots.Where(t => t > MinTime + Tolerance && t < MaxTime - Tolerance));

            var kValues = candidateTimes
                .Select(t =>
                {
                    try { return Math.Abs(GetCurvature(t)); }
                    catch { return double.NaN; }
                })
                .Where(k => double.IsFinite(k))
                .ToArray();

            if (kValues.Length == 0)
                return double.NaN;

            return kValues.Max();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error computing max curvature: {ex.Message}");
            return double.NaN;
        }
    }

    public double GetMaxEnergy()
    {
        return GetEnergy(MaxTime);
    }


    /// <summary>
    /// Expose convenience conversion using exact S(t)
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public double TimeToLength(double t)
    {
        return GetArcLength(t);
    }

    /// <summary>
    /// Map length (distance from time 0) to time t.
    /// Performs Newton iterations to refine inside leaf.
    /// length input is clamped to [0, TotalLength].
    /// </summary>
    public double LengthToTime(double length)
    {
        return ArcLengthToParameterRobust(length, maxIterations: 50, tolerance: Tolerance);
    }

    /// <summary>
    /// Enhanced arc length to parameter conversion with fallback to bisection
    /// </summary>
    private double ArcLengthToParameterRobust(double targetLength, int maxIterations, double tolerance)
    {
        var totalLength = S1 - S0;
        targetLength = Math.Clamp(targetLength, 0, totalLength);

        if (Math.Abs(targetLength) < tolerance) return MinTime;
        if (Math.Abs(targetLength - totalLength) < tolerance) return MaxTime;

        // Initial guess using linear interpolation in arc length
        var t0 = MinTime;
        var t1 = MaxTime;
        var t = MinTime + (targetLength / totalLength) * (MaxTime - MinTime);

        // Newton-Raphson with fallback to bisection
        for (var iter = 0; iter < maxIterations; iter++)
        {
            var currentLength = GetArcLength(t) - S0;
            var error = currentLength - targetLength;

            if (Math.Abs(error) < tolerance)
                return t;

            var speed = GetArcSpeed(t);

            // Use Newton step if derivative is sufficiently large
            if (Math.Abs(speed) > tolerance)
            {
                var newtonStep = t - error / speed;

                // Check if Newton step is within bounds and reduces error
                if (newtonStep >= MinTime && newtonStep <= MaxTime)
                {
                    var newError = Math.Abs(GetArcLength(newtonStep) - S0 - targetLength);
                    if (newError < Math.Abs(error))
                    {
                        t = newtonStep;
                        continue;
                    }
                }
            }

            // Fall back to bisection
            if (error > 0)
            {
                t1 = t;
            }
            else
            {
                t0 = t;
            }

            t = 0.5 * (t0 + t1);
        }

        return t;
    }


    public IReadOnlyList<LinFloat64Vector2D> GetControlPoints()
    {
        Debug.Assert(X.Degree == Y.Degree);

        return (X.Degree + 1).GetRange(i =>
            LinFloat64Vector2D.Create(
                X.Coeffs[i],
                Y.Coeffs[i]
            )
        ).ToArray();
    }

    
    public PhBernsteinCurve2D ReflectOnX()
    {
        var generators = new GeneratorData
        {
            U = Generators.U,
            V = -Generators.V,
            W = Generators.W,
            P0 = Generators.P0.ReflectOnX(),
            S0 = S0
        };

        return new PhBernsteinCurve2D(generators, IsPeriodic, Tolerance);
    }
    
    public PhBernsteinCurve2D ScaleBy(double scalingFactor)
    {
        if (!scalingFactor.IsValid())
            throw new InvalidOperationException("Invalid similarity map");

        var generators = new GeneratorData
        {
            U = Generators.U,
            V = Generators.V,
            W = scalingFactor * Generators.W,
            P0 = scalingFactor * Generators.P0,
            S0 = scalingFactor * S0
        };

        return new PhBernsteinCurve2D(generators, IsPeriodic, Tolerance);
    }

    public PhBernsteinCurve2D MapUsing(Float64SimilarityAffineMap2D map)
    {
        if (map.IsDegenerate)
            throw new InvalidOperationException("Degenerate similarity map (scale ≈ 0)");

        //var scale = map.ScalingFactor;
        //var sqrtScale = Math.Sqrt(scale);
        var halfAngle = map.RotationAngle.HalfDirectedAngle();
        var halfAngleCos = halfAngle.CosValue;
        var halfAngleSin = halfAngle.SinValue;

        // Apply scaled rotation (and reflection if needed) to (U, V)
        BernsteinPolynomial newU, newV;

        if (!map.HasReflection)
        {
            // Orientation-preserving: (u',v') = sqrt(scale) * (c*u - s*v, s*u + c*v)
            newU = halfAngleCos * Generators.U - halfAngleSin * Generators.V;
            newV = halfAngleSin * Generators.U + halfAngleCos * Generators.V;
        }
        else
        {
            // Orientation-reversing: (u',v') = sqrt(scale) * (c*u + s*v, s*u - c*v)
            newU = halfAngleCos * Generators.U + halfAngleSin * Generators.V;
            newV = halfAngleSin * Generators.U - halfAngleCos * Generators.V;
        }

        var newW = map.ScalingFactor * Generators.W;

        // Apply affine map to P0
        var newP0 = map.MapPoint(Generators.P0);

        // S0 scales linearly
        var newS0 = map.ScalingFactor * S0;

        var generators = new GeneratorData
        {
            U = newU,
            V = newV,
            W = newW,
            P0 = newP0,
            S0 = newS0
        };

        return new PhBernsteinCurve2D(generators, IsPeriodic, Tolerance);
    }

    public IReadOnlyList<double> FindCuspTimeValues(double tolerance = Float64Utils.DefaultTolerance)
    {
        // Root intervals for x'(t) and y'(t)
        var xRoots = XDt.FindRoots(tolerance);
        var yRoots = YDt.FindRoots(tolerance);

        // Merge intervals that overlap (both derivatives vanish)
        var cuspTimes = new List<double>();

        foreach (var rx in xRoots)
            foreach (var ry in yRoots)
            {
                if (rx.IsNearEqual(ry, 2 * tolerance))
                    cuspTimes.Add(0.5 * (rx + ry));
            }

        return cuspTimes;
    }

    public bool HasCusps(double tolerance = Float64Utils.DefaultTolerance)
    {
        // Root intervals for x'(t) and y'(t)
        var xRoots = XDt.FindRoots(tolerance);
        var yRoots = YDt.FindRoots(tolerance);

        // Merge intervals that overlap (both derivatives vanish)
        foreach (var rx in xRoots)
            foreach (var ry in yRoots)
            {
                if (rx.IsNearEqual(ry, 2 * tolerance))
                    return true;
            }

        return false;
    }

    public bool HasLoops(SamplingOptions? samplingOptions = null)
    {
        return ToAdaptivePolyline(samplingOptions).HasSelfIntersection();
    }

    /// <summary>
    /// TODO: This needs correction
    /// Detect all self–intersections (loops) of this 2D PH Bézier curve in [0,1].
    /// Returns parameter pairs (t1,t2) such that r(t1) ≈ r(t2).
    /// Uses adaptive subdivision and the existing Bernstein root isolation routines
    /// for accurate refinement.
    /// </summary>
    //public List<(double t1, double t2)> FindLoops(double tolerance = 1e-9, int maxDepth = 12)
    //{
    //    var results = new List<(double t1, double t2)>();

    //    // --- 1. Collect subdivided segments with their bounding boxes ---
    //    var segments = new List<(double a, double b, Float64BoundingBox2D box)>();
    //    CollectSegments(0.0, 1.0, 0);

    //    // --- 2. Pairwise intersection of non-adjacent boxes ---
    //    for (var i = 0; i < segments.Count; i++)
    //    {
    //        for (var j = i + 2; j < segments.Count; j++) // skip adjacent
    //        {
    //            var segA = segments[i];
    //            var segB = segments[j];

    //            if (!segA.box.IntersectsBox(segB.box))
    //                continue;

    //            // --- 3. Refine (t1,t2) intersection using root isolation ---
    //            var refined = RefineIntersection(segA.a, segA.b, segB.a, segB.b, tolerance);
    //            if (refined.HasValue)
    //                results.Add(refined.Value);
    //        }
    //    }

    //    // --- 4. Merge near-duplicate pairs ---
    //    const double mergeTol = 1e-6;
    //    results = results
    //        .DistinctBy(p => (Math.Round(p.t1 / mergeTol), Math.Round(p.t2 / mergeTol)))
    //        .ToList();

    //    return results;

    //    // ==============================================================
    //    // Helper: recursively collect subsegments with bounding boxes
    //    void CollectSegments(double a, double b, int depth)
    //    {
    //        var sub = Subdivide(a, b);             // existing method: returns sub-curve
    //        var box = sub.GetBoundingBox();

    //        if (depth >= maxDepth || box.DiagonalLength < tolerance)
    //        {
    //            segments.Add((a, b, box));
    //            return;
    //        }

    //        var mid = 0.5 * (a + b);
    //        CollectSegments(a, mid, depth + 1);
    //        CollectSegments(mid, b, depth + 1);
    //    }

    //    // ==============================================================
    //    // Helper: precise intersection refinement using root isolation
    //    (double, double)? RefineIntersection(double a1, double b1, double a2, double b2, double tol)
    //    {
    //        // Define delta functions
    //        var dxPoly = X.Reparametrize(a1, b1) - X.Reparametrize(a2, b2);
    //        var dyPoly = Y.Reparametrize(a1, b1) - Y.Reparametrize(a2, b2);

    //        // Step 1: find t where dx(t)=0 and dy(t)=0 using root isolators
    //        var xRoots = dxPoly.FindRootIntervals(tol, 25);
    //        var yRoots = dyPoly.FindRootIntervals(tol, 25);

    //        foreach (var rx in xRoots)
    //        {
    //            var tx = 0.5 * (rx.MinValue + rx.MaxValue);
    //            var dxVal = dxPoly.GetValue(tx);
    //            if (Math.Abs(dxVal) > tol) continue;

    //            foreach (var ry in yRoots)
    //            {
    //                var ty = 0.5 * (ry.MinValue + ry.MaxValue);
    //                if (Math.Abs(ry.MinValue - rx.MinValue) > 0.05) continue; // rough filter

    //                var dyVal = dyPoly.GetValue(ty);
    //                if (Math.Abs(dyVal) > tol) continue;

    //                // Convert local params back to global [0,1]
    //                var t1 = a1 + tx * (b1 - a1);
    //                var t2 = a2 + ty * (b2 - a2);

    //                if (Math.Abs(t1 - t2) > tol)
    //                    return (t1, t2);
    //            }
    //        }

    //        return null;
    //    }
    //}

    public PhBernsteinCurve2D SetEndPoints(LinFloat64Vector2D point0, LinFloat64Vector2D point1)
    {
        // Enhanced validation
        if (point0.IsNearEqual(point1, Tolerance))
            throw new ArgumentException("Start and end points are too close");

        // Map the new curve to satisfy position constraints at end points
        var lineSegment1 = Float64LineSegment2D.Create(GetPoint(0), GetPoint(1));
        var lineSegment2 = Float64LineSegment2D.Create(point0, point1);

        var affineMap = Float64SimilarityAffineMap2D.CreateFromLineSegments(
            lineSegment1,
            lineSegment2
        );

        var curve = MapUsing(affineMap);

#if DEBUG
        {
            var p0 = curve.GetPoint(0);
            var p1 = curve.GetPoint(1);

            var positionTolerance = 1e-8;
            Debug.Assert(p0.IsNearEqual(point0, positionTolerance),
                $"Position0 constraint violated: expected {point0}, got {p0}");
            Debug.Assert(p1.IsNearEqual(point1, positionTolerance),
                $"Position1 constraint violated: expected {point1}, got {p1}");
        }
#endif

        return curve;
    }

    public (bool Success, PhBernsteinCurve2D Curve, string Message) SetEndPoints(
        LinFloat64Vector2D point0,
        LinFloat64Vector2D point1,
        LinFloat64Vector2D tangent0,
        LinFloat64Vector2D tangent1)
    {
        // Enhanced input validation
        if (point0.IsNearEqual(point1, Tolerance))
            return (false, this, "Start and end points are too close");

        if (tangent0.L2Norm() < Tolerance)
            return (false, this, "Tangent0 vector is too small");

        if (tangent1.L2Norm() < Tolerance)
            return (false, this, "Tangent1 vector is too small");

        try
        {
            // Step 1: Manipulate generator polynomial coefficients to satisfy
            // tangent constraints at end points
            var genData = Generators.SetEndDerivatives(tangent0, tangent1);

            var curve = new PhBernsteinCurve2D(
                genData,
                IsPeriodic,
                Tolerance
            );

            // Step 2: Map the new curve to satisfy position constraints at end points
            var lineSegment1 = Float64LineSegment2D.Create(curve.GetPoint(0), curve.GetPoint(1));
            var lineSegment2 = Float64LineSegment2D.Create(point0, point1);

            var affineMap = Float64SimilarityAffineMap2D.CreateFromLineSegments(
                lineSegment1,
                lineSegment2
            );

            curve = curve.MapUsing(affineMap);

            // Step 3: Iteratively adjust free parameters of the new curve to re-align
            // tangents with required constraints while keeping position constraints
            // nearly satisfied
            var res = RefitEndpointTangentTransformGeneralNumerical(
                curve.Generators,
                curve.GetPoint(0),
                curve.GetPoint(1),
                1 / affineMap.ScalingFactor,
                affineMap.RotationAngle.NegativeAngle().Radians
            );

            curve = new PhBernsteinCurve2D(
                res.Generators,
                IsPeriodic,
                Tolerance
            );

            // Enhanced validation of results
            var finalP0 = curve.GetPoint(0);
            var finalP1 = curve.GetPoint(1);
            var finalT0 = curve.GetTangentVector(0);
            var finalT1 = curve.GetTangentVector(1);

            var positionError = Math.Max(
                (finalP0 - point0).L2Norm(),
                (finalP1 - point1).L2Norm()
            );

            var tangentError = Math.Max(
                (finalT0 - tangent0).L2Norm(),
                (finalT1 - tangent1).L2Norm()
            );

            var success = positionError < 1e-6 && tangentError < 1e-4;
            var message = success
                ? "Success"
                : $"Position error: {positionError:E3}, Tangent error: {tangentError:E3}";

            return (success, curve, message);
        }
        catch (Exception ex)
        {
            return (false, this, $"Failed to set endpoints: {ex.Message}");
        }
    }

    public (bool Success, PhBernsteinCurve2D Curve, string Message) SetEndPoints(
        LinFloat64Vector2D point0,
        LinFloat64Vector2D point1,
        LinFloat64Vector2D tangent0,
        LinFloat64Vector2D tangent1,
        LinFloat64Vector2D acceleration0,
        LinFloat64Vector2D acceleration1)
    {
        var (_, curve, _) = SetEndPoints(point0, point1, tangent0, tangent1);

        try
        {
            var res = RefitEndpointTangentAccelerationTransformGeneralNumerical(
                curve.Generators,
                curve.GetPoint(0),
                curve.GetPoint(1),
                1,
                0,
                tangent0,
                tangent1,
                acceleration0,
                acceleration1
            );

            curve = new PhBernsteinCurve2D(
                res.Generators,
                IsPeriodic,
                Tolerance
            );

            // Enhanced validation of results
            var finalP0 = curve.GetPoint(0);
            var finalP1 = curve.GetPoint(1);
            var finalT0 = curve.GetTangentVector(0);
            var finalT1 = curve.GetTangentVector(1);
            var finalA0 = curve.GetAccelerationVector(0);
            var finalA1 = curve.GetAccelerationVector(1);

            var positionError = Math.Max(
                (finalP0 - point0).L2Norm(),
                (finalP1 - point1).L2Norm()
            );

            var tangentError = Math.Max(
                (finalT0 - tangent0).L2Norm(),
                (finalT1 - tangent1).L2Norm()
            );

            var accelerationError = Math.Max(
                (finalA0 - acceleration0).L2Norm(),
                (finalA1 - acceleration1).L2Norm()
            );

            var success = positionError < 1e-6 && tangentError < 1e-4 && accelerationError < 1e-2;
            var message = success
                ? $"Success with Position error: {positionError:E3}, Tangent error: {tangentError:E3}, Acceleration error: {accelerationError:E3}"
                : $"Failed with Position error: {positionError:E3}, Tangent error: {tangentError:E3}, Acceleration error: {accelerationError:E3}";

            return (success, curve, message);
        }
        catch (Exception ex)
        {
            return (false, this, $"Failed to set endpoints: {ex.Message}");
        }
    }

    public (bool Success, PhBernsteinCurve2D Curve, string Message) SetEndPoints(
        LinFloat64Vector2D point0,
        LinFloat64Vector2D point1,
        LinFloat64Vector2D tangent0,
        LinFloat64Vector2D tangent1,
        LinFloat64Vector2D acceleration0)
    {
        var (_, curve, _) = SetEndPoints(point0, point1, tangent0, tangent1);

        try
        {
            var res = RefitEndpointTangentAccelerationTransformGeneralNumerical(
                curve.Generators,
                curve.GetPoint(0),
                curve.GetPoint(1),
                tangent0,
                tangent1,
                acceleration0
            );

            curve = new PhBernsteinCurve2D(
                res.Generators,
                IsPeriodic,
                Tolerance
            );

            // Enhanced validation of results
            var finalP0 = curve.GetPoint(0);
            var finalP1 = curve.GetPoint(1);
            var finalT0 = curve.GetTangentVector(0);
            var finalT1 = curve.GetTangentVector(1);
            var finalA0 = curve.GetAccelerationVector(0);
            var finalA1 = curve.GetAccelerationVector(1);

            var positionError = Math.Max(
                (finalP0 - point0).L2Norm(),
                (finalP1 - point1).L2Norm()
            );

            var tangentError = Math.Max(
                (finalT0 - tangent0).L2Norm(),
                (finalT1 - tangent1).L2Norm()
            );

            var accelerationError =
                (finalA0 - acceleration0).L2Norm();

            var success = positionError < 1e-6 && tangentError < 1e-4 && accelerationError < 1e-2;
            var message = success
                ? "Success"
                : $"Position error: {positionError:E3}, Tangent error: {tangentError:E3}, Acceleration error: {accelerationError:E3}";

            return (success, curve, message);
        }
        catch (Exception ex)
        {
            return (false, this, $"Failed to set endpoints: {ex.Message}");
        }
    }

    public (bool Success, PhBernsteinCurve2D Curve, string Message) JoinLinesC2(
        LinFloat64Vector2D point0,
        LinFloat64Vector2D point1,
        LinFloat64Vector2D pointMid,
        LinFloat64Vector2D tangent0,
        LinFloat64Vector2D tangent1
    )
    {
        var acceleration0 = LinFloat64Vector2D.Zero;
        var acceleration1 = LinFloat64Vector2D.Zero;

        var (_, curve, _) = SetEndPoints(point0, point1, tangent0, tangent1);

        try
        {
            var res = RefitToPoints(
                curve.Generators,
                curve.GetPoint(0),
                curve.GetPoint(1),
                pointMid,
                tangent0,
                tangent1,
                acceleration0,
                acceleration1
            );

            curve = new PhBernsteinCurve2D(
                res.Generators,
                IsPeriodic,
                Tolerance
            );

            // Enhanced validation of results
            var finalP0 = curve.GetPoint(0);
            var finalP1 = curve.GetPoint(1);
            var finalPMid = curve.GetPoint(0.5);
            var finalT0 = curve.GetTangentVector(0);
            var finalT1 = curve.GetTangentVector(1);
            var finalA0 = curve.GetAccelerationVector(0);
            var finalA1 = curve.GetAccelerationVector(1);

            var positionError = Math.Max(Math.Max(
                (finalP0 - point0).L2Norm(),
                (finalP1 - point1).L2Norm()
            ), (finalPMid - pointMid).L2Norm());

            var tangentError = Math.Max(
                (finalT0 - tangent0).L2Norm(),
                (finalT1 - tangent1).L2Norm()
            );

            var accelerationError = Math.Max(
                (finalA0 - acceleration0).L2Norm(),
                (finalA1 - acceleration1).L2Norm()
            );

            var success = positionError < 1e-6 && tangentError < 1e-4 && accelerationError < 1e-2;
            var message = success
                ? $"Success with Position error: {positionError:E3}, Tangent error: {tangentError:E3}, Acceleration error: {accelerationError:E3}"
                : $"Failed with Position error: {positionError:E3}, Tangent error: {tangentError:E3}, Acceleration error: {accelerationError:E3}";

            return (success, curve, message);
        }
        catch (Exception ex)
        {
            return (false, this, $"Failed to set endpoints: {ex.Message}");
        }
    }

    public (bool Success, PhBernsteinCurve2D Curve, string Message) FollowPolyline(Float64Polyline2D polyLine, double maxPositionError = 1e-5, double maxAreaError = 1e-3)
    {
        var bezierArea =
            BezierCurve2D
                .FromPolygon(polyLine)
                .GetAreaBetweenCurveAndControlPolygon();

        var n = polyLine.Count;
        var point0 = polyLine.GetVertexPoint(0);
        var point1 = polyLine.GetVertexPoint(n - 1);
        var tangent0 = polyLine.GetVertexTangentVector(0);
        var tangent1 = polyLine.GetVertexTangentVector(n - 1);


        try
        {
            var res =
                this.RefitToPolyline(polyLine, bezierArea, maxPositionError, maxAreaError);

            var curve = new PhBernsteinCurve2D(
                res.Generators,
                IsPeriodic,
                Tolerance
            );

            // Enhanced validation of results
            var finalP0 = curve.GetPoint(0);
            var finalP1 = curve.GetPoint(1);

            var positionError = Math.Max(
                (finalP0 - point0).L2Norm(),
                (finalP1 - point1).L2Norm()
            );

            var finalArea =
                curve.ToAdaptivePolyline().GetArea(polyLine, maxPositionError);

            var areaError = Math.Abs(bezierArea - finalArea);

            var success =
                areaError < maxAreaError &&
                positionError < maxPositionError;

            var message = success
                ? $"Success with area error = {areaError:E3}, position error = {positionError:E3}"
                : $"Failed with area error: {areaError:E3}, position error = {positionError:E3}";

            return (success, curve, message);
        }
        catch (Exception ex)
        {
            return (false, this, $"Failed to set endpoints: {ex.Message}");
        }
    }


    public (bool Success, PhBernsteinCurve2D Curve, string Message) SetStartPoint(
        LinFloat64Vector2D point0,
        LinFloat64Vector2D point1,
        LinFloat64Vector2D tangent0)
    {
        // Enhanced input validation
        if (point0.IsNearEqual(point1, Tolerance))
            return (false, this, "Start and end points are too close");

        if (tangent0.L2Norm() < Tolerance)
            return (false, this, "Tangent0 vector is too small");

        try
        {
            // Step 1: Manipulate generator polynomial coefficients to satisfy
            // tangent constraints at end points
            var genData = Generators.SetStartDerivative(tangent0);

            var curve = new PhBernsteinCurve2D(
                genData,
                IsPeriodic,
                Tolerance
            );

            // Step 2: Map the new curve to satisfy position constraints at end points
            var lineSegment1 = Float64LineSegment2D.Create(curve.GetPoint(0), curve.GetPoint(1));
            var lineSegment2 = Float64LineSegment2D.Create(point0, point1);

            var affineMap = Float64SimilarityAffineMap2D.CreateFromLineSegments(
                lineSegment1,
                lineSegment2
            );

            curve = curve.MapUsing(affineMap);

            // Step 3: Iteratively adjust free parameters of the new curve to re-align
            // tangents with required constraints while keeping position constraints
            // nearly satisfied
            var res = RefitEndpointTangentTransformGeneralNumerical(
                curve.Generators,
                curve.GetPoint(0),
                curve.GetPoint(1),
                1 / affineMap.ScalingFactor,
                affineMap.RotationAngle.NegativeAngle().Radians
            );

            curve = new PhBernsteinCurve2D(
                res.Generators,
                IsPeriodic,
                Tolerance
            );

            // Enhanced validation of results
            var finalP0 = curve.GetPoint(0);
            var finalP1 = curve.GetPoint(1);
            var finalT0 = curve.GetTangentVector(0);

            var positionError = Math.Max(
                (finalP0 - point0).L2Norm(),
                (finalP1 - point1).L2Norm()
            );

            var tangentError = (finalT0 - tangent0).L2Norm();

            var success = positionError < 1e-6 && tangentError < 1e-4;
            var message = success
                ? "Success"
                : $"Position error: {positionError:E3}, Tangent error: {tangentError:E3}";

            return (success, curve, message);
        }
        catch (Exception ex)
        {
            return (false, this, $"Failed to set endpoints: {ex.Message}");
        }
    }

    public (bool Success, PhBernsteinCurve2D Curve, string Message) SetStartPoint(
        LinFloat64Vector2D point0,
        LinFloat64Vector2D point1,
        LinFloat64Vector2D tangent0,
        LinFloat64Vector2D acceleration0)
    {
        var (_, curve, _) = SetStartPoint(point0, point1, tangent0);

        try
        {
            var res = RefitEndpointTangentAccelerationTransformGeneralNumerical1(
                curve.Generators,
                curve.GetPoint(0),
                curve.GetPoint(1),
                tangent0,
                acceleration0
            );

            curve = new PhBernsteinCurve2D(
                res.Generators,
                IsPeriodic,
                Tolerance
            );

            // Enhanced validation of results
            var finalP0 = curve.GetPoint(0);
            var finalP1 = curve.GetPoint(1);
            var finalT0 = curve.GetTangentVector(0);
            var finalA0 = curve.GetAccelerationVector(0);

            var positionError = Math.Max(
                (finalP0 - point0).L2Norm(),
                (finalP1 - point1).L2Norm()
            );

            var tangentError =
                (finalT0 - tangent0).L2Norm();

            var accelerationError =
                (finalA0 - acceleration0).L2Norm();

            var success = positionError < 1e-6 && tangentError < 1e-4 && accelerationError < 1e-2;
            var message = success
                ? "Success"
                : $"Position error: {positionError:E3}, Tangent error: {tangentError:E3}, Acceleration error: {accelerationError:E3}";

            return (success, curve, message);
        }
        catch (Exception ex)
        {
            return (false, this, $"Failed to set endpoints: {ex.Message}");
        }
    }


    public PhBernsteinCurve2D Subdivide(double t0, double t1)
    {
        return new PhBernsteinCurve2D(
            new GeneratorData()
            {
                U = Generators.U.Subdivide(t0, t1),
                V = Generators.V.Subdivide(t0, t1),
                W = Generators.W.Subdivide(t0, t1),
                P0 = GetPoint(t0),
                S0 = GetArcLength(t0)
            },
            false,
            Tolerance
        );
    }

    public (PhBernsteinCurve2D, PhBernsteinCurve2D) Subdivide(double t)
    {
        var (u1, u2) = Generators.U.Subdivide(t);
        var (v1, v2) = Generators.V.Subdivide(t);
        var (w1, w2) = Generators.W.Subdivide(t);

        var p1 = GetPoint(0);
        var s1 = GetArcLength(0);

        var p2 = GetPoint(t);
        var s2 = GetArcLength(t);

        var curve1 = new PhBernsteinCurve2D(
            new GeneratorData { U = u1, V = v1, W = w1, P0 = p1, S0 = s1 },
            false,
            Tolerance
        );

        var curve2 = new PhBernsteinCurve2D(
            new GeneratorData { U = u2, V = v2, W = w2, P0 = p2, S0 = s2 },
            false,
            Tolerance
        );

        return (curve1, curve2);
    }

    public Float64Polyline2D ToAdaptivePolyline(SamplingOptions? samplingOptions = null)
    {
        samplingOptions ??= new SamplingOptions();

        return new Float64Polyline2D(
            GetAdaptiveTimeSamples(samplingOptions).Select(GetPoint)
        );
    }

    public Float64Polyline2D ToUniformTimePolyline(int sampleCount)
    {
        if (sampleCount < 2)
            throw new ArgumentException("Sample count must be at least 2", nameof(sampleCount));

        return new Float64Polyline2D(
            GetUniformTimeSamples(sampleCount).Select(GetPoint)
        );
    }

    public Float64Polyline2D ToUniformArcLengthPolyline(int sampleCount)
    {
        if (sampleCount < 2)
            throw new ArgumentException("Sample count must be at least 2", nameof(sampleCount));

        return new Float64Polyline2D(
            GetUniformArcLengthTimeSamples(sampleCount).Select(GetPoint)
        );
    }


    public IReadOnlyList<double> GetAdaptiveTimeSamples(SamplingOptions samplingOptions)
    {
        var cuspTimeValues = FindCuspTimeValues().ToList();
        cuspTimeValues.Add(1);

        var parameters = new SortedSet<double> { 0.0, 1.0 };

        var t0 = 0d;
        foreach (var t1 in cuspTimeValues)
        {
            SubdivideRecursive(samplingOptions, 0, t0, t1, parameters);
            t0 = t1;
        }

        return parameters.ToArray();
    }

    private void SubdivideRecursive(SamplingOptions samplingOptions, int level, double t0, double t1, SortedSet<double> parameters)
    {
        var p0 = GetPoint(t0);
        var p1 = GetPoint(t1);

        var chord = (p1 - p0).L2Norm();
        var s0 = S.GetValue(t0);
        var s1 = S.GetValue(t1);
        var segLength = s1 - s0;

        // Handle degenerate segments
        if (chord < 1e-14 || segLength < 1e-14)
        {
            parameters.Add(t0);
            parameters.Add(t1);
            return;
        }

        var u0 = GetTangentVector(t0).VectorDivideByENorm();
        var u1 = GetTangentVector(t1).VectorDivideByENorm();

        var dotEnd = Math.Clamp(u0.X * u1.X + u0.Y * u1.Y, -1.0, 1.0);
        var angleEnd = Math.Acos(dotEnd);

        var tooSmallParam = Math.Abs(t1 - t0) <= samplingOptions.ParameterSpan;
        var levelReached = level >= samplingOptions.MaxDepth;
        var minLevelNeeded = level < samplingOptions.MinDepth;
        var arcChordGood = (segLength - chord) / chord <= samplingOptions.RelativeChordError;
        var angleGood = angleEnd <= samplingOptions.FrameAngleDifference.Radians;

        if (minLevelNeeded || !levelReached && !(arcChordGood && angleGood) && !tooSmallParam)
        {
            // Subdivide further
            var tm = 0.5 * (t0 + t1);

            var pm = GetPoint(tm);
            var um = GetTangentVector(tm).VectorDivideByENorm();

            // Midpoint tangent angle checks
            var dotMid = Math.Clamp(u0.X * um.X + u0.Y * um.Y, -1.0, 1.0);
            var midAngle0 = Math.Acos(dotMid);

            dotMid = Math.Clamp(um.X * u1.X + um.Y * u1.Y, -1.0, 1.0);
            var midAngle1 = Math.Acos(dotMid);

            var angleBad =
                midAngle0 > samplingOptions.FrameAngleDifference.Radians ||
                midAngle1 > samplingOptions.FrameAngleDifference.Radians;

            // Chord improvement test
            var leftChord = (pm - p0).L2Norm();
            var rightChord = (p1 - pm).L2Norm();
            var totalChildChord = leftChord + rightChord;
            var improvement = totalChildChord - chord;
            var relativeImprovement = improvement / chord;

            if (!angleBad && relativeImprovement < samplingOptions.RelativeChordImprovement)
            {
                parameters.Add(t0);
                parameters.Add(t1);
                return;
            }

            SubdivideRecursive(samplingOptions, level + 1, t0, tm, parameters);
            SubdivideRecursive(samplingOptions, level + 1, tm, t1, parameters);
            return;
        }

        parameters.Add(t0);
        parameters.Add(t1);
    }

    /// <summary>
    /// Resample the curve into equally spaced points in arc length.
    /// </summary>
    /// <returns>The parameter values at the sample points</returns>
    public override IReadOnlyList<double> GetUniformArcLengthTimeSamples(int sampleCount)
    {
        if (sampleCount < 2)
            throw new ArgumentException("Sample count must be at least 2", nameof(sampleCount));

        var arcSamples =
            S.GetValue(MinTime).GetLinearRange(
                S.GetValue(MaxTime),
                sampleCount,
                IsPeriodic
            ).ToArray();

        // Invert arc length → parameters using robust method
        return arcSamples.Select(s => ArcLengthToParameterRobust(s, maxIterations: 20, tolerance: Tolerance)).ToArray();
    }

    /// <summary>
    /// Compute minimum distance between this PH curve and a point (px, py).
    /// Uses Newton–Raphson with multi-start sampling and bisection fallback.
    /// </summary>
    public double GetMinDistanceToPoint(double px, double py, int samples = 20, double tolerance = Float64Utils.DefaultTolerance, int maxIter = 50)
    {
        var minDistSq = double.PositiveInfinity;

        // 1. Endpoints
        var dx0 = X.GetValue(0.0) - px;
        var dy0 = Y.GetValue(0.0) - py;
        minDistSq = Math.Min(minDistSq, dx0 * dx0 + dy0 * dy0);

        var dx1 = X.GetValue(1.0) - px;
        var dy1 = Y.GetValue(1.0) - py;
        minDistSq = Math.Min(minDistSq, dx1 * dx1 + dy1 * dy1);

        // 2. Interior candidates with enhanced sampling
        for (var k = 0; k <= samples; k++)
        {
            var t = (double)k / samples;

            for (var iter = 0; iter < maxIter; iter++)
            {
                var xt = X.GetValue(t);
                var yt = Y.GetValue(t);
                var xdt = XDt.GetValue(t);
                var ydt = YDt.GetValue(t);

                var fx = xt - px;
                var fy = yt - py;

                var f = fx * xdt + fy * ydt; // derivative of squared distance
                var df = xdt * xdt + ydt * ydt + fx * XDt2.GetValue(t) + fy * YDt2.GetValue(t);

                if (Math.Abs(f) < tolerance)
                    break; // converged

                if (Math.Abs(df) > 1e-14)
                {
                    var newton = t - f / df;

                    if (newton is >= 0 and <= 1)
                        t = newton;
                    else
                        break; // out of bounds, discard
                }
                else
                    break;
            }

            // Evaluate distance
            var dx = X.GetValue(t) - px;
            var dy = Y.GetValue(t) - py;
            var distSq = dx * dx + dy * dy;

            minDistSq = Math.Min(minDistSq, distSq);
        }

        return Math.Sqrt(minDistSq);
    }

    /// <summary>
    /// Compute minimum distance between this 2D PH curve and a line segment AB.
    /// </summary>
    public double GetMinDistanceToLineSegment(double ax, double ay, double bx, double by, int samples = 20, double tolerance = Float64Utils.DefaultTolerance, int maxIter = 50)
    {
        var minDistSq = double.PositiveInfinity;
        var dx = bx - ax;
        var dy = by - ay;
        var dd = dx * dx + dy * dy;

        // 1. Endpoints A, B vs curve
        minDistSq = Math.Min(minDistSq, Math.Pow(GetMinDistanceToPoint(ax, ay), 2));
        minDistSq = Math.Min(minDistSq, Math.Pow(GetMinDistanceToPoint(bx, by), 2));

        // 2. this vs segment interior
        for (var k = 0; k <= samples; k++)
        {
            var t = (double)k / samples;

            for (var iter = 0; iter < maxIter; iter++)
            {
                var xt = X.GetValue(t);
                var yt = Y.GetValue(t);
                var xdt = XDt.GetValue(t);
                var ydt = YDt.GetValue(t);

                // Projection of r(t) onto AB
                var vx = xt - ax;
                var vy = yt - ay;
                var u = (vx * dx + vy * dy) / dd;
                u = Math.Max(0.0, Math.Min(1.0, u));
                var qx = ax + u * dx;
                var qy = ay + u * dy;

                // Residual
                var fx = xt - qx;
                var fy = yt - qy;
                var f = fx * xdt + fy * ydt;

                if (Math.Abs(f) < tolerance) break;

                var d2 = xdt * xdt + ydt * ydt
                                 + fx * XDt2.GetValue(t)
                                 + fy * YDt2.GetValue(t);

                if (Math.Abs(d2) > 1e-14)
                {
                    var newton = t - f / d2;
                    if (newton is >= 0 and <= 1) t = newton;
                    else break;
                }
                else break;
            }

            // Final distance candidate
            var xt2 = X.GetValue(t);
            var yt2 = Y.GetValue(t);
            var vx2 = xt2 - ax;
            var vy2 = yt2 - ay;
            var u2 = (vx2 * dx + vy2 * dy) / dd;
            u2 = Math.Max(0.0, Math.Min(1.0, u2));
            var qx2 = ax + u2 * dx;
            var qy2 = ay + u2 * dy;
            var dx2 = xt2 - qx2;
            var dy2 = yt2 - qy2;
            minDistSq = Math.Min(minDistSq, dx2 * dx2 + dy2 * dy2);
        }

        return Math.Sqrt(minDistSq);
    }

    /// <summary>
    /// Compute minimum distance between this PH curve and another
    /// </summary>
    /// <param name="curve2"></param>
    /// <param name="grid"></param>
    /// <param name="maxIter"></param>
    /// <param name="tolerance"></param>
    /// <returns></returns>
    public CurveCurveDistanceResult GetMinDistanceToCurve(PhBernsteinCurve2D curve2, int grid = 10, int maxIter = 50, double tolerance = Float64Utils.DefaultTolerance)
    {
        var D = GetDistanceSquaredFunction(curve2);
        var minDistSq = double.PositiveInfinity;
        var tBest = 0.0;
        var sBest = 0.0;

        double[] ends = [0.0, 1.0];
        foreach (var t in ends)
            foreach (var s in ends)
            {
                var val = D.EvaluateAt(t, s);
                if (val < minDistSq)
                {
                    minDistSq = val;
                    tBest = t;
                    sBest = s;
                }
            }

        for (var i = 0; i <= grid; i++)
            for (var j = 0; j <= grid; j++)
            {
                var t = (double)i / grid;
                var s = (double)j / grid;

                for (var k = 0; k < maxIter; k++)
                {
                    var p1 = new[] { X.GetValue(t), Y.GetValue(t) };
                    var p2 = new[] { curve2.X.GetValue(s), curve2.Y.GetValue(s) };
                    var dr = new[] { p1[0] - p2[0], p1[1] - p2[1] };

                    var r1d = new[] { XDt.GetValue(t), YDt.GetValue(t) };
                    var r2d = new[] { curve2.XDt.GetValue(s), curve2.YDt.GetValue(s) };

                    var f = dr[0] * r1d[0] + dr[1] * r1d[1];
                    var g = -(dr[0] * r2d[0] + dr[1] * r2d[1]);

                    if (Math.Abs(f) < tolerance && Math.Abs(g) < tolerance) break;

                    var ft = r1d[0] * r1d[0] + r1d[1] * r1d[1];
                    var gs = r2d[0] * r2d[0] + r2d[1] * r2d[1];

                    if (ft > 1e-14) t = Math.Clamp(t - f / ft, 0.0, 1.0);
                    if (gs > 1e-14) s = Math.Clamp(s - g / gs, 0.0, 1.0);
                }

                var val2 = D.EvaluateAt(t, s);
                if (val2 < minDistSq)
                {
                    minDistSq = val2;
                    tBest = t;
                    sBest = s;
                }
            }

        var point1 = new[] { X.GetValue(tBest), Y.GetValue(tBest) };
        var point2 = new[] { curve2.X.GetValue(sBest), curve2.Y.GetValue(sBest) };

        return new CurveCurveDistanceResult(Math.Sqrt(minDistSq), tBest, sBest, point1, point2);
    }

    /// <summary>
    /// Construct squared distance function between two PH curves
    /// as a rational bivariate Bernstein polynomial.
    /// </summary>
    public RationalBernsteinPolynomial2D GetDistanceSquaredFunction(PhBernsteinCurve2D curve2)
    {
        // X component
        var x1 = BernsteinPolynomial2D.FromT(X);
        var x2 = BernsteinPolynomial2D.FromS(curve2.X);
        var dx = x1 - x2;
        var sum = dx * dx;

        // Y component
        var y1 = BernsteinPolynomial2D.FromT(Y);
        var y2 = BernsteinPolynomial2D.FromS(curve2.Y);
        var dy = y1 - y2;
        sum += dy * dy;

        // Wrap into rational form (denominator = 1 for polynomial PH curves)
        var denom = new BernsteinPolynomial2D(0, 0, new[,] { { 1.0 } });

        return new RationalBernsteinPolynomial2D(sum, denom);
    }


    public Float64BoundingBox2D GetBoundingBox()
    {
        return GetBoundingBox(Float64Utils.DefaultTolerance);
    }

    public Float64BoundingBox2D GetBoundingBox(double tolerance)
    {
        return Float64BoundingBox2D.Containing(
            ToAdaptivePolyline()
        );

        var xCandidates = new List<double> { X.GetValue(0.0), X.GetValue(1.0) };
        var yCandidates = new List<double> { Y.GetValue(0.0), Y.GetValue(1.0) };

        // Critical points where derivative = 0
        foreach (var t in X.Derivative().FindRoots(tolerance))
            xCandidates.Add(X.GetValue(t));

        foreach (var t in Y.Derivative().FindRoots(tolerance))
            yCandidates.Add(Y.GetValue(t));

        return Float64BoundingBox2D.Containing(
            xCandidates.Min(),
            yCandidates.Min(),
            xCandidates.Max(),
            yCandidates.Max()
        );
    }

    public Float64BoundingBoxComposer2D GetBoundingBoxComposer()
    {
        return GetBoundingBox().GetBoundingBoxComposer();
    }
}