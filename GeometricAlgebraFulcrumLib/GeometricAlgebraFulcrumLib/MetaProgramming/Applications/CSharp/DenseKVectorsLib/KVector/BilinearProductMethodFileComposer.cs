using System;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Languages;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.DenseKVectorsLib.KVector;

internal sealed class BilinearProductMethodFileComposer : 
    GaFuLLibraryMetaContextFileComposerBase
{
    private XGaKVector<IMetaExpressionAtomic> _inputKVector1;
    private XGaKVector<IMetaExpressionAtomic> _inputKVector2;
    private XGaKVector<IMetaExpressionAtomic> _outputKVector;
    private readonly GaFuLLanguageOperationSpecs _operationSpecs;
    private readonly int _inputGrade1;
    private readonly int _inputGrade2;
    private readonly int _outputGrade;


    internal BilinearProductMethodFileComposer(GaFuLLibraryComposer libGen, GaFuLLanguageOperationSpecs opSpecs, int inGrade1, int inGrade2)
        : base(libGen)
    {
        _operationSpecs = opSpecs;
            
        _inputGrade1 = inGrade1;
        _inputGrade2 = inGrade2;

        var (isValid, outputGrade) = opSpecs.GetKVectorsBilinearProductGrade(
            VSpaceDimensions,
            inGrade1, 
            inGrade2
        );

        if (!isValid)
            throw new InvalidOperationException();

        _outputGrade = outputGrade;
    }
        
    internal BilinearProductMethodFileComposer(GaFuLLibraryComposer libGen, GaFuLLanguageOperationSpecs opSpecs, int inGrade1, int inGrade2, int outGrade)
        : base(libGen)
    {
        _operationSpecs = opSpecs;
            
        _inputGrade1 = inGrade1;
        _inputGrade2 = inGrade2;

        _outputGrade = outGrade;
    }


    protected override void DefineContextParameters(MetaContext context)
    {
        _inputKVector1 = context.ParameterVariablesFactory.CreateDenseKVector(
            VSpaceDimensions,
            _inputGrade1,
            index => $"mv1Scalar{index}"
        );

        _inputKVector2 = context.ParameterVariablesFactory.CreateDenseKVector(
            VSpaceDimensions,
            _inputGrade2,
            index => $"mv2Scalar{index}"
        );
    }

    protected override void DefineContextComputations(MetaContext context)
    {
        var mv = _operationSpecs.OperationKind switch
        {
            GaFuLLanguageOperationKind.BinaryOuterProduct =>
                _inputKVector1.Op(_inputKVector2),

            GaFuLLanguageOperationKind.BinaryGeometricProduct =>
                _operationSpecs.IsEuclidean 
                    ? _inputKVector1.EGp(_inputKVector2)
                    : _inputKVector1.Gp(_inputKVector2),

            GaFuLLanguageOperationKind.BinaryGeometricProductDual =>
                _operationSpecs.IsEuclidean 
                    ? _inputKVector1.EGp(_inputKVector2).EDual(VSpaceDimensions)
                    : _inputKVector1.Gp(_inputKVector2).Dual(VSpaceDimensions),

            GaFuLLanguageOperationKind.BinaryLeftContractionProduct =>
                _operationSpecs.IsEuclidean 
                    ? _inputKVector1.ELcp(_inputKVector2)
                    : _inputKVector1.Lcp(_inputKVector2),

            GaFuLLanguageOperationKind.BinaryRightContractionProduct =>
                _operationSpecs.IsEuclidean 
                    ? _inputKVector1.ERcp(_inputKVector2)
                    : _inputKVector1.Rcp(_inputKVector2),

            GaFuLLanguageOperationKind.BinaryFatDotProduct =>
                _operationSpecs.IsEuclidean 
                    ? _inputKVector1.EFdp(_inputKVector2)
                    : _inputKVector1.Fdp(_inputKVector2),

            GaFuLLanguageOperationKind.BinaryHestenesInnerProduct =>
                _operationSpecs.IsEuclidean 
                    ? _inputKVector1.EHip(_inputKVector2)
                    : _inputKVector1.Hip(_inputKVector2),

            _ => throw new InvalidOperationException()
        };

        _outputKVector = mv.GetKVectorPart(_outputGrade);

        _outputKVector.SetIsOutput(true);
    }

    protected override void DefineContextExternalNames(MetaContext context)
    {
        context.SetExternalNamesByTermIndex(
            _inputKVector1,
            index => $"mv1[{index}]"
        );

        context.SetExternalNamesByTermIndex(
            _inputKVector2,
            index => $"mv2[{index}]"
        );

        context.SetExternalNamesByTermIndex(
            _outputKVector,
            index => $"c[{index}]"
        );

        context.SetIntermediateExternalNamesByNameIndex(
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
            VSpaceDimensions.KVectorSpaceDimension(_outputGrade);

        var methodName = _operationSpecs.GetName(
            _inputGrade1, _inputGrade2, _outputGrade
        );

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