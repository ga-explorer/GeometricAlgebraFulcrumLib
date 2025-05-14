using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Spaces.Conformal;

public static class XGaConformalComposerUtils
{
    /// <summary>
    /// Convert a Euclidean position vector into a conformal null vector
    /// </summary>
    /// <param name="space"></param>
    /// <param name="positionVector"></param>
    /// <returns></returns>
    public static XGaConformalIpnsPoint<T> CreateIpnsPoint<T>(this XGaConformalSpace<T> space, params T[] positionVector)
    {
        if (positionVector.Length > space.VSpaceDimensions - 2)
            throw new InvalidOperationException();

        var processor = space.Processor;
        var scalarProcessor = space.ScalarProcessor;
        var composer = processor.CreateComposer();
            
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
            space,
            composer.GetVector()
        );
    }

    public static XGaConformalIpnsPoint<T> CreateIpnsPoint<T>(this XGaConformalSpace<T> space, XGaVector<T> positionVector)
    {
        if (positionVector.Ids.Max(k => k.LastIndex) > space.VSpaceDimensions - 2)
            throw new InvalidOperationException();
            
        var processor = space.Processor;
        var scalarProcessor = space.ScalarProcessor;
        var composer = processor.CreateComposer();

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
            space,
            composer.GetVector()
        );
    }
        
    public static XGaConformalIpnsHyperSphere<T> CreateIpnsHyperSphere<T>(this XGaConformalSpace<T> space, XGaVector<T> centerPoint, double radiusSquared)
    {
        return space.CreateIpnsHyperSphere(
            centerPoint,
            space.ScalarProcessor.ScalarFromNumber(radiusSquared)
        );
    }

    public static XGaConformalIpnsHyperSphere<T> CreateIpnsHyperSphere<T>(this XGaConformalSpace<T> space, XGaVector<T> centerPoint, string radiusSquared)
    {
        return space.CreateIpnsHyperSphere(
            centerPoint,
            space.ScalarProcessor.ScalarFromText(radiusSquared)
        );
    }

    public static XGaConformalIpnsHyperSphere<T> CreateIpnsHyperSphere<T>(this XGaConformalSpace<T> space, XGaVector<T> centerPoint, IScalar<T> radiusSquared)
    {
        if (centerPoint.Ids.Max(k => k.LastIndex) > space.VSpaceDimensions - 2)
            throw new InvalidOperationException();
            
        var processor = space.Processor;
        var scalarProcessor = space.ScalarProcessor;
        var composer = processor.CreateComposer();

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
            space,
            composer.GetVector()
        );
    }
        
    public static XGaConformalIpnsHyperPlane<T> CreateIpnsHyperPlane<T>(this XGaConformalSpace<T> space, XGaVector<T> normal, double delta)
    {
        return space.CreateIpnsHyperPlane(
            normal,
            space.ScalarProcessor.ValueFromNumber(delta)
        );
    }

    public static XGaConformalIpnsHyperPlane<T> CreateIpnsHyperPlane<T>(this XGaConformalSpace<T> space, XGaVector<T> normal, string delta)
    {
        return space.CreateIpnsHyperPlane(
            normal,
            space.ScalarProcessor.ValueFromText(delta)
        );
    }

    public static XGaConformalIpnsHyperPlane<T> CreateIpnsHyperPlane<T>(this XGaConformalSpace<T> space, XGaVector<T> normal, T delta)
    {
        if (normal.Ids.Max(k => k.LastIndex) > space.VSpaceDimensions - 2)
            throw new InvalidOperationException();
            
        var processor = space.Processor;
        var composer = processor.CreateComposer();
            
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
            space,
            composer.GetVector()
        );
    }


    public static XGaConformalOpnsRound<T> CreateOpnsRound<T>(this XGaConformalSpace<T> space, params XGaVector<T>[] points)
    {
        if (points.Length < 2 || points.Length > space.VSpaceDimensions - 1)
            throw new InvalidOperationException();

        var kVector = 
            points
                .Select(p => space.CreateIpnsPoint(p).Vector)
                .Op(space.Processor);

        return new XGaConformalOpnsRound<T>(space, kVector);
    }

    public static XGaConformalOpnsHyperSphere<T> CreateOpnsHyperSphere<T>(this XGaConformalSpace<T> space, params XGaVector<T>[] points)
    {
        if (points.Length != space.VSpaceDimensions - 1)
            throw new InvalidOperationException();

        var kVector = 
            points
                .Select(p => space.CreateIpnsPoint(p).Vector)
                .Op(space.Processor)
                .Op(space.InfinityBasisVector);

        return new XGaConformalOpnsHyperSphere<T>(space, kVector);
    }

    public static XGaConformalOpnsFlat<T> CreateOpnsFlat<T>(this XGaConformalSpace<T> space, params XGaVector<T>[] points)
    {
        if (points.Length < 2 || points.Length >= space.VSpaceDimensions - 1)
            throw new InvalidOperationException();

        var kVector = 
            points
                .Select(p => space.CreateIpnsPoint(p).Vector)
                .Op(space.Processor)
                .Op(space.InfinityBasisVector);

        return new XGaConformalOpnsFlat<T>(space, kVector);
    }

    public static XGaConformalOpnsFlat<T> CreateOpnsFlat<T>(this XGaConformalSpace<T> space, XGaVector<T> positionVector, XGaKVector<T> directionBlade)
    {
        if (positionVector.VSpaceDimensions > space.VSpaceDimensions - 2)
            throw new InvalidOperationException();

        if (directionBlade.VSpaceDimensions > space.VSpaceDimensions - 2)
            throw new InvalidOperationException();

        var kVector = 
            positionVector
                .Op(directionBlade)
                .Op(space.InfinityBasisVector);

        return new XGaConformalOpnsFlat<T>(space, kVector);
    }

    public static XGaConformalOpnsDirection<T> CreateOpnsDirection<T>(this XGaConformalSpace<T> space, XGaKVector<T> directionBlade)
    {
        if (directionBlade.VSpaceDimensions > space.VSpaceDimensions - 2)
            throw new InvalidOperationException();

        var kVector = directionBlade.Op(space.InfinityBasisVector);

        return new XGaConformalOpnsDirection<T>(space, kVector);
    }

    public static XGaConformalOpnsHyperPlane<T> CreateOpnsHyperPlane<T>(this XGaConformalSpace<T> space, params XGaVector<T>[] points)
    {
        if (points.Length != space.VSpaceDimensions - 2)
            throw new InvalidOperationException();

        var kVector = 
            points
                .Select(p => space.CreateIpnsPoint(p).Vector)
                .Op(space.Processor)
                .Op(space.InfinityBasisVector);

        return new XGaConformalOpnsHyperPlane<T>(space, kVector);
    }
}