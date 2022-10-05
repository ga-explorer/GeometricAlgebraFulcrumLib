namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer
{
    public enum McOptAtomicExpressionsPropagationKind
    {
        /// <summary>
        /// Only propagate a temp low-level item if it is associated with a constant
        /// </summary>
        PropagateConstant = 1,

        /// <summary>
        /// Only propagate a temp low-level item if it is associated with a constant or a single variable
        /// </summary>
        PropagateSingleVariable = 2,

        /// <summary>
        /// Only propagate a temp low-level item if it is associated with a constant or an expression that depends on a single variable
        /// </summary>
        PropagateSingleVariableDependent = 3
    }
}