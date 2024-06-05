using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.GradedMultivectorsLib.Types;

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

    public override string ToString()
    {
        return ClassName;
    }
}