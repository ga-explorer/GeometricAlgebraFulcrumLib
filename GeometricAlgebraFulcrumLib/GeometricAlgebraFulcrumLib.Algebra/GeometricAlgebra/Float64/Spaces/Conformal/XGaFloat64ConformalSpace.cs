using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Spaces.Conformal;

public class XGaFloat64ConformalSpace :
    XGaFloat64Space
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ConformalSpace Create(int vSpaceDimensions)
    {
        return new XGaFloat64ConformalSpace(vSpaceDimensions);
    }


    public override int VSpaceDimensions { get; }

    public XGaFloat64ConformalProcessor ConformalProcessor
        => XGaFloat64Processor.Conformal;

    public override XGaFloat64Processor Processor
        => XGaFloat64Processor.Conformal;

    public XGaFloat64Vector OriginBasisVector { get; }

    public XGaFloat64Vector InfinityBasisVector { get; }


    internal XGaFloat64ConformalSpace(int vSpaceDimensions)
    {
        if (vSpaceDimensions < 2)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        VSpaceDimensions = vSpaceDimensions;
        
        OriginBasisVector =
            XGaFloat64Processor
                .Conformal
                .CreateVectorComposer()
                .SetVectorTerm(0, 0.5d)
                .SetVectorTerm(1, -0.5d)
                .GetVector();
        
        InfinityBasisVector = 
            XGaFloat64Processor
                .Conformal
                .CreateVectorComposer()
                .SetVectorTerm(0, 1d)
                .SetVectorTerm(1, 1d)
                .GetVector();
    }


    /// <summary>
    /// Convert a Euclidean position vector into a conformal null vector
    /// </summary>
    /// <param name="positionVector"></param>
    /// <returns></returns>
    public XGaFloat64ConformalIpnsPoint CreateIpnsPoint(params double[] positionVector)
    {
        if (positionVector.Length > VSpaceDimensions - 2)
            throw new InvalidOperationException();

        var processor = Processor;
        var composer = processor.CreateVectorComposer();
            
        var x2 = positionVector.Select(v => v * v).Sum();

        // e_o part
        composer.SetVectorTerm(0, 0.5d * (x2 - 1d));

        // e_i part
        composer.SetVectorTerm(1, 0.5d * (x2 + 1d));

        // Euclidean part
        composer.SetVectorTerms(2, positionVector);
            
        return new XGaFloat64ConformalIpnsPoint(
            this,
            composer.GetVector()
        );
    }

    public XGaFloat64ConformalIpnsPoint CreateIpnsPoint(XGaFloat64Vector positionVector)
    {
        if (positionVector.Ids.Max(k => k.LastIndex) > VSpaceDimensions - 2)
            throw new InvalidOperationException();
            
        var processor = Processor;
        var composer = processor.CreateVectorComposer();
            
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
            this,
            composer.GetVector()
        );
    }

    public XGaFloat64ConformalIpnsHyperSphere CreateIpnsHyperSphere(XGaFloat64Vector centerPoint, double radiusSquared)
    {
        if (centerPoint.Ids.Max(k => k.LastIndex) > VSpaceDimensions - 2)
            throw new InvalidOperationException();
            
        var processor = Processor;
        var composer = processor.CreateVectorComposer();
            
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
            this,
            composer.GetVector()
        );
    }

    public XGaFloat64ConformalIpnsHyperPlane CreateIpnsHyperPlane(XGaFloat64Vector normal, double delta)
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
                new KeyValuePair<IndexSet, double>(
                    p.Key.ShiftIndices(2), 
                    p.Value
                )
            )
        );

        return new XGaFloat64ConformalIpnsHyperPlane(
            this,
            composer.GetVector()
        );
    }


    public XGaFloat64ConformalOpnsRound CreateOpnsRound(params XGaFloat64Vector[] points)
    {
        if (points.Length < 2 || points.Length > VSpaceDimensions - 1)
            throw new InvalidOperationException();

        var kVector = 
            points
                .Select(p => CreateIpnsPoint(p).Vector)
                .Op(Processor);

        return new XGaFloat64ConformalOpnsRound(this, kVector);
    }

    public XGaFloat64ConformalOpnsHyperSphere CreateOpnsHyperSphere(params XGaFloat64Vector[] points)
    {
        if (points.Length != VSpaceDimensions - 1)
            throw new InvalidOperationException();

        var kVector = 
            points
                .Select(p => CreateIpnsPoint(p).Vector)
                .Op(Processor)
                .Op(InfinityBasisVector);

        return new XGaFloat64ConformalOpnsHyperSphere(this, kVector);
    }

    public XGaFloat64ConformalOpnsFlat CreateOpnsFlat(params XGaFloat64Vector[] points)
    {
        if (points.Length < 2 || points.Length >= VSpaceDimensions - 1)
            throw new InvalidOperationException();

        var kVector = 
            points
                .Select(p => CreateIpnsPoint(p).Vector)
                .Op(Processor)
                .Op(InfinityBasisVector);

        return new XGaFloat64ConformalOpnsFlat(this, kVector);
    }

    public XGaFloat64ConformalOpnsFlat CreateOpnsFlat(XGaFloat64Vector positionVector, XGaFloat64KVector directionBlade)
    {
        if (positionVector.VSpaceDimensions > VSpaceDimensions - 2)
            throw new InvalidOperationException();

        if (directionBlade.VSpaceDimensions > VSpaceDimensions - 2)
            throw new InvalidOperationException();

        var kVector = 
            positionVector
                .Op(directionBlade)
                .Op(InfinityBasisVector);

        return new XGaFloat64ConformalOpnsFlat(this, kVector);
    }

    public XGaFloat64ConformalOpnsDirection CreateOpnsDirection(XGaFloat64KVector directionBlade)
    {
        if (directionBlade.VSpaceDimensions > VSpaceDimensions - 2)
            throw new InvalidOperationException();

        var kVector = directionBlade.Op(InfinityBasisVector);

        return new XGaFloat64ConformalOpnsDirection(this, kVector);
    }

    public XGaFloat64ConformalOpnsHyperPlane CreateOpnsHyperPlane(params XGaFloat64Vector[] points)
    {
        if (points.Length != VSpaceDimensions - 2)
            throw new InvalidOperationException();

        var kVector = 
            points
                .Select(p => CreateIpnsPoint(p).Vector)
                .Op(Processor)
                .Op(InfinityBasisVector);

        return new XGaFloat64ConformalOpnsHyperPlane(this, kVector);
    }
}