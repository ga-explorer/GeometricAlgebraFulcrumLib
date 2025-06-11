using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Combinations;

public class XGaFloat64TrilinearCombination : 
    IReadOnlyCollection<XGaFloat64TrilinearCombinationTerm>, 
    IXGaLinearCombination
{
    private readonly Dictionary<Quad<IndexSet>, XGaFloat64TrilinearCombinationTerm> _termList;


    public XGaFloat64TrilinearCombinationTerm.InputsKind InputsKind { get; }

    public int Count
        => _termList.Count;

    public bool IsEmpty
        => _termList.Count == 0;


    public XGaFloat64TrilinearCombination(XGaFloat64TrilinearCombinationTerm.InputsKind inputsKind)
    {
        InputsKind = inputsKind;
        _termList = new Dictionary<Quad<IndexSet>, XGaFloat64TrilinearCombinationTerm>();
    }

    public XGaFloat64TrilinearCombination(XGaFloat64TrilinearCombinationTerm.InputsKind inputsKind, Dictionary<Quad<IndexSet>, XGaFloat64TrilinearCombinationTerm> termList)
    {
        InputsKind = inputsKind;
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

    public XGaFloat64TrilinearCombination Set(XGaFloat64TrilinearCombinationTerm term)
    {
        var key = 
            term.GetUniqueKey(InputsKind);

        if (term.IsInputScalarZero)
            _termList.Remove(key);

        else if (_termList.TryGetValue(key, out var oldTerm))
            oldTerm.InputScalar = term.InputScalar;

        else
            _termList.Add(key, term);

        return this;
    }

    public XGaFloat64TrilinearCombination Add(XGaFloat64TrilinearCombinationTerm term)
    {
        if (term.IsInputScalarZero)
            return this;

        var key = 
            term.GetUniqueKey(InputsKind);

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

    public XGaFloat64TrilinearCombination Add(IEnumerable<XGaFloat64TrilinearCombinationTerm> termList)
    {
        foreach (var term in termList)
            Add(term);

        return this;
    }

    public XGaFloat64TrilinearCombination Add(double inputScalar, XGaMetric metric, IndexSet input1BasisBladeId, IndexSet input2BasisBladeId, IndexSet input3BasisBladeId, IndexSet outputBasisBladeId)
    {
        var term = XGaFloat64TrilinearCombinationTerm.Create(
            inputScalar,
            metric,
            input1BasisBladeId,
            input2BasisBladeId,
            input3BasisBladeId,
            outputBasisBladeId
        );

        return Add(term);
    }

    public XGaFloat64TrilinearCombination Add(XGaBasisBlade input1BasisBlade, XGaBasisBlade input2BasisBlade, XGaBasisBlade input3BasisBlade, IXGaSignedBasisBlade outputBasisBlade)
    {
        var term = XGaFloat64TrilinearCombinationTerm.Create(
            input1BasisBlade,
            input2BasisBlade,
            input3BasisBlade,
            outputBasisBlade
        );

        return Add(term);
    }

    public XGaFloat64TrilinearCombination Add(double inputScalar, XGaBasisBlade input1BasisBlade, XGaBasisBlade input2BasisBlade, XGaBasisBlade input3BasisBlade, XGaBasisBlade outputBasisBlade)
    {
        var term = XGaFloat64TrilinearCombinationTerm.Create(
            inputScalar,
            input1BasisBlade,
            input2BasisBlade,
            input3BasisBlade,
            outputBasisBlade
        );

        return Add(term);
    }

    public XGaFloat64TrilinearCombination Add(XGaBasisBlade input1BasisBlade, XGaBasisBlade input2BasisBlade, XGaBasisBlade input3BasisBlade, XGaFloat64Multivector outputMultivector)
    {
        foreach (var (id, scalar) in outputMultivector.ToTuples())
        {
            var term = XGaFloat64TrilinearCombinationTerm.Create(
                scalar,
                input1BasisBlade.Metric,
                input1BasisBlade.Id,
                input2BasisBlade.Id,
                input3BasisBlade.Id,
                id
            );

            Add(term);
        }

        return this;
    }

    public XGaFloat64TrilinearCombination Add(IEnumerable<XGaBasisBlade> input1BasisBladeList, IReadOnlyCollection<XGaBasisBlade> input2BasisBladeList, IReadOnlyCollection<XGaBasisBlade> input3BasisBladeList, Func<XGaBasisBlade, XGaBasisBlade, XGaBasisBlade, IXGaSignedBasisBlade> basisMapFunc)
    {
        foreach (var input1BasisBlade in input1BasisBladeList)
        foreach (var input2BasisBlade in input2BasisBladeList)
        foreach (var input3BasisBlade in input3BasisBladeList)
        {
            var outputBasisBlade = basisMapFunc(
                input1BasisBlade, 
                input2BasisBlade, 
                input3BasisBlade
            );

            Add(
                input1BasisBlade,
                input2BasisBlade,
                input3BasisBlade,
                outputBasisBlade
            );
        }

        return this;
    }

    public XGaFloat64TrilinearCombination Add(IEnumerable<XGaBasisBlade> input1BasisBladeList, IReadOnlyCollection<XGaBasisBlade> input2BasisBladeList, IReadOnlyCollection<XGaBasisBlade> input3BasisBladeList, Func<XGaBasisBlade, XGaBasisBlade, XGaBasisBlade, XGaFloat64Multivector> basisMapFunc)
    {
        foreach (var input1BasisBlade in input1BasisBladeList)
        foreach (var input2BasisBlade in input2BasisBladeList)
        foreach (var input3BasisBlade in input3BasisBladeList)
        {
            var outputMultivector = basisMapFunc(
                input1BasisBlade, 
                input2BasisBlade, 
                input3BasisBlade
            );

            Add(
                input1BasisBlade,
                input2BasisBlade,
                input3BasisBlade,
                outputMultivector
            );
        }

        return this;
    }

    public SortedSet<IndexSet> GetInputBasisBladeIDs()
    {
        return _termList
            .Values
            .Select(term => term.Input1BasisBladeId)
            .Concat(_termList.Values.Select(term => term.Input2BasisBladeId))
            .Concat(_termList.Values.Select(term => term.Input3BasisBladeId))
            .ToSortedSet();
    }
    
    public SortedSet<IndexSet> GetInput12BasisBladeIDs()
    {
        return InputsKind switch
        {
            XGaFloat64TrilinearCombinationTerm.InputsKind.Equal => 
                GetInputBasisBladeIDs(),

            XGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstThird => 
                GetInputBasisBladeIDs(),

            XGaFloat64TrilinearCombinationTerm.InputsKind.EqualSecondThird => 
                GetInputBasisBladeIDs(),

            _ => _termList
                .Values
                .Select(term => term.Input1BasisBladeId)
                .Concat(_termList.Values.Select(term => term.Input2BasisBladeId))
                .ToSortedSet()
        };
    }
    
    public SortedSet<IndexSet> GetInput13BasisBladeIDs()
    {
        return InputsKind switch
        {
            XGaFloat64TrilinearCombinationTerm.InputsKind.Equal => 
                GetInputBasisBladeIDs(),

            XGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstSecond => 
                GetInputBasisBladeIDs(),

            XGaFloat64TrilinearCombinationTerm.InputsKind.EqualSecondThird => 
                GetInputBasisBladeIDs(),

            _ => _termList
                .Values
                .Select(term => term.Input1BasisBladeId)
                .Concat(_termList.Values.Select(term => term.Input3BasisBladeId))
                .ToSortedSet()
        };
    }
    
    public SortedSet<IndexSet> GetInput23BasisBladeIDs()
    {
        return InputsKind switch
        {
            XGaFloat64TrilinearCombinationTerm.InputsKind.Equal => 
                GetInputBasisBladeIDs(),

            XGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstSecond => 
                GetInputBasisBladeIDs(),

            XGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstThird => 
                GetInputBasisBladeIDs(),

            _ => _termList
                .Values
                .Select(term => term.Input2BasisBladeId)
                .Concat(_termList.Values.Select(term => term.Input3BasisBladeId))
                .ToSortedSet()
        };
    }

    public SortedSet<IndexSet> GetInput1BasisBladeIDs()
    {
        return InputsKind switch
        {
            XGaFloat64TrilinearCombinationTerm.InputsKind.Equal => 
                GetInputBasisBladeIDs(),

            XGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstSecond => 
                GetInput12BasisBladeIDs(),

            XGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstThird => 
                GetInput13BasisBladeIDs(),
                
            _ => _termList
                .Values
                .Select(term => term.Input1BasisBladeId)
                .ToSortedSet()
        };
    }
    
    public SortedSet<IndexSet> GetInput2BasisBladeIDs()
    {
        return InputsKind switch
        {
            XGaFloat64TrilinearCombinationTerm.InputsKind.Equal => 
                GetInputBasisBladeIDs(),

            XGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstSecond => 
                GetInput12BasisBladeIDs(),
                
            XGaFloat64TrilinearCombinationTerm.InputsKind.EqualSecondThird => 
                GetInput23BasisBladeIDs(),

            _ => _termList
                .Values
                .Select(term => term.Input2BasisBladeId)
                .ToSortedSet()
        };
    }
    
    public SortedSet<IndexSet> GetInput3BasisBladeIDs()
    {
        return InputsKind switch
        {
            XGaFloat64TrilinearCombinationTerm.InputsKind.Equal => 
                GetInputBasisBladeIDs(),

            XGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstThird => 
                GetInput13BasisBladeIDs(),
                
            XGaFloat64TrilinearCombinationTerm.InputsKind.EqualSecondThird => 
                GetInput23BasisBladeIDs(),

            _ => _termList
                .Values
                .Select(term => term.Input3BasisBladeId)
                .ToSortedSet()
        };
    }

    public SortedSet<IndexSet> GetOutputBasisBladeIDs()
    {
        return _termList
            .Values
            .Select(term => term.OutputBasisBladeId)
            .ToSortedSet();
    }
    
    public IndexSet GetInputBasisBladeGrades()
    {
        return _termList
            .Values
            .Select(term => term.Input1BasisBladeGrade)
            .Concat(_termList.Values.Select(term => term.Input2BasisBladeGrade))
            .Concat(_termList.Values.Select(term => term.Input3BasisBladeGrade))
            .ToIndexSet(false);
    }
    
    public IndexSet GetInput12BasisBladeGrades()
    {
        return InputsKind switch
        {
            XGaFloat64TrilinearCombinationTerm.InputsKind.Equal => 
                GetInputBasisBladeGrades(),

            XGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstThird => 
                GetInputBasisBladeGrades(),

            XGaFloat64TrilinearCombinationTerm.InputsKind.EqualSecondThird => 
                GetInputBasisBladeGrades(),

            _ => _termList
                .Values
                .Select(term => term.Input1BasisBladeGrade)
                .Concat(_termList.Values.Select(term => term.Input2BasisBladeGrade))
                .ToIndexSet(false)
        };
    }
    
    public IndexSet GetInput13BasisBladeGrades()
    {
        return InputsKind switch
        {
            XGaFloat64TrilinearCombinationTerm.InputsKind.Equal => 
                GetInputBasisBladeGrades(),

            XGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstSecond => 
                GetInputBasisBladeGrades(),

            XGaFloat64TrilinearCombinationTerm.InputsKind.EqualSecondThird => 
                GetInputBasisBladeGrades(),

            _ => _termList
                .Values
                .Select(term => term.Input1BasisBladeGrade)
                .Concat(_termList.Values.Select(term => term.Input3BasisBladeGrade))
                .ToIndexSet(false)
        };
    }
    
    public IndexSet GetInput23BasisBladeGrades()
    {
        return InputsKind switch
        {
            XGaFloat64TrilinearCombinationTerm.InputsKind.Equal => 
                GetInputBasisBladeGrades(),

            XGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstSecond => 
                GetInputBasisBladeGrades(),

            XGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstThird => 
                GetInputBasisBladeGrades(),

            _ => _termList
                .Values
                .Select(term => term.Input2BasisBladeGrade)
                .Concat(_termList.Values.Select(term => term.Input3BasisBladeGrade))
                .ToIndexSet(false)
        };
    }

    public IndexSet GetInput1BasisBladeGrades()
    {
        return InputsKind switch
        {
            XGaFloat64TrilinearCombinationTerm.InputsKind.Equal => 
                GetInputBasisBladeGrades(),

            XGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstSecond => 
                GetInput12BasisBladeGrades(),

            XGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstThird => 
                GetInput13BasisBladeGrades(),
                
            _ => _termList
                .Values
                .Select(term => term.Input1BasisBladeGrade)
                .ToIndexSet(false)
        };
    }
    
    public IndexSet GetInput2BasisBladeGrades()
    {
        return InputsKind switch
        {
            XGaFloat64TrilinearCombinationTerm.InputsKind.Equal => 
                GetInputBasisBladeGrades(),

            XGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstSecond => 
                GetInput12BasisBladeGrades(),
                
            XGaFloat64TrilinearCombinationTerm.InputsKind.EqualSecondThird => 
                GetInput23BasisBladeGrades(),

            _ => _termList
                .Values
                .Select(term => term.Input2BasisBladeGrade)
                .ToIndexSet(false)
        };
    }
    
    public IndexSet GetInput3BasisBladeGrades()
    {
        return InputsKind switch
        {
            XGaFloat64TrilinearCombinationTerm.InputsKind.Equal => 
                GetInputBasisBladeGrades(),

            XGaFloat64TrilinearCombinationTerm.InputsKind.EqualFirstThird => 
                GetInput13BasisBladeGrades(),
                
            XGaFloat64TrilinearCombinationTerm.InputsKind.EqualSecondThird => 
                GetInput23BasisBladeGrades(),

            _ => _termList
                .Values
                .Select(term => term.Input3BasisBladeGrade)
                .ToIndexSet(false)
        };
    }

    public IndexSet GetOutputBasisBladeGrades()
    {
        return _termList
            .Values
            .Select(term => term.OutputBasisBladeGrade)
            .ToIndexSet(false);
    }

    public XGaFloat64TrilinearCombination GetSubCombinationWithOutputId(IndexSet outputBasisBladeId)
    {
        var termList =
            _termList
                .Where(p => p.Value.OutputBasisBladeId.Equals(outputBasisBladeId))
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return new XGaFloat64TrilinearCombination(InputsKind, termList);
    }

    public XGaFloat64TrilinearCombination GetSubCombination(Func<XGaFloat64TrilinearCombinationTerm, bool> termFilter)
    {
        var termList =
            _termList
                .Where(p => termFilter(p.Value))
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return new XGaFloat64TrilinearCombination(InputsKind, termList);
    }

    public IEnumerator<XGaFloat64TrilinearCombinationTerm> GetEnumerator()
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