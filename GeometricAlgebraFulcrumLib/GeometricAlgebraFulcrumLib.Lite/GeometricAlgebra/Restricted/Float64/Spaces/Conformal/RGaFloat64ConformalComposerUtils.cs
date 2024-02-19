using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Spaces.Conformal;

public static class RGaFloat64ConformalComposerUtils
{
    /// <summary>
    /// Convert a Euclidean position vector into a conformal null vector
    /// </summary>
    /// <param name="space"></param>
    /// <param name="positionVector"></param>
    /// <returns></returns>
    public static RGaFloat64ConformalIpnsPoint CreateIpnsPoint(this RGaFloat64ConformalSpace space, params double[] positionVector)
    {
        if (positionVector.Length > space.VSpaceDimensions - 2)
            throw new InvalidOperationException();

        var processor = space.Processor;
        var composer = processor.CreateComposer();
            
        var x2 = positionVector.Select(v => v * v).Sum();

        // e_o part
        composer.SetVectorTerm(0, 0.5d * (x2 - 1d));

        // e_i part
        composer.SetVectorTerm(1, 0.5d * (x2 + 1d));

        // Euclidean part
        composer.SetVectorTerms(2, positionVector);
            
        return new RGaFloat64ConformalIpnsPoint(
            space,
            composer.GetVector()
        );
    }

    public static RGaFloat64ConformalIpnsPoint CreateIpnsPoint(this RGaFloat64ConformalSpace space, RGaFloat64Vector positionVector)
    {
        if (positionVector.Ids.Max(k => k.LastOneBitPosition()) > space.VSpaceDimensions - 2)
            throw new InvalidOperationException();
            
        var processor = space.Processor;
        var composer = processor.CreateComposer();
            
        var x2 = positionVector.Scalars.Select(v => v * v).Sum();

        // e+ part
        composer.SetVectorTerm(0, 0.5d * (x2 - 1d));

        // e- part
        composer.SetVectorTerm(1, 0.5d * (x2 + 1d));

        // Euclidean part
        composer.SetTerms(
            positionVector.Select(p => 
                new KeyValuePair<ulong, double>(
                    p.Key << 2,
                    p.Value
                )
            )
        );
            
        return new RGaFloat64ConformalIpnsPoint(
            space,
            composer.GetVector()
        );
    }

    public static RGaFloat64ConformalIpnsHyperSphere CreateIpnsHyperSphere(this RGaFloat64ConformalSpace space, RGaFloat64Vector centerPoint, double radiusSquared)
    {
        if (centerPoint.Ids.Max(k => k.LastOneBitPosition()) > space.VSpaceDimensions - 2)
            throw new InvalidOperationException();
            
        var processor = space.Processor;
        var composer = processor.CreateComposer();
            
        var x2 = centerPoint.Scalars.Select(v => v * v).Sum() - radiusSquared;
            
        // e+ part
        composer.SetVectorTerm(0, 0.5d * (x2 - 1d));

        // e- part
        composer.SetVectorTerm(1, 0.5d * (x2 + 1d));
            
        // Euclidean part
        composer.SetTerms(
            centerPoint.Select(p => 
                new KeyValuePair<ulong, double>(
                    p.Key << 2, 
                    p.Value
                )
            )
        );

        return new RGaFloat64ConformalIpnsHyperSphere(
            space,
            composer.GetVector()
        );
    }

    public static RGaFloat64ConformalIpnsHyperPlane CreateIpnsHyperPlane(this RGaFloat64ConformalSpace space, RGaFloat64Vector normal, double delta)
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
                new KeyValuePair<ulong, double>(
                    p.Key << 2, 
                    p.Value
                )
            )
        );

        return new RGaFloat64ConformalIpnsHyperPlane(
            space,
            composer.GetVector()
        );
    }


    public static RGaFloat64ConformalOpnsRound CreateOpnsRound(this RGaFloat64ConformalSpace space, params RGaFloat64Vector[] points)
    {
        if (points.Length < 2 || points.Length > space.VSpaceDimensions - 1)
            throw new InvalidOperationException();

        var kVector = 
            points
                .Select(p => space.CreateIpnsPoint(p).Vector)
                .Op(space.Processor);

        return new RGaFloat64ConformalOpnsRound(space, kVector);
    }

    public static RGaFloat64ConformalOpnsHyperSphere CreateOpnsHyperSphere(this RGaFloat64ConformalSpace space, params RGaFloat64Vector[] points)
    {
        if (points.Length != space.VSpaceDimensions - 1)
            throw new InvalidOperationException();

        var kVector = 
            points
                .Select(p => space.CreateIpnsPoint(p).Vector)
                .Op(space.Processor)
                .Op(space.InfinityBasisVector);

        return new RGaFloat64ConformalOpnsHyperSphere(space, kVector);
    }

    public static RGaFloat64ConformalOpnsFlat CreateOpnsFlat(this RGaFloat64ConformalSpace space, params RGaFloat64Vector[] points)
    {
        if (points.Length < 2 || points.Length >= space.VSpaceDimensions - 1)
            throw new InvalidOperationException();

        var kVector = 
            points
                .Select(p => space.CreateIpnsPoint(p).Vector)
                .Op(space.Processor)
                .Op(space.InfinityBasisVector);

        return new RGaFloat64ConformalOpnsFlat(space, kVector);
    }

    public static RGaFloat64ConformalOpnsFlat CreateOpnsFlat(this RGaFloat64ConformalSpace space, RGaFloat64Vector positionVector, RGaFloat64KVector directionBlade)
    {
        if (positionVector.VSpaceDimensions > space.VSpaceDimensions - 2)
            throw new InvalidOperationException();

        if (directionBlade.VSpaceDimensions > space.VSpaceDimensions - 2)
            throw new InvalidOperationException();

        var kVector = 
            positionVector
                .Op(directionBlade)
                .Op(space.InfinityBasisVector);

        return new RGaFloat64ConformalOpnsFlat(space, kVector);
    }

    public static RGaFloat64ConformalOpnsDirection CreateOpnsDirection(this RGaFloat64ConformalSpace space, RGaFloat64KVector directionBlade)
    {
        if (directionBlade.VSpaceDimensions > space.VSpaceDimensions - 2)
            throw new InvalidOperationException();

        var kVector = directionBlade.Op(space.InfinityBasisVector);

        return new RGaFloat64ConformalOpnsDirection(space, kVector);
    }

    public static RGaFloat64ConformalOpnsHyperPlane CreateOpnsHyperPlane(this RGaFloat64ConformalSpace space, params RGaFloat64Vector[] points)
    {
        if (points.Length != space.VSpaceDimensions - 2)
            throw new InvalidOperationException();

        var kVector = 
            points
                .Select(p => space.CreateIpnsPoint(p).Vector)
                .Op(space.Processor)
                .Op(space.InfinityBasisVector);

        return new RGaFloat64ConformalOpnsHyperPlane(space, kVector);
    }
}