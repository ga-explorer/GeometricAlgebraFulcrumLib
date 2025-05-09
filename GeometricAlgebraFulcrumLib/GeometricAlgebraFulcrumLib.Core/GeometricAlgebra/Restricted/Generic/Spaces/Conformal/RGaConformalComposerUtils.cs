using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Core.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Spaces.Conformal;

public static class RGaConformalComposerUtils
{
    /// <summary>
    /// Convert a Euclidean position vector into a conformal null vector
    /// </summary>
    /// <param name="space"></param>
    /// <param name="positionVector"></param>
    /// <returns></returns>
    public static RGaConformalIpnsPoint<T> CreateIpnsPoint<T>(this RGaConformalSpace<T> space, params T[] positionVector)
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
            
        return new RGaConformalIpnsPoint<T>(
            space,
            composer.GetVector()
        );
    }

    public static RGaConformalIpnsPoint<T> CreateIpnsPoint<T>(this RGaConformalSpace<T> space, RGaVector<T> positionVector)
    {
        if (positionVector.Ids.Max(k => k.LastOneBitPosition()) > space.VSpaceDimensions - 2)
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
                new KeyValuePair<ulong, T>(
                    p.Key.ShiftOnes(2),
                    p.Value
                )
            )
        );
            
        return new RGaConformalIpnsPoint<T>(
            space,
            composer.GetVector()
        );
    }
        
    public static RGaConformalIpnsHyperSphere<T> CreateIpnsHyperSphere<T>(this RGaConformalSpace<T> space, RGaVector<T> centerPoint, double radiusSquared)
    {
        return CreateIpnsHyperSphere(
            space,
            centerPoint,
            space.ScalarProcessor.ScalarFromNumber(radiusSquared)
        );
    }

    public static RGaConformalIpnsHyperSphere<T> CreateIpnsHyperSphere<T>(this RGaConformalSpace<T> space, RGaVector<T> centerPoint, string radiusSquared)
    {
        return CreateIpnsHyperSphere(
            space,
            centerPoint,
            space.ScalarProcessor.ScalarFromText(radiusSquared)
        );
    }

    public static RGaConformalIpnsHyperSphere<T> CreateIpnsHyperSphere<T>(this RGaConformalSpace<T> space, RGaVector<T> centerPoint, IScalar<T> radiusSquared)
    {
        if (centerPoint.Ids.Max(k => k.LastOneBitPosition()) > space.VSpaceDimensions - 2)
            throw new InvalidOperationException();
            
        var processor = space.Processor;
        var scalarProcessor = space.ScalarProcessor;
        var composer = processor.CreateComposer();

        Debug.Assert(radiusSquared != null, nameof(radiusSquared) + " != null");
        
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
                new KeyValuePair<ulong, T>(
                    p.Key.ShiftOnes(2), 
                    p.Value
                )
            )
        );

        return new RGaConformalIpnsHyperSphere<T>(
            space,
            composer.GetVector()
        );
    }
        
    public static RGaConformalIpnsHyperPlane<T> CreateIpnsHyperPlane<T>(this RGaConformalSpace<T> space, RGaVector<T> normal, double delta)
    {
        return CreateIpnsHyperPlane(
            space,
            normal,
            space.ScalarProcessor.ValueFromNumber(delta)
        );
    }

    public static RGaConformalIpnsHyperPlane<T> CreateIpnsHyperPlane<T>(this RGaConformalSpace<T> space, RGaVector<T> normal, string delta)
    {
        return CreateIpnsHyperPlane(
            space,
            normal,
            space.ScalarProcessor.ValueFromText(delta)
        );
    }

    public static RGaConformalIpnsHyperPlane<T> CreateIpnsHyperPlane<T>(this RGaConformalSpace<T> space, RGaVector<T> normal, T delta)
    {
        if (normal.Ids.Max(k => k.LastOneBitPosition()) > space.VSpaceDimensions - 2)
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
                new KeyValuePair<ulong, T>(
                    p.Key.ShiftOnes(2), 
                    p.Value
                )
            )
        );

        return new RGaConformalIpnsHyperPlane<T>(
            space,
            composer.GetVector()
        );
    }


    public static RGaConformalOpnsRound<T> CreateOpnsRound<T>(this RGaConformalSpace<T> space, params RGaVector<T>[] points)
    {
        if (points.Length < 2 || points.Length > space.VSpaceDimensions - 1)
            throw new InvalidOperationException();

        var kVector = 
            points
                .Select(p => space.CreateIpnsPoint(p).Vector)
                .Op(space.Processor);

        return new RGaConformalOpnsRound<T>(space, kVector);
    }

    public static RGaConformalOpnsHyperSphere<T> CreateOpnsHyperSphere<T>(this RGaConformalSpace<T> space, params RGaVector<T>[] points)
    {
        if (points.Length != space.VSpaceDimensions - 1)
            throw new InvalidOperationException();

        var kVector = 
            points
                .Select(p => space.CreateIpnsPoint(p).Vector)
                .Op(space.Processor)
                .Op(space.InfinityBasisVector);

        return new RGaConformalOpnsHyperSphere<T>(space, kVector);
    }

    public static RGaConformalOpnsFlat<T> CreateOpnsFlat<T>(this RGaConformalSpace<T> space, params RGaVector<T>[] points)
    {
        if (points.Length < 2 || points.Length >= space.VSpaceDimensions - 1)
            throw new InvalidOperationException();

        var kVector = 
            points
                .Select(p => space.CreateIpnsPoint(p).Vector)
                .Op(space.Processor)
                .Op(space.InfinityBasisVector);

        return new RGaConformalOpnsFlat<T>(space, kVector);
    }

    public static RGaConformalOpnsFlat<T> CreateOpnsFlat<T>(this RGaConformalSpace<T> space, RGaVector<T> positionVector, RGaKVector<T> directionBlade)
    {
        if (positionVector.VSpaceDimensions > space.VSpaceDimensions - 2)
            throw new InvalidOperationException();

        if (directionBlade.VSpaceDimensions > space.VSpaceDimensions - 2)
            throw new InvalidOperationException();

        var kVector = 
            positionVector
                .Op(directionBlade)
                .Op(space.InfinityBasisVector);

        return new RGaConformalOpnsFlat<T>(space, kVector);
    }

    public static RGaConformalOpnsDirection<T> CreateOpnsDirection<T>(this RGaConformalSpace<T> space, RGaKVector<T> directionBlade)
    {
        if (directionBlade.VSpaceDimensions > space.VSpaceDimensions - 2)
            throw new InvalidOperationException();

        var kVector = directionBlade.Op(space.InfinityBasisVector);

        return new RGaConformalOpnsDirection<T>(space, kVector);
    }

    public static RGaConformalOpnsHyperPlane<T> CreateOpnsHyperPlane<T>(this RGaConformalSpace<T> space, params RGaVector<T>[] points)
    {
        if (points.Length != space.VSpaceDimensions - 2)
            throw new InvalidOperationException();

        var kVector = 
            points
                .Select(p => space.CreateIpnsPoint(p).Vector)
                .Op(space.Processor)
                .Op(space.InfinityBasisVector);

        return new RGaConformalOpnsHyperPlane<T>(space, kVector);
    }
}