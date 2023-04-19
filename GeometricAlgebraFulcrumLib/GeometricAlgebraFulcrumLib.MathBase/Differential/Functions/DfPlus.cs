using System.Collections.Immutable;
using System.Runtime.CompilerServices;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.MathBase.Differential.Functions
{
    public sealed class DfPlus :
        DifferentialNaryFunction
    {
        private static void FlattenArgumentTree(IEnumerable<DifferentialFunction> arguments, List<DifferentialFunction> argList)
        {
            foreach (var fArg in arguments)
            {
                if (fArg.IsConstantZero)
                    continue;

                if (fArg is DfPlus fArgPlus)
                {
                    FlattenArgumentTree(fArgPlus.Arguments, argList);
                    continue;
                }

                argList.Add(fArg);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfPlus Create(IEnumerable<DifferentialFunction> baseFunctions, bool canBeSimplified = true)
        {
            var argList = new List<DifferentialFunction>();

            FlattenArgumentTree(baseFunctions, argList);

            var scalarTerm = 0d;
            var plusArgumentList = new List<DifferentialFunction>(argList.Count);
            var arguments = argList
                .OrderBy(a => a.TreeDepth)
                .ThenBy(a => a.ToString());

            foreach (var arg in arguments)
            {
                if (arg is DfConstant argConstant)
                {
                    scalarTerm += argConstant.Value;
                    continue;
                }

                plusArgumentList.Add(arg);
            }

            if (scalarTerm != 0d)
                plusArgumentList.Insert(0, scalarTerm);

            return new DfPlus(plusArgumentList, canBeSimplified);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfPlus Create(DifferentialFunction baseFunction1, DifferentialFunction baseFunction2, bool canBeSimplified = true)
        {
            return Create(
                new []{ baseFunction1, baseFunction2 }, 
                canBeSimplified
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfPlus Create(DifferentialFunction baseFunction1, DifferentialFunction baseFunction2, DifferentialFunction baseFunction3, bool canBeSimplified = true)
        {
            return Create(
                new []{ baseFunction1, baseFunction2, baseFunction3 }, 
                canBeSimplified
            );
        }

    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private DfPlus(IReadOnlyList<DifferentialFunction> baseFunctions, bool canBeSimplified) 
            : base(baseFunctions, canBeSimplified)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetValue(double t)
        {
            return Arguments.Sum(
                f => f.GetValue(t)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Tuple<bool, DifferentialFunction> TrySimplify()
        {
            if (!CanBeSimplified) 
                return new Tuple<bool, DifferentialFunction>(false, this);

            return new Tuple<bool, DifferentialFunction>(true, Simplify());
        }

        public override DifferentialFunction Simplify()
        {
            if (!CanBeSimplified) return this;

            var scalarTerm = 0d;
            var termGroupList = new List<Tuple<double, DifferentialFunction>>();
            var termList = new List<DifferentialFunction>();

            var fArguments = 
                Arguments.Select(f => f.Simplify());

            FlattenArgumentTree(fArguments, termList);

            if (termList.Count == 0)
                return DfConstant.Zero;

            if (termList.Count == 1)
                return termList[0];

            // Group similar terms
            foreach (var f in termList)
            {
                if (f is DfConstant fConstant)
                {
                    scalarTerm += fConstant.Value;
                    continue;
                }

                var fScalar = 1d;
                var fTerm = f;

                if (f is DfTimes fTimes)
                {
                    var (fScalar1, fTerm1) = fTimes.SeparateConstant();

                    fScalar = fScalar1;
                    fTerm = fTerm1;
                }

                var termGroupFound = false;
                for (var i = 0; i < termGroupList.Count; i++)
                {
                    var (gScalar, gTerm) = termGroupList[i];

                    if (!gTerm.IsSame(fTerm)) continue;

                    termGroupList[i] = new Tuple<double, DifferentialFunction>(
                        gScalar + fScalar,
                        gTerm
                    );

                    termGroupFound = true;
                    break;
                }

                if (!termGroupFound)
                    termGroupList.Add(
                        new Tuple<double, DifferentialFunction>(fScalar, fTerm)
                    );
            }

            termList.Clear();

            if (scalarTerm != 0d)
                termList.Add(scalarTerm);

            var termList1 =
                termGroupList
                    .OrderBy(t => t.Item2.TreeDepth)
                    .ThenBy(t => t.Item2.ToString());

            foreach (var (gScalar, gTerm) in termList1)
            {
                if (gScalar == 1d)
                {
                    termList.Add(gTerm);
                    continue;
                }
            
                if (gTerm is not DfTimes gTermTimes)
                {
                    termList.Add(
                        DfTimes.Create(gScalar, gTerm, false)
                    );
                    continue;
                }

                termList.Add(
                    gTermTimes.ScaleBy(gScalar)
                );
            }

            if (termList.Count == 0) return DfConstant.Zero;
            if (termList.Count == 1) return termList[0];

            if (termList.All(arg => arg is DfTimes or DfConstant))
            {
                var scalarFunctionList = 
                    termList.SeparateConstants().ToImmutableArray();

                var scalingFactor = scalarFunctionList[0].Item1;

                if (scalingFactor != 1d && scalarFunctionList.All(t => t.Item1 == scalingFactor))
                {
                    var fPlus = Create(
                        scalarFunctionList.Select(s => s.Item2)
                    ).Simplify();

                    return DfTimes.Create(
                        scalingFactor,
                        fPlus
                    ).Simplify();
                }
            }

            return new DfPlus(termList, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction GetDerivative1()
        {
            var baseFunctions = 
                Arguments.Select(f => f.GetDerivative1()).ToArray();

            return new DfPlus(baseFunctions, true).Simplify();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction MapArguments(Func<DifferentialFunction, DifferentialFunction> functionMapping)
        {
            var baseFunctions = 
                Arguments.Select(functionMapping).ToArray();

            return new DfPlus(baseFunctions, true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction MapArguments(Func<int, DifferentialFunction, DifferentialFunction> functionMapping)
        {
            var baseFunctions = 
                Arguments.Select((f, i) => functionMapping(i, f)).ToArray();

            return new DfPlus(baseFunctions, true);
        }
        
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public override string ToString()
        //{
        //    return $"Plus[{Arguments.Concatenate(", ")}]";
        //}
    }
}