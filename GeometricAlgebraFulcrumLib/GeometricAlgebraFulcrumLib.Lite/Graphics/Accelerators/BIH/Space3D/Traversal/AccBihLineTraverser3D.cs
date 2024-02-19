﻿using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Computers;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Accelerators.BIH.Space3D.Traversal;

public sealed class AccBihLineTraverser3D<T>
    where T : IFiniteGeometricShape3D
{
    public static AccBihLineTraverser3D<T> Create(IAccBih3D<T> bih, ILine3D line)
    {
        return new AccBihLineTraverser3D<T>(
            bih,
            line,
            Float64ScalarRange.Infinite
        );
    }

    public static AccBihLineTraverser3D<T> Create(IAccBih3D<T> bih, ILine3D line, Float64ScalarRange lineParamLimits)
    {
        return new AccBihLineTraverser3D<T>(
            bih,
            line,
            lineParamLimits
        );
    }

    public static AccBihLineTraverser3D<T> Create(IAccBih3D<T> bih, ILine3D line, double lineParamLimit1, double lineParamLimit2)
    {
        return new AccBihLineTraverser3D<T>(
            bih,
            line,
            Float64ScalarRange.Create(lineParamLimit1, lineParamLimit2)
        );
    }


    private readonly List<AccBihLineTraversalState3D> _statesList =
        new List<AccBihLineTraversalState3D>();


    public IAccBih3D<T> Bih { get; }

    public ILine3D Line { get; }

    public LineTraversalData3D LineData { get; }

    public Float64ScalarRange LineParameterRange { get; private set; }

    public IEnumerable<AccBihLineTraversalState3D> TraversalStates => _statesList;


    private AccBihLineTraverser3D(IAccBih3D<T> bih, ILine3D line, Float64ScalarRange lineParamLimits)
    {
        Bih = bih;
        Line = line;
        LineData = line.GetLineTraversalData();
        LineParameterRange = lineParamLimits;
    }

        
    internal AccBihLineTraverser3D<T> ResetMinParameterValue(double minValue)
    {
        LineParameterRange = LineParameterRange.ResetMinValue(minValue);

        return this;
    }
        
    internal AccBihLineTraverser3D<T> ResetMaxParameterValue(double maxValue)
    {
        LineParameterRange = LineParameterRange.ResetMaxValue(maxValue);

        return this;
    }

    public IEnumerable<AccBihLineTraversalState3D> GetLeafTraversalStates(bool storeStates = false)
    {
        return GetTraversalStates(storeStates)
            .Where(state => state.BihNode.IsLeaf);
    }

    public IEnumerable<AccBihLineTraversalState3D> GetTraversalStates(bool storeStates = false)
    {
        if (storeStates)
            _statesList.Clear();

        var stack = new Stack<AccBihLineTraversalState3D>();
        stack.Push(
            new AccBihLineTraversalState3D(Bih.RootNode, LineParameterRange)
        );

        while (stack.Count > 0)
        {
            var state = stack.Pop();

            //The outside caller can change LineParameterLimits used inside
            //this loop. Make sure to use LineParameterLimits values to make
            //correct BIH traversal decisions
            if (!state.RestrictLineParameterRange(LineParameterRange))
                continue;

            if (storeStates)
                _statesList.Add(state);

            yield return state;

            if (state.BihNode.HasNoChildren)
                continue;

            var childStatesList = state.GetChildStates(LineData);
            foreach (var childState in childStatesList)
                stack.Push(childState);
        }
    }
}