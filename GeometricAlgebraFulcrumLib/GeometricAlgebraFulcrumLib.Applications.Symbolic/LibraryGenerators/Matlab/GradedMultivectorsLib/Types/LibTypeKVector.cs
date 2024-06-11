using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.Matlab.GradedMultivectorsLib.Types;

public sealed class LibTypeKVector :
    LibType
{
    public override int GradeCount
        => 1;

    public override bool IsKVector
        => true;

    public override bool IsMultivector
        => false;
    
    public override int ScalarCount 
        => KvSpaceDimensions;

    public override string TypeSymbol 
        => "Kv" + Grade;

    public int Grade { get; }

    public int KvSpaceDimensions { get; }


    internal LibTypeKVector(RGaFloat64Processor metric, int vSpaceDimensions, string className, int grade)
        : base(metric, vSpaceDimensions, className)
    {
        if (grade < 0 || grade > VSpaceDimensions)
            throw new ArgumentOutOfRangeException(nameof(grade));

        Grade = grade;
        KvSpaceDimensions = (int)VSpaceDimensions.GetBinomialCoefficient(Grade);
    }


    public override IReadOnlyList<RGaBasisBlade> GetBasisBlades()
    {
        return KvSpaceDimensions
            .GetRange(index =>
                Metric.CreateBasisBlade(Grade, index)
            ).ToImmutableArray();
    }

    public override IReadOnlyList<int> GetBasisBladeIDs()
    {
        return KvSpaceDimensions
            .GetRange(index =>
                (int)BasisBladeUtils.BasisBladeGradeIndexToId(Grade, (ulong)index)
            ).ToImmutableArray();
    }

    public override int GetScalarIndex(int id)
    {
        return (int) ((ulong) id).BasisBladeIdToIndex();
    }

    public override string ToString()
    {
        return ClassName;
    }
}