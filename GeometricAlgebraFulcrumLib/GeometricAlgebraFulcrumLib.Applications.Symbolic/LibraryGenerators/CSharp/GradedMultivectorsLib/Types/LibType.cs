using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.GradedMultivectorsLib.Types;

public abstract class LibType
{
    public static LibTypeMultivector Multivector(RGaFloat64Processor metric, int vSpaceDimensions, string className)
    {
        return new LibTypeMultivector(metric, vSpaceDimensions, className);
    }

    public static LibTypeKVector KVector(RGaFloat64Processor metric, int vSpaceDimensions, string className, int grade)
    {
        return new LibTypeKVector(metric, vSpaceDimensions, className, grade);
    }


    public RGaFloat64Processor Metric { get; }

    public int VSpaceDimensions { get; }
    
    public string ClassName { get; }

    public int GaSpaceDimensions
        => 1 << VSpaceDimensions;

    public abstract int GradeCount { get; }

    public abstract bool IsKVector { get; }

    public abstract bool IsMultivector { get; }


    protected LibType(RGaFloat64Processor metric, int vSpaceDimensions, string className)
    {
        Metric = metric;
        VSpaceDimensions = vSpaceDimensions;
        ClassName = className;
    }



    public abstract IReadOnlyList<RGaBasisBlade> GetBasisBlades();

    public abstract IReadOnlyList<int> GetBasisBladeIDs();
}