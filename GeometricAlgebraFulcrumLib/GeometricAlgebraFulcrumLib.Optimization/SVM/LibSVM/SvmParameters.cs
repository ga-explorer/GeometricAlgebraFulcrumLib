using System.Runtime.InteropServices;

namespace GeometricAlgebraFulcrumLib.Optimization.SVM.LibSVM
{
    public class SvmParameters
    {
        internal static SvmParameters CreateFromStruct(LibSvmInterop.ParametersStruct x)
        {
            var y = new SvmParameters
            {
                Type = (SvmTaskType)x.svm_type,
                Kernel = (SvmKernelType)x.kernel_type,
                Degree = x.degree,
                Gamma = x.gamma,
                Coef0 = x.coef0,
                CacheSize = x.cache_size,
                Eps = x.eps,
                C = x.C,
                Nu = x.nu,
                P = x.p,
                Shrinking = x.shrinking != 0,
                Probability = x.probability != 0
            };

            var length = x.nr_weight;
            y.WeightLabels = new int[length];
            if (length > 0)
                Marshal.Copy(x.weight_label, y.WeightLabels, 0, length);

            y.Weights = new double[length];
            if (length > 0)
                Marshal.Copy(x.weight, y.Weights, 0, length);

            return y;
        }

        public static SvmParameters CreateFromStructPtr(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentNullException();

            var x = (LibSvmInterop.ParametersStruct)Marshal.PtrToStructure(ptr, typeof(LibSvmInterop.ParametersStruct))!;
            return CreateFromStruct(x);
        }


        /// <summary>
        /// Type of SVM formulation. Possible values are: 
        /// [C_SVC] C-Support Vector Classification. n-class classification (n >= 2), allows imperfect separation of classes with penalty multiplier C for outliers. 
        /// [NU_SVC] nu-Support Vector Classification. n-class classification with possible imperfect separation. Parameter Nu (in the range 0..1, the larger the value, the smoother the decision boundary) is used instead of C. 
        /// [ONE_CLASS] Distribution Estimation (One-class SVM). All the training data are from the same class, SVM builds a boundary that separates the class from the rest of the feature space. 
        /// [EPS_SVR] epsilon-Support Vector Regression. The distance between feature vectors from the training set and the fitting hyper-plane must be less than P. For outliers the penalty multiplier C is used.
        /// [NU_SVR] nu-Support Vector Regression. Nu is used instead of p.
        /// </summary>
        public SvmTaskType Type { get; init; } = SvmTaskType.CSvc;

        /// <summary>
        /// Type of SVM kernel. Possible values are:
        /// [LINEAR] Linear kernel. No mapping is done, linear discrimination (or regression) is done in the original feature space. It is the fastest option.
        /// [POLY] Polynomial kernel.
        /// [RBF] Radial basis function (RBF), a good choice in most cases.
        /// [SIGMOID] Sigmoid kernel.
        /// </summary>
        public SvmKernelType Kernel { get; init; } = SvmKernelType.Rbf;

        /// <summary>
        /// Parameter degree of a kernel function (POLY).
        /// </summary>
        public int Degree { get; set; } = 3;

        /// <summary>
        /// Parameter gamma of a kernel function (POLY / RBF / SIGMOID).
        /// </summary>
        public double Gamma { get; set; } = 1; // divided by num_of_features

        /// <summary>
        /// Parameter coef0 of a kernel function (POLY / SIGMOID).
        /// </summary>
        public double Coef0 { get; set; } = 0;

        /// <summary>
        /// Cache size in MegaBytes.
        /// </summary>
        public double CacheSize { get; init; } = 100;

        /// <summary>
        /// Term criteria. Tolerance of the iterative SVM training procedure which solves a partial case of constrained quadratic optimization problem
        /// </summary>
        public double Eps { get; init; } = 0.001;

        /// <summary>
        /// Parameter C of a SVM optimization problem (C_SVC / EPS_SVR / NU_SVR).
        /// </summary>
        public double C { get; init; } = 1;

        /// <summary>
        ///  Optional weights in the C_SVC problem , assigned to particular classes.
        /// </summary>
        public int[] WeightLabels { get; set; } = [];

        /// <summary>
        ///  Optional weights in the C_SVC problem , assigned to particular classes.
        /// </summary>
        public double[] Weights { get; set; } = [];

        /// <summary>
        /// Parameter nu of an SVM optimization problem (NU_SVC / ONE_CLASS / NU_SVR).
        /// </summary>
        public double Nu { get; init; } = 0.5;

        /// <summary>
        /// Parameter epsilon of a SVM optimization problem (EPS_SVR).
        /// </summary>
        public double P { get; init; } = 0.1;

        /// <summary>
        /// Use the shrinking heuristics.
        /// </summary>
        public bool Shrinking { get; init; } = true;

        /// <summary>
        /// Train an SVC or SVR model for probability estimates.
        /// </summary>
        public bool Probability { get; init; } = false;

        public SvmParameters GetCopy()
        {
            var y = new SvmParameters
            {
                Type = Type,
                Kernel = Kernel,
                Degree = Degree,
                Gamma = Gamma,
                Coef0 = Coef0,
                C = C,
                Nu = Nu,
                P = P,
                CacheSize = CacheSize,
                Eps = Eps,
                Shrinking = Shrinking,
                Probability = Probability,
                WeightLabels = WeightLabels.Select(a => a).ToArray(),
                Weights = Weights.Select(a => a).ToArray()
            };
            return y;
        }
    }
}
