using System.Text;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.EllipseFitting;

public sealed class JacobiEigen4X4Generated
{
    /// <summary>
    /// Code example for using the code
    /// </summary>
    public static void SampleUse()
    {
        var matrix = new double[,]
        {
            { 4, 1, 2, 1 },
            { 1, 5, 1, 2 },
            { 2, 1, 6, 1 },
            { 1, 2, 1, 7 }
        };

        var eig = new JacobiEigen4X4Generated(matrix);

        eig.EigenDecompose();

        Console.WriteLine(eig.ToString());
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


    public int MaxSweeps { get; set; } = 50;

    public double NormTolerance { get; set; } = 1e-14;

    public bool SortEigenValues { get; set; } = true;

    public double[,] SymmetricMatrix { get; }

    public double[,] DiagonalMatrix { get; }

    public double[] EigenValues { get; }

    public double[,] EigenVectors { get; }


    public JacobiEigen4X4Generated(double[,] symmetricMatrix)
    {
        SymmetricMatrix = symmetricMatrix;
        DiagonalMatrix = symmetricMatrix; //new double[4, 4];
        EigenValues = new double[4];
        EigenVectors = new double[4, 4];

        for (var i = 0; i < 4; i++)
            EigenVectors[i, i] = 1;

        //for (var i = 0; i < 4; i++)
        //for (var j = 0; j < 4; j++)
        //{
        //    DiagonalMatrix[i, j] = symmetricMatrix[i, j];
        //}
    }


    /// <summary>
    /// Compute off-diagonal norm
    /// </summary>
    /// <returns></returns>
    private double GetOffDiagonalNorm()
    {
        return
            DiagonalMatrix[0, 1] * DiagonalMatrix[0, 1] +
            DiagonalMatrix[0, 2] * DiagonalMatrix[0, 2] +
            DiagonalMatrix[0, 3] * DiagonalMatrix[0, 3] +
            DiagonalMatrix[1, 2] * DiagonalMatrix[1, 2] +
            DiagonalMatrix[1, 3] * DiagonalMatrix[1, 3] +
            DiagonalMatrix[2, 3] * DiagonalMatrix[2, 3];
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
        var t =
            tau >= 0
                ? 1 / (Math.Sqrt(1 + tau * tau) + tau)
                : -1 / (Math.Sqrt(1 + tau * tau) - tau);

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
        for (var r = 0; r < 4; r++)
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
        for (var r = 0; r < 4; r++)
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
        for (var i = 0; i < 4; i++)
            Swap(ref EigenVectors[i, col1], ref EigenVectors[i, col2]);
    }

    private void TestSwapEigenValues(int i, int j)
    {
        if (DiagonalMatrix[i, i] < DiagonalMatrix[j, j])
        {
            Swap(ref DiagonalMatrix[i, i], ref DiagonalMatrix[j, j]);
            SwapEigenVectors(i, j);
        }
    }

    private static double BinaryStep(double value)
    {
        return value >= 0 ? 1 : -1;
    }

    public void EigenDecompose()
    {
        // Sweep multiple times
        for (var sweep = 0; sweep < MaxSweeps; sweep++)
        {
            if (GetOffDiagonalNorm() < NormTolerance) 
                break;

            //Begin GA-FuL MetaContext Code Generation, 2025-07-01T03:54:34.9737731+03:00
            //MetaContext: 
            //Input Variables: 26 used, 0 not used, 26 total.
            //Temp Variables: 240 sub-expressions, 0 generated temps, 240 total.
            //Target Temp Variables: 240 total.
            //Output Variables: 26 total.
            //Computations: 1.4924812030075187 average, 397 total.
            //Memory Reads: 2.1315789473684212 average, 567 total.
            //Memory Writes: 266 total.
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
            //   iar0c2 = parameter: DiagonalMatrix[0, 2]
            //   iar0c3 = parameter: DiagonalMatrix[0, 3]
            //   iar1c1 = parameter: DiagonalMatrix[1, 1]
            //   iar1c2 = parameter: DiagonalMatrix[1, 2]
            //   iar1c3 = parameter: DiagonalMatrix[1, 3]
            //   iar2c2 = parameter: DiagonalMatrix[2, 2]
            //   iar2c3 = parameter: DiagonalMatrix[2, 3]
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

            var tmpVar99508 = -DiagonalMatrix[0, 0];
            var tmpVar99509 = DiagonalMatrix[1, 1] + tmpVar99508;
            var tmpVar99510 = 0.5 * 1 / DiagonalMatrix[0, 1] * tmpVar99509;
            var tmpVar99511 = BinaryStep(tmpVar99510);
            var tmpVar99512 = DiagonalMatrix[0, 1] * tmpVar99511;
            var tmpVar99517 = (Math.Sqrt(1 + tmpVar99510 * tmpVar99510)) + (Math.Abs(tmpVar99510));
            var tmpVar99518 = 1 / tmpVar99517;
            var tmpVar99519 = tmpVar99512 * tmpVar99518;
            var tmpVar99520 = -tmpVar99519;
            var tmpVar99521 = DiagonalMatrix[0, 0] + tmpVar99520;
            var tmpVar99522 = DiagonalMatrix[1, 2] * tmpVar99511;
            var tmpVar99523 = tmpVar99518 * tmpVar99522;
            var tmpVar99525 = DiagonalMatrix[0, 2] + -tmpVar99523;
            var tmpVar99530 = tmpVar99511 * tmpVar99511 * (Math.Pow(tmpVar99517, -2));
            var tmpVar99531 = 1 + tmpVar99530;
            var tmpVar99533 = 0.5 * 1 / tmpVar99525 * (Math.Sqrt(tmpVar99531));
            var tmpVar99534 = DiagonalMatrix[2, 2] + tmpVar99508;
            var tmpVar99535 = tmpVar99519 + tmpVar99534;
            var tmpVar99536 = tmpVar99533 * tmpVar99535;
            var tmpVar99537 = BinaryStep(tmpVar99536);
            var tmpVar99538 = 1 / (Math.Sqrt(tmpVar99531));
            var tmpVar99539 = tmpVar99525 * tmpVar99538;
            var tmpVar99544 = (Math.Sqrt(1 + tmpVar99536 * tmpVar99536)) + (Math.Abs(tmpVar99536));
            var tmpVar99545 = 1 / tmpVar99544;
            var tmpVar99546 = tmpVar99539 * tmpVar99545;
            var tmpVar99547 = tmpVar99537 * tmpVar99546;
            var tmpVar99548 = -tmpVar99547;
            var tmpVar99549 = tmpVar99521 + tmpVar99548;
            var tmpVar99551 = -1 * DiagonalMatrix[2, 3] * tmpVar99537;
            var tmpVar99553 = DiagonalMatrix[1, 3] * tmpVar99511;
            var tmpVar99554 = tmpVar99518 * tmpVar99553;
            var tmpVar99556 = DiagonalMatrix[0, 3] + -tmpVar99554;
            var tmpVar99558 = tmpVar99545 * tmpVar99551 + tmpVar99538 * tmpVar99556;
            var tmpVar99561 = tmpVar99537 * tmpVar99537 * (Math.Pow(tmpVar99544, -2));
            var tmpVar99563 = 1 / (Math.Sqrt(1 + tmpVar99561));
            var tmpVar99564 = tmpVar99558 * tmpVar99563;
            var tmpVar99567 = DiagonalMatrix[3, 3] + tmpVar99508;
            var tmpVar99568 = tmpVar99519 + tmpVar99567;
            var tmpVar99569 = tmpVar99547 + tmpVar99568;
            var tmpVar99570 = 0.5 * 1 / tmpVar99564 * tmpVar99569;
            var tmpVar99575 = (Math.Sqrt(1 + tmpVar99570 * tmpVar99570)) + (Math.Abs(tmpVar99570));
            var tmpVar99576 = 1 / tmpVar99575;
            var tmpVar99577 = tmpVar99564 * tmpVar99576;
            var tmpVar99578 = BinaryStep(tmpVar99570);
            var tmpVar99579 = tmpVar99577 * tmpVar99578;
            var oaR0C0 = tmpVar99549 + -tmpVar99579;

            var tmpVar99583 = (Math.Pow(tmpVar99575, -2)) * tmpVar99578 * tmpVar99578;
            var tmpVar99585 = 1 / (Math.Sqrt(1 + tmpVar99583));
            var tmpVar99586 = tmpVar99545 * tmpVar99563;
            var tmpVar99588 = -1 * EigenVectors[0, 2] * tmpVar99537;
            var tmpVar99590 = tmpVar99538 * tmpVar99563;
            var tmpVar99592 = -1 * EigenVectors[0, 1] * tmpVar99511;
            var tmpVar99594 = EigenVectors[0, 0] + tmpVar99518 * tmpVar99592;
            var tmpVar99596 = tmpVar99586 * tmpVar99588 + tmpVar99590 * tmpVar99594;
            var tmpVar99597 = EigenVectors[0, 3] * tmpVar99576;
            var tmpVar99598 = -tmpVar99578;
            var tmpVar99600 = tmpVar99596 + tmpVar99597 * tmpVar99598;
            var ovR0C0 = tmpVar99585 * tmpVar99600;

            var tmpVar99601 = EigenVectors[1, 1] * tmpVar99511;
            var tmpVar99602 = tmpVar99518 * tmpVar99601;
            var tmpVar99604 = EigenVectors[1, 0] + -tmpVar99602;
            var tmpVar99606 = EigenVectors[1, 2] * tmpVar99545;
            var tmpVar99608 = -1 * tmpVar99537 * tmpVar99563;
            var tmpVar99610 = tmpVar99590 * tmpVar99604 + tmpVar99606 * tmpVar99608;
            var tmpVar99611 = EigenVectors[1, 3] * tmpVar99576;
            var tmpVar99613 = tmpVar99610 + tmpVar99598 * tmpVar99611;
            var ovR1C0 = tmpVar99585 * tmpVar99613;

            var tmpVar99615 = -1 * EigenVectors[2, 2] * tmpVar99537;
            var tmpVar99617 = EigenVectors[2, 1] * tmpVar99511;
            var tmpVar99618 = tmpVar99518 * tmpVar99617;
            var tmpVar99620 = EigenVectors[2, 0] + -tmpVar99618;
            var tmpVar99622 = tmpVar99586 * tmpVar99615 + tmpVar99590 * tmpVar99620;
            var tmpVar99623 = EigenVectors[2, 3] * tmpVar99576;
            var tmpVar99625 = tmpVar99622 + tmpVar99598 * tmpVar99623;
            var ovR2C0 = tmpVar99585 * tmpVar99625;

            var tmpVar99626 = EigenVectors[3, 2] * tmpVar99545;
            var tmpVar99628 = EigenVectors[3, 1] * tmpVar99511;
            var tmpVar99629 = tmpVar99518 * tmpVar99628;
            var tmpVar99631 = EigenVectors[3, 0] + -tmpVar99629;
            var tmpVar99633 = tmpVar99608 * tmpVar99626 + tmpVar99590 * tmpVar99631;
            var tmpVar99634 = EigenVectors[3, 3] * tmpVar99576;
            var tmpVar99636 = tmpVar99633 + tmpVar99598 * tmpVar99634;
            var ovR3C0 = tmpVar99585 * tmpVar99636;

            var tmpVar99637 = DiagonalMatrix[1, 1] + tmpVar99519;
            var tmpVar99638 = DiagonalMatrix[0, 2] * tmpVar99511;
            var tmpVar99640 = DiagonalMatrix[1, 2] + tmpVar99518 * tmpVar99638;
            var tmpVar99641 = tmpVar99590 * tmpVar99640;
            var tmpVar99644 = DiagonalMatrix[2, 2] + tmpVar99547;
            var tmpVar99645 = -DiagonalMatrix[1, 1];
            var tmpVar99646 = tmpVar99520 + tmpVar99645;
            var tmpVar99647 = tmpVar99644 + tmpVar99646;
            var tmpVar99648 = 0.5 * 1 / tmpVar99641 * tmpVar99647;
            var tmpVar99649 = BinaryStep(tmpVar99648);
            var tmpVar99654 = (Math.Sqrt(1 + tmpVar99648 * tmpVar99648)) + (Math.Abs(tmpVar99648));
            var tmpVar99655 = 1 / tmpVar99654;
            var tmpVar99656 = tmpVar99641 * tmpVar99655;
            var tmpVar99657 = tmpVar99649 * tmpVar99656;
            var tmpVar99658 = -tmpVar99657;
            var tmpVar99659 = tmpVar99637 + tmpVar99658;
            var tmpVar99660 = tmpVar99585 * tmpVar99649;
            var tmpVar99661 = tmpVar99655 * tmpVar99660;
            var tmpVar99662 = tmpVar99537 * tmpVar99538;
            var tmpVar99663 = tmpVar99545 * tmpVar99662;
            var tmpVar99665 = DiagonalMatrix[2, 3] + tmpVar99556 * tmpVar99663;
            var tmpVar99666 = tmpVar99563 * tmpVar99665;
            var tmpVar99667 = tmpVar99661 * tmpVar99666;
            var tmpVar99670 = tmpVar99649 * tmpVar99649 * (Math.Pow(tmpVar99654, -2));
            var tmpVar99672 = 1 / (Math.Sqrt(1 + tmpVar99670));
            var tmpVar99673 = tmpVar99667 * tmpVar99672;
            var tmpVar99674 = tmpVar99585 * tmpVar99672;
            var tmpVar99675 = DiagonalMatrix[0, 3] * tmpVar99511;
            var tmpVar99677 = DiagonalMatrix[1, 3] + tmpVar99518 * tmpVar99675;
            var tmpVar99678 = tmpVar99545 * tmpVar99576;
            var tmpVar99679 = tmpVar99578 * tmpVar99678;
            var tmpVar99680 = tmpVar99608 * tmpVar99640;
            var tmpVar99682 = tmpVar99677 + tmpVar99679 * tmpVar99680;
            var tmpVar99683 = tmpVar99538 * tmpVar99682;
            var tmpVar99684 = tmpVar99674 * tmpVar99683;
            var tmpVar99686 = tmpVar99673 + -tmpVar99684;
            var tmpVar99688 = -tmpVar99673 + tmpVar99684;
            var tmpVar99691 = DiagonalMatrix[3, 3] + tmpVar99645;
            var tmpVar99692 = tmpVar99520 + tmpVar99691;
            var tmpVar99693 = tmpVar99579 + tmpVar99692;
            var tmpVar99694 = tmpVar99657 + tmpVar99693;
            var tmpVar99695 = 0.5 * 1 / tmpVar99688 * tmpVar99694;
            var tmpVar99700 = (Math.Sqrt(1 + tmpVar99695 * tmpVar99695)) + (Math.Abs(tmpVar99695));
            var tmpVar99701 = 1 / tmpVar99700;
            var tmpVar99702 = tmpVar99686 * tmpVar99701;
            var tmpVar99703 = BinaryStep(tmpVar99695);
            var oaR1C1 = tmpVar99659 + tmpVar99702 * tmpVar99703;

            var tmpVar99705 = tmpVar99538 * tmpVar99545;
            var tmpVar99706 = tmpVar99585 * tmpVar99680;
            var tmpVar99707 = tmpVar99705 * tmpVar99706;
            var tmpVar99708 = -tmpVar99649;
            var tmpVar99709 = tmpVar99655 * tmpVar99708;
            var tmpVar99710 = tmpVar99585 * tmpVar99666;
            var tmpVar99712 = -1 * tmpVar99576 * tmpVar99578;
            var tmpVar99713 = tmpVar99710 * tmpVar99712;
            var tmpVar99715 = tmpVar99707 + tmpVar99709 * tmpVar99713;
            var tmpVar99716 = tmpVar99585 * tmpVar99598;
            var tmpVar99717 = tmpVar99677 * tmpVar99716;
            var tmpVar99718 = tmpVar99538 * tmpVar99576;
            var tmpVar99719 = tmpVar99717 * tmpVar99718;
            var tmpVar99720 = tmpVar99715 + tmpVar99719;
            var tmpVar99721 = tmpVar99672 * tmpVar99720;
            var tmpVar99724 = (Math.Pow(tmpVar99700, -2)) * tmpVar99703 * tmpVar99703;
            var tmpVar99726 = 1 / (Math.Sqrt(1 + tmpVar99724));
            var oaR0C1 = tmpVar99721 * tmpVar99726;

            var tmpVar99727 = tmpVar99672 * tmpVar99726;
            var tmpVar99729 = EigenVectors[0, 2] + tmpVar99594 * tmpVar99663;
            var tmpVar99730 = tmpVar99563 * tmpVar99729;
            var tmpVar99731 = tmpVar99655 * tmpVar99730;
            var tmpVar99732 = tmpVar99649 * tmpVar99731;
            var tmpVar99734 = EigenVectors[0, 0] * tmpVar99511;
            var tmpVar99736 = EigenVectors[0, 1] + tmpVar99518 * tmpVar99734;
            var tmpVar99737 = tmpVar99538 * tmpVar99736;
            var tmpVar99738 = -tmpVar99732 + tmpVar99737;
            var tmpVar99741 = -1 * tmpVar99585 * tmpVar99726;
            var tmpVar99742 = tmpVar99701 * tmpVar99703;
            var tmpVar99743 = tmpVar99741 * tmpVar99742;
            var tmpVar99744 = tmpVar99576 * tmpVar99578;
            var tmpVar99746 = EigenVectors[0, 3] + tmpVar99596 * tmpVar99744;
            var ovR0C1 = tmpVar99727 * tmpVar99738 + tmpVar99743 * tmpVar99746;

            var tmpVar99749 = EigenVectors[1, 2] + tmpVar99604 * tmpVar99663;
            var tmpVar99750 = tmpVar99563 * tmpVar99749;
            var tmpVar99751 = tmpVar99655 * tmpVar99750;
            var tmpVar99753 = EigenVectors[1, 0] * tmpVar99511;
            var tmpVar99755 = EigenVectors[1, 1] + tmpVar99518 * tmpVar99753;
            var tmpVar99756 = tmpVar99538 * tmpVar99755;
            var tmpVar99757 = tmpVar99708 * tmpVar99751 + tmpVar99756;
            var tmpVar99760 = EigenVectors[1, 3] + tmpVar99610 * tmpVar99744;
            var ovR1C1 = tmpVar99727 * tmpVar99757 + tmpVar99743 * tmpVar99760;

            var tmpVar99763 = EigenVectors[2, 2] + tmpVar99620 * tmpVar99663;
            var tmpVar99764 = tmpVar99563 * tmpVar99763;
            var tmpVar99765 = tmpVar99655 * tmpVar99764;
            var tmpVar99766 = tmpVar99649 * tmpVar99765;
            var tmpVar99768 = EigenVectors[2, 0] * tmpVar99511;
            var tmpVar99770 = EigenVectors[2, 1] + tmpVar99518 * tmpVar99768;
            var tmpVar99771 = tmpVar99538 * tmpVar99770;
            var tmpVar99772 = -tmpVar99766 + tmpVar99771;
            var tmpVar99775 = EigenVectors[2, 3] + tmpVar99622 * tmpVar99744;
            var ovR2C1 = tmpVar99727 * tmpVar99772 + tmpVar99743 * tmpVar99775;

            var tmpVar99778 = EigenVectors[3, 2] + tmpVar99631 * tmpVar99663;
            var tmpVar99779 = tmpVar99563 * tmpVar99778;
            var tmpVar99780 = tmpVar99655 * tmpVar99779;
            var tmpVar99782 = EigenVectors[3, 0] * tmpVar99511;
            var tmpVar99784 = EigenVectors[3, 1] + tmpVar99518 * tmpVar99782;
            var tmpVar99785 = tmpVar99538 * tmpVar99784;
            var tmpVar99786 = tmpVar99708 * tmpVar99780 + tmpVar99785;
            var tmpVar99789 = EigenVectors[3, 3] + tmpVar99633 * tmpVar99744;
            var ovR3C1 = tmpVar99727 * tmpVar99786 + tmpVar99743 * tmpVar99789;

            var tmpVar99791 = tmpVar99644 + tmpVar99657;
            var tmpVar99792 = tmpVar99674 * tmpVar99726;
            var tmpVar99793 = tmpVar99649 * tmpVar99655;
            var tmpVar99795 = tmpVar99666 + tmpVar99683 * tmpVar99793;
            var tmpVar99796 = tmpVar99792 * tmpVar99795;
            var tmpVar99800 = DiagonalMatrix[3, 3] + tmpVar99579;
            var tmpVar99801 = tmpVar99658 + tmpVar99800;
            var tmpVar99802 = tmpVar99688 * tmpVar99701;
            var tmpVar99803 = tmpVar99703 * tmpVar99802;
            var tmpVar99804 = tmpVar99801 + tmpVar99803;
            var tmpVar99806 = -DiagonalMatrix[2, 2] + tmpVar99548;
            var tmpVar99807 = tmpVar99804 + tmpVar99806;
            var tmpVar99808 = 0.5 * 1 / tmpVar99796 * tmpVar99807;
            var tmpVar99813 = (Math.Sqrt(1 + tmpVar99808 * tmpVar99808)) + (Math.Abs(tmpVar99808));
            var tmpVar99814 = 1 / tmpVar99813;
            var tmpVar99815 = -1 * tmpVar99796 * tmpVar99814;
            var tmpVar99816 = BinaryStep(tmpVar99808);
            var oaR2C2 = tmpVar99791 + tmpVar99815 * tmpVar99816;

            var tmpVar99818 = tmpVar99800 + tmpVar99803;
            var tmpVar99819 = tmpVar99796 * tmpVar99814;
            var oaR3C3 = tmpVar99818 + tmpVar99816 * tmpVar99819;

            var tmpVar99823 = (Math.Pow(tmpVar99813, -2)) * tmpVar99816 * tmpVar99816;
            var tmpVar99825 = 1 / (Math.Sqrt(1 + tmpVar99823));
            var tmpVar99826 = tmpVar99707 + tmpVar99719;
            var tmpVar99828 = tmpVar99713 + tmpVar99793 * tmpVar99826;
            var tmpVar99829 = tmpVar99672 * tmpVar99828;
            var tmpVar99831 = -1 * tmpVar99814 * tmpVar99816;
            var tmpVar99832 = tmpVar99721 * tmpVar99726;
            var tmpVar99833 = tmpVar99742 * tmpVar99832;
            var tmpVar99835 = tmpVar99829 + tmpVar99831 * tmpVar99833;
            var oaR0C2 = tmpVar99825 * tmpVar99835;

            var tmpVar99836 = tmpVar99814 * tmpVar99816;
            var tmpVar99838 = tmpVar99833 + tmpVar99829 * tmpVar99836;
            var oaR0C3 = tmpVar99825 * tmpVar99838;

            var tmpVar99840 = -1 * tmpVar99674 * tmpVar99726;
            var tmpVar99841 = tmpVar99742 * tmpVar99840;
            var tmpVar99842 = tmpVar99795 * tmpVar99841;
            var oaR1C2 = tmpVar99825 * tmpVar99842;

            var tmpVar99843 = tmpVar99825 * tmpVar99836;
            var oaR1C3 = tmpVar99842 * tmpVar99843;

            var tmpVar99845 = tmpVar99730 + tmpVar99737 * tmpVar99793;
            var tmpVar99846 = tmpVar99672 * tmpVar99845;
            var tmpVar99848 = tmpVar99672 * tmpVar99738;
            var tmpVar99850 = tmpVar99585 * tmpVar99746 + tmpVar99742 * tmpVar99848;
            var tmpVar99851 = tmpVar99726 * tmpVar99850;
            var tmpVar99853 = tmpVar99846 + tmpVar99831 * tmpVar99851;
            var ovR0C2 = tmpVar99825 * tmpVar99853;

            var tmpVar99855 = tmpVar99836 * tmpVar99846 + tmpVar99851;
            var ovR0C3 = tmpVar99825 * tmpVar99855;

            var tmpVar99857 = tmpVar99750 + tmpVar99756 * tmpVar99793;
            var tmpVar99858 = tmpVar99672 * tmpVar99857;
            var tmpVar99860 = tmpVar99672 * tmpVar99742;
            var tmpVar99862 = tmpVar99585 * tmpVar99760 + tmpVar99757 * tmpVar99860;
            var tmpVar99863 = tmpVar99726 * tmpVar99862;
            var tmpVar99865 = tmpVar99858 + tmpVar99831 * tmpVar99863;
            var ovR1C2 = tmpVar99825 * tmpVar99865;

            var tmpVar99867 = tmpVar99836 * tmpVar99858 + tmpVar99863;
            var ovR1C3 = tmpVar99825 * tmpVar99867;

            var tmpVar99869 = tmpVar99764 + tmpVar99771 * tmpVar99793;
            var tmpVar99870 = tmpVar99672 * tmpVar99869;
            var tmpVar99873 = tmpVar99585 * tmpVar99775 + tmpVar99772 * tmpVar99860;
            var tmpVar99874 = tmpVar99726 * tmpVar99873;
            var tmpVar99876 = tmpVar99870 + tmpVar99831 * tmpVar99874;
            var ovR2C2 = tmpVar99825 * tmpVar99876;

            var tmpVar99878 = tmpVar99836 * tmpVar99870 + tmpVar99874;
            var ovR2C3 = tmpVar99825 * tmpVar99878;

            var tmpVar99880 = tmpVar99779 + tmpVar99785 * tmpVar99793;
            var tmpVar99881 = tmpVar99672 * tmpVar99880;
            var tmpVar99884 = tmpVar99585 * tmpVar99789 + tmpVar99786 * tmpVar99860;
            var tmpVar99885 = tmpVar99726 * tmpVar99884;
            var tmpVar99887 = tmpVar99881 + tmpVar99831 * tmpVar99885;
            var ovR3C2 = tmpVar99825 * tmpVar99887;

            var tmpVar99889 = tmpVar99836 * tmpVar99881 + tmpVar99885;
            var ovR3C3 = tmpVar99825 * tmpVar99889;

            //Finish GA-FuL MetaContext Code Generation, 2025-07-01T03:54:34.9745089+03:00

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

            //// Apply all pairs of rotations
            //Rotate(0, 1);
            //Rotate(0, 2);
            //Rotate(0, 3);
            //Rotate(1, 2);
            //Rotate(1, 3);
            //Rotate(2, 3);
        }

        // Sort eigenvalues and eigenvectors descending
        if (SortEigenValues)
        {
            TestSwapEigenValues(0, 1);
            TestSwapEigenValues(0, 2);
            TestSwapEigenValues(0, 3);
            TestSwapEigenValues(1, 2);
            TestSwapEigenValues(1, 3);
            TestSwapEigenValues(2, 3);
        }

        // Extract eigenvalues from diagonal
        EigenValues[0] = DiagonalMatrix[0, 0];
        EigenValues[1] = DiagonalMatrix[1, 1];
        EigenValues[2] = DiagonalMatrix[2, 2];
        EigenValues[3] = DiagonalMatrix[3, 3];
    }

    public override string ToString()
    {
        var composer = new StringBuilder();

        composer
            .AppendLine("Symmetric Matrix: [")
            .AppendLine($"    {SymmetricMatrix[0, 0]:F10}, {SymmetricMatrix[0, 1]:F10}, {SymmetricMatrix[0, 2]:F10}, {SymmetricMatrix[0, 3]:F10}; ")
            .AppendLine($"    {SymmetricMatrix[1, 0]:F10}, {SymmetricMatrix[1, 1]:F10}, {SymmetricMatrix[1, 2]:F10}, {SymmetricMatrix[1, 3]:F10}; ")
            .AppendLine($"    {SymmetricMatrix[2, 0]:F10}, {SymmetricMatrix[2, 1]:F10}, {SymmetricMatrix[2, 2]:F10}, {SymmetricMatrix[2, 3]:F10}; ")
            .AppendLine($"    {SymmetricMatrix[3, 0]:F10}, {SymmetricMatrix[3, 1]:F10}, {SymmetricMatrix[3, 2]:F10}, {SymmetricMatrix[3, 3]:F10}")
            .AppendLine("]")
            .AppendLine();

        composer
            .AppendLine("Diagonal Matrix: [")
            .AppendLine($"    {DiagonalMatrix[0, 0]:F10}, {DiagonalMatrix[0, 1]:F10}, {DiagonalMatrix[0, 2]:F10}, {DiagonalMatrix[0, 3]:F10}; ")
            .AppendLine($"    {DiagonalMatrix[1, 0]:F10}, {DiagonalMatrix[1, 1]:F10}, {DiagonalMatrix[1, 2]:F10}, {DiagonalMatrix[1, 3]:F10}; ")
            .AppendLine($"    {DiagonalMatrix[2, 0]:F10}, {DiagonalMatrix[2, 1]:F10}, {DiagonalMatrix[2, 2]:F10}, {DiagonalMatrix[2, 3]:F10}; ")
            .AppendLine($"    {DiagonalMatrix[3, 0]:F10}, {DiagonalMatrix[3, 1]:F10}, {DiagonalMatrix[3, 2]:F10}, {DiagonalMatrix[3, 3]:F10}")
            .AppendLine("]")
            .AppendLine();

        composer
            .Append($"Eigen Values: [{EigenValues[0]:F10}, {EigenValues[1]:F10}, {EigenValues[2]:F10}, {EigenValues[3]:F10}]")
            .AppendLine();

        composer
            .AppendLine("Eigen Vectors Matrix: [")
            .AppendLine($"    {EigenVectors[0, 0]:F10}, {EigenVectors[0, 1]:F10}, {EigenVectors[0, 2]:F10}, {EigenVectors[0, 3]:F10}; ")
            .AppendLine($"    {EigenVectors[1, 0]:F10}, {EigenVectors[1, 1]:F10}, {EigenVectors[1, 2]:F10}, {EigenVectors[1, 3]:F10}; ")
            .AppendLine($"    {EigenVectors[2, 0]:F10}, {EigenVectors[2, 1]:F10}, {EigenVectors[2, 2]:F10}, {EigenVectors[2, 3]:F10}; ")
            .AppendLine($"    {EigenVectors[3, 0]:F10}, {EigenVectors[3, 1]:F10}, {EigenVectors[3, 2]:F10}, {EigenVectors[3, 3]:F10}")
            .AppendLine("]")
            .AppendLine();

        return composer.ToString();
    }
}