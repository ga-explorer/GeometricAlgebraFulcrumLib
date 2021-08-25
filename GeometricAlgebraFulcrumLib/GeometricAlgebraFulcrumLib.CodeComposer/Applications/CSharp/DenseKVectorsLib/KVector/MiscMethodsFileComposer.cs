using System;
using System.Linq;
using CodeComposerLib.SyntaxTree;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.CodeComposer.Composers;
using GeometricAlgebraFulcrumLib.CodeComposer.Languages;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Storage.Utils;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.KVector
{
    internal sealed class MiscMethodsFileComposer : 
        GaLibraryFileComposerBase
    {
        internal MiscMethodsFileComposer(GaLibraryComposer libGen)
            : base(libGen)
        {
        }

        //private GaClcInfoSymbolicContext AddEuclideanDualGaClcSymbolicContext()
        //{
        //    var codeText =
        //        Templates["edual_macro"].GenerateUsing(CurrentNamespace);

        //    var gmacSymbolicContext =
        //        _tempSymbolsCompiler.CompileSymbolicContext(
        //            codeText,
        //            _currentFrame.AssociatedFrame.ChildScope
        //            );

        //    return new GaClcInfoSymbolicContext(gmacSymbolicContext);
        //}

        private void GenerateEuclideanDualFunction(uint inGrade)
        {
            var outGrade = VSpaceDimension - inGrade;

            var context = new SymbolicContext(
                DenseKVectorsLibraryComposer.DefaultContextOptions
            );

            var inputKVector = 
                context.ParameterVariablesFactory.CreateDenseKVector(
                    VSpaceDimension,
                    inGrade,
                    index => $"inputKVectorScalar{index}"
                );

            var outputKVector = 
                Processor
                    .EDual(inputKVector, VSpaceDimension)
                    .GetKVectorPart(outGrade);

            outputKVector.SetIsOutput(true);

            context.OptimizeContext();
            
            context.SetExternalNamesByTermIndex(
                inputKVector,
                index => $"scalars[{index}]"
            );
            
            context.SetExternalNamesByTermIndex(
                outputKVector,
                index => $"c[{index}]"
            );

            context.SetIntermediateExternalNamesByNameIndex(
                DenseKVectorsLibraryComposer.MaxTargetLocalVars,
                index => $"tempVar{index:X4}",
                index => $"tempArray[{index}]"
            );

            var macroComposer = new GaSymbolicContextCodeComposer(
                DenseKVectorsLibraryComposer.GaLanguage, 
                context,
                DenseKVectorsLibraryComposer.DefaultContextCodeComposerOptions
            );
            
            //Generate code from macro binding
            var computationsText = macroComposer.Generate();

            TextComposer.Append(
                Templates["edual"],
                "double", GaLanguage.ScalarTypeName,
                "grade", inGrade,
                "num", Processor.KvSpaceDimension(inGrade),
                "computations", computationsText
            );
        }

        private void GenerateMiscFunctions(ulong kvSpaceDim)
        {
            //This code can be replaced using ListTextBuilderCollection and ParametricTextBuilderCollection
            //objects. See GenerateMainMiscFunctions() in this file for an example
            var addCasesTemplate = Templates["add_case"];
            var subtCasesTemplate = Templates["subt_case"];
            var timesCasesTemplate = Templates["times_case"];

            var addCasesText = new ListTextComposer("," + Environment.NewLine);
            var subtCasesText = new ListTextComposer("," + Environment.NewLine);
            var timesCasesText = new ListTextComposer("," + Environment.NewLine);

            for (var index = 0UL; index < kvSpaceDim; index++)
            {
                addCasesText.Add(addCasesTemplate, "index", index);
                subtCasesText.Add(subtCasesTemplate, "index", index);
                timesCasesText.Add(timesCasesTemplate, "index", index);
            }


            var miscFuncsTemplate = Templates["misc"];

            TextComposer.Append(miscFuncsTemplate,
                "double", GaLanguage.ScalarTypeName,
                "num", kvSpaceDim,
                "addcases", addCasesText,
                "subtcases", subtCasesText,
                "timescases", timesCasesText
            );
        }


        private void TestSelfDpGradeFunctionComputationCondition(SteSyntaxElementsList textBuilder, GaSymbolicContextComputationCodeInfo compInfo)
        {
            if (compInfo.ComputedVariable.RhsExpression.ToString() == "0")
            {
                compInfo.EnableCodeGeneration = false;
                //return;
            }

            ////Prevent generation of processing code if output grade equals 0 because this is the default grade
            ////returned by the function
            //var valueAccess = ((TlOutputVariable)compInfo.ComputedVariable).AssociatedValueAccess;

            //var id = ((ValueAccessStepByKey<int>)valueAccess.LastAccessStep).AccessKey;

            //var grade = GaUtils.ID_To_Grade(id);

            //if (grade == 0)
            //    compInfo.EnableCodeGeneration = false;
        }

        private static void AddSelfDpGradeFunctionComputationCondition(SteSyntaxElementsList textBuilder, GaSymbolicContextComputationCodeInfo compInfo)
        {
            if (compInfo.ComputedVariable.IsOutputVariable == false || compInfo.EnableCodeGeneration == false)
                return;

            //var basisBlade = 
            //    (compInfo.ComputedVariable).ValueAccess.GetBasisBlade();

            //var grade = basisBlade.Grade;
            var grade = 0; //TODO: Fix this

            textBuilder.AddFixedCode($"if (c <= -Epsilon || c >= Epsilon) return {grade};");
            textBuilder.AddEmptyLines(2);
        }

        private void GenerateSelfDpGradeFunction(uint inGrade)
        {
            var context = new SymbolicContext(
                DenseKVectorsLibraryComposer.DefaultContextOptions
            );

            //var outGradesList =
            //    Processor
            //        .BasisSet
            //        .GradesOfEGp(inGrade, inGrade)
            //        .Where(grade => grade > 0)
            //        .OrderByDescending(g => g);

            var inputKVector =
                context
                    .ParameterVariablesFactory
                    .CreateDenseKVector(
                        VSpaceDimension,
                        inGrade,
                        index => $"inputKVectorScalar{index}"
                    );

            var outputKVector = 
                Processor.EGp(inputKVector);

            Processor
                .GetNotZeroTerms(outputKVector)
                .Select(t => t.Scalar)
                .SetIsOutput(true);

            context.ContextOptions.FixOutputComputationsOrder = true;

            context.OptimizeContext();
            
            context.SetExternalNamesByTermIndex(
                inputKVector,
                index => $"scalars[{index}]"
            );
            
            context.SetExternalNamesByTermId(
                outputKVector,
                id => "c"
            );

            context.SetIntermediateExternalNamesByNameIndex(
                DenseKVectorsLibraryComposer.MaxTargetLocalVars,
                index => $"tempVar{index:X4}",
                index => $"tempArray[{index}]"
            );

            var macroComposer = new GaSymbolicContextCodeComposer(
                DenseKVectorsLibraryComposer.GaLanguage, 
                context,
                DenseKVectorsLibraryComposer.DefaultContextCodeComposerOptions
            );

            macroComposer.ComposerOptions.ActionBeforeGenerateSingleComputation =
                TestSelfDpGradeFunctionComputationCondition;

            macroComposer.ComposerOptions.ActionAfterGenerateSingleComputation =
                AddSelfDpGradeFunctionComputationCondition;

            //Generate code from macro binding
            var computationsText = macroComposer.Generate();

            TextComposer.Append(
                Templates["self_dp_grade"],
                "grade", inGrade,
                "double", GaLanguage.ScalarTypeName,
                "computations", computationsText
            );
        }

        private void GenerateMainSelfDpGradeFunction()
        {
            if (VSpaceDimension <= 3)
            {
                TextComposer.Append("public int SelfDPGrade() { return 0; }");

                return;
            }

            var selfDpGradeCasesText = new ListTextComposer(Environment.NewLine);

            for (var grade = 2; grade < VSpaceDimension - 1; grade++)
                selfDpGradeCasesText.Add(
                    Templates["main_self_dp_grade_case"].GenerateUsing(grade)
                );

            TextComposer.Append(
                Templates["main_self_dp_grade"],
                "signature", CurrentNamespace,
                "main_self_dp_grade_cases", selfDpGradeCasesText.ToString()
            );
        }

        private void GenerateMainMiscFunctions()
        {
            var miscCasesTemplates =
                Templates.SubCollection(
                    "main_add_case",
                    "main_subt_case",
                    "main_times_case",
                    "main_divide_case",
                    "main_inverse_case",
                    "main_edual_case"
                );

            var miscCasesText = new ListComposerCollection(
                    "main_add_case",
                    "main_subt_case",
                    "main_times_case",
                    "main_divide_case",
                    "main_inverse_case",
                    "main_edual_case"
                );

            miscCasesText.SetSeparator(Environment.NewLine);

            foreach (var grade in Processor.Grades)
            {
                miscCasesText.AddTextItems(miscCasesTemplates,
                    "signature", CurrentNamespace,
                    "grade", grade,
                    "num", Processor.KvSpaceDimension(grade),
                    "sign", grade.GradeHasNegativeReverse() ? "-" : "",
                    "invgrade", VSpaceDimension - grade
                    );
            }

            var mainFuncsTemplate = Templates["misc_main"];

            mainFuncsTemplate.SetParametersValues(miscCasesText);

            TextComposer.Append(
                mainFuncsTemplate,
                "signature", CurrentNamespace,
                "double", GaLanguage.ScalarTypeName,
                "norm2_opname", GaLanguageOperationKind.UnaryNormSquared.GetName(false),
                "emag2_opname", GaLanguageOperationKind.UnaryNormSquared.GetName(true)
            );

            GenerateMainSelfDpGradeFunction();
        }

        public override void Generate()
        {
            GenerateKVectorFileStartCode();

            var kvSpaceDimList =
                VSpaceDimension
                    .GetRange()
                    .Select(grade => Processor.KvSpaceDimension(grade))
                    .Distinct();

            foreach (var kvSpaceDim in kvSpaceDimList)
                GenerateMiscFunctions(kvSpaceDim);

            foreach (var inGrade in Processor.Grades)
                GenerateEuclideanDualFunction(inGrade);

            for (var inGrade = 2U; inGrade < VSpaceDimension - 1; inGrade++)
                GenerateSelfDpGradeFunction(inGrade);

            GenerateMainMiscFunctions();

            GenerateKVectorFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
