using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.Variables;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context.Optimizer;

namespace GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context
{
    public sealed class SymbolicContextOptions
    {
        public string ContextName { get; set; } 
            = string.Empty;

        public string ScalarTypeName { get; set; }
            = "double";

        /// <summary>
        /// Determines the action that should be taken when trying to bind a macro output parameter to a constant
        /// </summary>
        public SymbolicExpressionBindOutputToConstantBehavior BindOutputToConstantBehavior { get; set; }
            = SymbolicExpressionBindOutputToConstantBehavior.BindToVariable;

        /// <summary>
        /// If true, any metric products on non-orthogonal derived frames' multivectors are replaced by equivalent
        /// orthogonal operations using derived-to-base and base-to-derived outermorphisms on the derived frames
        /// When this flag is used better compilation time is achieved but more computations are generated.
        /// 
        /// This flag is used during AST basic expressions generation.
        /// </summary>
        public bool ForceOrthogonalMetricProducts { get; set; }
            = true;

        /// <summary>
        /// When True the low-level optimizer attempts to extract all common sub-expressions in all rhs values
        /// and refactor the sub-expressions as temporary variables. This may take longer time during 
        /// low level optimization.
        /// 
        /// This flag is used during low-level optimization of a macro's code
        /// </summary>
        public bool ReduceLowLevelRhsSubExpressions { get; set; }
            = true;

        /// <summary>
        /// When True the low-level generator uses Mathematica's Simplify[] function on all rhs values before assigning
        /// them to lhs temp or output variables.
        /// 
        /// This flag us used during low-level generation of a macro's code
        /// </summary>
        public bool SimplifyLowLevelRhsValues { get; set; }
            = true;

        /// <summary>
        /// During low-level intermediate code generation this option selects a method for using the item in the
        /// RHS of any following items. If item A dependent on item B in its RHS value (for example 'A = 3 * B') there are 3 cases:
        /// 
        /// 1- Propagate constants only: If B is assigned a constant value (for example 'B = 5') then propagate the value 
        /// of B not the symbol B (A = 15) hence B is not required anymore. Else propagate the symbol B ('A = 3 * B' as is)
        /// 
        /// 2- Propagate constants and single variables: If B is a constant or is assigned a single variable (for example 'B = C')
        /// propagate the RHS assigned to B (i.e. A = 3 * C) hence B is not required anymore. Else propagate the symbol B ('A = 3 * B' as is)
        /// 
        /// 3- Propagate constants and expressions depending on a single variable: If B is a constant or is assigned 
        /// an expression depending on a single variable (for example 'B = C + Power[C, 2]') propagate the RHS assigned to B 
        /// (i.e. A = 3 * C + 3 * Power[C, 2]) hence B is not required anymore. Else propagate the symbol B ('A = 3 * B' as is)
        /// </summary>
        public ScOptAtomicExpressionsPropagationKind LowLevelPropagationMethod { get; set; }
            = ScOptAtomicExpressionsPropagationKind.PropagateSingleVariableDependent;

        /// <summary>
        /// If true, no optimization by re-ordering of output variables computation is performed
        /// </summary>
        public bool FixOutputComputationsOrder { get; set; } 
            = false;

        public bool EnableTestEvaluation { get; set; } 
            = false;

        public bool AllowGenerateCode { get; set; }
            = true;


        public SymbolicContextOptions()
        {
        }

        public SymbolicContextOptions(SymbolicContextOptions options)
        {
            SetOptions(options);
        }


        public void SetOptions([NotNull] SymbolicContextOptions options)
        {
            ScalarTypeName = options.ScalarTypeName;
            BindOutputToConstantBehavior = options.BindOutputToConstantBehavior;
            ReduceLowLevelRhsSubExpressions = options.ReduceLowLevelRhsSubExpressions;
            SimplifyLowLevelRhsValues = options.SimplifyLowLevelRhsValues;
            LowLevelPropagationMethod = options.LowLevelPropagationMethod;
            FixOutputComputationsOrder = options.FixOutputComputationsOrder;
            ForceOrthogonalMetricProducts = options.ForceOrthogonalMetricProducts;
            EnableTestEvaluation = options.EnableTestEvaluation;
            AllowGenerateCode = AllowGenerateCode;
        }
    }
}