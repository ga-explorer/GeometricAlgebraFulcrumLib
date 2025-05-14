using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Combinations;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.Matlab.GradedMultivectorsLib.Types;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.Matlab.GradedMultivectorsLib.Combinations;

public class LibBilinearCombination :
    ILibLinearCombination
{
    public static LibBilinearCombination Create(LibType in1Type, LibType in2Type, IEnumerable<XGaFloat64BilinearCombinationTerm> termList)
    {
        var termTable = new LibBilinearCombination(in1Type, in2Type, false);

        termTable.Add(termList);

        return termTable;
    }

    public static LibBilinearCombination Create(LibType in1Type, LibType in2Type, Func<XGaBasisBlade, XGaBasisBlade, IXGaSignedBasisBlade> basisMapFunc)
    {
        var termTable = new LibBilinearCombination(in1Type, in2Type, false);

        var inBasisBladeList1 = in1Type.GetBasisBlades();
        var inBasisBladeList2 = in2Type.GetBasisBlades();

        foreach (var input1BasisBlade in inBasisBladeList1)
            foreach (var input2BasisBlade in inBasisBladeList2)
            {
                var outBasisBlade = basisMapFunc(input1BasisBlade, input2BasisBlade);

                if (outBasisBlade.IsZero) continue;

                termTable.Add(
                    input1BasisBlade,
                    input2BasisBlade,
                    outBasisBlade
                );
            }

        return termTable;
    }

    public static LibBilinearCombination Create(LibType in1Type, LibType in2Type, Func<XGaBasisBlade, XGaBasisBlade, XGaFloat64Multivector> basisMapFunc)
    {
        var termTable = new LibBilinearCombination(in1Type, in2Type, false);

        var inBasisBladeList1 = in1Type.GetBasisBlades();
        var inBasisBladeList2 = in2Type.GetBasisBlades();

        foreach (var input1BasisBlade in inBasisBladeList1)
            foreach (var input2BasisBlade in inBasisBladeList2)
            {
                var outMultivector = basisMapFunc(input1BasisBlade, input2BasisBlade);

                if (outMultivector.IsZero) continue;

                termTable.Add(
                    input1BasisBlade,
                    input2BasisBlade,
                    outMultivector
                );
            }

        return termTable;
    }


    public static LibBilinearCombination CreateFromEqualInputs(LibType inType, IEnumerable<XGaFloat64BilinearCombinationTerm> termList)
    {
        var termTable = new LibBilinearCombination(inType);

        termTable.Add(termList);

        return termTable;
    }

    public static LibBilinearCombination CreateFromEqualInputs(LibType inType, Func<XGaBasisBlade, XGaBasisBlade, IXGaSignedBasisBlade> basisMapFunc)
    {
        var termTable = new LibBilinearCombination(inType);

        var inBasisBladeList = inType.GetBasisBlades();

        foreach (var input1BasisBlade in inBasisBladeList)
            foreach (var input2BasisBlade in inBasisBladeList)
            {
                var outBasisBlade = basisMapFunc(input1BasisBlade, input2BasisBlade);

                if (outBasisBlade.IsZero) continue;

                termTable.Add(
                    input1BasisBlade,
                    input2BasisBlade,
                    outBasisBlade
                );
            }

        return termTable;
    }

    public static LibBilinearCombination CreateFromEqualInputs(LibType inType, Func<XGaBasisBlade, XGaBasisBlade, XGaFloat64Multivector> basisMapFunc)
    {
        var termTable = new LibBilinearCombination(inType);

        var inBasisBladeList = inType.GetBasisBlades();

        foreach (var input1BasisBlade in inBasisBladeList)
            foreach (var input2BasisBlade in inBasisBladeList)
            {
                var outMultivector = basisMapFunc(input1BasisBlade, input2BasisBlade);

                if (outMultivector.IsZero) continue;

                termTable.Add(
                    input1BasisBlade,
                    input2BasisBlade,
                    outMultivector
                );
            }

        return termTable;
    }


    private readonly XGaFloat64BilinearCombination _bilinearCombination;


    public bool AssumeEqualInputs
        => _bilinearCombination.AssumeEqualInputs;
    
    public IXGaLinearCombination InnerLinearCombination 
        => _bilinearCombination;

    public LibType Input1Type { get; }

    public LibType Input2Type { get; }

    public LibType OutputType { get; private set; }
    
    public bool IsEmpty 
        => _bilinearCombination.IsEmpty;

    public int VSpaceDimensions
        => Input1Type.VSpaceDimensions;
    
    public int GaSpaceDimensions
        => Input1Type.GaSpaceDimensions;

    public IEnumerable<XGaFloat64BilinearCombinationTerm> Terms
        => _bilinearCombination;


    private LibBilinearCombination(LibType in1Type, LibType in2Type, bool assumeEqualInputs)
    {
        _bilinearCombination = new XGaFloat64BilinearCombination(assumeEqualInputs);
        Input1Type = in1Type;
        Input2Type = in2Type;
    }

    private LibBilinearCombination(LibType inType)
    {
        _bilinearCombination = new XGaFloat64BilinearCombination(true);
        Input1Type = inType;
        Input2Type = inType;
    }

    
    public bool IsOutputScalar()
    {
        return _bilinearCombination.IsOutputScalar();
    }

    public bool IsOutputVector()
    {
        return _bilinearCombination.IsOutputVector();
    }

    public bool IsOutputBivector()
    {
        return _bilinearCombination.IsOutputBivector();
    }

    public bool IsOutputKVector()
    {
        return _bilinearCombination.IsOutputKVector();
    }

    public bool IsOutputKVector(int grade)
    {
        return _bilinearCombination.IsOutputKVector(grade);
    }

    public IndexSet GetOutputBasisBladeGrades()
    {
        return _bilinearCombination.GetOutputBasisBladeGrades();
    }

    public IndexSet GetInputBasisBladeGrades()
    {
        return _bilinearCombination.GetInputBasisBladeGrades();
    }

    public IndexSet GetInput1BasisBladeGrades()
    {
        return _bilinearCombination.GetInput1BasisBladeGrades();
    }

    public IndexSet GetInput2BasisBladeGrades()
    {
        return _bilinearCombination.GetInput2BasisBladeGrades();
    }

    public ImmutableSortedSet<IndexSet> GetInputBasisBladeIDs()
    {
        return _bilinearCombination.GetInputBasisBladeIDs();
    }

    public ImmutableSortedSet<IndexSet> GetInput1BasisBladeIDs()
    {
        return _bilinearCombination.GetInput1BasisBladeIDs();
    }

    public ImmutableSortedSet<IndexSet> GetInput2BasisBladeIDs()
    {
        return _bilinearCombination.GetInput2BasisBladeIDs();
    }
    
    public ImmutableSortedSet<IndexSet> GetOutputBasisBladeIDs()
    {
        return _bilinearCombination.GetOutputBasisBladeIDs();
    }


    public LibBilinearCombination SetOutputType(LibType outTypeInfo)
    {
        OutputType = outTypeInfo;

        return this;
    }

    public LibBilinearCombination SelectOutputType(IReadOnlyList<LibType> typeList)
    {
        var outGradeList = GetOutputBasisBladeGrades();

        OutputType = outGradeList.Count switch
        {
            0 => typeList[0],
            1 => typeList[outGradeList[0]],
            _ => typeList[^1]
        };

        return this;
    }


    public LibBilinearCombination Add(XGaFloat64BilinearCombinationTerm term)
    {
        _bilinearCombination.Add(term);

        return this;
    }

    public LibBilinearCombination Add(XGaBasisBlade input1BasisBlade, XGaBasisBlade input2BasisBlade, IXGaSignedBasisBlade outputBasisBlade)
    {
        _bilinearCombination.Add(
            input1BasisBlade,
            input2BasisBlade,
            outputBasisBlade
        );

        return this;
    }

    public LibBilinearCombination Add(Float64Scalar inputScalar, XGaBasisBlade input1BasisBlade, XGaBasisBlade input2BasisBlade, XGaBasisBlade outputBasisBlade)
    {
        _bilinearCombination.Add(
            inputScalar,
            input1BasisBlade,
            input2BasisBlade,
            outputBasisBlade
        );

        return this;
    }

    public LibBilinearCombination Add(XGaBasisBlade input1BasisBlade, XGaBasisBlade input2BasisBlade, XGaFloat64Multivector outputMultivector)
    {
        _bilinearCombination.Add(
            input1BasisBlade,
            input2BasisBlade,
            outputMultivector
        );

        return this;
    }
    
    public LibBilinearCombination Add(IEnumerable<XGaFloat64BilinearCombinationTerm> termList)
    {
        _bilinearCombination.Add(termList);

        return this;
    }

    
    public LibBilinearCombination FilterTerms(LibType input1Type, LibType input2Type, LibType outputType, Func<XGaFloat64BilinearCombinationTerm, bool> termFilter)
    {
        return Create(
            input1Type,
            input2Type,
            _bilinearCombination.GetSubCombination(termFilter)
        ).SetOutputType(outputType);
    }


    public SortedDictionary<int, XGaFloat64BilinearCombination> GetIdTermListPairs()
    {
        var idList = new SortedDictionary<int, XGaFloat64BilinearCombination>();

        var groupList =
            _bilinearCombination
                .GroupBy(term => term.OutputBasisBladeId);

        foreach (var group in groupList)
        {
            var key = (int)group.Key;
            var value =
                new XGaFloat64BilinearCombination(AssumeEqualInputs).Add(group);

            idList.Add(key, value);
        }

        return idList;
    }
    
    public SortedDictionary<int, XGaFloat64BilinearCombination> GetIdTermListPairs(Func<XGaFloat64BilinearCombinationTerm, bool> termFilter)
    {
        var idList = new SortedDictionary<int, XGaFloat64BilinearCombination>();

        var groupList =
            _bilinearCombination
                .Where(termFilter)
                .GroupBy(term => term.OutputBasisBladeId);

        foreach (var group in groupList)
        {
            var key = (int)group.Key;
            var value =
                new XGaFloat64BilinearCombination(AssumeEqualInputs).Add(group);

            idList.Add(key, value);
        }

        return idList;
    }

    public SortedDictionary<int, XGaFloat64BilinearCombination> GetIdTermListPairsForOutGrade(int grade)
    {
        var idList = new SortedDictionary<int, XGaFloat64BilinearCombination>();

        var groupList =
            _bilinearCombination
                .Where(term => term.OutputBasisBladeGrade == grade)
                .GroupBy(term => term.OutputBasisBladeId);

        foreach (var group in groupList)
        {
            var key = (int)group.Key;
            var value =
                new XGaFloat64BilinearCombination(AssumeEqualInputs).Add(group);

            idList.Add(key, value);
        }

        return idList;
    }
}