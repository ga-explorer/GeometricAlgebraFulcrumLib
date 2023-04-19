namespace GeometricAlgebraFulcrumLib.MathBase.Parametric.Frames
{
    public enum ParametricCurveLocalFrameSamplingMethod
    {
        // Rotation minimization using the method described in the paper
        // "Computation of Rotation Minimizing Frames"
        // https://www.microsoft.com/en-us/research/wp-content/uploads/2016/12/Computation-of-rotation-minimizing-frames.pdf
        MinimizedRotation = 0,

        // Simple rotation of tangents using simple Geometric Algebra rotors
        SimpleRotation = 1
    }
}