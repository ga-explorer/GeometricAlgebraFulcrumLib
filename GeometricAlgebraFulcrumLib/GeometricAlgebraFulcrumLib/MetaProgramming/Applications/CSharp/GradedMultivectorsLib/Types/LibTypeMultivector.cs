using DataStructuresLib.BitManipulation;
using System.Collections.Generic;
using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.GradedMultivectorsLib.Types;

public sealed class LibTypeMultivector :
    LibType
{
    public override int GradeCount
        => VSpaceDimensions + 1;

    public override bool IsKVector
        => false;

    public override bool IsMultivector
        => true;


    internal LibTypeMultivector(RGaFloat64Processor metric, int vSpaceDimensions, string className)
        : base(metric, vSpaceDimensions, className)
    {
    }


    public override IReadOnlyList<RGaBasisBlade> GetBasisBlades()
    {
        return GaSpaceDimensions
            .GetRange(id =>
                Metric.CreateBasisBlade((ulong)id)
            ).ToImmutableArray();
    }

    public override IReadOnlyList<int> GetBasisBladeIDs()
    {
        return GaSpaceDimensions
            .GetRange()
            .ToImmutableArray();
    }

    public override string ToString()
    {
        return ClassName;
    }
}