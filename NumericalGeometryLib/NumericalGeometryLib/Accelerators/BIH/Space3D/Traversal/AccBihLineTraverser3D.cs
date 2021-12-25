using System.Collections.Generic;
using System.Linq;
using NumericalGeometryLib.BasicShapes;
using NumericalGeometryLib.BasicShapes.Lines;
using NumericalGeometryLib.Borders.Space1D;
using NumericalGeometryLib.Borders.Space1D.Mutable;
using NumericalGeometryLib.Computers;

namespace NumericalGeometryLib.Accelerators.BIH.Space3D.Traversal
{
    public sealed class AccBihLineTraverser3D<T>
        where T : IFiniteGeometricShape3D
    {
        public static AccBihLineTraverser3D<T> Create(IAccBih3D<T> bih, ILine3D line)
        {
            return new AccBihLineTraverser3D<T>(
                bih,
                line,
                MutableBoundingBox1D.CreateInfinite()
            );
        }

        public static AccBihLineTraverser3D<T> Create(IAccBih3D<T> bih, ILine3D line, IBoundingBox1D lineParamLimits)
        {
            return new AccBihLineTraverser3D<T>(
                bih,
                line,
                lineParamLimits.GetMutableBoundingBox()
            );
        }

        public static AccBihLineTraverser3D<T> Create(IAccBih3D<T> bih, ILine3D line, double lineParamLimit1, double lineParamLimit2)
        {
            return new AccBihLineTraverser3D<T>(
                bih,
                line,
                MutableBoundingBox1D.Create(lineParamLimit1, lineParamLimit2)
            );
        }


        private readonly List<AccBihLineTraversalState3D> _statesList =
            new List<AccBihLineTraversalState3D>();


        public IAccBih3D<T> Bih { get; }

        public ILine3D Line { get; }

        public LineTraversalData3D LineData { get; }

        public MutableBoundingBox1D LineParameterLimits { get; }

        public IEnumerable<AccBihLineTraversalState3D> TraversalStates
        {
            get { return _statesList; }
        }


        private AccBihLineTraverser3D(IAccBih3D<T> bih, ILine3D line, MutableBoundingBox1D lineParamLimits)
        {
            Bih = bih;
            Line = line;
            LineData = line.GetLineTraversalData();
            LineParameterLimits = lineParamLimits;
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
                new AccBihLineTraversalState3D(Bih.RootNode, LineParameterLimits)
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