using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Subspaces;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Permutations;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Frames;

public class XGaVectorFrame<T> :
    IXGaVectorFrame<T>
{
    internal static XGaVectorFrame<T> Create(XGaVectorFrameSpecs frameSpecs, IEnumerable<XGaVector<T>> vectorList)
    {
        return new XGaVectorFrame<T>(
            frameSpecs,
            vectorList.ToArray()
        );
    }


    private readonly IReadOnlyList<XGaVector<T>> _vectorList;

        
    public XGaProcessor<T> Processor
        => _vectorList[0].Processor;

    public XGaMetric Metric
        => _vectorList[0].Metric;

    public IScalarProcessor<T> ScalarProcessor
        => _vectorList[0].ScalarProcessor;


    public int VSpaceDimensions 
        => _vectorList.GetVSpaceDimensions();
        
    public int Count
        => _vectorList.Count;

    public XGaVector<T> this[int index]
    {
        get => _vectorList[index];
        //set => _vectorList[index] = value ?? throw new ArgumentNullException(nameof(value));
    }

    public XGaVectorFrameSpecs FrameSpecs { get; }

        
    private XGaVectorFrame(XGaVectorFrameSpecs frameSpecs, IReadOnlyList<XGaVector<T>> vectorList)
    {
        _vectorList = vectorList;
        FrameSpecs = frameSpecs;
    }


    public bool IsValid()
    {
        return _vectorList.All(v => v.IsValid());
    }

    //public XGaVectorFrame<T> AppendVector(XGaVector<T> vector)
    //{
    //    _vectorList.Add(vector);

    //    return this;
    //}

    //public XGaVectorFrame<T> AppendVectors(params XGaVector<T>[] vectorsList)
    //{
    //    foreach (var vector in vectorsList)
    //        _vectorList.Add(vector);

    //    return this;
    //}

    //public XGaVectorFrame<T> PrependVector(XGaVector<T> vector)
    //{
    //    _vectorList.Insert(0, vector);

    //    return this;
    //}

    //public XGaVectorFrame<T> InsertVector(int index, XGaVector<T> vector)
    //{
    //    _vectorList.Insert(index, vector);

    //    return this;
    //}


    public XGaVectorFrame<T> GetSubFrame(int startIndex, int count)
    {
        return new XGaVectorFrame<T>(
            FrameSpecs,
            _vectorList.Skip(startIndex).Take(count).ToArray()
        );
    }

    public XGaVectorFrame<T> GetNegativeFrame()
    {
        return new XGaVectorFrame<T>(
            FrameSpecs,
            _vectorList.Select(v => v.Negative()).ToArray()
        );
    }

    public XGaVectorFrame<T> GetOrthonormalFrame()
    {
        if (FrameSpecs.Orthonormal == true)
            return FrameSpecs.UnitNormSquared == true
                ? this
                : GetUnitNormFrame();

        var orthogonalVector = _vectorList[0];
        var vectorStoragesList = new List<XGaVector<T>>
        {
            orthogonalVector.Divide(orthogonalVector.Norm().ScalarValue)
        };

        XGaKVector<T> mv1 = orthogonalVector;

        for (var i = 1; i < _vectorList.Count; i++)
        {
            var mv2 = mv1.Op(_vectorList[i]);

            orthogonalVector =
                mv1.Reverse().Gp(mv2).GetVectorPart();

            vectorStoragesList.Add(
                orthogonalVector.Divide(orthogonalVector.Norm().ScalarValue)
            );

            mv1 = mv2;
        }

        var frameSpecs = new XGaVectorFrameSpecs()
        {
            EqualNormSquared = true,
            EqualScalarProduct = null,
            LinearlyIndependent = true,
            Orthogonal = true,
            UnitNormSquared = true
        };

        return new XGaVectorFrame<T>(
            frameSpecs,
            vectorStoragesList
        );
    }

    public XGaVectorFrame<T> GetOrthogonalFrame()
    {
        if (FrameSpecs.Orthogonal == true)
            return this;

        var orthogonalVector = _vectorList[0];
        var vectorStoragesList = new List<XGaVector<T>>
        {
            orthogonalVector
        };

        XGaKVector<T> mv1 = orthogonalVector;

        for (var i = 1; i < _vectorList.Count; i++)
        {
            var mv2 = mv1.Op(_vectorList[i]);

            orthogonalVector =
                mv1.Reverse().Gp(mv2).GetVectorPart();

            vectorStoragesList.Add(orthogonalVector);

            mv1 = mv2;
        }

        var frameSpecs = new XGaVectorFrameSpecs()
        {
            EqualNormSquared = null,
            EqualScalarProduct = null,
            LinearlyIndependent = FrameSpecs.LinearlyIndependent,
            Orthogonal = true,
            UnitNormSquared = null
        };

        return new XGaVectorFrame<T>(
            frameSpecs,
            vectorStoragesList
        );
    }

    public XGaVectorFrame<T> GetSwappedPairsFrame()
    {
        var frame = new List<XGaVector<T>>();

        //Swap each pair of two consecutive vectors in the frame
        for (var i = 0; i < _vectorList.Count - 1; i += 2)
        {
            frame.Add(_vectorList[i + 1]);
            frame.Add(_vectorList[i]);
        }

        if (_vectorList.Count % 2 == 1)
        {
            //To keep the same handedness we count the number of swaps and
            //negate the final vector if the number is odd

            var numberOfSwaps = (_vectorList.Count - 1) / 2;

            var lastVector = numberOfSwaps % 2 == 0
                ? _vectorList[^1]
                : _vectorList[^1].Negative();

            frame.Add(lastVector);
        }

        return new XGaVectorFrame<T>(
            FrameSpecs,
            frame
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaSubspace<T> GetSubspace()
    {
        var blade = 
            FrameSpecs.LinearlyIndependent.HasValue && 
            FrameSpecs.LinearlyIndependent.Value
                ? _vectorList.Op(Processor) 
                : Processor.SpanToBlade(_vectorList);

        return new XGaSubspace<T>(blade);
    }

    public bool IsOrthonormal()
    {
        for (var i = 0; i < Count; i++)
        {
            var v1 = _vectorList[i];

            var dii = v1.SpSquared() - 1;

            if (!dii.IsNearZero())
                return false;

            for (var j = i + 1; j < Count; j++)
            {
                var dij = v1.Sp(_vectorList[j]);

                if (!dij.IsNearZero())
                    return false;
            }
        }

        return true;
    }

    public bool HasSameHandedness(XGaVectorFrame<T> targetFrame)
    {
        var ps1 = GetSubspace();
        var ps2 = targetFrame.GetSubspace();
        var s = ps1.GetBlade() - ps2.GetBlade();

        //var s = GetPseudoScalar() - targetFrame.GetPseudoScalar();

        return s.IsZero;
    }

    public IEnumerable<XGaRotor<T>> GetRotorsToFrame(XGaVectorFrame<T> targetFrame)
    {
        Debug.Assert(targetFrame.Count == Count);
        Debug.Assert(IsOrthonormal() && targetFrame.IsOrthonormal());
        Debug.Assert(HasSameHandedness(targetFrame));

        var sourceFrame = new XGaVector<T>[Count];

        for (var i = 0; i < Count; i++)
            sourceFrame[i] = _vectorList[i];

        for (var i = 0; i < Count - 1; i++)
        {
            var rotor =
                XGaRotor<T>.CreateEuclideanPureRotor(
                    sourceFrame[i],
                    targetFrame[i]
                );

            yield return rotor;

            for (var j = i + 1; j < Count; j++)
                sourceFrame[j] = rotor.OmMap(sourceFrame[j]);
        }
    }

    public IEnumerable<XGaRotor<T>> GetRotorsToFrame(XGaVectorFrame<T> targetFrame, params int[] basisRotationOrderList)
    {
        Debug.Assert(targetFrame.Count == Count);
        Debug.Assert(IsOrthonormal() && targetFrame.IsOrthonormal());
        Debug.Assert(HasSameHandedness(targetFrame));
        //Debug.Assert(GeoPoTNumUtils.ValidateIndexPermutationList(basisRotationOrderList));

        var sourceFrame = new XGaVector<T>[Count];

        for (var i = 0; i < Count; i++)
            sourceFrame[i] = _vectorList[i];

        for (var i = 0; i < Count - 1; i++)
        {
            var vectorIndex = basisRotationOrderList[i];

            var rotor =
                XGaRotor<T>.CreateEuclideanPureRotor(
                    sourceFrame[vectorIndex],
                    targetFrame[vectorIndex]
                );

            yield return rotor;

            for (var j = i + 1; j < Count; j++)
            {
                var vectorIndex1 = basisRotationOrderList[j];

                sourceFrame[vectorIndex1] =
                    rotor.OmMap(sourceFrame[vectorIndex1]);
            }
        }
    }

    public IEnumerable<LinPolarAngle<T>> GetAnglesToFrame(XGaVectorFrame<T> targetFrame)
    {
        Debug.Assert(targetFrame.Count == Count);

        for (var i = 0; i < Count; i++)
        {
            var v1 = this[i];
            var v2 = targetFrame[i];

            yield return (v1.ESp(v2) / (v1.ESpSquared() * v2.ESpSquared()).Sqrt()).ArcCos();
        }
    }

    public IEnumerable<XGaVectorFrame<T>> GetFramePermutations()
    {
        var indexPermutationsList =
            PermutationsUtils.GetIndexPermutations(Count);

        foreach (var indexPermutation in indexPermutationsList)
        {
            var frame = 
                indexPermutation
                    .Select(index => _vectorList[index])
                    .ToArray();

            yield return new XGaVectorFrame<T>(FrameSpecs, frame);
        }
    }

    public XGaVectorFrame<T> GetProjectionOnFrame(XGaVectorFrame<T> frame)
    {
        var ps = frame.GetSubspace();

        return new XGaVectorFrame<T>(
            XGaVectorFrameSpecs.CreateUndefinedSpecs(),
            _vectorList.Select(v => ps.Project(v)).ToArray()
        );
    }

    public XGaVectorFrame<T> GetUnitNormFrame()
    {
        if (FrameSpecs.UnitNormSquared == true)
            return this;

        var frameSpecs = new XGaVectorFrameSpecs()
        {
            EqualNormSquared = true,
            EqualScalarProduct = FrameSpecs.EqualScalarProduct,
            LinearlyIndependent = FrameSpecs.LinearlyIndependent,
            Orthogonal = FrameSpecs.Orthogonal,
            UnitNormSquared = true
        };

        return new XGaVectorFrame<T>(
            frameSpecs,
            _vectorList.Select(v => 
                v.Divide(v.ENorm().ScalarValue)
            ).ToArray()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaOutermorphism<T> CreateComputedOutermorphism()
    {
        return this
            .Select(v => v.ToLinVector())
            .ToLinUnilinearMap(ScalarProcessor)
            .ToOutermorphism(Processor);
    }

    public T[,] GetArray()
    {
        return GetArray(Count);
    }

    public T[,] GetArray(int rowsCount)
    {
        var colsCount = Count;
        var itemsArray =
            ScalarProcessor.CreateArrayZero2D(rowsCount, colsCount);

        for (var j = 0; j < Count; j++)
        {
            var vectorTerms =
                _vectorList[j]
                    .IndexScalarPairs
                    .Where(pair => pair.Key < rowsCount);

            foreach (var (index, scalar) in vectorTerms)
                itemsArray[index, j] = scalar;
        }

        return itemsArray;
    }

    public T[,] GetInnerProductsArray()
    {
        var ipm = new T[Count, Count];

        for (var i = 0; i < Count; i++)
        {
            var v1 = _vectorList[i];

            ipm[i, i] = v1.SpSquared().ScalarValue;

            for (var j = i + 1; j < Count; j++)
            {
                var ip = v1.Sp(_vectorList[j]).ScalarValue;

                ipm[i, j] = ip;
                ipm[j, i] = ip;
            }
        }

        return ipm;
    }

    public T[,] GetInnerAnglesArray()
    {
        var ipm = new T[Count, Count];

        for (var i = 0; i < Count; i++)
        {
            var v1 = this[i];

            for (var j = i + 1; j < Count; j++)
            {
                var ip = v1.GetEuclideanAngle(this[j]).ScalarValue;

                ipm[i, j] = ip;
                ipm[j, i] = ip;
            }
        }

        return ipm;
    }

    public T[,] GetInnerAnglesInDegreesArray()
    {
        var ipm = new T[Count, Count];

        for (var i = 0; i < Count; i++)
        {
            var v1 = this[i];

            for (var j = i + 1; j < Count; j++)
            {
                var ip = v1.GetEuclideanAngle(this[j]).ScalarValue;

                ipm[i, j] = ip;
                ipm[j, i] = ip;
            }
        }

        return ipm;
    }

    public IEnumerator<XGaVector<T>> GetEnumerator()
    {
        return _vectorList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string ToString()
    {
        var composer = new LinearTextComposer();

        composer
            .AppendLine("Free Frame {")
            .IncreaseIndentation();

        foreach (var vector in _vectorList)
            composer.AppendAtNewLine(vector.ToString());

        composer
            .DecreaseIndentation()
            .AppendAtNewLine("}");

        return composer.ToString();
    }
}