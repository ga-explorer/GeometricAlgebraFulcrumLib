using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Combinations;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.Matlab.GradedMultivectorsLib.Types;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.Matlab.GradedMultivectorsLib.Combinations;

public class LibUnilinearCombination :
    ILibLinearCombination
{
    public static LibUnilinearCombination Create(LibType inputType, IEnumerable<XGaFloat64UnilinearCombinationTerm> termList)
    {
        var termTable = new LibUnilinearCombination(inputType);

        termTable.Add(termList);

        return termTable;
    }

    public static LibUnilinearCombination Create(LibType inputType, Func<XGaBasisBlade, IXGaSignedBasisBlade> basisMapFunc)
    {
        var termTable = new LibUnilinearCombination(inputType);

        var inputBasisBladeList = inputType.GetBasisBlades();

        foreach (var inputBasisBlade in inputBasisBladeList)
        {
            var outBasisBlade = basisMapFunc(inputBasisBlade);

            if (outBasisBlade.IsZero) continue;

            termTable.Add(
                inputBasisBlade,
                outBasisBlade
            );
        }

        return termTable;
    }

    public static LibUnilinearCombination Create(LibType inputType, Func<XGaBasisBlade, XGaFloat64Multivector> basisMapFunc)
    {
        var termTable = new LibUnilinearCombination(inputType);

        var inputBasisBladeList = inputType.GetBasisBlades();

        foreach (var input1BasisBlade in inputBasisBladeList)
        {
            var outMultivector = basisMapFunc(input1BasisBlade);

            if (outMultivector.IsZero) continue;

            termTable.Add(
                input1BasisBlade,
                outMultivector
            );
        }

        return termTable;
    }

    public static LibUnilinearCombination CreateFromOutermorphism(LibTypeMultivector inputType, double[,] vectorMapArray)
    {
        var termTable = new LibUnilinearCombination(inputType);

        termTable.AddOutermorphism(vectorMapArray);

        return termTable;
    }


    private readonly XGaFloat64UnilinearCombination _unilinearCombination;

    public IXGaLinearCombination InnerLinearCombination 
        => _unilinearCombination;

    public LibType InputType { get; }

    public LibType OutputType { get; private set; }
    
    public bool IsEmpty 
        => _unilinearCombination.IsEmpty;

    public int VSpaceDimensions
        => InputType.VSpaceDimensions;
    
    public int GaSpaceDimensions
        => InputType.GaSpaceDimensions;

    public IEnumerable<XGaFloat64UnilinearCombinationTerm> Terms
        => _unilinearCombination;

    
    private LibUnilinearCombination(LibType inputType)
    {
        _unilinearCombination = new XGaFloat64UnilinearCombination();
        InputType = inputType;
    }
    
    
    public bool IsOutputScalar()
    {
        return _unilinearCombination.IsOutputScalar();
    }

    public bool IsOutputVector()
    {
        return _unilinearCombination.IsOutputVector();
    }

    public bool IsOutputBivector()
    {
        return _unilinearCombination.IsOutputBivector();
    }
    
    public bool IsOutputKVector()
    {
        return _unilinearCombination.IsOutputKVector();
    }

    public bool IsOutputKVector(int grade)
    {
        return _unilinearCombination.IsOutputKVector(grade);
    }

    public bool IsKVectorIdentity(int grade)
    {
        var gradeList = _unilinearCombination.GetOutputBasisBladeGrades();

        if (gradeList.Count != 1 || gradeList[0] != grade)
            return false;

        var kvSpaceDimensions = 
            (int)VSpaceDimensions.GetBinomialCoefficient(grade);
        
        var idTermListPairs = 
            GetIdTermListPairs();

        if (idTermListPairs.Count != kvSpaceDimensions)
            return false;
        
        var i = 0;
        foreach (var (id, termList) in idTermListPairs)
        {
            var index = (int)((IndexSet)id).BasisBladeIdToIndex();
            if (i != index || termList.Count != 1 || !termList.First().IsPositiveIdentity)
                return false;

            i++;
        }

        return true;
    }

    public bool IsKVectorNegativeIdentity(int grade)
    {
        var gradeList = _unilinearCombination.GetOutputBasisBladeGrades();

        if (gradeList.Count != 1 || gradeList[0] != grade)
            return false;

        var kvSpaceDimensions = 
            (int)VSpaceDimensions.GetBinomialCoefficient(grade);
        
        var idTermListPairs = 
            GetIdTermListPairs();

        if (idTermListPairs.Count != kvSpaceDimensions)
            return false;
        
        var i = 0;
        foreach (var (id, termList) in idTermListPairs)
        {
            var index = (int)((IndexSet)id).BasisBladeIdToIndex();
            if (i != index || termList.Count != 1 || !termList.First().IsNegativeIdentity)
                return false;

            i++;
        }

        return true;
    }

    public bool IsGradePreserving()
    {
        return _unilinearCombination.All(term => term.IsGradePreserving);
    }

    public bool IsMultivectorIdentity()
    {
        var idTermListPairs = 
            GetIdTermListPairs();

        if (idTermListPairs.Count != GaSpaceDimensions)
            return false;
        
        var i = 0;
        foreach (var (id, termList) in idTermListPairs)
        {
            if (i != id || termList.Count != 1 || !termList.First().IsPositiveIdentity)
                return false;

            i++;
        }

        return true;
    }

    public bool IsMultivectorNegative()
    {
        var idTermListPairs = 
            GetIdTermListPairs();

        if (idTermListPairs.Count != GaSpaceDimensions)
            return false;
        
        var i = 0;
        foreach (var (id, termList) in idTermListPairs)
        {
            if (i != id || termList.Count != 1 || !termList.First().IsNegativeIdentity)
                return false;

            i++;
        }

        return true;
    }

    public IndexSet GetOutputBasisBladeGrades()
    {
        return _unilinearCombination.GetOutputBasisBladeGrades();
    }

    public IndexSet GetInputBasisBladeGrades()
    {
        return _unilinearCombination.GetInputBasisBladeGrades();
    }
    
    public ImmutableSortedSet<IndexSet> GetInputBasisBladeIDs()
    {
        return _unilinearCombination.GetInputBasisBladeIDs();
    }
    
    public ImmutableSortedSet<IndexSet> GetOutputBasisBladeIDs()
    {
        return _unilinearCombination.GetOutputBasisBladeIDs();
    }


    public LibUnilinearCombination SetOutputType(LibType outTypeInfo)
    {
        OutputType = outTypeInfo;

        return this;
    }

    public LibUnilinearCombination SelectOutputType(IReadOnlyList<LibType> typeList)
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


    public LibUnilinearCombination Add(XGaFloat64UnilinearCombinationTerm term)
    {
        _unilinearCombination.Add(term);

        return this;
    }

    public LibUnilinearCombination Add(XGaBasisBlade inputBasisBlade, IXGaSignedBasisBlade outputBasisBlade)
    {
        _unilinearCombination.Add(
            inputBasisBlade,
            outputBasisBlade
        );

        return this;
    }

    public LibUnilinearCombination Add(Float64Scalar inputScalar, XGaBasisBlade inputBasisBlade, XGaBasisBlade outputBasisBlade)
    {
        _unilinearCombination.Add(
            inputScalar,
            inputBasisBlade,
            outputBasisBlade
        );

        return this;
    }
    
    public LibUnilinearCombination Add(XGaBasisBlade inputBasisBlade, XGaFloat64Multivector outputMultivector)
    {
        _unilinearCombination.Add(
            inputBasisBlade,
            outputMultivector
        );

        return this;
    }
    
    public LibUnilinearCombination Add(IEnumerable<XGaFloat64UnilinearCombinationTerm> termList)
    {
        _unilinearCombination.Add(termList);

        return this;
    }

    public LibUnilinearCombination AddOutermorphism(double[,] vectorMapArray)
    {
        _unilinearCombination.AddOutermorphism(
            InputType.Metric, 
            vectorMapArray
        );

        return this;
    }


    public LibUnilinearCombination FilterTerms(LibType inputType, LibType outputType, Func<XGaFloat64UnilinearCombinationTerm, bool> termFilter)
    {
        return Create(
            inputType,
            _unilinearCombination.GetSubCombination(termFilter)
        ).SetOutputType(outputType);
    }


    public SortedDictionary<int, XGaFloat64UnilinearCombination> GetIdTermListPairs()
    {
        var idList = new SortedDictionary<int, XGaFloat64UnilinearCombination>();

        var groupList =
            _unilinearCombination
                .GroupBy(term => term.OutputBasisBladeId);

        foreach (var group in groupList)
        {
            var key = (int)group.Key;
            var value =
                new XGaFloat64UnilinearCombination().Add(group);

            idList.Add(key, value);
        }

        return idList;
    }
    
    public SortedDictionary<int, XGaFloat64UnilinearCombination> GetIdTermListPairs(Func<XGaFloat64UnilinearCombinationTerm, bool> termFilter)
    {
        var idList = new SortedDictionary<int, XGaFloat64UnilinearCombination>();

        var groupList =
            _unilinearCombination
                .Where(termFilter)
                .GroupBy(term => term.OutputBasisBladeId);

        foreach (var group in groupList)
        {
            var key = (int)group.Key;
            var value =
                new XGaFloat64UnilinearCombination().Add(group);

            idList.Add(key, value);
        }

        return idList;
    }

    public SortedDictionary<int, XGaFloat64UnilinearCombination> GetIdTermListPairsForOutGrade(int grade)
    {
        var idList = new SortedDictionary<int, XGaFloat64UnilinearCombination>();

        var groupList =
            _unilinearCombination
                .Where(term => term.OutputBasisBladeGrade == grade)
                .GroupBy(term => term.OutputBasisBladeId);

        foreach (var group in groupList)
        {
            var key = (int)group.Key;
            var value =
                new XGaFloat64UnilinearCombination().Add(group);

            idList.Add(key, value);
        }

        return idList;
    }
}