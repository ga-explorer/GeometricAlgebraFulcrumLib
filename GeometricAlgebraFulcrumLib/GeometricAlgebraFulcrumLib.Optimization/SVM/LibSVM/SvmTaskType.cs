namespace GeometricAlgebraFulcrumLib.Optimization.SVM.LibSVM;

//enum { C_SVC, NU_SVC, ONE_CLASS, EPSILON_SVR, NU_SVR };    /* svm_type * /
public enum SvmTaskType
{
    /// <summary>
    /// Multi-class classification.
    /// </summary>
    CSvc = 0,

    /// <summary>
    /// Multi-class classification.
    /// </summary>
    NuSvc = 1,

    /// <summary>
    /// One class SVM.
    /// </summary>
    OneClass = 2,

    /// <summary>
    /// Regression.
    /// </summary>
    EpsilonSvr = 3,

    /// <summary>
    /// Regression.
    /// </summary>
    NuSvr = 4
}