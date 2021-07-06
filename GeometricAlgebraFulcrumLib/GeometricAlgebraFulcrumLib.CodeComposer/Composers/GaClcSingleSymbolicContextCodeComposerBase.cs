using System.Diagnostics.CodeAnalysis;
using System.Text;
using GeometricAlgebraFulcrumLib.CodeComposer.LanguageServers;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Composers
{
    public abstract class GaClcSingleSymbolicContextCodeComposerBase : 
        GaCodeLibraryComposerBase
    {
        public SymbolicContext Context { get; protected set; }

        public override string Name 
            => "Single SymbolicContext Composer";

        public override string Description 
            => "Generates code for a single GaClc macro.";

        /// <summary>
        /// Enable the generation of an Excel file to visualize and perform
        /// the computations made in the macro under the given macro binding
        /// </summary>
        public bool GenerateExcel { get; set; } = true;

        public bool AllowGenerateSymbolicContextCode { get; set; } = true;

        protected GaClcSingleSymbolicContextCodeComposerBase([NotNull] SymbolicContext context, GaClcLanguageServer languageServer)
            : base(languageServer)
        {
            Context = context;
            //SymbolicContextGenDefaults = new GaClcSymbolicContextCodeComposerDefaults(this);
        }


        protected void GenerateTypeName()
        {
            ActiveFileTextComposer.Append(Context.ContextOptions.ScalarTypeName);
        }

        //protected abstract void GenerateTypeDefaultValue(AstType typeInfo);

        protected void GenerateSymbolicContextInputsCode(SymbolicContext macroInfo)
        {
            var flag = false;
            foreach (var paramInfo in macroInfo.ParameterVariables)
            {
                if (flag)
                    ActiveFileTextComposer.Append(", ");
                else
                    flag = true;

                ActiveFileTextComposer.Append(Context.ContextOptions.ScalarTypeName);

                ActiveFileTextComposer.Append(" ");

                ActiveFileTextComposer.Append(paramInfo.ExternalName);
            }
        }

        protected override bool VerifyReadyToGenerate()
        {
            return true;
        }

        protected override bool InitializeTemplates()
        {
            return true;
        }

        protected override void InitializeSubComponents()
        {
        }

        protected override void FinalizeSubComponents()
        {
        }

        protected virtual string BasisBladeIdToCode(string parentName, int id)
        {
            return
                new StringBuilder()
                    .Append(parentName)
                    .Append(".Coef[")
                    .Append(id)
                    .Append("]")
                    .ToString();
        }

        protected GaClcSymbolicContextCodeComposer InitSymbolicContextCodeComposer()
        {
            var macroComposer = CreateSymbolicContextCodeComposer(Context);

            //macroComposer.ActionSetSymbolicContextParametersBindings =
            //    macroGenBinding =>
            //    {
            //        foreach (var paramBinding in macroBinding.Bindings)
            //            if (paramBinding.IsVariable)
            //                macroGenBinding.BindToVariables(
            //                    paramBinding.ValueAccess,
            //                    paramBinding.FinalRhsScalarValue
            //                );
            //            else
            //                macroGenBinding.BindScalarToConstant(
            //                    paramBinding.ValueAccess, 
            //                    paramBinding.ConstantExpr
            //                );
            //    };

            //macroComposer.ActionSetTargetVariablesNames = 
            //    SetTargetNaming;

            return macroComposer;
        }

        protected abstract void GenerateSymbolicContextCode();

        protected override void ComposeTextFiles()
        {
            CodeFilesComposer.InitalizeFile(Context.ContextOptions.ContextName + "." + Language.DefaultFileExtension);

            GenerateSymbolicContextCode();

            CodeFilesComposer.UnselectActiveFile();
        }
    }
}