using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Spaces.Conformal;

public static class XGaFloat64ConformalComposerUtils
{
    /// <summary>
    /// Convert a Euclidean position vector into a conformal null vector
    /// </summary>
    /// <param name="space"></param>
    /// <param name="positionVector"></param>
    /// <returns></returns>
    public static XGaFloat64ConformalIpnsPoint CreateIpnsPoint(this XGaFloat64ConformalSpace space, params double[] positionVector)
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
            
        return new XGaFloat64ConformalIpnsPoint(
            space,
            composer.GetVector()
        );
    }

    public static XGaFloat64ConformalIpnsPoint CreateIpnsPoint(this XGaFloat64ConformalSpace space, XGaFloat64Vector positionVector)
    {
        if (positionVector.Ids.Max(k => k.LastIndex) > space.VSpaceDimensions - 2)
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
                new KeyValuePair<IndexSet, double>(
                    p.Key.ShiftIndices(2), 
                    p.Value
                )
            )
        );
            
        return new XGaFloat64ConformalIpnsPoint(
            space,
            composer.GetVector()
        );
    }

    public static XGaFloat64ConformalIpnsHyperSphere CreateIpnsHyperSphere(this XGaFloat64ConformalSpace space, XGaFloat64Vector centerPoint, double radiusSquared)
    {
        if (centerPoint.Ids.Max(k => k.LastIndex) > space.VSpaceDimensions - 2)
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
                new KeyValuePair<IndexSet, double>(
                    p.Key.ShiftIndices(2), 
                    p.Value
                )
            )
        );

        return new XGaFloat64ConformalIpnsHyperSphere(
            space,
            composer.GetVector()
        );
    }

    public static XGaFloat64ConformalIpnsHyperPlane CreateIpnsHyperPlane(this XGaFloat64ConformalSpace space, XGaFloat64Vector normal, double delta)
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
                new KeyValuePair<IndexSet, double>(
                    p.Key.ShiftIndices(2), 
                    p.Value
                )
            )
        );

        return new XGaFloat64ConformalIpnsHyperPlane(
            space,
            composer.GetVector()
        );
    }


    public static XGaFloat64ConformalOpnsRound CreateOpnsRound(this XGaFloat64ConformalSpace space, params XGaFloat64Vector[] points)
    {
        if (points.Length < 2 || points.Length > space.VSpaceDimensions - 1)
            throw new InvalidOperationException();

        var kVector = 
            points
                .Select(p => space.CreateIpnsPoint(p).Vector)
                .Op(space.Processor);

        return new XGaFloat64ConformalOpnsRound(space, kVector);
    }

    public static XGaFloat64ConformalOpnsHyperSphere CreateOpnsHyperSphere(this XGaFloat64ConformalSpace space, params XGaFloat64Vector[] points)
    {
        if (points.Length != space.VSpaceDimensions - 1)
            throw new InvalidOperationException();

        var kVector = 
            points
                .Select(p => space.CreateIpnsPoint(p).Vector)
                .Op(space.Processor)
                .Op(space.InfinityBasisVector);

        return new XGaFloat64ConformalOpnsHyperSphere(space, kVector);
    }

    public static XGaFloat64ConformalOpnsFlat CreateOpnsFlat(this XGaFloat64ConformalSpace space, params XGaFloat64Vector[] points)
    {
        if (points.Length < 2 || points.Length >= space.VSpaceDimensions - 1)
            throw new InvalidOperationException();

        var kVector = 
            points
                .Select(p => space.CreateIpnsPoint(p).Vector)
                .Op(space.Processor)
                .Op(space.InfinityBasisVector);

        return new XGaFloat64ConformalOpnsFlat(space, kVector);
    }

    public static XGaFloat64ConformalOpnsFlat CreateOpnsFlat(this XGaFloat64ConformalSpace space, XGaFloat64Vector positionVector, XGaFloat64KVector directionBlade)
    {
        if (positionVector.VSpaceDimensions > space.VSpaceDimensions - 2)
            throw new InvalidOperationException();

        if (directionBlade.VSpaceDimensions > space.VSpaceDimensions - 2)
            throw new InvalidOperationException();

        var kVector = 
            positionVector
                .Op(directionBlade)
                .Op(space.InfinityBasisVector);

        return new XGaFloat64ConformalOpnsFlat(space, kVector);
    }

    public static XGaFloat64ConformalOpnsDirection CreateOpnsDirection(this XGaFloat64ConformalSpace space, XGaFloat64KVector directionBlade)
    {
        if (directionBlade.VSpaceDimensions > space.VSpaceDimensions - 2)
            throw new InvalidOperationException();

        var kVector = directionBlade.Op(space.InfinityBasisVector);

        return new XGaFloat64ConformalOpnsDirection(space, kVector);
    }

    public static XGaFloat64ConformalOpnsHyperPlane CreateOpnsHyperPlane(this XGaFloat64ConformalSpace space, params XGaFloat64Vector[] points)
    {
        if (points.Length != space.VSpaceDimensions - 2)
            throw new InvalidOperationException();

        var kVector = 
            points
                .Select(p => space.CreateIpnsPoint(p).Vector)
                .Op(space.Processor)
                .Op(space.InfinityBasisVector);

        return new XGaFloat64ConformalOpnsHyperPlane(space, kVector);
    }
}