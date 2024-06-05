using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MetaProgramming.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.DenseKVectorsLib;

public abstract class GaFuLLibraryFileComposerBase : 
    GaFuLCodePartFileComposerBase
{
    internal XGaProcessor<IMetaExpressionAtomic> GeometricProcessor 
        => DenseKVectorsLibraryComposer.GeometricProcessor;

    internal XGaProcessor<IMetaExpressionAtomic> EuclideanProcessor 
        => DenseKVectorsLibraryComposer.EuclideanProcessor;

    public int VSpaceDimensions 
        => DenseKVectorsLibraryComposer.VSpaceDimensions;
        
    public int GradesCount 
        => VSpaceDimensions + 1;

    public IEnumerable<int> Grades 
        => GradesCount.GetRange();

    internal string CurrentNamespace 
        => DenseKVectorsLibraryComposer.CurrentNamespace;

    internal GaFuLLibraryComposer DenseKVectorsLibraryComposer 
        => (GaFuLLibraryComposer) CodeComposer;


    internal GaFuLLibraryFileComposerBase(GaFuLLibraryComposer libGen)
        : base(libGen)
    {
    }


    internal void GenerateKVectorFileStartCode()
    {
        TextComposer.AppendLine(
            Templates["kvector_file_start"].GenerateUsing(CurrentNamespace)
        );

        TextComposer.IncreaseIndentation();
        TextComposer.IncreaseIndentation();
    }

    internal void GenerateKVectorFileFinishCode()
    {
        TextComposer
            .DecreaseIndentation()
            .AppendLineAtNewLine("}")
            .DecreaseIndentation()
            .AppendLineAtNewLine("}");
    }

    internal void GenerateOutermorphismFileStartCode()
    {
        TextComposer.AppendLine(
            Templates["om_file_start"],
            "signature", CurrentNamespace,
            "grade", VSpaceDimensions
        );

        TextComposer.IncreaseIndentation();
        TextComposer.IncreaseIndentation();
    }

    internal void GenerateOutermorphismFileFinishCode()
    {
        TextComposer
            .DecreaseIndentation()
            .AppendLineAtNewLine("}")
            .DecreaseIndentation()
            .AppendLineAtNewLine("}");
    }

    internal void GenerateBeginRegion(string regionText)
    {
        TextComposer.AppendLineAtNewLine(@"#region " + regionText);
    }

    internal void GenerateEndRegion()
    {
        TextComposer.AppendLineAtNewLine(@"#endregion");
    }
}