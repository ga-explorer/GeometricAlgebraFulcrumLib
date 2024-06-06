using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.Matlab.GradedMultivectorsLib.Types;

public sealed class LibTypeMultivector :
    LibType
{
    public override int GradeCount
        => VSpaceDimensions + 1;

    public override bool IsKVector
        => false;

    public override bool IsMultivector
        => true;
    
    public override int ScalarCount 
        => GaSpaceDimensions;

    public override string TypeSymbol 
        => "Mv";


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
    
    public override int GetScalarIndex(int id)
    {
        if (id == 0) 
            return 0;

        var (grade, index) = 
            ((ulong)id).BasisBladeIdToGradeIndex();

        return grade.MapRange(g => 
            (int) VSpaceDimensions.GetBinomialCoefficient((int)g)
        ).Sum() + (int) index;
    }

    public override string ToString()
    {
        return ClassName;
    }
}