using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.GradedMultivectorsLib.Types;

public sealed class LibTypeMultivector :
    LibType
{
    public override int GradeCount
        => VSpaceDimensions + 1;

    public override bool IsKVector
        => false;

    public override bool IsMultivector
        => true;


    internal LibTypeMultivector(XGaFloat64Processor metric, int vSpaceDimensions, string className)
        : base(metric, vSpaceDimensions, className)
    {
    }


    public override IReadOnlyList<XGaBasisBlade> GetBasisBlades()
    {
        return GaSpaceDimensions
            .GetRange(id =>
                Metric.BasisBlade((IndexSet)id)
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