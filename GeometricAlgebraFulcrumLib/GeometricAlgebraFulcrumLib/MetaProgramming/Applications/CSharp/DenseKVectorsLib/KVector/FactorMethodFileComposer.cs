﻿using System;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.DenseKVectorsLib.KVector;

internal sealed class FactorMethodFileComposer : 
    GaFuLLibraryMetaContextFileComposerBase
{
    private readonly int _inputGrade;
    private readonly ulong _inputId;
    private XGaKVector<IMetaExpressionAtomic> _inputBlade;
    private XGaVector<IMetaExpressionAtomic>[] _inputBasisVectorsArray;
    private XGaVector<IMetaExpressionAtomic>[] _outputVectorsArray;


    internal FactorMethodFileComposer(GaFuLLibraryComposer libGen, int inGrade, ulong inId)
        : base(libGen)
    {
        _inputGrade = inGrade;
        _inputId = inId;
    }

        
    protected override void DefineContextParameters(MetaContext context)
    {
        _inputBlade = 
            context
                .ParameterVariablesFactory
                .CreateDenseKVector(
                    VSpaceDimensions,
                    _inputGrade,
                    index => $"bladeScalar{index}"
                );

        _inputBasisVectorsArray = 
            _inputId
                .PatternToPositions()
                .Select(index => 
                    context
                        .NumbersFactory
                        .CreateBasisVector(index)
                ).ToArray();
    }

    protected override void DefineContextComputations(MetaContext context)
    {
        var vectorsCount = 
            _inputBasisVectorsArray.Length;

        _outputVectorsArray = 
            new XGaVector<IMetaExpressionAtomic>[vectorsCount];

        var inputBlade = _inputBlade;
        for (var index = 0; index < vectorsCount - 1; index++)
        {
            _outputVectorsArray[index] =
                _inputBasisVectorsArray[index].Lcp(inputBlade).Lcp(inputBlade).AsVector();

            inputBlade = _outputVectorsArray[index].Lcp(inputBlade);
        }

        _outputVectorsArray[^1] = inputBlade.AsVector();

        _outputVectorsArray.SetIsOutput(true);
    }

    protected override void DefineContextExternalNames(MetaContext context)
    {
        context.SetExternalNamesByTermIndex(
            _inputBlade,
            index => $"scalars[{index}]"
        );

        var vectorsCount = _outputVectorsArray.Length;
        for (var i = 0; i < vectorsCount; i++)
        {
            var j = i;

            context.SetExternalNamesByTermIndex(
                _outputVectorsArray[i],
                index => $"vectors[{j}].C{index}"
            );
        }
            
        context.SetIntermediateExternalNamesByNameIndex(
            DenseKVectorsLibraryComposer.MaxTargetLocalVars,
            index => $"tempVar{index:X4}",
            index => $"tempArray[{index}]"
        );
    }

    public override void Generate()
    {
        GenerateBladeFileStartCode();

        var computationsText = GenerateCode();

        var newVectorsText = new ListTextComposer("," + Environment.NewLine);

        for (var i = 0; i < _inputGrade; i++)
            newVectorsText.Add("new " + CurrentNamespace + "Vector()");

        TextComposer.AppendAtNewLine(
            Templates["factor"],
            "signature", CurrentNamespace,
            "id", _inputId,
            "double", GeoLanguage.ScalarTypeName,
            "newvectors", newVectorsText,
            "computations", computationsText
        );

        GenerateBladeFileFinishCode();

        FileComposer.FinalizeText();
    }

}