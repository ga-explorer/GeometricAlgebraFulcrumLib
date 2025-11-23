using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Numerical.Bivectors;

public sealed class LinFloat64BivectorAlignment
{
    public class Result
    {
        public Matrix<double> SimpleRotor1 { get; init; }

        public Matrix<double> SimpleRotor2 { get; init; }

        public Matrix<double> CompositeRotor { get; init; }
    }
    

    public static void Example1()
    {
        // Define two simple bivectors
        var b1 = CreateBivector(1, 0, 0, 0, 0, 0); // e12 plane
        var b2 = CreateBivector(0, 0, 0, 0, 0, 1); // e34 plane
        
        // Compute geodesic rotation
        var result = ComputeGeodesicRotation(b1, b2);
        
        Console.WriteLine("R1 Matrix:");
        Console.WriteLine(result.SimpleRotor1.ToString());
        Console.WriteLine();
        Console.WriteLine("R2 Matrix:");
        Console.WriteLine(result.SimpleRotor2.ToString());
        Console.WriteLine();
        Console.WriteLine("Composite Rotation R = R2*R1:");
        Console.WriteLine(result.CompositeRotor.ToString());
    }
    
    public static void Example()
    {
        var ga = XGaFloat64Processor.Euclidean;
        var rg = new Random(10);

        var u1 = 
            XGaFloat64VectorUtils.ToXGaVector((IEnumerable<double>)rg.GetFloat64Array1D(4, -1, 1), ga);

        var u2 = 
            XGaFloat64VectorUtils.ToXGaVector((IEnumerable<double>)rg.GetFloat64Array1D(4, -1, 1), ga);

        var v1 = 
            XGaFloat64VectorUtils.ToXGaVector((IEnumerable<double>)rg.GetFloat64Array1D(4, -1, 1), ga);

        var v2 = 
            XGaFloat64VectorUtils.ToXGaVector((IEnumerable<double>)rg.GetFloat64Array1D(4, -1, 1), ga);

        // Define two simple bivectors
        var b1 = u1.Op(v1).DivideByNorm().BivectorToMathNetVector(6);
        var b2 = u2.Op(v2).DivideByNorm().BivectorToMathNetVector(6);
        
        // Compute geodesic rotation
        var result = ComputeGeodesicRotation(b1, b2);
        
        Console.WriteLine("R1 Matrix:");
        Console.WriteLine(result.SimpleRotor1.ToString());
        Console.WriteLine();
        Console.WriteLine("R2 Matrix:");
        Console.WriteLine(result.SimpleRotor2.ToString());
        Console.WriteLine();
        Console.WriteLine("Composite Rotation R = R2*R1:");
        Console.WriteLine(result.CompositeRotor.ToString());
    }

    /// <summary>
    /// Represents a bivector in 4D as a 6-component vector
    /// (basis: e12, e13, e14, e23, e24, e34)
    /// </summary>
    /// <param name="e12"></param>
    /// <param name="e13"></param>
    /// <param name="e14"></param>
    /// <param name="e23"></param>
    /// <param name="e24"></param>
    /// <param name="e34"></param>
    /// <returns></returns>
    public static Vector<double> CreateBivector(double e12, double e13, double e14, double e23, double e24, double e34)
    {
        return DenseVector.OfArray([e12, e13, e14, e23, e24, e34]);
    }
    
    /// <summary>
    /// Convert bivector to antisymmetric matrix representation
    /// </summary>
    /// <param name="bivector"></param>
    /// <returns></returns>
    public static Matrix<double> BivectorToMatrix(Vector<double> bivector)
    {
        var b = DenseMatrix.Create(4, 4, 0.0);
        b[0, 1] = bivector[0];  // e12
        b[0, 2] = bivector[1];  // e13
        b[0, 3] = bivector[2];  // e14
        b[1, 2] = bivector[3];  // e23
        b[1, 3] = bivector[4];  // e24
        b[2, 3] = bivector[5];  // e34
        
        // Make antisymmetric
        for (var i = 0; i < 4; i++)
        for (var j = i + 1; j < 4; j++)
            b[j, i] = -b[i, j];
                
        return b;
    }
    
    /// <summary>
    /// Normalize bivector
    /// </summary>
    /// <param name="bivector"></param>
    /// <returns></returns>
    public static Vector<double> NormalizeBivector(Vector<double> bivector)
    {
        return bivector / bivector.L2Norm();
    }
    
    /// <summary>
    /// Compute geodesic double rotation
    /// </summary>
    /// <param name="b1"></param>
    /// <param name="b2"></param>
    /// <returns></returns>
    public static Result ComputeGeodesicRotation(Vector<double> b1, Vector<double> b2)
    {
        // Step 1: Normalize bivectors
        b1 = NormalizeBivector(b1);
        b2 = NormalizeBivector(b2);
        
        // Step 2: Convert to matrix form
        var b1Matrix = BivectorToMatrix(b1);
        var b2Matrix = BivectorToMatrix(b2);
        
        // Step 3: Compute the rotation bivector
        // This represents the bivector that rotates B1 to B2
        var rotationBivector = 
            ComputeRotationBivector(b1Matrix, b2Matrix);
        
        Console.WriteLine("Rotation Bivector B:");
        Console.WriteLine(rotationBivector.ToString());

        // Step 4: Decompose into two simple rotations
        var (plane1, plane2, angle1, angle2) = 
            DecomposeIntoSimpleRotations(rotationBivector);
        
        // Step 5: Create rotation matrices
        var r1 = CreateSimpleRotation(plane1, angle1);
        var r2 = CreateSimpleRotation(plane2, angle2);
        
        return new Result
        {
            SimpleRotor1 = r1,
            SimpleRotor2 = r2,
            CompositeRotor = r2 * r1
        };
    }
    
    private static Matrix<double> ComputeRotationBivector(Matrix<double> b1, Matrix<double> b2)
    {
        // Compute B2 * B1^(-1) where B1^(-1) = -B1 for unit bivectors
        var b1Inv = -b1;
        var rotBiv = b2 * b1Inv;
        
        // Extract the bivector part (antisymmetric part)
        var result = DenseMatrix.Create(4, 4, 0.0);
        for (var i = 0; i < 4; i++)
        for (var j = i + 1; j < 4; j++)
        {
            result[i, j] = (rotBiv[i, j] - rotBiv[j, i]) / 2.0;
            result[j, i] = -result[i, j];
        }
        
        return result;
    }
    
    private static (Matrix<double> plane1, Matrix<double> plane2, double angle1, double angle2) DecomposeIntoSimpleRotations(Matrix<double> bivector)
    {
        // Convert bivector matrix to 6D representation for eigendecomposition
        var bv = XGaFloat64Processor.Euclidean.Bivector(
            bivector[0, 1], 
            bivector[0, 2], 
            bivector[1, 2],
            bivector[0, 3], 
            bivector[1, 3], 
            bivector[2, 3]
        );
        
        // Create the 6x6 matrix for bivector multiplication
        var commutator = 
            bv.LeftECpBivectorMapMatrix(4);
        
        // Find eigenvalues and eigenvectors
        var evd = commutator.ToComplex().Evd();
        var eigenvalues = evd.EigenValues;
        var eigenvectors = evd.EigenVectors;
        
        // Extract the two dominant eigenvectors corresponding to simple bivectors
        var ev1 = eigenvectors.Column(0).Real();
        var ev2 = eigenvectors.Column(1).Real();
        
        // Convert back to matrix form
        var plane1 = BivectorToMatrix(DenseVector.OfArray(ev1.ToArray()));
        var plane2 = BivectorToMatrix(DenseVector.OfArray(ev2.ToArray()));
        
        // Compute angles
        var angle1 = Math.Sqrt(eigenvalues[0].Real);
        var angle2 = Math.Sqrt(eigenvalues[1].Real);
        
        return (plane1, plane2, angle1, angle2);
    }
    
    private static Matrix<double> CreateBivectorCommutatorMatrix()
    {
        // This creates the 6x6 matrix representing bivector commutation relations
        // Implementation details for 4D bivector algebra structure constants
        var matrix = DenseMatrix.Create(6, 6, 0.0);
        
        // Structure constants for Cl(4,0) bivector algebra
        // [This would contain the full structure constant matrix]
        // Simplified version - in practice, this requires the complete
        // multiplication table for the 6 bivector basis elements
        
        return matrix;
    }
    
    private static Matrix<double> CreateSimpleRotation(Matrix<double> plane, double angle)
    {
        // Create rotation matrix from bivector plane and angle
        // R = exp(angle * plane)
        
        var e1234 = DenseMatrix.CreateIdentity(4);
        var plane2 = plane * plane;
        
        // Use Rodriguez formula: exp(θB) = I + sin(θ)B + (1-cos(θ))B²/2
        var result = e1234 + Math.Sin(angle) * plane + (1 - Math.Cos(angle)) * plane2 / 2.0;
        
        return result;
    }
}
