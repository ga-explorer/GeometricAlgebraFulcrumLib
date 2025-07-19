using System.Diagnostics;
using System.Text;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.EllipseFitting;

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


    public int Size { get; }

    public int MaxSweeps { get; set; } = 50;

    public double NormTolerance { get; set; } = 1e-14;

    public bool SortEigenValues { get; set; } = true;

    public double[,] SymmetricMatrix { get; }

    public double[,] DiagonalMatrix { get; }

    public double[] EigenValues { get; }

    public double[,] EigenVectors { get; }


    public JacobiSymmetricEigenDecomposer(double[,] symmetricMatrix)
    {
        Size = symmetricMatrix.GetLength(0);

        Debug.Assert(Size >= 2 && Size == symmetricMatrix.GetLength(1));

        SymmetricMatrix = symmetricMatrix;
        DiagonalMatrix = symmetricMatrix; //new double[Size, Size];
        EigenValues = new double[Size];
        EigenVectors = new double[Size, Size];

        for (var i = 0; i < Size; i++)
            EigenVectors[i, i] = 1;

        //for (var i = 0; i < Size; i++)
        //for (var j = 0; j < Size; j++)
        //{
        //    DiagonalMatrix[i, j] = symmetricMatrix[i, j];
        //}
    }


    /// <summary>
    /// Compute off-diagonal norm
    /// </summary>
    /// <returns></returns>
    private double GetOffDiagonalNorm2()
    {
        return DiagonalMatrix[0, 1] * DiagonalMatrix[0, 1];
    }
    
    /// <summary>
    /// Compute off-diagonal norm
    /// </summary>
    /// <returns></returns>
    private double GetOffDiagonalNorm3()
    {
        return DiagonalMatrix[0, 1] * DiagonalMatrix[0, 1] +
               DiagonalMatrix[0, 2] * DiagonalMatrix[0, 2] +
               DiagonalMatrix[1, 2] * DiagonalMatrix[1, 2];
    }
    
    /// <summary>
    /// Compute off-diagonal norm
    /// </summary>
    /// <returns></returns>
    private double GetOffDiagonalNorm4()
    {
        return DiagonalMatrix[0, 1] * DiagonalMatrix[0, 1] +
               DiagonalMatrix[0, 2] * DiagonalMatrix[0, 2] +
               DiagonalMatrix[0, 3] * DiagonalMatrix[0, 3] +
               DiagonalMatrix[1, 2] * DiagonalMatrix[1, 2] +
               DiagonalMatrix[1, 3] * DiagonalMatrix[1, 3] +
               DiagonalMatrix[2, 3] * DiagonalMatrix[2, 3];
    }
    
    /// <summary>
    /// Compute off-diagonal norm
    /// </summary>
    /// <returns></returns>
    private double GetOffDiagonalNorm5()
    {
        return DiagonalMatrix[0, 1] * DiagonalMatrix[0, 1] +
               DiagonalMatrix[0, 2] * DiagonalMatrix[0, 2] +
               DiagonalMatrix[0, 3] * DiagonalMatrix[0, 3] +
               DiagonalMatrix[0, 4] * DiagonalMatrix[0, 4] +
               DiagonalMatrix[1, 2] * DiagonalMatrix[1, 2] +
               DiagonalMatrix[1, 3] * DiagonalMatrix[1, 3] +
               DiagonalMatrix[1, 4] * DiagonalMatrix[1, 4] +
               DiagonalMatrix[2, 3] * DiagonalMatrix[2, 3] +
               DiagonalMatrix[2, 4] * DiagonalMatrix[2, 4] +
               DiagonalMatrix[3, 4] * DiagonalMatrix[3, 4];
    }
    
    /// <summary>
    /// Compute off-diagonal norm
    /// </summary>
    /// <returns></returns>
    private double GetOffDiagonalNorm6()
    {
        return DiagonalMatrix[0, 1] * DiagonalMatrix[0, 1] +
               DiagonalMatrix[0, 2] * DiagonalMatrix[0, 2] +
               DiagonalMatrix[0, 3] * DiagonalMatrix[0, 3] +
               DiagonalMatrix[0, 4] * DiagonalMatrix[0, 4] +
               DiagonalMatrix[0, 5] * DiagonalMatrix[0, 5] +
               DiagonalMatrix[1, 2] * DiagonalMatrix[1, 2] +
               DiagonalMatrix[1, 3] * DiagonalMatrix[1, 3] +
               DiagonalMatrix[1, 4] * DiagonalMatrix[1, 4] +
               DiagonalMatrix[1, 5] * DiagonalMatrix[1, 5] +
               DiagonalMatrix[2, 3] * DiagonalMatrix[2, 3] +
               DiagonalMatrix[2, 4] * DiagonalMatrix[2, 4] +
               DiagonalMatrix[2, 5] * DiagonalMatrix[2, 5] +
               DiagonalMatrix[3, 4] * DiagonalMatrix[3, 4] +
               DiagonalMatrix[3, 5] * DiagonalMatrix[3, 5] +
               DiagonalMatrix[4, 5] * DiagonalMatrix[4, 5];
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


    private void EigenDecompose2()
    {
        for (var sweep = 0; sweep < MaxSweeps; sweep++)
        {
            if (GetOffDiagonalNorm2() < NormTolerance) break;

            //Begin GA-FuL MetaContext Code Generation, 2025-07-01T00:27:44.9253620+03:00
            //MetaContext: JacobiSymmetricEigen2x2
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
            //   iar0c0 = parameter: DiagonalMatrix[0, 0]
            //   iar0c1 = parameter: DiagonalMatrix[0, 1]
            //   iar1c1 = parameter: DiagonalMatrix[1, 1]
            //   ivr0c0 = parameter: EigenVectors[0, 0]
            //   ivr1c0 = parameter: EigenVectors[1, 0]
            //   ivr0c1 = parameter: EigenVectors[0, 1]
            //   ivr1c1 = parameter: EigenVectors[1, 1]

            var temp0 = -DiagonalMatrix[0, 0] + DiagonalMatrix[1, 1];
            var temp1 = 0.5 * 1 / DiagonalMatrix[0, 1] * temp0;
            var temp2 = Math.Sqrt(1 + temp1 * temp1) + Math.Abs(temp1);
            var temp3 = 1 / temp2;
            var temp4 = DiagonalMatrix[0, 1] * temp3;
            var temp5 = BinaryStep(temp1);
            var temp6 = temp4 * temp5;
            var oaR0C0 = DiagonalMatrix[0, 0] + -temp6;

            var oaR1C1 = DiagonalMatrix[1, 1] + temp6;

            var temp9 = Math.Pow(temp2, -2) * temp5 * temp5;
            var temp10 = 1 / Math.Sqrt(1 + temp9);
            var temp11 = EigenVectors[0, 1] * temp3;
            var temp12 = temp5 * temp11;
            var temp13 = temp10 * temp12;
            var ovR0C0 = EigenVectors[0, 0] * temp10 + -temp13;

            var temp15 = EigenVectors[0, 0] * temp3;
            var temp16 = temp5 * temp15;
            var ovR0C1 = temp10 * (EigenVectors[0, 1] + temp16);

            var temp18 = EigenVectors[1, 1] * temp3;
            var temp19 = temp18 * temp5;
            var temp20 = temp19 * temp10;
            var ovR1C0 = -temp20 + EigenVectors[1, 0] * temp10;

            var temp22 = EigenVectors[1, 0] * temp3;
            var temp23 = temp22 * temp5;
            var ovR1C1 = (EigenVectors[1, 1] + temp23) * temp10;

            var oaR0C1 = 0;

            //Finish GA-FuL MetaContext Code Generation, 2025-07-01T00:27:45.0229094+03:00


            // Update diagonal matrix
            DiagonalMatrix[0, 0] = oaR0C0;
            DiagonalMatrix[0, 1] = oaR0C1;

            DiagonalMatrix[1, 0] = oaR0C1;
            DiagonalMatrix[1, 1] = oaR1C1;

            // Update eigen vectors matrix
            EigenVectors[0, 0] = ovR0C0;
            EigenVectors[0, 1] = ovR0C1;

            EigenVectors[1, 0] = ovR1C0;
            EigenVectors[1, 1] = ovR1C1;


        }
    }

    private void EigenDecompose3()
    {
        for (var sweep = 0; sweep < MaxSweeps; sweep++)
        {
            if (GetOffDiagonalNorm3() < NormTolerance) break;

            //Begin GA-FuL MetaContext Code Generation, 2025-07-01T00:27:46.4333689+03:00
            //MetaContext: JacobiSymmetricEigen3x3
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
            //   iar0c0 = parameter: DiagonalMatrix[0, 0]
            //   iar2c0 = parameter: DiagonalMatrix[2, 0]
            //   iar0c1 = parameter: DiagonalMatrix[0, 1]
            //   iar1c1 = parameter: DiagonalMatrix[1, 1]
            //   iar2c1 = parameter: DiagonalMatrix[2, 1]
            //   iar2c2 = parameter: DiagonalMatrix[2, 2]
            //   ivr0c0 = parameter: EigenVectors[0, 0]
            //   ivr1c0 = parameter: EigenVectors[1, 0]
            //   ivr2c0 = parameter: EigenVectors[2, 0]
            //   ivr0c1 = parameter: EigenVectors[0, 1]
            //   ivr1c1 = parameter: EigenVectors[1, 1]
            //   ivr2c1 = parameter: EigenVectors[2, 1]
            //   ivr0c2 = parameter: EigenVectors[0, 2]
            //   ivr1c2 = parameter: EigenVectors[1, 2]
            //   ivr2c2 = parameter: EigenVectors[2, 2]

            var temp0 = -DiagonalMatrix[0, 0];
            var temp1 = DiagonalMatrix[1, 1] + temp0;
            var temp2 = 0.5 * 1 / DiagonalMatrix[0, 1] * temp1;
            var temp3 = Math.Sqrt(1 + temp2 * temp2) + Math.Abs(temp2);
            var temp4 = 1 / temp3;
            var temp5 = DiagonalMatrix[0, 1] * temp4;
            var temp6 = BinaryStep(temp2);
            var temp7 = temp5 * temp6;
            var temp8 = -temp7;
            var temp9 = DiagonalMatrix[0, 0] + temp8;
            var temp10 = Math.Pow(temp3, -2) * temp6 * temp6;
            var temp11 = 1 / Math.Sqrt(1 + temp10);
            var temp12 = DiagonalMatrix[2, 1] * temp4;
            var temp13 = temp6 * temp12;
            var temp14 = temp11 * temp13;
            var temp15 = DiagonalMatrix[2, 0] * temp11 + -temp14;
            var temp16 = DiagonalMatrix[2, 2] + temp0;
            var temp17 = temp7 + temp16;
            var temp18 = 0.5 * 1 / temp15 * temp17;
            var temp19 = Math.Sqrt(1 + temp18 * temp18) + Math.Abs(temp18);
            var temp20 = 1 / temp19;
            var temp21 = temp15 * temp20;
            var temp22 = BinaryStep(temp18);
            var temp23 = temp21 * temp22;
            var oaR0C0 = temp9 + -temp23;

            var oaR1C2 = 0;

            var temp26 = EigenVectors[0, 1] * temp4;
            var temp27 = temp6 * temp26;
            var temp28 = temp11 * temp27;
            var temp29 = EigenVectors[0, 0] * temp11 + -temp28;
            var temp30 = Math.Pow(temp19, -2) * temp22 * temp22;
            var temp31 = 1 / Math.Sqrt(1 + temp30);
            var temp32 = EigenVectors[0, 2] * temp20;
            var temp33 = temp22 * temp32;
            var temp34 = temp31 * temp33;
            var ovR0C0 = temp29 * temp31 + -temp34;

            var temp36 = EigenVectors[1, 1] * temp4;
            var temp37 = temp6 * temp36;
            var temp38 = temp11 * temp37;
            var temp39 = EigenVectors[1, 0] * temp11 + -temp38;
            var temp40 = EigenVectors[1, 2] * temp20;
            var temp41 = temp22 * temp40;
            var temp42 = temp31 * temp41;
            var ovR1C0 = temp31 * temp39 + -temp42;

            var temp44 = EigenVectors[2, 1] * temp4;
            var temp45 = temp6 * temp44;
            var temp46 = temp11 * temp45;
            var temp47 = EigenVectors[2, 0] * temp11 + -temp46;
            var temp48 = EigenVectors[2, 2] * temp20;
            var temp49 = temp22 * temp48;
            var temp50 = temp31 * temp49;
            var ovR2C0 = temp31 * temp47 + -temp50;

            var temp52 = DiagonalMatrix[1, 1] + temp7;
            var temp53 = DiagonalMatrix[2, 0] * temp4;
            var temp54 = temp6 * temp53;
            var temp55 = temp11 * (DiagonalMatrix[2, 1] + temp54);
            var temp56 = temp31 * temp55;
            var temp57 = DiagonalMatrix[2, 2] + temp23;
            var temp58 = -DiagonalMatrix[1, 1] + temp8;
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
            var oaR0C1 = temp71 * temp73;

            var temp75 = temp62 * temp71;
            var temp76 = temp64 * temp75;
            var oaR0C2 = temp73 * temp76;

            var temp78 = EigenVectors[0, 0] * temp4;
            var temp79 = temp6 * temp78;
            var temp80 = temp11 * (EigenVectors[0, 1] + temp79);
            var temp81 = temp20 * temp29;
            var temp82 = temp22 * temp81;
            var temp83 = temp31 * (EigenVectors[0, 2] + temp82);
            var temp84 = temp62 * temp83;
            var temp85 = temp64 * temp84;
            var temp86 = temp73 * temp85;
            var ovR0C1 = temp73 * temp80 + -temp86;

            var temp88 = temp62 * temp80;
            var temp89 = temp64 * temp88;
            var ovR0C2 = temp73 * (temp83 + temp89);

            var temp91 = EigenVectors[1, 0] * temp4;
            var temp92 = temp6 * temp91;
            var temp93 = temp11 * (EigenVectors[1, 1] + temp92);
            var temp94 = temp20 * temp39;
            var temp95 = temp22 * temp94;
            var temp96 = temp31 * (EigenVectors[1, 2] + temp95);
            var temp97 = temp62 * temp96;
            var temp98 = temp64 * temp97;
            var temp99 = temp73 * temp98;
            var ovR1C1 = temp73 * temp93 + -temp99;

            var temp101 = temp62 * temp93;
            var temp102 = temp64 * temp101;
            var ovR1C2 = temp73 * (temp96 + temp102);

            var temp104 = EigenVectors[2, 0] * temp4;
            var temp105 = temp6 * temp104;
            var temp106 = temp11 * (EigenVectors[2, 1] + temp105);
            var temp107 = temp20 * temp47;
            var temp108 = temp22 * temp107;
            var temp109 = temp31 * (EigenVectors[2, 2] + temp108);
            var temp110 = temp62 * temp109;
            var temp111 = temp64 * temp110;
            var temp112 = temp73 * temp111;
            var ovR2C1 = temp73 * temp106 + -temp112;

            var temp114 = temp62 * temp106;
            var temp115 = temp64 * temp114;
            var ovR2C2 = temp73 * (temp109 + temp115);

            //Finish GA-FuL MetaContext Code Generation, 2025-07-01T00:27:46.4371215+03:00


            // Update diagonal matrix
            DiagonalMatrix[0, 0] = oaR0C0;
            DiagonalMatrix[0, 1] = oaR0C1;
            DiagonalMatrix[0, 2] = oaR0C2;

            DiagonalMatrix[1, 0] = oaR0C1;
            DiagonalMatrix[1, 1] = oaR1C1;
            DiagonalMatrix[1, 2] = oaR1C2;

            DiagonalMatrix[2, 0] = oaR0C2;
            DiagonalMatrix[2, 1] = oaR1C2;
            DiagonalMatrix[2, 2] = oaR2C2;

            // Update eigen vectors matrix
            EigenVectors[0, 0] = ovR0C0;
            EigenVectors[0, 1] = ovR0C1;
            EigenVectors[0, 2] = ovR0C2;

            EigenVectors[1, 0] = ovR1C0;
            EigenVectors[1, 1] = ovR1C1;
            EigenVectors[1, 2] = ovR1C2;

            EigenVectors[2, 0] = ovR2C0;
            EigenVectors[2, 1] = ovR2C1;
            EigenVectors[2, 2] = ovR2C2;


        }
    }

    private void EigenDecompose4()
    {
        for (var sweep = 0; sweep < MaxSweeps; sweep++)
        {
            if (GetOffDiagonalNorm4() < NormTolerance) 
                break;

            //Begin GA-FuL MetaContext Code Generation, 2025-07-01T00:27:50.5711838+03:00
            //MetaContext: JacobiSymmetricEigen4x4
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
            //   iar0c0 = parameter: DiagonalMatrix[0, 0]
            //   iar0c1 = parameter: DiagonalMatrix[0, 1]
            //   iar1c1 = parameter: DiagonalMatrix[1, 1]
            //   iar2c0 = parameter: DiagonalMatrix[2, 0]
            //   iar2c1 = parameter: DiagonalMatrix[2, 1]
            //   iar2c2 = parameter: DiagonalMatrix[2, 2]
            //   iar3c0 = parameter: DiagonalMatrix[3, 0]
            //   iar3c1 = parameter: DiagonalMatrix[3, 1]
            //   iar3c2 = parameter: DiagonalMatrix[3, 2]
            //   iar3c3 = parameter: DiagonalMatrix[3, 3]
            //   ivr0c0 = parameter: EigenVectors[0, 0]
            //   ivr1c0 = parameter: EigenVectors[1, 0]
            //   ivr2c0 = parameter: EigenVectors[2, 0]
            //   ivr3c0 = parameter: EigenVectors[3, 0]
            //   ivr0c1 = parameter: EigenVectors[0, 1]
            //   ivr1c1 = parameter: EigenVectors[1, 1]
            //   ivr2c1 = parameter: EigenVectors[2, 1]
            //   ivr3c1 = parameter: EigenVectors[3, 1]
            //   ivr0c2 = parameter: EigenVectors[0, 2]
            //   ivr1c2 = parameter: EigenVectors[1, 2]
            //   ivr2c2 = parameter: EigenVectors[2, 2]
            //   ivr3c2 = parameter: EigenVectors[3, 2]
            //   ivr0c3 = parameter: EigenVectors[0, 3]
            //   ivr1c3 = parameter: EigenVectors[1, 3]
            //   ivr2c3 = parameter: EigenVectors[2, 3]
            //   ivr3c3 = parameter: EigenVectors[3, 3]

            var oaR2C3 = 0;

            var temp1 = -DiagonalMatrix[0, 0];
            var temp2 = DiagonalMatrix[1, 1] + temp1;
            var temp3 = 0.5 * 1 / DiagonalMatrix[0, 1] * temp2;
            var temp4 = BinaryStep(temp3);
            var temp5 = Math.Sqrt(1 + temp3 * temp3) + Math.Abs(temp3);
            var temp6 = 1 / temp5;
            var temp7 = DiagonalMatrix[0, 1] * temp6;
            var temp8 = temp4 * temp7;
            var temp9 = -temp8;
            var temp10 = DiagonalMatrix[0, 0] + temp9;
            var temp11 = temp4 * temp4 * Math.Pow(temp5, -2);
            var temp12 = 1 / Math.Sqrt(1 + temp11);
            var temp13 = DiagonalMatrix[2, 1] * temp6;
            var temp14 = temp4 * temp13;
            var temp15 = temp12 * temp14;
            var temp16 = DiagonalMatrix[2, 0] * temp12 + -temp15;
            var temp17 = DiagonalMatrix[2, 2] + temp1;
            var temp18 = temp8 + temp17;
            var temp19 = 0.5 * 1 / temp16 * temp18;
            var temp20 = BinaryStep(temp19);
            var temp21 = Math.Sqrt(1 + temp19 * temp19) + Math.Abs(temp19);
            var temp22 = 1 / temp21;
            var temp23 = temp16 * temp22;
            var temp24 = temp20 * temp23;
            var temp25 = -temp24;
            var temp26 = temp10 + temp25;
            var temp27 = DiagonalMatrix[3, 1] * temp6;
            var temp28 = temp4 * temp27;
            var temp29 = temp12 * temp28;
            var temp30 = DiagonalMatrix[3, 0] * temp12 + -temp29;
            var temp31 = temp20 * temp20 * Math.Pow(temp21, -2);
            var temp32 = 1 / Math.Sqrt(1 + temp31);
            var temp33 = DiagonalMatrix[3, 2] * temp22;
            var temp34 = temp20 * temp33;
            var temp35 = temp32 * temp34;
            var temp36 = temp30 * temp32 + -temp35;
            var temp37 = DiagonalMatrix[3, 3] + temp1;
            var temp38 = temp8 + temp37;
            var temp39 = temp24 + temp38;
            var temp40 = 0.5 * 1 / temp36 * temp39;
            var temp41 = Math.Sqrt(1 + temp40 * temp40) + Math.Abs(temp40);
            var temp42 = 1 / temp41;
            var temp43 = temp36 * temp42;
            var temp44 = BinaryStep(temp40);
            var temp45 = temp43 * temp44;
            var oaR0C0 = temp26 + -temp45;

            var temp47 = EigenVectors[0, 1] * temp6;
            var temp48 = temp4 * temp47;
            var temp49 = temp12 * temp48;
            var temp50 = EigenVectors[0, 0] * temp12 + -temp49;
            var temp51 = EigenVectors[0, 2] * temp22;
            var temp52 = temp20 * temp51;
            var temp53 = temp32 * temp52;
            var temp54 = temp32 * temp50 + -temp53;
            var temp55 = Math.Pow(temp41, -2) * temp44 * temp44;
            var temp56 = 1 / Math.Sqrt(1 + temp55);
            var temp57 = EigenVectors[0, 3] * temp42;
            var temp58 = temp44 * temp57;
            var temp59 = temp56 * temp58;
            var ovR0C0 = temp54 * temp56 + -temp59;

            var temp61 = EigenVectors[1, 1] * temp6;
            var temp62 = temp4 * temp61;
            var temp63 = temp12 * temp62;
            var temp64 = EigenVectors[1, 0] * temp12 + -temp63;
            var temp65 = EigenVectors[1, 2] * temp22;
            var temp66 = temp20 * temp65;
            var temp67 = temp32 * temp66;
            var temp68 = temp32 * temp64 + -temp67;
            var temp69 = EigenVectors[1, 3] * temp42;
            var temp70 = temp44 * temp69;
            var temp71 = temp56 * temp70;
            var ovR1C0 = temp56 * temp68 + -temp71;

            var temp73 = EigenVectors[2, 2] * temp22;
            var temp74 = temp20 * temp73;
            var temp75 = temp32 * temp74;
            var temp76 = EigenVectors[2, 1] * temp6;
            var temp77 = temp4 * temp76;
            var temp78 = temp12 * temp77;
            var temp79 = EigenVectors[2, 0] * temp12 + -temp78;
            var temp80 = -temp75 + temp32 * temp79;
            var temp81 = EigenVectors[2, 3] * temp42;
            var temp82 = temp44 * temp81;
            var temp83 = temp56 * temp82;
            var ovR2C0 = temp56 * temp80 + -temp83;

            var temp85 = EigenVectors[3, 1] * temp6;
            var temp86 = temp4 * temp85;
            var temp87 = temp12 * temp86;
            var temp88 = EigenVectors[3, 0] * temp12 + -temp87;
            var temp89 = EigenVectors[3, 2] * temp22;
            var temp90 = temp20 * temp89;
            var temp91 = temp32 * temp90;
            var temp92 = temp32 * temp88 + -temp91;
            var temp93 = EigenVectors[3, 3] * temp42;
            var temp94 = temp44 * temp93;
            var temp95 = temp56 * temp94;
            var ovR3C0 = temp56 * temp92 + -temp95;

            var temp97 = DiagonalMatrix[1, 1] + temp8;
            var temp98 = DiagonalMatrix[2, 0] * temp6;
            var temp99 = temp4 * temp98;
            var temp100 = temp12 * (DiagonalMatrix[2, 1] + temp99);
            var temp101 = temp32 * temp100;
            var temp102 = DiagonalMatrix[2, 2] + temp24;
            var temp103 = -DiagonalMatrix[1, 1];
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
            var temp114 = DiagonalMatrix[3, 0] * temp6;
            var temp115 = temp4 * temp114;
            var temp116 = temp12 * (DiagonalMatrix[3, 1] + temp115);
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
            var temp128 = temp32 * (DiagonalMatrix[3, 2] + temp127);
            var temp129 = temp128 * temp56;
            var temp130 = temp129 * temp108;
            var temp131 = temp130 * temp110;
            var temp132 = temp131 * temp125;
            var temp133 = -temp132 + temp123 * temp125;
            var temp134 = DiagonalMatrix[3, 3] + temp103;
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
            var oaR0C1 = temp156 * temp158;

            var temp160 = EigenVectors[0, 0] * temp6;
            var temp161 = temp160 * temp4;
            var temp162 = (EigenVectors[0, 1] + temp161) * temp12;
            var temp163 = temp22 * temp50;
            var temp164 = temp163 * temp20;
            var temp165 = (EigenVectors[0, 2] + temp164) * temp32;
            var temp166 = temp165 * temp108;
            var temp167 = temp166 * temp110;
            var temp168 = temp167 * temp125;
            var temp169 = -temp168 + temp162 * temp125;
            var temp170 = temp42 * temp54;
            var temp171 = temp170 * temp44;
            var temp172 = (EigenVectors[0, 3] + temp171) * temp56;
            var temp173 = temp140 * temp172;
            var temp174 = temp142 * temp173;
            var temp175 = temp158 * temp174;
            var ovR0C1 = temp158 * temp169 + -temp175;

            var temp177 = EigenVectors[1, 0] * temp6;
            var temp178 = temp177 * temp4;
            var temp179 = (EigenVectors[1, 1] + temp178) * temp12;
            var temp180 = temp22 * temp64;
            var temp181 = temp180 * temp20;
            var temp182 = (EigenVectors[1, 2] + temp181) * temp32;
            var temp183 = temp182 * temp108;
            var temp184 = temp183 * temp110;
            var temp185 = temp184 * temp125;
            var temp186 = -temp185 + temp179 * temp125;
            var temp187 = temp42 * temp68;
            var temp188 = temp187 * temp44;
            var temp189 = (EigenVectors[1, 3] + temp188) * temp56;
            var temp190 = temp140 * temp189;
            var temp191 = temp142 * temp190;
            var temp192 = temp158 * temp191;
            var ovR1C1 = temp158 * temp186 + -temp192;

            var temp194 = EigenVectors[2, 0] * temp6;
            var temp195 = temp194 * temp4;
            var temp196 = (EigenVectors[2, 1] + temp195) * temp12;
            var temp197 = temp22 * temp79;
            var temp198 = temp197 * temp20;
            var temp199 = (EigenVectors[2, 2] + temp198) * temp32;
            var temp200 = temp199 * temp108;
            var temp201 = temp200 * temp110;
            var temp202 = temp201 * temp125;
            var temp203 = -temp202 + temp196 * temp125;
            var temp204 = temp42 * temp80;
            var temp205 = temp204 * temp44;
            var temp206 = (EigenVectors[2, 3] + temp205) * temp56;
            var temp207 = temp140 * temp206;
            var temp208 = temp142 * temp207;
            var temp209 = temp158 * temp208;
            var ovR2C1 = temp158 * temp203 + -temp209;

            var temp211 = EigenVectors[3, 0] * temp6;
            var temp212 = temp211 * temp4;
            var temp213 = (EigenVectors[3, 1] + temp212) * temp12;
            var temp214 = temp22 * temp88;
            var temp215 = temp214 * temp20;
            var temp216 = (EigenVectors[3, 2] + temp215) * temp32;
            var temp217 = temp216 * temp108;
            var temp218 = temp217 * temp110;
            var temp219 = temp218 * temp125;
            var temp220 = -temp219 + temp213 * temp125;
            var temp221 = temp42 * temp92;
            var temp222 = temp221 * temp44;
            var temp223 = (EigenVectors[3, 3] + temp222) * temp56;
            var temp224 = temp140 * temp223;
            var temp225 = temp142 * temp224;
            var temp226 = temp158 * temp225;
            var ovR3C1 = temp158 * temp220 + -temp226;

            var temp228 = temp102 + temp111;
            var temp229 = temp108 * temp123;
            var temp230 = temp229 * temp110;
            var temp231 = (temp129 + temp230) * temp125;
            var temp232 = temp158 * temp231;
            var temp233 = DiagonalMatrix[3, 3] + temp45;
            var temp234 = temp143 + temp233;
            var temp235 = -DiagonalMatrix[2, 2] + temp25;
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
            var oaR0C2 = temp248 * temp250 + -temp256;

            var temp258 = temp240 * temp248;
            var temp259 = temp242 * temp258;
            var oaR0C3 = temp250 * (temp253 + temp259);

            var temp261 = temp140 * temp231;
            var temp262 = temp142 * temp261;
            var temp263 = temp158 * temp262;
            var temp264 = -temp263;
            var oaR1C2 = temp250 * temp264;

            var temp266 = temp240 * temp264;
            var temp267 = temp242 * temp266;
            var oaR1C3 = temp250 * temp267;

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

            //Finish GA-FuL MetaContext Code Generation, 2025-07-01T00:27:50.5723470+03:00


            // Update diagonal matrix
            DiagonalMatrix[0, 0] = oaR0C0;
            DiagonalMatrix[0, 1] = oaR0C1;
            DiagonalMatrix[0, 2] = oaR0C2;
            DiagonalMatrix[0, 3] = oaR0C3;

            DiagonalMatrix[1, 0] = oaR0C1;
            DiagonalMatrix[1, 1] = oaR1C1;
            DiagonalMatrix[1, 2] = oaR1C2;
            DiagonalMatrix[1, 3] = oaR1C3;

            DiagonalMatrix[2, 0] = oaR0C2;
            DiagonalMatrix[2, 1] = oaR1C2;
            DiagonalMatrix[2, 2] = oaR2C2;
            DiagonalMatrix[2, 3] = oaR2C3;

            DiagonalMatrix[3, 0] = oaR0C3;
            DiagonalMatrix[3, 1] = oaR1C3;
            DiagonalMatrix[3, 2] = oaR2C3;
            DiagonalMatrix[3, 3] = oaR3C3;

            // Update eigen vectors matrix
            EigenVectors[0, 0] = ovR0C0;
            EigenVectors[0, 1] = ovR0C1;
            EigenVectors[0, 2] = ovR0C2;
            EigenVectors[0, 3] = ovR0C3;

            EigenVectors[1, 0] = ovR1C0;
            EigenVectors[1, 1] = ovR1C1;
            EigenVectors[1, 2] = ovR1C2;
            EigenVectors[1, 3] = ovR1C3;

            EigenVectors[2, 0] = ovR2C0;
            EigenVectors[2, 1] = ovR2C1;
            EigenVectors[2, 2] = ovR2C2;
            EigenVectors[2, 3] = ovR2C3;

            EigenVectors[3, 0] = ovR3C0;
            EigenVectors[3, 1] = ovR3C1;
            EigenVectors[3, 2] = ovR3C2;
            EigenVectors[3, 3] = ovR3C3;


        }
    }

    private void EigenDecompose5()
    {
        for (var sweep = 0; sweep < MaxSweeps; sweep++)
        {
            if (GetOffDiagonalNorm5() < NormTolerance) break;

            //Begin GA-FuL MetaContext Code Generation, 2025-07-01T00:28:00.1197177+03:00
            //MetaContext: JacobiSymmetricEigen5x5
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
            //   iar0c0 = parameter: DiagonalMatrix[0, 0]
            //   iar2c0 = parameter: DiagonalMatrix[2, 0]
            //   iar3c0 = parameter: DiagonalMatrix[3, 0]
            //   iar4c0 = parameter: DiagonalMatrix[4, 0]
            //   iar0c1 = parameter: DiagonalMatrix[0, 1]
            //   iar1c1 = parameter: DiagonalMatrix[1, 1]
            //   iar2c1 = parameter: DiagonalMatrix[2, 1]
            //   iar3c1 = parameter: DiagonalMatrix[3, 1]
            //   iar4c1 = parameter: DiagonalMatrix[4, 1]
            //   iar2c2 = parameter: DiagonalMatrix[2, 2]
            //   iar3c2 = parameter: DiagonalMatrix[3, 2]
            //   iar4c2 = parameter: DiagonalMatrix[4, 2]
            //   iar3c3 = parameter: DiagonalMatrix[3, 3]
            //   iar4c3 = parameter: DiagonalMatrix[4, 3]
            //   iar4c4 = parameter: DiagonalMatrix[4, 4]
            //   ivr0c0 = parameter: EigenVectors[0, 0]
            //   ivr1c0 = parameter: EigenVectors[1, 0]
            //   ivr2c0 = parameter: EigenVectors[2, 0]
            //   ivr3c0 = parameter: EigenVectors[3, 0]
            //   ivr4c0 = parameter: EigenVectors[4, 0]
            //   ivr0c1 = parameter: EigenVectors[0, 1]
            //   ivr1c1 = parameter: EigenVectors[1, 1]
            //   ivr2c1 = parameter: EigenVectors[2, 1]
            //   ivr3c1 = parameter: EigenVectors[3, 1]
            //   ivr4c1 = parameter: EigenVectors[4, 1]
            //   ivr0c2 = parameter: EigenVectors[0, 2]
            //   ivr1c2 = parameter: EigenVectors[1, 2]
            //   ivr2c2 = parameter: EigenVectors[2, 2]
            //   ivr3c2 = parameter: EigenVectors[3, 2]
            //   ivr4c2 = parameter: EigenVectors[4, 2]
            //   ivr0c3 = parameter: EigenVectors[0, 3]
            //   ivr1c3 = parameter: EigenVectors[1, 3]
            //   ivr2c3 = parameter: EigenVectors[2, 3]
            //   ivr3c3 = parameter: EigenVectors[3, 3]
            //   ivr4c3 = parameter: EigenVectors[4, 3]
            //   ivr0c4 = parameter: EigenVectors[0, 4]
            //   ivr1c4 = parameter: EigenVectors[1, 4]
            //   ivr2c4 = parameter: EigenVectors[2, 4]
            //   ivr3c4 = parameter: EigenVectors[3, 4]
            //   ivr4c4 = parameter: EigenVectors[4, 4]

            var oaR3C4 = 0;

            var temp1 = -DiagonalMatrix[0, 0];
            var temp2 = DiagonalMatrix[1, 1] + temp1;
            var temp3 = 0.5 * 1 / DiagonalMatrix[0, 1] * temp2;
            var temp4 = BinaryStep(temp3);
            var temp5 = Math.Sqrt(1 + temp3 * temp3) + Math.Abs(temp3);
            var temp6 = 1 / temp5;
            var temp7 = DiagonalMatrix[0, 1] * temp6;
            var temp8 = temp4 * temp7;
            var temp9 = -temp8;
            var temp10 = DiagonalMatrix[0, 0] + temp9;
            var temp11 = temp4 * temp4 * Math.Pow(temp5, -2);
            var temp12 = 1 / Math.Sqrt(1 + temp11);
            var temp13 = DiagonalMatrix[2, 1] * temp6;
            var temp14 = temp4 * temp13;
            var temp15 = temp12 * temp14;
            var temp16 = DiagonalMatrix[2, 0] * temp12 + -temp15;
            var temp17 = DiagonalMatrix[2, 2] + temp1;
            var temp18 = temp8 + temp17;
            var temp19 = 0.5 * 1 / temp16 * temp18;
            var temp20 = BinaryStep(temp19);
            var temp21 = Math.Sqrt(1 + temp19 * temp19) + Math.Abs(temp19);
            var temp22 = 1 / temp21;
            var temp23 = temp16 * temp22;
            var temp24 = temp20 * temp23;
            var temp25 = -temp24;
            var temp26 = temp10 + temp25;
            var temp27 = DiagonalMatrix[3, 1] * temp6;
            var temp28 = temp4 * temp27;
            var temp29 = temp12 * temp28;
            var temp30 = DiagonalMatrix[3, 0] * temp12 + -temp29;
            var temp31 = temp20 * temp20 * Math.Pow(temp21, -2);
            var temp32 = 1 / Math.Sqrt(1 + temp31);
            var temp33 = DiagonalMatrix[3, 2] * temp22;
            var temp34 = temp20 * temp33;
            var temp35 = temp32 * temp34;
            var temp36 = temp30 * temp32 + -temp35;
            var temp37 = DiagonalMatrix[3, 3] + temp1;
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
            var temp48 = DiagonalMatrix[4, 2] * temp22;
            var temp49 = temp20 * temp48;
            var temp50 = temp32 * temp49;
            var temp51 = DiagonalMatrix[4, 1] * temp6;
            var temp52 = temp4 * temp51;
            var temp53 = temp12 * temp52;
            var temp54 = DiagonalMatrix[4, 0] * temp12 + -temp53;
            var temp55 = -temp50 + temp32 * temp54;
            var temp56 = Math.Pow(temp41, -2) * temp44 * temp44;
            var temp57 = 1 / Math.Sqrt(1 + temp56);
            var temp58 = DiagonalMatrix[4, 3] * temp42;
            var temp59 = temp44 * temp58;
            var temp60 = temp57 * temp59;
            var temp61 = temp55 * temp57 + -temp60;
            var temp62 = DiagonalMatrix[4, 4] + temp1;
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

            var temp73 = EigenVectors[0, 1] * temp6;
            var temp74 = temp4 * temp73;
            var temp75 = temp12 * temp74;
            var temp76 = EigenVectors[0, 0] * temp12 + -temp75;
            var temp77 = EigenVectors[0, 2] * temp22;
            var temp78 = temp20 * temp77;
            var temp79 = temp32 * temp78;
            var temp80 = temp32 * temp76 + -temp79;
            var temp81 = EigenVectors[0, 3] * temp42;
            var temp82 = temp44 * temp81;
            var temp83 = temp57 * temp82;
            var temp84 = temp57 * temp80 + -temp83;
            var temp85 = Math.Pow(temp67, -2) * temp70 * temp70;
            var temp86 = 1 / Math.Sqrt(1 + temp85);
            var temp87 = EigenVectors[0, 4] * temp68;
            var temp88 = temp70 * temp87;
            var temp89 = temp86 * temp88;
            var ovR0C0 = temp84 * temp86 + -temp89;

            var temp91 = EigenVectors[1, 1] * temp6;
            var temp92 = temp4 * temp91;
            var temp93 = temp12 * temp92;
            var temp94 = EigenVectors[1, 0] * temp12 + -temp93;
            var temp95 = EigenVectors[1, 2] * temp22;
            var temp96 = temp20 * temp95;
            var temp97 = temp32 * temp96;
            var temp98 = temp32 * temp94 + -temp97;
            var temp99 = EigenVectors[1, 3] * temp42;
            var temp100 = temp44 * temp99;
            var temp101 = temp57 * temp100;
            var temp102 = temp57 * temp98 + -temp101;
            var temp103 = EigenVectors[1, 4] * temp68;
            var temp104 = temp70 * temp103;
            var temp105 = temp86 * temp104;
            var ovR1C0 = temp86 * temp102 + -temp105;

            var temp107 = EigenVectors[2, 1] * temp6;
            var temp108 = temp4 * temp107;
            var temp109 = temp12 * temp108;
            var temp110 = EigenVectors[2, 0] * temp12 + -temp109;
            var temp111 = EigenVectors[2, 2] * temp22;
            var temp112 = temp20 * temp111;
            var temp113 = temp32 * temp112;
            var temp114 = temp32 * temp110 + -temp113;
            var temp115 = EigenVectors[2, 3] * temp42;
            var temp116 = temp44 * temp115;
            var temp117 = temp57 * temp116;
            var temp118 = temp57 * temp114 + -temp117;
            var temp119 = EigenVectors[2, 4] * temp68;
            var temp120 = temp70 * temp119;
            var temp121 = temp86 * temp120;
            var ovR2C0 = temp86 * temp118 + -temp121;

            var temp123 = EigenVectors[3, 1] * temp6;
            var temp124 = temp4 * temp123;
            var temp125 = temp12 * temp124;
            var temp126 = EigenVectors[3, 0] * temp12 + -temp125;
            var temp127 = EigenVectors[3, 2] * temp22;
            var temp128 = temp20 * temp127;
            var temp129 = temp32 * temp128;
            var temp130 = temp32 * temp126 + -temp129;
            var temp131 = EigenVectors[3, 3] * temp42;
            var temp132 = temp44 * temp131;
            var temp133 = temp57 * temp132;
            var temp134 = temp57 * temp130 + -temp133;
            var temp135 = EigenVectors[3, 4] * temp68;
            var temp136 = temp70 * temp135;
            var temp137 = temp86 * temp136;
            var ovR3C0 = temp86 * temp134 + -temp137;

            var temp139 = EigenVectors[4, 1] * temp6;
            var temp140 = temp4 * temp139;
            var temp141 = temp12 * temp140;
            var temp142 = EigenVectors[4, 0] * temp12 + -temp141;
            var temp143 = EigenVectors[4, 2] * temp22;
            var temp144 = temp20 * temp143;
            var temp145 = temp32 * temp144;
            var temp146 = temp32 * temp142 + -temp145;
            var temp147 = EigenVectors[4, 3] * temp42;
            var temp148 = temp44 * temp147;
            var temp149 = temp57 * temp148;
            var temp150 = temp57 * temp146 + -temp149;
            var temp151 = EigenVectors[4, 4] * temp68;
            var temp152 = temp70 * temp151;
            var temp153 = temp86 * temp152;
            var ovR4C0 = temp86 * temp150 + -temp153;

            var temp155 = temp22 * temp30;
            var temp156 = temp20 * temp155;
            var temp157 = temp32 * (DiagonalMatrix[3, 2] + temp156);
            var temp158 = temp42 * temp157;
            var temp159 = temp44 * temp158;
            var temp160 = temp57 * temp159;
            var temp161 = -temp160;
            var temp162 = temp68 * temp161;
            var temp163 = temp70 * temp162;
            var temp164 = temp22 * temp54;
            var temp165 = temp20 * temp164;
            var temp166 = temp32 * (DiagonalMatrix[4, 2] + temp165);
            var temp167 = temp86 * (temp163 + temp166);
            var temp168 = DiagonalMatrix[2, 2] + temp24;
            var temp169 = -DiagonalMatrix[1, 1];
            var temp170 = temp9 + temp169;
            var temp171 = temp168 + temp170;
            var temp172 = DiagonalMatrix[2, 0] * temp6;
            var temp173 = temp4 * temp172;
            var temp174 = temp12 * (DiagonalMatrix[2, 1] + temp173);
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
            var temp185 = DiagonalMatrix[4, 0] * temp6;
            var temp186 = temp4 * temp185;
            var temp187 = temp12 * (DiagonalMatrix[4, 1] + temp186);
            var temp188 = temp22 * temp174;
            var temp189 = temp20 * temp188;
            var temp190 = temp32 * temp189;
            var temp191 = -temp190;
            var temp192 = DiagonalMatrix[3, 0] * temp6;
            var temp193 = temp4 * temp192;
            var temp194 = temp12 * (DiagonalMatrix[3, 1] + temp193);
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
            var temp211 = DiagonalMatrix[3, 3] + temp169;
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
            var temp224 = temp57 * (DiagonalMatrix[4, 3] + temp223);
            var temp225 = temp86 * temp224;
            var temp226 = 1 / temp218;
            var temp227 = temp225 * temp226;
            var temp228 = temp219 * temp227;
            var temp229 = temp221 * temp228;
            var temp230 = temp202 * temp221 + -temp229;
            var temp231 = DiagonalMatrix[4, 4] + temp169;
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
            var temp244 = DiagonalMatrix[1, 1] + -temp243;
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
            var oaR0C1 = temp269 * temp271;

            var temp273 = EigenVectors[0, 0] * temp6;
            var temp274 = temp4 * temp273;
            var temp275 = temp12 * (EigenVectors[0, 1] + temp274);
            var temp276 = temp22 * temp76;
            var temp277 = temp20 * temp276;
            var temp278 = temp32 * (EigenVectors[0, 2] + temp277);
            var temp279 = temp178 * temp278;
            var temp280 = temp180 * temp279;
            var temp281 = temp183 * temp280;
            var temp282 = temp183 * temp275 + -temp281;
            var temp283 = temp42 * temp80;
            var temp284 = temp44 * temp283;
            var temp285 = temp57 * (EigenVectors[0, 3] + temp284);
            var temp286 = temp226 * temp285;
            var temp287 = temp219 * temp286;
            var temp288 = temp221 * temp287;
            var temp289 = temp221 * temp282 + -temp288;
            var temp290 = temp68 * temp84;
            var temp291 = temp70 * temp290;
            var temp292 = temp86 * (EigenVectors[0, 4] + temp291);
            var temp293 = temp240 * temp292;
            var temp294 = temp242 * temp293;
            var temp295 = temp271 * temp294;
            var ovR0C1 = temp271 * temp289 + -temp295;

            var temp297 = EigenVectors[1, 0] * temp6;
            var temp298 = temp4 * temp297;
            var temp299 = temp12 * (EigenVectors[1, 1] + temp298);
            var temp300 = temp22 * temp94;
            var temp301 = temp20 * temp300;
            var temp302 = temp32 * (EigenVectors[1, 2] + temp301);
            var temp303 = temp178 * temp302;
            var temp304 = temp180 * temp303;
            var temp305 = temp183 * temp304;
            var temp306 = temp183 * temp299 + -temp305;
            var temp307 = temp42 * temp98;
            var temp308 = temp44 * temp307;
            var temp309 = temp57 * (EigenVectors[1, 3] + temp308);
            var temp310 = temp226 * temp309;
            var temp311 = temp219 * temp310;
            var temp312 = temp221 * temp311;
            var temp313 = temp221 * temp306 + -temp312;
            var temp314 = temp68 * temp102;
            var temp315 = temp70 * temp314;
            var temp316 = temp86 * (EigenVectors[1, 4] + temp315);
            var temp317 = temp240 * temp316;
            var temp318 = temp242 * temp317;
            var temp319 = temp271 * temp318;
            var ovR1C1 = temp271 * temp313 + -temp319;

            var temp321 = EigenVectors[2, 0] * temp6;
            var temp322 = temp4 * temp321;
            var temp323 = temp12 * (EigenVectors[2, 1] + temp322);
            var temp324 = temp22 * temp110;
            var temp325 = temp20 * temp324;
            var temp326 = temp32 * (EigenVectors[2, 2] + temp325);
            var temp327 = temp178 * temp326;
            var temp328 = temp180 * temp327;
            var temp329 = temp183 * temp328;
            var temp330 = temp183 * temp323 + -temp329;
            var temp331 = temp42 * temp114;
            var temp332 = temp44 * temp331;
            var temp333 = temp57 * (EigenVectors[2, 3] + temp332);
            var temp334 = temp226 * temp333;
            var temp335 = temp219 * temp334;
            var temp336 = temp221 * temp335;
            var temp337 = temp221 * temp330 + -temp336;
            var temp338 = temp68 * temp118;
            var temp339 = temp70 * temp338;
            var temp340 = temp86 * (EigenVectors[2, 4] + temp339);
            var temp341 = temp240 * temp340;
            var temp342 = temp242 * temp341;
            var temp343 = temp271 * temp342;
            var ovR2C1 = temp271 * temp337 + -temp343;

            var temp345 = EigenVectors[3, 0] * temp6;
            var temp346 = temp4 * temp345;
            var temp347 = temp12 * (EigenVectors[3, 1] + temp346);
            var temp348 = temp22 * temp126;
            var temp349 = temp20 * temp348;
            var temp350 = temp32 * (EigenVectors[3, 2] + temp349);
            var temp351 = temp178 * temp350;
            var temp352 = temp180 * temp351;
            var temp353 = temp183 * temp352;
            var temp354 = temp183 * temp347 + -temp353;
            var temp355 = temp42 * temp130;
            var temp356 = temp44 * temp355;
            var temp357 = temp57 * (EigenVectors[3, 3] + temp356);
            var temp358 = temp226 * temp357;
            var temp359 = temp219 * temp358;
            var temp360 = temp221 * temp359;
            var temp361 = temp221 * temp354 + -temp360;
            var temp362 = temp68 * temp134;
            var temp363 = temp70 * temp362;
            var temp364 = temp86 * (EigenVectors[3, 4] + temp363);
            var temp365 = temp240 * temp364;
            var temp366 = temp242 * temp365;
            var temp367 = temp271 * temp366;
            var ovR3C1 = temp271 * temp361 + -temp367;

            var temp369 = EigenVectors[4, 0] * temp6;
            var temp370 = temp4 * temp369;
            var temp371 = temp12 * (EigenVectors[4, 1] + temp370);
            var temp372 = temp22 * temp142;
            var temp373 = temp20 * temp372;
            var temp374 = temp32 * (EigenVectors[4, 2] + temp373);
            var temp375 = temp178 * temp374;
            var temp376 = temp180 * temp375;
            var temp377 = temp183 * temp376;
            var temp378 = temp183 * temp371 + -temp377;
            var temp379 = temp42 * temp146;
            var temp380 = temp44 * temp379;
            var temp381 = temp57 * (EigenVectors[4, 3] + temp380);
            var temp382 = temp226 * temp381;
            var temp383 = temp219 * temp382;
            var temp384 = temp221 * temp383;
            var temp385 = temp221 * temp378 + -temp384;
            var temp386 = temp68 * temp150;
            var temp387 = temp70 * temp386;
            var temp388 = temp86 * (EigenVectors[4, 4] + temp387);
            var temp389 = temp240 * temp388;
            var temp390 = temp242 * temp389;
            var temp391 = temp271 * temp390;
            var ovR4C1 = temp271 * temp385 + -temp391;

            var temp393 = temp178 * temp205;
            var temp394 = temp180 * temp393;
            var temp395 = temp183 * (temp206 + temp394);
            var temp396 = temp221 * temp395;
            var temp397 = -DiagonalMatrix[2, 2];
            var temp398 = temp25 + temp397;
            var temp399 = temp246 + temp398;
            var temp400 = DiagonalMatrix[3, 3] + temp399;
            var temp401 = temp45 + temp400;
            var temp402 = temp236 + temp401;
            var temp403 = 0.5 * 1 / temp396 * temp402;
            var temp404 = Math.Sqrt(1 + temp403 * temp403) + Math.Abs(temp403);
            var temp405 = 1 / temp404;
            var temp406 = temp396 * temp405;
            var temp407 = BinaryStep(temp403);
            var temp408 = temp406 * temp407;
            var temp409 = -temp408;
            var temp410 = DiagonalMatrix[2, 2] + temp409;
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
            var temp431 = DiagonalMatrix[4, 4] + temp397;
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
            var oaR0C2 = temp455 * temp457 + -temp463;

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
            var oaR1C2 = temp457 * temp476;

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

            var temp563 = DiagonalMatrix[3, 3] + temp408;
            var temp564 = temp405 * temp420;
            var temp565 = temp407 * temp564;
            var temp566 = temp422 * (temp426 + temp565);
            var temp567 = temp457 * temp566;
            var temp568 = DiagonalMatrix[4, 4] + temp243;
            var temp569 = temp442 + temp568;
            var temp570 = -DiagonalMatrix[3, 3] + temp409;
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
            var oaR0C3 = temp588 * temp590 + -temp596;

            var temp598 = temp577 * temp588;
            var temp599 = temp579 * temp598;
            var oaR0C4 = temp590 * (temp593 + temp599);

            var temp601 = temp405 * temp468;
            var temp602 = temp407 * temp601;
            var temp603 = temp422 * (temp472 + temp602);
            var temp604 = temp439 * temp476;
            var temp605 = temp441 * temp604;
            var temp606 = temp457 * temp605;
            var temp607 = temp577 * temp606;
            var temp608 = temp579 * temp607;
            var temp609 = temp590 * temp608;
            var oaR1C3 = temp590 * temp603 + -temp609;

            var temp611 = temp577 * temp603;
            var temp612 = temp579 * temp611;
            var oaR1C4 = temp590 * (temp606 + temp612);

            var temp614 = temp439 * temp566;
            var temp615 = temp441 * temp614;
            var temp616 = temp457 * temp615;
            var temp617 = -temp616;
            var oaR2C3 = temp590 * temp617;

            var temp619 = temp577 * temp617;
            var temp620 = temp579 * temp619;
            var oaR2C4 = temp590 * temp620;

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

            //Finish GA-FuL MetaContext Code Generation, 2025-07-01T00:28:00.1213284+03:00


            // Update diagonal matrix
            DiagonalMatrix[0, 0] = oaR0C0;
            DiagonalMatrix[0, 1] = oaR0C1;
            DiagonalMatrix[0, 2] = oaR0C2;
            DiagonalMatrix[0, 3] = oaR0C3;
            DiagonalMatrix[0, 4] = oaR0C4;

            DiagonalMatrix[1, 0] = oaR0C1;
            DiagonalMatrix[1, 1] = oaR1C1;
            DiagonalMatrix[1, 2] = oaR1C2;
            DiagonalMatrix[1, 3] = oaR1C3;
            DiagonalMatrix[1, 4] = oaR1C4;

            DiagonalMatrix[2, 0] = oaR0C2;
            DiagonalMatrix[2, 1] = oaR1C2;
            DiagonalMatrix[2, 2] = oaR2C2;
            DiagonalMatrix[2, 3] = oaR2C3;
            DiagonalMatrix[2, 4] = oaR2C4;

            DiagonalMatrix[3, 0] = oaR0C3;
            DiagonalMatrix[3, 1] = oaR1C3;
            DiagonalMatrix[3, 2] = oaR2C3;
            DiagonalMatrix[3, 3] = oaR3C3;
            DiagonalMatrix[3, 4] = oaR3C4;

            DiagonalMatrix[4, 0] = oaR0C4;
            DiagonalMatrix[4, 1] = oaR1C4;
            DiagonalMatrix[4, 2] = oaR2C4;
            DiagonalMatrix[4, 3] = oaR3C4;
            DiagonalMatrix[4, 4] = oaR4C4;

            // Update eigen vectors matrix
            EigenVectors[0, 0] = ovR0C0;
            EigenVectors[0, 1] = ovR0C1;
            EigenVectors[0, 2] = ovR0C2;
            EigenVectors[0, 3] = ovR0C3;
            EigenVectors[0, 4] = ovR0C4;

            EigenVectors[1, 0] = ovR1C0;
            EigenVectors[1, 1] = ovR1C1;
            EigenVectors[1, 2] = ovR1C2;
            EigenVectors[1, 3] = ovR1C3;
            EigenVectors[1, 4] = ovR1C4;

            EigenVectors[2, 0] = ovR2C0;
            EigenVectors[2, 1] = ovR2C1;
            EigenVectors[2, 2] = ovR2C2;
            EigenVectors[2, 3] = ovR2C3;
            EigenVectors[2, 4] = ovR2C4;

            EigenVectors[3, 0] = ovR3C0;
            EigenVectors[3, 1] = ovR3C1;
            EigenVectors[3, 2] = ovR3C2;
            EigenVectors[3, 3] = ovR3C3;
            EigenVectors[3, 4] = ovR3C4;

            EigenVectors[4, 0] = ovR4C0;
            EigenVectors[4, 1] = ovR4C1;
            EigenVectors[4, 2] = ovR4C2;
            EigenVectors[4, 3] = ovR4C3;
            EigenVectors[4, 4] = ovR4C4;


        }
    }

    private void EigenDecompose6()
    {
        for (var sweep = 0; sweep < MaxSweeps; sweep++)
        {
            if (GetOffDiagonalNorm6() < NormTolerance) break;

            //Begin GA-FuL MetaContext Code Generation, 2025-07-01T01:52:25.5944413+03:00
            //MetaContext: 
            //Input Variables: 57 used, 0 not used, 57 total.
            //Temp Variables: 1008 sub-expressions, 0 generated temps, 1008 total.
            //Target Temp Variables: 1008 total.
            //Output Variables: 57 total.
            //Computations: 1.4563380281690141 average, 1551 total.
            //Memory Reads: 2.174647887323944 average, 2316 total.
            //Memory Writes: 1065 total.
            //
            //MetaContext Binding Data: 
            //   0 = constant: '0'
            //   1 = constant: '1'
            //   -1 = constant: '-1'
            //   2 = constant: '2'
            //   -2 = constant: '-2'
            //   Rational[1, 2] = constant: 'Rational[1, 2]'
            //   Rational[-1, 2] = constant: 'Rational[-1, 2]'
            //   iar0c0 = parameter: DiagonalMatrix[0, 0]
            //   iar2c0 = parameter: DiagonalMatrix[2, 0]
            //   iar3c0 = parameter: DiagonalMatrix[3, 0]
            //   iar4c0 = parameter: DiagonalMatrix[4, 0]
            //   iar5c0 = parameter: DiagonalMatrix[5, 0]
            //   iar0c1 = parameter: DiagonalMatrix[0, 1]
            //   iar1c1 = parameter: DiagonalMatrix[1, 1]
            //   iar2c1 = parameter: DiagonalMatrix[2, 1]
            //   iar3c1 = parameter: DiagonalMatrix[3, 1]
            //   iar4c1 = parameter: DiagonalMatrix[4, 1]
            //   iar5c1 = parameter: DiagonalMatrix[5, 1]
            //   iar2c2 = parameter: DiagonalMatrix[2, 2]
            //   iar3c2 = parameter: DiagonalMatrix[3, 2]
            //   iar4c2 = parameter: DiagonalMatrix[4, 2]
            //   iar5c2 = parameter: DiagonalMatrix[5, 2]
            //   iar3c3 = parameter: DiagonalMatrix[3, 3]
            //   iar4c3 = parameter: DiagonalMatrix[4, 3]
            //   iar5c3 = parameter: DiagonalMatrix[5, 3]
            //   iar4c4 = parameter: DiagonalMatrix[4, 4]
            //   iar5c4 = parameter: DiagonalMatrix[5, 4]
            //   iar5c5 = parameter: DiagonalMatrix[5, 5]
            //   ivr0c0 = parameter: EigenVectors[0, 0]
            //   ivr1c0 = parameter: EigenVectors[1, 0]
            //   ivr2c0 = parameter: EigenVectors[2, 0]
            //   ivr3c0 = parameter: EigenVectors[3, 0]
            //   ivr4c0 = parameter: EigenVectors[4, 0]
            //   ivr5c0 = parameter: EigenVectors[5, 0]
            //   ivr0c1 = parameter: EigenVectors[0, 1]
            //   ivr1c1 = parameter: EigenVectors[1, 1]
            //   ivr2c1 = parameter: EigenVectors[2, 1]
            //   ivr3c1 = parameter: EigenVectors[3, 1]
            //   ivr4c1 = parameter: EigenVectors[4, 1]
            //   ivr5c1 = parameter: EigenVectors[5, 1]
            //   ivr0c2 = parameter: EigenVectors[0, 2]
            //   ivr1c2 = parameter: EigenVectors[1, 2]
            //   ivr2c2 = parameter: EigenVectors[2, 2]
            //   ivr3c2 = parameter: EigenVectors[3, 2]
            //   ivr4c2 = parameter: EigenVectors[4, 2]
            //   ivr5c2 = parameter: EigenVectors[5, 2]
            //   ivr0c3 = parameter: EigenVectors[0, 3]
            //   ivr1c3 = parameter: EigenVectors[1, 3]
            //   ivr2c3 = parameter: EigenVectors[2, 3]
            //   ivr3c3 = parameter: EigenVectors[3, 3]
            //   ivr4c3 = parameter: EigenVectors[4, 3]
            //   ivr5c3 = parameter: EigenVectors[5, 3]
            //   ivr0c4 = parameter: EigenVectors[0, 4]
            //   ivr1c4 = parameter: EigenVectors[1, 4]
            //   ivr2c4 = parameter: EigenVectors[2, 4]
            //   ivr3c4 = parameter: EigenVectors[3, 4]
            //   ivr4c4 = parameter: EigenVectors[4, 4]
            //   ivr5c4 = parameter: EigenVectors[5, 4]
            //   ivr0c5 = parameter: EigenVectors[0, 5]
            //   ivr1c5 = parameter: EigenVectors[1, 5]
            //   ivr2c5 = parameter: EigenVectors[2, 5]
            //   ivr3c5 = parameter: EigenVectors[3, 5]
            //   ivr4c5 = parameter: EigenVectors[4, 5]
            //   ivr5c5 = parameter: EigenVectors[5, 5]

            var oaR4C5 = 0;

            var temp1 = -DiagonalMatrix[0, 0];
            var temp2 = DiagonalMatrix[1, 1] + temp1;
            var temp3 = 0.5 * 1 / DiagonalMatrix[0, 1] * temp2;
            var temp4 = BinaryStep(temp3);
            var temp5 = Math.Sqrt(1 + temp3 * temp3) + Math.Abs(temp3);
            var temp6 = 1 / temp5;
            var temp7 = DiagonalMatrix[0, 1] * temp6;
            var temp8 = temp4 * temp7;
            var temp9 = -temp8;
            var temp10 = DiagonalMatrix[0, 0] + temp9;
            var temp11 = temp4 * temp4 * Math.Pow(temp5, -2);
            var temp12 = 1 / Math.Sqrt(1 + temp11);
            var temp13 = DiagonalMatrix[2, 1] * temp6;
            var temp14 = temp4 * temp13;
            var temp15 = temp12 * temp14;
            var temp16 = DiagonalMatrix[2, 0] * temp12 + -temp15;
            var temp17 = DiagonalMatrix[2, 2] + temp1;
            var temp18 = temp8 + temp17;
            var temp19 = 0.5 * 1 / temp16 * temp18;
            var temp20 = BinaryStep(temp19);
            var temp21 = Math.Sqrt(1 + temp19 * temp19) + Math.Abs(temp19);
            var temp22 = 1 / temp21;
            var temp23 = temp16 * temp22;
            var temp24 = temp20 * temp23;
            var temp25 = -temp24;
            var temp26 = temp10 + temp25;
            var temp27 = temp20 * temp20 * Math.Pow(temp21, -2);
            var temp28 = 1 + temp27;
            var temp29 = 1 / Math.Sqrt(temp28);
            var temp30 = DiagonalMatrix[3, 1] * temp6;
            var temp31 = temp4 * temp30;
            var temp32 = temp12 * temp31;
            var temp33 = DiagonalMatrix[3, 0] * temp12 + -temp32;
            var temp34 = DiagonalMatrix[3, 2] * temp22;
            var temp35 = temp20 * temp34;
            var temp36 = temp33 + -temp35;
            var temp37 = temp29 * temp36;
            var temp38 = 0.5 * Math.Sqrt(temp28);
            var temp39 = 1 / temp36 * temp38;
            var temp40 = DiagonalMatrix[3, 3] + temp1;
            var temp41 = temp8 + temp40;
            var temp42 = temp24 + temp41;
            var temp43 = temp39 * temp42;
            var temp44 = Math.Sqrt(1 + temp43 * temp43) + Math.Abs(temp43);
            var temp45 = 1 / temp44;
            var temp46 = temp37 * temp45;
            var temp47 = BinaryStep(temp43);
            var temp48 = temp46 * temp47;
            var temp49 = -temp48;
            var temp50 = temp26 + temp49;
            var temp51 = DiagonalMatrix[4, 1] * temp6;
            var temp52 = temp4 * temp51;
            var temp53 = temp12 * temp52;
            var temp54 = DiagonalMatrix[4, 0] * temp12 + -temp53;
            var temp55 = DiagonalMatrix[4, 2] * temp22;
            var temp56 = temp20 * temp55;
            var temp57 = temp54 + -temp56;
            var temp58 = temp29 * temp57;
            var temp59 = Math.Pow(temp44, -2) * temp47 * temp47;
            var temp60 = 1 / Math.Sqrt(1 + temp59);
            var temp61 = temp58 * temp60;
            var temp62 = DiagonalMatrix[4, 3] * temp45;
            var temp63 = temp47 * temp62;
            var temp64 = temp60 * temp63;
            var temp65 = -temp61 + temp64;
            var temp66 = temp61 + -temp64;
            var temp67 = DiagonalMatrix[4, 4] + temp1;
            var temp68 = temp8 + temp67;
            var temp69 = temp24 + temp68;
            var temp70 = temp48 + temp69;
            var temp71 = 0.5 * 1 / temp66 * temp70;
            var temp72 = Math.Sqrt(1 + temp71 * temp71) + Math.Abs(temp71);
            var temp73 = 1 / temp72;
            var temp74 = temp65 * temp73;
            var temp75 = BinaryStep(temp71);
            var temp76 = temp74 * temp75;
            var temp77 = temp50 + temp76;
            var temp78 = temp29 * temp60;
            var temp79 = -1 * DiagonalMatrix[5, 1] * temp4;
            var temp80 = DiagonalMatrix[5, 0] + temp6 * temp79;
            var temp81 = temp12 * temp80;
            var temp82 = DiagonalMatrix[5, 2] * temp22;
            var temp83 = temp20 * temp82;
            var temp84 = temp81 + -temp83;
            var temp85 = DiagonalMatrix[5, 3] * temp45;
            var temp86 = temp47 * temp85;
            var temp87 = temp60 * temp86;
            var temp88 = temp78 * temp84 + -temp87;
            var temp89 = Math.Pow(temp72, -2) * temp75 * temp75;
            var temp90 = 1 / Math.Sqrt(1 + temp89);
            var temp91 = DiagonalMatrix[5, 4] * temp73;
            var temp92 = temp75 * temp91;
            var temp93 = temp90 * temp92;
            var temp94 = temp88 * temp90 + -temp93;
            var temp95 = DiagonalMatrix[5, 5] + temp1;
            var temp96 = temp8 + temp95;
            var temp97 = temp24 + temp96;
            var temp98 = temp48 + temp97;
            var temp99 = temp66 * temp73;
            var temp100 = temp75 * temp99;
            var temp101 = temp98 + temp100;
            var temp102 = 0.5 * 1 / temp94 * temp101;
            var temp103 = Math.Sqrt(1 + temp102 * temp102) + Math.Abs(temp102);
            var temp104 = 1 / temp103;
            var temp105 = temp94 * temp104;
            var temp106 = BinaryStep(temp102);
            var temp107 = temp105 * temp106;
            var oaR0C0 = temp77 + -temp107;

            var temp109 = Math.Pow(temp103, -2) * temp106 * temp106;
            var temp110 = 1 / Math.Sqrt(1 + temp109);
            var temp111 = -1 * EigenVectors[0, 5] * temp104;
            var temp112 = -1 * EigenVectors[0, 4] * temp73;
            var temp113 = EigenVectors[0, 1] * temp6;
            var temp114 = temp4 * temp113;
            var temp115 = temp12 * temp114;
            var temp116 = EigenVectors[0, 0] * temp12 + -temp115;
            var temp117 = EigenVectors[0, 2] * temp22;
            var temp118 = temp20 * temp117;
            var temp119 = temp116 + -temp118;
            var temp120 = temp78 * temp119;
            var temp121 = temp75 * temp112 + temp120;
            var temp122 = EigenVectors[0, 3] * temp45;
            var temp123 = temp47 * temp122;
            var temp124 = temp60 * temp123;
            var temp125 = -temp124;
            var temp126 = temp121 + temp125;
            var temp127 = temp106 * temp111 + temp90 * temp126;
            var ovR0C0 = temp110 * temp127;

            var temp129 = EigenVectors[1, 1] * temp6;
            var temp130 = temp4 * temp129;
            var temp131 = temp12 * temp130;
            var temp132 = EigenVectors[1, 0] * temp12 + -temp131;
            var temp133 = EigenVectors[1, 2] * temp22;
            var temp134 = temp20 * temp133;
            var temp135 = temp132 + -temp134;
            var temp136 = EigenVectors[1, 3] * temp45;
            var temp137 = temp47 * temp136;
            var temp138 = temp60 * temp137;
            var temp139 = temp78 * temp135 + -temp138;
            var temp140 = EigenVectors[1, 4] * temp73;
            var temp141 = temp75 * temp140;
            var temp142 = temp139 + -temp141;
            var temp143 = EigenVectors[1, 5] * temp104;
            var temp144 = temp106 * temp143;
            var temp145 = temp90 * temp142 + -temp144;
            var ovR1C0 = temp110 * temp145;

            var temp147 = -1 * EigenVectors[2, 5] * temp104;
            var temp148 = -1 * EigenVectors[2, 4] * temp73;
            var temp149 = temp47 * temp60;
            var temp150 = -1 * EigenVectors[2, 3] * temp45;
            var temp151 = temp149 * temp150;
            var temp152 = temp75 * temp148 + temp151;
            var temp153 = EigenVectors[2, 1] * temp6;
            var temp154 = temp4 * temp153;
            var temp155 = temp12 * temp154;
            var temp156 = EigenVectors[2, 0] * temp12 + -temp155;
            var temp157 = EigenVectors[2, 2] * temp22;
            var temp158 = temp20 * temp157;
            var temp159 = temp156 + -temp158;
            var temp160 = temp78 * temp159;
            var temp161 = temp152 + temp160;
            var temp162 = temp90 * temp161;
            var temp163 = temp106 * temp147 + temp162;
            var ovR2C0 = temp110 * temp163;

            var temp165 = EigenVectors[3, 1] * temp6;
            var temp166 = temp4 * temp165;
            var temp167 = temp12 * temp166;
            var temp168 = EigenVectors[3, 0] * temp12 + -temp167;
            var temp169 = EigenVectors[3, 2] * temp22;
            var temp170 = temp20 * temp169;
            var temp171 = temp168 + -temp170;
            var temp172 = EigenVectors[3, 3] * temp45;
            var temp173 = temp47 * temp172;
            var temp174 = temp60 * temp173;
            var temp175 = temp78 * temp171 + -temp174;
            var temp176 = EigenVectors[3, 4] * temp73;
            var temp177 = temp75 * temp176;
            var temp178 = temp90 * temp177;
            var temp179 = temp90 * temp175 + -temp178;
            var temp180 = EigenVectors[3, 5] * temp104;
            var temp181 = temp106 * temp180;
            var temp182 = temp179 + -temp181;
            var ovR3C0 = temp110 * temp182;

            var temp184 = temp60 * temp90;
            var temp185 = EigenVectors[4, 1] * temp6;
            var temp186 = temp4 * temp185;
            var temp187 = temp12 * temp186;
            var temp188 = EigenVectors[4, 0] * temp12 + -temp187;
            var temp189 = EigenVectors[4, 2] * temp22;
            var temp190 = temp20 * temp189;
            var temp191 = temp188 + -temp190;
            var temp192 = EigenVectors[4, 3] * temp45;
            var temp193 = -1 * temp47 * temp192;
            var temp194 = temp29 * temp191 + temp193;
            var temp195 = EigenVectors[4, 4] * temp73;
            var temp196 = temp75 * temp195;
            var temp197 = temp90 * temp196;
            var temp198 = temp184 * temp194 + -temp197;
            var temp199 = -temp106;
            var temp200 = EigenVectors[4, 5] * temp104;
            var temp201 = temp198 + temp199 * temp200;
            var ovR4C0 = temp110 * temp201;

            var temp203 = -1 * EigenVectors[5, 3] * temp45;
            var temp204 = temp47 * temp203;
            var temp205 = EigenVectors[5, 1] * temp6;
            var temp206 = temp4 * temp205;
            var temp207 = temp12 * temp206;
            var temp208 = EigenVectors[5, 0] * temp12 + -temp207;
            var temp209 = EigenVectors[5, 2] * temp22;
            var temp210 = temp20 * temp209;
            var temp211 = temp208 + -temp210;
            var temp212 = temp60 * temp204 + temp78 * temp211;
            var temp213 = -temp75;
            var temp214 = EigenVectors[5, 4] * temp73;
            var temp215 = temp212 + temp213 * temp214;
            var temp216 = temp90 * temp215;
            var temp217 = EigenVectors[5, 5] * temp104;
            var temp218 = temp216 + temp199 * temp217;
            var ovR5C0 = temp110 * temp218;

            var temp220 = DiagonalMatrix[1, 1] + temp8;
            var temp221 = DiagonalMatrix[2, 0] * temp6;
            var temp222 = DiagonalMatrix[2, 1] + temp4 * temp221;
            var temp223 = temp12 * temp222;
            var temp224 = temp29 * temp223;
            var temp225 = temp38 * 1 / temp223;
            var temp226 = DiagonalMatrix[2, 2] + temp24;
            var temp227 = -DiagonalMatrix[1, 1];
            var temp228 = temp9 + temp227;
            var temp229 = temp226 + temp228;
            var temp230 = temp225 * temp229;
            var temp231 = BinaryStep(temp230);
            var temp232 = temp224 * temp231;
            var temp233 = Math.Sqrt(1 + temp230 * temp230) + Math.Abs(temp230);
            var temp234 = 1 / temp233;
            var temp235 = temp232 * temp234;
            var temp236 = -temp235;
            var temp237 = temp220 + temp236;
            var temp238 = temp231 * temp231 * Math.Pow(temp233, -2);
            var temp239 = 1 + temp238;
            var temp240 = 1 / Math.Sqrt(temp239);
            var temp241 = temp22 * temp33;
            var temp242 = DiagonalMatrix[3, 2] + temp20 * temp241;
            var temp243 = temp78 * temp242;
            var temp244 = temp231 * temp243;
            var temp245 = temp234 * temp244;
            var temp246 = DiagonalMatrix[3, 0] * temp6;
            var temp247 = DiagonalMatrix[3, 1] + temp4 * temp246;
            var temp248 = temp12 * temp247;
            var temp249 = temp22 * temp223;
            var temp250 = temp20 * temp249;
            var temp251 = -1 * temp29 * temp250;
            var temp252 = temp45 * temp251;
            var temp253 = temp248 + temp47 * temp252;
            var temp254 = temp60 * temp253;
            var temp255 = -temp245 + temp254;
            var temp256 = temp240 * temp255;
            var temp257 = 0.5 * Math.Sqrt(temp239) * 1 / temp255;
            var temp258 = DiagonalMatrix[3, 3] + temp227;
            var temp259 = temp9 + temp258;
            var temp260 = temp48 + temp259;
            var temp261 = temp235 + temp260;
            var temp262 = temp257 * temp261;
            var temp263 = Math.Sqrt(1 + temp262 * temp262) + Math.Abs(temp262);
            var temp264 = 1 / temp263;
            var temp265 = temp256 * temp264;
            var temp266 = BinaryStep(temp262);
            var temp267 = temp265 * temp266;
            var temp268 = -temp267;
            var temp269 = temp237 + temp268;
            var temp270 = temp22 * temp54;
            var temp271 = DiagonalMatrix[4, 2] + temp20 * temp270;
            var temp272 = temp73 * temp75;
            var temp273 = temp29 * temp45;
            var temp274 = temp242 * temp273;
            var temp275 = temp47 * temp274;
            var temp276 = temp60 * temp275;
            var temp277 = -temp276;
            var temp278 = temp29 * temp271 + temp272 * temp277;
            var temp279 = temp90 * temp278;
            var temp280 = temp231 * temp279;
            var temp281 = temp234 * temp280;
            var temp282 = DiagonalMatrix[4, 0] * temp6;
            var temp283 = DiagonalMatrix[4, 1] + temp4 * temp282;
            var temp284 = temp12 * temp283;
            var temp285 = -1 * temp45 * temp47;
            var temp286 = temp251 + temp248 * temp285;
            var temp287 = temp60 * temp286;
            var temp288 = temp284 + temp272 * temp287;
            var temp289 = temp90 * temp288;
            var temp290 = -temp281 + temp289;
            var temp291 = temp240 * temp290;
            var temp292 = Math.Pow(temp263, -2) * temp266 * temp266;
            var temp293 = 1 / Math.Sqrt(1 + temp292);
            var temp294 = temp291 * temp293;
            var temp295 = temp57 * temp273;
            var temp296 = DiagonalMatrix[4, 3] + temp47 * temp295;
            var temp297 = temp60 * temp296;
            var temp298 = temp90 * temp297;
            var temp299 = temp264 * temp298;
            var temp300 = temp266 * temp299;
            var temp301 = temp293 * temp300;
            var temp302 = -temp294 + temp301;
            var temp303 = temp294 + -temp301;
            var temp304 = DiagonalMatrix[4, 4] + temp227;
            var temp305 = temp9 + temp304;
            var temp306 = temp100 + temp305;
            var temp307 = temp235 + temp306;
            var temp308 = temp267 + temp307;
            var temp309 = 0.5 * 1 / temp303 * temp308;
            var temp310 = Math.Sqrt(1 + temp309 * temp309) + Math.Abs(temp309);
            var temp311 = 1 / temp310;
            var temp312 = temp302 * temp311;
            var temp313 = BinaryStep(temp309);
            var temp314 = temp312 * temp313;
            var temp315 = temp269 + temp314;
            var temp316 = temp110 * temp240;
            var temp317 = -1 * temp231 * temp234;
            var temp318 = temp22 * temp81;
            var temp319 = DiagonalMatrix[5, 2] + temp20 * temp318;
            var temp320 = temp213 * temp271;
            var temp321 = temp29 * temp73;
            var temp322 = temp277 + temp320 * temp321;
            var temp323 = temp90 * temp322;
            var temp324 = temp104 * temp323;
            var temp325 = temp29 * temp319 + temp106 * temp324;
            var temp326 = DiagonalMatrix[5, 0] * temp6;
            var temp327 = DiagonalMatrix[5, 1] + temp4 * temp326;
            var temp328 = temp12 * temp327;
            var temp329 = temp317 * temp325 + temp328;
            var temp330 = temp90 * temp104;
            var temp331 = -1 * temp272 * temp284;
            var temp332 = temp287 + temp331;
            var temp333 = temp330 * temp332;
            var temp334 = temp106 * temp333;
            var temp335 = temp329 + temp334;
            var temp336 = temp316 * temp335;
            var temp337 = -temp110;
            var temp338 = temp264 * temp337;
            var temp339 = temp266 * temp338;
            var temp340 = temp84 * temp273;
            var temp341 = DiagonalMatrix[5, 3] + temp47 * temp340;
            var temp342 = temp60 * temp341;
            var temp343 = -1 * temp90 * temp272;
            var temp344 = temp297 * temp343;
            var temp345 = temp104 * temp106;
            var temp346 = temp342 + temp344 * temp345;
            var temp347 = temp336 + temp339 * temp346;
            var temp348 = temp293 * temp347;
            var temp349 = Math.Pow(temp310, -2) * temp313 * temp313;
            var temp350 = 1 / Math.Sqrt(1 + temp349);
            var temp351 = temp348 * temp350;
            var temp352 = temp311 * temp313;
            var temp353 = temp350 * temp352;
            var temp354 = temp110 * temp353;
            var temp355 = DiagonalMatrix[5, 4] + temp88 * temp272;
            var temp356 = temp90 * temp355;
            var temp357 = temp354 * temp356;
            var temp358 = -temp351 + temp357;
            var temp359 = temp351 + -temp357;
            var temp360 = DiagonalMatrix[5, 5] + temp227;
            var temp361 = temp9 + temp360;
            var temp362 = temp107 + temp361;
            var temp363 = temp235 + temp362;
            var temp364 = temp267 + temp363;
            var temp365 = temp303 * temp311;
            var temp366 = temp313 * temp365;
            var temp367 = temp364 + temp366;
            var temp368 = 0.5 * 1 / temp359 * temp367;
            var temp369 = Math.Sqrt(1 + temp368 * temp368) + Math.Abs(temp368);
            var temp370 = 1 / temp369;
            var temp371 = temp358 * temp370;
            var temp372 = BinaryStep(temp368);
            var oaR1C1 = temp315 + temp371 * temp372;

            var temp374 = temp90 * temp332;
            var temp375 = temp199 * temp319;
            var temp376 = temp29 * temp104;
            var temp377 = temp323 + temp375 * temp376;
            var temp378 = temp374 + temp317 * temp377;
            var temp379 = temp104 * temp328;
            var temp380 = temp106 * temp379;
            var temp381 = -temp380;
            var temp382 = temp378 + temp381;
            var temp383 = temp316 * temp382;
            var temp384 = temp110 * temp264;
            var temp385 = temp266 * temp384;
            var temp386 = temp104 * temp342;
            var temp387 = temp344 + temp199 * temp386;
            var temp388 = temp385 * temp387;
            var temp389 = temp293 * temp388;
            var temp390 = temp293 * temp383 + -temp389;
            var temp391 = temp104 * temp356;
            var temp392 = temp106 * temp391;
            var temp393 = temp337 * temp392;
            var temp394 = temp353 * temp393;
            var temp395 = temp350 * temp390 + -temp394;
            var temp396 = Math.Pow(temp369, -2) * temp372 * temp372;
            var temp397 = 1 / Math.Sqrt(1 + temp396);
            var oaR0C1 = temp395 * temp397;

            var temp399 = temp240 * temp293;
            var temp400 = temp29 * temp231;
            var temp401 = temp22 * temp116;
            var temp402 = EigenVectors[0, 2] + temp20 * temp401;
            var temp403 = temp400 * temp402;
            var temp404 = -1 * temp234 * temp403;
            var temp405 = EigenVectors[0, 0] * temp6;
            var temp406 = EigenVectors[0, 1] + temp4 * temp405;
            var temp407 = temp12 * temp406;
            var temp408 = temp404 + temp407;
            var temp409 = temp119 * temp273;
            var temp410 = EigenVectors[0, 3] + temp47 * temp409;
            var temp411 = temp60 * temp410;
            var temp412 = temp264 * temp411;
            var temp413 = temp266 * temp412;
            var temp414 = temp293 * temp413;
            var temp415 = temp399 * temp408 + -temp414;
            var temp416 = temp120 + temp125;
            var temp417 = EigenVectors[0, 4] + temp272 * temp416;
            var temp418 = temp90 * temp417;
            var temp419 = temp353 * temp418;
            var temp420 = temp350 * temp415 + -temp419;
            var temp421 = temp370 * temp372;
            var temp422 = temp397 * temp421;
            var temp423 = temp110 * temp422;
            var temp424 = temp106 * temp330;
            var temp425 = EigenVectors[0, 5] + temp126 * temp424;
            var temp426 = temp423 * temp425;
            var ovR0C1 = temp397 * temp420 + -temp426;

            var temp428 = temp350 * temp397;
            var temp429 = temp22 * temp132;
            var temp430 = EigenVectors[1, 2] + temp20 * temp429;
            var temp431 = temp400 * temp430;
            var temp432 = temp234 * temp431;
            var temp433 = EigenVectors[1, 0] * temp6;
            var temp434 = EigenVectors[1, 1] + temp4 * temp433;
            var temp435 = temp12 * temp434;
            var temp436 = -temp432 + temp435;
            var temp437 = -1 * temp264 * temp266;
            var temp438 = temp293 * temp437;
            var temp439 = temp135 * temp273;
            var temp440 = EigenVectors[1, 3] + temp47 * temp439;
            var temp441 = temp60 * temp440;
            var temp442 = temp399 * temp436 + temp438 * temp441;
            var temp443 = -1 * temp311 * temp313;
            var temp444 = EigenVectors[1, 4] + temp139 * temp272;
            var temp445 = temp90 * temp444;
            var temp446 = temp442 + temp443 * temp445;
            var temp447 = EigenVectors[1, 5] + temp142 * temp424;
            var temp448 = temp423 * temp447;
            var ovR1C1 = temp428 * temp446 + -temp448;

            var temp450 = temp22 * temp156;
            var temp451 = EigenVectors[2, 2] + temp20 * temp450;
            var temp452 = temp400 * temp451;
            var temp453 = temp234 * temp452;
            var temp454 = EigenVectors[2, 0] * temp6;
            var temp455 = EigenVectors[2, 1] + temp4 * temp454;
            var temp456 = temp12 * temp455;
            var temp457 = -temp453 + temp456;
            var temp458 = temp159 * temp273;
            var temp459 = EigenVectors[2, 3] + temp47 * temp458;
            var temp460 = temp60 * temp459;
            var temp461 = temp264 * temp460;
            var temp462 = temp266 * temp461;
            var temp463 = temp293 * temp462;
            var temp464 = temp399 * temp457 + -temp463;
            var temp465 = temp151 + temp160;
            var temp466 = EigenVectors[2, 4] + temp272 * temp465;
            var temp467 = temp90 * temp466;
            var temp468 = temp353 * temp467;
            var temp469 = temp350 * temp464 + -temp468;
            var temp470 = temp104 * temp162;
            var temp471 = EigenVectors[2, 5] + temp106 * temp470;
            var temp472 = temp423 * temp471;
            var ovR2C1 = temp397 * temp469 + -temp472;

            var temp474 = temp22 * temp168;
            var temp475 = EigenVectors[3, 2] + temp20 * temp474;
            var temp476 = temp400 * temp475;
            var temp477 = temp234 * temp476;
            var temp478 = EigenVectors[3, 0] * temp6;
            var temp479 = EigenVectors[3, 1] + temp4 * temp478;
            var temp480 = temp12 * temp479;
            var temp481 = -temp477 + temp480;
            var temp482 = temp171 * temp273;
            var temp483 = EigenVectors[3, 3] + temp47 * temp482;
            var temp484 = temp60 * temp483;
            var temp485 = temp264 * temp484;
            var temp486 = temp266 * temp485;
            var temp487 = temp293 * temp486;
            var temp488 = temp399 * temp481 + -temp487;
            var temp489 = EigenVectors[3, 4] + temp175 * temp272;
            var temp490 = temp90 * temp489;
            var temp491 = temp353 * temp490;
            var temp492 = temp350 * temp488 + -temp491;
            var temp493 = temp337 * temp422;
            var temp494 = temp104 * temp179;
            var temp495 = EigenVectors[3, 5] + temp106 * temp494;
            var ovR3C1 = temp397 * temp492 + temp493 * temp495;

            var temp497 = temp22 * temp188;
            var temp498 = EigenVectors[4, 2] + temp20 * temp497;
            var temp499 = temp400 * temp498;
            var temp500 = temp234 * temp499;
            var temp501 = EigenVectors[4, 0] * temp6;
            var temp502 = EigenVectors[4, 1] + temp4 * temp501;
            var temp503 = temp12 * temp502;
            var temp504 = -temp500 + temp503;
            var temp505 = temp191 * temp273;
            var temp506 = EigenVectors[4, 3] + temp47 * temp505;
            var temp507 = temp60 * temp506;
            var temp508 = temp264 * temp507;
            var temp509 = temp266 * temp508;
            var temp510 = temp293 * temp509;
            var temp511 = temp399 * temp504 + -temp510;
            var temp512 = temp60 * temp73;
            var temp513 = temp75 * temp512;
            var temp514 = EigenVectors[4, 4] + temp194 * temp513;
            var temp515 = temp90 * temp514;
            var temp516 = temp353 * temp515;
            var temp517 = temp350 * temp511 + -temp516;
            var temp518 = EigenVectors[4, 5] + temp198 * temp345;
            var temp519 = temp423 * temp518;
            var ovR4C1 = temp397 * temp517 + -temp519;

            var temp521 = temp22 * temp208;
            var temp522 = EigenVectors[5, 2] + temp20 * temp521;
            var temp523 = temp400 * temp522;
            var temp524 = temp234 * temp523;
            var temp525 = EigenVectors[5, 0] * temp6;
            var temp526 = EigenVectors[5, 1] + temp4 * temp525;
            var temp527 = temp12 * temp526;
            var temp528 = -temp524 + temp527;
            var temp529 = temp211 * temp273;
            var temp530 = EigenVectors[5, 3] + temp47 * temp529;
            var temp531 = temp60 * temp530;
            var temp532 = temp264 * temp531;
            var temp533 = temp266 * temp532;
            var temp534 = temp293 * temp533;
            var temp535 = temp399 * temp528 + -temp534;
            var temp536 = EigenVectors[5, 4] + temp212 * temp272;
            var temp537 = temp90 * temp536;
            var temp538 = temp535 + temp443 * temp537;
            var temp539 = EigenVectors[5, 5] + temp216 * temp345;
            var temp540 = temp423 * temp539;
            var ovR5C1 = temp428 * temp538 + -temp540;

            var temp542 = temp226 + temp235;
            var temp543 = temp231 * temp254;
            var temp544 = temp243 + temp234 * temp543;
            var temp545 = temp399 * temp544;
            var temp546 = DiagonalMatrix[3, 3] + temp48;
            var temp547 = temp236 + temp546;
            var temp548 = temp267 + temp547;
            var temp549 = -DiagonalMatrix[2, 2];
            var temp550 = temp25 + temp549;
            var temp551 = temp548 + temp550;
            var temp552 = 0.5 * 1 / temp545 * temp551;
            var temp553 = Math.Sqrt(1 + temp552 * temp552) + Math.Abs(temp552);
            var temp554 = 1 / temp553;
            var temp555 = -1 * temp545 * temp554;
            var temp556 = BinaryStep(temp552);
            var temp557 = temp555 * temp556;
            var temp558 = temp542 + temp557;
            var temp559 = temp231 * temp289;
            var temp560 = temp279 + temp234 * temp559;
            var temp561 = temp240 * temp264;
            var temp562 = temp544 * temp561;
            var temp563 = temp266 * temp562;
            var temp564 = temp293 * temp563;
            var temp565 = -temp564;
            var temp566 = temp240 * temp560 + temp352 * temp565;
            var temp567 = temp350 * temp566;
            var temp568 = Math.Pow(temp553, -2) * temp556 * temp556;
            var temp569 = 1 / Math.Sqrt(1 + temp568);
            var temp570 = temp554 * temp556;
            var temp571 = temp569 * temp570;
            var temp572 = temp264 * temp266;
            var temp573 = temp298 + temp291 * temp572;
            var temp574 = temp293 * temp573;
            var temp575 = temp350 * temp574;
            var temp576 = temp571 * temp575;
            var temp577 = temp567 * temp569 + -temp576;
            var temp578 = DiagonalMatrix[4, 4] + temp549;
            var temp579 = temp25 + temp578;
            var temp580 = temp100 + temp579;
            var temp581 = temp236 + temp580;
            var temp582 = temp366 + temp581;
            var temp583 = temp545 * temp554;
            var temp584 = temp556 * temp583;
            var temp585 = temp582 + temp584;
            var temp586 = 0.5 * 1 / temp577 * temp585;
            var temp587 = BinaryStep(temp586);
            var temp588 = temp577 * temp587;
            var temp589 = Math.Sqrt(1 + temp586 * temp586) + Math.Abs(temp586);
            var temp590 = 1 / temp589;
            var temp591 = -1 * temp588 * temp590;
            var temp592 = temp558 + temp591;
            var temp593 = temp348 * temp352 + temp110 * temp356;
            var temp594 = temp350 * temp593;
            var temp595 = temp397 * temp594;
            var temp596 = temp587 * temp595;
            var temp597 = temp590 * temp596;
            var temp598 = temp587 * temp587 * Math.Pow(temp589, -2);
            var temp599 = 1 / Math.Sqrt(1 + temp598);
            var temp600 = temp328 + temp334;
            var temp601 = temp231 * temp234;
            var temp602 = temp325 + temp600 * temp601;
            var temp603 = temp240 * temp353;
            var temp604 = temp560 * temp603;
            var temp605 = temp350 * temp565 + -temp604;
            var temp606 = temp316 * temp602 + temp421 * temp605;
            var temp607 = temp397 * temp606;
            var temp608 = temp264 * temp336;
            var temp609 = temp110 * temp346 + temp266 * temp608;
            var temp610 = temp293 * temp609;
            var temp611 = temp353 * temp574;
            var temp612 = -temp611;
            var temp613 = temp610 + temp421 * temp612;
            var temp614 = temp397 * temp613;
            var temp615 = temp571 * temp614;
            var temp616 = temp569 * temp607 + -temp615;
            var temp617 = temp599 * temp616;
            var temp618 = temp597 * temp599 + -temp617;
            var temp619 = -temp590;
            var temp620 = temp596 * temp619;
            var temp621 = temp617 + temp599 * temp620;
            var temp622 = DiagonalMatrix[5, 5] + temp549;
            var temp623 = temp25 + temp622;
            var temp624 = temp107 + temp623;
            var temp625 = temp236 + temp624;
            var temp626 = temp584 + temp625;
            var temp627 = temp588 * temp590;
            var temp628 = temp626 + temp627;
            var temp629 = temp359 * temp370;
            var temp630 = temp372 * temp629;
            var temp631 = temp628 + temp630;
            var temp632 = 0.5 * 1 / temp621 * temp631;
            var temp633 = Math.Sqrt(1 + temp632 * temp632) + Math.Abs(temp632);
            var temp634 = 1 / temp633;
            var temp635 = temp618 * temp634;
            var temp636 = BinaryStep(temp632);
            var oaR2C2 = temp592 + temp635 * temp636;

            var temp638 = temp352 * temp390 + temp393;
            var temp639 = temp350 * temp638;
            var temp640 = temp587 * temp639;
            var temp641 = temp374 + temp381;
            var temp642 = temp377 + temp601 * temp641;
            var temp643 = temp110 * temp642;
            var temp644 = temp240 * temp569;
            var temp645 = temp643 * temp644;
            var temp646 = temp619 * temp640 + temp645;
            var temp647 = temp264 * temp383;
            var temp648 = temp110 * temp387 + temp266 * temp647;
            var temp649 = temp293 * temp648;
            var temp650 = temp571 * temp649;
            var temp651 = -temp650;
            var temp652 = temp646 + temp651;
            var temp653 = temp599 * temp652;
            var temp654 = Math.Pow(temp633, -2) * temp636 * temp636;
            var temp655 = 1 / Math.Sqrt(1 + temp654);
            var temp656 = temp634 * temp636;
            var temp657 = temp655 * temp656;
            var temp658 = temp395 * temp421;
            var temp659 = temp397 * temp658;
            var temp660 = temp657 * temp659;
            var oaR0C2 = temp653 * temp655 + -temp660;

            var temp662 = temp422 * temp594;
            var temp663 = -temp662;
            var temp664 = temp587 * temp663;
            var temp665 = temp110 * temp602;
            var temp666 = temp240 * temp422;
            var temp667 = temp665 * temp666;
            var temp668 = temp397 * temp605 + -temp667;
            var temp669 = temp569 * temp668;
            var temp670 = temp619 * temp664 + temp669;
            var temp671 = temp422 * temp610;
            var temp672 = temp397 * temp612 + -temp671;
            var temp673 = temp571 * temp672;
            var temp674 = -temp673;
            var temp675 = temp670 + temp674;
            var temp676 = temp599 * temp675;
            var oaR1C2 = temp655 * temp676;

            var temp678 = temp352 * temp415 + temp418;
            var temp679 = temp350 * temp678;
            var temp680 = temp587 * temp679;
            var temp681 = temp231 * temp407;
            var temp682 = temp29 * temp402 + temp234 * temp681;
            var temp683 = temp644 * temp682;
            var temp684 = temp619 * temp680 + temp683;
            var temp685 = temp408 * temp561;
            var temp686 = temp411 + temp266 * temp685;
            var temp687 = temp293 * temp686;
            var temp688 = temp571 * temp687;
            var temp689 = -temp688;
            var temp690 = temp684 + temp689;
            var temp691 = temp599 * temp690;
            var temp692 = -1 * temp634 * temp636;
            var temp693 = temp420 * temp421 + temp110 * temp425;
            var temp694 = temp397 * temp693;
            var temp695 = temp691 + temp692 * temp694;
            var ovR0C2 = temp655 * temp695;

            var temp697 = temp352 * temp442 + temp445;
            var temp698 = temp350 * temp697;
            var temp699 = temp587 * temp698;
            var temp700 = temp231 * temp435;
            var temp701 = temp29 * temp430 + temp234 * temp700;
            var temp702 = temp644 * temp701;
            var temp703 = temp619 * temp699 + temp702;
            var temp704 = temp436 * temp561;
            var temp705 = temp441 + temp266 * temp704;
            var temp706 = temp293 * temp705;
            var temp707 = temp571 * temp706;
            var temp708 = -temp707;
            var temp709 = temp703 + temp708;
            var temp710 = temp599 * temp709;
            var temp711 = temp350 * temp421;
            var temp712 = temp110 * temp447 + temp446 * temp711;
            var temp713 = temp397 * temp712;
            var temp714 = temp710 + temp692 * temp713;
            var ovR1C2 = temp655 * temp714;

            var temp716 = temp352 * temp464 + temp467;
            var temp717 = temp350 * temp716;
            var temp718 = temp587 * temp717;
            var temp719 = temp231 * temp456;
            var temp720 = temp29 * temp451 + temp234 * temp719;
            var temp721 = temp644 * temp720;
            var temp722 = temp619 * temp718 + temp721;
            var temp723 = temp457 * temp561;
            var temp724 = temp460 + temp266 * temp723;
            var temp725 = temp293 * temp724;
            var temp726 = temp571 * temp725;
            var temp727 = -temp726;
            var temp728 = temp722 + temp727;
            var temp729 = temp599 * temp728;
            var temp730 = temp421 * temp469 + temp110 * temp471;
            var temp731 = temp397 * temp730;
            var temp732 = temp729 + temp692 * temp731;
            var ovR2C2 = temp655 * temp732;

            var temp734 = temp352 * temp488 + temp490;
            var temp735 = temp350 * temp734;
            var temp736 = temp587 * temp735;
            var temp737 = temp231 * temp480;
            var temp738 = temp29 * temp475 + temp234 * temp737;
            var temp739 = temp644 * temp738;
            var temp740 = temp619 * temp736 + temp739;
            var temp741 = temp481 * temp561;
            var temp742 = temp484 + temp266 * temp741;
            var temp743 = temp293 * temp742;
            var temp744 = temp571 * temp743;
            var temp745 = -temp744;
            var temp746 = temp740 + temp745;
            var temp747 = temp599 * temp746;
            var temp748 = temp421 * temp492 + temp110 * temp495;
            var temp749 = temp397 * temp748;
            var temp750 = temp657 * temp749;
            var ovR3C2 = temp655 * temp747 + -temp750;

            var temp752 = temp352 * temp511 + temp515;
            var temp753 = temp350 * temp752;
            var temp754 = temp587 * temp753;
            var temp755 = temp231 * temp503;
            var temp756 = temp29 * temp498 + temp234 * temp755;
            var temp757 = temp644 * temp756;
            var temp758 = temp619 * temp754 + temp757;
            var temp759 = temp504 * temp561;
            var temp760 = temp507 + temp266 * temp759;
            var temp761 = temp293 * temp760;
            var temp762 = temp571 * temp761;
            var temp763 = -temp762;
            var temp764 = temp758 + temp763;
            var temp765 = temp599 * temp764;
            var temp766 = temp421 * temp517 + temp110 * temp518;
            var temp767 = temp397 * temp766;
            var temp768 = temp765 + temp692 * temp767;
            var ovR4C2 = temp655 * temp768;

            var temp770 = temp599 * temp655;
            var temp771 = temp231 * temp527;
            var temp772 = temp29 * temp522 + temp234 * temp771;
            var temp773 = temp528 * temp561;
            var temp774 = temp531 + temp266 * temp773;
            var temp775 = temp293 * temp774;
            var temp776 = temp571 * temp775;
            var temp777 = temp644 * temp772 + -temp776;
            var temp778 = temp352 * temp535 + temp537;
            var temp779 = temp350 * temp778;
            var temp780 = temp587 * temp779;
            var temp781 = temp777 + temp619 * temp780;
            var temp782 = temp110 * temp539 + temp538 * temp711;
            var temp783 = temp397 * temp782;
            var temp784 = temp657 * temp783;
            var ovR5C2 = temp770 * temp781 + -temp784;

            var temp786 = temp267 + temp546;
            var temp787 = temp584 + temp786;
            var temp788 = temp567 * temp570 + temp575;
            var temp789 = temp569 * temp788;
            var temp790 = temp599 * temp789;
            var temp791 = DiagonalMatrix[4, 4] + temp100;
            var temp792 = temp268 + temp791;
            var temp793 = temp366 + temp792;
            var temp794 = temp557 + temp793;
            var temp795 = temp627 + temp794;
            var temp796 = -DiagonalMatrix[3, 3];
            var temp797 = temp49 + temp796;
            var temp798 = temp795 + temp797;
            var temp799 = 0.5 * 1 / temp790 * temp798;
            var temp800 = BinaryStep(temp799);
            var temp801 = Math.Sqrt(1 + temp799 * temp799) + Math.Abs(temp799);
            var temp802 = 1 / temp801;
            var temp803 = temp790 * temp802;
            var temp804 = temp800 * temp803;
            var temp805 = -temp804;
            var temp806 = temp787 + temp805;
            var temp807 = temp800 * temp800 * Math.Pow(temp801, -2);
            var temp808 = 1 + temp807;
            var temp809 = 1 / Math.Sqrt(temp808);
            var temp810 = temp570 * temp607 + temp614;
            var temp811 = temp569 * temp810;
            var temp812 = temp599 * temp619;
            var temp813 = temp587 * temp789;
            var temp814 = temp812 * temp813;
            var temp815 = temp811 + temp656 * temp814;
            var temp816 = temp655 * temp815;
            var temp817 = temp587 * temp616;
            var temp818 = temp595 + temp590 * temp817;
            var temp819 = temp770 * temp818;
            var temp820 = temp802 * temp819;
            var temp821 = temp800 * temp820;
            var temp822 = temp816 + -temp821;
            var temp823 = temp809 * temp822;
            var temp824 = 0.5 * Math.Sqrt(temp808) * 1 / temp822;
            var temp825 = DiagonalMatrix[5, 5] + temp796;
            var temp826 = temp49 + temp825;
            var temp827 = temp107 + temp826;
            var temp828 = temp268 + temp827;
            var temp829 = temp557 + temp828;
            var temp830 = temp630 + temp829;
            var temp831 = temp804 + temp830;
            var temp832 = temp621 * temp634;
            var temp833 = temp636 * temp832;
            var temp834 = temp831 + temp833;
            var temp835 = temp824 * temp834;
            var temp836 = Math.Sqrt(1 + temp835 * temp835) + Math.Abs(temp835);
            var temp837 = 1 / temp836;
            var temp838 = -1 * temp823 * temp837;
            var temp839 = BinaryStep(temp835);
            var oaR3C3 = temp806 + temp838 * temp839;

            var temp841 = temp240 * temp570;
            var temp842 = temp649 + temp643 * temp841;
            var temp843 = temp569 * temp842;
            var temp844 = temp599 * temp802;
            var temp845 = temp645 + temp651;
            var temp846 = temp587 * temp845;
            var temp847 = temp639 + temp590 * temp846;
            var temp848 = temp844 * temp847;
            var temp849 = temp800 * temp848;
            var temp850 = temp843 + -temp849;
            var temp851 = temp809 * temp850;
            var temp852 = Math.Pow(temp836, -2) * temp839 * temp839;
            var temp853 = 1 / Math.Sqrt(1 + temp852);
            var temp854 = temp837 * temp839;
            var temp855 = temp853 * temp854;
            var temp856 = temp653 * temp656 + temp659;
            var temp857 = temp655 * temp856;
            var temp858 = temp855 * temp857;
            var oaR0C3 = temp851 * temp853 + -temp858;

            var temp860 = temp809 * temp853;
            var temp861 = temp570 * temp668 + temp672;
            var temp862 = temp569 * temp861;
            var temp863 = temp669 + temp674;
            var temp864 = temp587 * temp590;
            var temp865 = temp663 + temp863 * temp864;
            var temp866 = temp599 * temp865;
            var temp867 = -temp800;
            var temp868 = temp802 * temp867;
            var temp869 = temp862 + temp866 * temp868;
            var temp870 = temp657 * temp676;
            var temp871 = temp855 * temp870;
            var oaR1C3 = temp860 * temp869 + -temp871;

            var temp873 = temp692 * temp811 + temp814;
            var temp874 = temp599 * temp657;
            var temp875 = temp818 * temp874;
            var temp876 = -temp875;
            var temp877 = temp655 * temp873 + temp868 * temp876;
            var oaR2C3 = temp860 * temp877;

            var temp879 = temp687 + temp682 * temp841;
            var temp880 = temp569 * temp879;
            var temp881 = temp683 + temp689;
            var temp882 = temp679 + temp864 * temp881;
            var temp883 = temp844 * temp882;
            var temp884 = temp800 * temp883;
            var temp885 = temp880 + -temp884;
            var temp886 = -1 * temp837 * temp839;
            var temp887 = temp853 * temp886;
            var temp888 = temp656 * temp691 + temp694;
            var temp889 = temp655 * temp888;
            var ovR0C3 = temp860 * temp885 + temp887 * temp889;

            var temp891 = temp706 + temp701 * temp841;
            var temp892 = temp569 * temp891;
            var temp893 = temp702 + temp708;
            var temp894 = temp587 * temp893;
            var temp895 = temp698 + temp590 * temp894;
            var temp896 = temp844 * temp895;
            var temp897 = temp892 + temp867 * temp896;
            var temp898 = temp656 * temp710 + temp713;
            var temp899 = temp655 * temp898;
            var temp900 = temp855 * temp899;
            var ovR1C3 = temp860 * temp897 + -temp900;

            var temp902 = temp725 + temp720 * temp841;
            var temp903 = temp569 * temp902;
            var temp904 = temp721 + temp727;
            var temp905 = temp587 * temp904;
            var temp906 = temp717 + temp590 * temp905;
            var temp907 = temp844 * temp906;
            var temp908 = temp800 * temp907;
            var temp909 = temp903 + -temp908;
            var temp910 = temp656 * temp729 + temp731;
            var temp911 = temp655 * temp910;
            var temp912 = temp855 * temp911;
            var ovR2C3 = temp860 * temp909 + -temp912;

            var temp914 = temp743 + temp738 * temp841;
            var temp915 = temp569 * temp914;
            var temp916 = temp599 * temp868;
            var temp917 = temp739 + temp745;
            var temp918 = temp735 + temp864 * temp917;
            var temp919 = temp915 + temp916 * temp918;
            var temp920 = temp656 * temp747 + temp749;
            var temp921 = temp655 * temp920;
            var ovR3C3 = temp860 * temp919 + temp887 * temp921;

            var temp923 = temp761 + temp756 * temp841;
            var temp924 = temp569 * temp923;
            var temp925 = temp757 + temp763;
            var temp926 = temp587 * temp925;
            var temp927 = temp753 + temp590 * temp926;
            var temp928 = temp844 * temp927;
            var temp929 = temp800 * temp928;
            var temp930 = temp924 + -temp929;
            var temp931 = temp656 * temp765 + temp767;
            var temp932 = temp655 * temp931;
            var temp933 = temp855 * temp932;
            var ovR4C3 = temp860 * temp930 + -temp933;

            var temp935 = temp775 + temp772 * temp841;
            var temp936 = temp569 * temp935;
            var temp937 = temp587 * temp777;
            var temp938 = temp779 + temp590 * temp937;
            var temp939 = temp844 * temp938;
            var temp940 = temp800 * temp939;
            var temp941 = temp936 + -temp940;
            var temp942 = temp599 * temp656;
            var temp943 = temp783 + temp781 * temp942;
            var temp944 = temp655 * temp943;
            var ovR5C3 = temp860 * temp941 + temp887 * temp944;

            var temp946 = temp366 + temp791;
            var temp947 = temp627 + temp946;
            var temp948 = temp804 + temp947;
            var temp949 = temp802 * temp816;
            var temp950 = temp819 + temp800 * temp949;
            var temp951 = temp860 * temp950;
            var temp952 = -DiagonalMatrix[4, 4] + DiagonalMatrix[5, 5];
            var temp953 = temp76 + temp952;
            var temp954 = temp107 + temp953;
            var temp955 = temp314 + temp954;
            var temp956 = temp591 + temp955;
            var temp957 = temp630 + temp956;
            var temp958 = temp805 + temp957;
            var temp959 = temp833 + temp958;
            var temp960 = temp823 * temp837;
            var temp961 = temp839 * temp960;
            var temp962 = temp959 + temp961;
            var temp963 = 0.5 * 1 / temp951 * temp962;
            var temp964 = Math.Sqrt(1 + temp963 * temp963) + Math.Abs(temp963);
            var temp965 = 1 / temp964;
            var temp966 = -1 * temp951 * temp965;
            var temp967 = BinaryStep(temp963);
            var oaR4C4 = temp948 + temp966 * temp967;

            var temp969 = DiagonalMatrix[5, 5] + temp107;
            var temp970 = temp630 + temp969;
            var temp971 = temp833 + temp970;
            var temp972 = temp961 + temp971;
            var temp973 = temp951 * temp965;
            var oaR5C5 = temp972 + temp967 * temp973;

            var temp975 = Math.Pow(temp964, -2) * temp967 * temp967;
            var temp976 = 1 / Math.Sqrt(1 + temp975);
            var temp977 = temp802 * temp843;
            var temp978 = temp599 * temp847 + temp800 * temp977;
            var temp979 = -1 * temp965 * temp967;
            var temp980 = temp809 * temp837;
            var temp981 = temp839 * temp980;
            var temp982 = temp857 + temp850 * temp981;
            var temp983 = temp853 * temp982;
            var temp984 = temp809 * temp978 + temp979 * temp983;
            var oaR0C4 = temp976 * temp984;

            var temp986 = temp809 * temp965;
            var temp987 = temp967 * temp986;
            var temp988 = temp983 + temp978 * temp987;
            var oaR0C5 = temp976 * temp988;

            var temp990 = temp802 * temp862;
            var temp991 = temp866 + temp800 * temp990;
            var temp992 = temp870 + temp869 * temp981;
            var temp993 = temp853 * temp992;
            var temp994 = temp809 * temp991 + temp979 * temp993;
            var oaR1C4 = temp976 * temp994;

            var temp996 = temp987 * temp991 + temp993;
            var oaR1C5 = temp976 * temp996;

            var temp998 = temp655 * temp800;
            var temp999 = temp802 * temp998;
            var temp1000 = temp876 + temp873 * temp999;
            var temp1001 = temp877 * temp981;
            var temp1002 = temp853 * temp1001;
            var temp1003 = temp809 * temp1000 + temp979 * temp1002;
            var oaR2C4 = temp976 * temp1003;

            var temp1005 = temp987 * temp1000 + temp1002;
            var oaR2C5 = temp976 * temp1005;

            var temp1007 = -1 * temp839 * temp853;
            var temp1008 = temp976 * temp1007;
            var temp1009 = temp950 * temp980;
            var oaR3C4 = temp1008 * temp1009;

            var temp1011 = temp950 * temp1007;
            var temp1012 = temp980 * temp1011;
            var temp1013 = temp965 * temp967;
            var temp1014 = temp976 * temp1013;
            var oaR3C5 = temp1012 * temp1014;

            var temp1016 = temp802 * temp880;
            var temp1017 = temp599 * temp882 + temp800 * temp1016;
            var temp1018 = temp889 + temp885 * temp981;
            var temp1019 = temp853 * temp1018;
            var temp1020 = temp809 * temp1017 + temp979 * temp1019;
            var ovR0C4 = temp976 * temp1020;

            var temp1022 = temp987 * temp1017 + temp1019;
            var ovR0C5 = temp976 * temp1022;

            var temp1024 = temp802 * temp892;
            var temp1025 = temp599 * temp895 + temp800 * temp1024;
            var temp1026 = temp899 + temp897 * temp981;
            var temp1027 = temp853 * temp1026;
            var temp1028 = temp809 * temp1025 + temp979 * temp1027;
            var ovR1C4 = temp976 * temp1028;

            var temp1030 = temp987 * temp1025 + temp1027;
            var ovR1C5 = temp976 * temp1030;

            var temp1032 = temp802 * temp903;
            var temp1033 = temp599 * temp906 + temp800 * temp1032;
            var temp1034 = temp911 + temp909 * temp981;
            var temp1035 = temp853 * temp1034;
            var temp1036 = temp809 * temp1033 + temp979 * temp1035;
            var ovR2C4 = temp976 * temp1036;

            var temp1038 = temp987 * temp1033 + temp1035;
            var ovR2C5 = temp976 * temp1038;

            var temp1040 = temp802 * temp915;
            var temp1041 = temp599 * temp918 + temp800 * temp1040;
            var temp1042 = temp839 * temp919;
            var temp1043 = temp921 + temp980 * temp1042;
            var temp1044 = temp853 * temp1043;
            var temp1045 = temp809 * temp1041 + temp979 * temp1044;
            var ovR3C4 = temp976 * temp1045;

            var temp1047 = temp987 * temp1041 + temp1044;
            var ovR3C5 = temp976 * temp1047;

            var temp1049 = temp802 * temp924;
            var temp1050 = temp599 * temp927 + temp800 * temp1049;
            var temp1051 = temp932 + temp930 * temp981;
            var temp1052 = temp853 * temp1051;
            var temp1053 = temp809 * temp1050 + temp979 * temp1052;
            var ovR4C4 = temp976 * temp1053;

            var temp1055 = temp987 * temp1050 + temp1052;
            var ovR4C5 = temp976 * temp1055;

            var temp1057 = temp802 * temp936;
            var temp1058 = temp599 * temp938 + temp800 * temp1057;
            var temp1059 = temp944 + temp941 * temp981;
            var temp1060 = temp853 * temp1059;
            var temp1061 = temp809 * temp1058 + temp979 * temp1060;
            var ovR5C4 = temp976 * temp1061;

            var temp1063 = temp987 * temp1058 + temp1060;
            var ovR5C5 = temp976 * temp1063;

            //Finish GA-FuL MetaContext Code Generation, 2025-07-01T01:52:25.5967628+03:00


            // Update diagonal matrix
            DiagonalMatrix[0, 0] = oaR0C0;
            DiagonalMatrix[0, 1] = oaR0C1;
            DiagonalMatrix[0, 2] = oaR0C2;
            DiagonalMatrix[0, 3] = oaR0C3;
            DiagonalMatrix[0, 4] = oaR0C4;
            DiagonalMatrix[0, 5] = oaR0C5;

            DiagonalMatrix[1, 0] = oaR0C1;
            DiagonalMatrix[1, 1] = oaR1C1;
            DiagonalMatrix[1, 2] = oaR1C2;
            DiagonalMatrix[1, 3] = oaR1C3;
            DiagonalMatrix[1, 4] = oaR1C4;
            DiagonalMatrix[1, 5] = oaR1C5;

            DiagonalMatrix[2, 0] = oaR0C2;
            DiagonalMatrix[2, 1] = oaR1C2;
            DiagonalMatrix[2, 2] = oaR2C2;
            DiagonalMatrix[2, 3] = oaR2C3;
            DiagonalMatrix[2, 4] = oaR2C4;
            DiagonalMatrix[2, 5] = oaR2C5;

            DiagonalMatrix[3, 0] = oaR0C3;
            DiagonalMatrix[3, 1] = oaR1C3;
            DiagonalMatrix[3, 2] = oaR2C3;
            DiagonalMatrix[3, 3] = oaR3C3;
            DiagonalMatrix[3, 4] = oaR3C4;
            DiagonalMatrix[3, 5] = oaR3C5;

            DiagonalMatrix[4, 0] = oaR0C4;
            DiagonalMatrix[4, 1] = oaR1C4;
            DiagonalMatrix[4, 2] = oaR2C4;
            DiagonalMatrix[4, 3] = oaR3C4;
            DiagonalMatrix[4, 4] = oaR4C4;
            DiagonalMatrix[4, 5] = oaR4C5;

            DiagonalMatrix[5, 0] = oaR0C5;
            DiagonalMatrix[5, 1] = oaR1C5;
            DiagonalMatrix[5, 2] = oaR2C5;
            DiagonalMatrix[5, 3] = oaR3C5;
            DiagonalMatrix[5, 4] = oaR4C5;
            DiagonalMatrix[5, 5] = oaR5C5;

            // Update eigen vectors matrix
            EigenVectors[0, 0] = ovR0C0;
            EigenVectors[0, 1] = ovR0C1;
            EigenVectors[0, 2] = ovR0C2;
            EigenVectors[0, 3] = ovR0C3;
            EigenVectors[0, 4] = ovR0C4;
            EigenVectors[0, 5] = ovR0C5;

            EigenVectors[1, 0] = ovR1C0;
            EigenVectors[1, 1] = ovR1C1;
            EigenVectors[1, 2] = ovR1C2;
            EigenVectors[1, 3] = ovR1C3;
            EigenVectors[1, 4] = ovR1C4;
            EigenVectors[1, 5] = ovR1C5;

            EigenVectors[2, 0] = ovR2C0;
            EigenVectors[2, 1] = ovR2C1;
            EigenVectors[2, 2] = ovR2C2;
            EigenVectors[2, 3] = ovR2C3;
            EigenVectors[2, 4] = ovR2C4;
            EigenVectors[2, 5] = ovR2C5;

            EigenVectors[3, 0] = ovR3C0;
            EigenVectors[3, 1] = ovR3C1;
            EigenVectors[3, 2] = ovR3C2;
            EigenVectors[3, 3] = ovR3C3;
            EigenVectors[3, 4] = ovR3C4;
            EigenVectors[3, 5] = ovR3C5;

            EigenVectors[4, 0] = ovR4C0;
            EigenVectors[4, 1] = ovR4C1;
            EigenVectors[4, 2] = ovR4C2;
            EigenVectors[4, 3] = ovR4C3;
            EigenVectors[4, 4] = ovR4C4;
            EigenVectors[4, 5] = ovR4C5;

            EigenVectors[5, 0] = ovR5C0;
            EigenVectors[5, 1] = ovR5C1;
            EigenVectors[5, 2] = ovR5C2;
            EigenVectors[5, 3] = ovR5C3;
            EigenVectors[5, 4] = ovR5C4;
            EigenVectors[5, 5] = ovR5C5;


        }
    }
    
    private void EigenDecomposeN()
    {
        for (var sweep = 0; sweep < MaxSweeps; sweep++)
        {
            if (GetOffDiagonalNormN() < NormTolerance) 
                break;

            // Apply all pairs of rotations
            for (var p = 0; p < Size - 1; p++)
                for (var q = p + 1; q < Size; q++)
                    Rotate(p, q);
        }
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
    
    public void EigenDecompose1()
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