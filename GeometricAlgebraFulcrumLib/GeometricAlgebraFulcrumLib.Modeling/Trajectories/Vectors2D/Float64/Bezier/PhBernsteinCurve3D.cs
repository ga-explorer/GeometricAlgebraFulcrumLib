using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.Bernstein;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier
{
    public class PhBernsteinCurve3D :
        Float64Path3D,
        IPhBernsteinCurve
    {
        public sealed class GeneratorPolynomials
        {
            public required BernsteinPolynomial U { get; init; }

            public required BernsteinPolynomial V { get; init; }

            public required BernsteinPolynomial P { get; init; }
            
            public required BernsteinPolynomial Q { get; init; }

            public required BernsteinPolynomial W { get; init; }

            public required LinFloat64Vector3D P0 { get; init; }

            public required double S0 { get; init; }


            public void Deconstruct(out BernsteinPolynomial u, out BernsteinPolynomial v, out BernsteinPolynomial p, out BernsteinPolynomial q, out BernsteinPolynomial w, out LinFloat64Vector3D p0, out double s0)
            {
                u = U;
                v = V;
                p = P; 
                q = Q;
                w = W;
                p0 = P0;
                s0 = S0;
            }


            public GeneratorPolynomials PhMapUsing(Float64SimilarityAffineMap3D map)
            {
                if (map.IsDegenerate)
                    throw new InvalidOperationException("Degenerate similarity map (scale ~ 0)");

                // Extract scale
                var scale = map.ScalingFactor;
                var sqrtScale = Math.Sqrt(scale);

                // Extract rotation as quaternion
                var angle = map.RotationAngle.RadiansValue;
                var axis = map.RotationAxis;

                var halfAngle = 0.5 * angle;
                var sinHalf = Math.Sin(halfAngle);

                var qw = Math.Cos(halfAngle);
                var qx = axis.X * sinHalf;
                var qy = axis.Y * sinHalf;
                var qz = axis.Z * sinHalf;

                // Scale quaternion by sqrt(scale)
                qw *= sqrtScale;
                qx *= sqrtScale;
                qy *= sqrtScale;
                qz *= sqrtScale;

                // Apply quaternion multiplication rules to (u,v,p,q)
                BernsteinPolynomial newU, newV, newP, newQ;

                if (!map.HasReflection)
                {
                    // Orientation-preserving: q' = h q
                    newU = qw * U - qx * P - qy * Q - qz * V;
                    newV = qw * V + qx * Q - qy * P + qz * U;
                    newP = qw * P + qx * U + qy * V - qz * Q;
                    newQ = qw * Q - qx * V + qy * U + qz * P;
                }
                else
                {
                    // Orientation-reversing: q' = h * conj(q)
                    newU = qw * U + qx * P + qy * Q + qz * V;
                    newV = qw * V - qx * Q + qy * P - qz * U;
                    newP = qw * P - qx * U - qy * V + qz * Q;
                    newQ = qw * Q + qx * V - qy * U - qz * P;
                }

                var newW = W;

                // Transform initial point P0 using map
                var newP0 = map.MapPoint(P0);

                // S0 scales linearly
                var newS0 = scale * S0;

                return new GeneratorPolynomials
                {
                    U = newU,
                    V = newV,
                    P = newP,
                    Q = newQ,
                    W = newW,
                    P0 = newP0,
                    S0 = newS0
                };
            }

        }
        

        public double Tolerance { get; }

        public GeneratorPolynomials Generators { get; }
        
        
        public BernsteinPolynomial X { get; }
        
        public BernsteinPolynomial Y { get; }
        
        public BernsteinPolynomial Z { get; }


        public BernsteinPolynomial XDt { get; }
        
        public BernsteinPolynomial YDt { get; }
        
        public BernsteinPolynomial ZDt { get; }


        public BernsteinPolynomial XDt2 { get; }
        
        public BernsteinPolynomial YDt2 { get; }
        
        public BernsteinPolynomial ZDt2 { get; }


        public BernsteinPolynomial S { get; }
        
        public BernsteinPolynomial SDt { get; }
        
        
        public double X0 => X.GetValue(MinTime);

        public double Y0 => Y.GetValue(MinTime);
        
        public double Z0 => Z.GetValue(MinTime);

        public double S0 => S.GetValue(MinTime);


        public RationalBernsteinPolynomial Kappa1 { get; }

        public RationalBernsteinPolynomial Kappa2 { get; }

        public RationalBernsteinPolynomial EnergyDensity { get; }


        public PhBernsteinCurve3D(GeneratorPolynomials generators, bool isPeriodic, double tolerance = Float64Utils.DefaultTolerance) 
            : base(Float64ScalarRange.ZeroToOne, isPeriodic)
        {
            Debug.Assert(double.IsFinite(tolerance) && tolerance >= 0);

            Tolerance = tolerance;
            Generators = generators;

            var (uGen, vGen, pGen, qGen, wGen, (x0, y0, z0), s0) = generators;
            
            // Precompute squares
            var u2 = uGen.Square();
            var v2 = vGen.Square();
            var p2 = pGen.Square();
            var q2 = qGen.Square();

            var sumSquares = (u2 + v2 + p2 + q2).Simplify(tolerance);

            // Derivatives (hodograph components)
            (XDt, YDt, ZDt) = BernsteinPolynomial.SimplifyToEqualDegree(
                wGen * (u2 + v2 - p2 - q2),
                2 * wGen * (uGen * pGen + vGen * qGen),
                2 * wGen * (uGen * qGen - vGen * pGen),
                tolerance
            );
            //XDt = (wGen * (u2 + v2 - p2 - q2)).Simplify(tolerance);
            //YDt = (2 * wGen * (uGen * pGen + vGen * qGen)).Simplify(tolerance);
            //ZDt = (2 * wGen * (uGen * qGen - vGen * pGen)).Simplify(tolerance);
            
            (XDt2, YDt2, ZDt2) = BernsteinPolynomial.SimplifyToEqualDegree(
                XDt.Derivative(),
                YDt.Derivative(),
                ZDt.Derivative(),
                tolerance
            );
            //XDt2 = XDt.Derivative();
            //YDt2 = YDt.Derivative();
            //ZDt2 = ZDt.Derivative();

            // Integrals (anchored at x0,y0,z0,s0)
            (X, Y, Z) = BernsteinPolynomial.SimplifyToEqualDegree(
                XDt.Integral().Add(x0),
                YDt.Integral().Add(y0),
                ZDt.Integral().Add(z0),
                tolerance
            );
            //X = XDt.Integral().Add(x0).Simplify(tolerance);
            //Y = YDt.Integral().Add(y0).Simplify(tolerance);
            //Z = ZDt.Integral().Add(z0).Simplify(tolerance);

            SDt = (wGen * sumSquares).Simplify(tolerance);
            S = SDt.Integral().Add(s0).Simplify(tolerance);

            // Speed polynomial
            var speed = SDt;  // already computed as w * (u²+v²+p²+q²)
            var speed2 = speed.Square();
            var speed3 = speed2 * speed;
            var speed5 = speed2 * speed3;

            // Second derivatives
            var xDouble = XDt.Derivative();
            var yDouble = YDt.Derivative();
            var zDouble = ZDt.Derivative();

            // Cross product r' × r''
            var crossX = YDt * zDouble - ZDt * yDouble;
            var crossY = ZDt * xDouble - XDt * zDouble;
            var crossZ = XDt * yDouble - YDt * xDouble;

            var crossNormSq = (crossX.Square() + crossY.Square() + crossZ.Square()).Simplify(tolerance);

            // Curvature κ = ||r' × r''|| / (||r'||³) = ||r' × r''|| / (speed³)
            Kappa1 = new RationalBernsteinPolynomial(
                crossNormSq.Simplify(tolerance),
                speed3.Simplify(tolerance),
                tolerance
            );

            // Third derivatives
            var xTriple = xDouble.Derivative();
            var yTriple = yDouble.Derivative();
            var zTriple = zDouble.Derivative();

            // Determinant for torsion numerator
            var detNumerator =
                XDt * (yDouble * zTriple - zDouble * yTriple)
              - YDt * (xDouble * zTriple - zDouble * xTriple)
              + ZDt * (xDouble * yTriple - yDouble * xTriple);

            // Torsion τ = det(r', r'', r''') / ||r' × r''||²
            Kappa2 = new RationalBernsteinPolynomial(
                detNumerator,
                crossNormSq,
                tolerance
            );

            // Energy density: E(t) = κ² ||r'||
            // => numerator = ||r' × r''||², denominator = ||r'||⁵ = speed⁵
            EnergyDensity = new RationalBernsteinPolynomial(
                crossNormSq,
                speed5,
                tolerance
            );
        }

        
        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
        
        public override Float64Path3D ToFinitePath()
        {
            throw new NotImplementedException();
        }

        public override Float64Path3D ToPeriodicPath()
        {
            throw new NotImplementedException();
        }

        public override LinFloat64Vector3D GetValue(double t)
        {
            return LinFloat64Vector3D.Create(
                X.GetValue(t), 
                Y.GetValue(t), 
                Z.GetValue(t)
            );
        }

        public override LinFloat64Vector3D GetDerivative1Value(double t)
        {
            return LinFloat64Vector3D.Create(
                XDt.GetValue(t), 
                YDt.GetValue(t), 
                ZDt.GetValue(t)
            );
        }
    
        public override LinFloat64Vector3D GetDerivative2Value(double t)
        {
            return LinFloat64Vector3D.Create(
                XDt2.GetValue(t), 
                YDt2.GetValue(t), 
                ZDt2.GetValue(t)
            );
        }
        
        public override double GetArcLength(double t)
        {
            return S.GetValue(t);
        }
    
        public override double GetArcSpeed(double t)
        {
            return SDt.GetValue(t);
        }

        public double GetCurvature(double t)
        {
            return Kappa1.EvaluateAt(t);
        }
    
        public double GetEnergyDensity(double t)
        {
            return EnergyDensity.EvaluateAt(t);
        }
    
        public double GetEnergy(double t)
        {
            return EnergyDensity.Integrate(MinTime, t);
        }
        
        public IReadOnlyList<LinFloat64Vector3D> GetControlPoints()
        {
            Debug.Assert(X.Degree == Y.Degree && Y.Degree == Z.Degree);

            return (X.Degree + 1).GetRange(i => 
                LinFloat64Vector3D.Create(
                    X.Coeffs[i], 
                    Y.Coeffs[i], 
                    Z.Coeffs[i]
                )
            ).ToArray();
        }

        public Float64BoundingBox3D GetBoundingBox3D(double tol = Float64Utils.DefaultTolerance)
        {
            var xCandidates = new List<double> { X.GetValue(0.0), X.GetValue(1.0) };
            var yCandidates = new List<double> { Y.GetValue(0.0), Y.GetValue(1.0) };
            var zCandidates = new List<double> { Z.GetValue(0.0), Z.GetValue(1.0) };

            // Critical points where derivative = 0
            foreach (var t in X.Derivative().FindRoots(tol))
                xCandidates.Add(X.GetValue(t));
        
            foreach (var t in Y.Derivative().FindRoots(tol))
                yCandidates.Add(Y.GetValue(t));
            
            foreach (var t in Z.Derivative().FindRoots(tol))
                zCandidates.Add(Z.GetValue(t));

            return Float64BoundingBox3D.Containing(
                xCandidates.Min(), 
                yCandidates.Min(), 
                zCandidates.Min(), 
                xCandidates.Max(),
                yCandidates.Max(),
                zCandidates.Max()
            );
        }

    }
}
