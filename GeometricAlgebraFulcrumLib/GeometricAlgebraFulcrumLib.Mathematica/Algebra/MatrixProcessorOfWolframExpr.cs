//using System;
//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Algebra.Scalars;
//using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
//using GeometricAlgebraFulcrumLib.Mathematica.Mathematica;
//using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory;
//using GeometricAlgebraFulcrumLib.Processors.MatrixAlgebra;
//using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
//using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
//using Wolfram.NETLink;

//namespace GeometricAlgebraFulcrumLib.Mathematica.Processors;

//public sealed class MatrixProcessorOfWolframExpr
//    : ISymbolicMatrixProcessor<Expr>
//{
//    public static MatrixProcessorOfWolframExpr Instance { get; }
//        = new MatrixProcessorOfWolframExpr();


//    public IScalarProcessor<Expr> ScalarProcessor { get; set; }
//        = ScalarProcessorOfWolframExpr.Instance;
    
//    public double ZeroEpsilon
//    {
//        get => ScalarProcessor.ZeroEpsilon;
//        set => ScalarProcessor.ZeroEpsilon = value;
//    }

//    public bool IsNumeric 
//        => false;

//    public bool IsSymbolic 
//        => true;

//    public Scalar<Expr> Zero { get; }

//    public Scalar<Expr> PositiveInfinity { get; }
    
//    public Scalar<Expr> NegativeInfinity { get; }

//    public Scalar<Expr> One { get; }
    
//    public Scalar<Expr> MinusOne { get; }
    
//    public Scalar<Expr> Two { get; }
    
//    public Scalar<Expr> MinusTwo { get; }
    
//    public Scalar<Expr> Ten { get; }
    
//    public Scalar<Expr> MinusTen { get; }
    
//    public Scalar<Expr> Pi { get; }
    
//    public Scalar<Expr> PiTimes2 { get; }
    
//    public Scalar<Expr> PiTimes4 { get; }

//    public Scalar<Expr> PiOver2 { get; }
    
//    public Scalar<Expr> E { get; }
    
//    public Scalar<Expr> DegreeToRadianFactor { get; }
    
//    public Scalar<Expr> RadianToDegreeFactor { get; }

//    public Expr ZeroValue 
//        => Expr.INT_ZERO;

//    public Expr PositiveInfinityValue { get; } 
//        = "Infinity".ToExpr();

//    public Expr NegativeInfinityValue { get; }
//        = "-Infinity".ToExpr();

//    public Expr OneValue 
//        => Expr.INT_ONE;

//    public Expr MinusOneValue 
//        => Expr.INT_MINUSONE;

//    public Expr TwoValue { get; } 
//        = 2.ToExpr();

//    public Expr MinusTwoValue { get; } 
//        = (-2).ToExpr();

//    public Expr TenValue { get; } 
//        = 10.ToExpr();

//    public Expr MinusTenValue { get; } 
//        = (-10).ToExpr();

//    public Expr PiValue 
//        => MathematicaInterface.DefaultCasConstants.ExprPi;

//    public Expr PiTimes2Value { get; }
//        = MathematicaInterface.DefaultCas["2 * Pi"];
    
//    public Expr PiTimes4Value { get; }
//        = MathematicaInterface.DefaultCas["4 * Pi"];

//    public Expr PiOver2Value { get; }
//        = MathematicaInterface.DefaultCas["Pi / 2"];
    
//    public Expr EValue 
//        => MathematicaInterface.DefaultCasConstants.ExprE;

//    public Expr DegreeToRadianFactorValue { get; }
//        = MathematicaInterface.DefaultCas["Pi / 180"];

//    public Expr RadianToDegreeFactorValue { get; }
//        = MathematicaInterface.DefaultCas["180 / Pi"];


//    private MatrixProcessorOfWolframExpr()
//    {
//        Zero = this.ScalarFromValue(ZeroValue);
//        One = this.ScalarFromValue(OneValue);
//        MinusOne = this.ScalarFromValue(MinusOneValue);
//        Two = this.ScalarFromValue(TwoValue);
//        MinusTwo = this.ScalarFromValue(MinusTwoValue);
//        Ten = this.ScalarFromValue(TenValue);
//        MinusTen = this.ScalarFromValue(MinusTenValue);
//        Pi = this.ScalarFromValue(PiValue);
//        E = this.ScalarFromValue(EValue);
//        PiTimes2 = this.ScalarFromValue(PiTimes2Value);
//        PiTimes4 = this.ScalarFromValue(PiTimes4Value);
//        PiOver2 = this.ScalarFromValue(PiOver2Value);
//        DegreeToRadianFactor = this.ScalarFromValue(DegreeToRadianFactorValue);
//        RadianToDegreeFactor = this.ScalarFromValue(RadianToDegreeFactorValue);
//        PositiveInfinity = this.ScalarFromValue(PositiveInfinityValue);
//        NegativeInfinity = this.ScalarFromValue(NegativeInfinityValue);
//    }

        
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> Add(Expr scalar1, Expr scalar2)
//    {
//        return ScalarProcessor.Add(scalar1, scalar2);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> Subtract(Expr scalar1, Expr scalar2)
//    {
//        return ScalarProcessor.Subtract(scalar1, scalar2);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> Times(Expr scalar1, Expr scalar2)
//    {
//        return ScalarProcessor.Times(scalar1, scalar2);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> Divide(Expr scalar1, Expr scalar2)
//    {
//        return ScalarProcessor.Divide(scalar1, scalar2);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> Positive(Expr scalar)
//    {
//        return ScalarProcessor.Positive(scalar);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> Negative(Expr scalar)
//    {
//        return ScalarProcessor.Negative(scalar);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> Inverse(Expr scalar)
//    {
//        return ScalarProcessor.Inverse(scalar);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> Sign(Expr scalar)
//    {
//        return ScalarProcessor.Sign(scalar);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> UnitStep(Expr scalar)
//    {
//        return ScalarProcessor.UnitStep(scalar);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public int GetDenseRowsCount(Expr matrix)
//    {
//        var dimensionsExpr = Mfs.Dimensions[matrix].Simplify();

//        return (int) dimensionsExpr[0].AsInt64();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public int GetDenseColumnsCount(Expr matrix)
//    {
//        var dimensionsExpr = Mfs.Dimensions[matrix].Simplify();

//        return (int) dimensionsExpr[1].AsInt64();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Pair<int> GetDenseSize(Expr matrix)
//    {
//        var dimensionsExpr = Mfs.Dimensions[matrix].Simplify();

//        return new Pair<int>(
//            (int) dimensionsExpr[0].AsInt64(),
//            (int) dimensionsExpr[1].AsInt64()
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Expr GetScalar(Expr matrix, int rowIndex, int colIndex)
//    {
//        var rowExpr = Mfs.Part[matrix, (rowIndex + 1).ToExpr()];

//        return Mfs.Part[rowExpr, (colIndex + 1).ToExpr()].Simplify();
//    }

//    public ILinVectorStorage<Expr> MatrixRowToVector(Expr matrix, int rowIndex)
//    {
//        var columnsCount = GetDenseColumnsCount(matrix);

//        var array = new Expr[columnsCount];
//        var rowExpr = Mfs.Part[matrix, rowIndex.ToExpr()];

//        for (var j = 0; j < columnsCount; j++) 
//            array[j] = Mfs.Part[rowExpr, j.ToExpr()].Simplify();

//        return array.CreateLinVectorDenseStorage();
//    }

//    public ILinVectorStorage<Expr> MatrixColumnToVector(Expr matrix, int colIndex)
//    {
//        var rowsCount = GetDenseRowsCount(matrix);

//        var array = new Expr[rowsCount];

//        var colIndexExpr = colIndex.ToExpr();
//        for (var i = 0; i < rowsCount; i++)
//        {
//            var rowExpr = Mfs.Part[matrix, i.ToExpr()];

//            array[i] = Mfs.Part[rowExpr, colIndexExpr].Simplify();
//        }

//        return array.CreateLinVectorDenseStorage();
//    }

//    public ILinMatrixStorage<Expr> MatrixToArray(Expr matrix)
//    {
//        var (rowsCount, columnsCount) = GetDenseSize(matrix);

//        var array = new Expr[rowsCount, columnsCount];

//        for (var i = 0; i < rowsCount; i++)
//        {
//            var rowExpr = Mfs.Part[matrix, i.ToExpr()];

//            for (var j = 0; j < columnsCount; j++)
//            {
//                array[i, j] = Mfs.Part[rowExpr, j.ToExpr()].Simplify();
//            }
//        }

//        return array.CreateLinMatrixDenseStorage();
//    }

//    public Expr CreateMatrix(ILinMatrixStorage<Expr> array)
//    {
//        return array.ToArray().ToMatrixExpr().Simplify();
//    }

//    public Expr CreateRowVectorMatrix(ILinVectorStorage<Expr> array)
//    {
//        var rowExprArray = new Expr[1];
//        var scalarExprArray = new Expr[array.GetSparseCount()];
            
//        for (var j = 0; j < array.GetSparseCount(); j++)
//            scalarExprArray[j] = array.GetScalar((ulong) j) ?? Expr.INT_ZERO;

//        rowExprArray[0] = Mfs.ListExpr(scalarExprArray);

//        return Mfs.ListExpr(rowExprArray).Simplify();
//    }

//    public Expr CreateRowVectorMatrix(ILinMatrixStorage<Expr> array, int rowIndex)
//    {
//        var count = array.GetDenseCount2();
//        var rowExprArray = new Expr[1];
//        var scalarExprArray = new Expr[count];
            
//        for (var j = 0; j < count; j++)
//            scalarExprArray[j] = 
//                array.GetScalar(rowIndex, j, Expr.INT_ZERO);

//        rowExprArray[0] = Mfs.ListExpr(scalarExprArray);

//        return Mfs.ListExpr(rowExprArray).Simplify();
//    }

//    public Expr CreateColumnVectorMatrix(ILinVectorStorage<Expr> array)
//    {
//        var rowExprArray = new Expr[array.GetSparseCount()];
//        for (var i = 0; i < array.GetSparseCount(); i++)
//        {
//            var scalarExprArray = new Expr[1];
                
//            scalarExprArray[0] = 
//                array.GetScalar(i, Expr.INT_ZERO);

//            rowExprArray[i] = Mfs.ListExpr(scalarExprArray);
//        }

//        return Mfs.ListExpr(rowExprArray).Simplify();
//    }

//    public Expr CreateColumnVectorMatrix(ILinMatrixStorage<Expr> array, int columnIndex)
//    {
//        var count = array.GetDenseCount1();
//        var rowExprArray = new Expr[count];
//        for (var i = 0; i < count; i++)
//        {
//            var scalarExprArray = new Expr[1];
                
//            scalarExprArray[0] = 
//                array.GetScalar(i, columnIndex, Expr.INT_ZERO);

//            rowExprArray[i] = Mfs.ListExpr(scalarExprArray);
//        }

//        return Mfs.ListExpr(rowExprArray).Simplify();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Expr CreateZeroMatrix(int size)
//    {
//        var sizeExpr = size.ToExpr();

//        return Mfs.ConstantArray[
//            Expr.INT_ZERO,
//            Mfs.List[sizeExpr, sizeExpr]
//        ].Evaluate();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Expr CreateZeroMatrix(int rowsCount, int columnsCount)
//    {
//        return Mfs.ConstantArray[
//            Expr.INT_ZERO,
//            Mfs.List[rowsCount.ToExpr(), columnsCount.ToExpr()]
//        ].Evaluate();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Expr CreateUnityMatrix(int size)
//    {
//        return Mfs.IdentityMatrix[size.ToExpr()].Simplify();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Expr AddMatrixScalar(Expr matrix, Expr scalar)
//    {
//        return Mfs.Plus[matrix, scalar].Simplify();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Expr AddScalarMatrix(Expr scalar, Expr matrix)
//    {
//        return Mfs.Plus[scalar, matrix].Simplify();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Expr AddMatrices(Expr matrix1, Expr matrix2)
//    {
//        return Mfs.Plus[matrix1, matrix2].Simplify();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Expr SubtractMatrixScalar(Expr matrix, Expr scalar)
//    {
//        return Mfs.Plus[matrix, scalar].Simplify();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Expr SubtractScalarMatrix(Expr scalar, Expr matrix)
//    {
//        return Mfs.Plus[scalar, matrix].Simplify();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Expr SubtractMatrices(Expr matrix1, Expr matrix2)
//    {
//        return Mfs.Plus[matrix1, matrix2].Simplify();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Expr TimesScalarMatrix(Expr scalar, Expr matrix)
//    {
//        return Mfs.Times[scalar, matrix].Simplify();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Expr TimesMatrixScalar(Expr matrix, Expr scalar)
//    {
//        return Mfs.Times[matrix, scalar].Simplify();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Expr TimesMatrixItems(Expr matrix1, Expr matrix2)
//    {
//        return Mfs.Times[matrix1, matrix2].Simplify();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Expr DivideMatrixItems(Expr matrix1, Expr matrix2)
//    {
//        return Mfs.Divide[matrix1, matrix2].Simplify();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Expr DivideMatrixScalar(Expr matrix, Expr scalar)
//    {
//        return Mfs.Divide[matrix, scalar].Simplify();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Expr DivideScalarMatrix(Expr scalar, Expr matrix)
//    {
//        return Mfs.Divide[scalar, matrix].Simplify();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Expr TimesMatrices(Expr matrix1, Expr matrix2)
//    {
//        return Mfs.Dot[matrix1, matrix2].Simplify();
//    }

//    public Expr MapMatrixItems(Expr matrix, Func<int, int, Expr, Expr> mappingFunc)
//    {
//        var (rowsCount, columnsCount) = GetDenseSize(matrix);

//        var rowExprArray = new Expr[rowsCount];

//        for (var i = 0; i < rowsCount; i++)
//        {
//            var rowExpr = Mfs.Part[matrix, i.ToExpr()];
//            var scalarExprArray = new Expr[columnsCount];

//            for (var j = 0; j < columnsCount; j++)
//            {
//                scalarExprArray[j] = mappingFunc(
//                    i, 
//                    j, 
//                    Mfs.Part[rowExpr, j.ToExpr()]
//                );
//            }

//            rowExprArray[i] = Mfs.ListExpr(scalarExprArray);
//        }

//        return Mfs.ListExpr(rowExprArray).Simplify();
//    }

//    public Expr MapMatrixItems(Expr matrix1, Expr matrix2, Func<Expr, Expr, Expr> mappingFunc)
//    {
//        var (rowsCount, columnsCount) = GetDenseSize(matrix1);

//        var rowExprArray = new Expr[rowsCount];

//        for (var i = 0; i < rowsCount; i++)
//        {
//            var rowExpr1 = Mfs.Part[matrix1, i.ToExpr()];
//            var rowExpr2 = Mfs.Part[matrix2, i.ToExpr()];
//            var scalarExprArray = new Expr[columnsCount];

//            for (var j = 0; j < columnsCount; j++)
//            {
//                scalarExprArray[j] = mappingFunc(
//                    Mfs.Part[rowExpr1, j.ToExpr()],
//                    Mfs.Part[rowExpr2, j.ToExpr()]
//                );
//            }

//            rowExprArray[i] = Mfs.ListExpr(scalarExprArray);
//        }

//        return Mfs.ListExpr(rowExprArray).Simplify();
//    }

//    public Expr MapMatrixItems(Expr matrix, Func<Expr, Expr> mappingFunc)
//    {
//        var (rowsCount, columnsCount) = GetDenseSize(matrix);

//        var rowExprArray = new Expr[rowsCount];

//        for (var i = 0; i < rowsCount; i++)
//        {
//            var rowExpr = Mfs.Part[matrix, i.ToExpr()];
//            var scalarExprArray = new Expr[columnsCount];

//            for (var j = 0; j < columnsCount; j++)
//            {
//                scalarExprArray[j] = mappingFunc(
//                    Mfs.Part[rowExpr, j.ToExpr()]
//                );
//            }

//            rowExprArray[i] = Mfs.ListExpr(scalarExprArray);
//        }

//        return Mfs.ListExpr(rowExprArray).Simplify();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Expr MatrixNegative(Expr matrix)
//    {
//        return Mfs.Minus[matrix].Simplify();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Expr MatrixTranspose(Expr matrix)
//    {
//        return Mfs.Transpose[matrix].Simplify();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Expr MatrixInverse(Expr matrix)
//    {
//        return Mfs.Inverse[matrix].Simplify();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> Abs(Expr scalar)
//    {
//        return ScalarProcessor.Abs(scalar);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> Power(Expr baseScalar, Expr scalar)
//    {
//        return ScalarProcessor.Power(baseScalar, scalar);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> Sqrt(Expr scalar)
//    {
//        return ScalarProcessor.Sqrt(scalar);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> SqrtOfAbs(Expr scalar)
//    {
//        return ScalarProcessor.SqrtOfAbs(scalar);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> Exp(Expr scalar)
//    {
//        return ScalarProcessor.Exp(scalar);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> LogE(Expr scalar)
//    {
//        return ScalarProcessor.LogE(scalar);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> Log2(Expr scalar)
//    {
//        return ScalarProcessor.Log2(scalar);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> Log10(Expr scalar)
//    {
//        return ScalarProcessor.Log10(scalar);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> Log(Expr baseScalar, Expr scalar)
//    {
//        return ScalarProcessor.Log(baseScalar, scalar);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> Cos(Expr scalar)
//    {
//        return ScalarProcessor.Cos(scalar);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> Sin(Expr scalar)
//    {
//        return ScalarProcessor.Sin(scalar);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> Tan(Expr scalar)
//    {
//        return ScalarProcessor.Tan(scalar);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> Cosh(Expr scalar)
//    {
//        return ScalarProcessor.Cosh(scalar);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> Sinh(Expr scalar)
//    {
//        return ScalarProcessor.Sinh(scalar);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> Tanh(Expr scalar)
//    {
//        return ScalarProcessor.Tanh(scalar);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public bool IsValid(Expr scalar)
//    {
//        return ScalarProcessor.IsValid(scalar);
//    }

//    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
//    //public bool IsFiniteNumber(Expr scalar)
//    //{
//    //    return ScalarProcessor.IsFiniteNumber(scalar);
//    //}

//    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
//    //public bool IsZero(Expr scalar)
//    //{
//    //    return ScalarProcessor.IsZero(scalar);
//    //}

//    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
//    //public bool IsZero(Expr scalar, bool nearZeroFlag)
//    //{
//    //    return ScalarProcessor.IsZero(scalar, nearZeroFlag);
//    //}

//    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
//    //public bool IsNearZero(Expr scalar)
//    //{
//    //    return ScalarProcessor.IsNearZero(scalar);
//    //}

//    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
//    //public bool IsNotZero(Expr scalar)
//    //{
//    //    return !ScalarProcessor.IsZero(scalar);
//    //}

//    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
//    //public bool IsNotZero(Expr scalar, bool nearZeroFlag)
//    //{
//    //    return !ScalarProcessor.IsZero(scalar, nearZeroFlag);
//    //}

//    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
//    //public bool IsNotNearZero(Expr scalar)
//    //{
//    //    return !ScalarProcessor.IsNearZero(scalar);
//    //}

//    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
//    //public bool IsPositive(Expr scalar)
//    //{
//    //    return ScalarProcessor.IsPositive(scalar);
//    //}

//    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
//    //public bool IsNegative(Expr scalar)
//    //{
//    //    return ScalarProcessor.IsNegative(scalar);
//    //}

//    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
//    //public bool IsNotPositive(Expr scalar)
//    //{
//    //    return !ScalarProcessor.IsPositive(scalar);
//    //}

//    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
//    //public bool IsNotNegative(Expr scalar)
//    //{
//    //    return !ScalarProcessor.IsNegative(scalar);
//    //}

//    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
//    //public bool IsNotNearPositive(Expr scalar)
//    //{
//    //    return ScalarProcessor.IsNotNearPositive(scalar);
//    //}

//    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
//    //public bool IsNotNearNegative(Expr scalar)
//    //{
//    //    return ScalarProcessor.IsNotNearNegative(scalar);
//    //}

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> ScalarFromText(string text)
//    {
//        return ScalarProcessor.ScalarFromText(text);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> ScalarFromNumber(int value)
//    {
//        return ScalarProcessor.ScalarFromNumber(value);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> ScalarFromNumber(uint value)
//    {
//        return ScalarProcessor.ScalarFromNumber(value);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> ScalarFromNumber(long value)
//    {
//        return ScalarProcessor.ScalarFromNumber(value);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> ScalarFromNumber(ulong value)
//    {
//        return ScalarProcessor.ScalarFromNumber(value);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> ScalarFromNumber(float value)
//    {
//        return ScalarProcessor.ScalarFromNumber(value);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> ScalarFromNumber(double value)
//    {
//        return ScalarProcessor.ScalarFromNumber(value);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> ScalarFromRational(long numerator, long denominator)
//    {
//        return ScalarProcessor.ScalarFromRational(numerator, denominator);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> ScalarFromRandom(Random randomGenerator, double minValue, double maxValue)
//    {
//        return ScalarProcessor.ScalarFromRandom(randomGenerator, minValue, maxValue);
//    }
    
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public double ToFloat64(Expr scalar)
//    {
//        return ScalarProcessor.ToFloat64(scalar);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public string ToText(Expr scalar)
//    {
//        return ScalarProcessor.ToText(scalar);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<Expr> VectorToRadians(Expr scalarX, Expr scalarY)
//    {
//        return ScalarProcessor.VectorToRadians(scalarX, scalarY);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Expr MatrixInverseTranspose(Expr matrix)
//    {
//        return Mfs.Transpose[Mfs.Inverse[matrix]].Simplify();
//    }

//    public int MatrixEigenDecomposition(Expr matrix, out Tuple<Expr, ILinVectorStorage<Expr>>[] realPairs, out Tuple<Expr, ILinVectorStorage<Expr>>[] imagPairs)
//    {
//        var sysExpr = Mfs.Eigensystem[matrix].Evaluate();

//        var realEigenValues = Mfs.Re[sysExpr.Args[0]].Evaluate().Args;
//        var imagEigenValues = Mfs.Im[sysExpr.Args[0]].Evaluate().Args;

//        var realEigenVectors = Mfs.Re[sysExpr.Args[1]].Evaluate().Args;
//        var imagEigenVectors = Mfs.Im[sysExpr.Args[1]].Evaluate().Args;

//        var count = realEigenValues.Length;

//        realPairs = new Tuple<Expr, ILinVectorStorage<Expr>>[count];
//        imagPairs = new Tuple<Expr, ILinVectorStorage<Expr>>[count];

//        for (var i = 0; i < count; i++)
//        {
//            realPairs[i] = new Tuple<Expr, ILinVectorStorage<Expr>>(
//                realEigenValues[i],
//                realEigenVectors[i].Args.CreateLinVectorArrayStorage()
//            );

//            imagPairs[i] = new Tuple<Expr, ILinVectorStorage<Expr>>(
//                imagEigenValues[i],
//                imagEigenVectors[i].Args.CreateLinVectorArrayStorage()
//            );
//        }

//        return count;
//    }

//    public Pair<Expr>[] MatrixEigenValues(Expr matrix)
//    {
//        var sysExpr = Mfs.Eigensystem[matrix].Evaluate();

//        var realEigenValues = Mfs.Re[sysExpr.Args[0]].Evaluate().Args;
//        var imagEigenValues = Mfs.Im[sysExpr.Args[0]].Evaluate().Args;

//        var count = realEigenValues.Length;

//        var eigenValueTuples = new Pair<Expr>[count];

//        for (var i = 0; i < count; i++)
//        {
//            eigenValueTuples[i] = new Pair<Expr>(
//                realEigenValues[i],
//                imagEigenValues[i]
//            );
//        }

//        return eigenValueTuples;
//    }

//    public Pair<ILinVectorStorage<Expr>>[] MatrixEigenVectors(Expr matrix)
//    {
//        var sysExpr = Mfs.Eigenvectors[matrix].Evaluate();

//        var count = sysExpr.Length;
            
//        var realEigenVectors = Mfs.Re[sysExpr].Evaluate().Args;
//        var imagEigenVectors = Mfs.Im[sysExpr].Evaluate().Args;

//        var eigenVectorTuples = new Pair<ILinVectorStorage<Expr>>[count];

//        for (var i = 0; i < count; i++)
//        {
//            eigenVectorTuples[i] = new Pair<ILinVectorStorage<Expr>>(
//                realEigenVectors[i].Args.CreateLinVectorArrayStorage(),
//                imagEigenVectors[i].Args.CreateLinVectorArrayStorage()
//            );
//        }

//        return eigenVectorTuples;
//    }
//}