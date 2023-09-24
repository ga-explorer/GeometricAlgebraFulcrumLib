using System.Text;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Languages;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Composers
{
    public abstract class GaFuLSingleMetaContextCodeComposerBase : 
        GaFuLCodeLibraryComposerBase
    {
        public MetaContext Context { get; protected set; }

        public override string Name 
            => "Single MetaContext Composer";

        public override string Description 
            => "Generates code for a single MetaContext.";

        /// <summary>
        /// Enable the generation of an Excel file to visualize and perform
        /// the computations made in the macro under the given macro binding
        /// </summary>
        public bool GenerateExcel { get; set; } = true;

        public bool AllowGenerateMetaContextCode { get; set; } = true;

        protected GaFuLSingleMetaContextCodeComposerBase(MetaContext context, GaFuLLanguageServerBase languageServer)
            : base(languageServer)
        {
            Context = context;
        }


        protected void GenerateTypeName()
        {
            ActiveFileTextComposer.Append(Context.ContextOptions.ScalarTypeName);
        }

        //protected abstract void GenerateTypeDefaultValue(AstType typeInfo);

        protected void GenerateInputsCode(MetaContext context)
        {
            var flag = false;
            foreach (var paramInfo in context.GetParameterVariables())
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

        protected GaFuLMetaContextCodeComposer InitMetaContextCodeComposer()
        {
            var macroComposer = CreateMetaContextCodeComposer(Context);

            //macroComposer.ActionSetMetaContextParametersBindings =
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

        protected abstract void GenerateMetaContextCode();

        protected override void ComposeTextFiles()
        {
            CodeFilesComposer.InitializeFile(Context.ContextOptions.ContextName + "." + Language.DefaultFileExtension);

            GenerateMetaContextCode();

            CodeFilesComposer.UnselectActiveFile();
        }
    }
}