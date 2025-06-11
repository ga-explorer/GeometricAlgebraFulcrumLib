using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

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
    
    public override int GetScalarIndex(int id)
    {
        if (id == 0) 
            return 0;

        var (grade, index) = 
            ((IndexSet)id).BasisBladeIdToGradeIndex();

        return grade.MapRange(g => 
            (int) VSpaceDimensions.GetBinomialCoefficient((int)g)
        ).Sum() + (int) index;
    }

    public override string ToString()
    {
        return ClassName;
    }
}