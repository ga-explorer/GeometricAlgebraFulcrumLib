using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Combinations;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MetaProgramming.Applications.Matlab.GradedMultivectorsLib.Types;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.Matlab.GradedMultivectorsLib.Combinations;

public class LibTrilinearCombination :
    ILibLinearCombination
{
    public static LibTrilinearCombination Create(LibType in1Type, LibType in2Type, LibType in3Type, IEnumerable<RGaFloat64TrilinearCombinationTerm> termList)
    {
        var termTable = new LibTrilinearCombination(
            in1Type, 
            in2Type, 
            in3Type, 
            RGaFloat64TrilinearCombinationTerm.InputsKind.Distinct
        );

        termTable.Add(termList);

        return termTable;
    }
    
    public static LibTrilinearCombination Create(LibType in1Type, LibType in2Type, LibType in3Type, Func<RGaBasisBlade, RGaBasisBlade, RGaBasisBlade, IRGaSignedBasisBlade> basisMapFunc)
    {
        var termTable = new LibTrilinearCombination(
            in1Type, 
            in2Type, 
            in3Type,
            RGaFloat64TrilinearCombinationTerm.InputsKind.Distinct
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

    public static LibTrilinearCombination Create(LibType in1Type, LibType in2Type, LibType in3Type, Func<RGaBasisBlade, RGaBasisBlade, RGaBasisBlade, RGaFloat64Multivector> basisMapFunc)
    {
        var termTable = new LibTrilinearCombination(
            in1Type, 
            in2Type, 
            in3Type,
            RGaFloat64TrilinearCombinationTerm.InputsKind.Distinct
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

    
    public static LibTrilinearCombination CreateFromEqualFirstSecondInputs(LibType inType12, LibType inType3, IEnumerable<RGaFloat64TrilinearCombinationTerm> termList)
    {
        var termTable = new LibTrilinearCombination(
            inType12, 
            inType12, 
            inType3, 
            RGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstSecond
        );
        
        termTable.Add(termList);

        return termTable;
    }

    public static LibTrilinearCombination CreateFromEqualFirstSecondInputs(LibType inType12, LibType inType3, Func<RGaBasisBlade, RGaBasisBlade, RGaBasisBlade, IRGaSignedBasisBlade> basisMapFunc)
    {
        var termTable = new LibTrilinearCombination(
            inType12, 
            inType12, 
            inType3, 
            RGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstSecond
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
    
    public static LibTrilinearCombination CreateFromEqualFirstSecondInputs(LibType inType12, LibType inType3, Func<RGaBasisBlade, RGaBasisBlade, RGaBasisBlade, RGaFloat64Multivector> basisMapFunc)
    {
        var termTable = new LibTrilinearCombination(
            inType12, 
            inType12, 
            inType3, 
            RGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstSecond
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

    
    public static LibTrilinearCombination CreateFromEqualFirstThirdInputs(LibType inType13, LibType inType2, IEnumerable<RGaFloat64TrilinearCombinationTerm> termList)
    {
        var termTable = new LibTrilinearCombination(
            inType13, 
            inType2, 
            inType13, 
            RGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstThird
        );
        
        termTable.Add(termList);

        return termTable;
    }

    public static LibTrilinearCombination CreateFromEqualFirstThirdInputs(LibType inType13, LibType inType2, Func<RGaBasisBlade, RGaBasisBlade, RGaBasisBlade, IRGaSignedBasisBlade> basisMapFunc)
    {
        var termTable = new LibTrilinearCombination(
            inType13, 
            inType2, 
            inType13, 
            RGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstThird
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

    public static LibTrilinearCombination CreateFromEqualFirstThirdInputs(LibType inType13, LibType inType2, Func<RGaBasisBlade, RGaBasisBlade, RGaBasisBlade, RGaFloat64Multivector> basisMapFunc)
    {
        var termTable = new LibTrilinearCombination(
            inType13, 
            inType2, 
            inType13, 
            RGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstThird
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
    
    
    public static LibTrilinearCombination CreateFromEqualSecondThirdInputs(LibType inType1, LibType inType23, IEnumerable<RGaFloat64TrilinearCombinationTerm> termList)
    {
        var termTable = new LibTrilinearCombination(
            inType1, 
            inType23, 
            inType23, 
            RGaFloat64TrilinearCombinationTerm.InputsKind.EqualSecondThird
        );
        
        termTable.Add(termList);
        
        return termTable;
    }

    public static LibTrilinearCombination CreateFromEqualSecondThirdInputs(LibType inType1, LibType inType23, Func<RGaBasisBlade, RGaBasisBlade, RGaBasisBlade, IRGaSignedBasisBlade> basisMapFunc)
    {
        var termTable = new LibTrilinearCombination(
            inType1, 
            inType23, 
            inType23, 
            RGaFloat64TrilinearCombinationTerm.InputsKind.EqualSecondThird
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

    public static LibTrilinearCombination CreateFromEqualSecondThirdInputs(LibType inType1, LibType inType23, Func<RGaBasisBlade, RGaBasisBlade, RGaBasisBlade, RGaFloat64Multivector> basisMapFunc)
    {
        var termTable = new LibTrilinearCombination(
            inType1, 
            inType23, 
            inType23, 
            RGaFloat64TrilinearCombinationTerm.InputsKind.EqualSecondThird
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
    

    public static LibTrilinearCombination CreateFromEqualInputs(LibType inType, IEnumerable<RGaFloat64TrilinearCombinationTerm> termList)
    {
        var termTable = new LibTrilinearCombination(
            inType,
            inType,
            inType,
            RGaFloat64TrilinearCombinationTerm.InputsKind.Equal
        );

        termTable.Add(termList);

        return termTable;
    }

    public static LibTrilinearCombination CreateFromEqualInputs(LibType inType, Func<RGaBasisBlade, RGaBasisBlade, RGaBasisBlade, IRGaSignedBasisBlade> basisMapFunc)
    {
        var termTable = new LibTrilinearCombination(
            inType,
            inType,
            inType,
            RGaFloat64TrilinearCombinationTerm.InputsKind.Equal
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

    public static LibTrilinearCombination CreateFromEqualInputs(LibType inType, Func<RGaBasisBlade, RGaBasisBlade, RGaBasisBlade, RGaFloat64Multivector> basisMapFunc)
    {
        var termTable = new LibTrilinearCombination(
            inType, 
            inType, 
            inType, 
            RGaFloat64TrilinearCombinationTerm.InputsKind.Equal
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


    private readonly RGaFloat64TrilinearCombination _trilinearCombination;


    public RGaFloat64TrilinearCombinationTerm.InputsKind InputsKind
        => _trilinearCombination.InputsKind;
    
    public IRGaLinearCombination InnerLinearCombination 
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

    public IEnumerable<RGaFloat64TrilinearCombinationTerm> Terms
        => _trilinearCombination;


    private LibTrilinearCombination(LibType in1Type, LibType in2Type, LibType in3Type, RGaFloat64TrilinearCombinationTerm.InputsKind inputsKind)
    {
        _trilinearCombination = new RGaFloat64TrilinearCombination(inputsKind);
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

    public IReadOnlyList<int> GetOutputBasisBladeGrades()
    {
        return _trilinearCombination.GetOutputBasisBladeGrades();
    }

    public IReadOnlyList<int> GetInputBasisBladeGrades()
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

    public IReadOnlyList<int> GetInputBasisBladeIDs()
    {
        return _trilinearCombination
            .GetInputBasisBladeIDs()
            .Select(id => (int)id)
            .ToImmutableSortedSet();
    }

    public IReadOnlyList<int> GetInput1BasisBladeIDs()
    {
        return _trilinearCombination
            .GetInput1BasisBladeIDs()
            .Select(id => (int)id)
            .ToImmutableSortedSet();
    }

    public IReadOnlyList<int> GetInput2BasisBladeIDs()
    {
        return _trilinearCombination
            .GetInput2BasisBladeIDs()
            .Select(id => (int)id)
            .ToImmutableSortedSet();
    }
    
    public IReadOnlyList<int> GetInput3BasisBladeIDs()
    {
        return _trilinearCombination
            .GetInput3BasisBladeIDs()
            .Select(id => (int)id)
            .ToImmutableSortedSet();
    }
    
    public IReadOnlyList<int> GetInput12BasisBladeIDs()
    {
        return _trilinearCombination
            .GetInput12BasisBladeIDs()
            .Select(id => (int)id)
            .ToImmutableSortedSet();
    }
    
    public IReadOnlyList<int> GetInput13BasisBladeIDs()
    {
        return _trilinearCombination
            .GetInput13BasisBladeIDs()
            .Select(id => (int)id)
            .ToImmutableSortedSet();
    }
    
    public IReadOnlyList<int> GetInput23BasisBladeIDs()
    {
        return _trilinearCombination
            .GetInput23BasisBladeIDs()
            .Select(id => (int)id)
            .ToImmutableSortedSet();
    }

    public IReadOnlyList<int> GetOutputBasisBladeIDs()
    {
        return _trilinearCombination
            .GetOutputBasisBladeIDs()
            .Select(id => (int)id)
            .ToImmutableSortedSet();
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


    public LibTrilinearCombination Add(RGaFloat64TrilinearCombinationTerm term)
    {
        _trilinearCombination.Add(term);

        return this;
    }

    public LibTrilinearCombination Add(RGaBasisBlade input1BasisBlade, RGaBasisBlade input2BasisBlade, RGaBasisBlade input3BasisBlade, IRGaSignedBasisBlade outputBasisBlade)
    {
        _trilinearCombination.Add(
            input1BasisBlade,
            input2BasisBlade,
            input3BasisBlade,
            outputBasisBlade
        );

        return this;
    }

    public LibTrilinearCombination Add(Float64Scalar inputScalar, RGaBasisBlade input1BasisBlade, RGaBasisBlade input2BasisBlade, RGaBasisBlade input3BasisBlade, RGaBasisBlade outputBasisBlade)
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

    public LibTrilinearCombination Add(RGaBasisBlade input1BasisBlade, RGaBasisBlade input2BasisBlade, RGaBasisBlade input3BasisBlade, RGaFloat64Multivector outputMultivector)
    {
        _trilinearCombination.Add(
            input1BasisBlade,
            input2BasisBlade,
            input3BasisBlade,
            outputMultivector
        );

        return this;
    }

    public LibTrilinearCombination Add(IEnumerable<RGaFloat64TrilinearCombinationTerm> termList)
    {
        _trilinearCombination.Add(termList);

        return this;
    }
    
    
    public LibTrilinearCombination FilterTerms(LibType input1Type, LibType input2Type, LibType input3Type, LibType outputType, Func<RGaFloat64TrilinearCombinationTerm, bool> termFilter)
    {
        return Create(
            input1Type,
            input2Type,
            input3Type,
            _trilinearCombination.GetSubCombination(termFilter)
        ).SetOutputType(outputType);
    }


    public SortedDictionary<int, RGaFloat64TrilinearCombination> GetIdTermListPairs()
    {
        var idList = new SortedDictionary<int, RGaFloat64TrilinearCombination>();

        var groupList =
            _trilinearCombination
                .GroupBy(term => term.OutputBasisBladeId);

        foreach (var group in groupList)
        {
            var key = (int)group.Key;
            var value =
                new RGaFloat64TrilinearCombination(InputsKind).Add(group);

            idList.Add(key, value);
        }

        return idList;
    }
    
    public SortedDictionary<int, RGaFloat64TrilinearCombination> GetIdTermListPairs(Func<RGaFloat64TrilinearCombinationTerm, bool> termFilter)
    {
        var idList = new SortedDictionary<int, RGaFloat64TrilinearCombination>();

        var groupList =
            _trilinearCombination
                .Where(termFilter)
                .GroupBy(term => term.OutputBasisBladeId);

        foreach (var group in groupList)
        {
            var key = (int)group.Key;
            var value =
                new RGaFloat64TrilinearCombination(InputsKind).Add(group);

            idList.Add(key, value);
        }

        return idList;
    }

    public SortedDictionary<int, RGaFloat64TrilinearCombination> GetIdTermListPairsForOutGrade(int grade)
    {
        var idList = new SortedDictionary<int, RGaFloat64TrilinearCombination>();

        var groupList =
            _trilinearCombination
                .Where(term => term.OutputBasisBladeGrade == grade)
                .GroupBy(term => term.OutputBasisBladeId);

        foreach (var group in groupList)
        {
            var key = (int)group.Key;
            var value =
                new RGaFloat64TrilinearCombination(InputsKind).Add(group);

            idList.Add(key, value);
        }

        return idList;
    }
}