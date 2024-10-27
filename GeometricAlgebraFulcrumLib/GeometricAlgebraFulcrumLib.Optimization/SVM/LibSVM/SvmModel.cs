using System.Runtime.InteropServices;

namespace GeometricAlgebraFulcrumLib.Optimization.SVM.LibSVM
{
    public class SvmModel
    {
        public enum CreationType
        {
            Train = 0,
            LoadModel = 1
        }

        
        internal static SvmModel CreateFromModelStruct(LibSvmInterop.ModelStruct x)
        {
            var y = new SvmModel
            {
                Creation = (CreationType)x.free_sv,
                ClassCount = x.nr_class,
                SupportVectorTotalCount = x.l
            };

            if (y.Creation == CreationType.LoadModel)
            {
                y.Parameter = new SvmParameters
                {
                    Type = (SvmTaskType)x.param.svm_type,
                    Kernel = (SvmKernelType)x.param.kernel_type
                };

                switch (y.Parameter.Kernel)
                {
                    case SvmKernelType.Linear:
                        break;

                    case SvmKernelType.Polynomial:
                        y.Parameter.Gamma = x.param.gamma;
                        y.Parameter.Coef0 = x.param.coef0;
                        y.Parameter.Degree = x.param.degree;
                        break;

                    case SvmKernelType.Rbf:
                        y.Parameter.Gamma = x.param.gamma;
                        break;

                    case SvmKernelType.Sigmoid:
                        y.Parameter.Gamma = x.param.gamma;
                        y.Parameter.Coef0 = x.param.coef0;
                        break;
                }
            }
            else
            {
                y.Parameter = SvmParameters.CreateFromStruct(x.param);
            }

            var problemCount = (int)(y.ClassCount * (y.ClassCount - 1) * 0.5);

            y.Rho = new double[problemCount];
            Marshal.Copy(x.rho, y.Rho, 0, y.Rho.Length);

            y.ProbabilityA = [];
            if (x.probA != IntPtr.Zero)
            {
                y.ProbabilityA = new double[problemCount];
                Marshal.Copy(x.probA, y.ProbabilityA, 0, y.ProbabilityA.Length);
            }

            y.ProbabilityB = [];
            if (x.probB != IntPtr.Zero)
            {
                y.ProbabilityB = new double[problemCount];
                Marshal.Copy(x.probB, y.ProbabilityB, 0, y.ProbabilityB.Length);
            }

            if (x.nSV != IntPtr.Zero)
            {
                y.SupportVectorCounts = new int[y.ClassCount];
                Marshal.Copy(x.nSV, y.SupportVectorCounts, 0, y.SupportVectorCounts.Length);

                y.Labels = new int[y.ClassCount];
                Marshal.Copy(x.label, y.Labels, 0, y.Labels.Length);
            }

            y.SupportVectorCoefs = new List<double[]>(y.ClassCount - 1);
            var iPtrSvCoef = x.sv_coef;
            for (var i = 0; i < y.ClassCount - 1; i++)
            {
                y.SupportVectorCoefs.Add(new double[y.SupportVectorTotalCount]);
                var coefPtr = (IntPtr)Marshal.PtrToStructure(iPtrSvCoef, typeof(IntPtr))!;
                Marshal.Copy(coefPtr, y.SupportVectorCoefs[i], 0, y.SupportVectorCoefs[i].Length);
                iPtrSvCoef = IntPtr.Add(iPtrSvCoef, Marshal.SizeOf(typeof(IntPtr)));
            }

            y.SupportVectorIndices = [];
            if (x.sv_indices != IntPtr.Zero)
            {
                y.SupportVectorIndices = new int[y.SupportVectorTotalCount];
                Marshal.Copy(x.sv_indices, y.SupportVectorIndices, 0, y.SupportVectorIndices.Length);
            }

            y.SupportVectorList = [];
            var iPtrSv = x.SV;
            for (var i = 0; i < x.l; i++)
            {
                var ptrFeatures = (IntPtr)Marshal.PtrToStructure(iPtrSv, typeof(IntPtr))!;
                var features = SvmFeature.CreateFromStructArrayPtr(ptrFeatures);
                y.SupportVectorList.Add(features);
                iPtrSv = IntPtr.Add(iPtrSv, Marshal.SizeOf(typeof(IntPtr)));
            }

            return y;
        }
        
        public static SvmModel CreateFromModelStructPtr(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentNullException();

            var x = (LibSvmInterop.ModelStruct)Marshal.PtrToStructure(ptr, typeof(LibSvmInterop.ModelStruct))!;
            return CreateFromModelStruct(x);
        }


        /// <summary>
        /// SVM parameter.
        /// </summary>
        public SvmParameters Parameter { get; set; } = new();

        /// <summary>
        /// Number of classes, = 2 in regression/one class svm.
        /// </summary>
        public int ClassCount { get; init; }

        /// <summary>
        /// Total support vector count.
        /// </summary>
        public int SupportVectorTotalCount { get; init; }

        /// <summary>
        /// Support vectors (SV[TotalSVCount])
        /// </summary>
        public List<IReadOnlyList<SvmFeature>> SupportVectorList { get; set; } = [];

        /// <summary>
        /// Coefficients for SVs in decision functions (sv_coef[ClassCount-1][TotalSVCount])
        /// </summary>
        public List<double[]> SupportVectorCoefs { get; set; } = [];

        /// <summary>
        /// Constants in decision functions (rho[ClassCount*(ClassCount-1)/2])
        /// </summary>
        public double[] Rho { get; set; } = [];

        /// <summary>
        /// Pairwise probability information (A)
        /// </summary>
        public double[] ProbabilityA { get; set; } = [];

        /// <summary>
        /// Pairwise probability information (B)
        /// </summary>
        public double[] ProbabilityB { get; set; } = [];

        /// <summary>
        /// SVIndices[0,...,SVCounts-1] are values in [1,...,num_training_data] to indicate SVs in the training set.
        /// </summary>
        public int[] SupportVectorIndices { get; set; } = [];

        /// <summary>
        /// Label of each class (Labels[ClassCount]).
        /// </summary>
        public int[] Labels { get; set; } = [];

        /// <summary>
        /// Number of SVs for each class (SVCounts[ClassCount]). SVCounts[0] + SVCounts[1] + ... + SVCounts[ClassCount-1] = TotalSVCount.
        /// </summary>
        public int[] SupportVectorCounts { get; set; } = [];

        /// <summary>
        /// Creation type of the model.
        /// </summary>
        public CreationType Creation { get; set; } = CreationType.LoadModel;


        public SvmModel GetCopy()
        {
            var y = new SvmModel
            {
                Parameter = Parameter.GetCopy(),
                ClassCount = ClassCount,
                SupportVectorTotalCount = SupportVectorTotalCount,
                SupportVectorList = SupportVectorList
            };

            if (SupportVectorCoefs.Count > 0)
                y.SupportVectorCoefs = SupportVectorCoefs.Select(a => a.Select(b => b).ToArray()).ToList();

            if (Rho.Length > 0)
                y.Rho = Rho.Select(a => a).ToArray();

            if (ProbabilityA.Length > 0)
                y.ProbabilityA = ProbabilityA.Select(a => a).ToArray();

            if (ProbabilityB.Length > 0)
                y.ProbabilityB = ProbabilityB.Select(a => a).ToArray();

            if (SupportVectorIndices.Length > 0)
                y.SupportVectorIndices = SupportVectorIndices.Select(a => a).ToArray();

            if (Labels.Length > 0)
                y.Labels = Labels.Select(a => a).ToArray();

            if (SupportVectorCounts.Length > 0)
                y.SupportVectorCounts = SupportVectorCounts.Select(a => a).ToArray();

            y.Creation = Creation;
            return y;
        }
    }
}
