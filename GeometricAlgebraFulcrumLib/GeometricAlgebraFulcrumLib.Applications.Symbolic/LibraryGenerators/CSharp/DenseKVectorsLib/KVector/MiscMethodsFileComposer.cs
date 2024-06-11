using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MetaProgramming.Composers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Utilities.Code;
using GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Structured;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.DenseKVectorsLib.KVector;

internal sealed class MiscMethodsFileComposer : 
    GaFuLLibraryFileComposerBase
{
    internal MiscMethodsFileComposer(GaFuLLibraryComposer libGen)
        : base(libGen)
    {
    }

    //private GeoClcInfoMetaContext AddEuclideanDualGeoClcMetaContext()
    //{
    //    var codeText =
    //        Templates["edual_macro"].GenerateUsing(CurrentNamespace);

    //    var metaContext =
    //        _tempSymbolsCompiler.CompileMetaContext(
    //            codeText,
    //            _currentFrame.AssociatedFrame.ChildScope
    //            );

    //    return new GeoClcInfoMetaContext(metaContext);
    //}

    private void GenerateEuclideanDualFunction(int inGrade)
    {
        var outGrade = VSpaceDimensions - inGrade;

        var context = new MetaContext(
            DenseKVectorsLibraryComposer.DefaultContextOptions
        )
        {
            XGaProcessor = GeometricProcessor
        };

        var inputKVector = 
            context.ParameterVariablesFactory.CreateDenseKVector(
                VSpaceDimensions,
                inGrade,
                index => $"inputKVectorScalar{index}"
            );

        var outputKVector = inputKVector.EDual(VSpaceDimensions);

        inputKVector.SetExternalNamesByTermIndex(
            index => $"scalars[{index}]"
        );
            
        outputKVector.SetAsOutputByTermIndex(
            index => $"c[{index}]"
        );
        
        context.OptimizeContext();

        context.SetComputedExternalNamesByOrder(
            DenseKVectorsLibraryComposer.MaxTargetLocalVars,
            index => $"tempVar{index:X4}",
            index => $"tempArray[{index}]"
        );

        var macroComposer = new GaFuLMetaContextCodeComposer(
            DenseKVectorsLibraryComposer.GeoLanguage, 
            context,
            DenseKVectorsLibraryComposer.DefaultContextCodeComposerOptions
        );
            
        //Generate code from macro binding
        var computationsText = macroComposer.Generate();

        TextComposer.Append(
            Templates["edual"],
            "double", GeoLanguage.ScalarTypeName,
            "grade", inGrade,
            "num", VSpaceDimensions.KVectorSpaceDimension(inGrade),
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
            "double", GeoLanguage.ScalarTypeName,
            "num", kvSpaceDim,
            "addcases", addCasesText,
            "subtcases", subtCasesText,
            "timescases", timesCasesText
        );
    }


    private void TestSelfDpGradeFunctionComputationCondition(SteSyntaxElementsList textBuilder, GaFuLMetaContextComputationCodeInfo compInfo)
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

        //var grade = GeoUtils.ID_To_Grade(id);

        //if (grade == 0)
        //    compInfo.EnableCodeGeneration = false;
    }

    private static void AddSelfDpGradeFunctionComputationCondition(SteSyntaxElementsList textBuilder, GaFuLMetaContextComputationCodeInfo compInfo)
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

    private void GenerateSelfDpGradeFunction(int inGrade)
    {
        var context = new MetaContext(
            DenseKVectorsLibraryComposer.DefaultContextOptions
        )
        {
            XGaProcessor = GeometricProcessor
        };

        //var outGradesList =
        //    GeometricProcessor
        //        .BasisSet
        //        .GradesOfEGp(inGrade, inGrade)
        //        .Where(grade => grade > 0)
        //        .OrderByDescending(g => g);

        var inputKVector =
            context
                .ParameterVariablesFactory
                .CreateDenseKVector(
                    VSpaceDimensions,
                    inGrade,
                    index => $"inputKVectorScalar{index}"
                );

        var outputKVector = 
            inputKVector.EGp(inputKVector);

        context.ContextOptions.FixOutputComputationsOrder = true;
  
        inputKVector.SetExternalNamesByTermIndex(
            index => $"scalars[{index}]"
        );
            
        outputKVector.SetAsOutputByTermId(
            _ => "c"
        );
        
        context.OptimizeContext();

        context.SetComputedExternalNamesByOrder(
            DenseKVectorsLibraryComposer.MaxTargetLocalVars,
            index => $"tempVar{index:X4}",
            index => $"tempArray[{index}]"
        );

        var macroComposer = new GaFuLMetaContextCodeComposer(
            DenseKVectorsLibraryComposer.GeoLanguage, 
            context,
            DenseKVectorsLibraryComposer.DefaultContextCodeComposerOptions
        )
        {
            ComposerOptions =
            {
                ActionBeforeGenerateSingleComputation = TestSelfDpGradeFunctionComputationCondition,
                ActionAfterGenerateSingleComputation = AddSelfDpGradeFunctionComputationCondition
            }
        };

        //Generate code from macro binding
        var computationsText = macroComposer.Generate();

        TextComposer.Append(
            Templates["self_dp_grade"],
            "grade", inGrade,
            "double", GeoLanguage.ScalarTypeName,
            "computations", computationsText
        );
    }

    private void GenerateMainSelfDpGradeFunction()
    {
        if (VSpaceDimensions <= 3)
        {
            TextComposer.Append("public int SelfDPGrade() { return 0; }");

            return;
        }

        var selfDpGradeCasesText = new ListTextComposer(Environment.NewLine);

        for (var grade = 2; grade < VSpaceDimensions - 1; grade++)
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

        foreach (var grade in Grades)
        {
            miscCasesText.AddTextItems(miscCasesTemplates,
                "signature", CurrentNamespace,
                "grade", grade,
                "num", VSpaceDimensions.KVectorSpaceDimension(grade),
                "sign", grade.ReverseIsNegativeOfGrade() ? "-" : "",
                "invgrade", VSpaceDimensions - grade
            );
        }

        var mainFuncsTemplate = Templates["misc_main"];

        mainFuncsTemplate.SetParametersValues(miscCasesText);

        TextComposer.Append(
            mainFuncsTemplate,
            "signature", CurrentNamespace,
            "double", GeoLanguage.ScalarTypeName,
            "norm2_opname", GaFuLLanguageOperationKind.UnaryNormSquared.GetName(false),
            "emag2_opname", GaFuLLanguageOperationKind.UnaryNormSquared.GetName(true)
        );

        GenerateMainSelfDpGradeFunction();
    }

    public override void Generate()
    {
        GenerateKVectorFileStartCode();

        var kvSpaceDimList =
            VSpaceDimensions
                .GetRange()
                .Select(grade => VSpaceDimensions.KVectorSpaceDimension(grade))
                .Distinct();

        foreach (var kvSpaceDim in kvSpaceDimList)
            GenerateMiscFunctions(kvSpaceDim);

        foreach (var inGrade in Grades)
            GenerateEuclideanDualFunction(inGrade);

        for (var inGrade = 2; inGrade < VSpaceDimensions - 1; inGrade++)
            GenerateSelfDpGradeFunction(inGrade);

        GenerateMainMiscFunctions();

        GenerateKVectorFileFinishCode();

        FileComposer.FinalizeText();
    }
}