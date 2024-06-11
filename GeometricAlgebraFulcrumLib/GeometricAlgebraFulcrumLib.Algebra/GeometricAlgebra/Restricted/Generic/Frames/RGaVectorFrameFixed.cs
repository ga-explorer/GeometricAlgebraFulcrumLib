using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Frames;

public sealed class RGaVectorFrameFixed<T> :
    IRGaVectorFrame<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaVectorFrameFixed<T> Create(RGaVector<T> point, IRGaVectorFrame<T> vectorFrame)
    {
        return new RGaVectorFrameFixed<T>(point, vectorFrame);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaVectorFrameFixed<T> Create(RGaVector<T> point, RGaVectorFrameSpecs frameSpecs, IEnumerable<RGaVector<T>> vectorStoragesList)
    {
        var vectorFrame = RGaVectorFrame<T>.Create(frameSpecs, vectorStoragesList);

        return new RGaVectorFrameFixed<T>(point, vectorFrame);
    }

        
    public RGaProcessor<T> Processor 
        => RGaVectorFrame.Processor;

    public RGaMetric Metric 
        => RGaVectorFrame.Metric;

    public IScalarProcessor<T> ScalarProcessor
        => RGaVectorFrame.ScalarProcessor;
        
    public RGaVector<T> Point { get; }

    public IRGaVectorFrame<T> RGaVectorFrame { get; }
        
    public int VSpaceDimensions 
        => RGaVectorFrame.VSpaceDimensions;

    public int Count
        => RGaVectorFrame.Count;

    public RGaVector<T> this[int index]
        => Point + RGaVectorFrame[index];


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RGaVectorFrameFixed(RGaVector<T> point, IRGaVectorFrame<T> vectorFrame)
    {
        Point = point;
        RGaVectorFrame = vectorFrame;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return RGaVectorFrame.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<RGaVector<T>> GetEnumerator()
    {
        return RGaVectorFrame
            .Select(v => Point + v)
            .GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }


    public override string ToString()
    {
        var composer = new LinearTextComposer();

        composer
            .AppendLine($"Fixed Frame at {Point} {{")
            .IncreaseIndentation();

        foreach (var vector in RGaVectorFrame)
            composer.AppendAtNewLine(vector.ToString());

        composer
            .DecreaseIndentation()
            .AppendAtNewLine("}");

        return composer.ToString();
    }
}