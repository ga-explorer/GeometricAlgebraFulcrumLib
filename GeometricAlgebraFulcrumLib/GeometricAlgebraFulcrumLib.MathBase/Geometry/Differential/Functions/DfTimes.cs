using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Extensions;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Differential.Functions
{
    public sealed class DfTimes :
        DifferentialNaryFunction
    {
        private static bool FlattenArgumentTree(IEnumerable<DifferentialFunction> arguments, List<DifferentialFunction> argList)
        {
            foreach (var fArg in arguments)
            {
                if (fArg.IsConstantZero)
                {
                    argList.Clear();
                    argList.Add(DfConstant.Zero);
                    return true;
                }

                if (fArg.IsConstantOne)
                    continue;

                if (fArg is DfTimes fArgTimes)
                {
                    var zeroFlag = FlattenArgumentTree(fArgTimes.Arguments, argList);
                    if (zeroFlag) return true;
                    continue;
                }

                argList.Add(fArg);
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfTimes Create(IEnumerable<DifferentialFunction> baseFunctions, bool canBeSimplified = true)
        {
            var argList = new List<DifferentialFunction>();

            var zeroFlag = FlattenArgumentTree(baseFunctions, argList);

            if (zeroFlag)
            {
                Debug.Assert(canBeSimplified);

                return new DfTimes(argList, true);
            }

            var scalarFactor = 1d;
            var timesArgumentList = new List<DifferentialFunction>(argList.Count);
            var arguments = 
                argList
                    .OrderBy(a => a.TreeDepth)
                    .ThenBy(a => a.ToString());

            foreach (var arg in arguments)
            {
                if (arg is DfConstant argConstant)
                {
                    scalarFactor *= argConstant.Value;
                    continue;
                }

                timesArgumentList.Add(arg);
            }

            if (scalarFactor != 1d)
                timesArgumentList.Insert(0, scalarFactor);

            return new DfTimes(timesArgumentList, canBeSimplified);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfTimes Create(DifferentialFunction baseFunction1, DifferentialFunction baseFunction2, bool canBeSimplified = true)
        {
            return Create(
                new[] { baseFunction1, baseFunction2 }, 
                canBeSimplified
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfTimes Create(DifferentialFunction baseFunction1, DifferentialFunction baseFunction2, DifferentialFunction baseFunction3, bool canBeSimplified = true)
        {
            return Create(
                new[] { baseFunction1, baseFunction2, baseFunction3 }, 
                canBeSimplified
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private DfTimes(IReadOnlyList<DifferentialFunction> baseFunctions, bool canBeSimplified) 
            : base(baseFunctions, canBeSimplified)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Tuple<double, DifferentialFunction> SeparateConstant()
        {
            if (Arguments.Count == 0) 
                return new Tuple<double, DifferentialFunction>(1d, DfConstant.One);

            var scalar = 1d;
            var argumentList = Arguments;

            if (Arguments[0] is DfConstant a1Constant)
            {
                scalar = a1Constant.Value;
                argumentList = Arguments.SubList(1);
            }

            return argumentList.Count switch
            {
                0 => new Tuple<double, DifferentialFunction>(
                    scalar, 
                    DfConstant.One
                ),

                1 => new Tuple<double, DifferentialFunction>(
                    scalar, 
                    argumentList[0]
                ),

                _ => new Tuple<double, DifferentialFunction>(
                    scalar, 
                    Create(argumentList, CanBeSimplified)
                )
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal DifferentialFunction ScaleBy(double scalingFactor)
        {
            var (scalar, term) = SeparateConstant();

            scalar *= scalingFactor;

            return scalar switch
            {
                0d => DfConstant.Zero,
                1d => term,
                _ => Create(scalar, term, CanBeSimplified)
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetValue(double t)
        {
            return Arguments.Aggregate(
                1d,
                (v, f) => v * f.GetValue(t)
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

            var scalarFactor = 1d;
            var factorGroupList = new List<Tuple<double, DifferentialFunction>>();
            var factorList = new List<DifferentialFunction>();

            var fArguments = 
                Arguments.Select(f => f.Simplify());

            var isZero = FlattenArgumentTree(fArguments, factorList);
            if (isZero) return DfConstant.Zero;
        
            if (factorList.Count == 0)
                return DfConstant.One;

            if (factorList.Count == 1)
                return factorList[0];
        
            // Group similar factors
            foreach (var f in factorList)
            {
                if (f is DfConstant fConstant)
                {
                    scalarFactor *= fConstant.Value;
                    continue;
                }

                var fScalar = 1d;
                var fFactor = f;

                if (f is DfPowerScalar fPowerScalar)
                {
                    fScalar = fPowerScalar.PowerValue;
                    fFactor = fPowerScalar.Argument;
                }

                var termGroupFound = false;
                for (var i = 0; i < factorGroupList.Count; i++)
                {
                    var (gScalar, gTerm) = factorGroupList[i];

                    if (!gTerm.IsSame(fFactor)) continue;

                    factorGroupList[i] = new Tuple<double, DifferentialFunction>(
                        gScalar + fScalar,
                        gTerm
                    );

                    termGroupFound = true;
                    break;
                }

                if (!termGroupFound)
                    factorGroupList.Add(
                        new Tuple<double, DifferentialFunction>(fScalar, fFactor)
                    );
            }

            factorList.Clear();

            if (scalarFactor != 1d)
                factorList.Add(scalarFactor);

            var termList =
                factorGroupList
                    .OrderBy(t => t.Item2.TreeDepth)
                    .ThenBy(t => t.Item2.ToString());

            foreach (var (gScalar, gTerm) in termList)
            {
                //if (gTerm is DfTimes)
                //    throw new NotImplementedException();

                if (gScalar == 1d)
                {
                    factorList.Add(gTerm);
                    continue;
                }
            
                if (gTerm is not DfPowerScalar gTermPowerScalar)
                {
                    factorList.Add(
                        DfPowerScalar.Create(gTerm, gScalar, false)
                    );
                    continue;
                }

                factorList.Add(
                    gTermPowerScalar.ScalePowerBy(gScalar)
                );
            }
        
            //var scalarFactor = 1d;
            //var cosSinPowerSinList = new List<DfPowerScalar>();
            //var cosSinList = new List<DifferentialFunction>();
            //var timesList = new List<DifferentialFunction>();

            //var fArguments = 
            //    Arguments.Select(f => f.Simplify());

            //// Flatten the Times tree as best as possible, and collect scaling factors 
            //foreach (var f in fArguments)
            //{
            //    if (f is DfConstant fConstant)
            //    {
            //        if (fConstant.IsZero) return DfConstant.Zero;

            //        if (!fConstant.IsOne)
            //            scalarFactor *= fConstant.Value;

            //        continue;
            //    }

            //    if (f is DfCos or DfSin)
            //    {
            //        cosSinList.Add(f);
            //        continue;
            //    }

            //    if (f is DfPowerScalar fPowerScalar)
            //    {
            //        if (fPowerScalar.PowerValue >= 2 && fPowerScalar.PowerValueIsInteger)
            //        {
            //            if (fPowerScalar.Argument is DfCos or DfSin)
            //            {
            //                cosSinPowerSinList.Add(fPowerScalar);
            //                continue;
            //            }
            //        }
            //    }

            //    if (f is DfTimes fTimes)
            //    {
            //        foreach (var g in fTimes.Arguments)
            //        {
            //            if (g is DfConstant gConstant)
            //            {
            //                if (!gConstant.IsOne)
            //                    scalarFactor *= gConstant.Value;

            //                continue;
            //            }

            //            timesList.Add(g);
            //        }

            //        continue;
            //    }

            //    timesList.Add(f);
            //}

            return factorList.Count switch
            {
                0 => DfConstant.One,
                1 => factorList[0],
                _ => new DfTimes(factorList, false)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction GetDerivative1()
        {
            var argumentDerivatives = 
                Arguments.Select(f => f.GetDerivative1()).ToImmutableArray();

            var plusArgumentList = new List<DifferentialFunction>(ArgumentCount);

            for (var i = 0; i < ArgumentCount; i++)
            {
                var fDt = argumentDerivatives[i];

                if (fDt is DfConstant { IsZero:true })
                    continue;

                var k = i;

                var term =
                    MapArguments((j, f) =>
                        j == k ? fDt : f
                    ).Simplify();

                if (term is DfConstant { IsZero:true })
                    continue;

                plusArgumentList.Add(term);
            }

            return DfPlus.Create(plusArgumentList).Simplify();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction MapArguments(Func<DifferentialFunction, DifferentialFunction> functionMapping)
        {
            var baseFunctions = 
                Arguments.Select(functionMapping).ToArray();

            return Create(baseFunctions);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction MapArguments(Func<int, DifferentialFunction, DifferentialFunction> functionMapping)
        {
            var baseFunctions = 
                Arguments.Select((f, i) => functionMapping(i, f)).ToArray();

            return Create(baseFunctions);
        }
    
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public override string ToString()
        //{
        //    return $"Times[{Arguments.Concatenate(", ")}]";
        //}
    }
}

//public sealed class DfTimes :
//    DifferentialNaryFunction
//{
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static DfTimes Create(DifferentialFunction baseFunction1, DifferentialFunction baseFunction2, bool canBeSimplified = true) 
//    {
//        return new DfTimes(baseFunction1, baseFunction2, canBeSimplified);
//    }


//    public override string Name 
//        => "Times";


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private DfTimes(DifferentialFunction baseFunction1, DifferentialFunction baseFunction2, bool canBeSimplified) 
//        : base(baseFunction1, baseFunction2, canBeSimplified)
//    {
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public override double GetValue(double t)
//    {
//        var x = BaseFunction1.GetValue(t);
//        var y = BaseFunction2.GetValue(t);

//        return x * y;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private static DifferentialFunction SimplifyTimesVar(DifferentialFunction f2)
//    {
//        return f2 switch
//        {
//            DfConstant f2Constant => 
//                DfTimesScalar.Create(DfVar.DefaultFunction, f2Constant.ScalarValue),

//            DfVar => 
//                DfPowerScalar.Create(DfVar.DefaultFunction, 2),

//            DifferentialCompositeFunction f2Composite => 
//                f2Composite.MapBaseFunctions(
//                    baseFunction => new DfTimes(
//                        baseFunction, 
//                        DfVar.DefaultFunction, 
//                        true
//                    )
//                ).Simplify(),

//            _ => 
//                new DfTimes(
//                    DfVar.DefaultFunction, 
//                    f2, 
//                    false
//                )
//        };
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private static DifferentialFunction SimplifyTimes(DfConstant f1Constant, DifferentialFunction f2)
//    {
//        return f2 switch
//        {
//            DfConstant f2Constant => 
//                DfConstant.Create(f1Constant.ScalarValue * f2Constant.ScalarValue).Simplify(),

//            DfVar => 
//                DfTimesScalar.Create(DfVar.DefaultFunction, f1Constant.ScalarValue, false),

//            DfMonomialBasis f2MonomialBasis => 
//                DfTimesScalar.Create(f2MonomialBasis, f1Constant.ScalarValue, false),

//            DfTimesScalar f2TimesScalar => 
//                DfTimesScalar.Create(f2TimesScalar.BaseFunction, f1Constant.ScalarValue * f2TimesScalar.ScalarValue).Simplify(),

//            _ => 
//                new DfTimes(
//                    DfVar.DefaultFunction, 
//                    f2, 
//                    false
//                )
//        };
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private static DifferentialFunction SimplifyTimes(DfMonomialBasis f1MonomialBasis, DifferentialFunction f2)
//    {
//        return f2 switch
//        {
//            DfConstant f2Constant => 
//                DfTimesScalar.Create(f1MonomialBasis, f2Constant.ScalarValue),

//            DfVar => 
//                DfMonomialBasis.Create(
//                    f1MonomialBasis.PowerValue + 1
//                ),

//            DfMonomialBasis f2MonomialBasis => 
//                DfMonomialBasis.Create(
//                    f2MonomialBasis.PowerValue + f1MonomialBasis.PowerValue
//                ),

//            DfTimesScalar f2TimesScalar => 
//                DfTimesScalar.Create(
//                    new DfTimes(f1MonomialBasis, f2TimesScalar.BaseFunction, true), 
//                    f2TimesScalar.ScalarValue
//                ).Simplify(),

//            DfPowerScalar f2PowerScalar =>
//                f2PowerScalar.BaseFunction switch
//                {
//                    // TODO: Complete this
//                    _ => new DfTimes(
//                        DfVar.DefaultFunction, 
//                        f2, 
//                        false
//                    )
//                },

//            _ => 
//                new DfTimes(
//                    DfVar.DefaultFunction, 
//                    f2, 
//                    false
//                )
//        };
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private static DifferentialFunction SimplifyTimes(DfTimesScalar f1TimesScalar, DifferentialFunction f2)
//    {
//        return f2 switch
//        {
//            DfConstant f2Constant => 
//                DfTimesScalar.Create(
//                    f1TimesScalar.BaseFunction, 
//                    f1TimesScalar.ScalarValue * f2Constant.ScalarValue
//                ).Simplify(),

//            DfPlus f2Plus =>
//                DfPlus.Create(
//                    f2Plus.MapBaseFunctions(f => 
//                        DfTimesScalar.Create(f, f1TimesScalar.ScalarValue)
//                    )
//                ).Simplify(),

//            _ => 
//                DfTimesScalar.Create(
//                    new DfTimes(
//                        f1TimesScalar.BaseFunction, 
//                        f2, 
//                        true
//                    ),
//                    f1TimesScalar.ScalarValue
//                ).Simplify()
//        };
//    }

//    private DifferentialFunction SimplifyTimes(DfPowerScalar f1PowerScalar, DifferentialFunction f2)
//    {
//        return new DfTimes(
//            f1PowerScalar,
//            f2,
//            false
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private static DifferentialFunction SimplifyTimes(DfCos f1Cos, DifferentialFunction f2)
//    {
//        return f2 switch
//        {
//            DfCos f2Cos =>
//                0.5d * (
//                    (f1Cos.BaseFunction - f2Cos.BaseFunction).Cos() + (f1Cos.BaseFunction + f2Cos.BaseFunction).Cos()
//                ),

//            DfSin f2Sin =>
//                0.5d * (
//                    (f1Cos.BaseFunction + f2Sin.BaseFunction).Sin() - (f1Cos.BaseFunction - f2Sin.BaseFunction).Sin()
//                ),

//            _ =>
//                new DfTimes(f1Cos, f2, false)
//        };
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private static DifferentialFunction SimplifyTimes(DfSin f1Sin, DifferentialFunction f2)
//    {
//        return f2 switch
//        {
//            DfSin f2Sin =>
//                0.5d * (
//                    (f1Sin.BaseFunction - f2Sin.BaseFunction).Cos() - (f1Sin.BaseFunction + f2Sin.BaseFunction).Cos()
//                ),

//            DfCos f2Cos =>
//                0.5d * (
//                    (f1Sin.BaseFunction + f2Cos.BaseFunction).Sin() + (f1Sin.BaseFunction - f2Cos.BaseFunction).Sin()
//                ),

//            _ =>
//                new DfTimes(f1Sin, f2, false)
//        };
//    }

//    public override DifferentialFunction Simplify()
//    {
//        if (!CanBeSimplified) return this;

//        var f1 = BaseFunction1.Simplify();
//        var f2 = BaseFunction2.Simplify();

//        if (f1 is DfZero || f2 is DfZero)
//            return DfZero.DefaultFunction;

//        if (f1 is DfOne)
//            return f2;

//        if (f2 is DfOne)
//            return f1;

//        return f1 switch
//        {
//            DfVar => 
//                SimplifyTimesVar(f2),

//            DfConstant f1Constant => 
//                SimplifyTimes(f1Constant, f2),

//            DfMonomialBasis f1MonomialBasis => 
//                SimplifyTimes(f1MonomialBasis, f2),

//            DfTimesScalar f1TimesScalar => 
//                SimplifyTimes(f1TimesScalar, f2),

//            DfPowerScalar f1PowerScalar => 
//                SimplifyTimes(f1PowerScalar, f2),

//            DfCos f1Cos =>
//                SimplifyTimes(f1Cos, f2),

//            DfSin f1Sin =>
//                SimplifyTimes(f1Sin, f2),

//            _ => 
//                new DfTimes(
//                    f1, 
//                    f2, 
//                    false
//                )
//        };
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public override DifferentialFunction GetDerivative1()
//    {
//        var x = BaseFunction1;
//        var y = BaseFunction2;
//        var xDt = x.GetDerivative1();
//        var yDt = y.GetDerivative1();

//        return DfPlus.Create(
//            new DfTimes(x, yDt, false),
//            new DfTimes(xDt, y, false)
//        ).Simplify();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public override DifferentialFunction MapBaseFunctions(Func<DifferentialFunction, DifferentialFunction> functionMapping)
//    {
//        var baseFunction1 = functionMapping(BaseFunction1);
//        var baseFunction2 = functionMapping(BaseFunction2);

//        return new DfTimes(
//            baseFunction1,
//            baseFunction2,
//            true
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public override DifferentialFunction MapBaseFunctions(Func<int, DifferentialFunction, DifferentialFunction> functionMapping)
//    {
//        var baseFunction1 = functionMapping(0, BaseFunction1);
//        var baseFunction2 = functionMapping(1, BaseFunction2);

//        return new DfTimes(
//            baseFunction1,
//            baseFunction2,
//            true
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public override string ToString()
//    {
//        return $"Times[{BaseFunction1}, {BaseFunction2}]";
//    }
//}