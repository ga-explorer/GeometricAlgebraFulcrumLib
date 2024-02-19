using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Combinations;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.Matlab.GradedMultivectorsLib.Types;

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