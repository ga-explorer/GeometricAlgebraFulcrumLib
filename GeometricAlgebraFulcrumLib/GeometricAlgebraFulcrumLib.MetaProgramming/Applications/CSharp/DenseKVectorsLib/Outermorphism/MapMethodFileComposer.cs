using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.DenseKVectorsLib.Outermorphism;

internal class MapMethodFileComposer : 
    GaFuLLibraryMetaContextFileComposerBase
{
    private readonly int _inputGrade;
    private IMetaExpressionAtomic[,] _linearMapArray;
    private XGaKVector<IMetaExpressionAtomic> _inputKVector;
    private XGaKVector<IMetaExpressionAtomic> _outputKVector;


    internal MapMethodFileComposer(GaFuLLibraryComposer libGen, int inGrade)
        : base(libGen)
    {
        _inputGrade = inGrade;
    }

        
    protected override void DefineContextParameters(MetaContext context)
    {
        _linearMapArray = context.ParameterVariablesFactory.CreateDenseArray(
            VSpaceDimensions,
            VSpaceDimensions,
            (row, col) => $"omScalarR{row}C{col}"
        );

        _inputKVector = context.ParameterVariablesFactory.CreateDenseKVector(
            VSpaceDimensions,
            _inputGrade,
            index => $"kVectorScalar{index}"
        );
    }

    protected override void DefineContextComputations(MetaContext context)
    {
        var outermorphism =
            _linearMapArray
                .ColumnsToLinVectors(GeometricProcessor.ScalarProcessor)
                .ToLinUnilinearMap(GeometricProcessor.ScalarProcessor)
                .ToOutermorphism(GeometricProcessor);
                
        _outputKVector = outermorphism.OmMap(_inputKVector);
    }

    protected override void DefineContextExternalNames(MetaContext context)
    {
        _inputKVector.SetExternalNamesByTermIndex(
            index => $"kVectorScalars[{index}]"
        );

        _outputKVector.SetAsOutputByTermIndex(
            index => $"mappedKVectorScalars[{index}]"
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
        GenerateOutermorphismFileStartCode();

        var computationsText = GenerateCode();

        TextComposer.Append(
            Templates["om_apply"],
            "double", GeoLanguage.ScalarTypeName,
            "grade", _inputGrade,
            "num", VSpaceDimensions.KVectorSpaceDimension(_inputGrade),
            "computations", computationsText
        );

        GenerateOutermorphismFileFinishCode();

        FileComposer.FinalizeText();
    }
}