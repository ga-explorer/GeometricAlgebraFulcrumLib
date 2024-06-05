using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.Matlab.GradedMultivectorsLib.Types;

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

    public abstract int ScalarCount { get; }

    public abstract string TypeSymbol { get; }


    protected LibType(RGaFloat64Processor metric, int vSpaceDimensions, string className)
    {
        Metric = metric;
        VSpaceDimensions = vSpaceDimensions;
        ClassName = className;
    }
    

    public abstract IReadOnlyList<RGaBasisBlade> GetBasisBlades();

    public abstract IReadOnlyList<int> GetBasisBladeIDs();
    
    public abstract int GetScalarIndex(int id);
    
    public string GetScalarName(string arrayName, int id)
    {
        var scalarIndex = GetScalarIndex(id) + 1;

        return $"{arrayName}({scalarIndex},:)";
    }

    public string GetScalarName(string arrayName, int grade, int index)
    {
        var id = (int) BasisBladeUtils.BasisBladeGradeIndexToId(
            (uint) grade, 
            (ulong) index
        );

        return GetScalarName(arrayName, id);
    }
    
    public string GetScalarName(string arrayName, RGaBasisBlade basisBlade)
    {
        var id = (int) basisBlade.Id;

        return GetScalarName(arrayName, id);
    }

    public IEnumerable<string> GetScalarNames(string arrayName)
    {
        return GetBasisBladeIDs().Select(id => 
            GetScalarName(arrayName, id)
        );
    }
}