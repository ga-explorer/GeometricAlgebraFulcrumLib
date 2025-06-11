using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Frames;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps.Rotors;

public sealed class XGaFloat64PureScalingRotorSequence : 
    XGaFloat64ScalingRotorBase, 
    IXGaFloat64OutermorphismSequence,
    IReadOnlyList<XGaFloat64PureScalingRotor>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64PureScalingRotorSequence CreateIdentity(XGaFloat64Processor metric)
    {
        return new XGaFloat64PureScalingRotorSequence(
            [XGaFloat64PureScalingRotor.CreateIdentity(metric)]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64PureScalingRotorSequence Create(params XGaFloat64PureScalingRotor[] rotorsList)
    {
        return new XGaFloat64PureScalingRotorSequence(rotorsList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64PureScalingRotorSequence Create(IEnumerable<XGaFloat64PureScalingRotor> rotorsList)
    {
        return new XGaFloat64PureScalingRotorSequence(rotorsList.ToImmutableArray());
    }

    public static XGaFloat64PureScalingRotorSequence CreateFromOrthonormalEuclideanFrames(XGaFloat64VectorFrame sourceFrame, XGaFloat64VectorFrame targetFrame, bool fullScalingRotorsFlag = false)
    {
        Debug.Assert(targetFrame.Count == sourceFrame.Count);
        Debug.Assert(sourceFrame.IsOrthonormal() && targetFrame.IsOrthonormal());
        Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));
            
        var rotorsSequence = 
            new List<XGaFloat64PureScalingRotor>();

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

        return new XGaFloat64PureScalingRotorSequence(rotorsSequence);
    }

    public static XGaFloat64PureScalingRotorSequence CreateFromOrthonormalEuclideanFrames(XGaFloat64VectorFrame sourceFrame, XGaFloat64VectorFrame targetFrame, int[] sequenceArray)
    {
        Debug.Assert(targetFrame.Count == sourceFrame.Count);
        Debug.Assert(sourceFrame.IsOrthonormal() && targetFrame.IsOrthonormal());
        Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));

        Debug.Assert(sequenceArray.Length == sourceFrame.Count - 1);
        Debug.Assert(sequenceArray.Min() >= 0);
        Debug.Assert(sequenceArray.Max() < sourceFrame.Count);
        Debug.Assert(sequenceArray.Distinct().Count() == sourceFrame.Count - 1);
            
        var rotorsSequence = 
            new List<XGaFloat64PureScalingRotor>();
            
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

        return new XGaFloat64PureScalingRotorSequence(rotorsSequence);
    }

    public static XGaFloat64PureScalingRotorSequence CreateFromEuclideanFrames(int baseSpaceDimensions, XGaFloat64VectorFrame sourceFrame, XGaFloat64VectorFrame targetFrame)
    {
        Debug.Assert(targetFrame.Count == sourceFrame.Count);
        //Debug.Assert(IsOrthonormal() && targetFrame.IsOrthonormal());
        Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));
            
        var rotorsSequence = 
            new List<XGaFloat64PureScalingRotor>();

        var metric = sourceFrame.Processor;

        var pseudoScalarSubspace = 
            metric.CreatePseudoScalarSubspace(baseSpaceDimensions);

        var sourceFrameVectors = new XGaFloat64Vector[sourceFrame.Count];
        var targetFrameVectors = new XGaFloat64Vector[targetFrame.Count];

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

        return new XGaFloat64PureScalingRotorSequence(rotorsSequence);
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


    private readonly IReadOnlyList<XGaFloat64PureScalingRotor> _rotorsList;


    public int Count 
        => _rotorsList.Count;

    public XGaFloat64PureScalingRotor this[int index]
    {
        get => _rotorsList[index];
        //set => _rotorsList[index] = value ?? throw new ArgumentNullException(nameof(value));
    }

        
    private XGaFloat64PureScalingRotorSequence(IReadOnlyList<XGaFloat64PureScalingRotor> rotorsList)
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
    public override XGaFloat64Multivector GetMultivector()
    {
        return _rotorsList
            .Select(r => r.Multivector)
            .Gp();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector GetMultivectorReverse()
    {
        return _rotorsList
            .Select(r => r.MultivectorReverse)
            .Reverse()
            .Gp();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector GetMultivectorInverse()
    {
        return _rotorsList
            .Select(r => r.MultivectorReverse)
            .Reverse()
            .Gp();
    }
        
    public bool ValidateRotation(XGaFloat64VectorFrame sourceFrame, XGaFloat64VectorFrame targetFrame)
    {
        if (sourceFrame.Count != targetFrame.Count)
            return false;

        var rotatedFrame = Rotate(sourceFrame);

        return !rotatedFrame.Select(
            (v, i) => !(targetFrame[i] - v).IsZero
        ).Any();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64PureScalingRotor GetScalingRotor(int index)
    {
        return _rotorsList[index];
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64PureScalingRotorSequence GetSubSequence(int startIndex, int count)
    {
        return new XGaFloat64PureScalingRotorSequence(
            _rotorsList.Skip(startIndex).Take(count).ToImmutableArray()
        );
    }

    public IEnumerable<XGaFloat64Multivector> GetRotations(XGaFloat64Multivector storage)
    {
        var v = storage;

        yield return v;

        foreach (var rotor in _rotorsList)
        {
            v = rotor.Map(v);

            yield return v;
        }
    }

    public IEnumerable<XGaFloat64VectorFrame> GetRotations(XGaFloat64VectorFrame frame)
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
    public override IXGaFloat64ScalingRotor GetScalingRotorInverse()
    {
        return new XGaFloat64PureScalingRotorSequence(
            _rotorsList
                .Select(r => r.GetPureScalingRotorInverse())
                .Reverse()
                .ToImmutableArray()
        );
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Vector OmMap(XGaFloat64Vector mv)
    {
        return _rotorsList
            .Aggregate(
                mv, 
                (bv, rotor) => rotor.OmMap(bv)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector OmMap(XGaFloat64Bivector mv)
    {
        return _rotorsList
            .Aggregate(
                mv, 
                (bv, rotor) => rotor.OmMap(bv)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64HigherKVector OmMap(XGaFloat64HigherKVector mv)
    {
        return _rotorsList
            .Aggregate(
                mv, 
                (kv, rotor) => rotor.OmMap(kv)
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector OmMap(XGaFloat64Multivector mv)
    {
        return _rotorsList
            .Aggregate(
                mv, 
                (current, rotor) => rotor.OmMap(current)
            );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64VectorFrame Rotate(XGaFloat64VectorFrame frame)
    {
        return _rotorsList
            .Aggregate(
                frame, 
                (current, rotor) => rotor.OmMap(current)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64ScalingRotor GetFinalScalingRotor()
    {
        var storage = _rotorsList
            .Skip(1)
            .Select(r => r.Multivector)
            .Aggregate(
                _rotorsList[0].GetMultivector(), 
                (current, rotor) => 
                    rotor.Gp(current)
            );

        return XGaFloat64ScalingRotor.Create(storage);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[,] GetFinalScalingRotorArray(int rowsCount)
    {
        return Rotate(
            Processor.CreateFreeFrameOfBasis(rowsCount)
        ).GetArray(rowsCount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<XGaFloat64PureScalingRotor> GetEnumerator()
    {
        return _rotorsList.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<IXGaFloat64Outermorphism> GetLeafOutermorphisms()
    {
        return _rotorsList;
    }
}