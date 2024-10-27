namespace GeometricAlgebraFulcrumLib.Optimization.SVM.LibSVM;

//enum { LINEAR, POLY, RBF, SIGMOID, PRECOMPUTED }; /* kernel_type * /
public enum SvmKernelType
{
    /// <summary>
    /// Linear: u'*v
    /// </summary>
    Linear = 0,

    /// <summary>
    /// Polynomial: (gamma*u'*v + coef0)^degree
    /// </summary>
    Polynomial = 1,

    /// <summary>
    /// Radial Basis Function: exp(-gamma*|u-v|^2)
    /// </summary>
    Rbf = 2,

    /// <summary>
    /// Sigmoid: tanh(gamma*u'*v + coef0)
    /// </summary>
    Sigmoid = 3,

    /// <summary>
    /// Precomputed
    /// </summary>
    Precomputed = 4
}