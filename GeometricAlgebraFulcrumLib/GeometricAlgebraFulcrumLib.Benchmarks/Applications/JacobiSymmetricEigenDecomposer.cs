using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Diagnostics;
using System.Text;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Benchmarks.Applications;

public sealed class JacobiSymmetricEigenDecomposer
{
    /// <summary>
    /// Code example for using the code
    /// </summary>
    public static void SampleUse()
    {
        const int size = 6;
        var randGen = new Random(10);

        var matrix = new double[size, size];

        for (var i = 0; i < size; i++)
        {
            for (var j = 0; j <= i; j++)
            {
                var s = randGen.Next(10);
                matrix[i, j] = s;
                matrix[j, i] = s;
            }
        }

        //var matrix = new double[,]
        //{
        //    { 4, 1, 2, 1 },
        //    { 1, 5, 1, 2 },
        //    { 2, 1, 6, 1 },
        //    { 1, 2, 1, 7 }
        //};

        var eig = new JacobiSymmetricEigenDecomposer(matrix);
        eig.EigenDecompose();

        Console.WriteLine(eig.ToString());
        Console.WriteLine();

        Console.WriteLine(DenseMatrix.OfArray(matrix).Evd().EigenValues.ToString());
        Console.WriteLine();
    }


    /// <summary>
    /// Helper: Swap two doubles
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    private static void Swap(ref double a, ref double b)
    {
        (a, b) = (b, a);
    }

    private static double BinaryStep(double value)
    {
        return value >= 0 ? 1 : -1;
    }


    private double _oaR0C0;
    private double _oaR1C0, _oaR1C1;
    private double _oaR2C0, _oaR2C1, _oaR2C2;
    private double _oaR3C0, _oaR3C1, _oaR3C2, _oaR3C3;
    private double _oaR4C0, _oaR4C1, _oaR4C2, _oaR4C3, _oaR4C4;
    private double _oaR5C0, _oaR5C1, _oaR5C2, _oaR5C3, _oaR5C4, _oaR5C5;

    private double _ovR0C0, _ovR0C1, _ovR0C2, _ovR0C3, _ovR0C4, _ovR0C5;
    private double _ovR1C0, _ovR1C1, _ovR1C2, _ovR1C3, _ovR1C4, _ovR1C5;
    private double _ovR2C0, _ovR2C1, _ovR2C2, _ovR2C3, _ovR2C4, _ovR2C5;
    private double _ovR3C0, _ovR3C1, _ovR3C2, _ovR3C3, _ovR3C4, _ovR3C5;
    private double _ovR4C0, _ovR4C1, _ovR4C2, _ovR4C3, _ovR4C4, _ovR4C5;
    private double _ovR5C0, _ovR5C1, _ovR5C2, _ovR5C3, _ovR5C4, _ovR5C5;


    public int Size { get; }

    public int MaxSweeps { get; set; } = 50;

    public double NormTolerance { get; set; } = 1e-14;

    public bool SortEigenValues { get; set; } = true;

    public double[,] SymmetricMatrix { get; }

    public double[,] DiagonalMatrix { get; private set; }

    public double[] EigenValues { get; private set; }

    public double[,] EigenVectors { get; private set; }


    public JacobiSymmetricEigenDecomposer(double[,] symmetricMatrix)
    {
        Size = symmetricMatrix.GetLength(0);

        Debug.Assert(Size >= 2 && Size == symmetricMatrix.GetLength(1));

        SymmetricMatrix = symmetricMatrix;
    }


    /// <summary>
    /// The Rotate function performs a Givens rotation to reduce one off-diagonal
    /// element of the matrix to (nearly) zero. It is used iteratively in the
    /// Jacobi method to diagonalize a symmetric matrix, which allows us to extract
    /// eigenvalues from the diagonal and eigenvectors from the accumulated rotations.
    /// </summary>
    /// <param name="p"></param>
    /// <param name="q"></param>
    private void Rotate(int p, int q)
    {
        // The off-diagonal element we want to eliminate
        var apq = DiagonalMatrix[p, q];

        // The corresponding diagonal entries
        var app = DiagonalMatrix[p, p];
        var aqq = DiagonalMatrix[q, q];

        // This computes the tangent of the rotation angle t = tan(θ) using a
        // numerically stable formula derived from solving the zeroing condition
        var tau = (aqq - app) / (2 * apq);
        var t = BinaryStep(tau) / (Math.Sqrt(1 + tau * tau) + Math.Abs(tau));
        //var t =
        //    tau >= 0
        //        ? 1 / (Math.Sqrt(1 + tau * tau) + tau)
        //        : -1 / (Math.Sqrt(1 + tau * tau) - tau);

        // Compute Cosine and Sine of θ
        var c = 1 / Math.Sqrt(1 + t * t);
        var s = t * c;

        // Zero out off-diagonal elements
        DiagonalMatrix[p, q] = 0.0;
        DiagonalMatrix[q, p] = 0.0;

        // Update diagonal entries
        DiagonalMatrix[p, p] = app - t * apq; //app2;
        DiagonalMatrix[q, q] = aqq + t * apq; //aqq2;

        // Apply rotation to other rows/columns
        for (var r = 0; r < Size; r++)
        {
            if (r == p || r == q) continue;

            // Row updates
            var arp = DiagonalMatrix[r, p];
            var arq = DiagonalMatrix[r, q];

            DiagonalMatrix[r, p] = arp * c - arq * s;
            DiagonalMatrix[r, q] = arq * c + arp * s;

            // Symmetric column updates
            DiagonalMatrix[p, r] = DiagonalMatrix[r, p];
            DiagonalMatrix[q, r] = DiagonalMatrix[r, q];
        }

        // Update eigenvectors
        for (var r = 0; r < Size; r++)
        {
            var vrp = EigenVectors[r, p];
            var vrq = EigenVectors[r, q];

            EigenVectors[r, p] = vrp * c - vrq * s;
            EigenVectors[r, q] = vrq * c + vrp * s;
        }
    }

    /// <summary>
    /// Swap columns in EigenVectors matrix
    /// </summary>
    /// <param name="col1"></param>
    /// <param name="col2"></param>
    private void SwapEigenVectors(int col1, int col2)
    {
        for (var i = 0; i < Size; i++)
            Swap(ref EigenVectors[i, col1], ref EigenVectors[i, col2]);
    }


    private void Initialize2()
    {
        _ovR0C0 = 1d; _ovR0C1 = 0d;
        _ovR1C0 = 0d; _ovR1C1 = 1d;

        _oaR0C0 = SymmetricMatrix[0, 0];

        _oaR1C0 = SymmetricMatrix[1, 0];
        _oaR1C1 = SymmetricMatrix[1, 1];
    }

    private void Initialize3()
    {
        _ovR0C0 = 1d; _ovR0C1 = 0d; _ovR0C2 = 0d;
        _ovR1C0 = 0d; _ovR1C1 = 1d; _ovR1C2 = 0d;
        _ovR2C0 = 0d; _ovR2C1 = 0d; _ovR2C2 = 1d;

        _oaR0C0 = SymmetricMatrix[0, 0];

        _oaR1C0 = SymmetricMatrix[1, 0];
        _oaR1C1 = SymmetricMatrix[1, 1];

        _oaR2C0 = SymmetricMatrix[2, 0];
        _oaR2C1 = SymmetricMatrix[2, 1];
        _oaR2C2 = SymmetricMatrix[2, 2];
    }

    private void Initialize4()
    {
        _ovR0C0 = 1d; _ovR0C1 = 0d; _ovR0C2 = 0d; _ovR0C3 = 0d;
        _ovR1C0 = 0d; _ovR1C1 = 1d; _ovR1C2 = 0d; _ovR1C3 = 0d;
        _ovR2C0 = 0d; _ovR2C1 = 0d; _ovR2C2 = 1d; _ovR2C3 = 0d;
        _ovR3C0 = 0d; _ovR3C1 = 0d; _ovR3C2 = 0d; _ovR3C3 = 1d;

        _oaR0C0 = SymmetricMatrix[0, 0];

        _oaR1C0 = SymmetricMatrix[1, 0];
        _oaR1C1 = SymmetricMatrix[1, 1];

        _oaR2C0 = SymmetricMatrix[2, 0];
        _oaR2C1 = SymmetricMatrix[2, 1];
        _oaR2C2 = SymmetricMatrix[2, 2];

        _oaR3C0 = SymmetricMatrix[3, 0];
        _oaR3C1 = SymmetricMatrix[3, 1];
        _oaR3C2 = SymmetricMatrix[3, 2];
        _oaR3C3 = SymmetricMatrix[3, 3];
    }

    private void Initialize5()
    {
        _ovR0C0 = 1d; _ovR0C1 = 0d; _ovR0C2 = 0d; _ovR0C3 = 0d; _ovR0C4 = 0d;
        _ovR1C0 = 0d; _ovR1C1 = 1d; _ovR1C2 = 0d; _ovR1C3 = 0d; _ovR1C4 = 0d;
        _ovR2C0 = 0d; _ovR2C1 = 0d; _ovR2C2 = 1d; _ovR2C3 = 0d; _ovR2C4 = 0d;
        _ovR3C0 = 0d; _ovR3C1 = 0d; _ovR3C2 = 0d; _ovR3C3 = 1d; _ovR3C4 = 0d;
        _ovR4C0 = 0d; _ovR4C1 = 0d; _ovR4C2 = 0d; _ovR4C3 = 0d; _ovR4C4 = 1d;

        _oaR0C0 = SymmetricMatrix[0, 0];

        _oaR1C0 = SymmetricMatrix[1, 0];
        _oaR1C1 = SymmetricMatrix[1, 1];

        _oaR2C0 = SymmetricMatrix[2, 0];
        _oaR2C1 = SymmetricMatrix[2, 1];
        _oaR2C2 = SymmetricMatrix[2, 2];

        _oaR3C0 = SymmetricMatrix[3, 0];
        _oaR3C1 = SymmetricMatrix[3, 1];
        _oaR3C2 = SymmetricMatrix[3, 2];
        _oaR3C3 = SymmetricMatrix[3, 3];

        _oaR4C0 = SymmetricMatrix[4, 0];
        _oaR4C1 = SymmetricMatrix[4, 1];
        _oaR4C2 = SymmetricMatrix[4, 2];
        _oaR4C3 = SymmetricMatrix[4, 3];
        _oaR4C4 = SymmetricMatrix[4, 4];
    }

    private void Initialize6()
    {
        _ovR0C0 = 1d; _ovR0C1 = 0d; _ovR0C2 = 0d; _ovR0C3 = 0d; _ovR0C4 = 0d; _ovR0C5 = 0d;
        _ovR1C0 = 0d; _ovR1C1 = 1d; _ovR1C2 = 0d; _ovR1C3 = 0d; _ovR1C4 = 0d; _ovR1C5 = 0d;
        _ovR2C0 = 0d; _ovR2C1 = 0d; _ovR2C2 = 1d; _ovR2C3 = 0d; _ovR2C4 = 0d; _ovR2C5 = 0d;
        _ovR3C0 = 0d; _ovR3C1 = 0d; _ovR3C2 = 0d; _ovR3C3 = 1d; _ovR3C4 = 0d; _ovR3C5 = 0d;
        _ovR4C0 = 0d; _ovR4C1 = 0d; _ovR4C2 = 0d; _ovR4C3 = 0d; _ovR4C4 = 1d; _ovR4C5 = 0d;
        _ovR5C0 = 0d; _ovR5C1 = 0d; _ovR5C2 = 0d; _ovR5C3 = 0d; _ovR5C4 = 0d; _ovR5C5 = 1d;

        _oaR0C0 = SymmetricMatrix[0, 0];

        _oaR1C0 = SymmetricMatrix[1, 0];
        _oaR1C1 = SymmetricMatrix[1, 1];

        _oaR2C0 = SymmetricMatrix[2, 0];
        _oaR2C1 = SymmetricMatrix[2, 1];
        _oaR2C2 = SymmetricMatrix[2, 2];

        _oaR3C0 = SymmetricMatrix[3, 0];
        _oaR3C1 = SymmetricMatrix[3, 1];
        _oaR3C2 = SymmetricMatrix[3, 2];
        _oaR3C3 = SymmetricMatrix[3, 3];

        _oaR4C0 = SymmetricMatrix[4, 0];
        _oaR4C1 = SymmetricMatrix[4, 1];
        _oaR4C2 = SymmetricMatrix[4, 2];
        _oaR4C3 = SymmetricMatrix[4, 3];
        _oaR4C4 = SymmetricMatrix[4, 4];

        _oaR5C0 = SymmetricMatrix[5, 0];
        _oaR5C1 = SymmetricMatrix[5, 1];
        _oaR5C2 = SymmetricMatrix[5, 2];
        _oaR5C3 = SymmetricMatrix[5, 3];
        _oaR5C4 = SymmetricMatrix[5, 4];
        _oaR5C5 = SymmetricMatrix[5, 5];
    }

    private void InitializeN()
    {
        DiagonalMatrix = new double[Size, Size];
        EigenValues = new double[Size];
        EigenVectors = new double[Size, Size];

        for (var i = 0; i < Size; i++)
            EigenVectors[i, i] = 1;

        for (var i = 0; i < Size; i++)
            for (var j = i; j < Size; j++)
            {
                DiagonalMatrix[i, j] = SymmetricMatrix[i, j];
                DiagonalMatrix[j, i] = SymmetricMatrix[i, j];
            }
    }


    /// <summary>
    /// Compute off-diagonal norm
    /// </summary>
    /// <returns></returns>
    private double GetOffDiagonalNorm2()
    {
        return _oaR1C0 * _oaR1C0;
    }

    /// <summary>
    /// Compute off-diagonal norm
    /// </summary>
    /// <returns></returns>
    private double GetOffDiagonalNorm3()
    {
        return _oaR1C0 * _oaR1C0 +
               _oaR2C0 * _oaR2C0 +
               _oaR2C1 * _oaR2C1;
    }

    /// <summary>
    /// Compute off-diagonal norm
    /// </summary>
    /// <returns></returns>
    private double GetOffDiagonalNorm4()
    {
        return _oaR1C0 * _oaR1C0 +
               _oaR2C0 * _oaR2C0 +
               _oaR3C0 * _oaR3C0 +
               _oaR2C1 * _oaR2C1 +
               _oaR3C1 * _oaR3C1 +
               _oaR3C2 * _oaR3C2;
    }

    /// <summary>
    /// Compute off-diagonal norm
    /// </summary>
    /// <returns></returns>
    private double GetOffDiagonalNorm5()
    {
        return _oaR1C0 * _oaR1C0 +
               _oaR2C0 * _oaR2C0 +
               _oaR3C0 * _oaR3C0 +
               _oaR4C0 * _oaR4C0 +
               _oaR2C1 * _oaR2C1 +
               _oaR3C1 * _oaR3C1 +
               _oaR4C1 * _oaR4C1 +
               _oaR3C2 * _oaR3C2 +
               _oaR4C2 * _oaR4C2 +
               _oaR4C3 * _oaR4C3;
    }

    /// <summary>
    /// Compute off-diagonal norm
    /// </summary>
    /// <returns></returns>
    private double GetOffDiagonalNorm6()
    {
        return _oaR1C0 * _oaR1C0 +
               _oaR2C0 * _oaR2C0 +
               _oaR3C0 * _oaR3C0 +
               _oaR4C0 * _oaR4C0 +
               _oaR5C0 * _oaR5C0 +
               _oaR2C1 * _oaR2C1 +
               _oaR3C1 * _oaR3C1 +
               _oaR4C1 * _oaR4C1 +
               _oaR5C1 * _oaR5C1 +
               _oaR3C2 * _oaR3C2 +
               _oaR4C2 * _oaR4C2 +
               _oaR5C2 * _oaR5C2 +
               _oaR4C3 * _oaR4C3 +
               _oaR5C3 * _oaR5C3 +
               _oaR5C4 * _oaR5C4;
    }

    /// <summary>
    /// Compute off-diagonal norm
    /// </summary>
    /// <returns></returns>
    private double GetOffDiagonalNormN()
    {
        var norm = 0d;

        for (var i = 0; i < Size - 1; i++)
            for (var j = i + 1; j < Size; j++)
                norm += DiagonalMatrix[i, j] * DiagonalMatrix[i, j];

        return norm;
    }


    private void Rotate2()
    {
        //Begin GA-FuL MetaContext Code Generation, 2025-07-05T04:05:16.9813736+03:00
        //MetaContext: JacobiSymmetricEigenDecomposer2x2
        //Input Variables: 7 used, 0 not used, 7 total.
        //Temp Variables: 19 sub-expressions, 0 generated temps, 19 total.
        //Target Temp Variables: 19 total.
        //Output Variables: 7 total.
        //Computations: 1.5384615384615385 average, 40 total.
        //Memory Reads: 1.9230769230769231 average, 50 total.
        //Memory Writes: 26 total.
        //
        //MetaContext Binding Data: 
        //   0 = constant: '0'
        //   1 = constant: '1'
        //   -1 = constant: '-1'
        //   2 = constant: '2'
        //   -2 = constant: '-2'
        //   Rational[1, 2] = constant: 'Rational[1, 2]'
        //   Rational[-1, 2] = constant: 'Rational[-1, 2]'
        //   iaR0C0 = parameter: _oaR0C0
        //   iaR1C0 = parameter: _oaR1C0
        //   iaR1C1 = parameter: _oaR1C1
        //   ivR0C0 = parameter: _ovR0C0
        //   ivR1C0 = parameter: _ovR1C0
        //   ivR0C1 = parameter: _ovR0C1
        //   ivR1C1 = parameter: _ovR1C1

        var temp0 = -_oaR0C0 + _oaR1C1;
        var temp1 = 0.5 * 1 / _oaR1C0 * temp0;
        var temp2 = Math.Sqrt(1 + temp1 * temp1) + Math.Abs(temp1);
        var temp3 = 1 / temp2;
        var temp4 = _oaR1C0 * temp3;
        var temp5 = BinaryStep(temp1);
        var temp6 = temp4 * temp5;
        var oaR0C0 = _oaR0C0 + -temp6;

        var oaR1C1 = _oaR1C1 + temp6;

        var temp9 = Math.Pow(temp2, -2) * temp5 * temp5;
        var temp10 = 1 / Math.Sqrt(1 + temp9);
        var temp11 = _ovR0C1 * temp3;
        var temp12 = temp5 * temp11;
        var temp13 = temp10 * temp12;
        var ovR0C0 = _ovR0C0 * temp10 + -temp13;

        var temp15 = _ovR0C0 * temp3;
        var temp16 = temp5 * temp15;
        var ovR0C1 = temp10 * (_ovR0C1 + temp16);

        var temp18 = _ovR1C1 * temp3;
        var temp19 = temp18 * temp5;
        var temp20 = temp19 * temp10;
        var ovR1C0 = -temp20 + _ovR1C0 * temp10;

        var temp22 = _ovR1C0 * temp3;
        var temp23 = temp22 * temp5;
        var ovR1C1 = (_ovR1C1 + temp23) * temp10;

        var oaR1C0 = 0;

        //Finish GA-FuL MetaContext Code Generation, 2025-07-05T04:05:17.0824832+03:00

        _oaR0C0 = oaR0C0;
        _oaR1C0 = oaR1C0;
        _oaR1C1 = oaR1C1;

        _ovR0C0 = ovR0C0;
        _ovR1C0 = ovR1C0;
        _ovR0C1 = ovR0C1;
        _ovR1C1 = ovR1C1;
    }

    private void Rotate3()
    {
        //Begin GA-FuL MetaContext Code Generation, 2025-07-05T04:05:18.5975115+03:00
        //MetaContext: JacobiSymmetricEigenDecomposer3x3
        //Input Variables: 15 used, 0 not used, 15 total.
        //Temp Variables: 102 sub-expressions, 0 generated temps, 102 total.
        //Target Temp Variables: 102 total.
        //Output Variables: 15 total.
        //Computations: 1.452991452991453 average, 170 total.
        //Memory Reads: 2.0256410256410255 average, 237 total.
        //Memory Writes: 117 total.
        //
        //MetaContext Binding Data: 
        //   0 = constant: '0'
        //   1 = constant: '1'
        //   -1 = constant: '-1'
        //   2 = constant: '2'
        //   -2 = constant: '-2'
        //   Rational[1, 2] = constant: 'Rational[1, 2]'
        //   Rational[-1, 2] = constant: 'Rational[-1, 2]'
        //   iaR0C0 = parameter: _oaR0C0
        //   iaR1C0 = parameter: _oaR1C0
        //   iaR2C0 = parameter: _oaR2C0
        //   iaR1C1 = parameter: _oaR1C1
        //   iaR2C1 = parameter: _oaR2C1
        //   iaR2C2 = parameter: _oaR2C2
        //   ivR0C0 = parameter: _ovR0C0
        //   ivR1C0 = parameter: _ovR1C0
        //   ivR2C0 = parameter: _ovR2C0
        //   ivR0C1 = parameter: _ovR0C1
        //   ivR1C1 = parameter: _ovR1C1
        //   ivR2C1 = parameter: _ovR2C1
        //   ivR0C2 = parameter: _ovR0C2
        //   ivR1C2 = parameter: _ovR1C2
        //   ivR2C2 = parameter: _ovR2C2

        var temp0 = -_oaR0C0;
        var temp1 = _oaR1C1 + temp0;
        var temp2 = 0.5 * 1 / _oaR1C0 * temp1;
        var temp3 = Math.Sqrt(1 + temp2 * temp2) + Math.Abs(temp2);
        var temp4 = 1 / temp3;
        var temp5 = _oaR1C0 * temp4;
        var temp6 = BinaryStep(temp2);
        var temp7 = temp5 * temp6;
        var temp8 = -temp7;
        var temp9 = _oaR0C0 + temp8;
        var temp10 = Math.Pow(temp3, -2) * temp6 * temp6;
        var temp11 = 1 / Math.Sqrt(1 + temp10);
        var temp12 = _oaR2C1 * temp4;
        var temp13 = temp6 * temp12;
        var temp14 = temp11 * temp13;
        var temp15 = _oaR2C0 * temp11 + -temp14;
        var temp16 = _oaR2C2 + temp0;
        var temp17 = temp7 + temp16;
        var temp18 = 0.5 * 1 / temp15 * temp17;
        var temp19 = Math.Sqrt(1 + temp18 * temp18) + Math.Abs(temp18);
        var temp20 = 1 / temp19;
        var temp21 = temp15 * temp20;
        var temp22 = BinaryStep(temp18);
        var temp23 = temp21 * temp22;
        var oaR0C0 = temp9 + -temp23;

        var oaR2C1 = 0;

        var temp26 = _ovR0C1 * temp4;
        var temp27 = temp6 * temp26;
        var temp28 = temp11 * temp27;
        var temp29 = _ovR0C0 * temp11 + -temp28;
        var temp30 = Math.Pow(temp19, -2) * temp22 * temp22;
        var temp31 = 1 / Math.Sqrt(1 + temp30);
        var temp32 = _ovR0C2 * temp20;
        var temp33 = temp22 * temp32;
        var temp34 = temp31 * temp33;
        var ovR0C0 = temp29 * temp31 + -temp34;

        var temp36 = _ovR1C1 * temp4;
        var temp37 = temp6 * temp36;
        var temp38 = temp11 * temp37;
        var temp39 = _ovR1C0 * temp11 + -temp38;
        var temp40 = _ovR1C2 * temp20;
        var temp41 = temp22 * temp40;
        var temp42 = temp31 * temp41;
        var ovR1C0 = temp31 * temp39 + -temp42;

        var temp44 = _ovR2C1 * temp4;
        var temp45 = temp6 * temp44;
        var temp46 = temp11 * temp45;
        var temp47 = _ovR2C0 * temp11 + -temp46;
        var temp48 = _ovR2C2 * temp20;
        var temp49 = temp22 * temp48;
        var temp50 = temp31 * temp49;
        var ovR2C0 = temp31 * temp47 + -temp50;

        var temp52 = _oaR1C1 + temp7;
        var temp53 = _oaR2C0 * temp4;
        var temp54 = temp6 * temp53;
        var temp55 = temp11 * (_oaR2C1 + temp54);
        var temp56 = temp31 * temp55;
        var temp57 = _oaR2C2 + temp23;
        var temp58 = -_oaR1C1 + temp8;
        var temp59 = temp57 + temp58;
        var temp60 = 0.5 * 1 / temp56 * temp59;
        var temp61 = Math.Sqrt(1 + temp60 * temp60) + Math.Abs(temp60);
        var temp62 = 1 / temp61;
        var temp63 = temp56 * temp62;
        var temp64 = BinaryStep(temp60);
        var temp65 = temp63 * temp64;
        var oaR1C1 = temp52 + -temp65;

        var oaR2C2 = temp57 + temp65;

        var temp68 = temp20 * temp55;
        var temp69 = temp22 * temp68;
        var temp70 = temp31 * temp69;
        var temp71 = -temp70;
        var temp72 = Math.Pow(temp61, -2) * temp64 * temp64;
        var temp73 = 1 / Math.Sqrt(1 + temp72);
        var oaR1C0 = temp71 * temp73;

        var temp75 = temp62 * temp71;
        var temp76 = temp64 * temp75;
        var oaR2C0 = temp73 * temp76;

        var temp78 = _ovR0C0 * temp4;
        var temp79 = temp6 * temp78;
        var temp80 = temp11 * (_ovR0C1 + temp79);
        var temp81 = temp20 * temp29;
        var temp82 = temp22 * temp81;
        var temp83 = temp31 * (_ovR0C2 + temp82);
        var temp84 = temp62 * temp83;
        var temp85 = temp64 * temp84;
        var temp86 = temp73 * temp85;
        var ovR0C1 = temp73 * temp80 + -temp86;

        var temp88 = temp62 * temp80;
        var temp89 = temp64 * temp88;
        var ovR0C2 = temp73 * (temp83 + temp89);

        var temp91 = _ovR1C0 * temp4;
        var temp92 = temp6 * temp91;
        var temp93 = temp11 * (_ovR1C1 + temp92);
        var temp94 = temp20 * temp39;
        var temp95 = temp22 * temp94;
        var temp96 = temp31 * (_ovR1C2 + temp95);
        var temp97 = temp62 * temp96;
        var temp98 = temp64 * temp97;
        var temp99 = temp73 * temp98;
        var ovR1C1 = temp73 * temp93 + -temp99;

        var temp101 = temp62 * temp93;
        var temp102 = temp64 * temp101;
        var ovR1C2 = temp73 * (temp96 + temp102);

        var temp104 = _ovR2C0 * temp4;
        var temp105 = temp6 * temp104;
        var temp106 = temp11 * (_ovR2C1 + temp105);
        var temp107 = temp20 * temp47;
        var temp108 = temp22 * temp107;
        var temp109 = temp31 * (_ovR2C2 + temp108);
        var temp110 = temp62 * temp109;
        var temp111 = temp64 * temp110;
        var temp112 = temp73 * temp111;
        var ovR2C1 = temp73 * temp106 + -temp112;

        var temp114 = temp62 * temp106;
        var temp115 = temp64 * temp114;
        var ovR2C2 = temp73 * (temp109 + temp115);

        //Finish GA-FuL MetaContext Code Generation, 2025-07-05T04:05:18.6009637+03:00

        _oaR0C0 = oaR0C0;
        _oaR1C0 = oaR1C0;
        _oaR2C0 = oaR2C0;
        _oaR1C1 = oaR1C1;
        _oaR2C1 = oaR2C1;
        _oaR2C2 = oaR2C2;

        _ovR0C0 = ovR0C0;
        _ovR1C0 = ovR1C0;
        _ovR2C0 = ovR2C0;
        _ovR0C1 = ovR0C1;
        _ovR1C1 = ovR1C1;
        _ovR2C1 = ovR2C1;
        _ovR0C2 = ovR0C2;
        _ovR1C2 = ovR1C2;
        _ovR2C2 = ovR2C2;
    }

    private void Rotate4()
    {
        //Begin GA-FuL MetaContext Code Generation, 2025-07-05T04:05:23.0209987+03:00
        //MetaContext: JacobiSymmetricEigenDecomposer4x4
        //Input Variables: 26 used, 0 not used, 26 total.
        //Temp Variables: 295 sub-expressions, 0 generated temps, 295 total.
        //Target Temp Variables: 295 total.
        //Output Variables: 26 total.
        //Computations: 1.4299065420560748 average, 459 total.
        //Memory Reads: 2.087227414330218 average, 670 total.
        //Memory Writes: 321 total.
        //
        //MetaContext Binding Data: 
        //   0 = constant: '0'
        //   1 = constant: '1'
        //   -1 = constant: '-1'
        //   2 = constant: '2'
        //   -2 = constant: '-2'
        //   Rational[1, 2] = constant: 'Rational[1, 2]'
        //   Rational[-1, 2] = constant: 'Rational[-1, 2]'
        //   iaR0C0 = parameter: _oaR0C0
        //   iaR1C0 = parameter: _oaR1C0
        //   iaR2C0 = parameter: _oaR2C0
        //   iaR3C0 = parameter: _oaR3C0
        //   iaR1C1 = parameter: _oaR1C1
        //   iaR2C1 = parameter: _oaR2C1
        //   iaR3C1 = parameter: _oaR3C1
        //   iaR2C2 = parameter: _oaR2C2
        //   iaR3C2 = parameter: _oaR3C2
        //   iaR3C3 = parameter: _oaR3C3
        //   ivR0C0 = parameter: _ovR0C0
        //   ivR1C0 = parameter: _ovR1C0
        //   ivR2C0 = parameter: _ovR2C0
        //   ivR3C0 = parameter: _ovR3C0
        //   ivR0C1 = parameter: _ovR0C1
        //   ivR1C1 = parameter: _ovR1C1
        //   ivR2C1 = parameter: _ovR2C1
        //   ivR3C1 = parameter: _ovR3C1
        //   ivR0C2 = parameter: _ovR0C2
        //   ivR1C2 = parameter: _ovR1C2
        //   ivR2C2 = parameter: _ovR2C2
        //   ivR3C2 = parameter: _ovR3C2
        //   ivR0C3 = parameter: _ovR0C3
        //   ivR1C3 = parameter: _ovR1C3
        //   ivR2C3 = parameter: _ovR2C3
        //   ivR3C3 = parameter: _ovR3C3

        var oaR3C2 = 0;

        var temp1 = -_oaR0C0;
        var temp2 = _oaR1C1 + temp1;
        var temp3 = 0.5 * 1 / _oaR1C0 * temp2;
        var temp4 = BinaryStep(temp3);
        var temp5 = Math.Sqrt(1 + temp3 * temp3) + Math.Abs(temp3);
        var temp6 = 1 / temp5;
        var temp7 = _oaR1C0 * temp6;
        var temp8 = temp4 * temp7;
        var temp9 = -temp8;
        var temp10 = _oaR0C0 + temp9;
        var temp11 = temp4 * temp4 * Math.Pow(temp5, -2);
        var temp12 = 1 / Math.Sqrt(1 + temp11);
        var temp13 = _oaR2C1 * temp6;
        var temp14 = temp4 * temp13;
        var temp15 = temp12 * temp14;
        var temp16 = _oaR2C0 * temp12 + -temp15;
        var temp17 = _oaR2C2 + temp1;
        var temp18 = temp8 + temp17;
        var temp19 = 0.5 * 1 / temp16 * temp18;
        var temp20 = BinaryStep(temp19);
        var temp21 = Math.Sqrt(1 + temp19 * temp19) + Math.Abs(temp19);
        var temp22 = 1 / temp21;
        var temp23 = temp16 * temp22;
        var temp24 = temp20 * temp23;
        var temp25 = -temp24;
        var temp26 = temp10 + temp25;
        var temp27 = _oaR3C1 * temp6;
        var temp28 = temp4 * temp27;
        var temp29 = temp12 * temp28;
        var temp30 = _oaR3C0 * temp12 + -temp29;
        var temp31 = temp20 * temp20 * Math.Pow(temp21, -2);
        var temp32 = 1 / Math.Sqrt(1 + temp31);
        var temp33 = _oaR3C2 * temp22;
        var temp34 = temp20 * temp33;
        var temp35 = temp32 * temp34;
        var temp36 = temp30 * temp32 + -temp35;
        var temp37 = _oaR3C3 + temp1;
        var temp38 = temp8 + temp37;
        var temp39 = temp24 + temp38;
        var temp40 = 0.5 * 1 / temp36 * temp39;
        var temp41 = Math.Sqrt(1 + temp40 * temp40) + Math.Abs(temp40);
        var temp42 = 1 / temp41;
        var temp43 = temp36 * temp42;
        var temp44 = BinaryStep(temp40);
        var temp45 = temp43 * temp44;
        var oaR0C0 = temp26 + -temp45;

        var temp47 = _ovR0C1 * temp6;
        var temp48 = temp4 * temp47;
        var temp49 = temp12 * temp48;
        var temp50 = _ovR0C0 * temp12 + -temp49;
        var temp51 = _ovR0C2 * temp22;
        var temp52 = temp20 * temp51;
        var temp53 = temp32 * temp52;
        var temp54 = temp32 * temp50 + -temp53;
        var temp55 = Math.Pow(temp41, -2) * temp44 * temp44;
        var temp56 = 1 / Math.Sqrt(1 + temp55);
        var temp57 = _ovR0C3 * temp42;
        var temp58 = temp44 * temp57;
        var temp59 = temp56 * temp58;
        var ovR0C0 = temp54 * temp56 + -temp59;

        var temp61 = _ovR1C1 * temp6;
        var temp62 = temp4 * temp61;
        var temp63 = temp12 * temp62;
        var temp64 = _ovR1C0 * temp12 + -temp63;
        var temp65 = _ovR1C2 * temp22;
        var temp66 = temp20 * temp65;
        var temp67 = temp32 * temp66;
        var temp68 = temp32 * temp64 + -temp67;
        var temp69 = _ovR1C3 * temp42;
        var temp70 = temp44 * temp69;
        var temp71 = temp56 * temp70;
        var ovR1C0 = temp56 * temp68 + -temp71;

        var temp73 = _ovR2C2 * temp22;
        var temp74 = temp20 * temp73;
        var temp75 = temp32 * temp74;
        var temp76 = _ovR2C1 * temp6;
        var temp77 = temp4 * temp76;
        var temp78 = temp12 * temp77;
        var temp79 = _ovR2C0 * temp12 + -temp78;
        var temp80 = -temp75 + temp32 * temp79;
        var temp81 = _ovR2C3 * temp42;
        var temp82 = temp44 * temp81;
        var temp83 = temp56 * temp82;
        var ovR2C0 = temp56 * temp80 + -temp83;

        var temp85 = _ovR3C1 * temp6;
        var temp86 = temp4 * temp85;
        var temp87 = temp12 * temp86;
        var temp88 = _ovR3C0 * temp12 + -temp87;
        var temp89 = _ovR3C2 * temp22;
        var temp90 = temp20 * temp89;
        var temp91 = temp32 * temp90;
        var temp92 = temp32 * temp88 + -temp91;
        var temp93 = _ovR3C3 * temp42;
        var temp94 = temp44 * temp93;
        var temp95 = temp56 * temp94;
        var ovR3C0 = temp56 * temp92 + -temp95;

        var temp97 = _oaR1C1 + temp8;
        var temp98 = _oaR2C0 * temp6;
        var temp99 = temp4 * temp98;
        var temp100 = temp12 * (_oaR2C1 + temp99);
        var temp101 = temp32 * temp100;
        var temp102 = _oaR2C2 + temp24;
        var temp103 = -_oaR1C1;
        var temp104 = temp9 + temp103;
        var temp105 = temp102 + temp104;
        var temp106 = 0.5 * 1 / temp101 * temp105;
        var temp107 = Math.Sqrt(1 + temp106 * temp106) + Math.Abs(temp106);
        var temp108 = 1 / temp107;
        var temp109 = temp101 * temp108;
        var temp110 = BinaryStep(temp106);
        var temp111 = temp109 * temp110;
        var temp112 = -temp111;
        var temp113 = temp97 + temp112;
        var temp114 = _oaR3C0 * temp6;
        var temp115 = temp4 * temp114;
        var temp116 = temp12 * (_oaR3C1 + temp115);
        var temp117 = temp22 * temp100;
        var temp118 = temp20 * temp117;
        var temp119 = temp32 * temp118;
        var temp120 = -temp119;
        var temp121 = temp42 * temp120;
        var temp122 = temp44 * temp121;
        var temp123 = temp56 * (temp116 + temp122);
        var temp124 = Math.Pow(temp107, -2) * temp110 * temp110;
        var temp125 = 1 / Math.Sqrt(1 + temp124);
        var temp126 = temp22 * temp30;
        var temp127 = temp20 * temp126;
        var temp128 = temp32 * (_oaR3C2 + temp127);
        var temp129 = temp128 * temp56;
        var temp130 = temp129 * temp108;
        var temp131 = temp130 * temp110;
        var temp132 = temp131 * temp125;
        var temp133 = -temp132 + temp123 * temp125;
        var temp134 = _oaR3C3 + temp103;
        var temp135 = temp134 + temp9;
        var temp136 = temp135 + temp45;
        var temp137 = temp136 + temp111;
        var temp138 = 0.5 * 1 / temp133 * temp137;
        var temp139 = Math.Sqrt(1 + temp138 * temp138) + Math.Abs(temp138);
        var temp140 = 1 / temp139;
        var temp141 = temp133 * temp140;
        var temp142 = BinaryStep(temp138);
        var temp143 = temp141 * temp142;
        var oaR1C1 = -temp143 + temp113;

        var temp145 = temp42 * temp116;
        var temp146 = temp145 * temp44;
        var temp147 = temp146 * temp56;
        var temp148 = -temp147 + temp56 * temp120;
        var temp149 = temp128 * temp42;
        var temp150 = temp149 * temp44;
        var temp151 = temp150 * temp56;
        var temp152 = -temp151;
        var temp153 = temp152 * temp108;
        var temp154 = temp153 * temp110;
        var temp155 = temp154 * temp125;
        var temp156 = -temp155 + temp148 * temp125;
        var temp157 = Math.Pow(temp139, -2) * temp142 * temp142;
        var temp158 = 1 / Math.Sqrt(1 + temp157);
        var oaR1C0 = temp156 * temp158;

        var temp160 = _ovR0C0 * temp6;
        var temp161 = temp160 * temp4;
        var temp162 = (_ovR0C1 + temp161) * temp12;
        var temp163 = temp22 * temp50;
        var temp164 = temp163 * temp20;
        var temp165 = (_ovR0C2 + temp164) * temp32;
        var temp166 = temp165 * temp108;
        var temp167 = temp166 * temp110;
        var temp168 = temp167 * temp125;
        var temp169 = -temp168 + temp162 * temp125;
        var temp170 = temp42 * temp54;
        var temp171 = temp170 * temp44;
        var temp172 = (_ovR0C3 + temp171) * temp56;
        var temp173 = temp140 * temp172;
        var temp174 = temp142 * temp173;
        var temp175 = temp158 * temp174;
        var ovR0C1 = temp158 * temp169 + -temp175;

        var temp177 = _ovR1C0 * temp6;
        var temp178 = temp177 * temp4;
        var temp179 = (_ovR1C1 + temp178) * temp12;
        var temp180 = temp22 * temp64;
        var temp181 = temp180 * temp20;
        var temp182 = (_ovR1C2 + temp181) * temp32;
        var temp183 = temp182 * temp108;
        var temp184 = temp183 * temp110;
        var temp185 = temp184 * temp125;
        var temp186 = -temp185 + temp179 * temp125;
        var temp187 = temp42 * temp68;
        var temp188 = temp187 * temp44;
        var temp189 = (_ovR1C3 + temp188) * temp56;
        var temp190 = temp140 * temp189;
        var temp191 = temp142 * temp190;
        var temp192 = temp158 * temp191;
        var ovR1C1 = temp158 * temp186 + -temp192;

        var temp194 = _ovR2C0 * temp6;
        var temp195 = temp194 * temp4;
        var temp196 = (_ovR2C1 + temp195) * temp12;
        var temp197 = temp22 * temp79;
        var temp198 = temp197 * temp20;
        var temp199 = (_ovR2C2 + temp198) * temp32;
        var temp200 = temp199 * temp108;
        var temp201 = temp200 * temp110;
        var temp202 = temp201 * temp125;
        var temp203 = -temp202 + temp196 * temp125;
        var temp204 = temp42 * temp80;
        var temp205 = temp204 * temp44;
        var temp206 = (_ovR2C3 + temp205) * temp56;
        var temp207 = temp140 * temp206;
        var temp208 = temp142 * temp207;
        var temp209 = temp158 * temp208;
        var ovR2C1 = temp158 * temp203 + -temp209;

        var temp211 = _ovR3C0 * temp6;
        var temp212 = temp211 * temp4;
        var temp213 = (_ovR3C1 + temp212) * temp12;
        var temp214 = temp22 * temp88;
        var temp215 = temp214 * temp20;
        var temp216 = (_ovR3C2 + temp215) * temp32;
        var temp217 = temp216 * temp108;
        var temp218 = temp217 * temp110;
        var temp219 = temp218 * temp125;
        var temp220 = -temp219 + temp213 * temp125;
        var temp221 = temp42 * temp92;
        var temp222 = temp221 * temp44;
        var temp223 = (_ovR3C3 + temp222) * temp56;
        var temp224 = temp140 * temp223;
        var temp225 = temp142 * temp224;
        var temp226 = temp158 * temp225;
        var ovR3C1 = temp158 * temp220 + -temp226;

        var temp228 = temp102 + temp111;
        var temp229 = temp108 * temp123;
        var temp230 = temp229 * temp110;
        var temp231 = (temp129 + temp230) * temp125;
        var temp232 = temp158 * temp231;
        var temp233 = _oaR3C3 + temp45;
        var temp234 = temp143 + temp233;
        var temp235 = -_oaR2C2 + temp25;
        var temp236 = temp235 + temp112;
        var temp237 = temp234 + temp236;
        var temp238 = 0.5 * 1 / temp232 * temp237;
        var temp239 = Math.Sqrt(1 + temp238 * temp238) + Math.Abs(temp238);
        var temp240 = 1 / temp239;
        var temp241 = temp232 * temp240;
        var temp242 = BinaryStep(temp238);
        var temp243 = temp241 * temp242;
        var oaR2C2 = temp228 + -temp243;

        var oaR3C3 = temp234 + temp243;

        var temp246 = temp148 * temp108;
        var temp247 = temp246 * temp110;
        var temp248 = (temp152 + temp247) * temp125;
        var temp249 = Math.Pow(temp239, -2) * temp242 * temp242;
        var temp250 = 1 / Math.Sqrt(1 + temp249);
        var temp251 = temp140 * temp156;
        var temp252 = temp142 * temp251;
        var temp253 = temp158 * temp252;
        var temp254 = temp240 * temp253;
        var temp255 = temp242 * temp254;
        var temp256 = temp250 * temp255;
        var oaR2C0 = temp248 * temp250 + -temp256;

        var temp258 = temp240 * temp248;
        var temp259 = temp242 * temp258;
        var oaR3C0 = temp250 * (temp253 + temp259);

        var temp261 = temp140 * temp231;
        var temp262 = temp142 * temp261;
        var temp263 = temp158 * temp262;
        var temp264 = -temp263;
        var oaR2C1 = temp250 * temp264;

        var temp266 = temp240 * temp264;
        var temp267 = temp242 * temp266;
        var oaR3C1 = temp250 * temp267;

        var temp269 = temp162 * temp108;
        var temp270 = temp269 * temp110;
        var temp271 = (temp165 + temp270) * temp125;
        var temp272 = temp140 * temp169;
        var temp273 = temp142 * temp272;
        var temp274 = temp158 * (temp172 + temp273);
        var temp275 = temp240 * temp274;
        var temp276 = temp242 * temp275;
        var temp277 = temp250 * temp276;
        var ovR0C2 = temp250 * temp271 + -temp277;

        var temp279 = temp240 * temp271;
        var temp280 = temp242 * temp279;
        var ovR0C3 = temp250 * (temp274 + temp280);

        var temp282 = temp179 * temp108;
        var temp283 = temp282 * temp110;
        var temp284 = (temp182 + temp283) * temp125;
        var temp285 = temp140 * temp186;
        var temp286 = temp142 * temp285;
        var temp287 = temp158 * (temp189 + temp286);
        var temp288 = temp240 * temp287;
        var temp289 = temp242 * temp288;
        var temp290 = temp250 * temp289;
        var ovR1C2 = temp250 * temp284 + -temp290;

        var temp292 = temp240 * temp284;
        var temp293 = temp242 * temp292;
        var ovR1C3 = temp250 * (temp287 + temp293);

        var temp295 = temp196 * temp108;
        var temp296 = temp295 * temp110;
        var temp297 = (temp199 + temp296) * temp125;
        var temp298 = temp140 * temp203;
        var temp299 = temp142 * temp298;
        var temp300 = temp158 * (temp206 + temp299);
        var temp301 = temp240 * temp300;
        var temp302 = temp242 * temp301;
        var temp303 = temp250 * temp302;
        var ovR2C2 = temp250 * temp297 + -temp303;

        var temp305 = temp240 * temp297;
        var temp306 = temp242 * temp305;
        var ovR2C3 = temp250 * (temp300 + temp306);

        var temp308 = temp213 * temp108;
        var temp309 = temp308 * temp110;
        var temp310 = (temp216 + temp309) * temp125;
        var temp311 = temp140 * temp220;
        var temp312 = temp142 * temp311;
        var temp313 = temp158 * (temp223 + temp312);
        var temp314 = temp240 * temp313;
        var temp315 = temp242 * temp314;
        var temp316 = temp250 * temp315;
        var ovR3C2 = temp250 * temp310 + -temp316;

        var temp318 = temp240 * temp310;
        var temp319 = temp242 * temp318;
        var ovR3C3 = temp250 * (temp313 + temp319);

        //Finish GA-FuL MetaContext Code Generation, 2025-07-05T04:05:23.0220624+03:00

        _oaR0C0 = oaR0C0;
        _oaR1C0 = oaR1C0;
        _oaR2C0 = oaR2C0;
        _oaR3C0 = oaR3C0;
        _oaR1C1 = oaR1C1;
        _oaR2C1 = oaR2C1;
        _oaR3C1 = oaR3C1;
        _oaR2C2 = oaR2C2;
        _oaR3C2 = oaR3C2;
        _oaR3C3 = oaR3C3;

        _ovR0C0 = ovR0C0;
        _ovR1C0 = ovR1C0;
        _ovR2C0 = ovR2C0;
        _ovR3C0 = ovR3C0;
        _ovR0C1 = ovR0C1;
        _ovR1C1 = ovR1C1;
        _ovR2C1 = ovR2C1;
        _ovR3C1 = ovR3C1;
        _ovR0C2 = ovR0C2;
        _ovR1C2 = ovR1C2;
        _ovR2C2 = ovR2C2;
        _ovR3C2 = ovR3C2;
        _ovR0C3 = ovR0C3;
        _ovR1C3 = ovR1C3;
        _ovR2C3 = ovR2C3;
        _ovR3C3 = ovR3C3;
    }

    private void Rotate5()
    {
        //Begin GA-FuL MetaContext Code Generation, 2025-07-05T04:05:32.8182562+03:00
        //MetaContext: JacobiSymmetricEigenDecomposer5x5
        //Input Variables: 40 used, 0 not used, 40 total.
        //Temp Variables: 647 sub-expressions, 0 generated temps, 647 total.
        //Target Temp Variables: 647 total.
        //Output Variables: 40 total.
        //Computations: 1.4177583697234353 average, 974 total.
        //Memory Reads: 2.12372634643377 average, 1459 total.
        //Memory Writes: 687 total.
        //
        //MetaContext Binding Data: 
        //   0 = constant: '0'
        //   1 = constant: '1'
        //   -1 = constant: '-1'
        //   2 = constant: '2'
        //   -2 = constant: '-2'
        //   Rational[1, 2] = constant: 'Rational[1, 2]'
        //   Rational[-1, 2] = constant: 'Rational[-1, 2]'
        //   iaR0C0 = parameter: _oaR0C0
        //   iaR1C0 = parameter: _oaR1C0
        //   iaR2C0 = parameter: _oaR2C0
        //   iaR3C0 = parameter: _oaR3C0
        //   iaR4C0 = parameter: _oaR4C0
        //   iaR1C1 = parameter: _oaR1C1
        //   iaR2C1 = parameter: _oaR2C1
        //   iaR3C1 = parameter: _oaR3C1
        //   iaR4C1 = parameter: _oaR4C1
        //   iaR2C2 = parameter: _oaR2C2
        //   iaR3C2 = parameter: _oaR3C2
        //   iaR4C2 = parameter: _oaR4C2
        //   iaR3C3 = parameter: _oaR3C3
        //   iaR4C3 = parameter: _oaR4C3
        //   iaR4C4 = parameter: _oaR4C4
        //   ivR0C0 = parameter: _ovR0C0
        //   ivR1C0 = parameter: _ovR1C0
        //   ivR2C0 = parameter: _ovR2C0
        //   ivR3C0 = parameter: _ovR3C0
        //   ivR4C0 = parameter: _ovR4C0
        //   ivR0C1 = parameter: _ovR0C1
        //   ivR1C1 = parameter: _ovR1C1
        //   ivR2C1 = parameter: _ovR2C1
        //   ivR3C1 = parameter: _ovR3C1
        //   ivR4C1 = parameter: _ovR4C1
        //   ivR0C2 = parameter: _ovR0C2
        //   ivR1C2 = parameter: _ovR1C2
        //   ivR2C2 = parameter: _ovR2C2
        //   ivR3C2 = parameter: _ovR3C2
        //   ivR4C2 = parameter: _ovR4C2
        //   ivR0C3 = parameter: _ovR0C3
        //   ivR1C3 = parameter: _ovR1C3
        //   ivR2C3 = parameter: _ovR2C3
        //   ivR3C3 = parameter: _ovR3C3
        //   ivR4C3 = parameter: _ovR4C3
        //   ivR0C4 = parameter: _ovR0C4
        //   ivR1C4 = parameter: _ovR1C4
        //   ivR2C4 = parameter: _ovR2C4
        //   ivR3C4 = parameter: _ovR3C4
        //   ivR4C4 = parameter: _ovR4C4

        var oaR4C3 = 0;

        var temp1 = -_oaR0C0;
        var temp2 = _oaR1C1 + temp1;
        var temp3 = 0.5 * 1 / _oaR1C0 * temp2;
        var temp4 = BinaryStep(temp3);
        var temp5 = Math.Sqrt(1 + temp3 * temp3) + Math.Abs(temp3);
        var temp6 = 1 / temp5;
        var temp7 = _oaR1C0 * temp6;
        var temp8 = temp4 * temp7;
        var temp9 = -temp8;
        var temp10 = _oaR0C0 + temp9;
        var temp11 = temp4 * temp4 * Math.Pow(temp5, -2);
        var temp12 = 1 / Math.Sqrt(1 + temp11);
        var temp13 = _oaR2C1 * temp6;
        var temp14 = temp4 * temp13;
        var temp15 = temp12 * temp14;
        var temp16 = _oaR2C0 * temp12 + -temp15;
        var temp17 = _oaR2C2 + temp1;
        var temp18 = temp8 + temp17;
        var temp19 = 0.5 * 1 / temp16 * temp18;
        var temp20 = BinaryStep(temp19);
        var temp21 = Math.Sqrt(1 + temp19 * temp19) + Math.Abs(temp19);
        var temp22 = 1 / temp21;
        var temp23 = temp16 * temp22;
        var temp24 = temp20 * temp23;
        var temp25 = -temp24;
        var temp26 = temp10 + temp25;
        var temp27 = _oaR3C1 * temp6;
        var temp28 = temp4 * temp27;
        var temp29 = temp12 * temp28;
        var temp30 = _oaR3C0 * temp12 + -temp29;
        var temp31 = temp20 * temp20 * Math.Pow(temp21, -2);
        var temp32 = 1 / Math.Sqrt(1 + temp31);
        var temp33 = _oaR3C2 * temp22;
        var temp34 = temp20 * temp33;
        var temp35 = temp32 * temp34;
        var temp36 = temp30 * temp32 + -temp35;
        var temp37 = _oaR3C3 + temp1;
        var temp38 = temp8 + temp37;
        var temp39 = temp24 + temp38;
        var temp40 = 0.5 * 1 / temp36 * temp39;
        var temp41 = Math.Sqrt(1 + temp40 * temp40) + Math.Abs(temp40);
        var temp42 = 1 / temp41;
        var temp43 = temp36 * temp42;
        var temp44 = BinaryStep(temp40);
        var temp45 = temp43 * temp44;
        var temp46 = -temp45;
        var temp47 = temp26 + temp46;
        var temp48 = _oaR4C2 * temp22;
        var temp49 = temp20 * temp48;
        var temp50 = temp32 * temp49;
        var temp51 = _oaR4C1 * temp6;
        var temp52 = temp4 * temp51;
        var temp53 = temp12 * temp52;
        var temp54 = _oaR4C0 * temp12 + -temp53;
        var temp55 = -temp50 + temp32 * temp54;
        var temp56 = Math.Pow(temp41, -2) * temp44 * temp44;
        var temp57 = 1 / Math.Sqrt(1 + temp56);
        var temp58 = _oaR4C3 * temp42;
        var temp59 = temp44 * temp58;
        var temp60 = temp57 * temp59;
        var temp61 = temp55 * temp57 + -temp60;
        var temp62 = _oaR4C4 + temp1;
        var temp63 = temp8 + temp62;
        var temp64 = temp24 + temp63;
        var temp65 = temp45 + temp64;
        var temp66 = 0.5 * 1 / temp61 * temp65;
        var temp67 = Math.Sqrt(1 + temp66 * temp66) + Math.Abs(temp66);
        var temp68 = 1 / temp67;
        var temp69 = temp61 * temp68;
        var temp70 = BinaryStep(temp66);
        var temp71 = temp69 * temp70;
        var oaR0C0 = temp47 + -temp71;

        var temp73 = _ovR0C1 * temp6;
        var temp74 = temp4 * temp73;
        var temp75 = temp12 * temp74;
        var temp76 = _ovR0C0 * temp12 + -temp75;
        var temp77 = _ovR0C2 * temp22;
        var temp78 = temp20 * temp77;
        var temp79 = temp32 * temp78;
        var temp80 = temp32 * temp76 + -temp79;
        var temp81 = _ovR0C3 * temp42;
        var temp82 = temp44 * temp81;
        var temp83 = temp57 * temp82;
        var temp84 = temp57 * temp80 + -temp83;
        var temp85 = Math.Pow(temp67, -2) * temp70 * temp70;
        var temp86 = 1 / Math.Sqrt(1 + temp85);
        var temp87 = _ovR0C4 * temp68;
        var temp88 = temp70 * temp87;
        var temp89 = temp86 * temp88;
        var ovR0C0 = temp84 * temp86 + -temp89;

        var temp91 = _ovR1C1 * temp6;
        var temp92 = temp4 * temp91;
        var temp93 = temp12 * temp92;
        var temp94 = _ovR1C0 * temp12 + -temp93;
        var temp95 = _ovR1C2 * temp22;
        var temp96 = temp20 * temp95;
        var temp97 = temp32 * temp96;
        var temp98 = temp32 * temp94 + -temp97;
        var temp99 = _ovR1C3 * temp42;
        var temp100 = temp44 * temp99;
        var temp101 = temp57 * temp100;
        var temp102 = temp57 * temp98 + -temp101;
        var temp103 = _ovR1C4 * temp68;
        var temp104 = temp70 * temp103;
        var temp105 = temp86 * temp104;
        var ovR1C0 = temp86 * temp102 + -temp105;

        var temp107 = _ovR2C1 * temp6;
        var temp108 = temp4 * temp107;
        var temp109 = temp12 * temp108;
        var temp110 = _ovR2C0 * temp12 + -temp109;
        var temp111 = _ovR2C2 * temp22;
        var temp112 = temp20 * temp111;
        var temp113 = temp32 * temp112;
        var temp114 = temp32 * temp110 + -temp113;
        var temp115 = _ovR2C3 * temp42;
        var temp116 = temp44 * temp115;
        var temp117 = temp57 * temp116;
        var temp118 = temp57 * temp114 + -temp117;
        var temp119 = _ovR2C4 * temp68;
        var temp120 = temp70 * temp119;
        var temp121 = temp86 * temp120;
        var ovR2C0 = temp86 * temp118 + -temp121;

        var temp123 = _ovR3C1 * temp6;
        var temp124 = temp4 * temp123;
        var temp125 = temp12 * temp124;
        var temp126 = _ovR3C0 * temp12 + -temp125;
        var temp127 = _ovR3C2 * temp22;
        var temp128 = temp20 * temp127;
        var temp129 = temp32 * temp128;
        var temp130 = temp32 * temp126 + -temp129;
        var temp131 = _ovR3C3 * temp42;
        var temp132 = temp44 * temp131;
        var temp133 = temp57 * temp132;
        var temp134 = temp57 * temp130 + -temp133;
        var temp135 = _ovR3C4 * temp68;
        var temp136 = temp70 * temp135;
        var temp137 = temp86 * temp136;
        var ovR3C0 = temp86 * temp134 + -temp137;

        var temp139 = _ovR4C1 * temp6;
        var temp140 = temp4 * temp139;
        var temp141 = temp12 * temp140;
        var temp142 = _ovR4C0 * temp12 + -temp141;
        var temp143 = _ovR4C2 * temp22;
        var temp144 = temp20 * temp143;
        var temp145 = temp32 * temp144;
        var temp146 = temp32 * temp142 + -temp145;
        var temp147 = _ovR4C3 * temp42;
        var temp148 = temp44 * temp147;
        var temp149 = temp57 * temp148;
        var temp150 = temp57 * temp146 + -temp149;
        var temp151 = _ovR4C4 * temp68;
        var temp152 = temp70 * temp151;
        var temp153 = temp86 * temp152;
        var ovR4C0 = temp86 * temp150 + -temp153;

        var temp155 = temp22 * temp30;
        var temp156 = temp20 * temp155;
        var temp157 = temp32 * (_oaR3C2 + temp156);
        var temp158 = temp42 * temp157;
        var temp159 = temp44 * temp158;
        var temp160 = temp57 * temp159;
        var temp161 = -temp160;
        var temp162 = temp68 * temp161;
        var temp163 = temp70 * temp162;
        var temp164 = temp22 * temp54;
        var temp165 = temp20 * temp164;
        var temp166 = temp32 * (_oaR4C2 + temp165);
        var temp167 = temp86 * (temp163 + temp166);
        var temp168 = _oaR2C2 + temp24;
        var temp169 = -_oaR1C1;
        var temp170 = temp9 + temp169;
        var temp171 = temp168 + temp170;
        var temp172 = _oaR2C0 * temp6;
        var temp173 = temp4 * temp172;
        var temp174 = temp12 * (_oaR2C1 + temp173);
        var temp175 = temp32 * temp174;
        var temp176 = 0.5 * temp171 * 1 / temp175;
        var temp177 = Math.Sqrt(1 + temp176 * temp176) + Math.Abs(temp176);
        var temp178 = 1 / temp177;
        var temp179 = temp167 * temp178;
        var temp180 = BinaryStep(temp176);
        var temp181 = temp179 * temp180;
        var temp182 = Math.Pow(temp177, -2) * temp180 * temp180;
        var temp183 = 1 / Math.Sqrt(1 + temp182);
        var temp184 = temp181 * temp183;
        var temp185 = _oaR4C0 * temp6;
        var temp186 = temp4 * temp185;
        var temp187 = temp12 * (_oaR4C1 + temp186);
        var temp188 = temp22 * temp174;
        var temp189 = temp20 * temp188;
        var temp190 = temp32 * temp189;
        var temp191 = -temp190;
        var temp192 = _oaR3C0 * temp6;
        var temp193 = temp4 * temp192;
        var temp194 = temp12 * (_oaR3C1 + temp193);
        var temp195 = temp42 * temp194;
        var temp196 = temp44 * temp195;
        var temp197 = temp57 * temp196;
        var temp198 = temp57 * temp191 + -temp197;
        var temp199 = temp68 * temp198;
        var temp200 = temp70 * temp199;
        var temp201 = temp86 * (temp187 + temp200);
        var temp202 = -temp184 + temp183 * temp201;
        var temp203 = temp42 * temp191;
        var temp204 = temp44 * temp203;
        var temp205 = temp57 * (temp194 + temp204);
        var temp206 = temp57 * temp157;
        var temp207 = temp178 * temp206;
        var temp208 = temp180 * temp207;
        var temp209 = temp183 * temp208;
        var temp210 = temp183 * temp205 + -temp209;
        var temp211 = _oaR3C3 + temp169;
        var temp212 = temp9 + temp211;
        var temp213 = temp45 + temp212;
        var temp214 = temp175 * temp178;
        var temp215 = temp180 * temp214;
        var temp216 = temp213 + temp215;
        var temp217 = 0.5 * 1 / temp210 * temp216;
        var temp218 = Math.Sqrt(1 + temp217 * temp217) + Math.Abs(temp217);
        var temp219 = BinaryStep(temp217);
        var temp220 = Math.Pow(temp218, -2) * temp219 * temp219;
        var temp221 = 1 / Math.Sqrt(1 + temp220);
        var temp222 = temp42 * temp55;
        var temp223 = temp44 * temp222;
        var temp224 = temp57 * (_oaR4C3 + temp223);
        var temp225 = temp86 * temp224;
        var temp226 = 1 / temp218;
        var temp227 = temp225 * temp226;
        var temp228 = temp219 * temp227;
        var temp229 = temp221 * temp228;
        var temp230 = temp202 * temp221 + -temp229;
        var temp231 = _oaR4C4 + temp169;
        var temp232 = temp9 + temp231;
        var temp233 = temp71 + temp232;
        var temp234 = temp215 + temp233;
        var temp235 = temp210 * temp226;
        var temp236 = temp219 * temp235;
        var temp237 = temp234 + temp236;
        var temp238 = 0.5 * 1 / temp230 * temp237;
        var temp239 = Math.Sqrt(1 + temp238 * temp238) + Math.Abs(temp238);
        var temp240 = 1 / temp239;
        var temp241 = temp230 * temp240;
        var temp242 = BinaryStep(temp238);
        var temp243 = temp241 * temp242;
        var temp244 = _oaR1C1 + -temp243;
        var temp245 = temp8 + temp244;
        var temp246 = -temp215;
        var temp247 = temp245 + temp246;
        var temp248 = -temp236;
        var oaR1C1 = temp247 + temp248;

        var temp250 = temp68 * temp187;
        var temp251 = temp70 * temp250;
        var temp252 = temp86 * temp251;
        var temp253 = temp86 * temp198 + -temp252;
        var temp254 = temp68 * temp166;
        var temp255 = temp70 * temp254;
        var temp256 = temp86 * temp255;
        var temp257 = temp86 * temp161 + -temp256;
        var temp258 = temp178 * temp257;
        var temp259 = temp180 * temp258;
        var temp260 = temp183 * temp259;
        var temp261 = temp183 * temp253 + -temp260;
        var temp262 = temp68 * temp224;
        var temp263 = temp70 * temp262;
        var temp264 = temp86 * temp263;
        var temp265 = -temp264;
        var temp266 = temp226 * temp265;
        var temp267 = temp219 * temp266;
        var temp268 = temp221 * temp267;
        var temp269 = temp221 * temp261 + -temp268;
        var temp270 = Math.Pow(temp239, -2) * temp242 * temp242;
        var temp271 = 1 / Math.Sqrt(1 + temp270);
        var oaR1C0 = temp269 * temp271;

        var temp273 = _ovR0C0 * temp6;
        var temp274 = temp4 * temp273;
        var temp275 = temp12 * (_ovR0C1 + temp274);
        var temp276 = temp22 * temp76;
        var temp277 = temp20 * temp276;
        var temp278 = temp32 * (_ovR0C2 + temp277);
        var temp279 = temp178 * temp278;
        var temp280 = temp180 * temp279;
        var temp281 = temp183 * temp280;
        var temp282 = temp183 * temp275 + -temp281;
        var temp283 = temp42 * temp80;
        var temp284 = temp44 * temp283;
        var temp285 = temp57 * (_ovR0C3 + temp284);
        var temp286 = temp226 * temp285;
        var temp287 = temp219 * temp286;
        var temp288 = temp221 * temp287;
        var temp289 = temp221 * temp282 + -temp288;
        var temp290 = temp68 * temp84;
        var temp291 = temp70 * temp290;
        var temp292 = temp86 * (_ovR0C4 + temp291);
        var temp293 = temp240 * temp292;
        var temp294 = temp242 * temp293;
        var temp295 = temp271 * temp294;
        var ovR0C1 = temp271 * temp289 + -temp295;

        var temp297 = _ovR1C0 * temp6;
        var temp298 = temp4 * temp297;
        var temp299 = temp12 * (_ovR1C1 + temp298);
        var temp300 = temp22 * temp94;
        var temp301 = temp20 * temp300;
        var temp302 = temp32 * (_ovR1C2 + temp301);
        var temp303 = temp178 * temp302;
        var temp304 = temp180 * temp303;
        var temp305 = temp183 * temp304;
        var temp306 = temp183 * temp299 + -temp305;
        var temp307 = temp42 * temp98;
        var temp308 = temp44 * temp307;
        var temp309 = temp57 * (_ovR1C3 + temp308);
        var temp310 = temp226 * temp309;
        var temp311 = temp219 * temp310;
        var temp312 = temp221 * temp311;
        var temp313 = temp221 * temp306 + -temp312;
        var temp314 = temp68 * temp102;
        var temp315 = temp70 * temp314;
        var temp316 = temp86 * (_ovR1C4 + temp315);
        var temp317 = temp240 * temp316;
        var temp318 = temp242 * temp317;
        var temp319 = temp271 * temp318;
        var ovR1C1 = temp271 * temp313 + -temp319;

        var temp321 = _ovR2C0 * temp6;
        var temp322 = temp4 * temp321;
        var temp323 = temp12 * (_ovR2C1 + temp322);
        var temp324 = temp22 * temp110;
        var temp325 = temp20 * temp324;
        var temp326 = temp32 * (_ovR2C2 + temp325);
        var temp327 = temp178 * temp326;
        var temp328 = temp180 * temp327;
        var temp329 = temp183 * temp328;
        var temp330 = temp183 * temp323 + -temp329;
        var temp331 = temp42 * temp114;
        var temp332 = temp44 * temp331;
        var temp333 = temp57 * (_ovR2C3 + temp332);
        var temp334 = temp226 * temp333;
        var temp335 = temp219 * temp334;
        var temp336 = temp221 * temp335;
        var temp337 = temp221 * temp330 + -temp336;
        var temp338 = temp68 * temp118;
        var temp339 = temp70 * temp338;
        var temp340 = temp86 * (_ovR2C4 + temp339);
        var temp341 = temp240 * temp340;
        var temp342 = temp242 * temp341;
        var temp343 = temp271 * temp342;
        var ovR2C1 = temp271 * temp337 + -temp343;

        var temp345 = _ovR3C0 * temp6;
        var temp346 = temp4 * temp345;
        var temp347 = temp12 * (_ovR3C1 + temp346);
        var temp348 = temp22 * temp126;
        var temp349 = temp20 * temp348;
        var temp350 = temp32 * (_ovR3C2 + temp349);
        var temp351 = temp178 * temp350;
        var temp352 = temp180 * temp351;
        var temp353 = temp183 * temp352;
        var temp354 = temp183 * temp347 + -temp353;
        var temp355 = temp42 * temp130;
        var temp356 = temp44 * temp355;
        var temp357 = temp57 * (_ovR3C3 + temp356);
        var temp358 = temp226 * temp357;
        var temp359 = temp219 * temp358;
        var temp360 = temp221 * temp359;
        var temp361 = temp221 * temp354 + -temp360;
        var temp362 = temp68 * temp134;
        var temp363 = temp70 * temp362;
        var temp364 = temp86 * (_ovR3C4 + temp363);
        var temp365 = temp240 * temp364;
        var temp366 = temp242 * temp365;
        var temp367 = temp271 * temp366;
        var ovR3C1 = temp271 * temp361 + -temp367;

        var temp369 = _ovR4C0 * temp6;
        var temp370 = temp4 * temp369;
        var temp371 = temp12 * (_ovR4C1 + temp370);
        var temp372 = temp22 * temp142;
        var temp373 = temp20 * temp372;
        var temp374 = temp32 * (_ovR4C2 + temp373);
        var temp375 = temp178 * temp374;
        var temp376 = temp180 * temp375;
        var temp377 = temp183 * temp376;
        var temp378 = temp183 * temp371 + -temp377;
        var temp379 = temp42 * temp146;
        var temp380 = temp44 * temp379;
        var temp381 = temp57 * (_ovR4C3 + temp380);
        var temp382 = temp226 * temp381;
        var temp383 = temp219 * temp382;
        var temp384 = temp221 * temp383;
        var temp385 = temp221 * temp378 + -temp384;
        var temp386 = temp68 * temp150;
        var temp387 = temp70 * temp386;
        var temp388 = temp86 * (_ovR4C4 + temp387);
        var temp389 = temp240 * temp388;
        var temp390 = temp242 * temp389;
        var temp391 = temp271 * temp390;
        var ovR4C1 = temp271 * temp385 + -temp391;

        var temp393 = temp178 * temp205;
        var temp394 = temp180 * temp393;
        var temp395 = temp183 * (temp206 + temp394);
        var temp396 = temp221 * temp395;
        var temp397 = -_oaR2C2;
        var temp398 = temp25 + temp397;
        var temp399 = temp246 + temp398;
        var temp400 = _oaR3C3 + temp399;
        var temp401 = temp45 + temp400;
        var temp402 = temp236 + temp401;
        var temp403 = 0.5 * 1 / temp396 * temp402;
        var temp404 = Math.Sqrt(1 + temp403 * temp403) + Math.Abs(temp403);
        var temp405 = 1 / temp404;
        var temp406 = temp396 * temp405;
        var temp407 = BinaryStep(temp403);
        var temp408 = temp406 * temp407;
        var temp409 = -temp408;
        var temp410 = _oaR2C2 + temp409;
        var temp411 = temp178 * temp201;
        var temp412 = temp180 * temp411;
        var temp413 = temp183 * (temp167 + temp412);
        var temp414 = temp226 * temp395;
        var temp415 = temp219 * temp414;
        var temp416 = temp221 * temp415;
        var temp417 = -temp416;
        var temp418 = temp240 * temp417;
        var temp419 = temp242 * temp418;
        var temp420 = temp271 * (temp413 + temp419);
        var temp421 = Math.Pow(temp404, -2) * temp407 * temp407;
        var temp422 = 1 / Math.Sqrt(1 + temp421);
        var temp423 = temp202 * temp226;
        var temp424 = temp219 * temp423;
        var temp425 = temp221 * (temp225 + temp424);
        var temp426 = temp271 * temp425;
        var temp427 = temp405 * temp426;
        var temp428 = temp407 * temp427;
        var temp429 = temp422 * temp428;
        var temp430 = temp420 * temp422 + -temp429;
        var temp431 = _oaR4C4 + temp397;
        var temp432 = temp243 + temp431;
        var temp433 = temp408 + temp432;
        var temp434 = temp25 + temp433;
        var temp435 = temp71 + temp434;
        var temp436 = temp246 + temp435;
        var temp437 = 0.5 * 1 / temp430 * temp436;
        var temp438 = Math.Sqrt(1 + temp437 * temp437) + Math.Abs(temp437);
        var temp439 = 1 / temp438;
        var temp440 = temp430 * temp439;
        var temp441 = BinaryStep(temp437);
        var temp442 = temp440 * temp441;
        var temp443 = temp410 + -temp442;
        var temp444 = temp24 + temp443;
        var oaR2C2 = temp215 + temp444;

        var temp446 = temp178 * temp253;
        var temp447 = temp180 * temp446;
        var temp448 = temp183 * (temp257 + temp447);
        var temp449 = temp226 * temp261;
        var temp450 = temp219 * temp449;
        var temp451 = temp221 * (temp265 + temp450);
        var temp452 = temp405 * temp451;
        var temp453 = temp407 * temp452;
        var temp454 = temp422 * temp453;
        var temp455 = temp422 * temp448 + -temp454;
        var temp456 = Math.Pow(temp438, -2) * temp441 * temp441;
        var temp457 = 1 / Math.Sqrt(1 + temp456);
        var temp458 = temp240 * temp269;
        var temp459 = temp242 * temp458;
        var temp460 = temp271 * temp459;
        var temp461 = temp439 * temp460;
        var temp462 = temp441 * temp461;
        var temp463 = temp457 * temp462;
        var oaR2C0 = temp455 * temp457 + -temp463;

        var temp465 = temp240 * temp413;
        var temp466 = temp242 * temp465;
        var temp467 = temp271 * temp466;
        var temp468 = temp271 * temp417 + -temp467;
        var temp469 = temp240 * temp425;
        var temp470 = temp242 * temp469;
        var temp471 = temp271 * temp470;
        var temp472 = -temp471;
        var temp473 = temp405 * temp472;
        var temp474 = temp407 * temp473;
        var temp475 = temp422 * temp474;
        var temp476 = temp422 * temp468 + -temp475;
        var oaR2C1 = temp457 * temp476;

        var temp478 = temp178 * temp275;
        var temp479 = temp180 * temp478;
        var temp480 = temp183 * (temp278 + temp479);
        var temp481 = temp226 * temp282;
        var temp482 = temp219 * temp481;
        var temp483 = temp221 * (temp285 + temp482);
        var temp484 = temp405 * temp483;
        var temp485 = temp407 * temp484;
        var temp486 = temp422 * temp485;
        var temp487 = temp422 * temp480 + -temp486;
        var temp488 = temp240 * temp289;
        var temp489 = temp242 * temp488;
        var temp490 = temp271 * (temp292 + temp489);
        var temp491 = temp439 * temp490;
        var temp492 = temp441 * temp491;
        var temp493 = temp457 * temp492;
        var ovR0C2 = temp457 * temp487 + -temp493;

        var temp495 = temp178 * temp299;
        var temp496 = temp180 * temp495;
        var temp497 = temp183 * (temp302 + temp496);
        var temp498 = temp226 * temp306;
        var temp499 = temp219 * temp498;
        var temp500 = temp221 * (temp309 + temp499);
        var temp501 = temp405 * temp500;
        var temp502 = temp407 * temp501;
        var temp503 = temp422 * temp502;
        var temp504 = temp422 * temp497 + -temp503;
        var temp505 = temp240 * temp313;
        var temp506 = temp242 * temp505;
        var temp507 = temp271 * (temp316 + temp506);
        var temp508 = temp439 * temp507;
        var temp509 = temp441 * temp508;
        var temp510 = temp457 * temp509;
        var ovR1C2 = temp457 * temp504 + -temp510;

        var temp512 = temp178 * temp323;
        var temp513 = temp180 * temp512;
        var temp514 = temp183 * (temp326 + temp513);
        var temp515 = temp226 * temp330;
        var temp516 = temp219 * temp515;
        var temp517 = temp221 * (temp333 + temp516);
        var temp518 = temp405 * temp517;
        var temp519 = temp407 * temp518;
        var temp520 = temp422 * temp519;
        var temp521 = temp422 * temp514 + -temp520;
        var temp522 = temp240 * temp337;
        var temp523 = temp242 * temp522;
        var temp524 = temp271 * (temp340 + temp523);
        var temp525 = temp439 * temp524;
        var temp526 = temp441 * temp525;
        var temp527 = temp457 * temp526;
        var ovR2C2 = temp457 * temp521 + -temp527;

        var temp529 = temp178 * temp347;
        var temp530 = temp180 * temp529;
        var temp531 = temp183 * (temp350 + temp530);
        var temp532 = temp226 * temp354;
        var temp533 = temp219 * temp532;
        var temp534 = temp221 * (temp357 + temp533);
        var temp535 = temp405 * temp534;
        var temp536 = temp407 * temp535;
        var temp537 = temp422 * temp536;
        var temp538 = temp422 * temp531 + -temp537;
        var temp539 = temp240 * temp361;
        var temp540 = temp242 * temp539;
        var temp541 = temp271 * (temp364 + temp540);
        var temp542 = temp439 * temp541;
        var temp543 = temp441 * temp542;
        var temp544 = temp457 * temp543;
        var ovR3C2 = temp457 * temp538 + -temp544;

        var temp546 = temp178 * temp371;
        var temp547 = temp180 * temp546;
        var temp548 = temp183 * (temp374 + temp547);
        var temp549 = temp226 * temp378;
        var temp550 = temp219 * temp549;
        var temp551 = temp221 * (temp381 + temp550);
        var temp552 = temp405 * temp551;
        var temp553 = temp407 * temp552;
        var temp554 = temp422 * temp553;
        var temp555 = temp422 * temp548 + -temp554;
        var temp556 = temp240 * temp385;
        var temp557 = temp242 * temp556;
        var temp558 = temp271 * (temp388 + temp557);
        var temp559 = temp439 * temp558;
        var temp560 = temp441 * temp559;
        var temp561 = temp457 * temp560;
        var ovR4C2 = temp457 * temp555 + -temp561;

        var temp563 = _oaR3C3 + temp408;
        var temp564 = temp405 * temp420;
        var temp565 = temp407 * temp564;
        var temp566 = temp422 * (temp426 + temp565);
        var temp567 = temp457 * temp566;
        var temp568 = _oaR4C4 + temp243;
        var temp569 = temp442 + temp568;
        var temp570 = -_oaR3C3 + temp409;
        var temp571 = temp46 + temp570;
        var temp572 = temp248 + temp571;
        var temp573 = temp569 + temp572;
        var temp574 = temp71 + temp573;
        var temp575 = 0.5 * 1 / temp567 * temp574;
        var temp576 = Math.Sqrt(1 + temp575 * temp575) + Math.Abs(temp575);
        var temp577 = 1 / temp576;
        var temp578 = temp567 * temp577;
        var temp579 = BinaryStep(temp575);
        var temp580 = temp578 * temp579;
        var temp581 = temp563 + -temp580;
        var temp582 = temp45 + temp581;
        var oaR3C3 = temp236 + temp582;

        var temp584 = temp569 + temp580;
        var oaR4C4 = temp71 + temp584;

        var temp586 = temp405 * temp448;
        var temp587 = temp407 * temp586;
        var temp588 = temp422 * (temp451 + temp587);
        var temp589 = Math.Pow(temp576, -2) * temp579 * temp579;
        var temp590 = 1 / Math.Sqrt(1 + temp589);
        var temp591 = temp439 * temp455;
        var temp592 = temp441 * temp591;
        var temp593 = temp457 * (temp460 + temp592);
        var temp594 = temp577 * temp593;
        var temp595 = temp579 * temp594;
        var temp596 = temp590 * temp595;
        var oaR3C0 = temp588 * temp590 + -temp596;

        var temp598 = temp577 * temp588;
        var temp599 = temp579 * temp598;
        var oaR4C0 = temp590 * (temp593 + temp599);

        var temp601 = temp405 * temp468;
        var temp602 = temp407 * temp601;
        var temp603 = temp422 * (temp472 + temp602);
        var temp604 = temp439 * temp476;
        var temp605 = temp441 * temp604;
        var temp606 = temp457 * temp605;
        var temp607 = temp577 * temp606;
        var temp608 = temp579 * temp607;
        var temp609 = temp590 * temp608;
        var oaR3C1 = temp590 * temp603 + -temp609;

        var temp611 = temp577 * temp603;
        var temp612 = temp579 * temp611;
        var oaR4C1 = temp590 * (temp606 + temp612);

        var temp614 = temp439 * temp566;
        var temp615 = temp441 * temp614;
        var temp616 = temp457 * temp615;
        var temp617 = -temp616;
        var oaR3C2 = temp590 * temp617;

        var temp619 = temp577 * temp617;
        var temp620 = temp579 * temp619;
        var oaR4C2 = temp590 * temp620;

        var temp622 = temp405 * temp480;
        var temp623 = temp407 * temp622;
        var temp624 = temp422 * (temp483 + temp623);
        var temp625 = temp439 * temp487;
        var temp626 = temp441 * temp625;
        var temp627 = temp457 * (temp490 + temp626);
        var temp628 = temp577 * temp627;
        var temp629 = temp579 * temp628;
        var temp630 = temp590 * temp629;
        var ovR0C3 = temp590 * temp624 + -temp630;

        var temp632 = temp577 * temp624;
        var temp633 = temp579 * temp632;
        var ovR0C4 = temp590 * (temp627 + temp633);

        var temp635 = temp405 * temp497;
        var temp636 = temp407 * temp635;
        var temp637 = temp422 * (temp500 + temp636);
        var temp638 = temp439 * temp504;
        var temp639 = temp441 * temp638;
        var temp640 = temp457 * (temp507 + temp639);
        var temp641 = temp577 * temp640;
        var temp642 = temp579 * temp641;
        var temp643 = temp590 * temp642;
        var ovR1C3 = temp590 * temp637 + -temp643;

        var temp645 = temp577 * temp637;
        var temp646 = temp579 * temp645;
        var ovR1C4 = temp590 * (temp640 + temp646);

        var temp648 = temp405 * temp514;
        var temp649 = temp407 * temp648;
        var temp650 = temp422 * (temp517 + temp649);
        var temp651 = temp439 * temp521;
        var temp652 = temp441 * temp651;
        var temp653 = temp457 * (temp524 + temp652);
        var temp654 = temp577 * temp653;
        var temp655 = temp579 * temp654;
        var temp656 = temp590 * temp655;
        var ovR2C3 = temp590 * temp650 + -temp656;

        var temp658 = temp577 * temp650;
        var temp659 = temp579 * temp658;
        var ovR2C4 = temp590 * (temp653 + temp659);

        var temp661 = temp405 * temp531;
        var temp662 = temp407 * temp661;
        var temp663 = temp422 * (temp534 + temp662);
        var temp664 = temp439 * temp538;
        var temp665 = temp441 * temp664;
        var temp666 = temp457 * (temp541 + temp665);
        var temp667 = temp577 * temp666;
        var temp668 = temp579 * temp667;
        var temp669 = temp590 * temp668;
        var ovR3C3 = temp590 * temp663 + -temp669;

        var temp671 = temp577 * temp663;
        var temp672 = temp579 * temp671;
        var ovR3C4 = temp590 * (temp666 + temp672);

        var temp674 = temp405 * temp548;
        var temp675 = temp407 * temp674;
        var temp676 = temp422 * (temp551 + temp675);
        var temp677 = temp439 * temp555;
        var temp678 = temp441 * temp677;
        var temp679 = temp457 * (temp558 + temp678);
        var temp680 = temp577 * temp679;
        var temp681 = temp579 * temp680;
        var temp682 = temp590 * temp681;
        var ovR4C3 = temp590 * temp676 + -temp682;

        var temp684 = temp577 * temp676;
        var temp685 = temp579 * temp684;
        var ovR4C4 = temp590 * (temp679 + temp685);

        //Finish GA-FuL MetaContext Code Generation, 2025-07-05T04:05:32.8198018+03:00

        _oaR0C0 = oaR0C0;
        _oaR1C0 = oaR1C0;
        _oaR2C0 = oaR2C0;
        _oaR3C0 = oaR3C0;
        _oaR4C0 = oaR4C0;
        _oaR1C1 = oaR1C1;
        _oaR2C1 = oaR2C1;
        _oaR3C1 = oaR3C1;
        _oaR4C1 = oaR4C1;
        _oaR2C2 = oaR2C2;
        _oaR3C2 = oaR3C2;
        _oaR4C2 = oaR4C2;
        _oaR3C3 = oaR3C3;
        _oaR4C3 = oaR4C3;
        _oaR4C4 = oaR4C4;

        _ovR0C0 = ovR0C0;
        _ovR1C0 = ovR1C0;
        _ovR2C0 = ovR2C0;
        _ovR3C0 = ovR3C0;
        _ovR4C0 = ovR4C0;
        _ovR0C1 = ovR0C1;
        _ovR1C1 = ovR1C1;
        _ovR2C1 = ovR2C1;
        _ovR3C1 = ovR3C1;
        _ovR4C1 = ovR4C1;
        _ovR0C2 = ovR0C2;
        _ovR1C2 = ovR1C2;
        _ovR2C2 = ovR2C2;
        _ovR3C2 = ovR3C2;
        _ovR4C2 = ovR4C2;
        _ovR0C3 = ovR0C3;
        _ovR1C3 = ovR1C3;
        _ovR2C3 = ovR2C3;
        _ovR3C3 = ovR3C3;
        _ovR4C3 = ovR4C3;
        _ovR0C4 = ovR0C4;
        _ovR1C4 = ovR1C4;
        _ovR2C4 = ovR2C4;
        _ovR3C4 = ovR3C4;
        _ovR4C4 = ovR4C4;
    }

    private void Rotate6()
    {
        //Begin GA-FuL MetaContext Code Generation, 2025-07-05T04:05:53.8796095+03:00
        //MetaContext: JacobiSymmetricEigenDecomposer6x6
        //Input Variables: 57 used, 0 not used, 57 total.
        //Temp Variables: 1191 sub-expressions, 0 generated temps, 1191 total.
        //Target Temp Variables: 1191 total.
        //Output Variables: 57 total.
        //Computations: 1.4150641025641026 average, 1766 total.
        //Memory Reads: 2.1490384615384617 average, 2682 total.
        //Memory Writes: 1248 total.
        //
        //MetaContext Binding Data: 
        //   0 = constant: '0'
        //   1 = constant: '1'
        //   -1 = constant: '-1'
        //   2 = constant: '2'
        //   -2 = constant: '-2'
        //   Rational[1, 2] = constant: 'Rational[1, 2]'
        //   Rational[-1, 2] = constant: 'Rational[-1, 2]'
        //   iaR0C0 = parameter: _oaR0C0
        //   iaR1C0 = parameter: _oaR1C0
        //   iaR2C0 = parameter: _oaR2C0
        //   iaR3C0 = parameter: _oaR3C0
        //   iaR4C0 = parameter: _oaR4C0
        //   iaR5C0 = parameter: _oaR5C0
        //   iaR1C1 = parameter: _oaR1C1
        //   iaR2C1 = parameter: _oaR2C1
        //   iaR3C1 = parameter: _oaR3C1
        //   iaR4C1 = parameter: _oaR4C1
        //   iaR5C1 = parameter: _oaR5C1
        //   iaR2C2 = parameter: _oaR2C2
        //   iaR3C2 = parameter: _oaR3C2
        //   iaR4C2 = parameter: _oaR4C2
        //   iaR5C2 = parameter: _oaR5C2
        //   iaR3C3 = parameter: _oaR3C3
        //   iaR4C3 = parameter: _oaR4C3
        //   iaR5C3 = parameter: _oaR5C3
        //   iaR4C4 = parameter: _oaR4C4
        //   iaR5C4 = parameter: _oaR5C4
        //   iaR5C5 = parameter: _oaR5C5
        //   ivR0C0 = parameter: _ovR0C0
        //   ivR1C0 = parameter: _ovR1C0
        //   ivR2C0 = parameter: _ovR2C0
        //   ivR3C0 = parameter: _ovR3C0
        //   ivR4C0 = parameter: _ovR4C0
        //   ivR5C0 = parameter: _ovR5C0
        //   ivR0C1 = parameter: _ovR0C1
        //   ivR1C1 = parameter: _ovR1C1
        //   ivR2C1 = parameter: _ovR2C1
        //   ivR3C1 = parameter: _ovR3C1
        //   ivR4C1 = parameter: _ovR4C1
        //   ivR5C1 = parameter: _ovR5C1
        //   ivR0C2 = parameter: _ovR0C2
        //   ivR1C2 = parameter: _ovR1C2
        //   ivR2C2 = parameter: _ovR2C2
        //   ivR3C2 = parameter: _ovR3C2
        //   ivR4C2 = parameter: _ovR4C2
        //   ivR5C2 = parameter: _ovR5C2
        //   ivR0C3 = parameter: _ovR0C3
        //   ivR1C3 = parameter: _ovR1C3
        //   ivR2C3 = parameter: _ovR2C3
        //   ivR3C3 = parameter: _ovR3C3
        //   ivR4C3 = parameter: _ovR4C3
        //   ivR5C3 = parameter: _ovR5C3
        //   ivR0C4 = parameter: _ovR0C4
        //   ivR1C4 = parameter: _ovR1C4
        //   ivR2C4 = parameter: _ovR2C4
        //   ivR3C4 = parameter: _ovR3C4
        //   ivR4C4 = parameter: _ovR4C4
        //   ivR5C4 = parameter: _ovR5C4
        //   ivR0C5 = parameter: _ovR0C5
        //   ivR1C5 = parameter: _ovR1C5
        //   ivR2C5 = parameter: _ovR2C5
        //   ivR3C5 = parameter: _ovR3C5
        //   ivR4C5 = parameter: _ovR4C5
        //   ivR5C5 = parameter: _ovR5C5

        var oaR5C4 = 0;

        var temp1 = -_oaR0C0;
        var temp2 = _oaR1C1 + temp1;
        var temp3 = 0.5 * 1 / _oaR1C0 * temp2;
        var temp4 = BinaryStep(temp3);
        var temp5 = Math.Sqrt(1 + temp3 * temp3) + Math.Abs(temp3);
        var temp6 = 1 / temp5;
        var temp7 = _oaR1C0 * temp6;
        var temp8 = temp4 * temp7;
        var temp9 = -temp8;
        var temp10 = _oaR0C0 + temp9;
        var temp11 = temp4 * temp4 * Math.Pow(temp5, -2);
        var temp12 = 1 / Math.Sqrt(1 + temp11);
        var temp13 = _oaR2C1 * temp6;
        var temp14 = temp4 * temp13;
        var temp15 = temp12 * temp14;
        var temp16 = _oaR2C0 * temp12 + -temp15;
        var temp17 = _oaR2C2 + temp1;
        var temp18 = temp8 + temp17;
        var temp19 = 0.5 * 1 / temp16 * temp18;
        var temp20 = BinaryStep(temp19);
        var temp21 = Math.Sqrt(1 + temp19 * temp19) + Math.Abs(temp19);
        var temp22 = 1 / temp21;
        var temp23 = temp16 * temp22;
        var temp24 = temp20 * temp23;
        var temp25 = -temp24;
        var temp26 = temp10 + temp25;
        var temp27 = _oaR3C1 * temp6;
        var temp28 = temp4 * temp27;
        var temp29 = temp12 * temp28;
        var temp30 = _oaR3C0 * temp12 + -temp29;
        var temp31 = temp20 * temp20 * Math.Pow(temp21, -2);
        var temp32 = 1 / Math.Sqrt(1 + temp31);
        var temp33 = _oaR3C2 * temp22;
        var temp34 = temp20 * temp33;
        var temp35 = temp32 * temp34;
        var temp36 = temp30 * temp32 + -temp35;
        var temp37 = _oaR3C3 + temp1;
        var temp38 = temp8 + temp37;
        var temp39 = temp24 + temp38;
        var temp40 = 0.5 * 1 / temp36 * temp39;
        var temp41 = Math.Sqrt(1 + temp40 * temp40) + Math.Abs(temp40);
        var temp42 = 1 / temp41;
        var temp43 = temp36 * temp42;
        var temp44 = BinaryStep(temp40);
        var temp45 = temp43 * temp44;
        var temp46 = -temp45;
        var temp47 = temp26 + temp46;
        var temp48 = _oaR4C1 * temp6;
        var temp49 = temp4 * temp48;
        var temp50 = temp12 * temp49;
        var temp51 = _oaR4C0 * temp12 + -temp50;
        var temp52 = _oaR4C2 * temp22;
        var temp53 = temp20 * temp52;
        var temp54 = temp32 * temp53;
        var temp55 = temp32 * temp51 + -temp54;
        var temp56 = Math.Pow(temp41, -2) * temp44 * temp44;
        var temp57 = 1 / Math.Sqrt(1 + temp56);
        var temp58 = _oaR4C3 * temp42;
        var temp59 = temp44 * temp58;
        var temp60 = temp57 * temp59;
        var temp61 = temp55 * temp57 + -temp60;
        var temp62 = _oaR4C4 + temp1;
        var temp63 = temp8 + temp62;
        var temp64 = temp24 + temp63;
        var temp65 = temp45 + temp64;
        var temp66 = 0.5 * 1 / temp61 * temp65;
        var temp67 = Math.Sqrt(1 + temp66 * temp66) + Math.Abs(temp66);
        var temp68 = 1 / temp67;
        var temp69 = temp61 * temp68;
        var temp70 = BinaryStep(temp66);
        var temp71 = temp69 * temp70;
        var temp72 = -temp71;
        var temp73 = temp47 + temp72;
        var temp74 = _oaR5C1 * temp6;
        var temp75 = temp4 * temp74;
        var temp76 = temp12 * temp75;
        var temp77 = _oaR5C0 * temp12 + -temp76;
        var temp78 = _oaR5C2 * temp22;
        var temp79 = temp20 * temp78;
        var temp80 = temp32 * temp79;
        var temp81 = temp32 * temp77 + -temp80;
        var temp82 = _oaR5C3 * temp42;
        var temp83 = temp44 * temp82;
        var temp84 = temp57 * temp83;
        var temp85 = temp57 * temp81 + -temp84;
        var temp86 = Math.Pow(temp67, -2) * temp70 * temp70;
        var temp87 = 1 / Math.Sqrt(1 + temp86);
        var temp88 = _oaR5C4 * temp68;
        var temp89 = temp70 * temp88;
        var temp90 = temp87 * temp89;
        var temp91 = temp85 * temp87 + -temp90;
        var temp92 = _oaR5C5 + temp1;
        var temp93 = temp8 + temp92;
        var temp94 = temp24 + temp93;
        var temp95 = temp45 + temp94;
        var temp96 = temp71 + temp95;
        var temp97 = 0.5 * 1 / temp91 * temp96;
        var temp98 = Math.Sqrt(1 + temp97 * temp97) + Math.Abs(temp97);
        var temp99 = 1 / temp98;
        var temp100 = temp91 * temp99;
        var temp101 = BinaryStep(temp97);
        var temp102 = temp100 * temp101;
        var oaR0C0 = temp73 + -temp102;

        var temp104 = _ovR0C1 * temp6;
        var temp105 = temp4 * temp104;
        var temp106 = temp12 * temp105;
        var temp107 = _ovR0C0 * temp12 + -temp106;
        var temp108 = _ovR0C2 * temp22;
        var temp109 = temp20 * temp108;
        var temp110 = temp32 * temp109;
        var temp111 = temp32 * temp107 + -temp110;
        var temp112 = _ovR0C3 * temp42;
        var temp113 = temp44 * temp112;
        var temp114 = temp57 * temp113;
        var temp115 = temp57 * temp111 + -temp114;
        var temp116 = _ovR0C4 * temp68;
        var temp117 = temp70 * temp116;
        var temp118 = temp87 * temp117;
        var temp119 = temp87 * temp115 + -temp118;
        var temp120 = Math.Pow(temp98, -2) * temp101 * temp101;
        var temp121 = 1 / Math.Sqrt(1 + temp120);
        var temp122 = _ovR0C5 * temp99;
        var temp123 = temp101 * temp122;
        var temp124 = temp121 * temp123;
        var ovR0C0 = temp119 * temp121 + -temp124;

        var temp126 = _ovR1C1 * temp6;
        var temp127 = temp4 * temp126;
        var temp128 = temp12 * temp127;
        var temp129 = _ovR1C0 * temp12 + -temp128;
        var temp130 = _ovR1C2 * temp22;
        var temp131 = temp20 * temp130;
        var temp132 = temp32 * temp131;
        var temp133 = temp32 * temp129 + -temp132;
        var temp134 = _ovR1C3 * temp42;
        var temp135 = temp44 * temp134;
        var temp136 = temp57 * temp135;
        var temp137 = temp57 * temp133 + -temp136;
        var temp138 = _ovR1C4 * temp68;
        var temp139 = temp70 * temp138;
        var temp140 = temp87 * temp139;
        var temp141 = temp87 * temp137 + -temp140;
        var temp142 = _ovR1C5 * temp99;
        var temp143 = temp101 * temp142;
        var temp144 = temp121 * temp143;
        var ovR1C0 = temp121 * temp141 + -temp144;

        var temp146 = _ovR2C1 * temp6;
        var temp147 = temp4 * temp146;
        var temp148 = temp12 * temp147;
        var temp149 = _ovR2C0 * temp12 + -temp148;
        var temp150 = _ovR2C2 * temp22;
        var temp151 = temp20 * temp150;
        var temp152 = temp32 * temp151;
        var temp153 = temp32 * temp149 + -temp152;
        var temp154 = _ovR2C3 * temp42;
        var temp155 = temp44 * temp154;
        var temp156 = temp57 * temp155;
        var temp157 = temp57 * temp153 + -temp156;
        var temp158 = _ovR2C4 * temp68;
        var temp159 = temp70 * temp158;
        var temp160 = temp87 * temp159;
        var temp161 = temp87 * temp157 + -temp160;
        var temp162 = _ovR2C5 * temp99;
        var temp163 = temp101 * temp162;
        var temp164 = temp121 * temp163;
        var ovR2C0 = temp121 * temp161 + -temp164;

        var temp166 = _ovR3C1 * temp6;
        var temp167 = temp4 * temp166;
        var temp168 = temp12 * temp167;
        var temp169 = _ovR3C0 * temp12 + -temp168;
        var temp170 = _ovR3C2 * temp22;
        var temp171 = temp20 * temp170;
        var temp172 = temp32 * temp171;
        var temp173 = temp32 * temp169 + -temp172;
        var temp174 = _ovR3C3 * temp42;
        var temp175 = temp44 * temp174;
        var temp176 = temp57 * temp175;
        var temp177 = temp57 * temp173 + -temp176;
        var temp178 = _ovR3C4 * temp68;
        var temp179 = temp70 * temp178;
        var temp180 = temp87 * temp179;
        var temp181 = temp87 * temp177 + -temp180;
        var temp182 = _ovR3C5 * temp99;
        var temp183 = temp101 * temp182;
        var temp184 = temp121 * temp183;
        var ovR3C0 = temp121 * temp181 + -temp184;

        var temp186 = _ovR4C1 * temp6;
        var temp187 = temp4 * temp186;
        var temp188 = temp12 * temp187;
        var temp189 = _ovR4C0 * temp12 + -temp188;
        var temp190 = _ovR4C2 * temp22;
        var temp191 = temp20 * temp190;
        var temp192 = temp32 * temp191;
        var temp193 = temp32 * temp189 + -temp192;
        var temp194 = _ovR4C3 * temp42;
        var temp195 = temp44 * temp194;
        var temp196 = temp57 * temp195;
        var temp197 = temp57 * temp193 + -temp196;
        var temp198 = _ovR4C4 * temp68;
        var temp199 = temp70 * temp198;
        var temp200 = temp87 * temp199;
        var temp201 = temp87 * temp197 + -temp200;
        var temp202 = _ovR4C5 * temp99;
        var temp203 = temp101 * temp202;
        var temp204 = temp121 * temp203;
        var ovR4C0 = temp121 * temp201 + -temp204;

        var temp206 = _ovR5C1 * temp6;
        var temp207 = temp4 * temp206;
        var temp208 = temp12 * temp207;
        var temp209 = _ovR5C0 * temp12 + -temp208;
        var temp210 = _ovR5C2 * temp22;
        var temp211 = temp20 * temp210;
        var temp212 = temp32 * temp211;
        var temp213 = temp32 * temp209 + -temp212;
        var temp214 = _ovR5C3 * temp42;
        var temp215 = temp44 * temp214;
        var temp216 = temp57 * temp215;
        var temp217 = temp57 * temp213 + -temp216;
        var temp218 = _ovR5C4 * temp68;
        var temp219 = temp70 * temp218;
        var temp220 = temp87 * temp219;
        var temp221 = temp87 * temp217 + -temp220;
        var temp222 = _ovR5C5 * temp99;
        var temp223 = temp101 * temp222;
        var temp224 = temp121 * temp223;
        var ovR5C0 = temp121 * temp221 + -temp224;

        var temp226 = _oaR1C1 + temp8;
        var temp227 = _oaR2C0 * temp6;
        var temp228 = temp4 * temp227;
        var temp229 = temp12 * (_oaR2C1 + temp228);
        var temp230 = temp32 * temp229;
        var temp231 = _oaR2C2 + temp24;
        var temp232 = -_oaR1C1;
        var temp233 = temp9 + temp232;
        var temp234 = temp231 + temp233;
        var temp235 = 0.5 * 1 / temp230 * temp234;
        var temp236 = Math.Sqrt(1 + temp235 * temp235) + Math.Abs(temp235);
        var temp237 = 1 / temp236;
        var temp238 = temp230 * temp237;
        var temp239 = BinaryStep(temp235);
        var temp240 = temp238 * temp239;
        var temp241 = -temp240;
        var temp242 = temp226 + temp241;
        var temp243 = _oaR3C0 * temp6;
        var temp244 = temp4 * temp243;
        var temp245 = temp12 * (_oaR3C1 + temp244);
        var temp246 = temp22 * temp229;
        var temp247 = temp20 * temp246;
        var temp248 = temp32 * temp247;
        var temp249 = -temp248;
        var temp250 = temp42 * temp249;
        var temp251 = temp44 * temp250;
        var temp252 = temp57 * (temp245 + temp251);
        var temp253 = Math.Pow(temp236, -2) * temp239 * temp239;
        var temp254 = 1 / Math.Sqrt(1 + temp253);
        var temp255 = temp22 * temp30;
        var temp256 = temp20 * temp255;
        var temp257 = temp32 * (_oaR3C2 + temp256);
        var temp258 = temp57 * temp257;
        var temp259 = temp237 * temp258;
        var temp260 = temp239 * temp259;
        var temp261 = temp254 * temp260;
        var temp262 = temp252 * temp254 + -temp261;
        var temp263 = _oaR3C3 + temp232;
        var temp264 = temp9 + temp263;
        var temp265 = temp45 + temp264;
        var temp266 = temp240 + temp265;
        var temp267 = 0.5 * 1 / temp262 * temp266;
        var temp268 = Math.Sqrt(1 + temp267 * temp267) + Math.Abs(temp267);
        var temp269 = 1 / temp268;
        var temp270 = temp262 * temp269;
        var temp271 = BinaryStep(temp267);
        var temp272 = temp270 * temp271;
        var temp273 = -temp272;
        var temp274 = temp242 + temp273;
        var temp275 = _oaR4C0 * temp6;
        var temp276 = temp4 * temp275;
        var temp277 = temp12 * (_oaR4C1 + temp276);
        var temp278 = temp42 * temp245;
        var temp279 = temp44 * temp278;
        var temp280 = temp57 * temp279;
        var temp281 = temp57 * temp249 + -temp280;
        var temp282 = temp68 * temp281;
        var temp283 = temp70 * temp282;
        var temp284 = temp87 * (temp277 + temp283);
        var temp285 = temp22 * temp51;
        var temp286 = temp20 * temp285;
        var temp287 = temp32 * (_oaR4C2 + temp286);
        var temp288 = temp42 * temp257;
        var temp289 = temp44 * temp288;
        var temp290 = temp57 * temp289;
        var temp291 = -temp290;
        var temp292 = temp68 * temp291;
        var temp293 = temp70 * temp292;
        var temp294 = temp87 * (temp287 + temp293);
        var temp295 = temp237 * temp294;
        var temp296 = temp239 * temp295;
        var temp297 = temp254 * temp296;
        var temp298 = temp254 * temp284 + -temp297;
        var temp299 = Math.Pow(temp268, -2) * temp271 * temp271;
        var temp300 = 1 / Math.Sqrt(1 + temp299);
        var temp301 = temp42 * temp55;
        var temp302 = temp44 * temp301;
        var temp303 = temp57 * (_oaR4C3 + temp302);
        var temp304 = temp87 * temp303;
        var temp305 = temp269 * temp304;
        var temp306 = temp271 * temp305;
        var temp307 = temp300 * temp306;
        var temp308 = temp298 * temp300 + -temp307;
        var temp309 = _oaR4C4 + temp232;
        var temp310 = temp9 + temp309;
        var temp311 = temp71 + temp310;
        var temp312 = temp240 + temp311;
        var temp313 = temp272 + temp312;
        var temp314 = 0.5 * 1 / temp308 * temp313;
        var temp315 = Math.Sqrt(1 + temp314 * temp314) + Math.Abs(temp314);
        var temp316 = 1 / temp315;
        var temp317 = temp308 * temp316;
        var temp318 = BinaryStep(temp314);
        var temp319 = temp317 * temp318;
        var temp320 = -temp319;
        var temp321 = temp274 + temp320;
        var temp322 = _oaR5C0 * temp6;
        var temp323 = temp4 * temp322;
        var temp324 = temp12 * (_oaR5C1 + temp323);
        var temp325 = temp68 * temp277;
        var temp326 = temp70 * temp325;
        var temp327 = temp87 * temp326;
        var temp328 = temp87 * temp281 + -temp327;
        var temp329 = temp99 * temp328;
        var temp330 = temp101 * temp329;
        var temp331 = temp121 * (temp324 + temp330);
        var temp332 = temp22 * temp77;
        var temp333 = temp20 * temp332;
        var temp334 = temp32 * (_oaR5C2 + temp333);
        var temp335 = temp68 * temp287;
        var temp336 = temp70 * temp335;
        var temp337 = temp87 * temp336;
        var temp338 = temp87 * temp291 + -temp337;
        var temp339 = temp99 * temp338;
        var temp340 = temp101 * temp339;
        var temp341 = temp121 * (temp334 + temp340);
        var temp342 = temp237 * temp341;
        var temp343 = temp239 * temp342;
        var temp344 = temp254 * temp343;
        var temp345 = temp254 * temp331 + -temp344;
        var temp346 = temp42 * temp81;
        var temp347 = temp44 * temp346;
        var temp348 = temp57 * (_oaR5C3 + temp347);
        var temp349 = temp68 * temp303;
        var temp350 = temp70 * temp349;
        var temp351 = temp87 * temp350;
        var temp352 = -temp351;
        var temp353 = temp99 * temp352;
        var temp354 = temp101 * temp353;
        var temp355 = temp121 * (temp348 + temp354);
        var temp356 = temp269 * temp355;
        var temp357 = temp271 * temp356;
        var temp358 = temp300 * temp357;
        var temp359 = temp300 * temp345 + -temp358;
        var temp360 = Math.Pow(temp315, -2) * temp318 * temp318;
        var temp361 = 1 / Math.Sqrt(1 + temp360);
        var temp362 = temp68 * temp85;
        var temp363 = temp70 * temp362;
        var temp364 = temp87 * (_oaR5C4 + temp363);
        var temp365 = temp121 * temp364;
        var temp366 = temp316 * temp365;
        var temp367 = temp318 * temp366;
        var temp368 = temp361 * temp367;
        var temp369 = temp359 * temp361 + -temp368;
        var temp370 = _oaR5C5 + temp232;
        var temp371 = temp9 + temp370;
        var temp372 = temp102 + temp371;
        var temp373 = temp240 + temp372;
        var temp374 = temp272 + temp373;
        var temp375 = temp319 + temp374;
        var temp376 = 0.5 * 1 / temp369 * temp375;
        var temp377 = Math.Sqrt(1 + temp376 * temp376) + Math.Abs(temp376);
        var temp378 = 1 / temp377;
        var temp379 = temp369 * temp378;
        var temp380 = BinaryStep(temp376);
        var temp381 = temp379 * temp380;
        var oaR1C1 = temp321 + -temp381;

        var temp383 = temp99 * temp324;
        var temp384 = temp101 * temp383;
        var temp385 = temp121 * temp384;
        var temp386 = temp121 * temp328 + -temp385;
        var temp387 = temp99 * temp334;
        var temp388 = temp101 * temp387;
        var temp389 = temp121 * temp388;
        var temp390 = temp121 * temp338 + -temp389;
        var temp391 = temp237 * temp390;
        var temp392 = temp239 * temp391;
        var temp393 = temp254 * temp392;
        var temp394 = temp254 * temp386 + -temp393;
        var temp395 = temp99 * temp348;
        var temp396 = temp101 * temp395;
        var temp397 = temp121 * temp396;
        var temp398 = temp121 * temp352 + -temp397;
        var temp399 = temp269 * temp398;
        var temp400 = temp271 * temp399;
        var temp401 = temp300 * temp400;
        var temp402 = temp300 * temp394 + -temp401;
        var temp403 = temp99 * temp364;
        var temp404 = temp101 * temp403;
        var temp405 = temp121 * temp404;
        var temp406 = -temp405;
        var temp407 = temp316 * temp406;
        var temp408 = temp318 * temp407;
        var temp409 = temp361 * temp408;
        var temp410 = temp361 * temp402 + -temp409;
        var temp411 = Math.Pow(temp377, -2) * temp380 * temp380;
        var temp412 = 1 / Math.Sqrt(1 + temp411);
        var oaR1C0 = temp410 * temp412;

        var temp414 = _ovR0C0 * temp6;
        var temp415 = temp4 * temp414;
        var temp416 = temp12 * (_ovR0C1 + temp415);
        var temp417 = temp22 * temp107;
        var temp418 = temp20 * temp417;
        var temp419 = temp32 * (_ovR0C2 + temp418);
        var temp420 = temp237 * temp419;
        var temp421 = temp239 * temp420;
        var temp422 = temp254 * temp421;
        var temp423 = temp254 * temp416 + -temp422;
        var temp424 = temp42 * temp111;
        var temp425 = temp44 * temp424;
        var temp426 = temp57 * (_ovR0C3 + temp425);
        var temp427 = temp269 * temp426;
        var temp428 = temp271 * temp427;
        var temp429 = temp300 * temp428;
        var temp430 = temp300 * temp423 + -temp429;
        var temp431 = temp68 * temp115;
        var temp432 = temp70 * temp431;
        var temp433 = temp87 * (_ovR0C4 + temp432);
        var temp434 = temp316 * temp433;
        var temp435 = temp318 * temp434;
        var temp436 = temp361 * temp435;
        var temp437 = temp361 * temp430 + -temp436;
        var temp438 = temp99 * temp119;
        var temp439 = temp101 * temp438;
        var temp440 = temp121 * (_ovR0C5 + temp439);
        var temp441 = temp378 * temp440;
        var temp442 = temp380 * temp441;
        var temp443 = temp412 * temp442;
        var ovR0C1 = temp412 * temp437 + -temp443;

        var temp445 = _ovR1C0 * temp6;
        var temp446 = temp4 * temp445;
        var temp447 = temp12 * (_ovR1C1 + temp446);
        var temp448 = temp22 * temp129;
        var temp449 = temp20 * temp448;
        var temp450 = temp32 * (_ovR1C2 + temp449);
        var temp451 = temp237 * temp450;
        var temp452 = temp239 * temp451;
        var temp453 = temp254 * temp452;
        var temp454 = temp254 * temp447 + -temp453;
        var temp455 = temp42 * temp133;
        var temp456 = temp44 * temp455;
        var temp457 = temp57 * (_ovR1C3 + temp456);
        var temp458 = temp269 * temp457;
        var temp459 = temp271 * temp458;
        var temp460 = temp300 * temp459;
        var temp461 = temp300 * temp454 + -temp460;
        var temp462 = temp68 * temp137;
        var temp463 = temp70 * temp462;
        var temp464 = temp87 * (_ovR1C4 + temp463);
        var temp465 = temp316 * temp464;
        var temp466 = temp318 * temp465;
        var temp467 = temp361 * temp466;
        var temp468 = temp361 * temp461 + -temp467;
        var temp469 = temp99 * temp141;
        var temp470 = temp101 * temp469;
        var temp471 = temp121 * (_ovR1C5 + temp470);
        var temp472 = temp378 * temp471;
        var temp473 = temp380 * temp472;
        var temp474 = temp412 * temp473;
        var ovR1C1 = temp412 * temp468 + -temp474;

        var temp476 = _ovR2C0 * temp6;
        var temp477 = temp4 * temp476;
        var temp478 = temp12 * (_ovR2C1 + temp477);
        var temp479 = temp22 * temp149;
        var temp480 = temp20 * temp479;
        var temp481 = temp32 * (_ovR2C2 + temp480);
        var temp482 = temp237 * temp481;
        var temp483 = temp239 * temp482;
        var temp484 = temp254 * temp483;
        var temp485 = temp254 * temp478 + -temp484;
        var temp486 = temp42 * temp153;
        var temp487 = temp44 * temp486;
        var temp488 = temp57 * (_ovR2C3 + temp487);
        var temp489 = temp269 * temp488;
        var temp490 = temp271 * temp489;
        var temp491 = temp300 * temp490;
        var temp492 = temp300 * temp485 + -temp491;
        var temp493 = temp68 * temp157;
        var temp494 = temp70 * temp493;
        var temp495 = temp87 * (_ovR2C4 + temp494);
        var temp496 = temp316 * temp495;
        var temp497 = temp318 * temp496;
        var temp498 = temp361 * temp497;
        var temp499 = temp361 * temp492 + -temp498;
        var temp500 = temp99 * temp161;
        var temp501 = temp101 * temp500;
        var temp502 = temp121 * (_ovR2C5 + temp501);
        var temp503 = temp378 * temp502;
        var temp504 = temp380 * temp503;
        var temp505 = temp412 * temp504;
        var ovR2C1 = temp412 * temp499 + -temp505;

        var temp507 = _ovR3C0 * temp6;
        var temp508 = temp4 * temp507;
        var temp509 = temp12 * (_ovR3C1 + temp508);
        var temp510 = temp22 * temp169;
        var temp511 = temp20 * temp510;
        var temp512 = temp32 * (_ovR3C2 + temp511);
        var temp513 = temp237 * temp512;
        var temp514 = temp239 * temp513;
        var temp515 = temp254 * temp514;
        var temp516 = temp254 * temp509 + -temp515;
        var temp517 = temp42 * temp173;
        var temp518 = temp44 * temp517;
        var temp519 = temp57 * (_ovR3C3 + temp518);
        var temp520 = temp269 * temp519;
        var temp521 = temp271 * temp520;
        var temp522 = temp300 * temp521;
        var temp523 = temp300 * temp516 + -temp522;
        var temp524 = temp68 * temp177;
        var temp525 = temp70 * temp524;
        var temp526 = temp87 * (_ovR3C4 + temp525);
        var temp527 = temp316 * temp526;
        var temp528 = temp318 * temp527;
        var temp529 = temp361 * temp528;
        var temp530 = temp361 * temp523 + -temp529;
        var temp531 = temp99 * temp181;
        var temp532 = temp101 * temp531;
        var temp533 = temp121 * (_ovR3C5 + temp532);
        var temp534 = temp378 * temp533;
        var temp535 = temp380 * temp534;
        var temp536 = temp412 * temp535;
        var ovR3C1 = temp412 * temp530 + -temp536;

        var temp538 = _ovR4C0 * temp6;
        var temp539 = temp4 * temp538;
        var temp540 = temp12 * (_ovR4C1 + temp539);
        var temp541 = temp22 * temp189;
        var temp542 = temp20 * temp541;
        var temp543 = temp32 * (_ovR4C2 + temp542);
        var temp544 = temp237 * temp543;
        var temp545 = temp239 * temp544;
        var temp546 = temp254 * temp545;
        var temp547 = temp254 * temp540 + -temp546;
        var temp548 = temp42 * temp193;
        var temp549 = temp44 * temp548;
        var temp550 = temp57 * (_ovR4C3 + temp549);
        var temp551 = temp269 * temp550;
        var temp552 = temp271 * temp551;
        var temp553 = temp300 * temp552;
        var temp554 = temp300 * temp547 + -temp553;
        var temp555 = temp68 * temp197;
        var temp556 = temp70 * temp555;
        var temp557 = temp87 * (_ovR4C4 + temp556);
        var temp558 = temp316 * temp557;
        var temp559 = temp318 * temp558;
        var temp560 = temp361 * temp559;
        var temp561 = temp361 * temp554 + -temp560;
        var temp562 = temp99 * temp201;
        var temp563 = temp101 * temp562;
        var temp564 = temp121 * (_ovR4C5 + temp563);
        var temp565 = temp378 * temp564;
        var temp566 = temp380 * temp565;
        var temp567 = temp412 * temp566;
        var ovR4C1 = temp412 * temp561 + -temp567;

        var temp569 = _ovR5C0 * temp6;
        var temp570 = temp4 * temp569;
        var temp571 = temp12 * (_ovR5C1 + temp570);
        var temp572 = temp22 * temp209;
        var temp573 = temp20 * temp572;
        var temp574 = temp32 * (_ovR5C2 + temp573);
        var temp575 = temp237 * temp574;
        var temp576 = temp239 * temp575;
        var temp577 = temp254 * temp576;
        var temp578 = temp254 * temp571 + -temp577;
        var temp579 = temp42 * temp213;
        var temp580 = temp44 * temp579;
        var temp581 = temp57 * (_ovR5C3 + temp580);
        var temp582 = temp269 * temp581;
        var temp583 = temp271 * temp582;
        var temp584 = temp300 * temp583;
        var temp585 = temp300 * temp578 + -temp584;
        var temp586 = temp68 * temp217;
        var temp587 = temp70 * temp586;
        var temp588 = temp87 * (_ovR5C4 + temp587);
        var temp589 = temp316 * temp588;
        var temp590 = temp318 * temp589;
        var temp591 = temp361 * temp590;
        var temp592 = temp361 * temp585 + -temp591;
        var temp593 = temp99 * temp221;
        var temp594 = temp101 * temp593;
        var temp595 = temp121 * (_ovR5C5 + temp594);
        var temp596 = temp378 * temp595;
        var temp597 = temp380 * temp596;
        var temp598 = temp412 * temp597;
        var ovR5C1 = temp412 * temp592 + -temp598;

        var temp600 = temp231 + temp240;
        var temp601 = temp237 * temp252;
        var temp602 = temp239 * temp601;
        var temp603 = temp254 * (temp258 + temp602);
        var temp604 = temp300 * temp603;
        var temp605 = _oaR3C3 + temp45;
        var temp606 = temp272 + temp605;
        var temp607 = -_oaR2C2;
        var temp608 = temp25 + temp607;
        var temp609 = temp241 + temp608;
        var temp610 = temp606 + temp609;
        var temp611 = 0.5 * 1 / temp604 * temp610;
        var temp612 = Math.Sqrt(1 + temp611 * temp611) + Math.Abs(temp611);
        var temp613 = 1 / temp612;
        var temp614 = temp604 * temp613;
        var temp615 = BinaryStep(temp611);
        var temp616 = temp614 * temp615;
        var temp617 = -temp616;
        var temp618 = temp600 + temp617;
        var temp619 = temp237 * temp284;
        var temp620 = temp239 * temp619;
        var temp621 = temp254 * (temp294 + temp620);
        var temp622 = temp269 * temp603;
        var temp623 = temp271 * temp622;
        var temp624 = temp300 * temp623;
        var temp625 = -temp624;
        var temp626 = temp316 * temp625;
        var temp627 = temp318 * temp626;
        var temp628 = temp361 * (temp621 + temp627);
        var temp629 = Math.Pow(temp612, -2) * temp615 * temp615;
        var temp630 = 1 / Math.Sqrt(1 + temp629);
        var temp631 = temp269 * temp298;
        var temp632 = temp271 * temp631;
        var temp633 = temp300 * (temp304 + temp632);
        var temp634 = temp361 * temp633;
        var temp635 = temp613 * temp634;
        var temp636 = temp615 * temp635;
        var temp637 = temp630 * temp636;
        var temp638 = temp628 * temp630 + -temp637;
        var temp639 = _oaR4C4 + temp607;
        var temp640 = temp25 + temp639;
        var temp641 = temp71 + temp640;
        var temp642 = temp241 + temp641;
        var temp643 = temp319 + temp642;
        var temp644 = temp616 + temp643;
        var temp645 = 0.5 * 1 / temp638 * temp644;
        var temp646 = Math.Sqrt(1 + temp645 * temp645) + Math.Abs(temp645);
        var temp647 = 1 / temp646;
        var temp648 = temp638 * temp647;
        var temp649 = BinaryStep(temp645);
        var temp650 = temp648 * temp649;
        var temp651 = -temp650;
        var temp652 = temp618 + temp651;
        var temp653 = temp237 * temp331;
        var temp654 = temp239 * temp653;
        var temp655 = temp254 * (temp341 + temp654);
        var temp656 = temp316 * temp621;
        var temp657 = temp318 * temp656;
        var temp658 = temp361 * temp657;
        var temp659 = temp361 * temp625 + -temp658;
        var temp660 = temp378 * temp659;
        var temp661 = temp380 * temp660;
        var temp662 = temp412 * (temp655 + temp661);
        var temp663 = temp269 * temp345;
        var temp664 = temp271 * temp663;
        var temp665 = temp300 * (temp355 + temp664);
        var temp666 = temp316 * temp633;
        var temp667 = temp318 * temp666;
        var temp668 = temp361 * temp667;
        var temp669 = -temp668;
        var temp670 = temp378 * temp669;
        var temp671 = temp380 * temp670;
        var temp672 = temp412 * (temp665 + temp671);
        var temp673 = temp613 * temp672;
        var temp674 = temp615 * temp673;
        var temp675 = temp630 * temp674;
        var temp676 = temp630 * temp662 + -temp675;
        var temp677 = Math.Pow(temp646, -2) * temp649 * temp649;
        var temp678 = 1 / Math.Sqrt(1 + temp677);
        var temp679 = temp316 * temp359;
        var temp680 = temp318 * temp679;
        var temp681 = temp361 * (temp365 + temp680);
        var temp682 = temp412 * temp681;
        var temp683 = temp647 * temp682;
        var temp684 = temp649 * temp683;
        var temp685 = temp678 * temp684;
        var temp686 = temp676 * temp678 + -temp685;
        var temp687 = _oaR5C5 + temp607;
        var temp688 = temp25 + temp687;
        var temp689 = temp102 + temp688;
        var temp690 = temp241 + temp689;
        var temp691 = temp381 + temp690;
        var temp692 = temp616 + temp691;
        var temp693 = temp650 + temp692;
        var temp694 = 0.5 * 1 / temp686 * temp693;
        var temp695 = Math.Sqrt(1 + temp694 * temp694) + Math.Abs(temp694);
        var temp696 = 1 / temp695;
        var temp697 = temp686 * temp696;
        var temp698 = BinaryStep(temp694);
        var temp699 = temp697 * temp698;
        var oaR2C2 = temp652 + -temp699;

        var temp701 = temp237 * temp386;
        var temp702 = temp239 * temp701;
        var temp703 = temp254 * (temp390 + temp702);
        var temp704 = temp269 * temp394;
        var temp705 = temp271 * temp704;
        var temp706 = temp300 * (temp398 + temp705);
        var temp707 = temp613 * temp706;
        var temp708 = temp615 * temp707;
        var temp709 = temp630 * temp708;
        var temp710 = temp630 * temp703 + -temp709;
        var temp711 = temp316 * temp402;
        var temp712 = temp318 * temp711;
        var temp713 = temp361 * (temp406 + temp712);
        var temp714 = temp647 * temp713;
        var temp715 = temp649 * temp714;
        var temp716 = temp678 * temp715;
        var temp717 = temp678 * temp710 + -temp716;
        var temp718 = Math.Pow(temp695, -2) * temp698 * temp698;
        var temp719 = 1 / Math.Sqrt(1 + temp718);
        var temp720 = temp378 * temp410;
        var temp721 = temp380 * temp720;
        var temp722 = temp412 * temp721;
        var temp723 = temp696 * temp722;
        var temp724 = temp698 * temp723;
        var temp725 = temp719 * temp724;
        var oaR2C0 = temp717 * temp719 + -temp725;

        var temp727 = temp378 * temp655;
        var temp728 = temp380 * temp727;
        var temp729 = temp412 * temp728;
        var temp730 = temp412 * temp659 + -temp729;
        var temp731 = temp378 * temp665;
        var temp732 = temp380 * temp731;
        var temp733 = temp412 * temp732;
        var temp734 = temp412 * temp669 + -temp733;
        var temp735 = temp613 * temp734;
        var temp736 = temp615 * temp735;
        var temp737 = temp630 * temp736;
        var temp738 = temp630 * temp730 + -temp737;
        var temp739 = temp378 * temp681;
        var temp740 = temp380 * temp739;
        var temp741 = temp412 * temp740;
        var temp742 = -temp741;
        var temp743 = temp647 * temp742;
        var temp744 = temp649 * temp743;
        var temp745 = temp678 * temp744;
        var temp746 = temp678 * temp738 + -temp745;
        var oaR2C1 = temp719 * temp746;

        var temp748 = temp237 * temp416;
        var temp749 = temp239 * temp748;
        var temp750 = temp254 * (temp419 + temp749);
        var temp751 = temp269 * temp423;
        var temp752 = temp271 * temp751;
        var temp753 = temp300 * (temp426 + temp752);
        var temp754 = temp613 * temp753;
        var temp755 = temp615 * temp754;
        var temp756 = temp630 * temp755;
        var temp757 = temp630 * temp750 + -temp756;
        var temp758 = temp316 * temp430;
        var temp759 = temp318 * temp758;
        var temp760 = temp361 * (temp433 + temp759);
        var temp761 = temp647 * temp760;
        var temp762 = temp649 * temp761;
        var temp763 = temp678 * temp762;
        var temp764 = temp678 * temp757 + -temp763;
        var temp765 = temp378 * temp437;
        var temp766 = temp380 * temp765;
        var temp767 = temp412 * (temp440 + temp766);
        var temp768 = temp696 * temp767;
        var temp769 = temp698 * temp768;
        var temp770 = temp719 * temp769;
        var ovR0C2 = temp719 * temp764 + -temp770;

        var temp772 = temp237 * temp447;
        var temp773 = temp239 * temp772;
        var temp774 = temp254 * (temp450 + temp773);
        var temp775 = temp269 * temp454;
        var temp776 = temp271 * temp775;
        var temp777 = temp300 * (temp457 + temp776);
        var temp778 = temp613 * temp777;
        var temp779 = temp615 * temp778;
        var temp780 = temp630 * temp779;
        var temp781 = temp630 * temp774 + -temp780;
        var temp782 = temp316 * temp461;
        var temp783 = temp318 * temp782;
        var temp784 = temp361 * (temp464 + temp783);
        var temp785 = temp647 * temp784;
        var temp786 = temp649 * temp785;
        var temp787 = temp678 * temp786;
        var temp788 = temp678 * temp781 + -temp787;
        var temp789 = temp378 * temp468;
        var temp790 = temp380 * temp789;
        var temp791 = temp412 * (temp471 + temp790);
        var temp792 = temp696 * temp791;
        var temp793 = temp698 * temp792;
        var temp794 = temp719 * temp793;
        var ovR1C2 = temp719 * temp788 + -temp794;

        var temp796 = temp237 * temp478;
        var temp797 = temp239 * temp796;
        var temp798 = temp254 * (temp481 + temp797);
        var temp799 = temp269 * temp485;
        var temp800 = temp271 * temp799;
        var temp801 = temp300 * (temp488 + temp800);
        var temp802 = temp613 * temp801;
        var temp803 = temp615 * temp802;
        var temp804 = temp630 * temp803;
        var temp805 = temp630 * temp798 + -temp804;
        var temp806 = temp316 * temp492;
        var temp807 = temp318 * temp806;
        var temp808 = temp361 * (temp495 + temp807);
        var temp809 = temp647 * temp808;
        var temp810 = temp649 * temp809;
        var temp811 = temp678 * temp810;
        var temp812 = temp678 * temp805 + -temp811;
        var temp813 = temp378 * temp499;
        var temp814 = temp380 * temp813;
        var temp815 = temp412 * (temp502 + temp814);
        var temp816 = temp696 * temp815;
        var temp817 = temp698 * temp816;
        var temp818 = temp719 * temp817;
        var ovR2C2 = temp719 * temp812 + -temp818;

        var temp820 = temp237 * temp509;
        var temp821 = temp239 * temp820;
        var temp822 = temp254 * (temp512 + temp821);
        var temp823 = temp269 * temp516;
        var temp824 = temp271 * temp823;
        var temp825 = temp300 * (temp519 + temp824);
        var temp826 = temp613 * temp825;
        var temp827 = temp615 * temp826;
        var temp828 = temp630 * temp827;
        var temp829 = temp630 * temp822 + -temp828;
        var temp830 = temp316 * temp523;
        var temp831 = temp318 * temp830;
        var temp832 = temp361 * (temp526 + temp831);
        var temp833 = temp647 * temp832;
        var temp834 = temp649 * temp833;
        var temp835 = temp678 * temp834;
        var temp836 = temp678 * temp829 + -temp835;
        var temp837 = temp378 * temp530;
        var temp838 = temp380 * temp837;
        var temp839 = temp412 * (temp533 + temp838);
        var temp840 = temp696 * temp839;
        var temp841 = temp698 * temp840;
        var temp842 = temp719 * temp841;
        var ovR3C2 = temp719 * temp836 + -temp842;

        var temp844 = temp237 * temp540;
        var temp845 = temp239 * temp844;
        var temp846 = temp254 * (temp543 + temp845);
        var temp847 = temp269 * temp547;
        var temp848 = temp271 * temp847;
        var temp849 = temp300 * (temp550 + temp848);
        var temp850 = temp613 * temp849;
        var temp851 = temp615 * temp850;
        var temp852 = temp630 * temp851;
        var temp853 = temp630 * temp846 + -temp852;
        var temp854 = temp316 * temp554;
        var temp855 = temp318 * temp854;
        var temp856 = temp361 * (temp557 + temp855);
        var temp857 = temp647 * temp856;
        var temp858 = temp649 * temp857;
        var temp859 = temp678 * temp858;
        var temp860 = temp678 * temp853 + -temp859;
        var temp861 = temp378 * temp561;
        var temp862 = temp380 * temp861;
        var temp863 = temp412 * (temp564 + temp862);
        var temp864 = temp696 * temp863;
        var temp865 = temp698 * temp864;
        var temp866 = temp719 * temp865;
        var ovR4C2 = temp719 * temp860 + -temp866;

        var temp868 = temp237 * temp571;
        var temp869 = temp239 * temp868;
        var temp870 = temp254 * (temp574 + temp869);
        var temp871 = temp269 * temp578;
        var temp872 = temp271 * temp871;
        var temp873 = temp300 * (temp581 + temp872);
        var temp874 = temp613 * temp873;
        var temp875 = temp615 * temp874;
        var temp876 = temp630 * temp875;
        var temp877 = temp630 * temp870 + -temp876;
        var temp878 = temp316 * temp585;
        var temp879 = temp318 * temp878;
        var temp880 = temp361 * (temp588 + temp879);
        var temp881 = temp647 * temp880;
        var temp882 = temp649 * temp881;
        var temp883 = temp678 * temp882;
        var temp884 = temp678 * temp877 + -temp883;
        var temp885 = temp378 * temp592;
        var temp886 = temp380 * temp885;
        var temp887 = temp412 * (temp595 + temp886);
        var temp888 = temp696 * temp887;
        var temp889 = temp698 * temp888;
        var temp890 = temp719 * temp889;
        var ovR5C2 = temp719 * temp884 + -temp890;

        var temp892 = temp606 + temp616;
        var temp893 = temp613 * temp628;
        var temp894 = temp615 * temp893;
        var temp895 = temp630 * (temp634 + temp894);
        var temp896 = temp678 * temp895;
        var temp897 = _oaR4C4 + temp71;
        var temp898 = temp319 + temp897;
        var temp899 = temp650 + temp898;
        var temp900 = -_oaR3C3;
        var temp901 = temp46 + temp900;
        var temp902 = temp273 + temp901;
        var temp903 = temp617 + temp902;
        var temp904 = temp899 + temp903;
        var temp905 = 0.5 * 1 / temp896 * temp904;
        var temp906 = BinaryStep(temp905);
        var temp907 = Math.Sqrt(1 + temp905 * temp905) + Math.Abs(temp905);
        var temp908 = 1 / temp907;
        var temp909 = temp896 * temp908;
        var temp910 = temp906 * temp909;
        var temp911 = -temp910;
        var temp912 = temp892 + temp911;
        var temp913 = temp613 * temp662;
        var temp914 = temp615 * temp913;
        var temp915 = temp630 * (temp672 + temp914);
        var temp916 = temp647 * temp895;
        var temp917 = temp649 * temp916;
        var temp918 = temp678 * temp917;
        var temp919 = -temp918;
        var temp920 = temp696 * temp919;
        var temp921 = temp698 * temp920;
        var temp922 = temp719 * (temp915 + temp921);
        var temp923 = temp906 * temp906 * Math.Pow(temp907, -2);
        var temp924 = 1 / Math.Sqrt(1 + temp923);
        var temp925 = temp647 * temp676;
        var temp926 = temp649 * temp925;
        var temp927 = temp678 * (temp682 + temp926);
        var temp928 = temp719 * temp927;
        var temp929 = temp908 * temp928;
        var temp930 = temp906 * temp929;
        var temp931 = temp924 * temp930;
        var temp932 = temp922 * temp924 + -temp931;
        var temp933 = _oaR5C5 + temp900;
        var temp934 = temp46 + temp933;
        var temp935 = temp102 + temp934;
        var temp936 = temp273 + temp935;
        var temp937 = temp381 + temp936;
        var temp938 = temp617 + temp937;
        var temp939 = temp699 + temp938;
        var temp940 = temp910 + temp939;
        var temp941 = 0.5 * 1 / temp932 * temp940;
        var temp942 = Math.Sqrt(1 + temp941 * temp941) + Math.Abs(temp941);
        var temp943 = 1 / temp942;
        var temp944 = temp932 * temp943;
        var temp945 = BinaryStep(temp941);
        var temp946 = temp944 * temp945;
        var oaR3C3 = temp912 + -temp946;

        var temp948 = temp613 * temp703;
        var temp949 = temp615 * temp948;
        var temp950 = temp630 * (temp706 + temp949);
        var temp951 = temp647 * temp710;
        var temp952 = temp649 * temp951;
        var temp953 = temp678 * (temp713 + temp952);
        var temp954 = temp908 * temp953;
        var temp955 = temp906 * temp954;
        var temp956 = temp924 * temp955;
        var temp957 = temp924 * temp950 + -temp956;
        var temp958 = Math.Pow(temp942, -2) * temp945 * temp945;
        var temp959 = 1 / Math.Sqrt(1 + temp958);
        var temp960 = temp696 * temp717;
        var temp961 = temp698 * temp960;
        var temp962 = temp719 * (temp722 + temp961);
        var temp963 = temp943 * temp962;
        var temp964 = temp945 * temp963;
        var temp965 = temp959 * temp964;
        var oaR3C0 = temp957 * temp959 + -temp965;

        var temp967 = temp613 * temp730;
        var temp968 = temp615 * temp967;
        var temp969 = temp630 * (temp734 + temp968);
        var temp970 = temp647 * temp738;
        var temp971 = temp649 * temp970;
        var temp972 = temp678 * (temp742 + temp971);
        var temp973 = temp908 * temp972;
        var temp974 = temp906 * temp973;
        var temp975 = temp924 * temp974;
        var temp976 = temp924 * temp969 + -temp975;
        var temp977 = temp696 * temp746;
        var temp978 = temp698 * temp977;
        var temp979 = temp719 * temp978;
        var temp980 = temp943 * temp979;
        var temp981 = temp945 * temp980;
        var temp982 = temp959 * temp981;
        var oaR3C1 = temp959 * temp976 + -temp982;

        var temp984 = temp696 * temp915;
        var temp985 = temp698 * temp984;
        var temp986 = temp719 * temp985;
        var temp987 = temp719 * temp919 + -temp986;
        var temp988 = temp696 * temp927;
        var temp989 = temp698 * temp988;
        var temp990 = temp719 * temp989;
        var temp991 = -temp990;
        var temp992 = temp908 * temp991;
        var temp993 = temp906 * temp992;
        var temp994 = temp924 * temp993;
        var temp995 = temp924 * temp987 + -temp994;
        var oaR3C2 = temp959 * temp995;

        var temp997 = temp613 * temp750;
        var temp998 = temp615 * temp997;
        var temp999 = temp630 * (temp753 + temp998);
        var temp1000 = temp647 * temp757;
        var temp1001 = temp649 * temp1000;
        var temp1002 = temp678 * (temp760 + temp1001);
        var temp1003 = temp908 * temp1002;
        var temp1004 = temp906 * temp1003;
        var temp1005 = temp924 * temp1004;
        var temp1006 = temp924 * temp999 + -temp1005;
        var temp1007 = temp696 * temp764;
        var temp1008 = temp698 * temp1007;
        var temp1009 = temp719 * (temp767 + temp1008);
        var temp1010 = temp943 * temp1009;
        var temp1011 = temp945 * temp1010;
        var temp1012 = temp959 * temp1011;
        var ovR0C3 = temp959 * temp1006 + -temp1012;

        var temp1014 = temp613 * temp774;
        var temp1015 = temp615 * temp1014;
        var temp1016 = temp630 * (temp777 + temp1015);
        var temp1017 = temp647 * temp781;
        var temp1018 = temp649 * temp1017;
        var temp1019 = temp678 * (temp784 + temp1018);
        var temp1020 = temp908 * temp1019;
        var temp1021 = temp906 * temp1020;
        var temp1022 = temp924 * temp1021;
        var temp1023 = temp924 * temp1016 + -temp1022;
        var temp1024 = temp696 * temp788;
        var temp1025 = temp698 * temp1024;
        var temp1026 = temp719 * (temp791 + temp1025);
        var temp1027 = temp943 * temp1026;
        var temp1028 = temp945 * temp1027;
        var temp1029 = temp959 * temp1028;
        var ovR1C3 = temp959 * temp1023 + -temp1029;

        var temp1031 = temp613 * temp798;
        var temp1032 = temp615 * temp1031;
        var temp1033 = temp630 * (temp801 + temp1032);
        var temp1034 = temp647 * temp805;
        var temp1035 = temp649 * temp1034;
        var temp1036 = temp678 * (temp808 + temp1035);
        var temp1037 = temp908 * temp1036;
        var temp1038 = temp906 * temp1037;
        var temp1039 = temp924 * temp1038;
        var temp1040 = temp924 * temp1033 + -temp1039;
        var temp1041 = temp696 * temp812;
        var temp1042 = temp698 * temp1041;
        var temp1043 = temp719 * (temp815 + temp1042);
        var temp1044 = temp943 * temp1043;
        var temp1045 = temp945 * temp1044;
        var temp1046 = temp959 * temp1045;
        var ovR2C3 = temp959 * temp1040 + -temp1046;

        var temp1048 = temp613 * temp822;
        var temp1049 = temp615 * temp1048;
        var temp1050 = temp630 * (temp825 + temp1049);
        var temp1051 = temp647 * temp829;
        var temp1052 = temp649 * temp1051;
        var temp1053 = temp678 * (temp832 + temp1052);
        var temp1054 = temp908 * temp1053;
        var temp1055 = temp906 * temp1054;
        var temp1056 = temp924 * temp1055;
        var temp1057 = temp924 * temp1050 + -temp1056;
        var temp1058 = temp696 * temp836;
        var temp1059 = temp698 * temp1058;
        var temp1060 = temp719 * (temp839 + temp1059);
        var temp1061 = temp943 * temp1060;
        var temp1062 = temp945 * temp1061;
        var temp1063 = temp959 * temp1062;
        var ovR3C3 = temp959 * temp1057 + -temp1063;

        var temp1065 = temp613 * temp846;
        var temp1066 = temp615 * temp1065;
        var temp1067 = temp630 * (temp849 + temp1066);
        var temp1068 = temp647 * temp853;
        var temp1069 = temp649 * temp1068;
        var temp1070 = temp678 * (temp856 + temp1069);
        var temp1071 = temp908 * temp1070;
        var temp1072 = temp906 * temp1071;
        var temp1073 = temp924 * temp1072;
        var temp1074 = temp924 * temp1067 + -temp1073;
        var temp1075 = temp696 * temp860;
        var temp1076 = temp698 * temp1075;
        var temp1077 = temp719 * (temp863 + temp1076);
        var temp1078 = temp943 * temp1077;
        var temp1079 = temp945 * temp1078;
        var temp1080 = temp959 * temp1079;
        var ovR4C3 = temp959 * temp1074 + -temp1080;

        var temp1082 = temp613 * temp870;
        var temp1083 = temp615 * temp1082;
        var temp1084 = temp630 * (temp873 + temp1083);
        var temp1085 = temp647 * temp877;
        var temp1086 = temp649 * temp1085;
        var temp1087 = temp678 * (temp880 + temp1086);
        var temp1088 = temp908 * temp1087;
        var temp1089 = temp906 * temp1088;
        var temp1090 = temp924 * temp1089;
        var temp1091 = temp924 * temp1084 + -temp1090;
        var temp1092 = temp696 * temp884;
        var temp1093 = temp698 * temp1092;
        var temp1094 = temp719 * (temp887 + temp1093);
        var temp1095 = temp943 * temp1094;
        var temp1096 = temp945 * temp1095;
        var temp1097 = temp959 * temp1096;
        var ovR5C3 = temp959 * temp1091 + -temp1097;

        var temp1099 = temp899 + temp910;
        var temp1100 = temp908 * temp922;
        var temp1101 = temp906 * temp1100;
        var temp1102 = temp924 * (temp928 + temp1101);
        var temp1103 = temp959 * temp1102;
        var temp1104 = _oaR5C5 + temp102;
        var temp1105 = temp381 + temp1104;
        var temp1106 = temp699 + temp1105;
        var temp1107 = temp946 + temp1106;
        var temp1108 = -_oaR4C4 + temp72;
        var temp1109 = temp320 + temp1108;
        var temp1110 = temp651 + temp1109;
        var temp1111 = temp911 + temp1110;
        var temp1112 = temp1107 + temp1111;
        var temp1113 = 0.5 * 1 / temp1103 * temp1112;
        var temp1114 = Math.Sqrt(1 + temp1113 * temp1113) + Math.Abs(temp1113);
        var temp1115 = 1 / temp1114;
        var temp1116 = temp1103 * temp1115;
        var temp1117 = BinaryStep(temp1113);
        var temp1118 = temp1116 * temp1117;
        var oaR4C4 = temp1099 + -temp1118;

        var oaR5C5 = temp1107 + temp1118;

        var temp1121 = temp908 * temp950;
        var temp1122 = temp906 * temp1121;
        var temp1123 = temp924 * (temp953 + temp1122);
        var temp1124 = Math.Pow(temp1114, -2) * temp1117 * temp1117;
        var temp1125 = 1 / Math.Sqrt(1 + temp1124);
        var temp1126 = temp943 * temp957;
        var temp1127 = temp945 * temp1126;
        var temp1128 = temp959 * (temp962 + temp1127);
        var temp1129 = temp1115 * temp1128;
        var temp1130 = temp1117 * temp1129;
        var temp1131 = temp1125 * temp1130;
        var oaR4C0 = temp1123 * temp1125 + -temp1131;

        var temp1133 = temp1115 * temp1123;
        var temp1134 = temp1117 * temp1133;
        var oaR5C0 = temp1125 * (temp1128 + temp1134);

        var temp1136 = temp908 * temp969;
        var temp1137 = temp906 * temp1136;
        var temp1138 = temp924 * (temp972 + temp1137);
        var temp1139 = temp943 * temp976;
        var temp1140 = temp945 * temp1139;
        var temp1141 = temp959 * (temp979 + temp1140);
        var temp1142 = temp1115 * temp1141;
        var temp1143 = temp1117 * temp1142;
        var temp1144 = temp1125 * temp1143;
        var oaR4C1 = temp1125 * temp1138 + -temp1144;

        var temp1146 = temp1115 * temp1138;
        var temp1147 = temp1117 * temp1146;
        var oaR5C1 = temp1125 * (temp1141 + temp1147);

        var temp1149 = temp908 * temp987;
        var temp1150 = temp906 * temp1149;
        var temp1151 = temp924 * (temp991 + temp1150);
        var temp1152 = temp943 * temp995;
        var temp1153 = temp945 * temp1152;
        var temp1154 = temp959 * temp1153;
        var temp1155 = temp1115 * temp1154;
        var temp1156 = temp1117 * temp1155;
        var temp1157 = temp1125 * temp1156;
        var oaR4C2 = temp1125 * temp1151 + -temp1157;

        var temp1159 = temp1115 * temp1151;
        var temp1160 = temp1117 * temp1159;
        var oaR5C2 = temp1125 * (temp1154 + temp1160);

        var temp1162 = temp943 * temp1102;
        var temp1163 = temp945 * temp1162;
        var temp1164 = temp959 * temp1163;
        var temp1165 = -temp1164;
        var oaR4C3 = temp1125 * temp1165;

        var temp1167 = temp1115 * temp1165;
        var temp1168 = temp1117 * temp1167;
        var oaR5C3 = temp1125 * temp1168;

        var temp1170 = temp908 * temp999;
        var temp1171 = temp906 * temp1170;
        var temp1172 = temp924 * (temp1002 + temp1171);
        var temp1173 = temp943 * temp1006;
        var temp1174 = temp945 * temp1173;
        var temp1175 = temp959 * (temp1009 + temp1174);
        var temp1176 = temp1115 * temp1175;
        var temp1177 = temp1117 * temp1176;
        var temp1178 = temp1125 * temp1177;
        var ovR0C4 = temp1125 * temp1172 + -temp1178;

        var temp1180 = temp1115 * temp1172;
        var temp1181 = temp1117 * temp1180;
        var ovR0C5 = temp1125 * (temp1175 + temp1181);

        var temp1183 = temp908 * temp1016;
        var temp1184 = temp906 * temp1183;
        var temp1185 = temp924 * (temp1019 + temp1184);
        var temp1186 = temp943 * temp1023;
        var temp1187 = temp945 * temp1186;
        var temp1188 = temp959 * (temp1026 + temp1187);
        var temp1189 = temp1115 * temp1188;
        var temp1190 = temp1117 * temp1189;
        var temp1191 = temp1125 * temp1190;
        var ovR1C4 = temp1125 * temp1185 + -temp1191;

        var temp1193 = temp1115 * temp1185;
        var temp1194 = temp1117 * temp1193;
        var ovR1C5 = temp1125 * (temp1188 + temp1194);

        var temp1196 = temp908 * temp1033;
        var temp1197 = temp906 * temp1196;
        var temp1198 = temp924 * (temp1036 + temp1197);
        var temp1199 = temp943 * temp1040;
        var temp1200 = temp945 * temp1199;
        var temp1201 = temp959 * (temp1043 + temp1200);
        var temp1202 = temp1115 * temp1201;
        var temp1203 = temp1117 * temp1202;
        var temp1204 = temp1125 * temp1203;
        var ovR2C4 = temp1125 * temp1198 + -temp1204;

        var temp1206 = temp1115 * temp1198;
        var temp1207 = temp1117 * temp1206;
        var ovR2C5 = temp1125 * (temp1201 + temp1207);

        var temp1209 = temp908 * temp1050;
        var temp1210 = temp906 * temp1209;
        var temp1211 = temp924 * (temp1053 + temp1210);
        var temp1212 = temp943 * temp1057;
        var temp1213 = temp945 * temp1212;
        var temp1214 = temp959 * (temp1060 + temp1213);
        var temp1215 = temp1115 * temp1214;
        var temp1216 = temp1117 * temp1215;
        var temp1217 = temp1125 * temp1216;
        var ovR3C4 = temp1125 * temp1211 + -temp1217;

        var temp1219 = temp1115 * temp1211;
        var temp1220 = temp1117 * temp1219;
        var ovR3C5 = temp1125 * (temp1214 + temp1220);

        var temp1222 = temp908 * temp1067;
        var temp1223 = temp906 * temp1222;
        var temp1224 = temp924 * (temp1070 + temp1223);
        var temp1225 = temp943 * temp1074;
        var temp1226 = temp945 * temp1225;
        var temp1227 = temp959 * (temp1077 + temp1226);
        var temp1228 = temp1115 * temp1227;
        var temp1229 = temp1117 * temp1228;
        var temp1230 = temp1125 * temp1229;
        var ovR4C4 = temp1125 * temp1224 + -temp1230;

        var temp1232 = temp1115 * temp1224;
        var temp1233 = temp1117 * temp1232;
        var ovR4C5 = temp1125 * (temp1227 + temp1233);

        var temp1235 = temp908 * temp1084;
        var temp1236 = temp906 * temp1235;
        var temp1237 = temp924 * (temp1087 + temp1236);
        var temp1238 = temp943 * temp1091;
        var temp1239 = temp945 * temp1238;
        var temp1240 = temp959 * (temp1094 + temp1239);
        var temp1241 = temp1115 * temp1240;
        var temp1242 = temp1117 * temp1241;
        var temp1243 = temp1125 * temp1242;
        var ovR5C4 = temp1125 * temp1237 + -temp1243;

        var temp1245 = temp1115 * temp1237;
        var temp1246 = temp1117 * temp1245;
        var ovR5C5 = temp1125 * (temp1240 + temp1246);

        //Finish GA-FuL MetaContext Code Generation, 2025-07-05T04:05:53.8826737+03:00

        _oaR0C0 = oaR0C0;
        _oaR1C0 = oaR1C0;
        _oaR2C0 = oaR2C0;
        _oaR3C0 = oaR3C0;
        _oaR4C0 = oaR4C0;
        _oaR5C0 = oaR5C0;
        _oaR1C1 = oaR1C1;
        _oaR2C1 = oaR2C1;
        _oaR3C1 = oaR3C1;
        _oaR4C1 = oaR4C1;
        _oaR5C1 = oaR5C1;
        _oaR2C2 = oaR2C2;
        _oaR3C2 = oaR3C2;
        _oaR4C2 = oaR4C2;
        _oaR5C2 = oaR5C2;
        _oaR3C3 = oaR3C3;
        _oaR4C3 = oaR4C3;
        _oaR5C3 = oaR5C3;
        _oaR4C4 = oaR4C4;
        _oaR5C4 = oaR5C4;
        _oaR5C5 = oaR5C5;

        _ovR0C0 = ovR0C0;
        _ovR1C0 = ovR1C0;
        _ovR2C0 = ovR2C0;
        _ovR3C0 = ovR3C0;
        _ovR4C0 = ovR4C0;
        _ovR5C0 = ovR5C0;
        _ovR0C1 = ovR0C1;
        _ovR1C1 = ovR1C1;
        _ovR2C1 = ovR2C1;
        _ovR3C1 = ovR3C1;
        _ovR4C1 = ovR4C1;
        _ovR5C1 = ovR5C1;
        _ovR0C2 = ovR0C2;
        _ovR1C2 = ovR1C2;
        _ovR2C2 = ovR2C2;
        _ovR3C2 = ovR3C2;
        _ovR4C2 = ovR4C2;
        _ovR5C2 = ovR5C2;
        _ovR0C3 = ovR0C3;
        _ovR1C3 = ovR1C3;
        _ovR2C3 = ovR2C3;
        _ovR3C3 = ovR3C3;
        _ovR4C3 = ovR4C3;
        _ovR5C3 = ovR5C3;
        _ovR0C4 = ovR0C4;
        _ovR1C4 = ovR1C4;
        _ovR2C4 = ovR2C4;
        _ovR3C4 = ovR3C4;
        _ovR4C4 = ovR4C4;
        _ovR5C4 = ovR5C4;
        _ovR0C5 = ovR0C5;
        _ovR1C5 = ovR1C5;
        _ovR2C5 = ovR2C5;
        _ovR3C5 = ovR3C5;
        _ovR4C5 = ovR4C5;
        _ovR5C5 = ovR5C5;
    }

    private void RotateN()
    {
        for (var p = 0; p < Size - 1; p++)
        for (var q = p + 1; q < Size; q++)
            Rotate(p, q);
    }


    private void EigenDecompose2()
    {
        Initialize2();

        for (var sweep = 0; sweep < MaxSweeps; sweep++)
        {
            if (GetOffDiagonalNorm2() < NormTolerance) break;

            Rotate2();
        }

        DiagonalMatrix = new double[2, 2];
        EigenVectors = new double[2, 2];
        EigenValues = new double[2];

        // Update diagonal matrix
        DiagonalMatrix[0, 0] = _oaR0C0;
        DiagonalMatrix[0, 1] = _oaR1C0;

        DiagonalMatrix[1, 0] = _oaR1C0;
        DiagonalMatrix[1, 1] = _oaR1C1;

        // Update eigen vectors matrix
        EigenVectors[0, 0] = _ovR0C0;
        EigenVectors[0, 1] = _ovR0C1;

        EigenVectors[1, 0] = _ovR1C0;
        EigenVectors[1, 1] = _ovR1C1;

        // Update eigen values
        EigenValues[0] = _oaR0C0;
        EigenValues[1] = _oaR1C1;
    }

    private void EigenDecompose3()
    {
        Initialize3();

        for (var sweep = 0; sweep < MaxSweeps; sweep++)
        {
            if (GetOffDiagonalNorm3() < NormTolerance) break;

            Rotate3();
        }

        DiagonalMatrix = new double[3, 3];
        EigenVectors = new double[3, 3];
        EigenValues = new double[3];

        // Update diagonal matrix
        DiagonalMatrix[0, 0] = _oaR0C0;
        DiagonalMatrix[0, 1] = _oaR1C0;
        DiagonalMatrix[0, 2] = _oaR2C0;

        DiagonalMatrix[1, 0] = _oaR1C0;
        DiagonalMatrix[1, 1] = _oaR1C1;
        DiagonalMatrix[1, 2] = _oaR2C1;

        DiagonalMatrix[2, 0] = _oaR2C0;
        DiagonalMatrix[2, 1] = _oaR2C1;
        DiagonalMatrix[2, 2] = _oaR2C2;

        // Update eigen vectors matrix
        EigenVectors[0, 0] = _ovR0C0;
        EigenVectors[0, 1] = _ovR0C1;
        EigenVectors[0, 2] = _ovR0C2;

        EigenVectors[1, 0] = _ovR1C0;
        EigenVectors[1, 1] = _ovR1C1;
        EigenVectors[1, 2] = _ovR1C2;

        EigenVectors[2, 0] = _ovR2C0;
        EigenVectors[2, 1] = _ovR2C1;
        EigenVectors[2, 2] = _ovR2C2;

        // Update eigen values
        EigenValues[0] = _oaR0C0;
        EigenValues[1] = _oaR1C1;
        EigenValues[2] = _oaR2C2;

    }

    private void EigenDecompose4()
    {
        Initialize4();

        for (var sweep = 0; sweep < MaxSweeps; sweep++)
        {
            if (GetOffDiagonalNorm4() < NormTolerance) break;

            Rotate4();
        }

        DiagonalMatrix = new double[4, 4];
        EigenVectors = new double[4, 4];
        EigenValues = new double[4];

        // Update diagonal matrix
        DiagonalMatrix[0, 0] = _oaR0C0;
        DiagonalMatrix[0, 1] = _oaR1C0;
        DiagonalMatrix[0, 2] = _oaR2C0;
        DiagonalMatrix[0, 3] = _oaR3C0;

        DiagonalMatrix[1, 0] = _oaR1C0;
        DiagonalMatrix[1, 1] = _oaR1C1;
        DiagonalMatrix[1, 2] = _oaR2C1;
        DiagonalMatrix[1, 3] = _oaR3C1;

        DiagonalMatrix[2, 0] = _oaR2C0;
        DiagonalMatrix[2, 1] = _oaR2C1;
        DiagonalMatrix[2, 2] = _oaR2C2;
        DiagonalMatrix[2, 3] = _oaR3C2;

        DiagonalMatrix[3, 0] = _oaR3C0;
        DiagonalMatrix[3, 1] = _oaR3C1;
        DiagonalMatrix[3, 2] = _oaR3C2;
        DiagonalMatrix[3, 3] = _oaR3C3;

        // Update eigen vectors matrix
        EigenVectors[0, 0] = _ovR0C0;
        EigenVectors[0, 1] = _ovR0C1;
        EigenVectors[0, 2] = _ovR0C2;
        EigenVectors[0, 3] = _ovR0C3;

        EigenVectors[1, 0] = _ovR1C0;
        EigenVectors[1, 1] = _ovR1C1;
        EigenVectors[1, 2] = _ovR1C2;
        EigenVectors[1, 3] = _ovR1C3;

        EigenVectors[2, 0] = _ovR2C0;
        EigenVectors[2, 1] = _ovR2C1;
        EigenVectors[2, 2] = _ovR2C2;
        EigenVectors[2, 3] = _ovR2C3;

        EigenVectors[3, 0] = _ovR3C0;
        EigenVectors[3, 1] = _ovR3C1;
        EigenVectors[3, 2] = _ovR3C2;
        EigenVectors[3, 3] = _ovR3C3;

        // Update eigen values
        EigenValues[0] = _oaR0C0;
        EigenValues[1] = _oaR1C1;
        EigenValues[2] = _oaR2C2;
        EigenValues[3] = _oaR3C3;

    }

    private void EigenDecompose5()
    {
        Initialize5();

        for (var sweep = 0; sweep < MaxSweeps; sweep++)
        {
            if (GetOffDiagonalNorm5() < NormTolerance) break;

            Rotate5();
        }

        DiagonalMatrix = new double[5, 5];
        EigenVectors = new double[5, 5];
        EigenValues = new double[5];

        // Update diagonal matrix
        DiagonalMatrix[0, 0] = _oaR0C0;
        DiagonalMatrix[0, 1] = _oaR1C0;
        DiagonalMatrix[0, 2] = _oaR2C0;
        DiagonalMatrix[0, 3] = _oaR3C0;
        DiagonalMatrix[0, 4] = _oaR4C0;

        DiagonalMatrix[1, 0] = _oaR1C0;
        DiagonalMatrix[1, 1] = _oaR1C1;
        DiagonalMatrix[1, 2] = _oaR2C1;
        DiagonalMatrix[1, 3] = _oaR3C1;
        DiagonalMatrix[1, 4] = _oaR4C1;

        DiagonalMatrix[2, 0] = _oaR2C0;
        DiagonalMatrix[2, 1] = _oaR2C1;
        DiagonalMatrix[2, 2] = _oaR2C2;
        DiagonalMatrix[2, 3] = _oaR3C2;
        DiagonalMatrix[2, 4] = _oaR4C2;

        DiagonalMatrix[3, 0] = _oaR3C0;
        DiagonalMatrix[3, 1] = _oaR3C1;
        DiagonalMatrix[3, 2] = _oaR3C2;
        DiagonalMatrix[3, 3] = _oaR3C3;
        DiagonalMatrix[3, 4] = _oaR4C3;

        DiagonalMatrix[4, 0] = _oaR4C0;
        DiagonalMatrix[4, 1] = _oaR4C1;
        DiagonalMatrix[4, 2] = _oaR4C2;
        DiagonalMatrix[4, 3] = _oaR4C3;
        DiagonalMatrix[4, 4] = _oaR4C4;

        // Update eigen vectors matrix
        EigenVectors[0, 0] = _ovR0C0;
        EigenVectors[0, 1] = _ovR0C1;
        EigenVectors[0, 2] = _ovR0C2;
        EigenVectors[0, 3] = _ovR0C3;
        EigenVectors[0, 4] = _ovR0C4;

        EigenVectors[1, 0] = _ovR1C0;
        EigenVectors[1, 1] = _ovR1C1;
        EigenVectors[1, 2] = _ovR1C2;
        EigenVectors[1, 3] = _ovR1C3;
        EigenVectors[1, 4] = _ovR1C4;

        EigenVectors[2, 0] = _ovR2C0;
        EigenVectors[2, 1] = _ovR2C1;
        EigenVectors[2, 2] = _ovR2C2;
        EigenVectors[2, 3] = _ovR2C3;
        EigenVectors[2, 4] = _ovR2C4;

        EigenVectors[3, 0] = _ovR3C0;
        EigenVectors[3, 1] = _ovR3C1;
        EigenVectors[3, 2] = _ovR3C2;
        EigenVectors[3, 3] = _ovR3C3;
        EigenVectors[3, 4] = _ovR3C4;

        EigenVectors[4, 0] = _ovR4C0;
        EigenVectors[4, 1] = _ovR4C1;
        EigenVectors[4, 2] = _ovR4C2;
        EigenVectors[4, 3] = _ovR4C3;
        EigenVectors[4, 4] = _ovR4C4;

        // Update eigen values
        EigenValues[0] = _oaR0C0;
        EigenValues[1] = _oaR1C1;
        EigenValues[2] = _oaR2C2;
        EigenValues[3] = _oaR3C3;
        EigenValues[4] = _oaR4C4;

    }

    private void EigenDecompose6()
    {
        Initialize6();

        for (var sweep = 0; sweep < MaxSweeps; sweep++)
        {
            if (GetOffDiagonalNorm6() < NormTolerance) break;

            Rotate6();
        }

        DiagonalMatrix = new double[6, 6];
        EigenVectors = new double[6, 6];
        EigenValues = new double[6];

        // Update diagonal matrix
        DiagonalMatrix[0, 0] = _oaR0C0;
        DiagonalMatrix[0, 1] = _oaR1C0;
        DiagonalMatrix[0, 2] = _oaR2C0;
        DiagonalMatrix[0, 3] = _oaR3C0;
        DiagonalMatrix[0, 4] = _oaR4C0;
        DiagonalMatrix[0, 5] = _oaR5C0;

        DiagonalMatrix[1, 0] = _oaR1C0;
        DiagonalMatrix[1, 1] = _oaR1C1;
        DiagonalMatrix[1, 2] = _oaR2C1;
        DiagonalMatrix[1, 3] = _oaR3C1;
        DiagonalMatrix[1, 4] = _oaR4C1;
        DiagonalMatrix[1, 5] = _oaR5C1;

        DiagonalMatrix[2, 0] = _oaR2C0;
        DiagonalMatrix[2, 1] = _oaR2C1;
        DiagonalMatrix[2, 2] = _oaR2C2;
        DiagonalMatrix[2, 3] = _oaR3C2;
        DiagonalMatrix[2, 4] = _oaR4C2;
        DiagonalMatrix[2, 5] = _oaR5C2;

        DiagonalMatrix[3, 0] = _oaR3C0;
        DiagonalMatrix[3, 1] = _oaR3C1;
        DiagonalMatrix[3, 2] = _oaR3C2;
        DiagonalMatrix[3, 3] = _oaR3C3;
        DiagonalMatrix[3, 4] = _oaR4C3;
        DiagonalMatrix[3, 5] = _oaR5C3;

        DiagonalMatrix[4, 0] = _oaR4C0;
        DiagonalMatrix[4, 1] = _oaR4C1;
        DiagonalMatrix[4, 2] = _oaR4C2;
        DiagonalMatrix[4, 3] = _oaR4C3;
        DiagonalMatrix[4, 4] = _oaR4C4;
        DiagonalMatrix[4, 5] = _oaR5C4;

        DiagonalMatrix[5, 0] = _oaR5C0;
        DiagonalMatrix[5, 1] = _oaR5C1;
        DiagonalMatrix[5, 2] = _oaR5C2;
        DiagonalMatrix[5, 3] = _oaR5C3;
        DiagonalMatrix[5, 4] = _oaR5C4;
        DiagonalMatrix[5, 5] = _oaR5C5;

        // Update eigen vectors matrix
        EigenVectors[0, 0] = _ovR0C0;
        EigenVectors[0, 1] = _ovR0C1;
        EigenVectors[0, 2] = _ovR0C2;
        EigenVectors[0, 3] = _ovR0C3;
        EigenVectors[0, 4] = _ovR0C4;
        EigenVectors[0, 5] = _ovR0C5;

        EigenVectors[1, 0] = _ovR1C0;
        EigenVectors[1, 1] = _ovR1C1;
        EigenVectors[1, 2] = _ovR1C2;
        EigenVectors[1, 3] = _ovR1C3;
        EigenVectors[1, 4] = _ovR1C4;
        EigenVectors[1, 5] = _ovR1C5;

        EigenVectors[2, 0] = _ovR2C0;
        EigenVectors[2, 1] = _ovR2C1;
        EigenVectors[2, 2] = _ovR2C2;
        EigenVectors[2, 3] = _ovR2C3;
        EigenVectors[2, 4] = _ovR2C4;
        EigenVectors[2, 5] = _ovR2C5;

        EigenVectors[3, 0] = _ovR3C0;
        EigenVectors[3, 1] = _ovR3C1;
        EigenVectors[3, 2] = _ovR3C2;
        EigenVectors[3, 3] = _ovR3C3;
        EigenVectors[3, 4] = _ovR3C4;
        EigenVectors[3, 5] = _ovR3C5;

        EigenVectors[4, 0] = _ovR4C0;
        EigenVectors[4, 1] = _ovR4C1;
        EigenVectors[4, 2] = _ovR4C2;
        EigenVectors[4, 3] = _ovR4C3;
        EigenVectors[4, 4] = _ovR4C4;
        EigenVectors[4, 5] = _ovR4C5;

        EigenVectors[5, 0] = _ovR5C0;
        EigenVectors[5, 1] = _ovR5C1;
        EigenVectors[5, 2] = _ovR5C2;
        EigenVectors[5, 3] = _ovR5C3;
        EigenVectors[5, 4] = _ovR5C4;
        EigenVectors[5, 5] = _ovR5C5;

        // Update eigen values
        EigenValues[0] = _oaR0C0;
        EigenValues[1] = _oaR1C1;
        EigenValues[2] = _oaR2C2;
        EigenValues[3] = _oaR3C3;
        EigenValues[4] = _oaR4C4;
        EigenValues[5] = _oaR5C5;

    }

    private void EigenDecomposeN()
    {
        InitializeN();

        for (var sweep = 0; sweep < MaxSweeps; sweep++)
        {
            if (GetOffDiagonalNormN() < NormTolerance)
                break;

            // Apply all pairs of rotations
            RotateN();
        }

        // Extract eigenvalues from diagonal
        for (var i = 0; i < Size; i++)
            EigenValues[i] = DiagonalMatrix[i, i];
    }


    public void EigenDecompose()
    {
        if (Size == 2)
            EigenDecompose2();

        else if (Size == 3)
            EigenDecompose3();

        else if (Size == 4)
            EigenDecompose4();

        else if (Size == 5)
            EigenDecompose5();

        else if (Size == 6)
            EigenDecompose6();

        else
            EigenDecomposeN();

        // Sort eigenvalues and eigenvectors descending
        if (SortEigenValues)
        {
            bool swapFlag;
            do
            {
                swapFlag = false;

                for (var i = 1; i < Size; i++)
                {
                    if (EigenValues[i - 1] < EigenValues[i])
                    {
                        Swap(ref EigenValues[i - 1], ref EigenValues[i]);
                        SwapEigenVectors(i - 1, i);

                        swapFlag = true;
                    }
                }
            } while (swapFlag);
        }
    }

    public void EigenDecomposeManual()
    {
        EigenDecomposeN();

        // Extract eigenvalues from diagonal
        for (var i = 0; i < Size; i++)
            EigenValues[i] = DiagonalMatrix[i, i];

        // Sort eigenvalues and eigenvectors descending
        if (SortEigenValues)
        {
            bool swapFlag;
            do
            {
                swapFlag = false;

                for (var i = 1; i < Size; i++)
                {
                    if (EigenValues[i - 1] < EigenValues[i])
                    {
                        Swap(ref EigenValues[i - 1], ref EigenValues[i]);
                        SwapEigenVectors(i - 1, i);

                        swapFlag = true;
                    }
                }
            } while (swapFlag);
        }
    }

    public void EigenDecomposeLibrary()
    {
        var evd = Matrix.Build.DenseOfArray(SymmetricMatrix).Evd();

        EigenVectors = evd.EigenVectors.ToArray();
        EigenValues = evd.EigenValues.Real().ToArray();
    }


    public void EigenDecomposeStep()
    {
        if (Size == 2)
        {
            Initialize2();
            Rotate2();
            return;
        }

        if (Size == 3)
        {
            Initialize3();
            Rotate3();
            return;
        }

        if (Size == 4)
        {
            Initialize4();
            Rotate4();
            return;
        }

        if (Size == 5)
        {
            Initialize5();
            Rotate5();
            return;
        }

        if (Size == 6)
        {
            Initialize6();
            Rotate6();
            return;
        }

        InitializeN();
        RotateN();
    }

    public void EigenDecomposeStepManual()
    {
        InitializeN();
        RotateN();
    }


    public override string ToString()
    {
        var composer = new StringBuilder();

        composer.AppendLine("Symmetric Matrix: [");

        for (var i = 0; i < Size; i++)
        {
            composer.Append("    ");

            for (var j = 0; j < Size; j++)
            {
                if (j > 0) composer.Append(", ");

                composer.Append(SymmetricMatrix[i, j].ToString("F10"));
            }

            composer.AppendLine(i < Size - 1 ? ";" : "");
        }


        composer.AppendLine("]").AppendLine();


        composer.AppendLine("Diagonal Matrix: [");

        for (var i = 0; i < Size; i++)
        {
            composer.Append("    ");

            for (var j = 0; j < Size; j++)
            {
                if (j > 0) composer.Append(", ");

                composer.Append(DiagonalMatrix[i, j].ToString("F10"));
            }

            composer.AppendLine(i < Size - 1 ? ";" : "");
        }

        composer.AppendLine("]").AppendLine();


        composer.AppendLine("Eigen Vectors Matrix: [");

        for (var i = 0; i < Size; i++)
        {
            composer.Append("    ");

            for (var j = 0; j < Size; j++)
            {
                if (j > 0) composer.Append(", ");

                composer.Append(EigenVectors[i, j].ToString("F10"));
            }

            composer.AppendLine(i < Size - 1 ? ";" : "");
        }

        composer.AppendLine("]").AppendLine();


        composer.AppendLine("Eigen Values: [");

        for (var i = 0; i < Size; i++)
        {
            composer
                .Append("    ")
                .Append(EigenValues[i].ToString("G"))
                .AppendLine(i < Size - 1 ? ";" : "");
        }

        composer.AppendLine("]").AppendLine();


        return composer.ToString();
    }
}