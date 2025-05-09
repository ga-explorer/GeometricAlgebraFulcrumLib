using System.Collections;
using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Combinations;

public class RGaFloat64UnilinearCombination : 
    IReadOnlyCollection<RGaFloat64UnilinearCombinationTerm>,
    IRGaLinearCombination
{
    private readonly Dictionary<Pair<ulong>, RGaFloat64UnilinearCombinationTerm> _termList;


    public int Count
        => _termList.Count;

    public bool IsEmpty
        => _termList.Count == 0;


    public RGaFloat64UnilinearCombination()
    {
        _termList = new Dictionary<Pair<ulong>, RGaFloat64UnilinearCombinationTerm>();
    }

    public RGaFloat64UnilinearCombination(Dictionary<Pair<ulong>, RGaFloat64UnilinearCombinationTerm> termList)
    {
        _termList = termList;
    }


    public bool IsOutputScalar()
    {
        return !IsEmpty && 
               _termList.Values.All(
                   term => term.OutputBasisBladeGrade == 0
               );
    }
    
    public bool IsOutputVector()
    {
        return !IsEmpty && 
               _termList.Values.All(
                   term => term.OutputBasisBladeGrade == 1
               );
    }
    
    public bool IsOutputBivector()
    {
        return !IsEmpty && 
               _termList.Values.All(
                   term => term.OutputBasisBladeGrade == 2
               );
    }

    public bool IsOutputKVector()
    {
        if (IsEmpty) return false;

        var grade = 
            _termList.Values.First().OutputBasisBladeGrade;

        return _termList.Values.All(
            term => term.OutputBasisBladeGrade == grade
        );
    }

    public bool IsOutputKVector(int grade)
    {
        return !IsEmpty && 
               _termList.Values.All(
                   term => term.OutputBasisBladeGrade == grade
               );
    }


    public RGaFloat64UnilinearCombination Set(RGaFloat64UnilinearCombinationTerm term)
    {
        var key = term.GetUniqueKey();

        if (term.IsInputScalarZero)
            _termList.Remove(key);

        else if (_termList.TryGetValue(key, out var oldTerm))
            oldTerm.InputScalar = term.InputScalar;

        else
            _termList.Add(key, term);

        return this;
    }

    public RGaFloat64UnilinearCombination Add(RGaFloat64UnilinearCombinationTerm term)
    {
        if (term.IsInputScalarZero)
            return this;

        var key = term.GetUniqueKey();

        if (_termList.TryGetValue(key, out var oldTerm))
        {
            oldTerm.InputScalar += term.InputScalar;

            if (oldTerm.IsInputScalarZero)
                _termList.Remove(key);
        }
        else
        {
            _termList.Add(key, term);
        }

        return this;
    }

    public RGaFloat64UnilinearCombination Add(IEnumerable<RGaFloat64UnilinearCombinationTerm> termList)
    {
        foreach (var term in termList)
            Add(term);

        return this;
    }

    public RGaFloat64UnilinearCombination Add(Float64Scalar inputScalar, RGaMetric metric, ulong inputBasisBladeId, ulong outputBasisBladeId)
    {
        var term = RGaFloat64UnilinearCombinationTerm.Create(
            inputScalar,
            metric,
            inputBasisBladeId,
            outputBasisBladeId
        );

        return Add(term);
    }

    public RGaFloat64UnilinearCombination Add(RGaBasisBlade inputBasisBlade, IRGaSignedBasisBlade outputBasisBlade)
    {
        var term = RGaFloat64UnilinearCombinationTerm.Create(
            inputBasisBlade,
            outputBasisBlade
        );

        return Add(term);
    }

    public RGaFloat64UnilinearCombination Add(Float64Scalar inputScalar, RGaBasisBlade inputBasisBlade, RGaBasisBlade outputBasisBlade)
    {
        var term = RGaFloat64UnilinearCombinationTerm.Create(
            inputScalar,
            inputBasisBlade,
            outputBasisBlade
        );

        return Add(term);
    }
    
    public RGaFloat64UnilinearCombination Add(ulong inputBasisBladeId, RGaFloat64Multivector outputMultivector)
    {
        var metric = outputMultivector.Processor;

        foreach (var (id, scalar) in outputMultivector)
        {
            var term = RGaFloat64UnilinearCombinationTerm.Create(
                scalar,
                metric,
                inputBasisBladeId,
                id
            );

            Add(term);
        }

        return this;
    }

    public RGaFloat64UnilinearCombination Add(RGaBasisBlade inputBasisBlade, RGaFloat64Multivector outputMultivector)
    {
        foreach (var (id, scalar) in outputMultivector)
        {
            var term = RGaFloat64UnilinearCombinationTerm.Create(
                scalar,
                inputBasisBlade.Metric,
                inputBasisBlade.Id,
                id
            );

            Add(term);
        }

        return this;
    }

    public RGaFloat64UnilinearCombination Add(IEnumerable<RGaBasisBlade> inputBasisBladeList, Func<RGaBasisBlade, IRGaSignedBasisBlade> basisMapFunc)
    {
        foreach (var inputBasisBlade in inputBasisBladeList)
        {
            Add(
                inputBasisBlade,
                basisMapFunc(inputBasisBlade)
            );
        }

        return this;
    }

    public RGaFloat64UnilinearCombination Add(IEnumerable<RGaBasisBlade> inputBasisBladeList, Func<RGaBasisBlade, RGaFloat64Multivector> basisMapFunc)
    {
        foreach (var inputBasisBlade in inputBasisBladeList)
        {
            Add(
                inputBasisBlade,
                basisMapFunc(inputBasisBlade)
            );
        }

        return this;
    }
    
    public RGaFloat64UnilinearCombination AddOutermorphism(RGaFloat64Processor metric, double[,] vectorMapArray)
    {
        var outerMorphism =
            vectorMapArray
                .ColumnsToLinVectors()
                .ToLinUnilinearMap()
                .ToOutermorphism(metric);

        var vSpaceDimensions = 
            vectorMapArray.GetLength(0);

        var idMultivectorPairs =
            outerMorphism.GetMappedBasisBlades(vSpaceDimensions);

        foreach (var (id, mv) in idMultivectorPairs)
            Add(id, mv);

        return this;
    }


    public IReadOnlyList<ulong> GetInputBasisBladeIDs()
    {
        return _termList
            .Values
            .Select(term => term.InputBasisBladeId)
            .ToImmutableSortedSet();
    }

    public IReadOnlyList<ulong> GetOutputBasisBladeIDs()
    {
        return _termList
            .Values
            .Select(term => term.OutputBasisBladeId)
            .ToImmutableSortedSet();
    }

    public IReadOnlyList<int> GetInputBasisBladeGrades()
    {
        return _termList
            .Values
            .Select(term => term.InputBasisBladeGrade)
            .ToImmutableSortedSet();
    }

    public IReadOnlyList<int> GetOutputBasisBladeGrades()
    {
        return _termList
            .Values
            .Select(term => term.OutputBasisBladeGrade)
            .ToImmutableSortedSet();
    }

    public RGaFloat64UnilinearCombination GetSubCombinationWithOutputId(ulong outputBasisBladeId)
    {
        var termList =
            _termList
                .Where(p => p.Value.OutputBasisBladeId == outputBasisBladeId)
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return new RGaFloat64UnilinearCombination(termList);
    }
    
    public RGaFloat64UnilinearCombination GetSubCombinationWithOutputGrade(int grade)
    {
        var termList =
            _termList
                .Where(p => p.Value.OutputBasisBladeGrade == grade)
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return new RGaFloat64UnilinearCombination(termList);
    }

    public RGaFloat64UnilinearCombination GetSubCombination(Func<RGaFloat64UnilinearCombinationTerm, bool> termFilter)
    {
        var termList =
            _termList
                .Where(p => termFilter(p.Value))
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return new RGaFloat64UnilinearCombination(termList);
    }

    public IEnumerator<RGaFloat64UnilinearCombinationTerm> GetEnumerator()
    {
        return _termList
            .Values
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}