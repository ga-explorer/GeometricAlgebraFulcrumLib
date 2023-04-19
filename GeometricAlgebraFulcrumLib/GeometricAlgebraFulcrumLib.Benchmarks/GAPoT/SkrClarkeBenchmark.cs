using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BenchmarkDotNet.Attributes;
using DataStructuresLib.Basic;
using GAPoTNumLib.GAPoT;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Arrays.Float64;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Mutable;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Rotation;

namespace GeometricAlgebraFulcrumLib.Benchmarks.GAPoT
{
    [SimpleJob(baseline: true)]
    public class SkrClarkeBenchmark
    {
        //[Params(3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48)]
        [Params(28, 29, 30, 31, 32, 33)]
        public int VSpaceDimensions { get; set; }// = 24;

        public IReadOnlyList<double[]> VectorArrayList { get; private set; }

        //public IReadOnlyList<Float64Tuple> VectorList { get; private set; }
        
        public double[,] ClarkeArray { get; private set; }

        public IReadOnlyList<Triplet<double[]>> ClarkeSimpleRotationSequence { get; private set; }

        //public ClarkeRotation ClarkeArrayRotation { get; private set; }

        //public VectorToVectorRotationSequence ClarkeSimpleRotationSequence { get; private set; }

        //public Func<double[], double[]> SkrRotateFunction { get; private set; }

        //public Func<double[], double[]> ClarkeRotateFunction { get; private set; }


        [GlobalSetup]
        public void Setup()
        {
            VectorArrayList =
                Enumerable
                    .Range(0, 1000)
                    .Select(_ => GetRandomVector())
                    .ToImmutableArray();

            //VectorList =
            //    VectorArrayList
            //        .Select(Float64Tuple.Create)
            //        .ToImmutableArray();

            ClarkeArray = 
                Float64ArrayUtils.CreateClarkeRotationArray(VSpaceDimensions);

            //ClarkeArrayRotation = 
            //    ClarkeRotation.CreateForward(VSpaceDimensions);

            ClarkeSimpleRotationSequence =
                LinFloat64MatrixRotation
                    .CreateForwardClarkeRotation(VSpaceDimensions)
                    .ToVectorToVectorRotationSequence()
                    .Select(t => 
                        new Triplet<double[]>(
                            t.SourceVector.ToArray(VSpaceDimensions),
                            t.TargetOrthogonalVector.ToArray(VSpaceDimensions),
                            t.TargetVector.ToArray(VSpaceDimensions)
                        )
                    ).ToImmutableArray();

            //ClarkeArray =
            //    ScalarAlgebraFloat64Processor
            //        .DefaultProcessor
            //        .CreateClarkeArray(VSpaceDimensions);

            //SkrRotateFunction = VSpaceDimensions switch
            //{
            //    3 => SkrMapUtils.SkrRotate3D,
            //    4 => SkrMapUtils.SkrRotate4D,
            //    5 => SkrMapUtils.SkrRotate5D,
            //    6 => SkrMapUtils.SkrRotate6D,
            //    7 => SkrMapUtils.SkrRotate7D,
            //    8 => SkrMapUtils.SkrRotate8D,
            //    9 => SkrMapUtils.SkrRotate9D,
            //    10 => SkrMapUtils.SkrRotate10D,
            //    11 => SkrMapUtils.SkrRotate11D,
            //    12 => SkrMapUtils.SkrRotate12D,
            //    13 => SkrMapUtils.SkrRotate13D,
            //    14 => SkrMapUtils.SkrRotate14D,
            //    15 => SkrMapUtils.SkrRotate15D,
            //    16 => SkrMapUtils.SkrRotate16D,
            //    17 => SkrMapUtils.SkrRotate17D,
            //    18 => SkrMapUtils.SkrRotate18D,
            //    19 => SkrMapUtils.SkrRotate19D,
            //    20 => SkrMapUtils.SkrRotate20D,
            //    21 => SkrMapUtils.SkrRotate21D,
            //    22 => SkrMapUtils.SkrRotate22D,
            //    23 => SkrMapUtils.SkrRotate23D,
            //    24 => SkrMapUtils.SkrRotate24D,
            //    25 => SkrMapUtils.SkrRotate25D,
            //    26 => SkrMapUtils.SkrRotate26D,
            //    27 => SkrMapUtils.SkrRotate27D,
            //    28 => SkrMapUtils.SkrRotate28D,
            //    29 => SkrMapUtils.SkrRotate29D,
            //    30 => SkrMapUtils.SkrRotate30D,
            //    31 => SkrMapUtils.SkrRotate31D,
            //    32 => SkrMapUtils.SkrRotate32D,
            //    33 => SkrMapUtils.SkrRotate33D,
            //    34 => SkrMapUtils.SkrRotate34D,
            //    35 => SkrMapUtils.SkrRotate35D,
            //    36 => SkrMapUtils.SkrRotate36D,
            //    37 => SkrMapUtils.SkrRotate37D,
            //    38 => SkrMapUtils.SkrRotate38D,
            //    39 => SkrMapUtils.SkrRotate39D,
            //    40 => SkrMapUtils.SkrRotate40D,
            //    41 => SkrMapUtils.SkrRotate41D,
            //    42 => SkrMapUtils.SkrRotate42D,
            //    43 => SkrMapUtils.SkrRotate43D,
            //    44 => SkrMapUtils.SkrRotate44D,
            //    45 => SkrMapUtils.SkrRotate45D,
            //    46 => SkrMapUtils.SkrRotate46D,
            //    47 => SkrMapUtils.SkrRotate47D,
            //    48 => SkrMapUtils.SkrRotate48D,
            //    _ => throw new InvalidOperationException()
            //};

            //ClarkeRotateFunction = VSpaceDimensions switch
            //{
            //    3 => ClarkeMapUtils.ClarkeRotate3D,
            //    4 => ClarkeMapUtils.ClarkeRotate4D,
            //    5 => ClarkeMapUtils.ClarkeRotate5D,
            //    6 => ClarkeMapUtils.ClarkeRotate6D,
            //    7 => ClarkeMapUtils.ClarkeRotate7D,
            //    8 => ClarkeMapUtils.ClarkeRotate8D,
            //    9 => ClarkeMapUtils.ClarkeRotate9D,
            //    10 => ClarkeMapUtils.ClarkeRotate10D,
            //    11 => ClarkeMapUtils.ClarkeRotate11D,
            //    12 => ClarkeMapUtils.ClarkeRotate12D,
            //    13 => ClarkeMapUtils.ClarkeRotate13D,
            //    14 => ClarkeMapUtils.ClarkeRotate14D,
            //    15 => ClarkeMapUtils.ClarkeRotate15D,
            //    16 => ClarkeMapUtils.ClarkeRotate16D,
            //    17 => ClarkeMapUtils.ClarkeRotate17D,
            //    18 => ClarkeMapUtils.ClarkeRotate18D,
            //    19 => ClarkeMapUtils.ClarkeRotate19D,
            //    20 => ClarkeMapUtils.ClarkeRotate20D,
            //    21 => ClarkeMapUtils.ClarkeRotate21D,
            //    22 => ClarkeMapUtils.ClarkeRotate22D,
            //    23 => ClarkeMapUtils.ClarkeRotate23D,
            //    24 => ClarkeMapUtils.ClarkeRotate24D,
            //    25 => ClarkeMapUtils.ClarkeRotate25D,
            //    26 => ClarkeMapUtils.ClarkeRotate26D,
            //    27 => ClarkeMapUtils.ClarkeRotate27D,
            //    28 => ClarkeMapUtils.ClarkeRotate28D,
            //    29 => ClarkeMapUtils.ClarkeRotate29D,
            //    30 => ClarkeMapUtils.ClarkeRotate30D,
            //    31 => ClarkeMapUtils.ClarkeRotate31D,
            //    32 => ClarkeMapUtils.ClarkeRotate32D,
            //    33 => ClarkeMapUtils.ClarkeRotate33D,
            //    34 => ClarkeMapUtils.ClarkeRotate34D,
            //    35 => ClarkeMapUtils.ClarkeRotate35D,
            //    36 => ClarkeMapUtils.ClarkeRotate36D,
            //    37 => ClarkeMapUtils.ClarkeRotate37D,
            //    38 => ClarkeMapUtils.ClarkeRotate38D,
            //    39 => ClarkeMapUtils.ClarkeRotate39D,
            //    40 => ClarkeMapUtils.ClarkeRotate40D,
            //    41 => ClarkeMapUtils.ClarkeRotate41D,
            //    42 => ClarkeMapUtils.ClarkeRotate42D,
            //    43 => ClarkeMapUtils.ClarkeRotate43D,
            //    44 => ClarkeMapUtils.ClarkeRotate44D,
            //    45 => ClarkeMapUtils.ClarkeRotate45D,
            //    46 => ClarkeMapUtils.ClarkeRotate46D,
            //    47 => ClarkeMapUtils.ClarkeRotate47D,
            //    48 => ClarkeMapUtils.ClarkeRotate48D,
            //    _ => throw new InvalidOperationException()
            //};
        }

        public double[] GetRandomVector()
        {
            var random = new Random(10);

            return Enumerable
                .Range(0, VSpaceDimensions)
                .Select(_ => random.NextDouble())
                .ToArray();
        }

        //public double[] SkrRotate3D(double[] uVector)
        //{
        //    const int n = 3;

        //    var nSqrt = Math.Sqrt(n);
        //    var vVector = new double[n];

        //    var a =
        //        uVector[0] +
        //        uVector[1];
        //    a /= 1d + nSqrt;

        //    var un = uVector[n - 1];
        //    var k = (un - a) / nSqrt;
        //    var m = un + a;

        //    vVector[0] = uVector[0] + k;
        //    vVector[1] = uVector[1] + k;
        //    vVector[2] = uVector[2] + k - m;

        //    return vVector;
        //}

        //public double[] SkrRotate4D(double[] uVector)
        //{
        //    const int n = 4;

        //    var nSqrt = Math.Sqrt(n);
        //    var vVector = new double[n];

        //    var a =
        //        uVector[0] +
        //        uVector[1] +
        //        uVector[2];
        //    a /= 1d + nSqrt;

        //    var un = uVector[n - 1];
        //    var k = (un - a) / nSqrt;
        //    var m = un + a;

        //    vVector[0] = uVector[0] + k;
        //    vVector[1] = uVector[1] + k;
        //    vVector[2] = uVector[2] + k;
        //    vVector[3] = uVector[3] + k - m;

        //    return vVector;
        //}

        //public double[] SkrRotate24D(double[] uVector)
        //{
        //    // This is 12.5% faster than the general SKR function.
        //    const int n = 24;

        //    var nSqrt = Math.Sqrt(n);
        //    var vVector = new double[n];

        //    var a =
        //        uVector[0] +
        //        uVector[1] +
        //        uVector[2] +
        //        uVector[3] +
        //        uVector[4] +
        //        uVector[5] +
        //        uVector[6] +
        //        uVector[7] +
        //        uVector[8] +
        //        uVector[9] +
        //        uVector[10] +
        //        uVector[11] +
        //        uVector[12] +
        //        uVector[13] +
        //        uVector[14] +
        //        uVector[15] +
        //        uVector[16] +
        //        uVector[17] +
        //        uVector[18] +
        //        uVector[19] +
        //        uVector[20] +
        //        uVector[21] +
        //        uVector[22];
        //    a /= 1d + nSqrt;

        //    var un = uVector[n - 1];
        //    var k = (un - a) / nSqrt;
        //    var m = un + a;

        //    vVector[0] = uVector[0] + k;
        //    vVector[1] = uVector[1] + k;
        //    vVector[2] = uVector[2] + k;
        //    vVector[3] = uVector[3] + k;
        //    vVector[4] = uVector[4] + k;
        //    vVector[5] = uVector[5] + k;
        //    vVector[6] = uVector[6] + k;
        //    vVector[7] = uVector[7] + k;
        //    vVector[8] = uVector[8] + k;
        //    vVector[9] = uVector[9] + k;
        //    vVector[10] = uVector[10] + k;
        //    vVector[11] = uVector[11] + k;
        //    vVector[12] = uVector[12] + k;
        //    vVector[13] = uVector[13] + k;
        //    vVector[14] = uVector[14] + k;
        //    vVector[15] = uVector[15] + k;
        //    vVector[16] = uVector[16] + k;
        //    vVector[17] = uVector[17] + k;
        //    vVector[18] = uVector[18] + k;
        //    vVector[19] = uVector[19] + k;
        //    vVector[20] = uVector[20] + k;
        //    vVector[21] = uVector[21] + k;
        //    vVector[22] = uVector[22] + k;
        //    vVector[23] = uVector[23] + k - m;

        //    return vVector;
        //}
        
        public double[] ClarkeRotate1(double[] uVector)
        {
            return ClarkeArray.MatrixProduct(uVector);
        }

        //public Float64Tuple ClarkeRotate1(Float64Tuple uVector)
        //{
        //    return ClarkeArrayRotation.MapVector(uVector);

        //    //return ClarkeArray.MatrixProduct(uVector);
        //}

        public double[] ClarkeRotate2(double[] uVector)
        {
            var vVector = new double[VSpaceDimensions];

            uVector.CopyTo(vVector, 0);

            foreach (var (u, t, v) in ClarkeSimpleRotationSequence)
            {
                //var r = vector.VectorDot(TargetOrthogonalVector);
                //var s = vector.VectorDot(SourceVector);

                //return vector - (r + s) * SourceVector - (r - s) * TargetVector;

                var r = vVector.VectorDot(t);
                var s = vVector.VectorDot(u);
                var rsPlus = r + s;
                var rsMinus = r - s;

                for (var i = 0; i < VSpaceDimensions; i++)
                    vVector[i] -= rsPlus * u[i] + rsMinus * v[i];
            }

            return vVector;
        }
        
        [Benchmark]
        public IReadOnlyList<double[]> ClarkeMatrixProduct()
        {
            return VectorArrayList.Select(ClarkeRotate1).ToImmutableArray();
        }
        
        [Benchmark]
        public IReadOnlyList<double[]> ClarkeSimpleRotors()
        {
            return VectorArrayList.Select(ClarkeRotate2).ToImmutableArray();

            //return VectorList.Select(SkrMapUtils.SkrRotate).ToImmutableArray();
        }

        public void Validate2()
        {
            var m1 = Float64ArrayUtils.CreateClarkeRotationArray(VSpaceDimensions);
            var m2 = LinFloat64MatrixRotation.CreateForwardClarkeRotation(VSpaceDimensions).ToVectorToVectorRotationSequence();

            for (var i = 0; i < VectorArrayList.Count; i++)
            {
                var u = VectorArrayList[i];
                var v1 = m1.MatrixProduct(u).CreateLinVector();
                var v2 = m2.MapVector(u.CreateLinVector());

                var diff = (v1 - v2).GetVectorNormSquared();

                if (!diff.IsNearZero())
                    throw new InvalidOperationException();
            }
        }

        public void Validate()
        {
            var vList1 = ClarkeMatrixProduct().Select(Float64Tuple.Create).ToImmutableArray();
            var vList2 = ClarkeSimpleRotors().Select(Float64Tuple.Create).ToImmutableArray();

            for (var i = 0; i < vList1.Length; i++)
            {
                var diff = (vList1[i] - vList2[i]).GetVectorNormSquared();

                if (!diff.IsNearZero())
                    throw new InvalidOperationException();
            }
        }

        //[Benchmark]
        //public IReadOnlyList<double[]> SkrGenerated()
        //{
        //    return VectorList.Select(SkrRotateFunction).ToImmutableArray();
        //}
        
        //[Benchmark]
        //public IReadOnlyList<double[]> ClarkeGenerated()
        //{
        //    return VectorList.Select(ClarkeRotateFunction).ToImmutableArray();
        //}


    }
}
