using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;


namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.Matlab.GradedMultivectorsLib.Types;

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

    public abstract int ScalarCount { get; }

    public abstract string TypeSymbol { get; }


    protected LibType(XGaFloat64Processor metric, int vSpaceDimensions, string className)
    {
        Metric = metric;
        VSpaceDimensions = vSpaceDimensions;
        ClassName = className;
    }
    

    public abstract IReadOnlyList<XGaBasisBlade> GetBasisBlades();

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
    
    public string GetScalarName(string arrayName, XGaBasisBlade basisBlade)
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