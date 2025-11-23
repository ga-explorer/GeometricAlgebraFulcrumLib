using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.BSpline
{
    /// <summary>
    /// Abstract base class for B-spline curves to eliminate code duplication
    /// and provide common functionality for 2D, 3D, and 4D curves.
    /// </summary>
    public abstract class BSplineCurveBase<TVector>
    {
        protected const double Epsilon = 1e-14;

        public BSplineBasisSet BasisSet { get; }

        public IReadOnlyList<TVector> ControlPoints { get; }


        protected BSplineCurveBase(BSplineBasisSet basisSet, IReadOnlyList<TVector> controlPoints)
        {
            BasisSet = basisSet ?? throw new ArgumentNullException(nameof(basisSet));
            ControlPoints = controlPoints ?? throw new ArgumentNullException(nameof(controlPoints));
            
            if (BasisSet.BasisCount != ControlPoints.Count)
                throw new ArgumentException("Basis count must equal number of control points.");
        }

        #region Common Evaluation Methods


        protected int FindSpanLocal(double t)
            => BSplineCurveUtils.FindSpan(BasisSet.KnotVector, BasisSet.Degree, t, ControlPoints.Count - 1);

        protected double[,] DersBasisFunsLocal(int span, double t, int nDeriv)
            => BSplineCurveUtils.DersBasisFunsStatic(span, t, BasisSet.Degree, nDeriv, BasisSet.KnotVector);

        #endregion

        #region Abstract Methods for Vector Operations

        protected abstract TVector CreateZero();
        protected abstract TVector CreateFromComponents(double[] components);
        protected abstract TVector AddVectors(TVector a, TVector b);
        protected abstract TVector ScaleVector(TVector vector, double scalar);
        protected abstract double[] GetVectorComponents(TVector vector);

        #endregion

        #region Common Factory Methods

        protected static double[] CreateCentripetalParameters<TPoint>(IReadOnlyList<TPoint> points, Func<TPoint, TPoint, double> distanceFunction)
        {
            if (points == null) throw new ArgumentNullException(nameof(points));
            if (points.Count < 2) throw new ArgumentException("At least two points required.");
            
            var m = points.Count - 1;
            var u = new double[m + 1];
            u[0] = 0.0;
            var total = 0.0;

            for (var i = 1; i <= m; i++)
            {
                var d = Math.Sqrt(distanceFunction(points[i], points[i - 1]));
                total += d;
                u[i] = total;
            }

            if (total <= 0.0)
            {
                // Fall back to uniform parameterization
                for (var i = 1; i <= m; i++) 
                    u[i] = (double)i / m;
                return u;
            }

            // Normalize to [0,1]
            for (var i = 1; i <= m; i++) 
                u[i] /= total;

            return u;
        }

        protected static BSplineKnotVector CreateClampedKnotVector(double[] u, int degree, int n)
        {
            if (u == null) throw new ArgumentNullException(nameof(u));
            if (degree < 0) throw new ArgumentOutOfRangeException(nameof(degree));
            if (n < degree) throw new ArgumentException("n must be >= degree");

            var knotVector = BSplineKnotVector.Create();

            // Start clamp: degree + 1 repetitions of first parameter
            knotVector.AppendKnot(u[0], degree + 1);

            // Internal knots
            var internalCount = n - degree;
            if (internalCount > 0)
            {
                for (var j = 1; j <= internalCount; j++)
                {
                    var sum = 0.0;
                    for (var k = j; k < j + degree; k++)
                    {
                        // Use valid indices, clamp to parameter range
                        var idx = Math.Min(Math.Max(k, 0), u.Length - 1);
                        sum += u[idx];
                    }
                    knotVector.AppendKnot(sum / degree);
                }
            }

            // End clamp: degree + 1 repetitions of last parameter
            knotVector.AppendKnot(u[^1], degree + 1);

            return knotVector;
        }

        protected static (TVector[] controlPoints, BSplineBasisSet basisSet) SolveInterpolationSystem<TPoint>(
            IReadOnlyList<TPoint> points,
            double[] parameters,
            int degree,
            Func<TPoint, double[]> getComponents,
            Func<double[], TVector> createVector)
        {
            var m = points.Count - 1;
            var n = m; // Interpolation: control points = data points
            
            var knotVector = CreateClampedKnotVector(parameters, degree, n);
            var basisSet = knotVector.CreateBSplineBasisSet(degree);

            // Build collocation matrix
            var collocationMatrix = Matrix.Build.Dense(m + 1, n + 1);
            
            for (var row = 0; row <= m; row++)
            {
                var t = parameters[row];
                var span = BSplineCurveUtils.FindSpan(knotVector, degree, t, n);
                var basisValues = BSplineCurveUtils.DersBasisFunsStatic(span, t, degree, 0, knotVector);
                
                for (var col = 0; col <= degree; col++)
                {
                    var controlPointIndex = span - degree + col;
                    if (controlPointIndex >= 0 && controlPointIndex <= n)
                    {
                        collocationMatrix[row, controlPointIndex] = basisValues[0, col];
                    }
                }
            }

            // Solve for each component
            var componentCount = getComponents(points[0]).Length;
            var componentSolutions = new Vector<double>[componentCount];
            
            for (var comp = 0; comp < componentCount; comp++)
            {
                var comp1 = comp;
                var rhs = Vector.Build.Dense(m + 1, i => getComponents(points[i])[comp1]);
                componentSolutions[comp] = collocationMatrix.LU().Solve(rhs);
            }

            // Create control points from solutions
            var controlPoints = new TVector[n + 1];
            for (var i = 0; i <= n; i++)
            {
                var components = new double[componentCount];
                for (var comp = 0; comp < componentCount; comp++)
                {
                    components[comp] = componentSolutions[comp][i];
                }
                controlPoints[i] = createVector(components);
            }

            return (controlPoints, basisSet);
        }

        protected static (TVector[] controlPoints, BSplineBasisSet basisSet) SolveLeastSquaresSystem<TPoint>(
            IReadOnlyList<TPoint> points,
            double[] parameters,
            int degree,
            int controlPointCount,
            Func<TPoint, double[]> getComponents,
            Func<double[], TVector> createVector,
            bool clampEndpoints = true)
        {
            var m = points.Count - 1;
            var n = controlPointCount - 1;
            
            var knotVector = CreateClampedKnotVector(parameters, degree, n);
            var basisSet = knotVector.CreateBSplineBasisSet(degree);

            // Build design matrix
            var designMatrix = Matrix.Build.Dense(m + 1, n + 1);
            
            for (var row = 0; row <= m; row++)
            {
                var t = parameters[row];
                var span = BSplineCurveUtils.FindSpan(knotVector, degree, t, n);
                var basisValues = BSplineCurveUtils.DersBasisFunsStatic(span, t, degree, 0, knotVector);
                
                for (var col = 0; col <= degree; col++)
                {
                    var controlPointIndex = span - degree + col;
                    if (controlPointIndex >= 0 && controlPointIndex <= n)
                    {
                        designMatrix[row, controlPointIndex] = basisValues[0, col];
                    }
                }
            }

            // Handle endpoint constraints if requested
            var finalMatrix = designMatrix;
            var componentCount = getComponents(points[0]).Length;
            var constrainedRhs = new Vector<double>[componentCount];

            if (clampEndpoints)
            {
                var augmentedMatrix = Matrix.Build.Dense(m + 3, n + 1);
                
                // Copy design matrix into augmented matrix
                augmentedMatrix.SetSubMatrix(0, 0, designMatrix);
                
                // Add endpoint constraints
                augmentedMatrix[m + 1, 0] = 1.0; // First control point = first data point
                augmentedMatrix[m + 2, n] = 1.0; // Last control point = last data point
                
                finalMatrix = augmentedMatrix;

                for (var comp = 0; comp < componentCount; comp++)
                {
                    var augmentedRhs = Vector.Build.Dense(m + 3);
                    for (var i = 0; i <= m; i++)
                    {
                        augmentedRhs[i] = getComponents(points[i])[comp];
                    }
                    augmentedRhs[m + 1] = getComponents(points[0])[comp];
                    augmentedRhs[m + 2] = getComponents(points[^1])[comp];
                    
                    constrainedRhs[comp] = augmentedRhs;
                }
            }
            else
            {
                for (var comp = 0; comp < componentCount; comp++)
                {
                    var comp1 = comp;
                    constrainedRhs[comp] = Vector.Build.Dense(m + 1, i => getComponents(points[i])[comp1]);
                }
            }

            // Solve the least squares problem
            var componentSolutions = new Vector<double>[componentCount];
            var qr = finalMatrix.QR();
            
            for (var comp = 0; comp < componentCount; comp++)
            {
                componentSolutions[comp] = qr.Solve(constrainedRhs[comp]);
            }

            // Create control points
            var controlPoints = new TVector[n + 1];
            for (var i = 0; i <= n; i++)
            {
                var components = new double[componentCount];
                for (var comp = 0; comp < componentCount; comp++)
                {
                    components[comp] = componentSolutions[comp][i];
                }
                controlPoints[i] = createVector(components);
            }

            return (controlPoints, basisSet);
        }

        #endregion

        #region Common Evaluation with Generic Vector Operations

        protected TVector Evaluate(double t, int derivativeOrder)
        {
            var span = FindSpanLocal(t);
            var ders = DersBasisFunsLocal(span, t, derivativeOrder);
            var p = BasisSet.Degree;

            var result = CreateZero();
            for (var j = 0; j <= p; j++)
            {
                var controlPoint = ControlPoints[span - p + j];
                var basisValue = ders[derivativeOrder, j];
                result = AddVectors(result, ScaleVector(controlPoint, basisValue));
            }

            return result;
        }

        protected TVector EvaluateLocalBasisCombination(double t, Func<int, TVector> getVectorForBasis)
        {
            var span = FindSpanLocal(t);
            var basisValues = DersBasisFunsLocal(span, t, 0);
            var p = BasisSet.Degree;

            var result = CreateZero();
            for (var j = 0; j <= p; j++)
            {
                var basisIndex = span - p + j;
                var basisValue = basisValues[0, j];
                var vector = getVectorForBasis(basisIndex);
                result = AddVectors(result, ScaleVector(vector, basisValue));
            }

            return result;
        }

        #endregion
    }
}