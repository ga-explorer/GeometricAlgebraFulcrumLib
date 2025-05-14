using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Combinations;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.GradedMultivectorsLib.Types;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.GradedMultivectorsLib.Combinations;

public class LibTrilinearCombination :
    ILibLinearCombination
{
    public static LibTrilinearCombination Create(LibType in1Type, LibType in2Type, LibType in3Type, IEnumerable<XGaFloat64TrilinearCombinationTerm> termList)
    {
        var termTable = new LibTrilinearCombination(
            in1Type, 
            in2Type, 
            in3Type, 
            XGaFloat64TrilinearCombinationTerm.InputsKind.Distinct
        );

        termTable.Add(termList);

        return termTable;
    }
    
    public static LibTrilinearCombination Create(LibType in1Type, LibType in2Type, LibType in3Type, Func<XGaBasisBlade, XGaBasisBlade, XGaBasisBlade, IXGaSignedBasisBlade> basisMapFunc)
    {
        var termTable = new LibTrilinearCombination(
            in1Type, 
            in2Type, 
            in3Type,
            XGaFloat64TrilinearCombinationTerm.InputsKind.Distinct
        );

        var inBasisBladeList1 = in1Type.GetBasisBlades();
        var inBasisBladeList2 = in2Type.GetBasisBlades();
        var inBasisBladeList3 = in3Type.GetBasisBlades();

        foreach (var input1BasisBlade in inBasisBladeList1)
        foreach (var input2BasisBlade in inBasisBladeList2)
        foreach (var input3BasisBlade in inBasisBladeList3)
        {
            var outputBasisBlade = basisMapFunc(
                input1BasisBlade, 
                input2BasisBlade, 
                input3BasisBlade
            );

            if (outputBasisBlade.IsZero) continue;

            termTable.Add(
                input1BasisBlade,
                input2BasisBlade,
                input3BasisBlade,
                outputBasisBlade
            );
        }

        return termTable;
    }

    public static LibTrilinearCombination Create(LibType in1Type, LibType in2Type, LibType in3Type, Func<XGaBasisBlade, XGaBasisBlade, XGaBasisBlade, XGaFloat64Multivector> basisMapFunc)
    {
        var termTable = new LibTrilinearCombination(
            in1Type, 
            in2Type, 
            in3Type,
            XGaFloat64TrilinearCombinationTerm.InputsKind.Distinct
        );

        var inBasisBladeList1 = in1Type.GetBasisBlades();
        var inBasisBladeList2 = in2Type.GetBasisBlades();
        var inBasisBladeList3 = in3Type.GetBasisBlades();

        foreach (var input1BasisBlade in inBasisBladeList1)
        foreach (var input2BasisBlade in inBasisBladeList2)
        foreach (var input3BasisBlade in inBasisBladeList3)
        {
            var outMultivector = basisMapFunc(
                input1BasisBlade, 
                input2BasisBlade, 
                input3BasisBlade
            );

            if (outMultivector.IsZero) continue;

            termTable.Add(
                input1BasisBlade,
                input2BasisBlade,
                input3BasisBlade,
                outMultivector
            );
        }

        return termTable;
    }

    
    public static LibTrilinearCombination CreateFromEqualFirstSecondInputs(LibType inType12, LibType inType3, IEnumerable<XGaFloat64TrilinearCombinationTerm> termList)
    {
        var termTable = new LibTrilinearCombination(
            inType12, 
            inType12, 
            inType3, 
            XGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstSecond
        );
        
        termTable.Add(termList);

        return termTable;
    }

    public static LibTrilinearCombination CreateFromEqualFirstSecondInputs(LibType inType12, LibType inType3, Func<XGaBasisBlade, XGaBasisBlade, XGaBasisBlade, IXGaSignedBasisBlade> basisMapFunc)
    {
        var termTable = new LibTrilinearCombination(
            inType12, 
            inType12, 
            inType3, 
            XGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstSecond
        );

        var inBasisBladeList12 = inType12.GetBasisBlades();
        var inBasisBladeList3 = inType3.GetBasisBlades();

        foreach (var input1BasisBlade in inBasisBladeList12)
        foreach (var input2BasisBlade in inBasisBladeList12)
        foreach (var input3BasisBlade in inBasisBladeList3)
        {
            var outMultivector = basisMapFunc(
                input1BasisBlade, 
                input2BasisBlade,
                input3BasisBlade
            );

            if (outMultivector.IsZero) continue;

            termTable.Add(
                input1BasisBlade,
                input2BasisBlade,
                input3BasisBlade,
                outMultivector
            );
        }

        return termTable;
    }
    
    public static LibTrilinearCombination CreateFromEqualFirstSecondInputs(LibType inType12, LibType inType3, Func<XGaBasisBlade, XGaBasisBlade, XGaBasisBlade, XGaFloat64Multivector> basisMapFunc)
    {
        var termTable = new LibTrilinearCombination(
            inType12, 
            inType12, 
            inType3, 
            XGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstSecond
        );

        var inBasisBladeList12 = inType12.GetBasisBlades();
        var inBasisBladeList3 = inType3.GetBasisBlades();

        foreach (var input1BasisBlade in inBasisBladeList12)
        foreach (var input2BasisBlade in inBasisBladeList12)
        foreach (var input3BasisBlade in inBasisBladeList3)
        {
            var outMultivector = basisMapFunc(
                input1BasisBlade, 
                input2BasisBlade,
                input3BasisBlade
            );

            if (outMultivector.IsZero) continue;

            termTable.Add(
                input1BasisBlade,
                input2BasisBlade,
                input3BasisBlade,
                outMultivector
            );
        }

        return termTable;
    }

    
    public static LibTrilinearCombination CreateFromEqualFirstThirdInputs(LibType inType13, LibType inType2, IEnumerable<XGaFloat64TrilinearCombinationTerm> termList)
    {
        var termTable = new LibTrilinearCombination(
            inType13, 
            inType2, 
            inType13, 
            XGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstThird
        );
        
        termTable.Add(termList);

        return termTable;
    }

    public static LibTrilinearCombination CreateFromEqualFirstThirdInputs(LibType inType13, LibType inType2, Func<XGaBasisBlade, XGaBasisBlade, XGaBasisBlade, IXGaSignedBasisBlade> basisMapFunc)
    {
        var termTable = new LibTrilinearCombination(
            inType13, 
            inType2, 
            inType13, 
            XGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstThird
        );

        var inBasisBladeList13 = inType13.GetBasisBlades();
        var inBasisBladeList2 = inType2.GetBasisBlades();

        foreach (var input1BasisBlade in inBasisBladeList13)
        foreach (var input2BasisBlade in inBasisBladeList2)
        foreach (var input3BasisBlade in inBasisBladeList13)
        {
            var outMultivector = basisMapFunc(
                input1BasisBlade, 
                input2BasisBlade,
                input3BasisBlade
            );

            if (outMultivector.IsZero) continue;

            termTable.Add(
                input1BasisBlade,
                input2BasisBlade,
                input3BasisBlade,
                outMultivector
            );
        }

        return termTable;
    }

    public static LibTrilinearCombination CreateFromEqualFirstThirdInputs(LibType inType13, LibType inType2, Func<XGaBasisBlade, XGaBasisBlade, XGaBasisBlade, XGaFloat64Multivector> basisMapFunc)
    {
        var termTable = new LibTrilinearCombination(
            inType13, 
            inType2, 
            inType13, 
            XGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstThird
        );

        var inBasisBladeList13 = inType13.GetBasisBlades();
        var inBasisBladeList2 = inType2.GetBasisBlades();

        foreach (var input1BasisBlade in inBasisBladeList13)
        foreach (var input2BasisBlade in inBasisBladeList2)
        foreach (var input3BasisBlade in inBasisBladeList13)
        {
            var outMultivector = basisMapFunc(
                input1BasisBlade, 
                input2BasisBlade,
                input3BasisBlade
            );

            if (outMultivector.IsZero) continue;

            termTable.Add(
                input1BasisBlade,
                input2BasisBlade,
                input3BasisBlade,
                outMultivector
            );
        }

        return termTable;
    }
    
    
    public static LibTrilinearCombination CreateFromEqualSecondThirdInputs(LibType inType1, LibType inType23, IEnumerable<XGaFloat64TrilinearCombinationTerm> termList)
    {
        var termTable = new LibTrilinearCombination(
            inType1, 
            inType23, 
            inType23, 
            XGaFloat64TrilinearCombinationTerm.InputsKind.EqualSecondThird
        );
        
        termTable.Add(termList);
        
        return termTable;
    }

    public static LibTrilinearCombination CreateFromEqualSecondThirdInputs(LibType inType1, LibType inType23, Func<XGaBasisBlade, XGaBasisBlade, XGaBasisBlade, IXGaSignedBasisBlade> basisMapFunc)
    {
        var termTable = new LibTrilinearCombination(
            inType1, 
            inType23, 
            inType23, 
            XGaFloat64TrilinearCombinationTerm.InputsKind.EqualSecondThird
        );

        var inBasisBladeList1 = inType1.GetBasisBlades();
        var inBasisBladeList23 = inType23.GetBasisBlades();

        foreach (var input1BasisBlade in inBasisBladeList1)
        foreach (var input2BasisBlade in inBasisBladeList23)
        foreach (var input3BasisBlade in inBasisBladeList23)
        {
            var outMultivector = basisMapFunc(
                input1BasisBlade, 
                input2BasisBlade,
                input3BasisBlade
            );

            if (outMultivector.IsZero) continue;

            termTable.Add(
                input1BasisBlade,
                input2BasisBlade,
                input3BasisBlade,
                outMultivector
            );
        }

        return termTable;
    }

    public static LibTrilinearCombination CreateFromEqualSecondThirdInputs(LibType inType1, LibType inType23, Func<XGaBasisBlade, XGaBasisBlade, XGaBasisBlade, XGaFloat64Multivector> basisMapFunc)
    {
        var termTable = new LibTrilinearCombination(
            inType1, 
            inType23, 
            inType23, 
            XGaFloat64TrilinearCombinationTerm.InputsKind.EqualSecondThird
        );

        var inBasisBladeList1 = inType1.GetBasisBlades();
        var inBasisBladeList23 = inType23.GetBasisBlades();

        foreach (var input1BasisBlade in inBasisBladeList1)
        foreach (var input2BasisBlade in inBasisBladeList23)
        foreach (var input3BasisBlade in inBasisBladeList23)
        {
            var outMultivector = basisMapFunc(
                input1BasisBlade, 
                input2BasisBlade,
                input3BasisBlade
            );

            if (outMultivector.IsZero) continue;

            termTable.Add(
                input1BasisBlade,
                input2BasisBlade,
                input3BasisBlade,
                outMultivector
            );
        }

        return termTable;
    }
    

    public static LibTrilinearCombination CreateFromEqualInputs(LibType inType, IEnumerable<XGaFloat64TrilinearCombinationTerm> termList)
    {
        var termTable = new LibTrilinearCombination(
            inType,
            inType,
            inType,
            XGaFloat64TrilinearCombinationTerm.InputsKind.Equal
        );

        termTable.Add(termList);

        return termTable;
    }

    public static LibTrilinearCombination CreateFromEqualInputs(LibType inType, Func<XGaBasisBlade, XGaBasisBlade, XGaBasisBlade, IXGaSignedBasisBlade> basisMapFunc)
    {
        var termTable = new LibTrilinearCombination(
            inType,
            inType,
            inType,
            XGaFloat64TrilinearCombinationTerm.InputsKind.Equal
        );

        var inBasisBladeList = inType.GetBasisBlades();

        foreach (var input1BasisBlade in inBasisBladeList)
        foreach (var input2BasisBlade in inBasisBladeList)
        foreach (var input3BasisBlade in inBasisBladeList)
        {
            var outBasisBlade = basisMapFunc(
                input1BasisBlade, 
                input2BasisBlade, 
                input3BasisBlade
            );

            if (outBasisBlade.IsZero) continue;

            termTable.Add(
                input1BasisBlade,
                input2BasisBlade,
                input3BasisBlade,
                outBasisBlade
            );
        }

        return termTable;
    }

    public static LibTrilinearCombination CreateFromEqualInputs(LibType inType, Func<XGaBasisBlade, XGaBasisBlade, XGaBasisBlade, XGaFloat64Multivector> basisMapFunc)
    {
        var termTable = new LibTrilinearCombination(
            inType, 
            inType, 
            inType, 
            XGaFloat64TrilinearCombinationTerm.InputsKind.Equal
        );

        var inBasisBladeList = inType.GetBasisBlades();

        foreach (var input1BasisBlade in inBasisBladeList)
        foreach (var input2BasisBlade in inBasisBladeList)
        foreach (var input3BasisBlade in inBasisBladeList)
        {
            var outMultivector = basisMapFunc(
                input1BasisBlade, 
                input2BasisBlade,
                input3BasisBlade
            );

            if (outMultivector.IsZero) continue;

            termTable.Add(
                input1BasisBlade,
                input2BasisBlade,
                input3BasisBlade,
                outMultivector
            );
        }

        return termTable;
    }


    private readonly XGaFloat64TrilinearCombination _trilinearCombination;


    public XGaFloat64TrilinearCombinationTerm.InputsKind InputsKind
        => _trilinearCombination.InputsKind;
    
    public IXGaLinearCombination InnerLinearCombination 
        => _trilinearCombination;

    public LibType Input1Type { get; }

    public LibType Input2Type { get; }
    
    public LibType Input3Type { get; }

    public LibType OutputType { get; private set; }
    
    public bool IsEmpty 
        => _trilinearCombination.IsEmpty;

    public int VSpaceDimensions
        => Input1Type.VSpaceDimensions;
    
    public int GaSpaceDimensions
        => Input1Type.GaSpaceDimensions;

    public IEnumerable<XGaFloat64TrilinearCombinationTerm> Terms
        => _trilinearCombination;


    private LibTrilinearCombination(LibType in1Type, LibType in2Type, LibType in3Type, XGaFloat64TrilinearCombinationTerm.InputsKind inputsKind)
    {
        _trilinearCombination = new XGaFloat64TrilinearCombination(inputsKind);
        Input1Type = in1Type;
        Input2Type = in2Type;
        Input3Type = in3Type;
    }
    
    
    public bool IsOutputScalar()
    {
        return _trilinearCombination.IsOutputScalar();
    }

    public bool IsOutputVector()
    {
        return _trilinearCombination.IsOutputVector();
    }

    public bool IsOutputBivector()
    {
        return _trilinearCombination.IsOutputBivector();
    }

    public bool IsOutputKVector()
    {
        return _trilinearCombination.IsOutputKVector();
    }

    public bool IsOutputKVector(int grade)
    {
        return _trilinearCombination.IsOutputKVector(grade);
    }

    public IndexSet GetOutputBasisBladeGrades()
    {
        return _trilinearCombination.GetOutputBasisBladeGrades();
    }

    public IndexSet GetInputBasisBladeGrades()
    {
        return _trilinearCombination.GetInputBasisBladeGrades();
    }

    public IReadOnlyList<int> GetInput1BasisBladeGrades()
    {
        return _trilinearCombination.GetInput1BasisBladeGrades();
    }

    public IReadOnlyList<int> GetInput2BasisBladeGrades()
    {
        return _trilinearCombination.GetInput2BasisBladeGrades();
    }
    
    public IReadOnlyList<int> GetInput3BasisBladeGrades()
    {
        return _trilinearCombination.GetInput3BasisBladeGrades();
    }

    public IReadOnlyList<int> GetInput12BasisBladeGrades()
    {
        return _trilinearCombination.GetInput12BasisBladeGrades();
    }
    
    public IReadOnlyList<int> GetInput13BasisBladeGrades()
    {
        return _trilinearCombination.GetInput13BasisBladeGrades();
    }
    
    public IReadOnlyList<int> GetInput23BasisBladeGrades()
    {
        return _trilinearCombination.GetInput23BasisBladeGrades();
    }

    public IReadOnlyList<IndexSet> GetInputBasisBladeIDs()
    {
        return _trilinearCombination.GetInputBasisBladeIDs();
    }

    public IReadOnlyList<IndexSet> GetInput1BasisBladeIDs()
    {
        return _trilinearCombination.GetInput1BasisBladeIDs();
    }

    public IReadOnlyList<IndexSet> GetInput2BasisBladeIDs()
    {
        return _trilinearCombination.GetInput2BasisBladeIDs();
    }
    
    public IReadOnlyList<IndexSet> GetInput3BasisBladeIDs()
    {
        return _trilinearCombination.GetInput3BasisBladeIDs();
    }
    
    public IReadOnlyList<IndexSet> GetInput12BasisBladeIDs()
    {
        return _trilinearCombination.GetInput12BasisBladeIDs();
    }
    
    public IReadOnlyList<IndexSet> GetInput13BasisBladeIDs()
    {
        return _trilinearCombination.GetInput13BasisBladeIDs();
    }
    
    public IReadOnlyList<IndexSet> GetInput23BasisBladeIDs()
    {
        return _trilinearCombination.GetInput23BasisBladeIDs();
    }

    public IReadOnlyList<IndexSet> GetOutputBasisBladeIDs()
    {
        return _trilinearCombination.GetOutputBasisBladeIDs();
    }


    public LibTrilinearCombination SetOutputType(LibType outTypeInfo)
    {
        OutputType = outTypeInfo;

        return this;
    }

    public LibTrilinearCombination SelectOutputType(IReadOnlyList<LibType> typeList)
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


    public LibTrilinearCombination Add(XGaFloat64TrilinearCombinationTerm term)
    {
        _trilinearCombination.Add(term);

        return this;
    }

    public LibTrilinearCombination Add(XGaBasisBlade input1BasisBlade, XGaBasisBlade input2BasisBlade, XGaBasisBlade input3BasisBlade, IXGaSignedBasisBlade outputBasisBlade)
    {
        _trilinearCombination.Add(
            input1BasisBlade,
            input2BasisBlade,
            input3BasisBlade,
            outputBasisBlade
        );

        return this;
    }

    public LibTrilinearCombination Add(Float64Scalar inputScalar, XGaBasisBlade input1BasisBlade, XGaBasisBlade input2BasisBlade, XGaBasisBlade input3BasisBlade, XGaBasisBlade outputBasisBlade)
    {
        _trilinearCombination.Add(
            inputScalar,
            input1BasisBlade,
            input2BasisBlade,
            input3BasisBlade,
            outputBasisBlade
        );

        return this;
    }

    public LibTrilinearCombination Add(XGaBasisBlade input1BasisBlade, XGaBasisBlade input2BasisBlade, XGaBasisBlade input3BasisBlade, XGaFloat64Multivector outputMultivector)
    {
        _trilinearCombination.Add(
            input1BasisBlade,
            input2BasisBlade,
            input3BasisBlade,
            outputMultivector
        );

        return this;
    }

    public LibTrilinearCombination Add(IEnumerable<XGaFloat64TrilinearCombinationTerm> termList)
    {
        _trilinearCombination.Add(termList);

        return this;
    }
    
    
    public LibTrilinearCombination FilterTerms(LibType input1Type, LibType input2Type, LibType input3Type, LibType outputType, Func<XGaFloat64TrilinearCombinationTerm, bool> termFilter)
    {
        return Create(
            input1Type,
            input2Type,
            input3Type,
            _trilinearCombination.GetSubCombination(termFilter)
        ).SetOutputType(outputType);
    }


    public SortedDictionary<int, XGaFloat64TrilinearCombination> GetIdTermListPairs()
    {
        var idList = new SortedDictionary<int, XGaFloat64TrilinearCombination>();

        var groupList =
            _trilinearCombination
                .GroupBy(term => term.OutputBasisBladeId);

        foreach (var group in groupList)
        {
            var key = (int)group.Key;
            var value =
                new XGaFloat64TrilinearCombination(InputsKind).Add(group);

            idList.Add(key, value);
        }

        return idList;
    }
    
    public SortedDictionary<int, XGaFloat64TrilinearCombination> GetIdTermListPairs(Func<XGaFloat64TrilinearCombinationTerm, bool> termFilter)
    {
        var idList = new SortedDictionary<int, XGaFloat64TrilinearCombination>();

        var groupList =
            _trilinearCombination
                .Where(termFilter)
                .GroupBy(term => term.OutputBasisBladeId);

        foreach (var group in groupList)
        {
            var key = (int)group.Key;
            var value =
                new XGaFloat64TrilinearCombination(InputsKind).Add(group);

            idList.Add(key, value);
        }

        return idList;
    }

    public SortedDictionary<int, XGaFloat64TrilinearCombination> GetIdTermListPairsForOutGrade(int grade)
    {
        var idList = new SortedDictionary<int, XGaFloat64TrilinearCombination>();

        var groupList =
            _trilinearCombination
                .Where(term => term.OutputBasisBladeGrade == grade)
                .GroupBy(term => term.OutputBasisBladeId);

        foreach (var group in groupList)
        {
            var key = (int)group.Key;
            var value =
                new XGaFloat64TrilinearCombination(InputsKind).Add(group);

            idList.Add(key, value);
        }

        return idList;
    }
}