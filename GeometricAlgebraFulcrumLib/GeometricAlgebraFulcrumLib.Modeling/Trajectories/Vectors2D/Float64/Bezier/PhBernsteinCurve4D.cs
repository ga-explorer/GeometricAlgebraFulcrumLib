using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors4D.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.Bernstein;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier
{
    public class PhBernsteinCurve4D :
        Float64Path4D,
        IPhBernsteinCurve
    {
        public sealed class GeneratorPolynomials
        {
            public required BernsteinPolynomial U { get; init; }

            public required BernsteinPolynomial V { get; init; }

            public required BernsteinPolynomial P { get; init; }
            
            public required BernsteinPolynomial Q { get; init; }
            
            public required BernsteinPolynomial R { get; init; }
            
            public required BernsteinPolynomial S { get; init; }

            public required BernsteinPolynomial W { get; init; }

            public required LinFloat64Vector4D P0 { get; init; }

            public required double S0 { get; init; }


            public void Deconstruct(out BernsteinPolynomial u, out BernsteinPolynomial v, out BernsteinPolynomial p, out BernsteinPolynomial q, out BernsteinPolynomial r, out BernsteinPolynomial s, out BernsteinPolynomial w, out LinFloat64Vector4D p0, out double s0)
            {
                u = U;
                v = V;
                p = P; 
                q = Q;
                r = R;
                s = S;
                w = W;
                p0 = P0;
                s0 = S0;
            }
        }
        

        public double Tolerance { get; }

        public GeneratorPolynomials Generators { get; }
        

        //public BernsteinPolynomial U { get; }
        //public BernsteinPolynomial V { get; }
        //public BernsteinPolynomial P { get; }
        //public BernsteinPolynomial Q { get; }
        //public BernsteinPolynomial R { get; }
        //public BernsteinPolynomial Sg { get; } // avoid clash with arc length S
        //public BernsteinPolynomial W { get; }
        
        public BernsteinPolynomial X { get; }
        public BernsteinPolynomial Y { get; }
        public BernsteinPolynomial Z { get; }
        public BernsteinPolynomial W { get; }

        public BernsteinPolynomial XDt { get; }
        public BernsteinPolynomial YDt { get; }
        public BernsteinPolynomial ZDt { get; }
        public BernsteinPolynomial WDt { get; }
        
        
        public BernsteinPolynomial XDt2 { get; }
        public BernsteinPolynomial YDt2 { get; }
        public BernsteinPolynomial ZDt2 { get; }
        public BernsteinPolynomial WDt2 { get; }

        public BernsteinPolynomial S { get; }

        public BernsteinPolynomial SDt { get; }
        
        
        public double X0 => X.GetValue(MinTime);

        public double Y0 => Y.GetValue(MinTime);
        
        public double Z0 => Z.GetValue(MinTime);
        
        public double W0 => W.GetValue(MinTime);

        public double S0 => S.GetValue(MinTime);


        // Curvatures
        public RationalBernsteinPolynomial Kappa1 { get; }

        public RationalBernsteinPolynomial Kappa2Squared { get; }

        public RationalBernsteinPolynomial Kappa3Squared { get; }

        public RationalBernsteinPolynomial EnergyDensity { get; }


        public PhBernsteinCurve4D(
            GeneratorPolynomials generators,
            bool isPeriodic,
            double tolerance = Float64Utils.DefaultTolerance
        ) : base(Float64ScalarRange.ZeroToOne, isPeriodic)
        {
            Debug.Assert(double.IsFinite(tolerance) && tolerance >= 0);

            Tolerance = tolerance;
            Generators = generators;

            var (uGen, vGen, pGen, qGen, rGen, sGen, wGen, (x0, y0, z0, w0), s0) = generators;

            // Squares
            var u2 = uGen.Square();
            var v2 = vGen.Square();
            var p2 = pGen.Square();
            var q2 = qGen.Square();
            var r2 = rGen.Square();
            var s2 = sGen.Square();

            var sumSquares = 
                (u2 + v2 + p2 + q2 + r2 + s2).Simplify(tolerance);

            // Derivatives (hodograph components)
            (XDt, YDt, ZDt, WDt) = BernsteinPolynomial.SimplifyToEqualDegree(
                2 * wGen * (uGen * pGen + vGen * qGen),
                2 * wGen * (uGen * qGen - vGen * pGen),
                2 * wGen * (uGen * rGen + vGen * sGen),
                2 * wGen * (uGen * sGen - vGen * rGen),
                tolerance
            );
            //XDt = (2 * wGen * (uGen * pGen + vGen * qGen)).Simplify(tolerance);
            //YDt = (2 * wGen * (uGen * qGen - vGen * pGen)).Simplify(tolerance);
            //ZDt = (2 * wGen * (uGen * rGen + vGen * sGen)).Simplify(tolerance);
            //WDt = (2 * wGen * (uGen * sGen - vGen * rGen)).Simplify(tolerance);
            
            (XDt2, YDt2, ZDt2, WDt2) = BernsteinPolynomial.SimplifyToEqualDegree(
                XDt.Derivative(),
                YDt.Derivative(),
                ZDt.Derivative(),
                WDt.Derivative(),
                tolerance
            );
            //XDt2 = XDt.Derivative();
            //YDt2 = YDt.Derivative();
            //ZDt2 = ZDt.Derivative();
            //WDt2 = WDt.Derivative();

            // Integrals
            (X, Y, Z, W) = BernsteinPolynomial.SimplifyToEqualDegree(
                XDt.Integral().Add(x0),
                YDt.Integral().Add(y0),
                ZDt.Integral().Add(z0),
                WDt.Integral().Add(w0),
                tolerance
            );
            //X = XDt.Integral().Add(x0).Simplify(tolerance);
            //Y = YDt.Integral().Add(y0).Simplify(tolerance);
            //Z = ZDt.Integral().Add(z0).Simplify(tolerance);
            //W = WDt.Integral().Add(w0).Simplify(tolerance);
            
            SDt = (wGen * sumSquares).Simplify(tolerance);
            S  = SDt.Integral().Add(s0).Simplify(tolerance);
            
            var speed = SDt;

            // Derivatives
            var x2 = XDt.Derivative();
            var y2 = YDt.Derivative();
            var z2 = ZDt.Derivative();
            var w2 = WDt.Derivative();

            var x3 = x2.Derivative();
            var y3 = y2.Derivative();
            var z3 = z2.Derivative();
            var w3 = w2.Derivative();

            var x4 = x3.Derivative();
            var y4 = y3.Derivative();
            var z4 = z3.Derivative();
            var w4 = w3.Derivative();

            // --- κ1 numerator: ||r''||² ---
            var accelNormSq = (x2.Square() + y2.Square() + z2.Square() + w2.Square()).Simplify(tolerance);

            // --- κ2 numerator: || r' ∧ r'' ∧ r''' ||² ---
            var Mxyz = XDt * (y2 * z3 - z2 * y3) - YDt * (x2 * z3 - z2 * x3) + ZDt * (x2 * y3 - y2 * x3);
            var Mxyw = XDt * (y2 * w3 - w2 * y3) - YDt * (x2 * w3 - w2 * x3) + WDt * (x2 * y3 - y2 * x3);
            var Mxzw = XDt * (z2 * w3 - w2 * z3) - ZDt * (x2 * w3 - w2 * x3) + WDt * (x2 * z3 - z2 * x3);
            var Myzw = YDt * (z2 * w3 - w2 * z3) - ZDt * (y2 * w3 - w2 * y3) + WDt * (y2 * z3 - z2 * y3);

            var wedgeNormSq = (Mxyz.Square() + Mxyw.Square() + Mxzw.Square() + Myzw.Square()).Simplify(tolerance);

            // --- κ3 numerator: det([r', r'', r''', r(4)]) ---
            var G4Det =
                XDt * (y2 * (z3 * w4 - z4 * w3) - z2 * (y3 * w4 - y4 * w3) + w2 * (y3 * z4 - y4 * z3))
              - YDt * (x2 * (z3 * w4 - z4 * w3) - z2 * (x3 * w4 - x4 * w3) + w2 * (x3 * z4 - x4 * z3))
              + ZDt * (x2 * (y3 * w4 - y4 * w3) - y2 * (x3 * w4 - x4 * w3) + w2 * (x3 * y4 - x4 * y3))
              - WDt * (x2 * (y3 * z4 - y4 * z3) - y2 * (x3 * z4 - x4 * z3) + z2 * (x3 * y4 - x4 * y3));

            // --- Curvatures ---
            Kappa1 = new RationalBernsteinPolynomial(
                accelNormSq,
                speed.Cube(),
                tolerance
            );

            // κ2² = wedgeNormSq / (||r' ∧ r''||⁴ * ||r'||²)
            Kappa2Squared = new RationalBernsteinPolynomial(
                wedgeNormSq,
                accelNormSq.Square() * speed.Square(),
                tolerance
            );

            // κ3² = (det([r',r'',r''',r(4)]))² / (||r' ∧ r'' ∧ r'''||⁴)
            Kappa3Squared = new RationalBernsteinPolynomial(
                G4Det.Square(),
                wedgeNormSq.Square(),
                tolerance
            );

            // --- Energy density: κ1² ||r'|| ---
            EnergyDensity = new RationalBernsteinPolynomial(
                accelNormSq,
                speed.Power(5),
                tolerance
            );
        }

        
        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
        
        public override Float64Path4D ToFinitePath()
        {
            throw new NotImplementedException();
        }

        public override Float64Path4D ToPeriodicPath()
        {
            throw new NotImplementedException();
        }

        public override LinFloat64Vector4D GetValue(double t)
        {
            return LinFloat64Vector4D.Create(
                X.GetValue(t), 
                Y.GetValue(t), 
                Z.GetValue(t), 
                W.GetValue(t)
            );
        }

        public override LinFloat64Vector4D GetDerivative1Value(double t)
        {
            return LinFloat64Vector4D.Create(
                XDt.GetValue(t), 
                YDt.GetValue(t), 
                ZDt.GetValue(t), 
                WDt.GetValue(t)
            );
        }
    
        public override LinFloat64Vector4D GetDerivative2Value(double t)
        {
            return LinFloat64Vector4D.Create(
                XDt2.GetValue(t), 
                YDt2.GetValue(t), 
                ZDt2.GetValue(t), 
                WDt2.GetValue(t)
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
        
        public IReadOnlyList<LinFloat64Vector4D> GetControlPoints()
        {
            Debug.Assert(X.Degree == Y.Degree && Y.Degree == Z.Degree && Z.Degree == W.Degree);

            return (X.Degree + 1).GetRange(i => 
                LinFloat64Vector4D.Create(
                    X.Coeffs[i], 
                    Y.Coeffs[i], 
                    Z.Coeffs[i], 
                    W.Coeffs[i]
                )
            ).ToArray();
        }

        public Float64BoundingBox4D GetBoundingBox4D(double tol = Float64Utils.DefaultTolerance)
        {
            var xCandidates = new List<double> { X.GetValue(0.0), X.GetValue(1.0) };
            var yCandidates = new List<double> { Y.GetValue(0.0), Y.GetValue(1.0) };
            var zCandidates = new List<double> { Z.GetValue(0.0), Z.GetValue(1.0) };
            var wCandidates = new List<double> { W.GetValue(0.0), W.GetValue(1.0) };

            // Critical points where derivative = 0
            foreach (var t in X.Derivative().FindRoots(tol))
                xCandidates.Add(X.GetValue(t));
        
            foreach (var t in Y.Derivative().FindRoots(tol))
                yCandidates.Add(Y.GetValue(t));
            
            foreach (var t in Z.Derivative().FindRoots(tol))
                zCandidates.Add(Z.GetValue(t));
            
            foreach (var t in W.Derivative().FindRoots(tol))
                wCandidates.Add(W.GetValue(t));

            return Float64BoundingBox4D.Containing(
                xCandidates.Min(), 
                yCandidates.Min(), 
                zCandidates.Min(), 
                wCandidates.Min(), 
                xCandidates.Max(),
                yCandidates.Max(),
                zCandidates.Max(),
                wCandidates.Max()
            );
        }
    }
}
