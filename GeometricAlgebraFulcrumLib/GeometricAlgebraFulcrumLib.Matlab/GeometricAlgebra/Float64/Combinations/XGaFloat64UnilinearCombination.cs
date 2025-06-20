﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Combinations;

public class XGaFloat64UnilinearCombination : 
    IReadOnlyCollection<XGaFloat64UnilinearCombinationTerm>,
    IXGaLinearCombination
{
    private readonly Dictionary<Pair<IndexSet>, XGaFloat64UnilinearCombinationTerm> _termList;


    public int Count
        => _termList.Count;

    public bool IsEmpty
        => _termList.Count == 0;


    public XGaFloat64UnilinearCombination()
    {
        _termList = new Dictionary<Pair<IndexSet>, XGaFloat64UnilinearCombinationTerm>();
    }

    public XGaFloat64UnilinearCombination(Dictionary<Pair<IndexSet>, XGaFloat64UnilinearCombinationTerm> termList)
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

    public XGaFloat64UnilinearCombination Set(XGaFloat64UnilinearCombinationTerm term)
    {
        var key = new Pair<IndexSet>(
            term.InputBasisBladeId,
            term.OutputBasisBladeId
        );

        if (term.IsInputScalarZero)
            _termList.Remove(key);

        else if (_termList.TryGetValue(key, out var oldTerm))
            oldTerm.InputScalar = term.InputScalar;

        else
            _termList.Add(key, term);

        return this;
    }

    public XGaFloat64UnilinearCombination Add(XGaFloat64UnilinearCombinationTerm term)
    {
        if (term.IsInputScalarZero)
            return this;

        var key = new Pair<IndexSet>(
            term.InputBasisBladeId,
            term.OutputBasisBladeId
        );

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

    public XGaFloat64UnilinearCombination Add(IEnumerable<XGaFloat64UnilinearCombinationTerm> termList)
    {
        foreach (var term in termList)
            Add(term);

        return this;
    }

    public XGaFloat64UnilinearCombination Add(double inputScalar, XGaMetric metric, IndexSet inputBasisBladeId, IndexSet outputBasisBladeId)
    {
        var term = XGaFloat64UnilinearCombinationTerm.Create(
            inputScalar,
            metric,
            inputBasisBladeId,
            outputBasisBladeId
        );

        return Add(term);
    }

    public XGaFloat64UnilinearCombination Add(XGaBasisBlade inputBasisBlade, IXGaSignedBasisBlade outputBasisBlade)
    {
        var term = XGaFloat64UnilinearCombinationTerm.Create(
            inputBasisBlade,
            outputBasisBlade
        );

        return Add(term);
    }

    public XGaFloat64UnilinearCombination Add(double inputScalar, XGaBasisBlade inputBasisBlade, XGaBasisBlade outputBasisBlade)
    {
        var term = XGaFloat64UnilinearCombinationTerm.Create(
            inputScalar,
            inputBasisBlade,
            outputBasisBlade
        );

        return Add(term);
    }
    
    public XGaFloat64UnilinearCombination Add(IndexSet inputBasisBladeId, XGaFloat64Multivector outputMultivector)
    {
        var metric = outputMultivector.Processor;

        foreach (var (id, scalar) in outputMultivector.ToTuples())
        {
            var term = XGaFloat64UnilinearCombinationTerm.Create(
                scalar,
                metric,
                inputBasisBladeId,
                id
            );

            Add(term);
        }

        return this;
    }

    public XGaFloat64UnilinearCombination Add(XGaBasisBlade inputBasisBlade, XGaFloat64Multivector outputMultivector)
    {
        foreach (var (id, scalar) in outputMultivector.ToTuples())
        {
            var term = XGaFloat64UnilinearCombinationTerm.Create(
                scalar,
                inputBasisBlade.Metric,
                inputBasisBlade.Id,
                id
            );

            Add(term);
        }

        return this;
    }

    public XGaFloat64UnilinearCombination Add(IEnumerable<XGaBasisBlade> inputBasisBladeList, Func<XGaBasisBlade, IXGaSignedBasisBlade> basisMapFunc)
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

    public XGaFloat64UnilinearCombination Add(IEnumerable<XGaBasisBlade> inputBasisBladeList, Func<XGaBasisBlade, XGaFloat64Multivector> basisMapFunc)
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
    
    public XGaFloat64UnilinearCombination AddOutermorphism(XGaFloat64Processor metric, double[,] vectorMapArray)
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

        foreach (var (id, mv) in idMultivectorPairs.ToTuples())
            Add(id, mv);

        return this;
    }


    public SortedSet<IndexSet> GetInputBasisBladeIDs()
    {
        return _termList
            .Values
            .Select(term => term.InputBasisBladeId)
            .ToSortedSet();
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
            .Select(term => term.InputBasisBladeGrade)
            .ToIndexSet(false);
    }

    public IndexSet GetOutputBasisBladeGrades()
    {
        return _termList
            .Values
            .Select(term => term.OutputBasisBladeGrade)
            .ToIndexSet(false);
    }

    public XGaFloat64UnilinearCombination GetSubCombinationWithOutputId(IndexSet outputBasisBladeId)
    {
        var termList =
            _termList
                .Where(p => p.Value.OutputBasisBladeId.Equals(outputBasisBladeId))
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return new XGaFloat64UnilinearCombination(termList);
    }

    public XGaFloat64UnilinearCombination GetSubCombination(Func<XGaFloat64UnilinearCombinationTerm, bool> termFilter)
    {
        var termList =
            _termList
                .Where(p => termFilter(p.Value))
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return new XGaFloat64UnilinearCombination(termList);
    }

    public IEnumerator<XGaFloat64UnilinearCombinationTerm> GetEnumerator()
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