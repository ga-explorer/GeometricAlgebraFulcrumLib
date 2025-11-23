namespace GeometricAlgebraFulcrumLib.Algebra.Utilities.MathNetNumerics.Schur.Exceptions
{
    /// <summary>
    /// Exception thrown when an iterative algorithm does not converge within the allowed number of steps.
    /// This corresponds to Julia's UnconvergedException.
    /// </summary>
    public class UnconvergedException : Exception
    {
        public UnconvergedException(string message) : base($"Convergence failure; {message}")
        {
        }

        public UnconvergedException(string message, Exception innerException) 
            : base($"Convergence failure; {message}", innerException)
        {
        }
    }

    /// <summary>
    /// Exception thrown when argument matrix or matrices are too ill-conditioned
    /// for the requested operation. The Index field may indicate the block
    /// where near-singularity was detected.
    /// </summary>
    public class IllConditionException : Exception
    {
        public int Index { get; }

        public IllConditionException(int index) : base($"Matrix is too ill-conditioned at index {index}")
        {
            Index = index;
        }

        public IllConditionException(int index, string message) 
            : base($"Matrix is too ill-conditioned at index {index}: {message}")
        {
            Index = index;
        }

        public IllConditionException(int index, string message, Exception innerException) 
            : base($"Matrix is too ill-conditioned at index {index}: {message}", innerException)
        {
            Index = index;
        }
    }
}