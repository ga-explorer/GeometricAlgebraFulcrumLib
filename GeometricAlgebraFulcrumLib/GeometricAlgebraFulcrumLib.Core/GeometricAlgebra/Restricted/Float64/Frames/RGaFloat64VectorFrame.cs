using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Subspaces;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Structures.Permutations;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Frames;

public class RGaFloat64VectorFrame :
    IRGaFloat64VectorFrame
{
    internal static RGaFloat64VectorFrame Create(RGaFloat64VectorFrameSpecs frameSpecs, IEnumerable<RGaFloat64Vector> vectorList)
    {
        return new RGaFloat64VectorFrame(
            frameSpecs,
            vectorList.ToArray()
        );
    }


    private readonly IReadOnlyList<RGaFloat64Vector> _vectorList;

        
    public RGaFloat64Processor Processor
        => _vectorList[0].Processor;

    public RGaMetric Metric
        => _vectorList[0].Metric;
        

    public int VSpaceDimensions 
        => _vectorList.GetVSpaceDimensions();
        
    public int Count
        => _vectorList.Count;

    public RGaFloat64Vector this[int index] 
        => _vectorList[index];

    public RGaFloat64VectorFrameSpecs FrameSpecs { get; }

        
    private RGaFloat64VectorFrame(RGaFloat64VectorFrameSpecs frameSpecs, IReadOnlyList<RGaFloat64Vector> vectorList)
    {
        _vectorList = vectorList;
        FrameSpecs = frameSpecs;
    }


    public bool IsValid()
    {
        return _vectorList.All(v => v.IsValid());
    }

    //public RGaVectorFrame AppendVector(RGaVector vector)
    //{
    //    _vectorList.Add(vector);

    //    return this;
    //}

    //public RGaVectorFrame AppendVectors(params RGaVector[] vectorsList)
    //{
    //    foreach (var vector in vectorsList)
    //        _vectorList.Add(vector);

    //    return this;
    //}

    //public RGaVectorFrame PrependVector(RGaVector vector)
    //{
    //    _vectorList.Insert(0, vector);

    //    return this;
    //}

    //public RGaVectorFrame InsertVector(int index, RGaVector vector)
    //{
    //    _vectorList.Insert(index, vector);

    //    return this;
    //}


    public RGaFloat64VectorFrame GetSubFrame(int startIndex, int count)
    {
        return new RGaFloat64VectorFrame(
            FrameSpecs,
            _vectorList.Skip(startIndex).Take(count).ToArray()
        );
    }

    public RGaFloat64VectorFrame GetNegativeFrame()
    {
        return new RGaFloat64VectorFrame(
            FrameSpecs,
            _vectorList.Select(v => v.Negative()).ToArray()
        );
    }

    public RGaFloat64VectorFrame GetOrthonormalFrame()
    {
        if (FrameSpecs.Orthonormal == true)
            return FrameSpecs.UnitNormSquared == true
                ? this
                : GetUnitNormFrame();

        var orthogonalVector = _vectorList[0];
        var vectorStoragesList = new List<RGaFloat64Vector>
        {
            orthogonalVector.Divide(orthogonalVector.Norm().ScalarValue)
        };

        RGaFloat64KVector mv1 = orthogonalVector;

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

        var frameSpecs = new RGaFloat64VectorFrameSpecs()
        {
            EqualNormSquared = true,
            EqualScalarProduct = null,
            LinearlyIndependent = true,
            Orthogonal = true,
            UnitNormSquared = true
        };

        return new RGaFloat64VectorFrame(
            frameSpecs,
            vectorStoragesList
        );
    }

    public RGaFloat64VectorFrame GetOrthogonalFrame()
    {
        if (FrameSpecs.Orthogonal == true)
            return this;

        var orthogonalVector = _vectorList[0];
        var vectorStoragesList = new List<RGaFloat64Vector>
        {
            orthogonalVector
        };

        RGaFloat64KVector mv1 = orthogonalVector;

        for (var i = 1; i < _vectorList.Count; i++)
        {
            var mv2 = mv1.Op(_vectorList[i]);

            orthogonalVector =
                mv1.Reverse().Gp(mv2).GetVectorPart();

            vectorStoragesList.Add(orthogonalVector);

            mv1 = mv2;
        }

        var frameSpecs = new RGaFloat64VectorFrameSpecs()
        {
            EqualNormSquared = null,
            EqualScalarProduct = null,
            LinearlyIndependent = FrameSpecs.LinearlyIndependent,
            Orthogonal = true,
            UnitNormSquared = null
        };

        return new RGaFloat64VectorFrame(
            frameSpecs,
            vectorStoragesList
        );
    }

    public RGaFloat64VectorFrame GetSwappedPairsFrame()
    {
        var frame = new List<RGaFloat64Vector>();

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

        return new RGaFloat64VectorFrame(
            FrameSpecs,
            frame
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Subspace GetSubspace()
    {
        var blade = 
            FrameSpecs.LinearlyIndependent.HasValue && 
            FrameSpecs.LinearlyIndependent.Value
                ? Processor.Op(_vectorList) 
                : Processor.SpanToBlade(_vectorList);

        return new RGaFloat64Subspace(blade);
    }

    public bool IsOrthonormal()
    {
        for (var i = 0; i < Count; i++)
        {
            var v1 = _vectorList[i];

            var dii = v1.SpSquared() - 1d;

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

    public bool HasSameHandedness(RGaFloat64VectorFrame targetFrame)
    {
        var ps1 = GetSubspace();
        var ps2 = targetFrame.GetSubspace();
        var s = ps1.GetBlade() - ps2.GetBlade();

        //var s = GetPseudoScalar() - targetFrame.GetPseudoScalar();

        return s.IsZero;
    }

    public IEnumerable<RGaFloat64Rotor> GetRotorsToFrame(RGaFloat64VectorFrame targetFrame)
    {
        Debug.Assert(targetFrame.Count == Count);
        Debug.Assert(IsOrthonormal() && targetFrame.IsOrthonormal());
        Debug.Assert(HasSameHandedness(targetFrame));

        var sourceFrame = new RGaFloat64Vector[Count];

        for (var i = 0; i < Count; i++)
            sourceFrame[i] = _vectorList[i];

        for (var i = 0; i < Count - 1; i++)
        {
            var rotor =
                RGaFloat64Rotor.CreateEuclideanPureRotor(
                    sourceFrame[i],
                    targetFrame[i]
                );

            yield return rotor;

            for (var j = i + 1; j < Count; j++)
                sourceFrame[j] = rotor.OmMap(sourceFrame[j]);
        }
    }

    public IEnumerable<RGaFloat64Rotor> GetRotorsToFrame(RGaFloat64VectorFrame targetFrame, params int[] basisRotationOrderList)
    {
        Debug.Assert(targetFrame.Count == Count);
        Debug.Assert(IsOrthonormal() && targetFrame.IsOrthonormal());
        Debug.Assert(HasSameHandedness(targetFrame));
        //Debug.Assert(GeoPoTNumUtils.ValidateIndexPermutationList(basisRotationOrderList));

        var sourceFrame = new RGaFloat64Vector[Count];

        for (var i = 0; i < Count; i++)
            sourceFrame[i] = _vectorList[i];

        for (var i = 0; i < Count - 1; i++)
        {
            var vectorIndex = basisRotationOrderList[i];

            var rotor =
                RGaFloat64Rotor.CreateEuclideanPureRotor(
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

    public IEnumerable GetAnglesToFrame(RGaFloat64VectorFrame targetFrame)
    {
        Debug.Assert(targetFrame.Count == Count);

        for (var i = 0; i < Count; i++)
        {
            var v1 = this[i];
            var v2 = targetFrame[i];

            yield return (v1.ESp(v2).Divide(v1.ESpSquared() * v2.ESpSquared()).Sqrt()).ArcCos().ScalarValue;
        }
    }

    public IEnumerable<RGaFloat64VectorFrame> GetFramePermutations()
    {
        var indexPermutationsList =
            PermutationsUtils.GetIndexPermutations(Count);

        foreach (var indexPermutation in indexPermutationsList)
        {
            var frame = 
                indexPermutation
                    .Select(index => _vectorList[index])
                    .ToArray();

            yield return new RGaFloat64VectorFrame(FrameSpecs, frame);
        }
    }

    public RGaFloat64VectorFrame GetProjectionOnFrame(RGaFloat64VectorFrame frame)
    {
        var ps = frame.GetSubspace();

        return new RGaFloat64VectorFrame(
            RGaFloat64VectorFrameSpecs.CreateUndefinedSpecs(),
            _vectorList.Select(v => ps.Project(v)).ToArray()
        );
    }

    public RGaFloat64VectorFrame GetUnitNormFrame()
    {
        if (FrameSpecs.UnitNormSquared == true)
            return this;

        var frameSpecs = new RGaFloat64VectorFrameSpecs()
        {
            EqualNormSquared = true,
            EqualScalarProduct = FrameSpecs.EqualScalarProduct,
            LinearlyIndependent = FrameSpecs.LinearlyIndependent,
            Orthogonal = FrameSpecs.Orthogonal,
            UnitNormSquared = true
        };

        return new RGaFloat64VectorFrame(
            frameSpecs,
            _vectorList.Select(v => 
                v.Divide(v.ENorm().ScalarValue)
            ).ToArray()
        );
    }

    public double[,] GetArray()
    {
        return GetArray(Count);
    }

    public double[,] GetArray(int rowsCount)
    {
        var colsCount = Count;
        var itemsArray =
            new double[rowsCount, colsCount];

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

    public double[,] GetInnerProductsArray()
    {
        var ipm = new double[Count, Count];

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

    public double[,] GetInnerAnglesArray()
    {
        var ipm = new double[Count, Count];

        for (var i = 0; i < Count; i++)
        {
            var v1 = this[i];

            for (var j = i + 1; j < Count; j++)
            {
                var ip = v1.GetEuclideanAngle(this[j]);

                ipm[i, j] = ip;
                ipm[j, i] = ip;
            }
        }

        return ipm;
    }

    public double[,] GetInnerAnglesInDegreesArray()
    {
        var ipm = new double[Count, Count];

        for (var i = 0; i < Count; i++)
        {
            var v1 = this[i];

            for (var j = i + 1; j < Count; j++)
            {
                var ip = v1.GetEuclideanAngle(this[j]);

                ipm[i, j] = ip;
                ipm[j, i] = ip;
            }
        }

        return ipm;
    }

    public IEnumerator<RGaFloat64Vector> GetEnumerator()
    {
        return _vectorList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string ToString()
    {
        var composer = new StringBuilder();

        composer
            .AppendLine("Free Frame {");

        foreach (var vector in _vectorList)
            composer.AppendLine(vector.ToString());

        composer
            .Append("}");

        return composer.ToString();
    }
}