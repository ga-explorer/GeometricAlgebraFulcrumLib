using System.Collections.Generic;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MetaProgramming.Composers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.DenseKVectorsLib;

public abstract class GaFuLLibraryMetaContextFileComposerBase
    : GaFuLMetaContextCodeFileComposerBase
{
    internal XGaProcessor<IMetaExpressionAtomic> GeometricProcessor
        => DenseKVectorsLibraryComposer.GeometricProcessor;

    internal XGaProcessor<IMetaExpressionAtomic> EuclideanProcessor 
        => DenseKVectorsLibraryComposer.EuclideanProcessor;

    public int VSpaceDimensions { get; }
        
    public int GradesCount 
        => VSpaceDimensions + 1;

    public IEnumerable<int> Grades 
        => GradesCount.GetRange();

    internal string CurrentNamespace 
        => DenseKVectorsLibraryComposer.CurrentNamespace;

    internal GaFuLLibraryComposer DenseKVectorsLibraryComposer 
        => (GaFuLLibraryComposer) CodeComposer;


    internal GaFuLLibraryMetaContextFileComposerBase(GaFuLLibraryComposer libGen)
        : base(libGen)
    {
        VSpaceDimensions = libGen.VSpaceDimensions;
    }
        

    internal void GenerateBladeFileStartCode()
    {
        TextComposer.AppendLine(
            Templates["kvector_file_start"].GenerateUsing(CurrentNamespace)
        );

        TextComposer.IncreaseIndentation();
        TextComposer.IncreaseIndentation();
    }

    internal void GenerateBladeFileFinishCode()
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