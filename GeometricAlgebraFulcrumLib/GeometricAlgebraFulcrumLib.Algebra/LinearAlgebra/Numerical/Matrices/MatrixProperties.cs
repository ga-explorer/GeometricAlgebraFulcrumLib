namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.Matrices;

/// <summary>
/// Represents the detected properties of a matrix.
/// </summary>
[Flags]
public enum MatrixProperties
{
    /// <summary>
    /// 
    /// </summary>
    None = 0,

    /// <summary>
    /// 
    /// </summary>
    Symmetric = 1 << 0,

    /// <summary>
    /// 
    /// </summary>
    SkewSymmetric = 1 << 1,

    /// <summary>
    /// 
    /// </summary>
    Hermitian = 1 << 2,

    /// <summary>
    /// 
    /// </summary>
    SkewHermitian = 1 << 3,

    /// <summary>
    /// 
    /// </summary>
    Orthogonal = 1 << 4,

    /// <summary>
    /// 
    /// </summary>
    Unitary = 1 << 5,

    /// <summary>
    /// 
    /// </summary>
    Normal = 1 << 6,

    /// <summary>
    /// 
    /// </summary>
    Diagonal = 1 << 7,

    /// <summary>
    /// 
    /// </summary>
    UpperTriangular = 1 << 8,

    /// <summary>
    /// 
    /// </summary>
    LowerTriangular = 1 << 9,

    /// <summary>
    /// 
    /// </summary>
    Triangular = UpperTriangular | LowerTriangular,

    /// <summary>
    /// 
    /// </summary>
    Positive = 1 << 10,

    /// <summary>
    /// 
    /// </summary>
    Negative = 1 << 11,

    /// <summary>
    /// 
    /// </summary>
    NonNegative = 1 << 12,

    /// <summary>
    /// 
    /// </summary>
    Stochastic = 1 << 13,

    /// <summary>
    /// 
    /// </summary>
    DoublyStochastic = 1 << 14,

    /// <summary>
    /// 
    /// </summary>
    Nilpotent = 1 << 15,

    /// <summary>
    /// 
    /// </summary>
    Idempotent = 1 << 16,

    /// <summary>
    /// 
    /// </summary>
    Involutory = 1 << 17,

    /// <summary>
    /// 
    /// </summary>
    EssentiallyNonnegative = 1 << 18,

    /// <summary>
    /// 
    /// </summary>
    IntensityMatrix = 1 << 19
}