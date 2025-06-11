using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Spaces.Conformal;

public class XGaConformalSpace<T> :
    XGaSpace<T>
{
    public override int VSpaceDimensions { get; }

    public XGaConformalProcessor<T> ConformalProcessor { get; }

    public override XGaProcessor<T> Processor
        => ConformalProcessor;
    
    public XGaVector<T> OriginBasisVector { get; }

    public XGaVector<T> InfinityBasisVector { get; }


    internal XGaConformalSpace(XGaConformalProcessor<T> processor, int vSpaceDimensions)
    {
        if (vSpaceDimensions < 2)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        ConformalProcessor = processor;
        VSpaceDimensions = vSpaceDimensions;

        var scalarOne = ScalarProcessor.OneValue;

        OriginBasisVector = 
            processor
                .CreateVectorComposer()
                .SetVectorTerm(0, ScalarProcessor.DivideTwo(scalarOne).ScalarValue)
                .SetVectorTerm(1, ScalarProcessor.DivideMinusTwo(scalarOne).ScalarValue)
                .GetVector();
        
        InfinityBasisVector = 
            processor
                .CreateVectorComposer()
                .SetVectorTerm(0, scalarOne)
                .SetVectorTerm(1, scalarOne)
                .GetVector();
    }


    /// <summary>
    /// Convert a Euclidean position vector into a conformal null vector
    /// </summary>
    /// <param name="positionVector"></param>
    /// <returns></returns>
    public XGaConformalIpnsPoint<T> CreateIpnsPoint(params T[] positionVector)
    {
        if (positionVector.Length > VSpaceDimensions - 2)
            throw new InvalidOperationException();

        var processor = Processor;
        var scalarProcessor = ScalarProcessor;
        var composer = processor.CreateVectorComposer();
            
        var x2 = 
            positionVector
                .Select(v => scalarProcessor.Times(v, v).ScalarValue)
                .Aggregate(scalarProcessor.ZeroValue, (scalar1, scalar2) => scalarProcessor.Add(scalar1, scalar2).ScalarValue);

        // e+ part
        composer.SetVectorTerm(
            0, 
            scalarProcessor.DivideTwo(scalarProcessor.SubtractOne(x2).ScalarValue)
        );

        // e- part
        composer.SetVectorTerm(
            1, 
            scalarProcessor.DivideTwo(scalarProcessor.AddOne(x2).ScalarValue)
        );

        // Euclidean part
        composer.SetVectorTerms(2, positionVector);
            
        return new XGaConformalIpnsPoint<T>(
            this,
            composer.GetVector()
        );
    }

    public XGaConformalIpnsPoint<T> CreateIpnsPoint(XGaVector<T> positionVector)
    {
        if (positionVector.Ids.Max(k => k.LastIndex) > VSpaceDimensions - 2)
            throw new InvalidOperationException();
            
        var processor = Processor;
        var scalarProcessor = ScalarProcessor;
        var composer = processor.CreateVectorComposer();

        var x2 = scalarProcessor.AddSquares(positionVector.Scalars).ScalarValue;
            
        // e+ part
        composer.SetVectorTerm(
            0, 
            scalarProcessor.DivideTwo(scalarProcessor.SubtractOne(x2).ScalarValue)
        );

        // e- part
        composer.SetVectorTerm(
            1, 
            scalarProcessor.DivideTwo(scalarProcessor.AddOne(x2).ScalarValue)
        );

        // Euclidean part
        composer.SetTerms(
            positionVector.Select(p => 
                new KeyValuePair<IndexSet, T>(
                    p.Key.ShiftIndices(2), 
                    p.Value
                )
            )
        );
            
        return new XGaConformalIpnsPoint<T>(
            this,
            composer.GetVector()
        );
    }
        
    public XGaConformalIpnsHyperSphere<T> CreateIpnsHyperSphere(XGaVector<T> centerPoint, double radiusSquared)
    {
        return CreateIpnsHyperSphere(
            centerPoint,
            ScalarProcessor.ScalarFromNumber(radiusSquared)
        );
    }

    public XGaConformalIpnsHyperSphere<T> CreateIpnsHyperSphere(XGaVector<T> centerPoint, string radiusSquared)
    {
        return CreateIpnsHyperSphere(
            centerPoint,
            ScalarProcessor.ScalarFromText(radiusSquared)
        );
    }

    public XGaConformalIpnsHyperSphere<T> CreateIpnsHyperSphere(XGaVector<T> centerPoint, IScalar<T> radiusSquared)
    {
        if (centerPoint.Ids.Max(k => k.LastIndex) > VSpaceDimensions - 2)
            throw new InvalidOperationException();
            
        var processor = Processor;
        var scalarProcessor = ScalarProcessor;
        var composer = processor.CreateVectorComposer();

        var x2 = scalarProcessor.AddSquares(centerPoint.Scalars) - radiusSquared;
            
        // e+ part
        composer.SetVectorTerm(
            0, 
            scalarProcessor.DivideTwo(scalarProcessor.SubtractOne(x2.ScalarValue).ScalarValue)
        );

        // e- part
        composer.SetVectorTerm(
            1, 
            scalarProcessor.DivideTwo(scalarProcessor.AddOne(x2.ScalarValue).ScalarValue)
        );

        // Euclidean part
        composer.SetTerms(
            centerPoint.Select(p => 
                new KeyValuePair<IndexSet, T>(
                    p.Key.ShiftIndices(2), 
                    p.Value
                )
            )
        );

        return new XGaConformalIpnsHyperSphere<T>(
            this,
            composer.GetVector()
        );
    }
        
    public XGaConformalIpnsHyperPlane<T> CreateIpnsHyperPlane(XGaVector<T> normal, double delta)
    {
        return CreateIpnsHyperPlane(
            normal,
            ScalarProcessor.ValueFromNumber(delta)
        );
    }

    public XGaConformalIpnsHyperPlane<T> CreateIpnsHyperPlane(XGaVector<T> normal, string delta)
    {
        return CreateIpnsHyperPlane(
            normal,
            ScalarProcessor.ValueFromText(delta)
        );
    }

    public XGaConformalIpnsHyperPlane<T> CreateIpnsHyperPlane(XGaVector<T> normal, T delta)
    {
        if (normal.Ids.Max(k => k.LastIndex) > VSpaceDimensions - 2)
            throw new InvalidOperationException();
            
        var processor = Processor;
        var composer = processor.CreateVectorComposer();
            
        // e+ part
        composer.SetVectorTerm(0, delta);

        // e- part
        composer.SetVectorTerm(1, delta);
            
        // Euclidean part
        composer.SetTerms(
            normal.Select(p => 
                new KeyValuePair<IndexSet, T>(
                    p.Key.ShiftIndices(2), 
                    p.Value
                )
            )
        );

        return new XGaConformalIpnsHyperPlane<T>(
            this,
            composer.GetVector()
        );
    }


    public XGaConformalOpnsRound<T> CreateOpnsRound(params XGaVector<T>[] points)
    {
        if (points.Length < 2 || points.Length > VSpaceDimensions - 1)
            throw new InvalidOperationException();

        var kVector = 
            points
                .Select(p => CreateIpnsPoint(p).Vector)
                .Op(Processor);

        return new XGaConformalOpnsRound<T>(this, kVector);
    }

    public XGaConformalOpnsHyperSphere<T> CreateOpnsHyperSphere(params XGaVector<T>[] points)
    {
        if (points.Length != VSpaceDimensions - 1)
            throw new InvalidOperationException();

        var kVector = 
            points
                .Select(p => CreateIpnsPoint(p).Vector)
                .Op(Processor)
                .Op(InfinityBasisVector);

        return new XGaConformalOpnsHyperSphere<T>(this, kVector);
    }

    public XGaConformalOpnsFlat<T> CreateOpnsFlat(params XGaVector<T>[] points)
    {
        if (points.Length < 2 || points.Length >= VSpaceDimensions - 1)
            throw new InvalidOperationException();

        var kVector = 
            points
                .Select(p => CreateIpnsPoint(p).Vector)
                .Op(Processor)
                .Op(InfinityBasisVector);

        return new XGaConformalOpnsFlat<T>(this, kVector);
    }

    public XGaConformalOpnsFlat<T> CreateOpnsFlat(XGaVector<T> positionVector, XGaKVector<T> directionBlade)
    {
        if (positionVector.VSpaceDimensions > VSpaceDimensions - 2)
            throw new InvalidOperationException();

        if (directionBlade.VSpaceDimensions > VSpaceDimensions - 2)
            throw new InvalidOperationException();

        var kVector = 
            positionVector
                .Op(directionBlade)
                .Op(InfinityBasisVector);

        return new XGaConformalOpnsFlat<T>(this, kVector);
    }

    public XGaConformalOpnsDirection<T> CreateOpnsDirection(XGaKVector<T> directionBlade)
    {
        if (directionBlade.VSpaceDimensions > VSpaceDimensions - 2)
            throw new InvalidOperationException();

        var kVector = directionBlade.Op(InfinityBasisVector);

        return new XGaConformalOpnsDirection<T>(this, kVector);
    }

    public XGaConformalOpnsHyperPlane<T> CreateOpnsHyperPlane(params XGaVector<T>[] points)
    {
        if (points.Length != VSpaceDimensions - 2)
            throw new InvalidOperationException();

        var kVector = 
            points
                .Select(p => CreateIpnsPoint(p).Vector)
                .Op(Processor)
                .Op(InfinityBasisVector);

        return new XGaConformalOpnsHyperPlane<T>(this, kVector);
    }
}