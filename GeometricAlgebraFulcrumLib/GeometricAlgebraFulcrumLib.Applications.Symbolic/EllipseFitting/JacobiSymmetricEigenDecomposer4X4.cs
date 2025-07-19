using System.Diagnostics;
using System.Text;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.EllipseFitting;

public sealed class JacobiSymmetricEigenDecomposer4X4
{
    /// <summary>
    /// Code example for using the code
    /// </summary>
    public static void SampleUse()
    {
        const int size = 4;
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


    public JacobiSymmetricEigenDecomposer4X4(double[,] symmetricMatrix)
    {
        Size = symmetricMatrix.GetLength(0);

        Debug.Assert(Size >= 2 && Size == symmetricMatrix.GetLength(1));

        SymmetricMatrix = symmetricMatrix;
        DiagonalMatrix = new double[Size, Size];
        EigenValues = new double[Size];
        EigenVectors = new double[Size, Size];

        for (var i = 0; i < Size; i++)
            EigenVectors[i, i] = 1;

        for (var i = 0; i < Size; i++)
            for (var j = 0; j < Size; j++)
            {
                DiagonalMatrix[i, j] = symmetricMatrix[i, j];
            }
    }


    /// <summary>
    /// Compute off-diagonal norm
    /// </summary>
    /// <returns></returns>
    private double GetOffDiagonalNorm()
    {
        return DiagonalMatrix[0, 1] * DiagonalMatrix[0, 1] +
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
        Swap(ref EigenVectors[0, col1], ref EigenVectors[0, col2]);
        Swap(ref EigenVectors[1, col1], ref EigenVectors[1, col2]);
        Swap(ref EigenVectors[2, col1], ref EigenVectors[2, col2]);
        Swap(ref EigenVectors[3, col1], ref EigenVectors[3, col2]);
    }
    
    private void TestSwapEigenValues(int i, int j)
    {
        if (EigenValues[i] < EigenValues[j])
        {
            Swap(ref EigenValues[i], ref EigenValues[j]);
            SwapEigenVectors(i, j);
        }
    }


    private void EigenDecompose4()
    {
        for (var sweep = 0; sweep < MaxSweeps; sweep++)
        {
            if (GetOffDiagonalNorm() < NormTolerance) break;

            //Begin GA-FuL MetaContext Code Generation, 2025-07-01T03:13:42.8137988+03:00
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
            var temp13 = DiagonalMatrix[1, 2] * temp6;
            var temp14 = temp4 * temp13;
            var temp15 = temp12 * temp14;
            var temp16 = DiagonalMatrix[0, 2] * temp12 + -temp15;
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
            var temp27 = DiagonalMatrix[1, 3] * temp6;
            var temp28 = temp4 * temp27;
            var temp29 = temp12 * temp28;
            var temp30 = DiagonalMatrix[0, 3] * temp12 + -temp29;
            var temp31 = temp20 * temp20 * Math.Pow(temp21, -2);
            var temp32 = 1 / Math.Sqrt(1 + temp31);
            var temp33 = DiagonalMatrix[2, 3] * temp22;
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
            var temp98 = DiagonalMatrix[0, 2] * temp6;
            var temp99 = temp4 * temp98;
            var temp100 = temp12 * (DiagonalMatrix[1, 2] + temp99);
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
            var temp114 = DiagonalMatrix[0, 3] * temp6;
            var temp115 = temp4 * temp114;
            var temp116 = temp12 * (DiagonalMatrix[1, 3] + temp115);
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
            var temp128 = temp32 * (DiagonalMatrix[2, 3] + temp127);
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

            //Finish GA-FuL MetaContext Code Generation, 2025-07-01T03:13:42.9026186+03:00


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
    
    public void EigenDecompose()
    {
        EigenDecompose4();
        
        // Extract eigenvalues from diagonal
        EigenValues[0] = DiagonalMatrix[0, 0];
        EigenValues[1] = DiagonalMatrix[1, 1];
        EigenValues[2] = DiagonalMatrix[2, 2];
        EigenValues[3] = DiagonalMatrix[3, 3];

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
                .Append(EigenValues[i].ToString("F10"))
                .AppendLine(i < Size - 1 ? ";" : "");
        }

        composer.AppendLine("]").AppendLine();


        return composer.ToString();
    }
}