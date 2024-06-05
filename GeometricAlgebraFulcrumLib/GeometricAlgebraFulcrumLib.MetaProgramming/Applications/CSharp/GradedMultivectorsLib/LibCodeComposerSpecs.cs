using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.GradedMultivectorsLib.Combinations;
using GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.GradedMultivectorsLib.Types;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.GradedMultivectorsLib;

public sealed class LibCodeComposerSpecs
{
    public string GaClassPrefix { get; }

    public string GaClassPostfix { get; }

    public LibTypeMultivector MultivectorType { get; }

    public IReadOnlyList<LibTypeKVector> KVectorTypes { get; }
        
    public IReadOnlyList<LibType> Types { get; }
        
    public string LaTeXBasisSymbol { get; init; }

    //public IReadOnlyList<string> LaTeXBasisVectorSubscripts { get; init; }

    //public double[,] LaTeXBasisVectorMap { get; init; }

    public string GaSpaceName 
        => GaClassPrefix + GaClassPostfix;

    public LibTypeKVector ScalarType 
        => KVectorTypes[0];
        
    public LibTypeKVector VectorType 
        => KVectorTypes[1];
        
    public LibTypeKVector BivectorType 
        => KVectorTypes[2];
        
    /// <summary>
    /// Recommended range: 2,3,4,5,6
    /// </summary>
    public int VSpaceDimensions 
        => MultivectorType.VSpaceDimensions;

    public RGaFloat64Processor Metric 
        => MultivectorType.Metric;

    public int GradeCount
        => MultivectorType.GradeCount;

    public int GaSpaceDimensions
        => MultivectorType.GaSpaceDimensions;

    public int MaxBasisBladeId
        => GaSpaceDimensions - 1;


    public LibCodeComposerSpecs(int vSpaceDimensions, RGaFloat64Processor metric, string gaClassPrefix, string gaClassPostfix)
    {
        GaClassPrefix = gaClassPrefix;
        GaClassPostfix = gaClassPostfix;

        MultivectorType =
            LibType.Multivector(
                metric, 
                vSpaceDimensions, 
                GetMultivectorClassName()
            );

        KVectorTypes =
            GradeCount.GetRange(grade =>
                LibType.KVector(
                    metric, 
                    vSpaceDimensions, 
                    GetKVectorClassName(grade),
                    grade
                )
            ).ToImmutableArray();

        var typeList = new List<LibType>(VSpaceDimensions + 2);
            
        typeList.AddRange(KVectorTypes);
        typeList.Add(MultivectorType);

        Types = typeList.ToImmutableArray();
    }
        
        
    public bool IsValidGrade(int grade)
    {
        return grade >= 0 && grade <= VSpaceDimensions;
    }
        
        
    public IReadOnlyList<int> GetGrades()
    {
        return Enumerable
            .Range(0, GradeCount)
            .ToImmutableArray();
    }
        
    public IReadOnlyList<int> GetGrades(Func<int, bool> gradeFilter)
    {
        return Enumerable
            .Range(0, GradeCount)
            .Where(gradeFilter)
            .ToImmutableArray();
    }
        
    private string GetKVectorClassName(int grade)
    {
        if (grade < 0 || grade > VSpaceDimensions)
            throw new ArgumentOutOfRangeException(nameof(grade));

        return $"{GaClassPrefix}KVector{grade}{GaClassPostfix}";

        //return grade switch
        //{
        //    0 => $"{GaClassPrefix}Scalar{GaClassPostfix}",
        //    1 => $"{GaClassPrefix}Vector{GaClassPostfix}",
        //    2 => $"{GaClassPrefix}Bivector{GaClassPostfix}",
        //    3 => $"{GaClassPrefix}Trivector{GaClassPostfix}",
        //    4 => $"{GaClassPrefix}Quadvector{GaClassPostfix}",
        //    5 => $"{GaClassPrefix}Pentavector{GaClassPostfix}",
        //    6 => $"{GaClassPrefix}Hexavector{GaClassPostfix}",
        //    _ => $"{GaClassPrefix}KVector{grade}{GaClassPostfix}"
        //};
    }

    private string GetMultivectorClassName()
    {
        return $"{GaClassPrefix}Multivector{GaClassPostfix}";
    }
        
    public string GetOutputClassName(ILibLinearCombination termTable)
    {
        var gradeList = termTable.GetOutputBasisBladeGrades();

        if (gradeList.Count == 0)
            return ScalarType.ClassName;
            
        if (gradeList.Count == 1)
            return KVectorTypes[gradeList[0]].ClassName;

        return MultivectorType.ClassName;
    }
        
    public IEnumerable<string> GetKVectorScalarNames(int grade)
    {
        return KVectorTypes[grade]
            .GetBasisBladeIDs()
            .Select(id => GradedMultivectorLibUtils.GetBasisBladeScalarName(id));
    }
        
    public LibType GetOutType(LibType in1Type, LibType in2Type, Func<RGaBasisBlade, RGaBasisBlade, IRGaSignedBasisBlade> basisMapFunc)
    {
        var termTable = LibBilinearCombination.Create(
            in1Type,
            in2Type,
            basisMapFunc
        ).SelectOutputType(Types);

        return termTable.OutputType;
    }

    public LibType GetOutType(int in1Grade, int in2Grade, Func<RGaBasisBlade, RGaBasisBlade, IRGaSignedBasisBlade> basisMapFunc)
    {
        return GetOutType(
            KVectorTypes[in1Grade], 
            KVectorTypes[in2Grade], 
            basisMapFunc
        );
    }

    public LibType GetLcpOutType(LibType in1Type, LibType in2Type)
    {
        return GetOutType(
            in1Type, 
            in2Type, 
            (b1, b2) => b1.Lcp(b2)
        );
    }
        
    public LibType GetLcpOutType(int in1Grade, int in2Grade)
    {
        return GetOutType(
            in1Grade, 
            in2Grade, 
            (b1, b2) => b1.Lcp(b2)
        );
    }


    public LibMainCodeComposer CreateMainCodeComposer()
    {
        return new LibMainCodeComposer(this);
    }
}