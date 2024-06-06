using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Subspaces;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Utilities.Code;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.DenseKVectorsLib.KVector;

internal sealed class ApplyVersorMethodFileComposer : 
    GaFuLLibraryMetaContextFileComposerBase
{
    private IXGaSubspace<IMetaExpressionAtomic> _subspace;
    private XGaKVector<IMetaExpressionAtomic> _inputKVector;
    private XGaKVector<IMetaExpressionAtomic> _outputKVector;
    private readonly GaFuLLanguageOperationSpecs _operationSpecs;
    private readonly int _inputGrade1;
    private readonly int _inputGrade2;


    internal ApplyVersorMethodFileComposer(GaFuLLibraryComposer libGen, GaFuLLanguageOperationSpecs opSpecs, int inGrade1, int inGrade2)
        : base(libGen)
    {
        _operationSpecs = opSpecs;
        _inputGrade1 = inGrade1;
        _inputGrade2 = inGrade2;
    }


    protected override void DefineContextParameters(MetaContext context)
    {
        var subspaceKVector = context.ParameterVariablesFactory.CreateDenseKVector(
            VSpaceDimensions,
            _inputGrade1,
            index => $"versorScalar{index}"
        );

        _subspace = subspaceKVector.ToSubspace();

        _inputKVector = context.ParameterVariablesFactory.CreateDenseKVector(
            VSpaceDimensions,
            _inputGrade2,
            index => $"kVectorScalar{index}"
        );
    }

    protected override void DefineContextComputations(MetaContext context)
    {
        var inputSubspace = 
            _inputKVector.ToSubspace();

        _outputKVector = _operationSpecs.OperationKind switch
        {
            GaFuLLanguageOperationKind.BinaryProject => 
                _subspace.Project(inputSubspace).GetBlade(),//.GetKVectorPart(_inputGrade2),

            //GaFuLLanguageOperationKind.BinaryRotate => 
            //    _subspace.Rotate(inputSubspace).Blade,//.GetKVectorPart(_inputGrade2),

            GaFuLLanguageOperationKind.BinaryReflect => 
                _subspace.Reflect(inputSubspace).GetBlade(),//.GetKVectorPart(_inputGrade2),

            GaFuLLanguageOperationKind.BinaryComplement => 
                _subspace.Complement(inputSubspace).GetBlade(),//.GetKVectorPart(_inputGrade2),

            _ => throw new InvalidOperationException()
        };
    }

    protected override void DefineContextExternalNames(MetaContext context)
    {
        _subspace.GetBlade().SetExternalNamesByTermIndex(
            index => $"scalars1[{index}]"
        );

        _inputKVector.SetExternalNamesByTermIndex(
            index => $"scalars2[{index}]"
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

    public override void Generate()
    {
        GenerateBladeFileStartCode();

        var computationsText = 
            GenerateCode();

        var kvSpaceDimensions = 
            VSpaceDimensions.KVectorSpaceDimension(_inputGrade2);

        var methodName =
            _operationSpecs.GetName(_inputGrade1, _inputGrade2, _inputGrade2);
            
        TextComposer.AppendAtNewLine(
            Templates["bilinearproduct"],
            "name", methodName,
            "num", kvSpaceDimensions,
            "double", GeoLanguage.ScalarTypeName,
            "computations", computationsText
        );

        GenerateBladeFileFinishCode();

        FileComposer.FinalizeText();
    }
}