using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using CodeComposerLib.SyntaxTree;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Languages;
using TextComposerLib.Loggers.Progress;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Composers
{
    /// <summary>
    /// This abstract class can be used to implement a sub-process of macro-based computational code generation 
    /// using the main code library generator components and a macro binding component
    /// </summary>
    public sealed class GaFuLMetaContextCodeComposer : 
        IProgressReportSource
    {
        private static void GenerateDeclareIntermediateVariablesCode(GaFuLMetaContextCodeComposer contextCodeComposer)
        {
            var tempVarNames =
                contextCodeComposer
                    .Context.GetIntermediateVariables()
                    .Select(item => item.ExternalName)
                    .Distinct();

            //Add temp variables declaration code
            foreach (var tempVarName in tempVarNames)
                contextCodeComposer.SyntaxList.Add(
                    contextCodeComposer
                        .GeoLanguage
                        .SyntaxFactory
                        .DeclareLocalVariable(contextCodeComposer.GeoLanguage.ScalarTypeName, tempVarName)
                    );

            contextCodeComposer.SyntaxList.AddEmptyLine();
        }

        public static void DefaultGenerateCommentsBeforeComputations(GaFuLMetaContextCodeComposer contextCodeComposer)
        {
            contextCodeComposer.SyntaxList.Add(
                contextCodeComposer.GeoLanguage.SyntaxFactory.Comment(
                    "Begin GA-FuL MetaContext Code Generation, " + DateTime.Now.ToString("O")
                )
            );

            contextCodeComposer.SyntaxList.Add(
                contextCodeComposer.GeoLanguage.SyntaxFactory.Comment("MetaContext: " + contextCodeComposer.Context.ContextOptions.ContextName)
            );

            if (!contextCodeComposer.Context.ContextOptions.AllowGenerateComments)
                return;

            contextCodeComposer.SyntaxList.Add(
                contextCodeComposer.GeoLanguage.SyntaxFactory.Comment(
                    contextCodeComposer
                        .Context
                        .GetStatisticsReport()
                        .Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                    )
                );

            var commentTextLines = 
                contextCodeComposer
                    .Context.GetIndependentAtomics()
                    .Select(p =>
                        {
                            var s = new StringBuilder();

                            s.Append("   ").Append(p.InternalName);

                            if (p.IsNumber) 
                                return s.Append(" = constant: '").Append(p.RhsExpressionText).Append("'").ToString();

                            if (!contextCodeComposer.Context.TryGetParameterVariable(p.InternalName, out var paramVar))
                                return s.Append(" = constant: '0', the parameter binding was not found in the code block!!").ToString();

                            return (string.IsNullOrEmpty(paramVar.ExternalName))
                                ? s.ToString()
                                : s.Append(" = parameter: ").Append(paramVar.ExternalName).ToString();
                        })
                    .ToArray();

            contextCodeComposer.SyntaxList.Add(
                contextCodeComposer.GeoLanguage.SyntaxFactory.Comment()
            );

            contextCodeComposer.SyntaxList.Add(
                contextCodeComposer.GeoLanguage.SyntaxFactory.Comment("MetaContext Binding Data: ")
            );

            contextCodeComposer.SyntaxList.Add(contextCodeComposer.GeoLanguage.SyntaxFactory.Comment(commentTextLines));

            contextCodeComposer.SyntaxList.AddEmptyLine();
        }

        public static void DefaultGenerateCommentsAfterComputations(GaFuLMetaContextCodeComposer contextCodeComposer)
        {
            contextCodeComposer.SyntaxList.Add(
                contextCodeComposer.GeoLanguage.SyntaxFactory.Comment(
                    "Finish GA-FuL MetaContext Code Generation, " + DateTime.Now.ToString("O")
                    )
                );
        }

        public static bool DefaultActionBeforeGenerateComputations(GaFuLMetaContextCodeComposer contextCodeComposer)
        {
            DefaultGenerateCommentsBeforeComputations(contextCodeComposer);

            //GenerateDeclareTempsCode(contextCodeComposer);

            return true;
        }

        public static void DefaultActionAfterGenerateComputations(GaFuLMetaContextCodeComposer contextCodeComposer)
        {
            DefaultGenerateCommentsAfterComputations(contextCodeComposer);
        }
        

        public GaFuLLanguageServerBase GeoLanguage { get; }

        /// <summary>
        /// The expression converter object used in this class
        /// </summary>
        public MetaExpressionToLanguageConverterBase ExpressionConverter 
            => GeoLanguage.ExpressionConverter;

        public string ProgressSourceId 
            => "MetaContext Code Composer";

        public ProgressComposer Progress { get; set; }

        /// <summary>
        /// The text composer object where all generated macro code is written
        /// </summary>
        public SteSyntaxElementsList SyntaxList { get; }

        /// <summary>
        /// The base macro used for code generation
        /// </summary>
        public MetaContext Context { get; private set; }


        public GaFuLMetaContextCodeComposerOptions ComposerOptions { get; }
            = new GaFuLMetaContextCodeComposerOptions();


        internal GaFuLMetaContextCodeComposer([NotNull] GaFuLLanguageServerBase languageServer, [NotNull] MetaContext context)
        {
            GeoLanguage = languageServer;
            SyntaxList = new SteSyntaxElementsList();
            Context = context;
        }

        internal GaFuLMetaContextCodeComposer([NotNull] GaFuLLanguageServerBase languageServer, [NotNull] MetaContext context, [NotNull] GaFuLMetaContextCodeComposerOptions options)
            : this(languageServer, context)
        {
            ComposerOptions.SetOptions(options);
        }


        /// <summary>
        /// Used to replace the context by another one. This clears the internal code composer
        /// </summary>
        /// <param name="context"></param>
        public void SetContext([NotNull] MetaContext context)
        {
            SyntaxList.Clear();

            Context = context;
        }

        /// <summary>
        /// Generate the code for a single computation
        /// </summary>
        /// <param name="codeInfo"></param>
        public void GenerateSingleComputationCode(GaFuLMetaContextComputationCodeInfo codeInfo)
        {
            //Generate comment to show symbolic form for this computation
            if (ComposerOptions.AllowGenerateComputationComments)
            {
                var assignmentText =
                    $"{codeInfo.ComputedVariable.InternalName} = {codeInfo.ComputedVariable.RhsExpressionText}";

                SyntaxList.Add(
                    GeoLanguage
                        .SyntaxFactory
                        .Comment(assignmentText)
                );
            }

            //Generate assignment statement for this computation
            var code =
                codeInfo.ComputedVariable.IsIntermediateVariable && !codeInfo.ComputedVariable.IsReused
                ? (ISyntaxTreeElement)GeoLanguage
                    .SyntaxFactory
                    .DeclareLocalVariable("var", codeInfo.ExternalName, codeInfo.RhsSimpleTextExpression)
                : GeoLanguage
                    .SyntaxFactory
                    .AssignToLocalVariable(codeInfo.ExternalName, codeInfo.RhsSimpleTextExpression);

            SyntaxList.Add(code);

            //Add an empty line
            if (ComposerOptions.AllowGenerateComputationComments || codeInfo.ComputedVariable.IsOutputVariable)
                SyntaxList.Add(GeoLanguage.SyntaxFactory.EmptyLine());
        }

        /// <summary>
        /// Generate the low-level assignments code from the low-level optimized macro code
        /// </summary>
        private void GenerateProcessingCode()
        {
            //Iterate over optimized low-level computations
            foreach (var computedVar in Context.GetComputedVariables())
            {
                //Convert the rhs text expression tree into target language code
                var rhsExprCode = 
                    ExpressionConverter.Convert(
                        computedVar.RhsExpression
                    );

                //Create the codeInfo object
                var codeInfo = new GaFuLMetaContextComputationCodeInfo()
                {
                    ComputedVariable = computedVar,
                    RhsSimpleTextExpression = rhsExprCode,
                    LanguageServer = GeoLanguage,
                    EnableCodeGeneration = true
                };

                //Generate the assignment target code based on the codeInfo object
                //Execute this action before generating computation code
                ComposerOptions.ActionBeforeGenerateSingleComputation?.Invoke(SyntaxList, codeInfo);

                //If the action prevented generation of code don't generating computation code
                if (codeInfo.EnableCodeGeneration)
                    GenerateSingleComputationCode(codeInfo);

                //Execute this action after generating computation code
                ComposerOptions.ActionAfterGenerateSingleComputation?.Invoke(SyntaxList, codeInfo);
            }

            //SyntaxList.AddEmptyLine();
        }

        /// <summary>
        /// Generate optimized macro code in the target language given a list of macro parameters bindings
        /// </summary>
        /// <returns></returns>
        public string Generate()
        {
            //Initialize components of macro code generator
            SyntaxList.Clear();

            if (Context.ContextOptions.AllowGenerateCode == false) 
                return string.Empty;

            var progressId = this.ReportStart(
                "Generating MetaContext Code For: " + Context.ContextOptions.ContextName
            );

            try
            {
                this.ReportNormal(
                    "MetaContext Binding Ready", 
                    Context.ContextOptions.ToString()
                );
                
                //Generate code before computations for comments, temp declarations, and the like
                var result =
                    ComposerOptions.ActionBeforeGenerateComputations?.Invoke(this) 
                    ?? DefaultActionBeforeGenerateComputations(this);

                //Generate computations code if allowed by last action result
                if (result) 
                    GenerateProcessingCode();

                //Generate code after computations for comments, temp destruction, and the like
                if (ComposerOptions.ActionAfterGenerateComputations == null)
                    DefaultActionAfterGenerateComputations(this);
                else
                    ComposerOptions.ActionAfterGenerateComputations(this);
            }
            catch (Exception e)
            {
                this.ReportError(e);
            }

            //Clean everything up and return final generated code

            //Un-parse the SyntaxList into the final code
            var codeText = 
                GeoLanguage.CodeGenerator.GenerateCode(SyntaxList);

            this.ReportFinish(progressId, codeText);

            return codeText;
        }
    }
}
