using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.MathBase.BasicShapes;
using GeometricAlgebraFulcrumLib.MathBase.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.MathBase.Borders.Space1D;
using GeometricAlgebraFulcrumLib.MathBase.Borders.Space1D.Mutable;
using NumericalGeometryLib.Computers;

namespace NumericalGeometryLib.Accelerators.BIH.Space2D.Traversal
{
    public sealed class AccBihLineTraverser2D<T> 
        where T : IFiniteGeometricShape2D
    {
        public static AccBihLineTraverser2D<T> Create(IAccBih2D<T> bih, ILine2D line)
        {
            return new AccBihLineTraverser2D<T>(
                bih,
                line,
                MutableBoundingBox1D.CreateInfinite()
            );
        }

        public static AccBihLineTraverser2D<T> Create(IAccBih2D<T> bih, ILine2D line, IBoundingBox1D lineParamLimits)
        {
            return new AccBihLineTraverser2D<T>(
                bih,
                line,
                lineParamLimits.GetMutableBoundingBox()
            );
        }

        public static AccBihLineTraverser2D<T> Create(IAccBih2D<T> bih, ILine2D line, double lineParamLimit1, double lineParamLimit2)
        {
            return new AccBihLineTraverser2D<T>(
                bih,
                line,
                MutableBoundingBox1D.Create(lineParamLimit1, lineParamLimit2)
            );
        }


        private readonly List<AccBihLineTraversalState2D> _statesList =
            new List<AccBihLineTraversalState2D>();


        public IAccBih2D<T> Bih { get; }

        public ILine2D Line { get; }

        public LineTraversalData2D LineData { get; }

        public MutableBoundingBox1D LineParameterLimits { get; }

        public IEnumerable<AccBihLineTraversalState2D> TraversalStates
        {
            get { return _statesList; }
        }


        private AccBihLineTraverser2D(IAccBih2D<T> bih, ILine2D line, MutableBoundingBox1D lineParamLimits)
        {
            Bih = bih;
            Line = line;
            LineData = line.GetLineTraversalData();
            LineParameterLimits = lineParamLimits;
        }


        public IEnumerable<AccBihLineTraversalState2D> GetLeafTraversalStates(bool storeStates = false)
        {
            return GetTraversalStates(storeStates)
                .Where(state => state.BihNode.IsLeaf);
        }

        public IEnumerable<AccBihLineTraversalState2D> GetTraversalStates(bool storeStates = false)
        {
            if (storeStates)
                _statesList.Clear();

            var stack = new Stack<AccBihLineTraversalState2D>();
            stack.Push(
                new AccBihLineTraversalState2D(Bih.RootNode, LineParameterLimits)
            );

            while (stack.Count > 0)
            {
                var state = stack.Pop();

                //The outside caller can change LineParameterLimits used inside
                //this loop. Make sure to use LineParameterLimits values to make
                //correct BIH traversal decisions
                if (!state.RestrictLineParameterRange(LineParameterLimits))
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
}
