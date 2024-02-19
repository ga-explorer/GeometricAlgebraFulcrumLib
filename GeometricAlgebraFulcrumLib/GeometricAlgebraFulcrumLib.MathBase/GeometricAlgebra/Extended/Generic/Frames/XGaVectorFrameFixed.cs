using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Frames;

public sealed class XGaVectorFrameFixed<T> :
    IXGaVectorFrame<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVectorFrameFixed<T> Create(XGaVector<T> point, IXGaVectorFrame<T> vectorFrame)
    {
        return new XGaVectorFrameFixed<T>(point, vectorFrame);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVectorFrameFixed<T> Create(XGaVector<T> point, XGaVectorFrameSpecs frameSpecs, IEnumerable<XGaVector<T>> vectorStoragesList)
    {
        var vectorFrame = XGaVectorFrame<T>.Create(frameSpecs, vectorStoragesList);

        return new XGaVectorFrameFixed<T>(point, vectorFrame);
    }

        
    public XGaProcessor<T> Processor 
        => XGaVectorFrame.Processor;

    public XGaMetric Metric 
        => XGaVectorFrame.Metric;

    public IScalarProcessor<T> ScalarProcessor
        => XGaVectorFrame.ScalarProcessor;
        
    public XGaVector<T> Point { get; }

    public IXGaVectorFrame<T> XGaVectorFrame { get; }
        
    public int VSpaceDimensions 
        => XGaVectorFrame.VSpaceDimensions;

    public int Count
        => XGaVectorFrame.Count;

    public XGaVector<T> this[int index]
        => Point + XGaVectorFrame[index];


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private XGaVectorFrameFixed(XGaVector<T> point, IXGaVectorFrame<T> vectorFrame)
    {
        Point = point;
        XGaVectorFrame = vectorFrame;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return XGaVectorFrame.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<XGaVector<T>> GetEnumerator()
    {
        return XGaVectorFrame
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

        foreach (var vector in XGaVectorFrame)
            composer.AppendAtNewLine(vector.ToString());

        composer
            .DecreaseIndentation()
            .AppendAtNewLine("}");

        return composer.ToString();
    }
}