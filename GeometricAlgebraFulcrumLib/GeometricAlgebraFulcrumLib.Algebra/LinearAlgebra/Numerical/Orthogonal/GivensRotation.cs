using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.Orthogonal;

/// <summary>
/// Represents a Givens rotation matrix G(i,j,θ) that rotates in the (i,j)-plane.
/// A Givens rotation is an orthogonal matrix that differs from the identity matrix
/// in only four elements:
/// G[i,i] = cos(θ), G[i,j] = -sin(θ)
/// G[j,i] = sin(θ), G[j,j] = cos(θ)
/// 
/// For numerical stability, we store cos(θ) and sin(θ) directly rather than θ.
/// </summary>
public readonly struct GivensRotation : 
    IEquatable<GivensRotation>
{
    public static GivensRotation Create(int i, int j, LinFloat64PolarAngle angle)
    {
        return new GivensRotation(i, j, angle);
    }

    /// <summary>
    /// Constructs a Givens rotation to zero out element (j,i) in a matrix,
    /// i.e., to eliminate the element below the diagonal.
    /// Given elements a = A[i,i] and b = A[j,i], computes rotation that zeros out b.
    /// </summary>
    /// <param name="i">Row index of pivot element</param>
    /// <param name="j">Row index of element to be zeroed</param>
    /// <param name="a">Pivot element A[i,i]</param>
    /// <param name="b">Element to be zeroed A[j,i]</param>
    /// <returns>Givens rotation that zeros out b</returns>
    public static GivensRotation Create(int i, int j, double a, double b)
    {
        if (i >= j)
            throw new ArgumentException("i must be less than j", nameof(j));

        if (Math.Abs(b) < double.Epsilon)
        {
            // Already zero, return identity rotation
            return new GivensRotation(i, j, LinFloat64PolarAngle.Angle0);
        }

        if (Math.Abs(a) < double.Epsilon)
        {
            // Pivot is zero, simple rotation by π/2
            var angle = LinFloat64PolarAngle.CreateFromCosSin(0, Math.Sign(b));
            
            return new GivensRotation(i, j, angle);
        }

        {
            // Standard Givens rotation computation for numerical stability
            double t, c, s;

            if (Math.Abs(b) > Math.Abs(a))
            {
                t = a / b;
                s = Math.Sign(b) / Math.Sqrt(1 + t * t);
                c = s * t;
            }
            else
            {
                t = b / a;
                c = Math.Sign(a) / Math.Sqrt(1 + t * t);
                s = c * t;
            }

            var angle = LinFloat64PolarAngle.CreateFromCosSin(c, s);

            return new GivensRotation(i, j, angle);
        }
    }


    /// <summary>
    /// Row index of the rotation plane
    /// </summary>
    public int IndexI { get; }
    
    /// <summary>
    /// Column index of the rotation plane
    /// </summary>
    public int IndexJ { get; }
    
    /// <summary>
    /// Rotation angle
    /// </summary>
    public LinFloat64PolarAngle Angle { get; }

    /// <summary>
    /// Cosine of the rotation angle
    /// </summary>
    public double Cosine 
        => Angle.CosValue;

    /// <summary>
    /// Sine of the rotation angle
    /// </summary>
    public double Sine 
        => Angle.SinValue;


    ///// <summary>
    ///// Creates a Givens rotation with specified parameters
    ///// </summary>
    ///// <param name="i">Row index</param>
    ///// <param name="j">Column index</param>
    ///// <param name="cosine">Cosine of rotation angle</param>
    ///// <param name="sine">Sine of rotation angle</param>
    //private GivensRotation(int i, int j, double cosine, double sine)
    //{
    //    if (i == j)
    //        throw new ArgumentException("Rotation indices must be different", nameof(j));
    //    if (i > j)
    //        throw new ArgumentException("Row index must be less than column index", nameof(i));

    //    IndexI = i;
    //    IndexJ = j;
    //    Angle = LinFloat64PolarAngle.CreateFromCosSin(cosine, sine);
    //}
    
    /// <summary>
    /// Creates a Givens rotation with specified parameters
    /// </summary>
    /// <param name="i">Row index</param>
    /// <param name="j">Column index</param>
    /// <param name="angle">Rotation angle</param>
    private GivensRotation(int i, int j, LinFloat64PolarAngle angle)
    {
        if (i == j)
            throw new ArgumentException("Rotation indices must be different", nameof(j));
        
        if (i > j)
            throw new ArgumentException("Row index must be less than column index", nameof(i));

        IndexI = i;
        IndexJ = j;
        Angle = angle;
    }

    
    /// <summary>
    /// Validates that the rotation parameters satisfy orthogonality constraints
    /// </summary>
    /// <param name="tolerance">Numerical tolerance</param>
    /// <returns>True if rotation is valid</returns>
    public bool IsValid(double tolerance = Float64Utils.DefaultTolerance)
    {
        // Check that cos²(θ) + sin²(θ) = 1
        var normSquared = Cosine * Cosine + Sine * Sine;

        return IndexI >= 0 && IndexJ >= 0 && 
               Math.Abs(normSquared - 1.0) < tolerance;
    }

    /// <summary>
    /// Checks if this is essentially the identity rotation
    /// </summary>
    public bool IsIdentity(double tolerance = Float64Utils.DefaultTolerance)
    {
        return Math.Abs(Cosine - 1.0) < tolerance && 
               Math.Abs(Sine) < tolerance;
    }

    
    /// <summary>
    /// Creates the full matrix representation of the Givens rotation
    /// </summary>
    /// <param name="size">Size of the matrix</param>
    /// <returns>Full matrix representation</returns>
    public DenseMatrix ToDenseMatrix(int size)
    {
        if (IndexI >= size || IndexJ >= size)
            throw new ArgumentException("Indices exceed matrix size");

        var result = DenseMatrix.Create(size, size, 0);
        
        result.At(IndexI, IndexI, Cosine);
        result.At(IndexI, IndexJ, -Sine);
        result.At(IndexJ, IndexI, Sine);
        result.At(IndexJ, IndexJ, Cosine);
        
        return result;
    }
    
    /// <summary>
    /// Creates the full matrix representation of the Givens rotation
    /// </summary>
    /// <param name="size">Size of the matrix</param>
    /// <returns>Full matrix representation</returns>
    public SparseMatrix ToSparseMatrix(int size)
    {
        if (IndexI >= size || IndexJ >= size)
            throw new ArgumentException("Indices exceed matrix size");

        var result = SparseMatrix.Create(size, size, 0);
        
        result.At(IndexI, IndexI, Cosine);
        result.At(IndexI, IndexJ, -Sine);
        result.At(IndexJ, IndexI, Sine);
        result.At(IndexJ, IndexJ, Cosine);
        
        return result;
    }


    /// <summary>
    /// Applies the Givens rotation to two elements, computing the rotated values.
    /// This is the fundamental operation: [x'] = [c -s] [x]
    ///                                    [y']   [s  c] [y]
    /// </summary>
    /// <param name="x">First element</param>
    /// <param name="y">Second element</param>
    /// <returns>Tuple of rotated elements (x', y')</returns>
    public (double X, double Y) Apply(double x, double y)
    {
        return (Cosine * x - Sine * y, Sine * x + Cosine * y);
    }

    /// <summary>
    /// Applies the transpose (inverse) of the Givens rotation to two elements.
    /// Since Givens rotations are orthogonal, G^T = G^(-1), where:
    /// G^T = [c  s]
    ///       [-s c]
    /// </summary>
    /// <param name="x">First element</param>
    /// <param name="y">Second element</param>
    /// <returns>Tuple of rotated elements</returns>
    public (double X, double Y) ApplyTranspose(double x, double y)
    {
        return (Cosine * x + Sine * y, -Sine * x + Cosine * y);
    }
    
    /// <summary>
    /// Applies the Givens rotation to a vector V in-place from the left: G * V.
    /// This affects rows I and J of the vector.
    /// </summary>
    /// <param name="vector">Matrix to transform</param>
    public void ApplyLeft(Vector<double> vector)
    {
        ArgumentNullException.ThrowIfNull(vector);
        
        if (IndexI >= vector.Count || IndexJ >= vector.Count)
            throw new ArgumentException("Rotation indices exceed vector size");

        (vector[IndexI], vector[IndexJ]) = Apply(vector[IndexI], vector[IndexJ]);
    }

    /// <summary>
    /// Applies the Givens rotation to a matrix from the left: G * A
    /// This affects rows I and J of the matrix.
    /// </summary>
    /// <param name="matrix">Matrix to transform</param>
    public void ApplyLeft(Matrix<double> matrix)
    {
        ArgumentNullException.ThrowIfNull(matrix);
        
        if (IndexI >= matrix.RowCount || IndexJ >= matrix.RowCount)
            throw new ArgumentException("Rotation indices exceed matrix dimensions");

        for (var col = 0; col < matrix.ColumnCount; col++)
        {
            var (newI, newJ) = Apply(matrix[IndexI, col], matrix[IndexJ, col]);
            matrix[IndexI, col] = newI;
            matrix[IndexJ, col] = newJ;
        }
    }
    
    /// <summary>
    /// Applies the Givens rotation to a vector V in-place from the right: V * G.
    /// This affects rows I and J of the vector.
    /// </summary>
    /// <param name="vector">Matrix to transform</param>
    public void ApplyRight(Vector<double> vector)
    {
        ArgumentNullException.ThrowIfNull(vector);
        
        if (IndexI >= vector.Count || IndexJ >= vector.Count)
            throw new ArgumentException("Rotation indices exceed vector size");

        (vector[IndexI], vector[IndexJ]) = ApplyTranspose(vector[IndexI], vector[IndexJ]);
    }

    /// <summary>
    /// Applies the Givens rotation to a matrix from the right: A * G^T
    /// This affects columns I and J of the matrix.
    /// </summary>
    /// <param name="matrix">Matrix to transform</param>
    public void ApplyRight(Matrix<double> matrix)
    {
        ArgumentNullException.ThrowIfNull(matrix);
        
        if (IndexI >= matrix.ColumnCount || IndexJ >= matrix.ColumnCount)
            throw new ArgumentException("Rotation indices exceed matrix dimensions");

        for (var row = 0; row < matrix.RowCount; row++)
        {
            var (newI, newJ) = ApplyTranspose(matrix[row, IndexI], matrix[row, IndexJ]);
            matrix[row, IndexI] = newI;
            matrix[row, IndexJ] = newJ;
        }
    }

    ///// <summary>
    ///// Applies the Givens rotation to a skew-symmetric tridiagonal matrix,
    ///// preserving the special structure. This is optimized for the specific
    ///// sparsity pattern of tridiagonal matrices.
    ///// </summary>
    ///// <param name="matrix">Skew-symmetric tridiagonal matrix</param>
    ///// <param name="workArray">Temporary work array (size >= matrix.Size)</param>
    //public void ApplyToSkewSymmetricTridiagonal(SkewSymmetricTridiagonalMatrix matrix, double[] workArray)
    //{
    //    ArgumentNullException.ThrowIfNull(matrix);
    //    ArgumentNullException.ThrowIfNull(workArray);
        
    //    if (workArray.Length < matrix.Size)
    //        throw new ArgumentException("Work array too small", nameof(workArray));

    //    // Convert to dense form temporarily for the rotation
    //    // In a production implementation, this would be optimized to work
    //    // directly on the tridiagonal structure
    //    var dense = matrix.ToDenseMatrix();
    //    ApplyLeft(dense);
    //    ApplyRight(dense);
        
    //    // Extract the new tridiagonal form
    //    // Note: After rotation, the matrix may no longer be perfectly tridiagonal
    //    // This is where bulge chasing comes into play
    //}


    #region Equality and String Representation

    public bool Equals(GivensRotation other)
    {
        return IndexI == other.IndexI && 
               IndexJ == other.IndexJ && 
               Math.Abs(Angle.RadiansValue - other.Angle.RadiansValue) < Float64Utils.DefaultTolerance;
    }

    public override bool Equals(object? obj) => 
        obj is GivensRotation rotation && Equals(rotation);

    public override int GetHashCode() => 
        HashCode.Combine(IndexI, IndexJ, Angle.RadiansValue);

    public static bool operator ==(GivensRotation left, GivensRotation right) => 
        left.Equals(right);

    public static bool operator !=(GivensRotation left, GivensRotation right) => 
        !(left == right);

    public override string ToString()
    {
        return $"G({IndexI},{IndexJ}) [cos={Cosine:F6}, sin={Sine:F6}, θ={Angle.RadiansValue:F4} rad]";
    }

    #endregion
}