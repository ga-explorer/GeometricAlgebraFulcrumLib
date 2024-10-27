using System.Runtime.InteropServices;


namespace GeometricAlgebraFulcrumLib.Optimization.SVM.LibLinear
{
    /*
       struct model* train(const struct problem *prob, const struct parameter *param);
       void cross_validation(const struct problem *prob, const struct parameter *param, int nr_fold, double *target);
       void find_parameters(const struct problem *prob, const struct parameter *param, int nr_fold, double start_C, double start_p, double *best_C, double *best_p, double *best_score);
       
       double predict_values(const struct model *model_, const struct feature_node *x, double* dec_values);
       double predict(const struct model *model_, const struct feature_node *x);
       double predict_probability(const struct model *model_, const struct feature_node *x, double* prob_estimates);
       
       int save_model(const char *model_file_name, const struct model *model_);
       struct model *load_model(const char *model_file_name);
       
       int get_nr_feature(const struct model *model_);
       int get_nr_class(const struct model *model_);
       void get_labels(const struct model *model_, int* label);
       double get_decfun_coef(const struct model *model_, int feat_idx, int label_idx);
       double get_decfun_bias(const struct model *model_, int label_idx);
       double get_decfun_rho(const struct model *model_);
       
       void free_model_content(struct model *model_ptr);
       void free_and_destroy_model(struct model **model_ptr_ptr);
       void destroy_param(struct parameter *param);
       
       const char *check_parameter(const struct problem *prob, const struct parameter *param);
       int check_probability_model(const struct model *model);
       int check_regression_model(const struct model *model);
       int check_oneclass_model(const struct model *model);
       void set_print_string_function(void (*print_func) (const char*));
    */

    public static class LibLinearInterop
    {
        public static string Version => "2.47";

        //enum { L2R_LR, L2R_L2LOSS_SVC_DUAL, L2R_L2LOSS_SVC, L2R_L1LOSS_SVC_DUAL, MCSVM_CS, L1R_L2LOSS_SVC, L1R_LR, L2R_LR_DUAL, L2R_L2LOSS_SVR = 11, L2R_L2LOSS_SVR_DUAL, L2R_L1LOSS_SVR_DUAL, ONECLASS_SVM = 21 }; /* solver_type * /
        public enum SolverType : int
        {
            L2R_LR = 0,
            L2R_L2LOSS_SVC_DUAL = 1,
            L2R_L2LOSS_SVC = 2,
            L2R_L1LOSS_SVC_DUAL = 3, 
            MCSVM_CS = 4, 
            L1R_L2LOSS_SVC = 5, 
            L1R_LR = 6, 
            L2R_LR_DUAL = 7, 
            L2R_L2LOSS_SVR = 11, 
            L2R_L2LOSS_SVR_DUAL = 12, 
            L2R_L1LOSS_SVR_DUAL = 13, 
            ONECLASS_SVM = 21
        }


        //struct feature_node
        //{
        //    int index;
        //    double value;
        //};
        [StructLayout(LayoutKind.Sequential)]
        public struct feature_node
        {
            internal int index;
            internal double value;
        }

        //struct problem
        // {
        //  int l, n;
        //  double *y;
        //  struct feature_node **x;
        //  double bias;            /* < 0 if no bias term * /
        // };
        [StructLayout(LayoutKind.Sequential)]
        public struct problem
        {
            public int l;
            public int n;
            public IntPtr y; // double*
            public IntPtr x; // feature_node**
            public double bias;
        }

        //struct parameter
        //{
        //    int solver_type;
        // 
        //    /* these are for training only * /
        //    double eps;             /* stopping tolerance * /
        //    double C;
        //    int nr_weight;
        //    int *weight_label;
        //    double* weight;
        //    double p;
        //    double nu;
        //    double *init_sol;
        //    int regularize_bias;
        //};
        [StructLayout(LayoutKind.Sequential)]
        public struct parameter
        {
            public int solver_type;

            public double eps;
            public double C;
            public int nr_weight;
            public IntPtr weight_label; // int*
            public IntPtr weight; // double*
            public double p;
            public double nu;
            public IntPtr init_sol; // double*
            public int regularize_bias;
        }

        //struct model
        //{
        //    struct parameter param;
        //    int nr_class;           /* number of classes * /
        //    int nr_feature;
        //    double *w;
        //    int *label;             /* label of each class * /
        //    double bias;
        //    double rho;             /* one-class SVM only * /
        //};
        [StructLayout(LayoutKind.Sequential)]
        public struct model
        {
            [MarshalAs(UnmanagedType.Struct, SizeConst = 96)]
            public parameter param;
            public int nr_class;
            public int nr_feature;
            public IntPtr w; // double*
            public IntPtr label; // double*
            public double bias;
            public double rho;
        }
    }
}
