using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.GradedMultivectorsLib.Types;

public sealed class LibTypeKVector :
    LibType
{
    public override int GradeCount
        => 1;

    public override bool IsKVector
        => true;

    public override bool IsMultivector
        => false;

    public int Grade { get; }

    public int KvSpaceDimensions { get; }


    internal LibTypeKVector(XGaFloat64Processor metric, int vSpaceDimensions, string className, int grade)
        : base(metric, vSpaceDimensions, className)
    {
        if (grade < 0 || grade > VSpaceDimensions)
            throw new ArgumentOutOfRangeException(nameof(grade));

        Grade = grade;
        KvSpaceDimensions = (int)VSpaceDimensions.GetBinomialCoefficient(Grade);
    }


    public override IReadOnlyList<XGaBasisBlade> GetBasisBlades()
    {
        return KvSpaceDimensions
            .GetRange(index =>
                Metric.BasisBlade(Grade, index)
            ).ToImmutableArray();
    }

    public override IReadOnlyList<int> GetBasisBladeIDs()
    {
        return KvSpaceDimensions
            .GetRange(index =>
                (int)BasisBladeUtils.BasisBladeGradeIndexToId(Grade, (ulong)index)
            ).ToImmutableArray();
    }

    public override string ToString()
    {
        return ClassName;
    }
}