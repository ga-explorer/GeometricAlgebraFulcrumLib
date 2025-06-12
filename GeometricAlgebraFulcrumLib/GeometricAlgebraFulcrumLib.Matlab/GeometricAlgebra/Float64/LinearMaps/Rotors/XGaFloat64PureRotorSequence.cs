using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Frames;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Rotors;

public sealed class XGaFloat64PureRotorSequence : 
    XGaFloat64RotorBase, 
    IXGaFloat64OutermorphismSequence,
    IReadOnlyList<XGaFloat64PureRotor>
{
    
    public static XGaFloat64PureRotorSequence CreateIdentity(XGaFloat64Processor metric)
    {
        return new XGaFloat64PureRotorSequence(
            [metric.IdentityRotor()]
        );
    }

    
    public static XGaFloat64PureRotorSequence Create(params XGaFloat64PureRotor[] rotorsList)
    {
        return new XGaFloat64PureRotorSequence(rotorsList);
    }

    
    public static XGaFloat64PureRotorSequence Create(IEnumerable<XGaFloat64PureRotor> rotorsList)
    {
        return new XGaFloat64PureRotorSequence(rotorsList.ToArray());
    }

    public static XGaFloat64PureRotorSequence CreateFromOrthonormalEuclideanFrames(XGaFloat64VectorFrame sourceFrame, XGaFloat64VectorFrame targetFrame, bool fullRotorsFlag = false)
    {
        Debug.Assert(targetFrame.Count == sourceFrame.Count);
        Debug.Assert(sourceFrame.IsOrthonormal() && targetFrame.IsOrthonormal());
        Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));
            
        var rotorsSequence = new List<XGaFloat64PureRotor>();

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

        return new XGaFloat64PureRotorSequence(rotorsSequence);
    }

    public static XGaFloat64PureRotorSequence CreateFromOrthonormalEuclideanFrames(XGaFloat64VectorFrame sourceFrame, XGaFloat64VectorFrame targetFrame, int[] sequenceArray)
    {
        Debug.Assert(targetFrame.Count == sourceFrame.Count);
        Debug.Assert(sourceFrame.IsOrthonormal() && targetFrame.IsOrthonormal());
        Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));

        Debug.Assert(sequenceArray.Length == sourceFrame.Count - 1);
        Debug.Assert(sequenceArray.Min() >= 0);
        Debug.Assert(sequenceArray.Max() < sourceFrame.Count);
        Debug.Assert(sequenceArray.Distinct().Count() == sourceFrame.Count - 1);
            
        var rotorsSequence = new List<XGaFloat64PureRotor>();

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

        return new XGaFloat64PureRotorSequence(rotorsSequence);
    }

    public static XGaFloat64PureRotorSequence CreateFromEuclideanFrames(int baseSpaceDimensions, XGaFloat64VectorFrame sourceFrame, XGaFloat64VectorFrame targetFrame)
    {
        Debug.Assert(targetFrame.Count == sourceFrame.Count);
        //Debug.Assert(IsOrthonormal() && targetFrame.IsOrthonormal());
        Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));

        var metric = sourceFrame.Processor;

        var rotorsSequence = new List<XGaFloat64PureRotor>();

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

        return new XGaFloat64PureRotorSequence(rotorsSequence);
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


    private readonly IReadOnlyList<XGaFloat64PureRotor> _rotorsList;


    public int Count 
        => _rotorsList.Count;

    public XGaFloat64PureRotor this[int index]
    {
        get => _rotorsList[index];
        //set => _rotorsList[index] = value ?? throw new ArgumentNullException(nameof(value));
    }


    private XGaFloat64PureRotorSequence(IReadOnlyList<XGaFloat64PureRotor> rotorsList)
        : base(rotorsList[0].Processor)
    {
        Debug.Assert(rotorsList.Count > 0);

        _rotorsList = rotorsList;
    }

        
    
    public override bool IsValid()
    {
        return _rotorsList.All(rotor => rotor.IsValid());
    }

    
    public override XGaFloat64Multivector GetMultivector()
    {
        return _rotorsList
            .Select(r => r.Multivector)
            .Gp();
    }

    
    public override XGaFloat64Multivector GetMultivectorReverse()
    {
        return _rotorsList
            .Select(r => r.Multivector)
            .Reverse()
            .Gp();
    }

    
    public override XGaFloat64Multivector GetMultivectorInverse()
    {
        return GetMultivectorReverse();
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
        

    
    public XGaFloat64PureRotor GetRotor(int index)
    {
        return _rotorsList[index];
    }
        
    
    public XGaFloat64PureRotorSequence GetSubSequence(int startIndex, int count)
    {
        return new XGaFloat64PureRotorSequence(
            _rotorsList.Skip(startIndex).Take(count).ToArray()
        );
    }

    public IEnumerable<XGaFloat64Multivector> GetRotations(XGaFloat64Multivector mv)
    {
        var v = mv;

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
        

    
    public override IXGaFloat64Rotor GetRotorInverse()
    {
        return new XGaFloat64PureRotorSequence(
            _rotorsList
                .Select(r => r.GetPureRotorInverse())
                .Reverse()
                .ToArray()
        );
    }
        

    
    public override XGaFloat64Vector OmMap(XGaFloat64Vector mv)
    {
        return _rotorsList
            .Aggregate(
                mv, 
                (bv, rotor) => rotor.OmMap(bv)
            );
    }

    
    public override XGaFloat64Bivector OmMap(XGaFloat64Bivector mv)
    {
        return _rotorsList
            .Aggregate(
                mv, 
                (bv, rotor) => rotor.OmMap(bv)
            );
    }

    
    public override XGaFloat64HigherKVector OmMap(XGaFloat64HigherKVector mv)
    {
        return _rotorsList
            .Aggregate(
                mv, 
                (kv, rotor) => rotor.OmMap(kv)
            );
    }
        
    
    public override XGaFloat64Multivector OmMap(XGaFloat64Multivector mv)
    {
        return _rotorsList
            .Aggregate(
                mv, 
                (current, rotor) => rotor.OmMap(current)
            );
    }


    
    public XGaFloat64VectorFrame Rotate(XGaFloat64VectorFrame frame)
    {
        return _rotorsList
            .Aggregate(
                frame, 
                (current, rotor) => rotor.OmMap(current)
            );
    }

    
    public XGaFloat64Rotor GetFinalRotor()
    {
        var storage = _rotorsList
            .Skip(1)
            .Select(r => r.Multivector)
            .Aggregate(
                _rotorsList[0].Multivector, 
                (current, rotor) => 
                    rotor.Gp(current)
            );

        return XGaFloat64Rotor.Create(storage);
    }

    
    public double[,] GetFinalRotorArray(int rowsCount)
    {
        return Rotate(
            Processor.CreateFreeFrameOfBasis(rowsCount)
        ).GetArray(rowsCount);
    }

    
    public IEnumerator<XGaFloat64PureRotor> GetEnumerator()
    {
        return _rotorsList.GetEnumerator();
    }

    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    
    public IEnumerable<IXGaFloat64Outermorphism> GetLeafOutermorphisms()
    {
        return _rotorsList;
    }

    
    public override double GetScalingFactor()
    {
        return 1d;
    }
}