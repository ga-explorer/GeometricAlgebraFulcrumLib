using System;
using System.Diagnostics.CodeAnalysis;
using CodeComposerLib.SyntaxTree;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Composers
{
    public sealed class GaSymbolicContextCodeComposerOptions
    {
        /// <summary>
        /// If false, the comments before each computational line are not generated
        /// </summary>
        public bool AllowGenerateComputationComments { get; set; } = false;

        /// <summary>
        /// This is executed before generating computation code. It can be used to add comments, declare temp 
        /// variables in the target code or any other similar purpose.
        /// </summary>
        public Func<GaSymbolicContextCodeComposer, bool> ActionBeforeGenerateComputations { get; set; }

        /// <summary>
        /// This is executed after generating computation code. It can be used to add comments, destruct temp
        /// variables in the target code or or any other similar purpose.
        /// </summary>
        public Action<GaSymbolicContextCodeComposer> ActionAfterGenerateComputations { get; set; }

        /// <summary>
        /// This is executed each time before a computation code is generated. It can be used to inject code
        /// in the final generated code or to prevent code generation of this line by returning false
        /// </summary>
        public Action<SteSyntaxElementsList, GaSymbolicContextComputationCodeInfo> ActionBeforeGenerateSingleComputation { get; set; }

        /// <summary>
        /// This is executed each time after a computation code is generated. It can be used to inject code
        /// in the final generated code
        /// </summary>
        public Action<SteSyntaxElementsList, GaSymbolicContextComputationCodeInfo> ActionAfterGenerateSingleComputation { get; set; }


        public GaSymbolicContextCodeComposerOptions()
        {
        }

        public GaSymbolicContextCodeComposerOptions(GaSymbolicContextCodeComposerOptions options)
        {
            SetOptions(options);
        }


        public void SetOptions([NotNull] GaSymbolicContextCodeComposerOptions options)
        {
            AllowGenerateComputationComments = options.AllowGenerateComputationComments;
            ActionBeforeGenerateComputations = options.ActionBeforeGenerateComputations;
            ActionAfterGenerateComputations = options.ActionAfterGenerateComputations;
            ActionBeforeGenerateSingleComputation = options.ActionBeforeGenerateSingleComputation;
            ActionAfterGenerateSingleComputation = options.ActionAfterGenerateSingleComputation;
        }
    }
}