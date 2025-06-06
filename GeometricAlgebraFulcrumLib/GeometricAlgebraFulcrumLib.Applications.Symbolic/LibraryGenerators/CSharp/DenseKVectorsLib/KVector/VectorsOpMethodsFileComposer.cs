﻿using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Structured;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.DenseKVectorsLib.KVector;

/// <summary>
/// This class generates a single macro into a code file using several related bindings and target variable
/// namings.
/// </summary>
internal sealed class VectorsOpMethodsFileComposer : 
    GaFuLLibraryMetaContextFileComposerBase
{
    private int _outGrade;
    private XGaVector<IMetaExpressionAtomic>[] _inputVectorsArray;
    private XGaKVector<IMetaExpressionAtomic> _outputKVector;


    internal VectorsOpMethodsFileComposer(GaFuLLibraryComposer libGen)
        : base(libGen)
    {
    }


    protected override void DefineContextParameters(MetaContext context)
    {
        _inputVectorsArray = 
            new XGaVector<IMetaExpressionAtomic>[_outGrade];

        for (var g = 0; g < _outGrade; g++)
        {
            var grade = g;

            _inputVectorsArray[grade] =
                context.ParameterVariablesFactory.CreateDenseVector(
                    VSpaceDimensions,
                    index => $"vector{grade}Scalar{index}"
                );
        }
    }

    protected override void DefineContextComputations(MetaContext context)
    {
        _outputKVector = _inputVectorsArray.Op(context.XGaProcessor);
    }

    protected override void DefineContextExternalNames(MetaContext context)
    {
        for (var g = 0; g < _outGrade; g++)
        {
            var grade = g;

            _inputVectorsArray[grade].SetExternalNamesByTermIndex(
                index => $"vectors[{grade}].C{index}"
            );
        }

        _outputKVector.SetAsOutputByTermIndex(
            index => $"scalars[{index}]"
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

    private void GenerateVectorsOpFunction()
    {
        //Each time this protected method is called the internal GeoClcMetaContextCodeComposer is initialized,
        //the bindings and target names are set, and the macro code is generated automatically.
        var computationsText = GenerateCode();

        TextComposer.Append(
            Templates["op_vectors"],
            "signature", CurrentNamespace,
            "double", GeoLanguage.ScalarTypeName,
            "grade", _outGrade,
            "num", VSpaceDimensions.KVectorSpaceDimensions(_outGrade),
            "computations", computationsText
        );
    }

    public override void Generate()
    {
        GenerateBladeFileStartCode();

        var casesText = new ListTextComposer(Environment.NewLine);

        for (var grade = 2; grade <= VSpaceDimensions; grade++)
        {
            _outGrade = grade;

            GenerateVectorsOpFunction();

            casesText.Add(
                Templates["op_vectors_main_case"].GenerateUsing(grade)
            );
        }

        TextComposer.Append(
            Templates["op_vectors_main"],
            "signature", CurrentNamespace,
            "op_vectors_main_case", casesText
        );

        GenerateBladeFileFinishCode();

        FileComposer.FinalizeText();
    }
}