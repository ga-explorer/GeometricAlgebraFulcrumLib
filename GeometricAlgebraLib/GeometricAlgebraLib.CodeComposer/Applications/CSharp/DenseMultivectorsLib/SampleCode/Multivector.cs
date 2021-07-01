using System.Collections;
using System.Collections.Generic;

namespace GeometricAlgebraLib.CodeComposer.Applications.CSharp.DenseMultivectorsLib.SampleCode
{
    public sealed class Multivector
        : IReadOnlyList<double>
    {
        public static Multivector operator +(Multivector mv1, Multivector mv2)
        {
            var mv = new Multivector();

            //Begin GaClc SymbolicContext Code Generation, 2021-06-26T22:48:42.7671770+02:00
            //SymbolicContext:
            //Input Variables: 32 used, 0 not used, 32 total.
            //Temp Variables: 0 sub-expressions, 0 generated temps, 0 total.
            //Output Variables: 16 total.
            //Computations: 1 average, 16 total.
            //Memory Reads: 2 average, 32 total.
            //Memory Writes: 16 total.
            //
            //SymbolicContext Binding Data:
            //   mv1s0 = parameter: mv1._scalarsArray[0]
            //   mv1s1 = parameter: mv1._scalarsArray[1]
            //   mv1s2 = parameter: mv1._scalarsArray[2]
            //   mv1s3 = parameter: mv1._scalarsArray[3]
            //   mv1s4 = parameter: mv1._scalarsArray[4]
            //   mv1s5 = parameter: mv1._scalarsArray[5]
            //   mv1s6 = parameter: mv1._scalarsArray[6]
            //   mv1s7 = parameter: mv1._scalarsArray[7]
            //   mv1s8 = parameter: mv1._scalarsArray[8]
            //   mv1s9 = parameter: mv1._scalarsArray[9]
            //   mv1s10 = parameter: mv1._scalarsArray[10]
            //   mv1s11 = parameter: mv1._scalarsArray[11]
            //   mv1s12 = parameter: mv1._scalarsArray[12]
            //   mv1s13 = parameter: mv1._scalarsArray[13]
            //   mv1s14 = parameter: mv1._scalarsArray[14]
            //   mv1s15 = parameter: mv1._scalarsArray[15]
            //   mv2s0 = parameter: mv2._scalarsArray[0]
            //   mv2s1 = parameter: mv2._scalarsArray[1]
            //   mv2s2 = parameter: mv2._scalarsArray[2]
            //   mv2s3 = parameter: mv2._scalarsArray[3]
            //   mv2s4 = parameter: mv2._scalarsArray[4]
            //   mv2s5 = parameter: mv2._scalarsArray[5]
            //   mv2s6 = parameter: mv2._scalarsArray[6]
            //   mv2s7 = parameter: mv2._scalarsArray[7]
            //   mv2s8 = parameter: mv2._scalarsArray[8]
            //   mv2s9 = parameter: mv2._scalarsArray[9]
            //   mv2s10 = parameter: mv2._scalarsArray[10]
            //   mv2s11 = parameter: mv2._scalarsArray[11]
            //   mv2s12 = parameter: mv2._scalarsArray[12]
            //   mv2s13 = parameter: mv2._scalarsArray[13]
            //   mv2s14 = parameter: mv2._scalarsArray[14]
            //   mv2s15 = parameter: mv2._scalarsArray[15]

            mv._scalarsArray[0] = mv1._scalarsArray[0] + mv2._scalarsArray[0];

            mv._scalarsArray[1] = mv1._scalarsArray[1] + mv2._scalarsArray[1];

            mv._scalarsArray[2] = mv1._scalarsArray[2] + mv2._scalarsArray[2];

            mv._scalarsArray[3] = mv1._scalarsArray[3] + mv2._scalarsArray[3];

            mv._scalarsArray[4] = mv1._scalarsArray[4] + mv2._scalarsArray[4];

            mv._scalarsArray[5] = mv1._scalarsArray[5] + mv2._scalarsArray[5];

            mv._scalarsArray[6] = mv1._scalarsArray[6] + mv2._scalarsArray[6];

            mv._scalarsArray[7] = mv1._scalarsArray[7] + mv2._scalarsArray[7];

            mv._scalarsArray[8] = mv1._scalarsArray[8] + mv2._scalarsArray[8];

            mv._scalarsArray[9] = mv1._scalarsArray[9] + mv2._scalarsArray[9];

            mv._scalarsArray[10] = mv1._scalarsArray[10] + mv2._scalarsArray[10];

            mv._scalarsArray[11] = mv1._scalarsArray[11] + mv2._scalarsArray[11];

            mv._scalarsArray[12] = mv1._scalarsArray[12] + mv2._scalarsArray[12];

            mv._scalarsArray[13] = mv1._scalarsArray[13] + mv2._scalarsArray[13];

            mv._scalarsArray[14] = mv1._scalarsArray[14] + mv2._scalarsArray[14];

            mv._scalarsArray[15] = mv1._scalarsArray[15] + mv2._scalarsArray[15];

            //Finish GaClc SymbolicContext Code Generation, 2021-06-26T22:48:42.7838694+02:00


            return mv;
        }

        public static Multivector operator -(Multivector mv1, Multivector mv2)
        {
            var mv = new Multivector();

            //Begin GaClc SymbolicContext Code Generation, 2021-06-26T22:48:42.9142741+02:00
            //SymbolicContext:
            //Input Variables: 32 used, 0 not used, 32 total.
            //Temp Variables: 16 sub-expressions, 0 generated temps, 16 total.
            //Target Temp Variables: 1 total.
            //Output Variables: 16 total.
            //Computations: 1 average, 32 total.
            //Memory Reads: 1.5 average, 48 total.
            //Memory Writes: 32 total.
            //
            //SymbolicContext Binding Data:
            //   -1 = constant: '-1'
            //   mv1s0 = parameter: mv1._scalarsArray[0]
            //   mv1s1 = parameter: mv1._scalarsArray[1]
            //   mv1s2 = parameter: mv1._scalarsArray[2]
            //   mv1s3 = parameter: mv1._scalarsArray[3]
            //   mv1s4 = parameter: mv1._scalarsArray[4]
            //   mv1s5 = parameter: mv1._scalarsArray[5]
            //   mv1s6 = parameter: mv1._scalarsArray[6]
            //   mv1s7 = parameter: mv1._scalarsArray[7]
            //   mv1s8 = parameter: mv1._scalarsArray[8]
            //   mv1s9 = parameter: mv1._scalarsArray[9]
            //   mv1s10 = parameter: mv1._scalarsArray[10]
            //   mv1s11 = parameter: mv1._scalarsArray[11]
            //   mv1s12 = parameter: mv1._scalarsArray[12]
            //   mv1s13 = parameter: mv1._scalarsArray[13]
            //   mv1s14 = parameter: mv1._scalarsArray[14]
            //   mv1s15 = parameter: mv1._scalarsArray[15]
            //   mv2s0 = parameter: mv2._scalarsArray[0]
            //   mv2s1 = parameter: mv2._scalarsArray[1]
            //   mv2s2 = parameter: mv2._scalarsArray[2]
            //   mv2s3 = parameter: mv2._scalarsArray[3]
            //   mv2s4 = parameter: mv2._scalarsArray[4]
            //   mv2s5 = parameter: mv2._scalarsArray[5]
            //   mv2s6 = parameter: mv2._scalarsArray[6]
            //   mv2s7 = parameter: mv2._scalarsArray[7]
            //   mv2s8 = parameter: mv2._scalarsArray[8]
            //   mv2s9 = parameter: mv2._scalarsArray[9]
            //   mv2s10 = parameter: mv2._scalarsArray[10]
            //   mv2s11 = parameter: mv2._scalarsArray[11]
            //   mv2s12 = parameter: mv2._scalarsArray[12]
            //   mv2s13 = parameter: mv2._scalarsArray[13]
            //   mv2s14 = parameter: mv2._scalarsArray[14]
            //   mv2s15 = parameter: mv2._scalarsArray[15]

            var tempVar0 = -1 * mv2._scalarsArray[0];
            mv._scalarsArray[0] = mv1._scalarsArray[0] + tempVar0;

            tempVar0 = -1 * mv2._scalarsArray[1];
            mv._scalarsArray[1] = mv1._scalarsArray[1] + tempVar0;

            tempVar0 = -1 * mv2._scalarsArray[2];
            mv._scalarsArray[2] = mv1._scalarsArray[2] + tempVar0;

            tempVar0 = -1 * mv2._scalarsArray[3];
            mv._scalarsArray[3] = mv1._scalarsArray[3] + tempVar0;

            tempVar0 = -1 * mv2._scalarsArray[4];
            mv._scalarsArray[4] = mv1._scalarsArray[4] + tempVar0;

            tempVar0 = -1 * mv2._scalarsArray[5];
            mv._scalarsArray[5] = mv1._scalarsArray[5] + tempVar0;

            tempVar0 = -1 * mv2._scalarsArray[6];
            mv._scalarsArray[6] = mv1._scalarsArray[6] + tempVar0;

            tempVar0 = -1 * mv2._scalarsArray[7];
            mv._scalarsArray[7] = mv1._scalarsArray[7] + tempVar0;

            tempVar0 = -1 * mv2._scalarsArray[8];
            mv._scalarsArray[8] = mv1._scalarsArray[8] + tempVar0;

            tempVar0 = -1 * mv2._scalarsArray[9];
            mv._scalarsArray[9] = mv1._scalarsArray[9] + tempVar0;

            tempVar0 = -1 * mv2._scalarsArray[10];
            mv._scalarsArray[10] = mv1._scalarsArray[10] + tempVar0;

            tempVar0 = -1 * mv2._scalarsArray[11];
            mv._scalarsArray[11] = mv1._scalarsArray[11] + tempVar0;

            tempVar0 = -1 * mv2._scalarsArray[12];
            mv._scalarsArray[12] = mv1._scalarsArray[12] + tempVar0;

            tempVar0 = -1 * mv2._scalarsArray[13];
            mv._scalarsArray[13] = mv1._scalarsArray[13] + tempVar0;

            tempVar0 = -1 * mv2._scalarsArray[14];
            mv._scalarsArray[14] = mv1._scalarsArray[14] + tempVar0;

            tempVar0 = -1 * mv2._scalarsArray[15];
            mv._scalarsArray[15] = mv1._scalarsArray[15] + tempVar0;

            //Finish GaClc SymbolicContext Code Generation, 2021-06-26T22:48:42.9153454+02:00


            return mv;
        }


        private readonly double[] _scalarsArray
            = new double[16];


        public int Count => 16;

        public double this[int index]
        {
            get => _scalarsArray[index];
            set => _scalarsArray[index] = value;
        }


        public Multivector()
        {

        }


        public Multivector Gp(Multivector mv2)
        {
            var mv = new Multivector();

            //Begin GaClc SymbolicContext Code Generation, 2021-06-26T22:48:47.8080623+02:00
            //SymbolicContext:
            //Input Variables: 32 used, 0 not used, 32 total.
            //Temp Variables: 494 sub-expressions, 0 generated temps, 494 total.
            //Target Temp Variables: 16 total.
            //Output Variables: 16 total.
            //Computations: 1 average, 510 total.
            //Memory Reads: 1.9725490196078432 average, 1006 total.
            //Memory Writes: 510 total.
            //
            //SymbolicContext Binding Data:
            //   -1 = constant: '-1'
            //   mv1s0 = parameter: _scalarsArray[0]
            //   mv1s1 = parameter: _scalarsArray[1]
            //   mv1s2 = parameter: _scalarsArray[2]
            //   mv1s3 = parameter: _scalarsArray[3]
            //   mv1s4 = parameter: _scalarsArray[4]
            //   mv1s5 = parameter: _scalarsArray[5]
            //   mv1s6 = parameter: _scalarsArray[6]
            //   mv1s7 = parameter: _scalarsArray[7]
            //   mv1s8 = parameter: _scalarsArray[8]
            //   mv1s9 = parameter: _scalarsArray[9]
            //   mv1s10 = parameter: _scalarsArray[10]
            //   mv1s11 = parameter: _scalarsArray[11]
            //   mv1s12 = parameter: _scalarsArray[12]
            //   mv1s13 = parameter: _scalarsArray[13]
            //   mv1s14 = parameter: _scalarsArray[14]
            //   mv1s15 = parameter: _scalarsArray[15]
            //   mv2s0 = parameter: mv2._scalarsArray[0]
            //   mv2s1 = parameter: mv2._scalarsArray[1]
            //   mv2s2 = parameter: mv2._scalarsArray[2]
            //   mv2s3 = parameter: mv2._scalarsArray[3]
            //   mv2s4 = parameter: mv2._scalarsArray[4]
            //   mv2s5 = parameter: mv2._scalarsArray[5]
            //   mv2s6 = parameter: mv2._scalarsArray[6]
            //   mv2s7 = parameter: mv2._scalarsArray[7]
            //   mv2s8 = parameter: mv2._scalarsArray[8]
            //   mv2s9 = parameter: mv2._scalarsArray[9]
            //   mv2s10 = parameter: mv2._scalarsArray[10]
            //   mv2s11 = parameter: mv2._scalarsArray[11]
            //   mv2s12 = parameter: mv2._scalarsArray[12]
            //   mv2s13 = parameter: mv2._scalarsArray[13]
            //   mv2s14 = parameter: mv2._scalarsArray[14]
            //   mv2s15 = parameter: mv2._scalarsArray[15]

            var tempVar0 = _scalarsArray[15] * mv2._scalarsArray[0];
            var tempVar1 = -1 * _scalarsArray[14];
            var tempVar2 = mv2._scalarsArray[1] * tempVar1;
            tempVar0 += tempVar2;
            tempVar2 = -1 * _scalarsArray[5];
            var tempVar3 = mv2._scalarsArray[10] * tempVar2;
            tempVar0 += tempVar3;
            tempVar3 = _scalarsArray[4] * mv2._scalarsArray[11];
            tempVar0 += tempVar3;
            tempVar3 = _scalarsArray[3] * mv2._scalarsArray[12];
            tempVar0 += tempVar3;
            tempVar3 = -1 * _scalarsArray[2];
            var tempVar4 = mv2._scalarsArray[13] * tempVar3;
            tempVar0 += tempVar4;
            tempVar4 = _scalarsArray[1] * mv2._scalarsArray[14];
            tempVar0 += tempVar4;
            tempVar4 = _scalarsArray[0] * mv2._scalarsArray[15];
            tempVar0 += tempVar4;
            tempVar4 = _scalarsArray[13] * mv2._scalarsArray[2];
            tempVar0 += tempVar4;
            tempVar4 = _scalarsArray[12] * mv2._scalarsArray[3];
            tempVar0 += tempVar4;
            tempVar4 = -1 * _scalarsArray[11];
            var tempVar5 = mv2._scalarsArray[4] * tempVar4;
            tempVar0 += tempVar5;
            tempVar5 = -1 * _scalarsArray[10];
            var tempVar6 = mv2._scalarsArray[5] * tempVar5;
            tempVar0 += tempVar6;
            tempVar6 = _scalarsArray[9] * mv2._scalarsArray[6];
            tempVar0 += tempVar6;
            tempVar6 = -1 * _scalarsArray[8];
            var tempVar7 = mv2._scalarsArray[7] * tempVar6;
            tempVar0 += tempVar7;
            tempVar7 = _scalarsArray[7] * mv2._scalarsArray[8];
            tempVar0 += tempVar7;
            tempVar7 = _scalarsArray[6] * mv2._scalarsArray[9];
            mv._scalarsArray[15] = tempVar0 + tempVar7;

            tempVar0 = _scalarsArray[14] * mv2._scalarsArray[0];
            tempVar7 = -1 * _scalarsArray[15];
            var tempVar8 = mv2._scalarsArray[1] * tempVar7;
            tempVar0 += tempVar8;
            tempVar8 = -1 * _scalarsArray[4];
            var tempVar9 = mv2._scalarsArray[10] * tempVar8;
            tempVar0 += tempVar9;
            tempVar9 = _scalarsArray[5] * mv2._scalarsArray[11];
            tempVar0 += tempVar9;
            tempVar9 = _scalarsArray[2] * mv2._scalarsArray[12];
            tempVar0 += tempVar9;
            tempVar9 = -1 * _scalarsArray[3];
            var tempVar10 = mv2._scalarsArray[13] * tempVar9;
            tempVar0 += tempVar10;
            tempVar10 = _scalarsArray[0] * mv2._scalarsArray[14];
            tempVar0 += tempVar10;
            tempVar10 = _scalarsArray[1] * mv2._scalarsArray[15];
            tempVar0 += tempVar10;
            tempVar10 = _scalarsArray[12] * mv2._scalarsArray[2];
            tempVar0 += tempVar10;
            tempVar10 = _scalarsArray[13] * mv2._scalarsArray[3];
            tempVar0 += tempVar10;
            tempVar10 = mv2._scalarsArray[4] * tempVar5;
            tempVar0 += tempVar10;
            tempVar10 = mv2._scalarsArray[5] * tempVar4;
            tempVar0 += tempVar10;
            tempVar10 = _scalarsArray[8] * mv2._scalarsArray[6];
            tempVar0 += tempVar10;
            tempVar10 = -1 * _scalarsArray[9];
            var tempVar11 = mv2._scalarsArray[7] * tempVar10;
            tempVar0 += tempVar11;
            tempVar11 = _scalarsArray[6] * mv2._scalarsArray[8];
            tempVar0 += tempVar11;
            tempVar11 = _scalarsArray[7] * mv2._scalarsArray[9];
            mv._scalarsArray[14] = tempVar0 + tempVar11;

            tempVar0 = _scalarsArray[13] * mv2._scalarsArray[0];
            tempVar11 = _scalarsArray[12] * mv2._scalarsArray[1];
            tempVar0 += tempVar11;
            tempVar11 = -1 * _scalarsArray[7];
            var tempVar12 = mv2._scalarsArray[10] * tempVar11;
            tempVar0 += tempVar12;
            tempVar12 = -1 * _scalarsArray[6];
            var tempVar13 = mv2._scalarsArray[11] * tempVar12;
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[1] * mv2._scalarsArray[12];
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[0] * mv2._scalarsArray[13];
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[3] * mv2._scalarsArray[14];
            tempVar0 += tempVar13;
            tempVar13 = mv2._scalarsArray[15] * tempVar3;
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[15] * mv2._scalarsArray[2];
            tempVar0 += tempVar13;
            tempVar13 = mv2._scalarsArray[3] * tempVar1;
            tempVar0 += tempVar13;
            tempVar13 = mv2._scalarsArray[4] * tempVar10;
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[8] * mv2._scalarsArray[5];
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[11] * mv2._scalarsArray[6];
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[10] * mv2._scalarsArray[7];
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[5] * mv2._scalarsArray[8];
            tempVar0 += tempVar13;
            tempVar13 = mv2._scalarsArray[9] * tempVar8;
            mv._scalarsArray[13] = tempVar0 + tempVar13;

            tempVar0 = _scalarsArray[12] * mv2._scalarsArray[0];
            tempVar13 = _scalarsArray[13] * mv2._scalarsArray[1];
            tempVar0 += tempVar13;
            tempVar13 = mv2._scalarsArray[10] * tempVar12;
            tempVar0 += tempVar13;
            tempVar13 = mv2._scalarsArray[11] * tempVar11;
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[0] * mv2._scalarsArray[12];
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[1] * mv2._scalarsArray[13];
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[2] * mv2._scalarsArray[14];
            tempVar0 += tempVar13;
            tempVar13 = mv2._scalarsArray[15] * tempVar9;
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[14] * mv2._scalarsArray[2];
            tempVar0 += tempVar13;
            tempVar13 = mv2._scalarsArray[3] * tempVar7;
            tempVar0 += tempVar13;
            tempVar13 = mv2._scalarsArray[4] * tempVar6;
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[9] * mv2._scalarsArray[5];
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[10] * mv2._scalarsArray[6];
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[11] * mv2._scalarsArray[7];
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[4] * mv2._scalarsArray[8];
            tempVar0 += tempVar13;
            tempVar13 = mv2._scalarsArray[9] * tempVar2;
            mv._scalarsArray[12] = tempVar0 + tempVar13;

            tempVar0 = _scalarsArray[11] * mv2._scalarsArray[0];
            tempVar13 = _scalarsArray[10] * mv2._scalarsArray[1];
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[1] * mv2._scalarsArray[10];
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[0] * mv2._scalarsArray[11];
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[7] * mv2._scalarsArray[12];
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[6] * mv2._scalarsArray[13];
            tempVar0 += tempVar13;
            tempVar13 = mv2._scalarsArray[14] * tempVar2;
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[4] * mv2._scalarsArray[15];
            tempVar0 += tempVar13;
            tempVar13 = mv2._scalarsArray[2] * tempVar10;
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[8] * mv2._scalarsArray[3];
            tempVar0 += tempVar13;
            tempVar13 = mv2._scalarsArray[4] * tempVar7;
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[14] * mv2._scalarsArray[5];
            tempVar0 += tempVar13;
            tempVar13 = -1 * _scalarsArray[13];
            var tempVar14 = mv2._scalarsArray[6] * tempVar13;
            tempVar0 += tempVar14;
            tempVar14 = -1 * _scalarsArray[12];
            var tempVar15 = mv2._scalarsArray[7] * tempVar14;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[3] * mv2._scalarsArray[8];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[9] * tempVar3;
            mv._scalarsArray[11] = tempVar0 + tempVar15;

            tempVar0 = _scalarsArray[10] * mv2._scalarsArray[0];
            tempVar15 = _scalarsArray[11] * mv2._scalarsArray[1];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[0] * mv2._scalarsArray[10];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[1] * mv2._scalarsArray[11];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[6] * mv2._scalarsArray[12];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[7] * mv2._scalarsArray[13];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[14] * tempVar8;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[5] * mv2._scalarsArray[15];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[2] * tempVar6;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[9] * mv2._scalarsArray[3];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[4] * tempVar1;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[15] * mv2._scalarsArray[5];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[6] * tempVar14;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[7] * tempVar13;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[2] * mv2._scalarsArray[8];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[9] * tempVar9;
            mv._scalarsArray[10] = tempVar0 + tempVar15;

            tempVar0 = _scalarsArray[9] * mv2._scalarsArray[0];
            tempVar15 = mv2._scalarsArray[1] * tempVar6;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[3] * mv2._scalarsArray[10];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[11] * tempVar3;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[5] * mv2._scalarsArray[12];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[13] * tempVar8;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[14] * tempVar11;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[15] * tempVar12;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[2] * tempVar4;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[3] * tempVar5;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[4] * tempVar13;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[5] * tempVar14;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[6] * tempVar7;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[14] * mv2._scalarsArray[7];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[1] * mv2._scalarsArray[8];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[0] * mv2._scalarsArray[9];
            mv._scalarsArray[9] = tempVar0 + tempVar15;

            tempVar0 = _scalarsArray[8] * mv2._scalarsArray[0];
            tempVar15 = mv2._scalarsArray[1] * tempVar10;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[2] * mv2._scalarsArray[10];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[11] * tempVar9;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[4] * mv2._scalarsArray[12];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[13] * tempVar2;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[14] * tempVar12;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[15] * tempVar11;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[2] * tempVar5;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[3] * tempVar4;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[4] * tempVar14;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[5] * tempVar13;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[6] * tempVar1;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[15] * mv2._scalarsArray[7];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[0] * mv2._scalarsArray[8];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[1] * mv2._scalarsArray[9];
            mv._scalarsArray[8] = tempVar0 + tempVar15;

            tempVar0 = _scalarsArray[7] * mv2._scalarsArray[0];
            tempVar15 = _scalarsArray[6] * mv2._scalarsArray[1];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[13] * mv2._scalarsArray[10];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[12] * mv2._scalarsArray[11];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[12] * tempVar4;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[13] * tempVar5;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[9] * mv2._scalarsArray[14];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[15] * tempVar6;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[2] * tempVar2;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[4] * mv2._scalarsArray[3];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[3] * mv2._scalarsArray[4];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[5] * tempVar3;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[1] * mv2._scalarsArray[6];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[0] * mv2._scalarsArray[7];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[15] * mv2._scalarsArray[8];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[9] * tempVar1;
            mv._scalarsArray[7] = tempVar0 + tempVar15;

            tempVar0 = _scalarsArray[6] * mv2._scalarsArray[0];
            tempVar15 = _scalarsArray[7] * mv2._scalarsArray[1];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[12] * mv2._scalarsArray[10];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[13] * mv2._scalarsArray[11];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[12] * tempVar5;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[13] * tempVar4;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[8] * mv2._scalarsArray[14];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[15] * tempVar10;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[2] * tempVar8;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[5] * mv2._scalarsArray[3];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[2] * mv2._scalarsArray[4];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[5] * tempVar9;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[0] * mv2._scalarsArray[6];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[1] * mv2._scalarsArray[7];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[14] * mv2._scalarsArray[8];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[9] * tempVar7;
            mv._scalarsArray[6] = tempVar0 + tempVar15;

            tempVar0 = _scalarsArray[5] * mv2._scalarsArray[0];
            tempVar15 = mv2._scalarsArray[1] * tempVar8;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[15] * mv2._scalarsArray[10];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[11] * tempVar1;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[12] * tempVar10;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[8] * mv2._scalarsArray[13];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[11] * mv2._scalarsArray[14];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[10] * mv2._scalarsArray[15];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[2] * tempVar11;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[3] * tempVar12;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[1] * mv2._scalarsArray[4];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[0] * mv2._scalarsArray[5];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[3] * mv2._scalarsArray[6];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[7] * tempVar3;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[13] * mv2._scalarsArray[8];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[12] * mv2._scalarsArray[9];
            mv._scalarsArray[5] = tempVar0 + tempVar15;

            tempVar0 = _scalarsArray[4] * mv2._scalarsArray[0];
            tempVar15 = mv2._scalarsArray[1] * tempVar2;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[14] * mv2._scalarsArray[10];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[11] * tempVar7;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[12] * tempVar6;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[9] * mv2._scalarsArray[13];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[10] * mv2._scalarsArray[14];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[11] * mv2._scalarsArray[15];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[2] * tempVar12;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[3] * tempVar11;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[0] * mv2._scalarsArray[4];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[1] * mv2._scalarsArray[5];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[2] * mv2._scalarsArray[6];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[7] * tempVar9;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[12] * mv2._scalarsArray[8];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[13] * mv2._scalarsArray[9];
            mv._scalarsArray[4] = tempVar0 + tempVar15;

            tempVar0 = _scalarsArray[3] * mv2._scalarsArray[0];
            tempVar15 = mv2._scalarsArray[1] * tempVar3;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[10] * tempVar10;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[8] * mv2._scalarsArray[11];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[12] * tempVar7;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[14] * mv2._scalarsArray[13];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[14] * tempVar13;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[15] * tempVar14;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[1] * mv2._scalarsArray[2];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[0] * mv2._scalarsArray[3];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[7] * mv2._scalarsArray[4];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[6] * mv2._scalarsArray[5];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[6] * tempVar2;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[4] * mv2._scalarsArray[7];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[11] * mv2._scalarsArray[8];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[10] * mv2._scalarsArray[9];
            mv._scalarsArray[3] = tempVar0 + tempVar15;

            tempVar0 = _scalarsArray[2] * mv2._scalarsArray[0];
            tempVar15 = mv2._scalarsArray[1] * tempVar9;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[10] * tempVar6;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[9] * mv2._scalarsArray[11];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[12] * tempVar1;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[15] * mv2._scalarsArray[13];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[14] * tempVar14;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[15] * tempVar13;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[0] * mv2._scalarsArray[2];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[1] * mv2._scalarsArray[3];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[6] * mv2._scalarsArray[4];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[7] * mv2._scalarsArray[5];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[6] * tempVar8;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[5] * mv2._scalarsArray[7];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[10] * mv2._scalarsArray[8];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[11] * mv2._scalarsArray[9];
            mv._scalarsArray[2] = tempVar0 + tempVar15;

            tempVar0 = _scalarsArray[1] * mv2._scalarsArray[0];
            tempVar15 = _scalarsArray[0] * mv2._scalarsArray[1];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[10] * tempVar4;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[11] * tempVar5;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[12] * tempVar13;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[13] * tempVar14;
            tempVar0 += tempVar15;
            tempVar7 = mv2._scalarsArray[14] * tempVar7;
            tempVar0 += tempVar7;
            tempVar7 = _scalarsArray[14] * mv2._scalarsArray[15];
            tempVar0 += tempVar7;
            tempVar7 = _scalarsArray[3] * mv2._scalarsArray[2];
            tempVar0 += tempVar7;
            tempVar3 = mv2._scalarsArray[3] * tempVar3;
            tempVar0 += tempVar3;
            tempVar3 = _scalarsArray[5] * mv2._scalarsArray[4];
            tempVar0 += tempVar3;
            tempVar3 = mv2._scalarsArray[5] * tempVar8;
            tempVar0 += tempVar3;
            tempVar3 = mv2._scalarsArray[6] * tempVar11;
            tempVar0 += tempVar3;
            tempVar3 = mv2._scalarsArray[7] * tempVar12;
            tempVar0 += tempVar3;
            tempVar3 = _scalarsArray[9] * mv2._scalarsArray[8];
            tempVar0 += tempVar3;
            tempVar3 = mv2._scalarsArray[9] * tempVar6;
            mv._scalarsArray[1] = tempVar0 + tempVar3;

            tempVar0 = _scalarsArray[0] * mv2._scalarsArray[0];
            tempVar3 = _scalarsArray[1] * mv2._scalarsArray[1];
            tempVar0 += tempVar3;
            tempVar3 = mv2._scalarsArray[10] * tempVar5;
            tempVar0 += tempVar3;
            tempVar3 = mv2._scalarsArray[11] * tempVar4;
            tempVar0 += tempVar3;
            tempVar3 = mv2._scalarsArray[12] * tempVar14;
            tempVar0 += tempVar3;
            tempVar3 = mv2._scalarsArray[13] * tempVar13;
            tempVar0 += tempVar3;
            tempVar1 = mv2._scalarsArray[14] * tempVar1;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[15] * mv2._scalarsArray[15];
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[2] * mv2._scalarsArray[2];
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[3] * tempVar9;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[4] * mv2._scalarsArray[4];
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[5] * tempVar2;
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[6] * tempVar12;
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[7] * tempVar11;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[8] * mv2._scalarsArray[8];
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[9] * tempVar10;
            mv._scalarsArray[0] = tempVar0 + tempVar1;

            //Finish GaClc SymbolicContext Code Generation, 2021-06-26T22:48:47.8095619+02:00


            return mv;
        }

        public Multivector Op(Multivector mv2)
        {
            var mv = new Multivector();

            //Begin GaClc SymbolicContext Code Generation, 2021-06-26T22:48:47.9860874+02:00
            //SymbolicContext:
            //Input Variables: 32 used, 0 not used, 32 total.
            //Temp Variables: 138 sub-expressions, 0 generated temps, 138 total.
            //Target Temp Variables: 7 total.
            //Output Variables: 16 total.
            //Computations: 1 average, 154 total.
            //Memory Reads: 1.948051948051948 average, 300 total.
            //Memory Writes: 154 total.
            //
            //SymbolicContext Binding Data:
            //   -1 = constant: '-1'
            //   mv1s0 = parameter: _scalarsArray[0]
            //   mv1s1 = parameter: _scalarsArray[1]
            //   mv1s2 = parameter: _scalarsArray[2]
            //   mv1s3 = parameter: _scalarsArray[3]
            //   mv1s4 = parameter: _scalarsArray[4]
            //   mv1s5 = parameter: _scalarsArray[5]
            //   mv1s6 = parameter: _scalarsArray[6]
            //   mv1s7 = parameter: _scalarsArray[7]
            //   mv1s8 = parameter: _scalarsArray[8]
            //   mv1s9 = parameter: _scalarsArray[9]
            //   mv1s10 = parameter: _scalarsArray[10]
            //   mv1s11 = parameter: _scalarsArray[11]
            //   mv1s12 = parameter: _scalarsArray[12]
            //   mv1s13 = parameter: _scalarsArray[13]
            //   mv1s14 = parameter: _scalarsArray[14]
            //   mv1s15 = parameter: _scalarsArray[15]
            //   mv2s0 = parameter: mv2._scalarsArray[0]
            //   mv2s1 = parameter: mv2._scalarsArray[1]
            //   mv2s2 = parameter: mv2._scalarsArray[2]
            //   mv2s3 = parameter: mv2._scalarsArray[3]
            //   mv2s4 = parameter: mv2._scalarsArray[4]
            //   mv2s5 = parameter: mv2._scalarsArray[5]
            //   mv2s6 = parameter: mv2._scalarsArray[6]
            //   mv2s7 = parameter: mv2._scalarsArray[7]
            //   mv2s8 = parameter: mv2._scalarsArray[8]
            //   mv2s9 = parameter: mv2._scalarsArray[9]
            //   mv2s10 = parameter: mv2._scalarsArray[10]
            //   mv2s11 = parameter: mv2._scalarsArray[11]
            //   mv2s12 = parameter: mv2._scalarsArray[12]
            //   mv2s13 = parameter: mv2._scalarsArray[13]
            //   mv2s14 = parameter: mv2._scalarsArray[14]
            //   mv2s15 = parameter: mv2._scalarsArray[15]

            var tempVar0 = _scalarsArray[15] * mv2._scalarsArray[0];
            var tempVar1 = -1 * _scalarsArray[14];
            tempVar1 = mv2._scalarsArray[1] * tempVar1;
            tempVar0 += tempVar1;
            tempVar1 = -1 * _scalarsArray[5];
            var tempVar2 = mv2._scalarsArray[10] * tempVar1;
            tempVar0 += tempVar2;
            tempVar2 = _scalarsArray[4] * mv2._scalarsArray[11];
            tempVar0 += tempVar2;
            tempVar2 = _scalarsArray[3] * mv2._scalarsArray[12];
            tempVar0 += tempVar2;
            tempVar2 = -1 * _scalarsArray[2];
            var tempVar3 = mv2._scalarsArray[13] * tempVar2;
            tempVar0 += tempVar3;
            tempVar3 = _scalarsArray[1] * mv2._scalarsArray[14];
            tempVar0 += tempVar3;
            tempVar3 = _scalarsArray[0] * mv2._scalarsArray[15];
            tempVar0 += tempVar3;
            tempVar3 = _scalarsArray[13] * mv2._scalarsArray[2];
            tempVar0 += tempVar3;
            tempVar3 = _scalarsArray[12] * mv2._scalarsArray[3];
            tempVar0 += tempVar3;
            tempVar3 = -1 * _scalarsArray[11];
            tempVar3 = mv2._scalarsArray[4] * tempVar3;
            tempVar0 += tempVar3;
            tempVar3 = -1 * _scalarsArray[10];
            var tempVar4 = mv2._scalarsArray[5] * tempVar3;
            tempVar0 += tempVar4;
            tempVar4 = _scalarsArray[9] * mv2._scalarsArray[6];
            tempVar0 += tempVar4;
            tempVar4 = -1 * _scalarsArray[8];
            var tempVar5 = mv2._scalarsArray[7] * tempVar4;
            tempVar0 += tempVar5;
            tempVar5 = _scalarsArray[7] * mv2._scalarsArray[8];
            tempVar0 += tempVar5;
            tempVar5 = _scalarsArray[6] * mv2._scalarsArray[9];
            mv._scalarsArray[15] = tempVar0 + tempVar5;

            tempVar0 = _scalarsArray[14] * mv2._scalarsArray[0];
            tempVar5 = -1 * _scalarsArray[4];
            var tempVar6 = mv2._scalarsArray[10] * tempVar5;
            tempVar0 += tempVar6;
            tempVar6 = _scalarsArray[2] * mv2._scalarsArray[12];
            tempVar0 += tempVar6;
            tempVar6 = _scalarsArray[0] * mv2._scalarsArray[14];
            tempVar0 += tempVar6;
            tempVar6 = _scalarsArray[12] * mv2._scalarsArray[2];
            tempVar0 += tempVar6;
            tempVar3 = mv2._scalarsArray[4] * tempVar3;
            tempVar0 += tempVar3;
            tempVar3 = _scalarsArray[8] * mv2._scalarsArray[6];
            tempVar0 += tempVar3;
            tempVar3 = _scalarsArray[6] * mv2._scalarsArray[8];
            mv._scalarsArray[14] = tempVar0 + tempVar3;

            tempVar0 = _scalarsArray[13] * mv2._scalarsArray[0];
            tempVar3 = _scalarsArray[12] * mv2._scalarsArray[1];
            tempVar0 += tempVar3;
            tempVar3 = _scalarsArray[1] * mv2._scalarsArray[12];
            tempVar0 += tempVar3;
            tempVar3 = _scalarsArray[0] * mv2._scalarsArray[13];
            tempVar0 += tempVar3;
            tempVar3 = -1 * _scalarsArray[9];
            tempVar6 = mv2._scalarsArray[4] * tempVar3;
            tempVar0 += tempVar6;
            tempVar6 = _scalarsArray[8] * mv2._scalarsArray[5];
            tempVar0 += tempVar6;
            tempVar6 = _scalarsArray[5] * mv2._scalarsArray[8];
            tempVar0 += tempVar6;
            tempVar6 = mv2._scalarsArray[9] * tempVar5;
            mv._scalarsArray[13] = tempVar0 + tempVar6;

            tempVar0 = _scalarsArray[12] * mv2._scalarsArray[0];
            tempVar6 = _scalarsArray[0] * mv2._scalarsArray[12];
            tempVar0 += tempVar6;
            tempVar6 = mv2._scalarsArray[4] * tempVar4;
            tempVar0 += tempVar6;
            tempVar6 = _scalarsArray[4] * mv2._scalarsArray[8];
            mv._scalarsArray[12] = tempVar0 + tempVar6;

            tempVar0 = _scalarsArray[11] * mv2._scalarsArray[0];
            tempVar6 = _scalarsArray[10] * mv2._scalarsArray[1];
            tempVar0 += tempVar6;
            tempVar6 = _scalarsArray[1] * mv2._scalarsArray[10];
            tempVar0 += tempVar6;
            tempVar6 = _scalarsArray[0] * mv2._scalarsArray[11];
            tempVar0 += tempVar6;
            tempVar3 = mv2._scalarsArray[2] * tempVar3;
            tempVar0 += tempVar3;
            tempVar3 = _scalarsArray[8] * mv2._scalarsArray[3];
            tempVar0 += tempVar3;
            tempVar3 = _scalarsArray[3] * mv2._scalarsArray[8];
            tempVar0 += tempVar3;
            tempVar3 = mv2._scalarsArray[9] * tempVar2;
            mv._scalarsArray[11] = tempVar0 + tempVar3;

            tempVar0 = _scalarsArray[10] * mv2._scalarsArray[0];
            tempVar3 = _scalarsArray[0] * mv2._scalarsArray[10];
            tempVar0 += tempVar3;
            tempVar3 = mv2._scalarsArray[2] * tempVar4;
            tempVar0 += tempVar3;
            tempVar3 = _scalarsArray[2] * mv2._scalarsArray[8];
            mv._scalarsArray[10] = tempVar0 + tempVar3;

            tempVar0 = _scalarsArray[9] * mv2._scalarsArray[0];
            tempVar3 = mv2._scalarsArray[1] * tempVar4;
            tempVar0 += tempVar3;
            tempVar3 = _scalarsArray[1] * mv2._scalarsArray[8];
            tempVar0 += tempVar3;
            tempVar3 = _scalarsArray[0] * mv2._scalarsArray[9];
            mv._scalarsArray[9] = tempVar0 + tempVar3;

            tempVar0 = _scalarsArray[8] * mv2._scalarsArray[0];
            tempVar3 = _scalarsArray[0] * mv2._scalarsArray[8];
            mv._scalarsArray[8] = tempVar0 + tempVar3;

            tempVar0 = _scalarsArray[7] * mv2._scalarsArray[0];
            tempVar3 = _scalarsArray[6] * mv2._scalarsArray[1];
            tempVar0 += tempVar3;
            tempVar1 = mv2._scalarsArray[2] * tempVar1;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[4] * mv2._scalarsArray[3];
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[3] * mv2._scalarsArray[4];
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[5] * tempVar2;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[1] * mv2._scalarsArray[6];
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[0] * mv2._scalarsArray[7];
            mv._scalarsArray[7] = tempVar0 + tempVar1;

            tempVar0 = _scalarsArray[6] * mv2._scalarsArray[0];
            tempVar1 = mv2._scalarsArray[2] * tempVar5;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[2] * mv2._scalarsArray[4];
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[0] * mv2._scalarsArray[6];
            mv._scalarsArray[6] = tempVar0 + tempVar1;

            tempVar0 = _scalarsArray[5] * mv2._scalarsArray[0];
            tempVar1 = mv2._scalarsArray[1] * tempVar5;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[1] * mv2._scalarsArray[4];
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[0] * mv2._scalarsArray[5];
            mv._scalarsArray[5] = tempVar0 + tempVar1;

            tempVar0 = _scalarsArray[4] * mv2._scalarsArray[0];
            tempVar1 = _scalarsArray[0] * mv2._scalarsArray[4];
            mv._scalarsArray[4] = tempVar0 + tempVar1;

            tempVar0 = _scalarsArray[3] * mv2._scalarsArray[0];
            tempVar1 = mv2._scalarsArray[1] * tempVar2;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[1] * mv2._scalarsArray[2];
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[0] * mv2._scalarsArray[3];
            mv._scalarsArray[3] = tempVar0 + tempVar1;

            tempVar0 = _scalarsArray[2] * mv2._scalarsArray[0];
            tempVar1 = _scalarsArray[0] * mv2._scalarsArray[2];
            mv._scalarsArray[2] = tempVar0 + tempVar1;

            tempVar0 = _scalarsArray[1] * mv2._scalarsArray[0];
            tempVar1 = _scalarsArray[0] * mv2._scalarsArray[1];
            mv._scalarsArray[1] = tempVar0 + tempVar1;

            mv._scalarsArray[0] = _scalarsArray[0] * mv2._scalarsArray[0];

            //Finish GaClc SymbolicContext Code Generation, 2021-06-26T22:48:47.9864726+02:00


            return mv;
        }

        public Multivector Lcp(Multivector mv2)
        {
            var mv = new Multivector();

            //Begin GaClc SymbolicContext Code Generation, 2021-06-26T22:48:48.1751723+02:00
            //SymbolicContext:
            //Input Variables: 32 used, 0 not used, 32 total.
            //Temp Variables: 143 sub-expressions, 0 generated temps, 143 total.
            //Target Temp Variables: 13 total.
            //Output Variables: 16 total.
            //Computations: 1 average, 159 total.
            //Memory Reads: 1.9182389937106918 average, 305 total.
            //Memory Writes: 159 total.
            //
            //SymbolicContext Binding Data:
            //   -1 = constant: '-1'
            //   mv1s0 = parameter: _scalarsArray[0]
            //   mv1s1 = parameter: _scalarsArray[1]
            //   mv1s2 = parameter: _scalarsArray[2]
            //   mv1s3 = parameter: _scalarsArray[3]
            //   mv1s4 = parameter: _scalarsArray[4]
            //   mv1s5 = parameter: _scalarsArray[5]
            //   mv1s6 = parameter: _scalarsArray[6]
            //   mv1s7 = parameter: _scalarsArray[7]
            //   mv1s8 = parameter: _scalarsArray[8]
            //   mv1s9 = parameter: _scalarsArray[9]
            //   mv1s10 = parameter: _scalarsArray[10]
            //   mv1s11 = parameter: _scalarsArray[11]
            //   mv1s12 = parameter: _scalarsArray[12]
            //   mv1s13 = parameter: _scalarsArray[13]
            //   mv1s14 = parameter: _scalarsArray[14]
            //   mv1s15 = parameter: _scalarsArray[15]
            //   mv2s0 = parameter: mv2._scalarsArray[0]
            //   mv2s1 = parameter: mv2._scalarsArray[1]
            //   mv2s2 = parameter: mv2._scalarsArray[2]
            //   mv2s3 = parameter: mv2._scalarsArray[3]
            //   mv2s4 = parameter: mv2._scalarsArray[4]
            //   mv2s5 = parameter: mv2._scalarsArray[5]
            //   mv2s6 = parameter: mv2._scalarsArray[6]
            //   mv2s7 = parameter: mv2._scalarsArray[7]
            //   mv2s8 = parameter: mv2._scalarsArray[8]
            //   mv2s9 = parameter: mv2._scalarsArray[9]
            //   mv2s10 = parameter: mv2._scalarsArray[10]
            //   mv2s11 = parameter: mv2._scalarsArray[11]
            //   mv2s12 = parameter: mv2._scalarsArray[12]
            //   mv2s13 = parameter: mv2._scalarsArray[13]
            //   mv2s14 = parameter: mv2._scalarsArray[14]
            //   mv2s15 = parameter: mv2._scalarsArray[15]

            mv._scalarsArray[15] = _scalarsArray[0] * mv2._scalarsArray[15];

            var tempVar0 = _scalarsArray[0] * mv2._scalarsArray[14];
            var tempVar1 = _scalarsArray[1] * mv2._scalarsArray[15];
            mv._scalarsArray[14] = tempVar0 + tempVar1;

            tempVar0 = _scalarsArray[0] * mv2._scalarsArray[13];
            tempVar1 = -1 * _scalarsArray[2];
            var tempVar2 = mv2._scalarsArray[15] * tempVar1;
            mv._scalarsArray[13] = tempVar0 + tempVar2;

            tempVar0 = _scalarsArray[0] * mv2._scalarsArray[12];
            tempVar2 = _scalarsArray[1] * mv2._scalarsArray[13];
            tempVar0 += tempVar2;
            tempVar2 = _scalarsArray[2] * mv2._scalarsArray[14];
            tempVar0 += tempVar2;
            tempVar2 = -1 * _scalarsArray[3];
            var tempVar3 = mv2._scalarsArray[15] * tempVar2;
            mv._scalarsArray[12] = tempVar0 + tempVar3;

            tempVar0 = _scalarsArray[0] * mv2._scalarsArray[11];
            tempVar3 = _scalarsArray[4] * mv2._scalarsArray[15];
            mv._scalarsArray[11] = tempVar0 + tempVar3;

            tempVar0 = _scalarsArray[0] * mv2._scalarsArray[10];
            tempVar3 = _scalarsArray[1] * mv2._scalarsArray[11];
            tempVar0 += tempVar3;
            tempVar3 = -1 * _scalarsArray[4];
            var tempVar4 = mv2._scalarsArray[14] * tempVar3;
            tempVar0 += tempVar4;
            tempVar4 = _scalarsArray[5] * mv2._scalarsArray[15];
            mv._scalarsArray[10] = tempVar0 + tempVar4;

            tempVar0 = mv2._scalarsArray[11] * tempVar1;
            tempVar4 = mv2._scalarsArray[13] * tempVar3;
            tempVar0 += tempVar4;
            tempVar4 = -1 * _scalarsArray[6];
            var tempVar5 = mv2._scalarsArray[15] * tempVar4;
            tempVar0 += tempVar5;
            tempVar5 = _scalarsArray[0] * mv2._scalarsArray[9];
            mv._scalarsArray[9] = tempVar0 + tempVar5;

            tempVar0 = _scalarsArray[2] * mv2._scalarsArray[10];
            tempVar5 = mv2._scalarsArray[11] * tempVar2;
            tempVar0 += tempVar5;
            tempVar5 = _scalarsArray[4] * mv2._scalarsArray[12];
            tempVar0 += tempVar5;
            tempVar5 = -1 * _scalarsArray[5];
            var tempVar6 = mv2._scalarsArray[13] * tempVar5;
            tempVar0 += tempVar6;
            tempVar6 = mv2._scalarsArray[14] * tempVar4;
            tempVar0 += tempVar6;
            tempVar6 = -1 * _scalarsArray[7];
            var tempVar7 = mv2._scalarsArray[15] * tempVar6;
            tempVar0 += tempVar7;
            tempVar7 = _scalarsArray[0] * mv2._scalarsArray[8];
            tempVar0 += tempVar7;
            tempVar7 = _scalarsArray[1] * mv2._scalarsArray[9];
            mv._scalarsArray[8] = tempVar0 + tempVar7;

            tempVar0 = -1 * _scalarsArray[8];
            tempVar7 = mv2._scalarsArray[15] * tempVar0;
            var tempVar8 = _scalarsArray[0] * mv2._scalarsArray[7];
            mv._scalarsArray[7] = tempVar7 + tempVar8;

            tempVar7 = _scalarsArray[8] * mv2._scalarsArray[14];
            tempVar8 = -1 * _scalarsArray[9];
            var tempVar9 = mv2._scalarsArray[15] * tempVar8;
            tempVar7 += tempVar9;
            tempVar9 = _scalarsArray[0] * mv2._scalarsArray[6];
            tempVar7 += tempVar9;
            tempVar9 = _scalarsArray[1] * mv2._scalarsArray[7];
            mv._scalarsArray[6] = tempVar7 + tempVar9;

            tempVar7 = _scalarsArray[8] * mv2._scalarsArray[13];
            tempVar9 = _scalarsArray[10] * mv2._scalarsArray[15];
            tempVar7 += tempVar9;
            tempVar9 = _scalarsArray[0] * mv2._scalarsArray[5];
            tempVar7 += tempVar9;
            tempVar9 = mv2._scalarsArray[7] * tempVar1;
            mv._scalarsArray[5] = tempVar7 + tempVar9;

            tempVar7 = mv2._scalarsArray[12] * tempVar0;
            tempVar9 = _scalarsArray[9] * mv2._scalarsArray[13];
            tempVar7 += tempVar9;
            tempVar9 = _scalarsArray[10] * mv2._scalarsArray[14];
            tempVar7 += tempVar9;
            tempVar9 = _scalarsArray[11] * mv2._scalarsArray[15];
            tempVar7 += tempVar9;
            tempVar9 = _scalarsArray[0] * mv2._scalarsArray[4];
            tempVar7 += tempVar9;
            tempVar9 = _scalarsArray[1] * mv2._scalarsArray[5];
            tempVar7 += tempVar9;
            tempVar9 = _scalarsArray[2] * mv2._scalarsArray[6];
            tempVar7 += tempVar9;
            tempVar9 = mv2._scalarsArray[7] * tempVar2;
            mv._scalarsArray[4] = tempVar7 + tempVar9;

            tempVar7 = _scalarsArray[8] * mv2._scalarsArray[11];
            tempVar9 = -1 * _scalarsArray[12];
            var tempVar10 = mv2._scalarsArray[15] * tempVar9;
            tempVar7 += tempVar10;
            tempVar10 = _scalarsArray[0] * mv2._scalarsArray[3];
            tempVar7 += tempVar10;
            tempVar10 = _scalarsArray[4] * mv2._scalarsArray[7];
            mv._scalarsArray[3] = tempVar7 + tempVar10;

            tempVar7 = mv2._scalarsArray[10] * tempVar0;
            tempVar10 = _scalarsArray[9] * mv2._scalarsArray[11];
            tempVar7 += tempVar10;
            tempVar10 = mv2._scalarsArray[14] * tempVar9;
            tempVar7 += tempVar10;
            tempVar10 = -1 * _scalarsArray[13];
            var tempVar11 = mv2._scalarsArray[15] * tempVar10;
            tempVar7 += tempVar11;
            tempVar11 = _scalarsArray[0] * mv2._scalarsArray[2];
            tempVar7 += tempVar11;
            tempVar11 = _scalarsArray[1] * mv2._scalarsArray[3];
            tempVar7 += tempVar11;
            tempVar11 = mv2._scalarsArray[6] * tempVar3;
            tempVar7 += tempVar11;
            tempVar11 = _scalarsArray[5] * mv2._scalarsArray[7];
            mv._scalarsArray[2] = tempVar7 + tempVar11;

            tempVar7 = _scalarsArray[0] * mv2._scalarsArray[1];
            tempVar11 = -1 * _scalarsArray[10];
            var tempVar12 = mv2._scalarsArray[11] * tempVar11;
            tempVar7 += tempVar12;
            tempVar12 = mv2._scalarsArray[13] * tempVar9;
            tempVar7 += tempVar12;
            tempVar12 = _scalarsArray[14] * mv2._scalarsArray[15];
            tempVar7 += tempVar12;
            tempVar1 = mv2._scalarsArray[3] * tempVar1;
            tempVar1 = tempVar7 + tempVar1;
            tempVar3 = mv2._scalarsArray[5] * tempVar3;
            tempVar1 += tempVar3;
            tempVar3 = mv2._scalarsArray[7] * tempVar4;
            tempVar1 += tempVar3;
            tempVar0 = mv2._scalarsArray[9] * tempVar0;
            mv._scalarsArray[1] = tempVar1 + tempVar0;

            tempVar0 = _scalarsArray[0] * mv2._scalarsArray[0];
            tempVar1 = _scalarsArray[1] * mv2._scalarsArray[1];
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[10] * tempVar11;
            tempVar0 += tempVar1;
            tempVar1 = -1 * _scalarsArray[11];
            tempVar1 = mv2._scalarsArray[11] * tempVar1;
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[12] * tempVar9;
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[13] * tempVar10;
            tempVar0 += tempVar1;
            tempVar1 = -1 * _scalarsArray[14];
            tempVar1 = mv2._scalarsArray[14] * tempVar1;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[15] * mv2._scalarsArray[15];
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[2] * mv2._scalarsArray[2];
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[3] * tempVar2;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[4] * mv2._scalarsArray[4];
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[5] * tempVar5;
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[6] * tempVar4;
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[7] * tempVar6;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[8] * mv2._scalarsArray[8];
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[9] * tempVar8;
            mv._scalarsArray[0] = tempVar0 + tempVar1;

            //Finish GaClc SymbolicContext Code Generation, 2021-06-26T22:48:48.1755620+02:00


            return mv;
        }

        public Multivector Rcp(Multivector mv2)
        {
            var mv = new Multivector();

            //Begin GaClc SymbolicContext Code Generation, 2021-06-26T22:48:48.3324321+02:00
            //SymbolicContext:
            //Input Variables: 32 used, 0 not used, 32 total.
            //Temp Variables: 141 sub-expressions, 0 generated temps, 141 total.
            //Target Temp Variables: 13 total.
            //Output Variables: 16 total.
            //Computations: 1 average, 157 total.
            //Memory Reads: 1.929936305732484 average, 303 total.
            //Memory Writes: 157 total.
            //
            //SymbolicContext Binding Data:
            //   -1 = constant: '-1'
            //   mv1s0 = parameter: _scalarsArray[0]
            //   mv1s1 = parameter: _scalarsArray[1]
            //   mv1s2 = parameter: _scalarsArray[2]
            //   mv1s3 = parameter: _scalarsArray[3]
            //   mv1s4 = parameter: _scalarsArray[4]
            //   mv1s5 = parameter: _scalarsArray[5]
            //   mv1s6 = parameter: _scalarsArray[6]
            //   mv1s7 = parameter: _scalarsArray[7]
            //   mv1s8 = parameter: _scalarsArray[8]
            //   mv1s9 = parameter: _scalarsArray[9]
            //   mv1s10 = parameter: _scalarsArray[10]
            //   mv1s11 = parameter: _scalarsArray[11]
            //   mv1s12 = parameter: _scalarsArray[12]
            //   mv1s13 = parameter: _scalarsArray[13]
            //   mv1s14 = parameter: _scalarsArray[14]
            //   mv1s15 = parameter: _scalarsArray[15]
            //   mv2s0 = parameter: mv2._scalarsArray[0]
            //   mv2s1 = parameter: mv2._scalarsArray[1]
            //   mv2s2 = parameter: mv2._scalarsArray[2]
            //   mv2s3 = parameter: mv2._scalarsArray[3]
            //   mv2s4 = parameter: mv2._scalarsArray[4]
            //   mv2s5 = parameter: mv2._scalarsArray[5]
            //   mv2s6 = parameter: mv2._scalarsArray[6]
            //   mv2s7 = parameter: mv2._scalarsArray[7]
            //   mv2s8 = parameter: mv2._scalarsArray[8]
            //   mv2s9 = parameter: mv2._scalarsArray[9]
            //   mv2s10 = parameter: mv2._scalarsArray[10]
            //   mv2s11 = parameter: mv2._scalarsArray[11]
            //   mv2s12 = parameter: mv2._scalarsArray[12]
            //   mv2s13 = parameter: mv2._scalarsArray[13]
            //   mv2s14 = parameter: mv2._scalarsArray[14]
            //   mv2s15 = parameter: mv2._scalarsArray[15]

            mv._scalarsArray[15] = _scalarsArray[15] * mv2._scalarsArray[0];

            var tempVar0 = _scalarsArray[14] * mv2._scalarsArray[0];
            var tempVar1 = -1 * _scalarsArray[15];
            var tempVar2 = mv2._scalarsArray[1] * tempVar1;
            mv._scalarsArray[14] = tempVar0 + tempVar2;

            tempVar0 = _scalarsArray[13] * mv2._scalarsArray[0];
            tempVar2 = _scalarsArray[15] * mv2._scalarsArray[2];
            mv._scalarsArray[13] = tempVar0 + tempVar2;

            tempVar0 = _scalarsArray[12] * mv2._scalarsArray[0];
            tempVar2 = _scalarsArray[13] * mv2._scalarsArray[1];
            tempVar0 += tempVar2;
            tempVar2 = _scalarsArray[14] * mv2._scalarsArray[2];
            tempVar0 += tempVar2;
            tempVar2 = mv2._scalarsArray[3] * tempVar1;
            mv._scalarsArray[12] = tempVar0 + tempVar2;

            tempVar0 = _scalarsArray[11] * mv2._scalarsArray[0];
            tempVar2 = mv2._scalarsArray[4] * tempVar1;
            mv._scalarsArray[11] = tempVar0 + tempVar2;

            tempVar0 = _scalarsArray[10] * mv2._scalarsArray[0];
            tempVar2 = _scalarsArray[11] * mv2._scalarsArray[1];
            tempVar0 += tempVar2;
            tempVar2 = -1 * _scalarsArray[14];
            var tempVar3 = mv2._scalarsArray[4] * tempVar2;
            tempVar0 += tempVar3;
            tempVar3 = _scalarsArray[15] * mv2._scalarsArray[5];
            mv._scalarsArray[10] = tempVar0 + tempVar3;

            tempVar0 = _scalarsArray[9] * mv2._scalarsArray[0];
            tempVar3 = -1 * _scalarsArray[11];
            var tempVar4 = mv2._scalarsArray[2] * tempVar3;
            tempVar0 += tempVar4;
            tempVar4 = -1 * _scalarsArray[13];
            var tempVar5 = mv2._scalarsArray[4] * tempVar4;
            tempVar0 += tempVar5;
            tempVar5 = mv2._scalarsArray[6] * tempVar1;
            mv._scalarsArray[9] = tempVar0 + tempVar5;

            tempVar0 = _scalarsArray[8] * mv2._scalarsArray[0];
            tempVar5 = -1 * _scalarsArray[9];
            var tempVar6 = mv2._scalarsArray[1] * tempVar5;
            tempVar0 += tempVar6;
            tempVar6 = -1 * _scalarsArray[10];
            var tempVar7 = mv2._scalarsArray[2] * tempVar6;
            tempVar0 += tempVar7;
            tempVar7 = mv2._scalarsArray[3] * tempVar3;
            tempVar0 += tempVar7;
            tempVar7 = -1 * _scalarsArray[12];
            var tempVar8 = mv2._scalarsArray[4] * tempVar7;
            tempVar0 += tempVar8;
            tempVar8 = mv2._scalarsArray[5] * tempVar4;
            tempVar0 += tempVar8;
            tempVar8 = mv2._scalarsArray[6] * tempVar2;
            tempVar0 += tempVar8;
            tempVar8 = _scalarsArray[15] * mv2._scalarsArray[7];
            mv._scalarsArray[8] = tempVar0 + tempVar8;

            tempVar0 = _scalarsArray[7] * mv2._scalarsArray[0];
            tempVar8 = _scalarsArray[15] * mv2._scalarsArray[8];
            mv._scalarsArray[7] = tempVar0 + tempVar8;

            tempVar0 = _scalarsArray[6] * mv2._scalarsArray[0];
            tempVar8 = _scalarsArray[7] * mv2._scalarsArray[1];
            tempVar0 += tempVar8;
            tempVar8 = _scalarsArray[14] * mv2._scalarsArray[8];
            tempVar0 += tempVar8;
            tempVar8 = mv2._scalarsArray[9] * tempVar1;
            mv._scalarsArray[6] = tempVar0 + tempVar8;

            tempVar0 = _scalarsArray[5] * mv2._scalarsArray[0];
            tempVar8 = _scalarsArray[15] * mv2._scalarsArray[10];
            tempVar0 += tempVar8;
            tempVar8 = -1 * _scalarsArray[7];
            var tempVar9 = mv2._scalarsArray[2] * tempVar8;
            tempVar0 += tempVar9;
            tempVar9 = _scalarsArray[13] * mv2._scalarsArray[8];
            mv._scalarsArray[5] = tempVar0 + tempVar9;

            tempVar0 = _scalarsArray[4] * mv2._scalarsArray[0];
            tempVar9 = -1 * _scalarsArray[5];
            var tempVar10 = mv2._scalarsArray[1] * tempVar9;
            tempVar0 += tempVar10;
            tempVar10 = _scalarsArray[14] * mv2._scalarsArray[10];
            tempVar0 += tempVar10;
            tempVar10 = mv2._scalarsArray[11] * tempVar1;
            tempVar0 += tempVar10;
            tempVar10 = -1 * _scalarsArray[6];
            var tempVar11 = mv2._scalarsArray[2] * tempVar10;
            tempVar0 += tempVar11;
            tempVar11 = mv2._scalarsArray[3] * tempVar8;
            tempVar0 += tempVar11;
            tempVar11 = _scalarsArray[12] * mv2._scalarsArray[8];
            tempVar0 += tempVar11;
            tempVar11 = _scalarsArray[13] * mv2._scalarsArray[9];
            mv._scalarsArray[4] = tempVar0 + tempVar11;

            tempVar0 = _scalarsArray[3] * mv2._scalarsArray[0];
            tempVar11 = mv2._scalarsArray[12] * tempVar1;
            tempVar0 += tempVar11;
            tempVar11 = _scalarsArray[7] * mv2._scalarsArray[4];
            tempVar0 += tempVar11;
            tempVar11 = _scalarsArray[11] * mv2._scalarsArray[8];
            mv._scalarsArray[3] = tempVar0 + tempVar11;

            tempVar0 = _scalarsArray[2] * mv2._scalarsArray[0];
            tempVar11 = -1 * _scalarsArray[3];
            var tempVar12 = mv2._scalarsArray[1] * tempVar11;
            tempVar0 += tempVar12;
            tempVar12 = mv2._scalarsArray[12] * tempVar2;
            tempVar0 += tempVar12;
            tempVar12 = _scalarsArray[15] * mv2._scalarsArray[13];
            tempVar0 += tempVar12;
            tempVar12 = _scalarsArray[6] * mv2._scalarsArray[4];
            tempVar0 += tempVar12;
            tempVar12 = _scalarsArray[7] * mv2._scalarsArray[5];
            tempVar0 += tempVar12;
            tempVar12 = _scalarsArray[10] * mv2._scalarsArray[8];
            tempVar0 += tempVar12;
            tempVar12 = _scalarsArray[11] * mv2._scalarsArray[9];
            mv._scalarsArray[2] = tempVar0 + tempVar12;

            tempVar0 = _scalarsArray[1] * mv2._scalarsArray[0];
            tempVar12 = mv2._scalarsArray[10] * tempVar3;
            tempVar0 += tempVar12;
            tempVar12 = mv2._scalarsArray[12] * tempVar4;
            tempVar0 += tempVar12;
            tempVar1 = mv2._scalarsArray[14] * tempVar1;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[3] * mv2._scalarsArray[2];
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[5] * mv2._scalarsArray[4];
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[6] * tempVar8;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[9] * mv2._scalarsArray[8];
            mv._scalarsArray[1] = tempVar0 + tempVar1;

            tempVar0 = _scalarsArray[0] * mv2._scalarsArray[0];
            tempVar1 = _scalarsArray[1] * mv2._scalarsArray[1];
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[10] * tempVar6;
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[11] * tempVar3;
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[12] * tempVar7;
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[13] * tempVar4;
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[14] * tempVar2;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[15] * mv2._scalarsArray[15];
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[2] * mv2._scalarsArray[2];
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[3] * tempVar11;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[4] * mv2._scalarsArray[4];
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[5] * tempVar9;
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[6] * tempVar10;
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[7] * tempVar8;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[8] * mv2._scalarsArray[8];
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[9] * tempVar5;
            mv._scalarsArray[0] = tempVar0 + tempVar1;

            //Finish GaClc SymbolicContext Code Generation, 2021-06-26T22:48:48.3327715+02:00


            return mv;
        }

        public Multivector Fdp(Multivector mv2)
        {
            var mv = new Multivector();

            //Begin GaClc SymbolicContext Code Generation, 2021-06-26T22:48:48.7548659+02:00
            //SymbolicContext:
            //Input Variables: 32 used, 0 not used, 32 total.
            //Temp Variables: 274 sub-expressions, 0 generated temps, 274 total.
            //Target Temp Variables: 16 total.
            //Output Variables: 16 total.
            //Computations: 1 average, 290 total.
            //Memory Reads: 1.9517241379310344 average, 566 total.
            //Memory Writes: 290 total.
            //
            //SymbolicContext Binding Data:
            //   -1 = constant: '-1'
            //   mv1s0 = parameter: _scalarsArray[0]
            //   mv1s1 = parameter: _scalarsArray[1]
            //   mv1s2 = parameter: _scalarsArray[2]
            //   mv1s3 = parameter: _scalarsArray[3]
            //   mv1s4 = parameter: _scalarsArray[4]
            //   mv1s5 = parameter: _scalarsArray[5]
            //   mv1s6 = parameter: _scalarsArray[6]
            //   mv1s7 = parameter: _scalarsArray[7]
            //   mv1s8 = parameter: _scalarsArray[8]
            //   mv1s9 = parameter: _scalarsArray[9]
            //   mv1s10 = parameter: _scalarsArray[10]
            //   mv1s11 = parameter: _scalarsArray[11]
            //   mv1s12 = parameter: _scalarsArray[12]
            //   mv1s13 = parameter: _scalarsArray[13]
            //   mv1s14 = parameter: _scalarsArray[14]
            //   mv1s15 = parameter: _scalarsArray[15]
            //   mv2s0 = parameter: mv2._scalarsArray[0]
            //   mv2s1 = parameter: mv2._scalarsArray[1]
            //   mv2s2 = parameter: mv2._scalarsArray[2]
            //   mv2s3 = parameter: mv2._scalarsArray[3]
            //   mv2s4 = parameter: mv2._scalarsArray[4]
            //   mv2s5 = parameter: mv2._scalarsArray[5]
            //   mv2s6 = parameter: mv2._scalarsArray[6]
            //   mv2s7 = parameter: mv2._scalarsArray[7]
            //   mv2s8 = parameter: mv2._scalarsArray[8]
            //   mv2s9 = parameter: mv2._scalarsArray[9]
            //   mv2s10 = parameter: mv2._scalarsArray[10]
            //   mv2s11 = parameter: mv2._scalarsArray[11]
            //   mv2s12 = parameter: mv2._scalarsArray[12]
            //   mv2s13 = parameter: mv2._scalarsArray[13]
            //   mv2s14 = parameter: mv2._scalarsArray[14]
            //   mv2s15 = parameter: mv2._scalarsArray[15]

            var tempVar0 = _scalarsArray[15] * mv2._scalarsArray[0];
            var tempVar1 = _scalarsArray[0] * mv2._scalarsArray[15];
            mv._scalarsArray[15] = tempVar0 + tempVar1;

            tempVar0 = _scalarsArray[14] * mv2._scalarsArray[0];
            tempVar1 = -1 * _scalarsArray[15];
            var tempVar2 = mv2._scalarsArray[1] * tempVar1;
            tempVar0 += tempVar2;
            tempVar2 = _scalarsArray[0] * mv2._scalarsArray[14];
            tempVar0 += tempVar2;
            tempVar2 = _scalarsArray[1] * mv2._scalarsArray[15];
            mv._scalarsArray[14] = tempVar0 + tempVar2;

            tempVar0 = _scalarsArray[13] * mv2._scalarsArray[0];
            tempVar2 = _scalarsArray[0] * mv2._scalarsArray[13];
            tempVar0 += tempVar2;
            tempVar2 = -1 * _scalarsArray[2];
            var tempVar3 = mv2._scalarsArray[15] * tempVar2;
            tempVar0 += tempVar3;
            tempVar3 = _scalarsArray[15] * mv2._scalarsArray[2];
            mv._scalarsArray[13] = tempVar0 + tempVar3;

            tempVar0 = _scalarsArray[12] * mv2._scalarsArray[0];
            tempVar3 = _scalarsArray[13] * mv2._scalarsArray[1];
            tempVar0 += tempVar3;
            tempVar3 = _scalarsArray[0] * mv2._scalarsArray[12];
            tempVar0 += tempVar3;
            tempVar3 = _scalarsArray[1] * mv2._scalarsArray[13];
            tempVar0 += tempVar3;
            tempVar3 = _scalarsArray[2] * mv2._scalarsArray[14];
            tempVar0 += tempVar3;
            tempVar3 = -1 * _scalarsArray[3];
            var tempVar4 = mv2._scalarsArray[15] * tempVar3;
            tempVar0 += tempVar4;
            tempVar4 = _scalarsArray[14] * mv2._scalarsArray[2];
            tempVar0 += tempVar4;
            tempVar4 = mv2._scalarsArray[3] * tempVar1;
            mv._scalarsArray[12] = tempVar0 + tempVar4;

            tempVar0 = _scalarsArray[11] * mv2._scalarsArray[0];
            tempVar4 = _scalarsArray[0] * mv2._scalarsArray[11];
            tempVar0 += tempVar4;
            tempVar4 = _scalarsArray[4] * mv2._scalarsArray[15];
            tempVar0 += tempVar4;
            tempVar4 = mv2._scalarsArray[4] * tempVar1;
            mv._scalarsArray[11] = tempVar0 + tempVar4;

            tempVar0 = _scalarsArray[10] * mv2._scalarsArray[0];
            tempVar4 = _scalarsArray[11] * mv2._scalarsArray[1];
            tempVar0 += tempVar4;
            tempVar4 = _scalarsArray[0] * mv2._scalarsArray[10];
            tempVar0 += tempVar4;
            tempVar4 = _scalarsArray[1] * mv2._scalarsArray[11];
            tempVar0 += tempVar4;
            tempVar4 = -1 * _scalarsArray[4];
            var tempVar5 = mv2._scalarsArray[14] * tempVar4;
            tempVar0 += tempVar5;
            tempVar5 = _scalarsArray[5] * mv2._scalarsArray[15];
            tempVar0 += tempVar5;
            tempVar5 = -1 * _scalarsArray[14];
            var tempVar6 = mv2._scalarsArray[4] * tempVar5;
            tempVar0 += tempVar6;
            tempVar6 = _scalarsArray[15] * mv2._scalarsArray[5];
            mv._scalarsArray[10] = tempVar0 + tempVar6;

            tempVar0 = _scalarsArray[9] * mv2._scalarsArray[0];
            tempVar6 = mv2._scalarsArray[11] * tempVar2;
            tempVar0 += tempVar6;
            tempVar6 = mv2._scalarsArray[13] * tempVar4;
            tempVar0 += tempVar6;
            tempVar6 = -1 * _scalarsArray[6];
            var tempVar7 = mv2._scalarsArray[15] * tempVar6;
            tempVar0 += tempVar7;
            tempVar7 = -1 * _scalarsArray[11];
            var tempVar8 = mv2._scalarsArray[2] * tempVar7;
            tempVar0 += tempVar8;
            tempVar8 = -1 * _scalarsArray[13];
            var tempVar9 = mv2._scalarsArray[4] * tempVar8;
            tempVar0 += tempVar9;
            tempVar9 = mv2._scalarsArray[6] * tempVar1;
            tempVar0 += tempVar9;
            tempVar9 = _scalarsArray[0] * mv2._scalarsArray[9];
            mv._scalarsArray[9] = tempVar0 + tempVar9;

            tempVar0 = _scalarsArray[8] * mv2._scalarsArray[0];
            tempVar9 = -1 * _scalarsArray[9];
            var tempVar10 = mv2._scalarsArray[1] * tempVar9;
            tempVar0 += tempVar10;
            tempVar10 = _scalarsArray[2] * mv2._scalarsArray[10];
            tempVar0 += tempVar10;
            tempVar10 = mv2._scalarsArray[11] * tempVar3;
            tempVar0 += tempVar10;
            tempVar10 = _scalarsArray[4] * mv2._scalarsArray[12];
            tempVar0 += tempVar10;
            tempVar10 = -1 * _scalarsArray[5];
            var tempVar11 = mv2._scalarsArray[13] * tempVar10;
            tempVar0 += tempVar11;
            tempVar11 = mv2._scalarsArray[14] * tempVar6;
            tempVar0 += tempVar11;
            tempVar11 = -1 * _scalarsArray[7];
            var tempVar12 = mv2._scalarsArray[15] * tempVar11;
            tempVar0 += tempVar12;
            tempVar12 = -1 * _scalarsArray[10];
            var tempVar13 = mv2._scalarsArray[2] * tempVar12;
            tempVar0 += tempVar13;
            tempVar13 = mv2._scalarsArray[3] * tempVar7;
            tempVar0 += tempVar13;
            tempVar13 = -1 * _scalarsArray[12];
            var tempVar14 = mv2._scalarsArray[4] * tempVar13;
            tempVar0 += tempVar14;
            tempVar14 = mv2._scalarsArray[5] * tempVar8;
            tempVar0 += tempVar14;
            tempVar14 = mv2._scalarsArray[6] * tempVar5;
            tempVar0 += tempVar14;
            tempVar14 = _scalarsArray[15] * mv2._scalarsArray[7];
            tempVar0 += tempVar14;
            tempVar14 = _scalarsArray[0] * mv2._scalarsArray[8];
            tempVar0 += tempVar14;
            tempVar14 = _scalarsArray[1] * mv2._scalarsArray[9];
            mv._scalarsArray[8] = tempVar0 + tempVar14;

            tempVar0 = _scalarsArray[7] * mv2._scalarsArray[0];
            tempVar14 = -1 * _scalarsArray[8];
            var tempVar15 = mv2._scalarsArray[15] * tempVar14;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[0] * mv2._scalarsArray[7];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[15] * mv2._scalarsArray[8];
            mv._scalarsArray[7] = tempVar0 + tempVar15;

            tempVar0 = _scalarsArray[6] * mv2._scalarsArray[0];
            tempVar15 = _scalarsArray[7] * mv2._scalarsArray[1];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[8] * mv2._scalarsArray[14];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[15] * tempVar9;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[0] * mv2._scalarsArray[6];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[1] * mv2._scalarsArray[7];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[14] * mv2._scalarsArray[8];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[9] * tempVar1;
            mv._scalarsArray[6] = tempVar0 + tempVar15;

            tempVar0 = _scalarsArray[5] * mv2._scalarsArray[0];
            tempVar15 = _scalarsArray[15] * mv2._scalarsArray[10];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[8] * mv2._scalarsArray[13];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[10] * mv2._scalarsArray[15];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[2] * tempVar11;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[0] * mv2._scalarsArray[5];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[7] * tempVar2;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[13] * mv2._scalarsArray[8];
            mv._scalarsArray[5] = tempVar0 + tempVar15;

            tempVar0 = _scalarsArray[4] * mv2._scalarsArray[0];
            tempVar15 = mv2._scalarsArray[1] * tempVar10;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[14] * mv2._scalarsArray[10];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[11] * tempVar1;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[12] * tempVar14;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[9] * mv2._scalarsArray[13];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[10] * mv2._scalarsArray[14];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[11] * mv2._scalarsArray[15];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[2] * tempVar6;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[3] * tempVar11;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[0] * mv2._scalarsArray[4];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[1] * mv2._scalarsArray[5];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[2] * mv2._scalarsArray[6];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[7] * tempVar3;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[12] * mv2._scalarsArray[8];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[13] * mv2._scalarsArray[9];
            mv._scalarsArray[4] = tempVar0 + tempVar15;

            tempVar0 = _scalarsArray[3] * mv2._scalarsArray[0];
            tempVar15 = _scalarsArray[8] * mv2._scalarsArray[11];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[12] * tempVar1;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[15] * tempVar13;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[0] * mv2._scalarsArray[3];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[7] * mv2._scalarsArray[4];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[4] * mv2._scalarsArray[7];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[11] * mv2._scalarsArray[8];
            mv._scalarsArray[3] = tempVar0 + tempVar15;

            tempVar0 = _scalarsArray[2] * mv2._scalarsArray[0];
            tempVar15 = mv2._scalarsArray[1] * tempVar3;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[10] * tempVar14;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[9] * mv2._scalarsArray[11];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[12] * tempVar5;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[15] * mv2._scalarsArray[13];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[14] * tempVar13;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[15] * tempVar8;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[0] * mv2._scalarsArray[2];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[1] * mv2._scalarsArray[3];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[6] * mv2._scalarsArray[4];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[7] * mv2._scalarsArray[5];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[6] * tempVar4;
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[5] * mv2._scalarsArray[7];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[10] * mv2._scalarsArray[8];
            tempVar0 += tempVar15;
            tempVar15 = _scalarsArray[11] * mv2._scalarsArray[9];
            mv._scalarsArray[2] = tempVar0 + tempVar15;

            tempVar0 = _scalarsArray[1] * mv2._scalarsArray[0];
            tempVar15 = _scalarsArray[0] * mv2._scalarsArray[1];
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[10] * tempVar7;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[11] * tempVar12;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[12] * tempVar8;
            tempVar0 += tempVar15;
            tempVar15 = mv2._scalarsArray[13] * tempVar13;
            tempVar0 += tempVar15;
            tempVar1 = mv2._scalarsArray[14] * tempVar1;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[14] * mv2._scalarsArray[15];
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[3] * mv2._scalarsArray[2];
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[3] * tempVar2;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[5] * mv2._scalarsArray[4];
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[5] * tempVar4;
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[6] * tempVar11;
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[7] * tempVar6;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[9] * mv2._scalarsArray[8];
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[9] * tempVar14;
            mv._scalarsArray[1] = tempVar0 + tempVar1;

            tempVar0 = _scalarsArray[0] * mv2._scalarsArray[0];
            tempVar1 = _scalarsArray[1] * mv2._scalarsArray[1];
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[10] * tempVar12;
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[11] * tempVar7;
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[12] * tempVar13;
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[13] * tempVar8;
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[14] * tempVar5;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[15] * mv2._scalarsArray[15];
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[2] * mv2._scalarsArray[2];
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[3] * tempVar3;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[4] * mv2._scalarsArray[4];
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[5] * tempVar10;
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[6] * tempVar6;
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[7] * tempVar11;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[8] * mv2._scalarsArray[8];
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[9] * tempVar9;
            mv._scalarsArray[0] = tempVar0 + tempVar1;

            //Finish GaClc SymbolicContext Code Generation, 2021-06-26T22:48:48.7554653+02:00


            return mv;
        }

        public Multivector Hip(Multivector mv2)
        {
            var mv = new Multivector();

            //Begin GaClc SymbolicContext Code Generation, 2021-06-26T22:48:49.0155945+02:00
            //SymbolicContext:
            //Input Variables: 30 used, 0 not used, 30 total.
            //Temp Variables: 214 sub-expressions, 0 generated temps, 214 total.
            //Target Temp Variables: 16 total.
            //Output Variables: 15 total.
            //Computations: 1 average, 229 total.
            //Memory Reads: 1.9388646288209608 average, 444 total.
            //Memory Writes: 229 total.
            //
            //SymbolicContext Binding Data:
            //   -1 = constant: '-1'
            //   mv1s1 = parameter: _scalarsArray[1]
            //   mv1s2 = parameter: _scalarsArray[2]
            //   mv1s3 = parameter: _scalarsArray[3]
            //   mv1s4 = parameter: _scalarsArray[4]
            //   mv1s5 = parameter: _scalarsArray[5]
            //   mv1s6 = parameter: _scalarsArray[6]
            //   mv1s7 = parameter: _scalarsArray[7]
            //   mv1s8 = parameter: _scalarsArray[8]
            //   mv1s9 = parameter: _scalarsArray[9]
            //   mv1s10 = parameter: _scalarsArray[10]
            //   mv1s11 = parameter: _scalarsArray[11]
            //   mv1s12 = parameter: _scalarsArray[12]
            //   mv1s13 = parameter: _scalarsArray[13]
            //   mv1s14 = parameter: _scalarsArray[14]
            //   mv1s15 = parameter: _scalarsArray[15]
            //   mv2s1 = parameter: mv2._scalarsArray[1]
            //   mv2s2 = parameter: mv2._scalarsArray[2]
            //   mv2s3 = parameter: mv2._scalarsArray[3]
            //   mv2s4 = parameter: mv2._scalarsArray[4]
            //   mv2s5 = parameter: mv2._scalarsArray[5]
            //   mv2s6 = parameter: mv2._scalarsArray[6]
            //   mv2s7 = parameter: mv2._scalarsArray[7]
            //   mv2s8 = parameter: mv2._scalarsArray[8]
            //   mv2s9 = parameter: mv2._scalarsArray[9]
            //   mv2s10 = parameter: mv2._scalarsArray[10]
            //   mv2s11 = parameter: mv2._scalarsArray[11]
            //   mv2s12 = parameter: mv2._scalarsArray[12]
            //   mv2s13 = parameter: mv2._scalarsArray[13]
            //   mv2s14 = parameter: mv2._scalarsArray[14]
            //   mv2s15 = parameter: mv2._scalarsArray[15]

            var tempVar0 = -1 * _scalarsArray[8];
            var tempVar1 = mv2._scalarsArray[15] * tempVar0;
            var tempVar2 = _scalarsArray[15] * mv2._scalarsArray[8];
            mv._scalarsArray[7] = tempVar1 + tempVar2;

            tempVar1 = _scalarsArray[4] * mv2._scalarsArray[15];
            tempVar2 = -1 * _scalarsArray[15];
            var tempVar3 = mv2._scalarsArray[4] * tempVar2;
            mv._scalarsArray[11] = tempVar1 + tempVar3;

            tempVar1 = -1 * _scalarsArray[2];
            tempVar3 = mv2._scalarsArray[15] * tempVar1;
            var tempVar4 = _scalarsArray[15] * mv2._scalarsArray[2];
            mv._scalarsArray[13] = tempVar3 + tempVar4;

            tempVar3 = mv2._scalarsArray[1] * tempVar2;
            tempVar4 = _scalarsArray[1] * mv2._scalarsArray[15];
            mv._scalarsArray[14] = tempVar3 + tempVar4;

            tempVar3 = _scalarsArray[13] * mv2._scalarsArray[1];
            tempVar4 = _scalarsArray[1] * mv2._scalarsArray[13];
            tempVar3 += tempVar4;
            tempVar4 = _scalarsArray[2] * mv2._scalarsArray[14];
            tempVar3 += tempVar4;
            tempVar4 = -1 * _scalarsArray[3];
            var tempVar5 = mv2._scalarsArray[15] * tempVar4;
            tempVar3 += tempVar5;
            tempVar5 = _scalarsArray[14] * mv2._scalarsArray[2];
            tempVar3 += tempVar5;
            tempVar5 = mv2._scalarsArray[3] * tempVar2;
            mv._scalarsArray[12] = tempVar3 + tempVar5;

            tempVar3 = mv2._scalarsArray[11] * tempVar1;
            tempVar5 = -1 * _scalarsArray[4];
            var tempVar6 = mv2._scalarsArray[13] * tempVar5;
            tempVar3 += tempVar6;
            tempVar6 = -1 * _scalarsArray[6];
            var tempVar7 = mv2._scalarsArray[15] * tempVar6;
            tempVar3 += tempVar7;
            tempVar7 = -1 * _scalarsArray[11];
            var tempVar8 = mv2._scalarsArray[2] * tempVar7;
            tempVar3 += tempVar8;
            tempVar8 = -1 * _scalarsArray[13];
            var tempVar9 = mv2._scalarsArray[4] * tempVar8;
            tempVar3 += tempVar9;
            tempVar9 = mv2._scalarsArray[6] * tempVar2;
            mv._scalarsArray[9] = tempVar3 + tempVar9;

            tempVar3 = _scalarsArray[11] * mv2._scalarsArray[1];
            tempVar9 = _scalarsArray[1] * mv2._scalarsArray[11];
            tempVar3 += tempVar9;
            tempVar9 = mv2._scalarsArray[14] * tempVar5;
            tempVar3 += tempVar9;
            tempVar9 = _scalarsArray[5] * mv2._scalarsArray[15];
            tempVar3 += tempVar9;
            tempVar9 = -1 * _scalarsArray[14];
            var tempVar10 = mv2._scalarsArray[4] * tempVar9;
            tempVar3 += tempVar10;
            tempVar10 = _scalarsArray[15] * mv2._scalarsArray[5];
            mv._scalarsArray[10] = tempVar3 + tempVar10;

            tempVar3 = -1 * _scalarsArray[9];
            tempVar10 = mv2._scalarsArray[1] * tempVar3;
            var tempVar11 = _scalarsArray[2] * mv2._scalarsArray[10];
            tempVar10 += tempVar11;
            tempVar11 = mv2._scalarsArray[11] * tempVar4;
            tempVar10 += tempVar11;
            tempVar11 = _scalarsArray[4] * mv2._scalarsArray[12];
            tempVar10 += tempVar11;
            tempVar11 = -1 * _scalarsArray[5];
            var tempVar12 = mv2._scalarsArray[13] * tempVar11;
            tempVar10 += tempVar12;
            tempVar12 = mv2._scalarsArray[14] * tempVar6;
            tempVar10 += tempVar12;
            tempVar12 = -1 * _scalarsArray[7];
            var tempVar13 = mv2._scalarsArray[15] * tempVar12;
            tempVar10 += tempVar13;
            tempVar13 = -1 * _scalarsArray[10];
            var tempVar14 = mv2._scalarsArray[2] * tempVar13;
            tempVar10 += tempVar14;
            tempVar14 = mv2._scalarsArray[3] * tempVar7;
            tempVar10 += tempVar14;
            tempVar14 = -1 * _scalarsArray[12];
            var tempVar15 = mv2._scalarsArray[4] * tempVar14;
            tempVar10 += tempVar15;
            tempVar15 = mv2._scalarsArray[5] * tempVar8;
            tempVar10 += tempVar15;
            tempVar15 = mv2._scalarsArray[6] * tempVar9;
            tempVar10 += tempVar15;
            tempVar15 = _scalarsArray[15] * mv2._scalarsArray[7];
            tempVar10 += tempVar15;
            tempVar15 = _scalarsArray[1] * mv2._scalarsArray[9];
            mv._scalarsArray[8] = tempVar10 + tempVar15;

            tempVar10 = _scalarsArray[8] * mv2._scalarsArray[11];
            tempVar15 = mv2._scalarsArray[12] * tempVar2;
            tempVar10 += tempVar15;
            tempVar15 = mv2._scalarsArray[15] * tempVar14;
            tempVar10 += tempVar15;
            tempVar15 = _scalarsArray[7] * mv2._scalarsArray[4];
            tempVar10 += tempVar15;
            tempVar15 = _scalarsArray[4] * mv2._scalarsArray[7];
            tempVar10 += tempVar15;
            tempVar15 = _scalarsArray[11] * mv2._scalarsArray[8];
            mv._scalarsArray[3] = tempVar10 + tempVar15;

            tempVar10 = _scalarsArray[15] * mv2._scalarsArray[10];
            tempVar15 = _scalarsArray[8] * mv2._scalarsArray[13];
            tempVar10 += tempVar15;
            tempVar15 = _scalarsArray[10] * mv2._scalarsArray[15];
            tempVar10 += tempVar15;
            tempVar15 = mv2._scalarsArray[2] * tempVar12;
            tempVar10 += tempVar15;
            tempVar15 = mv2._scalarsArray[7] * tempVar1;
            tempVar10 += tempVar15;
            tempVar15 = _scalarsArray[13] * mv2._scalarsArray[8];
            mv._scalarsArray[5] = tempVar10 + tempVar15;

            tempVar10 = _scalarsArray[7] * mv2._scalarsArray[1];
            tempVar15 = _scalarsArray[8] * mv2._scalarsArray[14];
            tempVar10 += tempVar15;
            tempVar15 = mv2._scalarsArray[15] * tempVar3;
            tempVar10 += tempVar15;
            tempVar15 = _scalarsArray[1] * mv2._scalarsArray[7];
            tempVar10 += tempVar15;
            tempVar15 = _scalarsArray[14] * mv2._scalarsArray[8];
            tempVar10 += tempVar15;
            tempVar15 = mv2._scalarsArray[9] * tempVar2;
            mv._scalarsArray[6] = tempVar10 + tempVar15;

            tempVar10 = mv2._scalarsArray[1] * tempVar11;
            tempVar15 = _scalarsArray[14] * mv2._scalarsArray[10];
            tempVar10 += tempVar15;
            tempVar15 = mv2._scalarsArray[11] * tempVar2;
            tempVar10 += tempVar15;
            tempVar15 = mv2._scalarsArray[12] * tempVar0;
            tempVar10 += tempVar15;
            tempVar15 = _scalarsArray[9] * mv2._scalarsArray[13];
            tempVar10 += tempVar15;
            tempVar15 = _scalarsArray[10] * mv2._scalarsArray[14];
            tempVar10 += tempVar15;
            tempVar15 = _scalarsArray[11] * mv2._scalarsArray[15];
            tempVar10 += tempVar15;
            tempVar15 = mv2._scalarsArray[2] * tempVar6;
            tempVar10 += tempVar15;
            tempVar15 = mv2._scalarsArray[3] * tempVar12;
            tempVar10 += tempVar15;
            tempVar15 = _scalarsArray[1] * mv2._scalarsArray[5];
            tempVar10 += tempVar15;
            tempVar15 = _scalarsArray[2] * mv2._scalarsArray[6];
            tempVar10 += tempVar15;
            tempVar15 = mv2._scalarsArray[7] * tempVar4;
            tempVar10 += tempVar15;
            tempVar15 = _scalarsArray[12] * mv2._scalarsArray[8];
            tempVar10 += tempVar15;
            tempVar15 = _scalarsArray[13] * mv2._scalarsArray[9];
            mv._scalarsArray[4] = tempVar10 + tempVar15;

            tempVar10 = mv2._scalarsArray[10] * tempVar7;
            tempVar15 = mv2._scalarsArray[11] * tempVar13;
            tempVar10 += tempVar15;
            tempVar15 = mv2._scalarsArray[12] * tempVar8;
            tempVar10 += tempVar15;
            tempVar15 = mv2._scalarsArray[13] * tempVar14;
            tempVar10 += tempVar15;
            tempVar2 = mv2._scalarsArray[14] * tempVar2;
            tempVar2 = tempVar10 + tempVar2;
            tempVar10 = _scalarsArray[14] * mv2._scalarsArray[15];
            tempVar2 += tempVar10;
            tempVar10 = _scalarsArray[3] * mv2._scalarsArray[2];
            tempVar2 += tempVar10;
            tempVar1 = mv2._scalarsArray[3] * tempVar1;
            tempVar1 = tempVar2 + tempVar1;
            tempVar2 = _scalarsArray[5] * mv2._scalarsArray[4];
            tempVar1 += tempVar2;
            tempVar2 = mv2._scalarsArray[5] * tempVar5;
            tempVar1 += tempVar2;
            tempVar2 = mv2._scalarsArray[6] * tempVar12;
            tempVar1 += tempVar2;
            tempVar2 = mv2._scalarsArray[7] * tempVar6;
            tempVar1 += tempVar2;
            tempVar2 = _scalarsArray[9] * mv2._scalarsArray[8];
            tempVar1 += tempVar2;
            tempVar2 = mv2._scalarsArray[9] * tempVar0;
            mv._scalarsArray[1] = tempVar1 + tempVar2;

            tempVar1 = mv2._scalarsArray[1] * tempVar4;
            tempVar0 = mv2._scalarsArray[10] * tempVar0;
            tempVar0 = tempVar1 + tempVar0;
            tempVar1 = _scalarsArray[9] * mv2._scalarsArray[11];
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[12] * tempVar9;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[15] * mv2._scalarsArray[13];
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[14] * tempVar14;
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[15] * tempVar8;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[1] * mv2._scalarsArray[3];
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[6] * mv2._scalarsArray[4];
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[7] * mv2._scalarsArray[5];
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[6] * tempVar5;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[5] * mv2._scalarsArray[7];
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[10] * mv2._scalarsArray[8];
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[11] * mv2._scalarsArray[9];
            mv._scalarsArray[2] = tempVar0 + tempVar1;

            tempVar0 = _scalarsArray[1] * mv2._scalarsArray[1];
            tempVar1 = mv2._scalarsArray[10] * tempVar13;
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[11] * tempVar7;
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[12] * tempVar14;
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[13] * tempVar8;
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[14] * tempVar9;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[15] * mv2._scalarsArray[15];
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[2] * mv2._scalarsArray[2];
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[3] * tempVar4;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[4] * mv2._scalarsArray[4];
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[5] * tempVar11;
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[6] * tempVar6;
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[7] * tempVar12;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[8] * mv2._scalarsArray[8];
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[9] * tempVar3;
            mv._scalarsArray[0] = tempVar0 + tempVar1;

            //Finish GaClc SymbolicContext Code Generation, 2021-06-26T22:48:49.0160604+02:00


            return mv;
        }

        public Multivector Acp(Multivector mv2)
        {
            var mv = new Multivector();

            //Begin GaClc SymbolicContext Code Generation, 2021-06-26T22:48:49.2813829+02:00
            //SymbolicContext:
            //Input Variables: 32 used, 0 not used, 32 total.
            //Temp Variables: 253 sub-expressions, 0 generated temps, 253 total.
            //Target Temp Variables: 15 total.
            //Output Variables: 16 total.
            //Computations: 1 average, 269 total.
            //Memory Reads: 1.9516728624535316 average, 525 total.
            //Memory Writes: 269 total.
            //
            //SymbolicContext Binding Data:
            //   -1 = constant: '-1'
            //   mv1s0 = parameter: _scalarsArray[0]
            //   mv1s1 = parameter: _scalarsArray[1]
            //   mv1s2 = parameter: _scalarsArray[2]
            //   mv1s3 = parameter: _scalarsArray[3]
            //   mv1s4 = parameter: _scalarsArray[4]
            //   mv1s5 = parameter: _scalarsArray[5]
            //   mv1s6 = parameter: _scalarsArray[6]
            //   mv1s7 = parameter: _scalarsArray[7]
            //   mv1s8 = parameter: _scalarsArray[8]
            //   mv1s9 = parameter: _scalarsArray[9]
            //   mv1s10 = parameter: _scalarsArray[10]
            //   mv1s11 = parameter: _scalarsArray[11]
            //   mv1s12 = parameter: _scalarsArray[12]
            //   mv1s13 = parameter: _scalarsArray[13]
            //   mv1s14 = parameter: _scalarsArray[14]
            //   mv1s15 = parameter: _scalarsArray[15]
            //   mv2s0 = parameter: mv2._scalarsArray[0]
            //   mv2s1 = parameter: mv2._scalarsArray[1]
            //   mv2s2 = parameter: mv2._scalarsArray[2]
            //   mv2s3 = parameter: mv2._scalarsArray[3]
            //   mv2s4 = parameter: mv2._scalarsArray[4]
            //   mv2s5 = parameter: mv2._scalarsArray[5]
            //   mv2s6 = parameter: mv2._scalarsArray[6]
            //   mv2s7 = parameter: mv2._scalarsArray[7]
            //   mv2s8 = parameter: mv2._scalarsArray[8]
            //   mv2s9 = parameter: mv2._scalarsArray[9]
            //   mv2s10 = parameter: mv2._scalarsArray[10]
            //   mv2s11 = parameter: mv2._scalarsArray[11]
            //   mv2s12 = parameter: mv2._scalarsArray[12]
            //   mv2s13 = parameter: mv2._scalarsArray[13]
            //   mv2s14 = parameter: mv2._scalarsArray[14]
            //   mv2s15 = parameter: mv2._scalarsArray[15]

            var tempVar0 = _scalarsArray[7] * mv2._scalarsArray[0];
            var tempVar1 = _scalarsArray[6] * mv2._scalarsArray[1];
            tempVar0 += tempVar1;
            tempVar1 = -1 * _scalarsArray[5];
            var tempVar2 = mv2._scalarsArray[2] * tempVar1;
            tempVar0 += tempVar2;
            tempVar2 = _scalarsArray[4] * mv2._scalarsArray[3];
            tempVar0 += tempVar2;
            tempVar2 = _scalarsArray[3] * mv2._scalarsArray[4];
            tempVar0 += tempVar2;
            tempVar2 = -1 * _scalarsArray[2];
            var tempVar3 = mv2._scalarsArray[5] * tempVar2;
            tempVar0 += tempVar3;
            tempVar3 = _scalarsArray[1] * mv2._scalarsArray[6];
            tempVar0 += tempVar3;
            tempVar3 = _scalarsArray[0] * mv2._scalarsArray[7];
            mv._scalarsArray[7] = tempVar0 + tempVar3;

            tempVar0 = _scalarsArray[11] * mv2._scalarsArray[0];
            tempVar3 = _scalarsArray[10] * mv2._scalarsArray[1];
            tempVar0 += tempVar3;
            tempVar3 = _scalarsArray[1] * mv2._scalarsArray[10];
            tempVar0 += tempVar3;
            tempVar3 = _scalarsArray[0] * mv2._scalarsArray[11];
            tempVar0 += tempVar3;
            tempVar3 = -1 * _scalarsArray[9];
            var tempVar4 = mv2._scalarsArray[2] * tempVar3;
            tempVar0 += tempVar4;
            tempVar4 = _scalarsArray[8] * mv2._scalarsArray[3];
            tempVar0 += tempVar4;
            tempVar4 = _scalarsArray[3] * mv2._scalarsArray[8];
            tempVar0 += tempVar4;
            tempVar4 = mv2._scalarsArray[9] * tempVar2;
            mv._scalarsArray[11] = tempVar0 + tempVar4;

            tempVar0 = _scalarsArray[13] * mv2._scalarsArray[0];
            tempVar4 = _scalarsArray[12] * mv2._scalarsArray[1];
            tempVar0 += tempVar4;
            tempVar4 = _scalarsArray[1] * mv2._scalarsArray[12];
            tempVar0 += tempVar4;
            tempVar4 = _scalarsArray[0] * mv2._scalarsArray[13];
            tempVar0 += tempVar4;
            tempVar4 = mv2._scalarsArray[4] * tempVar3;
            tempVar0 += tempVar4;
            tempVar4 = _scalarsArray[8] * mv2._scalarsArray[5];
            tempVar0 += tempVar4;
            tempVar4 = _scalarsArray[5] * mv2._scalarsArray[8];
            tempVar0 += tempVar4;
            tempVar4 = -1 * _scalarsArray[4];
            var tempVar5 = mv2._scalarsArray[9] * tempVar4;
            mv._scalarsArray[13] = tempVar0 + tempVar5;

            tempVar0 = _scalarsArray[1] * mv2._scalarsArray[0];
            tempVar5 = _scalarsArray[0] * mv2._scalarsArray[1];
            tempVar0 += tempVar5;
            tempVar5 = -1 * _scalarsArray[11];
            var tempVar6 = mv2._scalarsArray[10] * tempVar5;
            tempVar0 += tempVar6;
            tempVar6 = -1 * _scalarsArray[10];
            var tempVar7 = mv2._scalarsArray[11] * tempVar6;
            tempVar0 += tempVar7;
            tempVar7 = -1 * _scalarsArray[13];
            var tempVar8 = mv2._scalarsArray[12] * tempVar7;
            tempVar0 += tempVar8;
            tempVar8 = -1 * _scalarsArray[12];
            var tempVar9 = mv2._scalarsArray[13] * tempVar8;
            tempVar0 += tempVar9;
            tempVar9 = -1 * _scalarsArray[7];
            var tempVar10 = mv2._scalarsArray[6] * tempVar9;
            tempVar0 += tempVar10;
            tempVar10 = -1 * _scalarsArray[6];
            var tempVar11 = mv2._scalarsArray[7] * tempVar10;
            mv._scalarsArray[1] = tempVar0 + tempVar11;

            tempVar0 = _scalarsArray[14] * mv2._scalarsArray[0];
            tempVar11 = mv2._scalarsArray[10] * tempVar4;
            tempVar0 += tempVar11;
            tempVar11 = _scalarsArray[2] * mv2._scalarsArray[12];
            tempVar0 += tempVar11;
            tempVar11 = _scalarsArray[0] * mv2._scalarsArray[14];
            tempVar0 += tempVar11;
            tempVar11 = _scalarsArray[12] * mv2._scalarsArray[2];
            tempVar0 += tempVar11;
            tempVar11 = mv2._scalarsArray[4] * tempVar6;
            tempVar0 += tempVar11;
            tempVar11 = _scalarsArray[8] * mv2._scalarsArray[6];
            tempVar0 += tempVar11;
            tempVar11 = _scalarsArray[6] * mv2._scalarsArray[8];
            mv._scalarsArray[14] = tempVar0 + tempVar11;

            tempVar0 = _scalarsArray[8] * mv2._scalarsArray[0];
            tempVar11 = -1 * _scalarsArray[3];
            var tempVar12 = mv2._scalarsArray[11] * tempVar11;
            tempVar0 += tempVar12;
            tempVar12 = mv2._scalarsArray[13] * tempVar1;
            tempVar0 += tempVar12;
            tempVar12 = mv2._scalarsArray[14] * tempVar10;
            tempVar0 += tempVar12;
            tempVar12 = mv2._scalarsArray[3] * tempVar5;
            tempVar0 += tempVar12;
            tempVar12 = mv2._scalarsArray[5] * tempVar7;
            tempVar0 += tempVar12;
            tempVar12 = -1 * _scalarsArray[14];
            var tempVar13 = mv2._scalarsArray[6] * tempVar12;
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[0] * mv2._scalarsArray[8];
            mv._scalarsArray[8] = tempVar0 + tempVar13;

            tempVar0 = _scalarsArray[4] * mv2._scalarsArray[0];
            tempVar13 = _scalarsArray[14] * mv2._scalarsArray[10];
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[9] * mv2._scalarsArray[13];
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[10] * mv2._scalarsArray[14];
            tempVar0 += tempVar13;
            tempVar13 = mv2._scalarsArray[3] * tempVar9;
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[0] * mv2._scalarsArray[4];
            tempVar0 += tempVar13;
            tempVar13 = mv2._scalarsArray[7] * tempVar11;
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[13] * mv2._scalarsArray[9];
            mv._scalarsArray[4] = tempVar0 + tempVar13;

            tempVar0 = _scalarsArray[2] * mv2._scalarsArray[0];
            tempVar13 = _scalarsArray[9] * mv2._scalarsArray[11];
            tempVar0 += tempVar13;
            tempVar13 = mv2._scalarsArray[12] * tempVar12;
            tempVar0 += tempVar13;
            tempVar13 = mv2._scalarsArray[14] * tempVar8;
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[0] * mv2._scalarsArray[2];
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[7] * mv2._scalarsArray[5];
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[5] * mv2._scalarsArray[7];
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[11] * mv2._scalarsArray[9];
            mv._scalarsArray[2] = tempVar0 + tempVar13;

            tempVar0 = _scalarsArray[15] * mv2._scalarsArray[0];
            tempVar13 = mv2._scalarsArray[10] * tempVar1;
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[3] * mv2._scalarsArray[12];
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[0] * mv2._scalarsArray[15];
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[12] * mv2._scalarsArray[3];
            tempVar0 += tempVar13;
            tempVar13 = mv2._scalarsArray[5] * tempVar6;
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[9] * mv2._scalarsArray[6];
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[6] * mv2._scalarsArray[9];
            mv._scalarsArray[15] = tempVar0 + tempVar13;

            tempVar0 = _scalarsArray[12] * mv2._scalarsArray[0];
            tempVar13 = _scalarsArray[13] * mv2._scalarsArray[1];
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[0] * mv2._scalarsArray[12];
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[1] * mv2._scalarsArray[13];
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[2] * mv2._scalarsArray[14];
            tempVar0 += tempVar13;
            tempVar13 = mv2._scalarsArray[15] * tempVar11;
            tempVar0 += tempVar13;
            tempVar13 = _scalarsArray[14] * mv2._scalarsArray[2];
            tempVar0 += tempVar13;
            tempVar13 = -1 * _scalarsArray[15];
            var tempVar14 = mv2._scalarsArray[3] * tempVar13;
            mv._scalarsArray[12] = tempVar0 + tempVar14;

            tempVar0 = _scalarsArray[10] * mv2._scalarsArray[0];
            tempVar14 = _scalarsArray[11] * mv2._scalarsArray[1];
            tempVar0 += tempVar14;
            tempVar14 = _scalarsArray[0] * mv2._scalarsArray[10];
            tempVar0 += tempVar14;
            tempVar14 = _scalarsArray[1] * mv2._scalarsArray[11];
            tempVar0 += tempVar14;
            tempVar14 = mv2._scalarsArray[14] * tempVar4;
            tempVar0 += tempVar14;
            tempVar14 = _scalarsArray[5] * mv2._scalarsArray[15];
            tempVar0 += tempVar14;
            tempVar14 = mv2._scalarsArray[4] * tempVar12;
            tempVar0 += tempVar14;
            tempVar14 = _scalarsArray[15] * mv2._scalarsArray[5];
            mv._scalarsArray[10] = tempVar0 + tempVar14;

            tempVar0 = _scalarsArray[9] * mv2._scalarsArray[0];
            tempVar14 = mv2._scalarsArray[11] * tempVar2;
            tempVar0 += tempVar14;
            tempVar4 = mv2._scalarsArray[13] * tempVar4;
            tempVar0 += tempVar4;
            tempVar4 = mv2._scalarsArray[15] * tempVar10;
            tempVar0 += tempVar4;
            tempVar4 = mv2._scalarsArray[2] * tempVar5;
            tempVar0 += tempVar4;
            tempVar4 = mv2._scalarsArray[4] * tempVar7;
            tempVar0 += tempVar4;
            tempVar4 = mv2._scalarsArray[6] * tempVar13;
            tempVar0 += tempVar4;
            tempVar4 = _scalarsArray[0] * mv2._scalarsArray[9];
            mv._scalarsArray[9] = tempVar0 + tempVar4;

            tempVar0 = _scalarsArray[6] * mv2._scalarsArray[0];
            tempVar4 = _scalarsArray[7] * mv2._scalarsArray[1];
            tempVar0 += tempVar4;
            tempVar4 = _scalarsArray[8] * mv2._scalarsArray[14];
            tempVar0 += tempVar4;
            tempVar4 = mv2._scalarsArray[15] * tempVar3;
            tempVar0 += tempVar4;
            tempVar4 = _scalarsArray[0] * mv2._scalarsArray[6];
            tempVar0 += tempVar4;
            tempVar4 = _scalarsArray[1] * mv2._scalarsArray[7];
            tempVar0 += tempVar4;
            tempVar4 = _scalarsArray[14] * mv2._scalarsArray[8];
            tempVar0 += tempVar4;
            tempVar4 = mv2._scalarsArray[9] * tempVar13;
            mv._scalarsArray[6] = tempVar0 + tempVar4;

            tempVar0 = _scalarsArray[5] * mv2._scalarsArray[0];
            tempVar4 = _scalarsArray[15] * mv2._scalarsArray[10];
            tempVar0 += tempVar4;
            tempVar4 = _scalarsArray[8] * mv2._scalarsArray[13];
            tempVar0 += tempVar4;
            tempVar4 = _scalarsArray[10] * mv2._scalarsArray[15];
            tempVar0 += tempVar4;
            tempVar4 = mv2._scalarsArray[2] * tempVar9;
            tempVar0 += tempVar4;
            tempVar4 = _scalarsArray[0] * mv2._scalarsArray[5];
            tempVar0 += tempVar4;
            tempVar2 = mv2._scalarsArray[7] * tempVar2;
            tempVar0 += tempVar2;
            tempVar2 = _scalarsArray[13] * mv2._scalarsArray[8];
            mv._scalarsArray[5] = tempVar0 + tempVar2;

            tempVar0 = _scalarsArray[3] * mv2._scalarsArray[0];
            tempVar2 = _scalarsArray[8] * mv2._scalarsArray[11];
            tempVar0 += tempVar2;
            tempVar2 = mv2._scalarsArray[12] * tempVar13;
            tempVar0 += tempVar2;
            tempVar2 = mv2._scalarsArray[15] * tempVar8;
            tempVar0 += tempVar2;
            tempVar2 = _scalarsArray[0] * mv2._scalarsArray[3];
            tempVar0 += tempVar2;
            tempVar2 = _scalarsArray[7] * mv2._scalarsArray[4];
            tempVar0 += tempVar2;
            tempVar2 = _scalarsArray[4] * mv2._scalarsArray[7];
            tempVar0 += tempVar2;
            tempVar2 = _scalarsArray[11] * mv2._scalarsArray[8];
            mv._scalarsArray[3] = tempVar0 + tempVar2;

            tempVar0 = _scalarsArray[0] * mv2._scalarsArray[0];
            tempVar2 = _scalarsArray[1] * mv2._scalarsArray[1];
            tempVar0 += tempVar2;
            tempVar2 = mv2._scalarsArray[10] * tempVar6;
            tempVar0 += tempVar2;
            tempVar2 = mv2._scalarsArray[11] * tempVar5;
            tempVar0 += tempVar2;
            tempVar2 = mv2._scalarsArray[12] * tempVar8;
            tempVar0 += tempVar2;
            tempVar2 = mv2._scalarsArray[13] * tempVar7;
            tempVar0 += tempVar2;
            tempVar2 = mv2._scalarsArray[14] * tempVar12;
            tempVar0 += tempVar2;
            tempVar2 = _scalarsArray[15] * mv2._scalarsArray[15];
            tempVar0 += tempVar2;
            tempVar2 = _scalarsArray[2] * mv2._scalarsArray[2];
            tempVar0 += tempVar2;
            tempVar2 = mv2._scalarsArray[3] * tempVar11;
            tempVar0 += tempVar2;
            tempVar2 = _scalarsArray[4] * mv2._scalarsArray[4];
            tempVar0 += tempVar2;
            tempVar1 = mv2._scalarsArray[5] * tempVar1;
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[6] * tempVar10;
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[7] * tempVar9;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[8] * mv2._scalarsArray[8];
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[9] * tempVar3;
            mv._scalarsArray[0] = tempVar0 + tempVar1;

            //Finish GaClc SymbolicContext Code Generation, 2021-06-26T22:48:49.2819212+02:00


            return mv;
        }

        public Multivector Cp(Multivector mv2)
        {
            var mv = new Multivector();

            //Begin GaClc SymbolicContext Code Generation, 2021-06-26T22:48:49.5531124+02:00
            //SymbolicContext:
            //Input Variables: 30 used, 0 not used, 30 total.
            //Temp Variables: 224 sub-expressions, 0 generated temps, 224 total.
            //Target Temp Variables: 16 total.
            //Output Variables: 15 total.
            //Computations: 1 average, 239 total.
            //Memory Reads: 1.9414225941422594 average, 464 total.
            //Memory Writes: 239 total.
            //
            //SymbolicContext Binding Data:
            //   -1 = constant: '-1'
            //   mv1s1 = parameter: _scalarsArray[1]
            //   mv1s2 = parameter: _scalarsArray[2]
            //   mv1s3 = parameter: _scalarsArray[3]
            //   mv1s4 = parameter: _scalarsArray[4]
            //   mv1s5 = parameter: _scalarsArray[5]
            //   mv1s6 = parameter: _scalarsArray[6]
            //   mv1s7 = parameter: _scalarsArray[7]
            //   mv1s8 = parameter: _scalarsArray[8]
            //   mv1s9 = parameter: _scalarsArray[9]
            //   mv1s10 = parameter: _scalarsArray[10]
            //   mv1s11 = parameter: _scalarsArray[11]
            //   mv1s12 = parameter: _scalarsArray[12]
            //   mv1s13 = parameter: _scalarsArray[13]
            //   mv1s14 = parameter: _scalarsArray[14]
            //   mv1s15 = parameter: _scalarsArray[15]
            //   mv2s1 = parameter: mv2._scalarsArray[1]
            //   mv2s2 = parameter: mv2._scalarsArray[2]
            //   mv2s3 = parameter: mv2._scalarsArray[3]
            //   mv2s4 = parameter: mv2._scalarsArray[4]
            //   mv2s5 = parameter: mv2._scalarsArray[5]
            //   mv2s6 = parameter: mv2._scalarsArray[6]
            //   mv2s7 = parameter: mv2._scalarsArray[7]
            //   mv2s8 = parameter: mv2._scalarsArray[8]
            //   mv2s9 = parameter: mv2._scalarsArray[9]
            //   mv2s10 = parameter: mv2._scalarsArray[10]
            //   mv2s11 = parameter: mv2._scalarsArray[11]
            //   mv2s12 = parameter: mv2._scalarsArray[12]
            //   mv2s13 = parameter: mv2._scalarsArray[13]
            //   mv2s14 = parameter: mv2._scalarsArray[14]
            //   mv2s15 = parameter: mv2._scalarsArray[15]

            var tempVar0 = -1 * _scalarsArray[6];
            var tempVar1 = mv2._scalarsArray[10] * tempVar0;
            var tempVar2 = -1 * _scalarsArray[7];
            var tempVar3 = mv2._scalarsArray[11] * tempVar2;
            tempVar1 += tempVar3;
            tempVar3 = -1 * _scalarsArray[8];
            var tempVar4 = mv2._scalarsArray[4] * tempVar3;
            tempVar1 += tempVar4;
            tempVar4 = _scalarsArray[9] * mv2._scalarsArray[5];
            tempVar1 += tempVar4;
            tempVar4 = _scalarsArray[10] * mv2._scalarsArray[6];
            tempVar1 += tempVar4;
            tempVar4 = _scalarsArray[11] * mv2._scalarsArray[7];
            tempVar1 += tempVar4;
            tempVar4 = _scalarsArray[4] * mv2._scalarsArray[8];
            tempVar1 += tempVar4;
            tempVar4 = -1 * _scalarsArray[5];
            var tempVar5 = mv2._scalarsArray[9] * tempVar4;
            mv._scalarsArray[12] = tempVar1 + tempVar5;

            tempVar1 = _scalarsArray[6] * mv2._scalarsArray[12];
            tempVar5 = _scalarsArray[7] * mv2._scalarsArray[13];
            tempVar1 += tempVar5;
            tempVar5 = mv2._scalarsArray[2] * tempVar3;
            tempVar1 += tempVar5;
            tempVar5 = _scalarsArray[9] * mv2._scalarsArray[3];
            tempVar1 += tempVar5;
            tempVar5 = -1 * _scalarsArray[12];
            var tempVar6 = mv2._scalarsArray[6] * tempVar5;
            tempVar1 += tempVar6;
            tempVar6 = -1 * _scalarsArray[13];
            var tempVar7 = mv2._scalarsArray[7] * tempVar6;
            tempVar1 += tempVar7;
            tempVar7 = _scalarsArray[2] * mv2._scalarsArray[8];
            tempVar1 += tempVar7;
            tempVar7 = -1 * _scalarsArray[3];
            var tempVar8 = mv2._scalarsArray[9] * tempVar7;
            mv._scalarsArray[10] = tempVar1 + tempVar8;

            tempVar1 = _scalarsArray[12] * mv2._scalarsArray[10];
            tempVar8 = _scalarsArray[13] * mv2._scalarsArray[11];
            tempVar1 += tempVar8;
            tempVar8 = -1 * _scalarsArray[10];
            var tempVar9 = mv2._scalarsArray[12] * tempVar8;
            tempVar1 += tempVar9;
            tempVar9 = -1 * _scalarsArray[11];
            var tempVar10 = mv2._scalarsArray[13] * tempVar9;
            tempVar1 += tempVar10;
            tempVar10 = -1 * _scalarsArray[4];
            var tempVar11 = mv2._scalarsArray[2] * tempVar10;
            tempVar1 += tempVar11;
            tempVar11 = _scalarsArray[5] * mv2._scalarsArray[3];
            tempVar1 += tempVar11;
            tempVar11 = _scalarsArray[2] * mv2._scalarsArray[4];
            tempVar1 += tempVar11;
            tempVar11 = mv2._scalarsArray[5] * tempVar7;
            mv._scalarsArray[6] = tempVar1 + tempVar11;

            tempVar1 = -1 * _scalarsArray[14];
            tempVar11 = mv2._scalarsArray[1] * tempVar1;
            var tempVar12 = _scalarsArray[4] * mv2._scalarsArray[11];
            tempVar11 += tempVar12;
            tempVar12 = -1 * _scalarsArray[2];
            var tempVar13 = mv2._scalarsArray[13] * tempVar12;
            tempVar11 += tempVar13;
            tempVar13 = _scalarsArray[1] * mv2._scalarsArray[14];
            tempVar11 += tempVar13;
            tempVar13 = _scalarsArray[13] * mv2._scalarsArray[2];
            tempVar11 += tempVar13;
            tempVar13 = mv2._scalarsArray[4] * tempVar9;
            tempVar11 += tempVar13;
            tempVar13 = mv2._scalarsArray[7] * tempVar3;
            tempVar11 += tempVar13;
            tempVar13 = _scalarsArray[7] * mv2._scalarsArray[8];
            mv._scalarsArray[15] = tempVar11 + tempVar13;

            tempVar11 = mv2._scalarsArray[1] * tempVar3;
            tempVar13 = _scalarsArray[3] * mv2._scalarsArray[10];
            tempVar11 += tempVar13;
            tempVar13 = _scalarsArray[5] * mv2._scalarsArray[12];
            tempVar11 += tempVar13;
            tempVar13 = mv2._scalarsArray[14] * tempVar2;
            tempVar11 += tempVar13;
            tempVar13 = mv2._scalarsArray[3] * tempVar8;
            tempVar11 += tempVar13;
            tempVar13 = mv2._scalarsArray[5] * tempVar5;
            tempVar11 += tempVar13;
            tempVar13 = _scalarsArray[14] * mv2._scalarsArray[7];
            tempVar11 += tempVar13;
            tempVar13 = _scalarsArray[1] * mv2._scalarsArray[8];
            mv._scalarsArray[9] = tempVar11 + tempVar13;

            tempVar11 = mv2._scalarsArray[1] * tempVar10;
            tempVar13 = mv2._scalarsArray[11] * tempVar1;
            tempVar11 += tempVar13;
            tempVar13 = -1 * _scalarsArray[9];
            var tempVar14 = mv2._scalarsArray[12] * tempVar13;
            tempVar11 += tempVar14;
            tempVar14 = _scalarsArray[11] * mv2._scalarsArray[14];
            tempVar11 += tempVar14;
            tempVar14 = mv2._scalarsArray[3] * tempVar0;
            tempVar11 += tempVar14;
            tempVar14 = _scalarsArray[1] * mv2._scalarsArray[4];
            tempVar11 += tempVar14;
            tempVar14 = _scalarsArray[3] * mv2._scalarsArray[6];
            tempVar11 += tempVar14;
            tempVar14 = _scalarsArray[12] * mv2._scalarsArray[9];
            mv._scalarsArray[5] = tempVar11 + tempVar14;

            tempVar11 = mv2._scalarsArray[1] * tempVar12;
            tempVar14 = mv2._scalarsArray[10] * tempVar13;
            tempVar11 += tempVar14;
            tempVar14 = _scalarsArray[14] * mv2._scalarsArray[13];
            tempVar11 += tempVar14;
            tempVar14 = mv2._scalarsArray[14] * tempVar6;
            tempVar11 += tempVar14;
            tempVar14 = _scalarsArray[1] * mv2._scalarsArray[2];
            tempVar11 += tempVar14;
            tempVar14 = _scalarsArray[6] * mv2._scalarsArray[5];
            tempVar11 += tempVar14;
            tempVar14 = mv2._scalarsArray[6] * tempVar4;
            tempVar11 += tempVar14;
            tempVar14 = _scalarsArray[10] * mv2._scalarsArray[9];
            mv._scalarsArray[3] = tempVar11 + tempVar14;

            tempVar11 = -1 * _scalarsArray[15];
            tempVar14 = mv2._scalarsArray[1] * tempVar11;
            var tempVar15 = _scalarsArray[5] * mv2._scalarsArray[11];
            tempVar14 += tempVar15;
            tempVar15 = mv2._scalarsArray[13] * tempVar7;
            tempVar14 += tempVar15;
            tempVar15 = _scalarsArray[1] * mv2._scalarsArray[15];
            tempVar14 += tempVar15;
            tempVar15 = _scalarsArray[13] * mv2._scalarsArray[3];
            tempVar14 += tempVar15;
            tempVar15 = mv2._scalarsArray[5] * tempVar9;
            tempVar14 += tempVar15;
            tempVar15 = mv2._scalarsArray[7] * tempVar13;
            tempVar14 += tempVar15;
            tempVar15 = _scalarsArray[7] * mv2._scalarsArray[9];
            mv._scalarsArray[14] = tempVar14 + tempVar15;

            tempVar14 = mv2._scalarsArray[10] * tempVar2;
            tempVar15 = mv2._scalarsArray[11] * tempVar0;
            tempVar14 += tempVar15;
            tempVar15 = _scalarsArray[3] * mv2._scalarsArray[14];
            tempVar14 += tempVar15;
            tempVar15 = mv2._scalarsArray[15] * tempVar12;
            tempVar14 += tempVar15;
            tempVar15 = _scalarsArray[15] * mv2._scalarsArray[2];
            tempVar14 += tempVar15;
            tempVar15 = mv2._scalarsArray[3] * tempVar1;
            tempVar14 += tempVar15;
            tempVar15 = _scalarsArray[11] * mv2._scalarsArray[6];
            tempVar14 += tempVar15;
            tempVar15 = _scalarsArray[10] * mv2._scalarsArray[7];
            mv._scalarsArray[13] = tempVar14 + tempVar15;

            tempVar14 = _scalarsArray[7] * mv2._scalarsArray[12];
            tempVar15 = _scalarsArray[6] * mv2._scalarsArray[13];
            tempVar14 += tempVar15;
            tempVar15 = mv2._scalarsArray[14] * tempVar4;
            tempVar14 += tempVar15;
            tempVar15 = _scalarsArray[4] * mv2._scalarsArray[15];
            tempVar14 += tempVar15;
            tempVar15 = mv2._scalarsArray[4] * tempVar11;
            tempVar14 += tempVar15;
            tempVar15 = _scalarsArray[14] * mv2._scalarsArray[5];
            tempVar14 += tempVar15;
            tempVar15 = mv2._scalarsArray[6] * tempVar6;
            tempVar14 += tempVar15;
            tempVar15 = mv2._scalarsArray[7] * tempVar5;
            mv._scalarsArray[11] = tempVar14 + tempVar15;

            tempVar13 = mv2._scalarsArray[1] * tempVar13;
            tempVar14 = _scalarsArray[2] * mv2._scalarsArray[10];
            tempVar13 += tempVar14;
            tempVar14 = _scalarsArray[4] * mv2._scalarsArray[12];
            tempVar13 += tempVar14;
            tempVar2 = mv2._scalarsArray[15] * tempVar2;
            tempVar2 = tempVar13 + tempVar2;
            tempVar13 = mv2._scalarsArray[2] * tempVar8;
            tempVar2 += tempVar13;
            tempVar5 = mv2._scalarsArray[4] * tempVar5;
            tempVar2 += tempVar5;
            tempVar5 = _scalarsArray[15] * mv2._scalarsArray[7];
            tempVar2 += tempVar5;
            tempVar5 = _scalarsArray[1] * mv2._scalarsArray[9];
            mv._scalarsArray[8] = tempVar2 + tempVar5;

            tempVar2 = _scalarsArray[13] * mv2._scalarsArray[10];
            tempVar5 = _scalarsArray[12] * mv2._scalarsArray[11];
            tempVar2 += tempVar5;
            tempVar5 = mv2._scalarsArray[12] * tempVar9;
            tempVar2 += tempVar5;
            tempVar5 = mv2._scalarsArray[13] * tempVar8;
            tempVar2 += tempVar5;
            tempVar5 = _scalarsArray[9] * mv2._scalarsArray[14];
            tempVar2 += tempVar5;
            tempVar5 = mv2._scalarsArray[15] * tempVar3;
            tempVar2 += tempVar5;
            tempVar5 = _scalarsArray[15] * mv2._scalarsArray[8];
            tempVar2 += tempVar5;
            tempVar1 = mv2._scalarsArray[9] * tempVar1;
            mv._scalarsArray[7] = tempVar2 + tempVar1;

            tempVar1 = mv2._scalarsArray[1] * tempVar4;
            tempVar2 = mv2._scalarsArray[11] * tempVar11;
            tempVar1 += tempVar2;
            tempVar2 = mv2._scalarsArray[12] * tempVar3;
            tempVar1 += tempVar2;
            tempVar2 = _scalarsArray[11] * mv2._scalarsArray[15];
            tempVar1 += tempVar2;
            tempVar0 = mv2._scalarsArray[2] * tempVar0;
            tempVar0 = tempVar1 + tempVar0;
            tempVar1 = _scalarsArray[1] * mv2._scalarsArray[5];
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[2] * mv2._scalarsArray[6];
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[12] * mv2._scalarsArray[8];
            mv._scalarsArray[4] = tempVar0 + tempVar1;

            tempVar0 = mv2._scalarsArray[1] * tempVar7;
            tempVar1 = mv2._scalarsArray[10] * tempVar3;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[15] * mv2._scalarsArray[13];
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[15] * tempVar6;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[1] * mv2._scalarsArray[3];
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[6] * mv2._scalarsArray[4];
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[6] * tempVar10;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[10] * mv2._scalarsArray[8];
            mv._scalarsArray[2] = tempVar0 + tempVar1;

            tempVar0 = mv2._scalarsArray[14] * tempVar11;
            tempVar1 = _scalarsArray[14] * mv2._scalarsArray[15];
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[3] * mv2._scalarsArray[2];
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[3] * tempVar12;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[5] * mv2._scalarsArray[4];
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[5] * tempVar10;
            tempVar0 += tempVar1;
            tempVar1 = _scalarsArray[9] * mv2._scalarsArray[8];
            tempVar0 += tempVar1;
            tempVar1 = mv2._scalarsArray[9] * tempVar3;
            mv._scalarsArray[1] = tempVar0 + tempVar1;

            //Finish GaClc SymbolicContext Code Generation, 2021-06-26T22:48:49.5536254+02:00


            return mv;
        }

        public IEnumerator<double> GetEnumerator()
        {
            return ((IEnumerable<double>)_scalarsArray).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}