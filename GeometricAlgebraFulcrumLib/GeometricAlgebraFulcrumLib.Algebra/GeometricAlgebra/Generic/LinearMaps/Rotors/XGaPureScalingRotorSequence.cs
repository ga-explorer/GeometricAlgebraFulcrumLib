using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Frames;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Rotors;

public sealed class XGaPureScalingRotorSequence<T> : 
    XGaScalingRotorBase<T>, 
    IXGaOutermorphismSequence<T>,
    IReadOnlyList<XGaPureScalingRotor<T>>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaPureScalingRotorSequence<T> CreateIdentity(XGaProcessor<T> processor)
    {
        return new XGaPureScalingRotorSequence<T>(
            [XGaPureScalingRotor<T>.CreateIdentity(processor)]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaPureScalingRotorSequence<T> Create(IScalarProcessor<T> processor, params XGaPureScalingRotor<T>[] rotorsList)
    {
        return new XGaPureScalingRotorSequence<T>(rotorsList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaPureScalingRotorSequence<T> Create(IScalarProcessor<T> processor, IEnumerable<XGaPureScalingRotor<T>> rotorsList)
    {
        return new XGaPureScalingRotorSequence<T>(rotorsList.ToImmutableArray());
    }

    public static XGaPureScalingRotorSequence<T> CreateFromOrthonormalEuclideanFrames(XGaVectorFrame<T> sourceFrame, XGaVectorFrame<T> targetFrame, bool fullScalingRotorsFlag = false)
    {
        Debug.Assert(targetFrame.Count == sourceFrame.Count);
        Debug.Assert(sourceFrame.IsOrthonormal() && targetFrame.IsOrthonormal());
        Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));
            
        var rotorsSequence = 
            new List<XGaPureScalingRotor<T>>();

        var sourceFrameVectors = sourceFrame.ToArray();

        var n = fullScalingRotorsFlag 
            ? sourceFrame.Count 
            : sourceFrame.Count - 1;
            
        for (var i = 0; i < n; i++)
        {
            var sourceVector = sourceFrameVectors[i];
            var targetVector = targetFrame[i];

            var rotor = 
                sourceVector.CreatePureScalingRotor(targetVector);

            rotorsSequence.Add(rotor);

            for (var j = i + 1; j < sourceFrame.Count; j++)
                sourceFrameVectors[j] = rotor.OmMap(sourceFrameVectors[j]);
        }

        return new XGaPureScalingRotorSequence<T>(rotorsSequence);
    }

    public static XGaPureScalingRotorSequence<T> CreateFromOrthonormalEuclideanFrames(XGaVectorFrame<T> sourceFrame, XGaVectorFrame<T> targetFrame, int[] sequenceArray)
    {
        Debug.Assert(targetFrame.Count == sourceFrame.Count);
        Debug.Assert(sourceFrame.IsOrthonormal() && targetFrame.IsOrthonormal());
        Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));

        Debug.Assert(sequenceArray.Length == sourceFrame.Count - 1);
        Debug.Assert(sequenceArray.Min() >= 0);
        Debug.Assert(sequenceArray.Max() < sourceFrame.Count);
        Debug.Assert(sequenceArray.Distinct().Count() == sourceFrame.Count - 1);
            
        var rotorsSequence = 
            new List<XGaPureScalingRotor<T>>();
            
        var sourceFrameVectors = sourceFrame.ToArray();
            
        for (var i = 0; i < sourceFrame.Count - 1; i++)
        {
            var vectorIndex = sequenceArray[i];

            var sourceVector = sourceFrameVectors[vectorIndex];
            var targetVector = targetFrame[vectorIndex];

            var rotor = sourceVector.CreatePureScalingRotor(targetVector);

            rotorsSequence.Add(rotor);

            for (var j = i + 1; j < sourceFrame.Count; j++)
                sourceFrameVectors[j] = 
                    rotor.OmMap(sourceFrameVectors[j]);
        }

        return new XGaPureScalingRotorSequence<T>(rotorsSequence);
    }

    public static XGaPureScalingRotorSequence<T> CreateFromEuclideanFrames(int baseSpaceDimensions, XGaVectorFrame<T> sourceFrame, XGaVectorFrame<T> targetFrame)
    {
        Debug.Assert(targetFrame.Count == sourceFrame.Count);
        //Debug.Assert(IsOrthonormal() && targetFrame.IsOrthonormal());
        Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));
            
        var rotorsSequence = 
            new List<XGaPureScalingRotor<T>>();

        var metric = sourceFrame.Processor;

        var pseudoScalarSubspace = 
            metric.CreatePseudoScalarSubspace(baseSpaceDimensions);

        var sourceFrameVectors = new XGaVector<T>[sourceFrame.Count];
        var targetFrameVectors = new XGaVector<T>[targetFrame.Count];

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
                sourceVector.CreatePureScalingRotor(targetVector);

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

        return new XGaPureScalingRotorSequence<T>(rotorsSequence);
    }

    //public static PureScalingRotorsSequence<double> CreateOrthogonalScalingRotors(double[,] rotationMatrix)
    //{
    //    var evdSolver = Matrix<double>.Build.DenseOfArray(rotationMatrix).Evd();

    //    var eigenValuesReal = evdSolver.EigenValues.Real();
    //    var eigenValuesImag = evdSolver.EigenValues.Imaginary();
    //    var eigenVectors = evdSolver.EigenVectors;

    //    //TODO: Complete this

    //    return new PureScalingRotorsSequence<double>(
    //        ScalarAlgebraFloat64Processor.Instance.CreateGeometricAlgebraEuclideanProcessor(63)
    //    );
    //}


    private readonly IReadOnlyList<XGaPureScalingRotor<T>> _rotorsList;


    public int Count 
        => _rotorsList.Count;

    public XGaPureScalingRotor<T> this[int index]
    {
        get => _rotorsList[index];
        //set => _rotorsList[index] = value ?? throw new ArgumentNullException(nameof(value));
    }

        
    private XGaPureScalingRotorSequence(IReadOnlyList<XGaPureScalingRotor<T>> rotorsList)
        : base(rotorsList[0].Processor)
    {
        _rotorsList = rotorsList;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return _rotorsList.All(rotor => rotor.IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> GetMultivector()
    {
        return _rotorsList
            .Select(r => r.Multivector)
            .Gp();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> GetMultivectorReverse()
    {
        return _rotorsList
            .Select(r => r.MultivectorReverse)
            .Reverse()
            .Gp();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> GetMultivectorInverse()
    {
        return _rotorsList
            .Select(r => r.MultivectorReverse)
            .Reverse()
            .Gp();
    }
        
    public bool ValidateRotation(XGaVectorFrame<T> sourceFrame, XGaVectorFrame<T> targetFrame)
    {
        if (sourceFrame.Count != targetFrame.Count)
            return false;

        var rotatedFrame = Rotate(sourceFrame);

        return !rotatedFrame.Select(
            (v, i) => !(targetFrame[i] - v).IsZero
        ).Any();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaPureScalingRotor<T> GetScalingRotor(int index)
    {
        return _rotorsList[index];
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaPureScalingRotorSequence<T> GetSubSequence(int startIndex, int count)
    {
        return new XGaPureScalingRotorSequence<T>(
            _rotorsList.Skip(startIndex).Take(count).ToImmutableArray()
        );
    }

    public IEnumerable<XGaMultivector<T>> GetRotations(XGaMultivector<T> storage)
    {
        var v = storage;

        yield return v;

        foreach (var rotor in _rotorsList)
        {
            v = rotor.Map(v);

            yield return v;
        }
    }

    public IEnumerable<XGaVectorFrame<T>> GetRotations(XGaVectorFrame<T> frame)
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
    public override Scalar<T> GetScalingFactor()
    {
        return ScalarProcessor.Times(
            _rotorsList.Select(r => r.GetScalingFactor())
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IXGaScalingRotor<T> GetScalingRotorInverse()
    {
        return new XGaPureScalingRotorSequence<T>(
            _rotorsList
                .Select(r => r.GetPureScalingRotorInverse())
                .Reverse()
                .ToImmutableArray()
        );
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaVector<T> OmMap(XGaVector<T> mv)
    {
        return _rotorsList
            .Aggregate(
                mv, 
                (bv, rotor) => rotor.OmMap(bv)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> OmMap(XGaBivector<T> mv)
    {
        return _rotorsList
            .Aggregate(
                mv, 
                (bv, rotor) => rotor.OmMap(bv)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> OmMap(XGaHigherKVector<T> mv)
    {
        return _rotorsList
            .Aggregate(
                mv, 
                (kv, rotor) => rotor.OmMap(kv)
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> OmMap(XGaMultivector<T> mv)
    {
        return _rotorsList
            .Aggregate(
                mv, 
                (current, rotor) => rotor.OmMap(current)
            );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVectorFrame<T> Rotate(XGaVectorFrame<T> frame)
    {
        return _rotorsList
            .Aggregate(
                frame, 
                (current, rotor) => rotor.OmMap(current)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalingRotor<T> GetFinalScalingRotor()
    {
        var storage = _rotorsList
            .Skip(1)
            .Select(r => r.Multivector)
            .Aggregate(
                _rotorsList[0].GetMultivector(), 
                (current, rotor) => 
                    rotor.Gp(current)
            );

        return XGaScalingRotor<T>.Create(storage);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T[,] GetFinalScalingRotorArray(int rowsCount)
    {
        return Rotate(
            Processor.CreateFreeFrameOfBasis(rowsCount)
        ).GetArray(rowsCount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<XGaPureScalingRotor<T>> GetEnumerator()
    {
        return _rotorsList.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<IXGaOutermorphism<T>> GetLeafOutermorphisms()
    {
        return _rotorsList;
    }
}