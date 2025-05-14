using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Utilities.Code;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Structured;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.DenseKVectorsLib.KVector;

internal sealed class UnaryGpMethodsFileComposer 
    : GaFuLLibraryMetaContextFileComposerBase
{
    private int _inGrade;
    private int _outGrade;
    private XGaKVector<IMetaExpressionAtomic> _inputKVector;
    private XGaKVector<IMetaExpressionAtomic> _outputKVector;

    internal GaFuLLanguageOperationSpecs OperationSpecs { get; }


    internal UnaryGpMethodsFileComposer(GaFuLLibraryComposer libGen, GaFuLLanguageOperationSpecs opSpecs)
        : base(libGen)
    {
        OperationSpecs = opSpecs;
    }

        
    protected override void DefineContextParameters(MetaContext context)
    {
        _inputKVector = context.ParameterVariablesFactory.CreateDenseKVector(
            VSpaceDimensions,
            _inGrade,
            index => $"kVectorScalar{index}"
        );
    }
        
    protected override void DefineContextComputations(MetaContext context)
    {
        _outputKVector = OperationSpecs.OperationKind switch
        {
            GaFuLLanguageOperationKind.UnaryGeometricProductSquared
                => OperationSpecs.IsEuclidean
                    ? _inputKVector.EGp(_inputKVector).GetKVectorPart(_outGrade)
                    : _inputKVector.Gp(_inputKVector).GetKVectorPart(_outGrade),

            GaFuLLanguageOperationKind.UnaryGeometricProductReverse
                => OperationSpecs.IsEuclidean
                    ? _inputKVector.EGp(_inputKVector.Reverse()).GetKVectorPart(_outGrade)
                    : _inputKVector.Gp(_inputKVector.Reverse()).GetKVectorPart(_outGrade),

            _ => throw new InvalidOperationException()
        };
    }

    protected override void DefineContextExternalNames(MetaContext context)
    {
        _inputKVector.SetExternalNamesByTermIndex(
            index => $"scalars[{index}]"
        );

        _outputKVector.SetAsOutputByTermIndex(
            index => $"c[{index}]"
        );
    }
     
    protected override void DefineContextComputedExternalNames(MetaContext context)
    {
        context.SetComputedExternalNamesByOrder(
            DenseKVectorsLibraryComposer.MaxTargetLocalVars,
            index => $"tempVar{index:X4}",
            index => $"tempArray[{index}]"
        );
    }


    private void GenerateMethod(string funcName, int inputGrade, int outputGrade)
    {
        _inGrade = inputGrade;
        _outGrade = outputGrade;

        var computationsText = GenerateCode();

        var kvSpaceDim = 
            VSpaceDimensions.KVectorSpaceDimensions(_outGrade);

        TextComposer.AppendAtNewLine(
            Templates["self_bilinearproduct"],
            "name", funcName,
            "num", kvSpaceDim,
            "double", GeoLanguage.ScalarTypeName,
            "computations", computationsText
        );
    }

    private void GenerateMethods(int inputGrade)
    {
        var gpCaseText = new ListTextComposer("," + Environment.NewLine);

        var gradesList = 
            VSpaceDimensions.GradesOfEGp(inputGrade, inputGrade);

        foreach (var outputGrade in gradesList)
        {
            var funcName = OperationSpecs.GetName(
                inputGrade, inputGrade, outputGrade
            );

            GenerateMethod(
                funcName,
                inputGrade,
                outputGrade
            );

            gpCaseText.Add(Templates["selfgp_case"],
                "signature", CurrentNamespace,
                "grade", outputGrade,
                "name", funcName
            );
        }

        TextComposer.AppendAtNewLine(
            Templates["selfgp"],
            "signature", CurrentNamespace,
            "name", OperationSpecs.GetName(inputGrade, inputGrade),
            "double", GeoLanguage.ScalarTypeName,
            "selfgp_case", gpCaseText
        );
    }

    private void GenerateMainMethod()
    {
        var casesTemplate = Templates["selfgp_main_case"];

        var casesText = new ListTextComposer(Environment.NewLine);

        foreach (var grade in Grades)
        {
            casesText.Add(
                casesTemplate,
                "name", OperationSpecs.GetName(grade, grade),
                "grade", grade,
                "signature", CurrentNamespace
            );
        }

        TextComposer.AppendAtNewLine(
            Templates["selfgp_main"],
            "name", OperationSpecs,
            "signature", CurrentNamespace,
            "cases", casesText
        );
    }

    public override void Generate()
    {
        GenerateBladeFileStartCode();

        foreach (var grade in Grades)
            GenerateMethods(grade);

        GenerateMainMethod();

        GenerateBladeFileFinishCode();

        FileComposer.FinalizeText();
    }

}