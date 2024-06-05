using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Frames;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Subspaces;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.LinearMaps.Rotors;

public sealed class RGaPureRotorsSequence<T> : 
    RGaRotorBase<T>, 
    IRGaOutermorphismSequence<T>,
    IReadOnlyList<RGaPureRotor<T>>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaPureRotorsSequence<T> CreateIdentity(RGaProcessor<T> processor)
    {
        return new RGaPureRotorsSequence<T>(
            new[]{ processor.CreateIdentityRotor() }
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaPureRotorsSequence<T> Create(params RGaPureRotor<T>[] rotorsList)
    {
        return new RGaPureRotorsSequence<T>(rotorsList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaPureRotorsSequence<T> Create(IEnumerable<RGaPureRotor<T>> rotorsList)
    {
        return new RGaPureRotorsSequence<T>(rotorsList.ToImmutableArray());
    }

    public static RGaPureRotorsSequence<T> CreateFromOrthonormalEuclideanFrames(RGaVectorFrame<T> sourceFrame, RGaVectorFrame<T> targetFrame, bool fullRotorsFlag = false)
    {
        Debug.Assert(targetFrame.Count == sourceFrame.Count);
        Debug.Assert(sourceFrame.IsOrthonormal() && targetFrame.IsOrthonormal());
        Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));
            
        var rotorsSequence = new List<RGaPureRotor<T>>();

        var sourceFrameVectors = sourceFrame.ToArray();

        var n = fullRotorsFlag 
            ? sourceFrame.Count 
            : sourceFrame.Count - 1;
            
        for (var i = 0; i < n; i++)
        {
            var sourceVector = sourceFrameVectors[i];
            var targetVector = targetFrame[i];

            var rotor = 
                sourceVector.CreatePureRotor(targetVector);

            rotorsSequence.Add(rotor);

            for (var j = i + 1; j < sourceFrame.Count; j++)
                sourceFrameVectors[j] = rotor.OmMap(sourceFrameVectors[j]);
        }

        return new RGaPureRotorsSequence<T>(rotorsSequence);
    }

    public static RGaPureRotorsSequence<T> CreateFromOrthonormalEuclideanFrames(RGaVectorFrame<T> sourceFrame, RGaVectorFrame<T> targetFrame, int[] sequenceArray)
    {
        Debug.Assert(targetFrame.Count == sourceFrame.Count);
        Debug.Assert(sourceFrame.IsOrthonormal() && targetFrame.IsOrthonormal());
        Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));

        Debug.Assert(sequenceArray.Length == sourceFrame.Count - 1);
        Debug.Assert(sequenceArray.Min() >= 0);
        Debug.Assert(sequenceArray.Max() < sourceFrame.Count);
        Debug.Assert(sequenceArray.Distinct().Count() == sourceFrame.Count - 1);
            
        var rotorsSequence = new List<RGaPureRotor<T>>();

        var sourceFrameVectors = sourceFrame.ToArray();
            
        for (var i = 0; i < sourceFrame.Count - 1; i++)
        {
            var vectorIndex = sequenceArray[i];

            var sourceVector = sourceFrameVectors[vectorIndex];
            var targetVector = targetFrame[vectorIndex];

            var rotor = sourceVector.CreatePureRotor(targetVector);

            rotorsSequence.Add(rotor);

            for (var j = i + 1; j < sourceFrame.Count; j++)
                sourceFrameVectors[j] = 
                    rotor.OmMap(sourceFrameVectors[j]);
        }

        return new RGaPureRotorsSequence<T>(rotorsSequence);
    }

    public static RGaPureRotorsSequence<T> CreateFromEuclideanFrames(int baseSpaceDimensions, RGaVectorFrame<T> sourceFrame, RGaVectorFrame<T> targetFrame)
    {
        Debug.Assert(targetFrame.Count == sourceFrame.Count);
        //Debug.Assert(IsOrthonormal() && targetFrame.IsOrthonormal());
        Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));

        var metric = sourceFrame.Processor;

        var rotorsSequence = new List<RGaPureRotor<T>>();

        var pseudoScalarSubspace = 
            metric.CreatePseudoScalarSubspace(baseSpaceDimensions);

        var sourceFrameVectors = new RGaVector<T>[sourceFrame.Count];
        var targetFrameVectors = new RGaVector<T>[targetFrame.Count];

        for (var i = 0; i < sourceFrame.Count; i++)
        {
            sourceFrameVectors[i] = sourceFrame[i];
            targetFrameVectors[i] = targetFrame[i];
        }
            
        for (var i = 0U; i < sourceFrame.Count - 1; i++)
        {
            var sourceVector = sourceFrameVectors[i];
            var targetVector = targetFrameVectors[i];

            var rotor = 
                sourceVector.CreatePureRotor(targetVector);

            rotorsSequence.Add(rotor);

            pseudoScalarSubspace = 
                pseudoScalarSubspace.Complement(targetVector).ToSubspace();

            for (var j = i + 1; j < sourceFrame.Count; j++)
            {
                sourceFrameVectors[j] =
                    pseudoScalarSubspace
                        .Project(rotor.OmMap(sourceFrameVectors[j]));

                targetFrameVectors[j] =
                    pseudoScalarSubspace
                        .Project(targetFrameVectors[j]);
            }
        }

        return new RGaPureRotorsSequence<T>(rotorsSequence);
    }

    //public static PureRotorsSequence<double> CreateOrthogonalRotors(double[,] rotationMatrix)
    //{
    //    var evdSolver = Matrix<double>.Build.DenseOfArray(rotationMatrix).Evd();

    //    var eigenValuesReal = evdSolver.EigenValues.Real();
    //    var eigenValuesImag = evdSolver.EigenValues.Imaginary();
    //    var eigenVectors = evdSolver.EigenVectors;

    //    //TODO: Complete this

    //    return new PureRotorsSequence<double>(
    //        ScalarAlgebraFloat64Processor.Instance.CreateGeometricAlgebraEuclideanProcessor(63)
    //    );
    //}


    private readonly IReadOnlyList<RGaPureRotor<T>> _rotorsList;


    public int Count 
        => _rotorsList.Count;

    public RGaPureRotor<T> this[int index] 
        => _rotorsList[index];


    private RGaPureRotorsSequence(IReadOnlyList<RGaPureRotor<T>> rotorsList)
        : base(rotorsList[0].Processor)
    {
        Debug.Assert(rotorsList.Count > 0);

        _rotorsList = rotorsList;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return _rotorsList.All(rotor => rotor.IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> GetMultivector()
    {
        return _rotorsList
            .Select(r => r.Multivector)
            .Gp();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> GetMultivectorReverse()
    {
        return _rotorsList
            .Select(r => r.Multivector)
            .Reverse()
            .Gp();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> GetMultivectorInverse()
    {
        return GetMultivectorReverse();
    }
        
    public bool ValidateRotation(RGaVectorFrame<T> sourceFrame, RGaVectorFrame<T> targetFrame)
    {
        if (sourceFrame.Count != targetFrame.Count)
            return false;

        var rotatedFrame = Rotate(sourceFrame);

        return !rotatedFrame.Select(
            (v, i) => !(targetFrame[i] - v).IsZero
        ).Any();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaPureRotor<T> GetRotor(int index)
    {
        return _rotorsList[index];
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaPureRotorsSequence<T> GetSubSequence(int startIndex, int count)
    {
        return new RGaPureRotorsSequence<T>(
            _rotorsList.Skip(startIndex).Take(count).ToImmutableArray()
        );
    }

    public IEnumerable<RGaMultivector<T>> GetRotations(RGaMultivector<T> mv)
    {
        var v = mv;

        yield return v;

        foreach (var rotor in _rotorsList)
        {
            v = rotor.Map(v);

            yield return v;
        }
    }

    public IEnumerable<RGaVectorFrame<T>> GetRotations(RGaVectorFrame<T> frame)
    {
        var f = frame;

        yield return f;

        foreach (var rotor in _rotorsList)
        {
            f = rotor.OmMap(f);

            yield return f;
        }
    }

    public IEnumerable<T[,]> GetRotationMatrices(int rowsCount)
    {
        var f = 
            Processor.CreateFreeFrameOfBasis(rowsCount);

        yield return f.GetArray(rowsCount);

        foreach (var rotor in _rotorsList)
            yield return rotor.OmMap(f).GetArray(rowsCount);
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IRGaRotor<T> GetRotorInverse()
    {
        return new RGaPureRotorsSequence<T>(
            _rotorsList
                .Select(r => r.GetPureRotorInverse())
                .Reverse()
                .ToImmutableArray()
        );
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaVector<T> OmMap(RGaVector<T> mv)
    {
        return _rotorsList
            .Aggregate(
                mv, 
                (bv, rotor) => rotor.OmMap(bv)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaBivector<T> OmMap(RGaBivector<T> mv)
    {
        return _rotorsList
            .Aggregate(
                mv, 
                (bv, rotor) => rotor.OmMap(bv)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaHigherKVector<T> OmMap(RGaHigherKVector<T> mv)
    {
        return _rotorsList
            .Aggregate(
                mv, 
                (kv, rotor) => rotor.OmMap(kv)
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> OmMap(RGaMultivector<T> mv)
    {
        return _rotorsList
            .Aggregate(
                mv, 
                (current, rotor) => rotor.OmMap(current)
            );
    }

    public override LinUnilinearMap<T> GetVectorMapPart(int vSpaceDimensions)
    {
        throw new NotImplementedException();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVectorFrame<T> Rotate(RGaVectorFrame<T> frame)
    {
        return _rotorsList
            .Aggregate(
                frame, 
                (current, rotor) => rotor.OmMap(current)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaRotor<T> GetFinalRotor()
    {
        var storage = _rotorsList
            .Skip(1)
            .Select(r => r.Multivector)
            .Aggregate(
                _rotorsList[0].Multivector, 
                (current, rotor) => 
                    rotor.Gp(current)
            );

        return RGaRotor<T>.Create(storage);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T[,] GetFinalRotorArray(int rowsCount)
    {
        return Rotate(
            Processor.CreateFreeFrameOfBasis(rowsCount)
        ).GetArray(rowsCount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<RGaPureRotor<T>> GetEnumerator()
    {
        return _rotorsList.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<IRGaOutermorphism<T>> GetLeafOutermorphisms()
    {
        return _rotorsList;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Scalar<T> GetScalingFactor()
    {
        return ScalarProcessor.One;
    }
}