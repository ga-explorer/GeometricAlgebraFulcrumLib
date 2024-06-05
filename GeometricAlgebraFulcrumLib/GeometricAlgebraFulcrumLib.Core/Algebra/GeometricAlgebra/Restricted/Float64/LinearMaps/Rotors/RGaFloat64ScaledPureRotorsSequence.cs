using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Frames;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Subspaces;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Rotors;

public sealed class RGaFloat64ScaledPureRotorsSequence : 
    RGaFloat64ScaledRotorBase, 
    IRGaFloat64OutermorphismSequence,
    IReadOnlyList<RGaFloat64ScaledPureRotor>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ScaledPureRotorsSequence CreateIdentity(RGaFloat64Processor metric)
    {
        return new RGaFloat64ScaledPureRotorsSequence(
            new []{ RGaFloat64ScaledPureRotor.CreateIdentity(metric) }
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ScaledPureRotorsSequence Create(params RGaFloat64ScaledPureRotor[] rotorsList)
    {
        return new RGaFloat64ScaledPureRotorsSequence(rotorsList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ScaledPureRotorsSequence Create(IEnumerable<RGaFloat64ScaledPureRotor> rotorsList)
    {
        return new RGaFloat64ScaledPureRotorsSequence(rotorsList.ToImmutableArray());
    }

    public static RGaFloat64ScaledPureRotorsSequence CreateFromOrthonormalEuclideanFrames(RGaFloat64VectorFrame sourceFrame, RGaFloat64VectorFrame targetFrame, bool fullScaledRotorsFlag = false)
    {
        Debug.Assert(targetFrame.Count == sourceFrame.Count);
        Debug.Assert(sourceFrame.IsOrthonormal() && targetFrame.IsOrthonormal());
        Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));
            
        var rotorsSequence = 
            new List<RGaFloat64ScaledPureRotor>();

        var sourceFrameVectors = sourceFrame.ToArray();

        var n = fullScaledRotorsFlag 
            ? sourceFrame.Count 
            : sourceFrame.Count - 1;
            
        for (var i = 0; i < n; i++)
        {
            var sourceVector = sourceFrameVectors[i];
            var targetVector = targetFrame[i];

            var rotor = 
                sourceVector.CreateScaledPureRotor(targetVector);

            rotorsSequence.Add(rotor);

            for (var j = i + 1; j < sourceFrame.Count; j++)
                sourceFrameVectors[j] = rotor.OmMap(sourceFrameVectors[j]);
        }

        return new RGaFloat64ScaledPureRotorsSequence(rotorsSequence);
    }

    public static RGaFloat64ScaledPureRotorsSequence CreateFromOrthonormalEuclideanFrames(RGaFloat64VectorFrame sourceFrame, RGaFloat64VectorFrame targetFrame, int[] sequenceArray)
    {
        Debug.Assert(targetFrame.Count == sourceFrame.Count);
        Debug.Assert(sourceFrame.IsOrthonormal() && targetFrame.IsOrthonormal());
        Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));

        Debug.Assert(sequenceArray.Length == sourceFrame.Count - 1);
        Debug.Assert(sequenceArray.Min() >= 0);
        Debug.Assert(sequenceArray.Max() < sourceFrame.Count);
        Debug.Assert(sequenceArray.Distinct().Count() == sourceFrame.Count - 1);
            
        var rotorsSequence = 
            new List<RGaFloat64ScaledPureRotor>();
            
        var sourceFrameVectors = sourceFrame.ToArray();
            
        for (var i = 0; i < sourceFrame.Count - 1; i++)
        {
            var vectorIndex = sequenceArray[i];

            var sourceVector = sourceFrameVectors[vectorIndex];
            var targetVector = targetFrame[vectorIndex];

            var rotor = sourceVector.CreateScaledPureRotor(targetVector);

            rotorsSequence.Add(rotor);

            for (var j = i + 1; j < sourceFrame.Count; j++)
                sourceFrameVectors[j] = 
                    rotor.OmMap(sourceFrameVectors[j]);
        }

        return new RGaFloat64ScaledPureRotorsSequence(rotorsSequence);
    }

    public static RGaFloat64ScaledPureRotorsSequence CreateFromEuclideanFrames(int baseSpaceDimensions, RGaFloat64VectorFrame sourceFrame, RGaFloat64VectorFrame targetFrame)
    {
        Debug.Assert(targetFrame.Count == sourceFrame.Count);
        //Debug.Assert(IsOrthonormal() && targetFrame.IsOrthonormal());
        Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));
            
        var rotorsSequence = 
            new List<RGaFloat64ScaledPureRotor>();

        var metric = sourceFrame.Processor;

        var pseudoScalarSubspace = 
            metric.CreatePseudoScalarSubspace(baseSpaceDimensions);

        var sourceFrameVectors = new RGaFloat64Vector[sourceFrame.Count];
        var targetFrameVectors = new RGaFloat64Vector[targetFrame.Count];

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
                sourceVector.CreateScaledPureRotor(targetVector);

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

        return new RGaFloat64ScaledPureRotorsSequence(rotorsSequence);
    }

    //public static PureScaledRotorsSequence<double> CreateOrthogonalScaledRotors(double[,] rotationMatrix)
    //{
    //    var evdSolver = Matrix<double>.Build.DenseOfArray(rotationMatrix).Evd();

    //    var eigenValuesReal = evdSolver.EigenValues.Real();
    //    var eigenValuesImag = evdSolver.EigenValues.Imaginary();
    //    var eigenVectors = evdSolver.EigenVectors;

    //    //TODO: Complete this

    //    return new PureScaledRotorsSequence<double>(
    //        ScalarAlgebraFloat64Processor.Instance.CreateGeometricAlgebraEuclideanProcessor(63)
    //    );
    //}


    private readonly IReadOnlyList<RGaFloat64ScaledPureRotor> _rotorsList;


    public int Count 
        => _rotorsList.Count;

    public RGaFloat64ScaledPureRotor this[int index]
    {
        get => _rotorsList[index];
        //set => _rotorsList[index] = value ?? throw new ArgumentNullException(nameof(value));
    }

        
    private RGaFloat64ScaledPureRotorsSequence(IReadOnlyList<RGaFloat64ScaledPureRotor> rotorsList)
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
    public override RGaFloat64Multivector GetMultivector()
    {
        return _rotorsList
            .Select(r => r.Multivector)
            .Gp();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector GetMultivectorReverse()
    {
        return _rotorsList
            .Select(r => r.MultivectorReverse)
            .Reverse()
            .Gp();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector GetMultivectorInverse()
    {
        return _rotorsList
            .Select(r => r.MultivectorReverse)
            .Reverse()
            .Gp();
    }
        
    public bool ValidateRotation(RGaFloat64VectorFrame sourceFrame, RGaFloat64VectorFrame targetFrame)
    {
        if (sourceFrame.Count != targetFrame.Count)
            return false;

        var rotatedFrame = Rotate(sourceFrame);

        return !rotatedFrame.Select(
            (v, i) => !(targetFrame[i] - v).IsZero
        ).Any();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledPureRotor GetScaledRotor(int index)
    {
        return _rotorsList[index];
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledPureRotorsSequence GetSubSequence(int startIndex, int count)
    {
        return new RGaFloat64ScaledPureRotorsSequence(
            _rotorsList.Skip(startIndex).Take(count).ToImmutableArray()
        );
    }

    public IEnumerable<RGaFloat64Multivector> GetRotations(RGaFloat64Multivector storage)
    {
        var v = storage;

        yield return v;

        foreach (var rotor in _rotorsList)
        {
            v = rotor.Map(v);

            yield return v;
        }
    }

    public IEnumerable<RGaFloat64VectorFrame> GetRotations(RGaFloat64VectorFrame frame)
    {
        var f = frame;

        yield return f;

        foreach (var rotor in _rotorsList)
        {
            f = rotor.OmMap(f);

            yield return f;
        }
    }

    public IEnumerable<double[,]> GetRotationMatrices(int rowsCount)
    {
        var f = 
            Processor.CreateFreeFrameOfBasis(rowsCount);

        yield return f.GetArray(rowsCount);

        foreach (var rotor in _rotorsList)
            yield return rotor.OmMap(f).GetArray(rowsCount);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetScalingFactor()
    {
        return _rotorsList
            .Select(r => r.GetScalingFactor())
            .Aggregate(1d, (d, d1) => d * d1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IRGaFloat64ScaledRotor GetScaledRotorInverse()
    {
        return new RGaFloat64ScaledPureRotorsSequence(
            _rotorsList
                .Select(r => r.GetPureScaledRotorInverse())
                .Reverse()
                .ToImmutableArray()
        );
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Vector OmMap(RGaFloat64Vector mv)
    {
        return _rotorsList
            .Aggregate(
                mv, 
                (bv, rotor) => rotor.OmMap(bv)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Bivector OmMap(RGaFloat64Bivector mv)
    {
        return _rotorsList
            .Aggregate(
                mv, 
                (bv, rotor) => rotor.OmMap(bv)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64HigherKVector OmMap(RGaFloat64HigherKVector mv)
    {
        return _rotorsList
            .Aggregate(
                mv, 
                (kv, rotor) => rotor.OmMap(kv)
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector OmMap(RGaFloat64Multivector mv)
    {
        return _rotorsList
            .Aggregate(
                mv, 
                (current, rotor) => rotor.OmMap(current)
            );
    }

    public override LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
    {
        throw new NotImplementedException();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64VectorFrame Rotate(RGaFloat64VectorFrame frame)
    {
        return _rotorsList
            .Aggregate(
                frame, 
                (current, rotor) => rotor.OmMap(current)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledRotor GetFinalScaledRotor()
    {
        var storage = _rotorsList
            .Skip(1)
            .Select(r => r.Multivector)
            .Aggregate(
                _rotorsList[0].GetMultivector(), 
                (current, rotor) => 
                    rotor.Gp(current)
            );

        return RGaFloat64ScaledRotor.Create(storage);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[,] GetFinalScaledRotorArray(int rowsCount)
    {
        return Rotate(
            Processor.CreateFreeFrameOfBasis(rowsCount)
        ).GetArray(rowsCount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<RGaFloat64ScaledPureRotor> GetEnumerator()
    {
        return _rotorsList.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<IRGaFloat64Outermorphism> GetLeafOutermorphisms()
    {
        return _rotorsList;
    }
}