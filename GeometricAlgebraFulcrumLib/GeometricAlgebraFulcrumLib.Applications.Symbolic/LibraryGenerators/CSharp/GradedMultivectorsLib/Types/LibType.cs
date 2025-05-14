


using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.GradedMultivectorsLib.Types;

public abstract class LibType
{
    public static LibTypeMultivector Multivector(XGaFloat64Processor metric, int vSpaceDimensions, string className)
    {
        return new LibTypeMultivector(metric, vSpaceDimensions, className);
    }

    public static LibTypeKVector KVector(XGaFloat64Processor metric, int vSpaceDimensions, string className, int grade)
    {
        return new LibTypeKVector(metric, vSpaceDimensions, className, grade);
    }


    public XGaFloat64Processor Metric { get; }

    public int VSpaceDimensions { get; }
    
    public string ClassName { get; }

    public int GaSpaceDimensions
        => 1 << VSpaceDimensions;

    public abstract int GradeCount { get; }

    public abstract bool IsKVector { get; }

    public abstract bool IsMultivector { get; }


    protected LibType(XGaFloat64Processor metric, int vSpaceDimensions, string className)
    {
        Metric = metric;
        VSpaceDimensions = vSpaceDimensions;
        ClassName = className;
    }



    public abstract IReadOnlyList<XGaBasisBlade> GetBasisBlades();

    public abstract IReadOnlyList<int> GetBasisBladeIDs();
}