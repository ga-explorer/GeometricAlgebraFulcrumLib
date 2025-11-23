using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Numerical.Bivectors;

public sealed class XGaFloat64BivectorAlignment
{
    public sealed class Result
    {
        public XGaFloat64Multivector SimpleRotor1 { get; init; }

        public XGaFloat64Multivector SimpleRotor2 { get; init; }

        public XGaFloat64Multivector CompositeRotor { get; init; }
    }
    

    public static XGaFloat64Processor Processor 
        => XGaFloat64Processor.Euclidean;
    
    public static void Example()
    {
        // Define two simple bivectors
        var b1 = CreateSimpleBivector(1, 0, 0, 0, 0, 0); // e12 plane
        var b2 = CreateSimpleBivector(0, 0, 0, 0, 0, 1); // e34 plane
        
        // Compute geodesic rotation
        var result = ComputeGeodesicRotation(b1, b2);
        
        Console.WriteLine("GA R1 Rotor:");
        Console.WriteLine(result.SimpleRotor1.ToString());
        Console.WriteLine();
        Console.WriteLine("GA R2 Rotor:");
        Console.WriteLine(result.SimpleRotor2.ToString());
        Console.WriteLine();
        Console.WriteLine("GA Composite Rotation R = R2*R1:");
        Console.WriteLine(result.CompositeRotor.ToString());
    }

    public static XGaFloat64Bivector CreateSimpleBivector(double e12, double e13, double e14, double e23, double e24, double e34)
    {
        return Processor.CreateBivectorComposer()
            .SetBivectorTerm(0, 1, e12)  // e12
            .SetBivectorTerm(0, 2, e13)  // e13
            .SetBivectorTerm(0, 3, e14)  // e14
            .SetBivectorTerm(1, 2, e23)  // e23
            .SetBivectorTerm(1, 3, e24)  // e24
            .SetBivectorTerm(2, 3, e34)  // e34
            .GetBivector();
    }
    
    public static Result ComputeGeodesicRotation(XGaFloat64Bivector b1, XGaFloat64Bivector b2)
    {
        // Step 1: Normalize bivectors
        var b1Norm = b1.DivideByNorm().RemoveSmallTerms();
        var b2Norm = b2.DivideByNorm().RemoveSmallTerms();
        
        // Step 2: Compute the rotor that rotates B1 to B2
        // R = (B2 * B1^(-1) + 1) / |B2 * B1^(-1) + 1|
        var b1NormInv = 
            b1Norm.Negative(); // For unit bivectors, inverse is negative
        
        var rotor = 
            (b2Norm.Gp(b1NormInv) + 1).DivideByNorm();
       
        //var rotorBiv = b2Norm.Gp(b1Inv).RemoveSmallTerms();
        
        // Add scalar part
        //var rotor = 
        //Processor
        //    .CreateMultivectorComposer()
        //    .SetScalarTerm(1.0)
        //    .AddMultivector(rotorBiv)
        //    .GetMultivector()
        //    .RemoveSmallTerms()
        //    .DivideByNorm();
        
        // Step 3: Decompose into simple rotations
        var (r1, r2) = DecomposeRotor(rotor);
        
        return new Result
        {
            SimpleRotor1 = r1,
            SimpleRotor2 = r2,
            CompositeRotor = r2.Gp(r1)
        };
    }
    
    private static (XGaFloat64Multivector r1, XGaFloat64Multivector r2) DecomposeRotor(XGaFloat64Multivector rotor)
    {
        // Extract bivector part
        var bivectorPart = rotor.GetBivectorPart();
        
        // Decompose bivector into two simple bivectors
        var (simpleBiv1, simpleBiv2, angle1, angle2) = 
            DecomposeBivector(bivectorPart);
        
        // Create simple rotors
        var r1 = CreateSimpleRotor(simpleBiv1, angle1);
        var r2 = CreateSimpleRotor(simpleBiv2, angle2);
        
        return (r1, r2);
    }
    
    private static (XGaFloat64Bivector biv1, XGaFloat64Bivector biv2, double angle1, double angle2) DecomposeBivector(XGaFloat64Bivector bivector)
    {
        // This is the core algorithm for decomposing a general bivector
        // into two orthogonal simple bivectors
        
        // For 4D, we need to find the eigendecomposition of the bivector
        // represented as a 6x6 matrix in the bivector basis
        
        // Extract coefficients
        var coeffs = DenseVector.Create(6, 0);
        coeffs[0] = bivector.Scalar(0, 1); // e12
        coeffs[1] = bivector.Scalar(0, 2); // e13
        coeffs[2] = bivector.Scalar(0, 3); // e14
        coeffs[3] = bivector.Scalar(1, 2); // e23
        coeffs[4] = bivector.Scalar(1, 3); // e24
        coeffs[5] = bivector.Scalar(2, 3); // e34
        
        // Perform eigendecomposition to find simple bivectors
        // This requires solving for the principal planes
        
        // Simplified approach: find orthogonal simple bivectors
        var (plane1Coeffs, plane2Coeffs) = 
            FindOrthogonalSimpleBivectors(coeffs);
        
        var biv1 = 
            CreateSimpleBivector(
                plane1Coeffs[0], 
                plane1Coeffs[1], 
                plane1Coeffs[2], 
                plane1Coeffs[3], 
                plane1Coeffs[4], 
                plane1Coeffs[5]
            );

        var biv2 = 
            CreateSimpleBivector(
                plane2Coeffs[0], 
                plane2Coeffs[1], 
                plane2Coeffs[2], 
                plane2Coeffs[3], 
                plane2Coeffs[4], 
                plane2Coeffs[5]
            );
        
        var angle1 = biv1.Norm();
        var angle2 = biv2.Norm();
        
        return (biv1.DivideByNorm(), biv2.DivideByNorm(), angle1, angle2);
    }
    
    private static (Vector<double> plane1, Vector<double> plane2) FindOrthogonalSimpleBivectors(Vector<double> coeffs)
    {
        // Algorithm to find two orthogonal simple bivectors that sum to the given bivector
        // This involves solving a system of equations based on the constraint that
        // simple bivectors square to scalars
        
        // For a simple bivector B = a*e12 + b*e34, we have B² = (a² - b²)
        // We need to find two such bivectors B1, B2 where B1 + B2 equals our input
        
        // Simplified version - in practice this requires more sophisticated decomposition
        var plane1 = DenseVector.Create(6, 0);
        var plane2 = DenseVector.Create(6, 0);
        
        // This is a placeholder - the actual algorithm would involve:
        // 1. Setting up the constraint equations
        // 2. Solving for the simple bivector components
        // 3. Ensuring orthogonality
        
        // For now, return a basic decomposition
        plane1[0] = coeffs[0];
        plane1[1] = coeffs[1];
        plane1[2] = coeffs[2];
        plane2[3] = coeffs[3];
        plane2[4] = coeffs[4];
        plane2[5] = coeffs[5];
        
        return (plane1, plane2);
    }
    
    private static XGaFloat64Multivector CreateSimpleRotor(XGaFloat64Bivector bivector, double angle)
    {
        // Create rotor: R = cos(θ/2) + sin(θ/2) * B
        var halfAngle = angle / 2.0;
        var cosHalf = Math.Cos(halfAngle);
        var sinHalf = Math.Sin(halfAngle);
        
        return Processor.CreateMultivectorComposer()
            .SetScalarTerm(cosHalf)
            .AddMultivector(bivector.Times(sinHalf))
            .GetMultivector();
    }
}