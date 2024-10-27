using System.Runtime.InteropServices;

namespace GeometricAlgebraFulcrumLib.Optimization.SVM.LibSVM
{
    internal static class LibSvmInterop
    {
        public static string Version => "3.33";


        //struct svm_feature
        //{
        //    int index;
        //    double value;
        //};
        [StructLayout(LayoutKind.Sequential)]
        public struct FeatureStruct
        {
            internal int index;
            internal double value;
        }

        //struct svm_problem
        //{
        //    int l;
        //    double *y;
        //    struct svm_feature **x;
        //};
        [StructLayout(LayoutKind.Sequential)]
        public struct DataSetStruct
        {
            public int l;
            public IntPtr y; // double*
            public IntPtr x; // svm_feature**
        }

        //struct svm_parameter
        //{
        //    int svm_type;
        //    int kernel_type;
        //    int degree;    /* for poly * /
        //    double gamma;    /* for poly/rbf/sigmoid * /
        //    double coef0;    /* for poly/sigmoid * /
        // 
        //    /* these are for training only * /
        //    double cache_size; /* in MB * /
        //    double eps;    /* stopping criteria * /
        //    double C;    /* for C_SVC, EPSILON_SVR and NU_SVR * /
        //    int nr_weight;        /* for C_SVC * /
        //    int *weight_label;    /* for C_SVC * /
        //    double* weight;        /* for C_SVC * /
        //    double nu;    /* for NU_SVC, ONE_CLASS, and NU_SVR * /
        //    double p;    /* for EPSILON_SVR * /
        //    int shrinking;    /* use the shrinking heuristics * /
        //    int probability; /* do probability estimates * /
        // };
        [StructLayout(LayoutKind.Sequential)]
        public struct ParametersStruct
        {
            public int svm_type;
            public int kernel_type;
            public int degree;      // for polynomial kernels
            public double gamma;    // for polynomial, rbf, sigmoid
            public double coef0;    // for polynomial and sigmoid

            // these are for training only
            public double cache_size;   // in Mega Bytes
            public double eps;          // stopping criteria
            public double C;            // for C_SVC, EPSILON_SVR and NU_SVR
            public int nr_weight;       // for C_SVC
            public IntPtr weight_label; // for C_SVC (int*)
            public IntPtr weight;       // for C_SVC (double*)
            public double nu;           // for NU_SVC, ONE_CLASS, and NU_SVR
            public double p;            // for EPSILON_SVR
            public int shrinking;       // use the shrinking heuristics
            public int probability;     // do probability estimates
        }

        //struct svm_model
        //{
        //    struct svm_parameter param;    /* parameter * /
        //    int nr_class;        /* number of classes, = 2 in regression/one class svm * /
        //    int l;            /* total #SV * /
        //    struct svm_feature **SV;        /* SVs (SV[l]) * /
        //    double **sv_coef;    /* coefficients for SVs in decision functions (sv_coef[k-1][l]) * /
        //    double *rho;        /* constants in decision functions (rho[k*(k-1)/2]) * /
        //    double *probA;        /* pairwise probability information * /
        //    double *probB;
        //    double *prob_density_marks;    /* probability information for ONE_CLASS * /
        //    int *sv_indices;        /* sv_indices[0,...,nSV-1] are values in [1,...,num_training_data] to indicate SVs in the training set * /
        //
        // /* for classification only * /
        //
        //    int *label;        /* label of each class (label[k]) * /
        //    int *nSV;          /* number of SVs for each class (nSV[k]) * /
        //                       /* nSV[0] + nSV[1] + ... + nSV[k-1] = l * /
        // /* XXX * /
        //    int free_sv;        /* 1 if svm_model is created by svm_load_model* /
        //                        /* 0 if svm_model is created by svm_train * /
        // };
        [StructLayout(LayoutKind.Sequential)]
        public struct ModelStruct
        {
            [MarshalAs(UnmanagedType.Struct, SizeConst = 96)]
            public ParametersStruct param;       // parameters
            public int nr_class;                // number of classes, = 2 in regression/one class svm
            public int l;                       // total number of support vectors
            public IntPtr SV;                   // support vectors (SV[l])
            public IntPtr sv_coef;              // coefficients for support vectors in decision functions (sv_coef[k-1][l]) (svm_feature**)
            public IntPtr rho;                  // constants in decision functions (rho[k*(k-1)/2])
            public IntPtr probA;                // pairwise probability information
            public IntPtr probB;            
            public IntPtr prob_density_marks;   // probability information for ONE_CLASS
            public IntPtr sv_indices;           // sv_indices[0,...,nSV-1] are values in [1,...,num_training_data] to indicate SVs in the training set
            
            // for classification only
            public IntPtr label;                // label of each class (label[k])
            public IntPtr nSV;                  // number of support vectors for each class (nSV[k]) where nSV[0] + nSV[1] + ... + nSV[k-1] = l
            
            public int free_sv; // 1 if svm_model is created by svm_load_model,
                                // 0 if svm_model is created by svm_train
        }


        //struct svm_model *svm_train(const struct svm_problem *prob, const struct svm_parameter *param);
        /// <param name="prob">svm_problem</param>
        /// <param name="param">svm_parameter</param>
        /// <returns>svm_model</returns>
        [DllImport("libsvm", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr svm_train(IntPtr prob, IntPtr param);

        //void svm_cross_validation(const struct svm_problem *prob, const struct svm_parameter *param, int nr_fold, double *target);
        /// <param name="prob">svm_problem</param>
        /// <param name="param">svm_parameter</param>
        /// <param name="nrFold">int</param>
        /// <param name="target">double[]</param>
        [DllImport("libsvm", CallingConvention = CallingConvention.Cdecl)]
        public static extern void svm_cross_validation(IntPtr prob, IntPtr param, int nrFold, IntPtr target);

        //int svm_save_model(const char *model_file_name, const struct svm_model *model);
        /// <param name="modelFileName">string</param>
        /// <param name="model">svm_model</param>
        /// <returns>bool</returns>
        [DllImport("libsvm", CallingConvention = CallingConvention.Cdecl)]
        public static extern int svm_save_model(IntPtr modelFileName, IntPtr model);

        //struct svm_model *svm_load_model(const char *model_file_name);
        /// <param name="modelFileName">string</param>
        /// <returns>svm_model</returns>
        [DllImport("libsvm", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr svm_load_model(IntPtr modelFileName);

        //int svm_get_svm_type(const struct svm_model *model);
        /// <param name="model">svm_model</param>
        /// <returns>int</returns>
        [DllImport("libsvm", CallingConvention = CallingConvention.Cdecl)]
        public static extern int svm_get_svm_type(IntPtr model);

        //int svm_get_nr_class(const struct svm_model *model);
        /// <param name="model">svm_model</param>
        /// <returns>int</returns>
        [DllImport("libsvm", CallingConvention = CallingConvention.Cdecl)]
        public static extern int svm_get_nr_class(IntPtr model);

        //void svm_get_labels(const struct svm_model *model, int *label);
        /// <param name="model">svm_model</param>
        /// <param name="label">int[]</param>
        [DllImport("libsvm", CallingConvention = CallingConvention.Cdecl)]
        public static extern void svm_get_labels(IntPtr model, IntPtr label);

        //void svm_get_sv_indices(const struct svm_model *model, int *sv_indices);
        /// <param name="model">svm_model</param>
        /// <param name="svIndices"></param>
        [DllImport("libsvm", CallingConvention = CallingConvention.Cdecl)]
        public static extern void svm_get_sv_indices(IntPtr model, IntPtr svIndices);

        //int svm_get_nr_sv(const struct svm_model *model);
        /// <param name="model">svm_model</param>
        /// <returns>int</returns>
        [DllImport("libsvm", CallingConvention = CallingConvention.Cdecl)]
        public static extern int svm_get_nr_sv(IntPtr model);

        //double svm_get_svr_probability(const struct svm_model *model);
        /// <param name="model">svm_model</param>
        /// <returns>double</returns>
        [DllImport("libsvm", CallingConvention = CallingConvention.Cdecl)]
        public static extern double svm_get_svr_probability(IntPtr model);

        //double svm_predict_values(const struct svm_model *model, const struct svm_feature *x, double* dec_values);
        /// <param name="model">svm_model</param>
        /// <param name="featureStructArrayPtr">svm_feature[]</param>
        /// <param name="decValues">double[]</param>
        /// <returns>double</returns>
        [DllImport("libsvm", CallingConvention = CallingConvention.Cdecl)]
        public static extern double svm_predict_values(IntPtr model, IntPtr featureStructArrayPtr, IntPtr decValues);

        //double svm_predict(const struct svm_model *model, const struct svm_feature *x);
        /// <param name="model">svm_model</param>
        /// <param name="featureStructArrayPtr"></param>
        /// <returns>double</returns>
        [DllImport("libsvm", CallingConvention = CallingConvention.Cdecl)]
        public static extern double svm_predict(IntPtr model, IntPtr featureStructArrayPtr);

        //double svm_predict_probability(const struct svm_model *model, const struct svm_feature *x, double* prob_estimates);
        /// <param name="model">svm_model</param>
        /// <param name="featureStructArrayPtr">svm_feature[]</param>
        /// <param name="probEstimates"></param>
        /// <returns>double</returns>
        [DllImport("libsvm", CallingConvention = CallingConvention.Cdecl)]
        public static extern double svm_predict_probability(IntPtr model, IntPtr featureStructArrayPtr, IntPtr probEstimates);

        //void svm_free_model_content(struct svm_model *model_ptr);
        /// <param name="modelPtr">svm_model</param>
        [DllImport("libsvm", CallingConvention = CallingConvention.Cdecl)]
        public static extern void svm_free_model_content(IntPtr modelPtr);

        //void svm_free_and_destroy_model(struct svm_model **model_ptr_ptr);
        /// <param name="modelPtrPtr">svm_model*</param>
        [DllImport("libsvm", CallingConvention = CallingConvention.Cdecl)]
        public static extern void svm_free_and_destroy_model(ref IntPtr modelPtrPtr);

        //void svm_destroy_param(struct svm_parameter *param);
        /// <param name="param">svm_parameter</param>
        [DllImport("libsvm", CallingConvention = CallingConvention.Cdecl)]
        public static extern void svm_destroy_param(IntPtr param);

        //const char *svm_check_parameter(const struct svm_problem *prob, const struct svm_parameter *param);
        /// <param name="prob">svm_problem</param>
        /// <param name="param">svm_parameter</param>
        /// <returns>string</returns>
        [DllImport("libsvm", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr svm_check_parameter(IntPtr prob, IntPtr param);

        //int svm_check_probability_model(const struct svm_model *model);
        /// <param name="model">svm_model</param>
        /// <returns>int</returns>
        [DllImport("libsvm", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool svm_check_probability_model(IntPtr model);

        //void svm_set_print_string_function(void (*print_func)(const char *));
        ///
        [DllImport("libsvm", CallingConvention = CallingConvention.Cdecl)]
        public static extern void svm_set_print_string_function(IntPtr printFunc);


        public static IntPtr AllocateParametersStruct(this SvmParameters parameters)
        {
            var y = new ParametersStruct
            {
                svm_type = (int)parameters.Type,
                kernel_type = (int)parameters.Kernel,
                degree = parameters.Degree,
                gamma = parameters.Gamma,
                coef0 = parameters.Coef0,
                cache_size = parameters.CacheSize,
                eps = parameters.Eps,
                C = parameters.C,
                nu = parameters.Nu,
                p = parameters.P,
                shrinking = parameters.Shrinking ? 1 : 0,
                probability = parameters.Probability ? 1 : 0,
                nr_weight = parameters.WeightLabels.Length,
                weight_label = IntPtr.Zero
            };

            if (y.nr_weight > 0)
            {
                y.weight_label = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)) * parameters.WeightLabels.Length);
                Marshal.Copy(parameters.WeightLabels, 0, y.weight_label, parameters.WeightLabels.Length);
            }

            y.weight = IntPtr.Zero;
            if (y.nr_weight > 0)
            {
                y.weight = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)) * parameters.Weights.Length);
                Marshal.Copy(parameters.Weights, 0, y.weight, parameters.Weights.Length);
            }

            var size = Marshal.SizeOf(y);
            var ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(y, ptr, true);

            return ptr;
        }
        
        public static IntPtr AllocateFeatureStructArray(this IReadOnlyList<double> featureVector)
        {
            var featureCount = featureVector.Count;
            var featureStructCount = featureCount + 1;
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(FeatureStruct)) * featureStructCount);
            var iPtrFeatures = ptr;

            for (var colIndex = 0; colIndex < featureCount; colIndex++)
            {
                var featureStruct = new FeatureStruct
                {
                    index = colIndex + 1,
                    value = featureVector[colIndex]
                };

                Marshal.StructureToPtr(featureStruct, iPtrFeatures, true);
                iPtrFeatures = IntPtr.Add(iPtrFeatures, Marshal.SizeOf(typeof(FeatureStruct)));
            }

            {
                var featureStruct = new FeatureStruct
                {
                    index = -1,
                    value = 0d
                };

                Marshal.StructureToPtr(featureStruct, iPtrFeatures, true);
                //iPtrFeatures = IntPtr.Add(iPtrFeatures, Marshal.SizeOf(typeof(FeatureStruct)));
            }

            return ptr;
        }

        public static IntPtr AllocateFeatureStructArray(this double[,] featureArray, int rowIndex)
        {
            var featureCount = featureArray.GetLength(1);
            var featureStructCount = featureCount + 1;
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(FeatureStruct)) * featureStructCount);
            var iPtrFeatures = ptr;

            for (var colIndex = 0; colIndex < featureCount; colIndex++)
            {
                var featureStruct = new FeatureStruct
                {
                    index = colIndex + 1,
                    value = featureArray[rowIndex, colIndex]
                };

                Marshal.StructureToPtr(featureStruct, iPtrFeatures, true);
                iPtrFeatures = IntPtr.Add(iPtrFeatures, Marshal.SizeOf(typeof(FeatureStruct)));
            }

            {
                var featureStruct = new FeatureStruct
                {
                    index = -1,
                    value = 0d
                };

                Marshal.StructureToPtr(featureStruct, iPtrFeatures, true);
                //iPtrFeatures = IntPtr.Add(iPtrFeatures, Marshal.SizeOf(typeof(FeatureStruct)));
            }

            return ptr;
        }

        public static IntPtr AllocateFeatureStructArray(this IReadOnlyList<SvmFeature> featureList)
        {
            var featureStructCount = featureList.Count + 1;
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(FeatureStruct)) * featureStructCount);
            var iPtrFeatures = ptr;

            foreach (var feature in featureList)
            {
                var featureStruct = new FeatureStruct
                {
                    index = feature.IndexPlus1,
                    value = feature.Value
                };

                Marshal.StructureToPtr(featureStruct, iPtrFeatures, true);
                iPtrFeatures = IntPtr.Add(iPtrFeatures, Marshal.SizeOf(typeof(FeatureStruct)));
            }

            {
                var featureStruct = new FeatureStruct
                {
                    index = -1,
                    value = 0d
                };

                Marshal.StructureToPtr(featureStruct, iPtrFeatures, true);
                //iPtrFeatures = IntPtr.Add(iPtrFeatures, Marshal.SizeOf(typeof(FeatureStruct)));
            }

            return ptr;
        }

        public static IntPtr AllocateDataSetStruct(this SvmDataSet dataSet)
        {
            if (dataSet.SampleCount < 1)
                return IntPtr.Zero;

            var y = new DataSetStruct
            {
                l = dataSet.SampleCount,

                // Allocate problem.y
                y = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)) * dataSet.OutputValueList.Count)
            };

            Marshal.Copy(dataSet.OutputValueList.ToArray(), 0, y.y, dataSet.OutputValueList.Count);

            // Allocate problem.x
            y.x = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)) * dataSet.InputFeatureList.Count);
            var iPtrX = y.x;
            foreach (var featureList in dataSet.InputFeatureList)
            {
                // Allocate feature array
                // Prepare each feature array 
                // 1) All features containing zero value is removed 
                // 2) A feature which index is -1 is added to the end
                var featureStructArrayPtr = 
                    featureList
                        .Where(a => a.Value != 0)
                        .ToArray()
                        .AllocateFeatureStructArray();

                Marshal.StructureToPtr(featureStructArrayPtr, iPtrX, true);

                iPtrX = IntPtr.Add(iPtrX, Marshal.SizeOf(typeof(IntPtr)));
            }

            // Allocate the problem
            var size = Marshal.SizeOf(y);
            var ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(y, ptr, true);

            return ptr;
        }
        
        public static IntPtr AllocateModelStruct(this SvmModel model)
        {
            if (model.ClassCount < 1 || model.Rho.Length < 1 || model.SupportVectorCoefs.Count < 1 || model.SupportVectorTotalCount < 1)
                return IntPtr.Zero;

            if (model.Parameter.Type != SvmTaskType.EpsilonSvr && model.Parameter.Type != SvmTaskType.NuSvr &&
                model.Parameter.Type != SvmTaskType.OneClass &&
                (model.Labels.Length < 1 || model.SupportVectorCounts.Length < 1))
                return IntPtr.Zero;

            var y = new ModelStruct
            {
                nr_class = model.ClassCount,
                l = model.SupportVectorTotalCount,
                free_sv = (int)model.Creation
            };

            // Allocate model.parameter
            var ptrParam = model.Parameter.AllocateParametersStruct();
            y.param = (ParametersStruct)Marshal.PtrToStructure(ptrParam, typeof(ParametersStruct))!;
            FreeParametersStruct(ptrParam);
            
            // Allocate model.rho
            y.rho = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)) * model.Rho.Length);
            Marshal.Copy(model.Rho, 0, y.rho, model.Rho.Length);

            // Allocate model.probA
            y.probA = IntPtr.Zero;
            if (model.ProbabilityA.Length > 0)
            {
                y.probA = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)) * model.ProbabilityA.Length);
                Marshal.Copy(model.ProbabilityA, 0, y.probA, model.ProbabilityA.Length);
            }

            // Allocate model.probB
            y.probB = IntPtr.Zero;
            if (model.ProbabilityB.Length > 0)
            {
                y.probB = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)) * model.ProbabilityB.Length);
                Marshal.Copy(model.ProbabilityB, 0, y.probB, model.ProbabilityB.Length);
            }

            if (model.Parameter.Type != SvmTaskType.EpsilonSvr && model.Parameter.Type != SvmTaskType.NuSvr &&
                model.Parameter.Type != SvmTaskType.OneClass)
            {
                // Allocate model.label
                y.label = Marshal.AllocHGlobal(Marshal.SizeOf(typeof (int))*model.Labels.Length);
                Marshal.Copy(model.Labels, 0, y.label, model.Labels.Length);

                // Allocate model.nSV
                y.nSV = Marshal.AllocHGlobal(Marshal.SizeOf(typeof (int))*model.SupportVectorCounts.Length);
                Marshal.Copy(model.SupportVectorCounts, 0, y.nSV, model.SupportVectorCounts.Length);
            }
            else
            {
                y.label = IntPtr.Zero;
                y.nSV = IntPtr.Zero;
            }

            // Allocate model.sv_coef
            y.sv_coef = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)) * model.SupportVectorCoefs.Count);
            var iPtrSvCoef = y.sv_coef;
            foreach (var coefArray in model.SupportVectorCoefs)
            {
                var temp = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)) * coefArray.Length);
                Marshal.Copy(coefArray, 0, temp, coefArray.Length);
                Marshal.StructureToPtr(temp, iPtrSvCoef, true);
                iPtrSvCoef = IntPtr.Add(iPtrSvCoef, Marshal.SizeOf(typeof(IntPtr)));
            }

            // Allocate model.sv_indices
            y.sv_indices = IntPtr.Zero;
            if (model.SupportVectorIndices.Length > 0)
            {
                y.sv_indices = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)) * model.SupportVectorIndices.Length);
                Marshal.Copy(model.SupportVectorIndices, 0, y.sv_indices, model.SupportVectorIndices.Length);
            }

            // Allocate model.SV
            y.SV = IntPtr.Zero;
            if (model.SupportVectorList.Count > 0)
            {
                y.SV = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)) * model.SupportVectorList.Count);
                var iPtrSv = y.SV;
                foreach (var supportVector in model.SupportVectorList)
                {
                    // Allocate feature array
                    // Prepare each feature array 
                    // 1) All features containing zero value is removed 
                    // 2) A feature which index is -1 is added to the end
                    var featureStructArrayPtr = 
                        supportVector
                            .Where(a => a.Value != 0)
                            .ToArray()
                            .AllocateFeatureStructArray();

                    Marshal.StructureToPtr(featureStructArrayPtr, iPtrSv, true);

                    iPtrSv = IntPtr.Add(iPtrSv, Marshal.SizeOf(typeof(IntPtr)));
                }
            }

            // Allocate the model
            var size = Marshal.SizeOf(y);
            var ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(y, ptr, true);

            return ptr;
        }


        public static void FreeParametersStruct(this ParametersStruct parameters)
        {
            Marshal.FreeHGlobal(parameters.weight);
            parameters.weight = IntPtr.Zero;

            Marshal.FreeHGlobal(parameters.weight_label);
            parameters.weight_label = IntPtr.Zero;
        }

        public static void FreeParametersStruct(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return;

            var parametersStruct = (ParametersStruct)Marshal.PtrToStructure(ptr, typeof(ParametersStruct))!;

            parametersStruct.FreeParametersStruct();

            Marshal.DestroyStructure(ptr, typeof(ParametersStruct));
            Marshal.FreeHGlobal(ptr);
        }

        public static void FreeFeatureStructArray(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return;

            Marshal.DestroyStructure(ptr, typeof(IntPtr));
            Marshal.FreeHGlobal(ptr);
        }

        public static void FreeDataSetStruct(this DataSetStruct dataSet)
        {
            Marshal.FreeHGlobal(dataSet.y);
            dataSet.y = IntPtr.Zero;

            var iPtrX = dataSet.x;
            for (var i = 0; i < dataSet.l; i++)
            {
                var ptrFeatures = (IntPtr)Marshal.PtrToStructure(iPtrX, typeof(IntPtr))!;
                
                FreeFeatureStructArray(ptrFeatures);

                iPtrX = IntPtr.Add(iPtrX, Marshal.SizeOf(typeof(IntPtr)));
            }

            Marshal.FreeHGlobal(dataSet.x);
            dataSet.x = IntPtr.Zero;
        }

        public static void FreeDataSetStruct(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return;

            var x = (DataSetStruct)Marshal.PtrToStructure(ptr, typeof(DataSetStruct))!;

            x.FreeDataSetStruct();

            Marshal.DestroyStructure(ptr, typeof(DataSetStruct));
            Marshal.FreeHGlobal(ptr);
        }

        public static void FreeModelStruct(this ModelStruct x)
        {
            Marshal.FreeHGlobal(x.rho);
            x.rho = IntPtr.Zero;

            Marshal.FreeHGlobal(x.probA);
            x.probA = IntPtr.Zero;

            Marshal.FreeHGlobal(x.probB);
            x.probB = IntPtr.Zero;

            Marshal.FreeHGlobal(x.sv_indices);
            x.sv_indices = IntPtr.Zero;

            Marshal.FreeHGlobal(x.label);
            x.label = IntPtr.Zero;

            Marshal.FreeHGlobal(x.nSV);
            x.nSV = IntPtr.Zero;

            x.param.FreeParametersStruct();

            var iPtrSv = x.SV;
            for (var i = 0; i < x.l; i++)
            {
                var ptrFeatures = (IntPtr)Marshal.PtrToStructure(iPtrSv, typeof(IntPtr))!;
                FreeFeatureStructArray(ptrFeatures);

                iPtrSv = IntPtr.Add(iPtrSv, Marshal.SizeOf(typeof(IntPtr)));
            }

            Marshal.FreeHGlobal(x.SV);
            x.SV = IntPtr.Zero;

            var iPtrSvCoef = x.sv_coef;
            for (var i = 0; i < x.nr_class - 1; i++)
            {
                var temp = (IntPtr)Marshal.PtrToStructure(iPtrSvCoef, typeof(IntPtr))!;
                Marshal.FreeHGlobal(temp);

                iPtrSvCoef = IntPtr.Add(iPtrSvCoef, Marshal.SizeOf(typeof(IntPtr)));
            }

            Marshal.FreeHGlobal(x.sv_coef);
            x.sv_coef = IntPtr.Zero;
        }

        public static void FreeModelStruct(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return;

            var x = (ModelStruct)Marshal.PtrToStructure(ptr, typeof(ModelStruct))!;

            x.FreeModelStruct();

            Marshal.DestroyStructure(ptr, typeof(ModelStruct));
            Marshal.FreeHGlobal(ptr);
        }
    }
}
