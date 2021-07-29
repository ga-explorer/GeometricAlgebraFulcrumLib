using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.FactoredBlade;
using GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.FrameUtils;
using GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.KVector;
using GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.Multivector;
using GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.Outermorphism;
using GeometricAlgebraFulcrumLib.CodeComposer.Composers;
using GeometricAlgebraFulcrumLib.CodeComposer.Languages;
using GeometricAlgebraFulcrumLib.Processing;
using GeometricAlgebraFulcrumLib.Processing.Generic;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Storage;
using TextComposerLib.Loggers.Progress;
using TextComposerLib.Text.Parametric;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib
{
    public sealed partial class GaLibraryComposer 
        : GaCodeLibraryComposerBase, IGaSpace
    {
        /// <summary>
        /// Generate for a single signature
        /// </summary>
        /// <param name="rootNamespace"></param>
        /// <param name="processor"></param>
        /// <param name="generateSymbolicContextCode"></param>
        /// <returns></returns>
        public static GaLibraryComposer Generate(string rootNamespace, IGaProcessor<ISymbolicExpressionAtomic> processor, bool generateSymbolicContextCode = true)
        {
            var libGen =
                new GaLibraryComposer(
                    rootNamespace, 
                    processor
                );

            libGen.DefaultContextOptions.AllowGenerateCode = 
                generateSymbolicContextCode;

            libGen.Generate();

            return libGen;
        }


        internal int MaxTargetLocalVars 
            => 1024;

        internal IGaProcessor<ISymbolicExpressionAtomic> Processor { get; }

        internal IGaProcessor<ISymbolicExpressionAtomic> EuclideanProcessor { get; }

        public uint VSpaceDimension 
            => Processor.VSpaceDimension;

        public ulong GaSpaceDimension 
            => 1UL << (int) VSpaceDimension;

        public ulong MaxBasisBladeId 
            => (1UL << (int) VSpaceDimension) - 1UL;

        public uint GradesCount 
            => VSpaceDimension + 1;

        public IEnumerable<uint> Grades 
            => GradesCount.GetRange();

        internal string RootNamespace { get; }

        internal string CurrentNamespace { get; private set; }
        

        public override string Name 
            => "Graded Multivectors Library Composer";

        public override string Description 
            => "Generate multiple files holding a library for processing multivectors of a given GA signature. A Multivector is represented using zero or more non-zero k-vectors";


        private GaLibraryComposer(string rootNamespace, IGaProcessor<ISymbolicExpressionAtomic> processor)
            : base(GaLanguageServer.CSharpWithMathematica())
        {
            RootNamespace = rootNamespace;
            Processor = processor;
            EuclideanProcessor =
                processor.IsOrthonormal && processor.Signature.IsEuclidean
                    ? processor
                    : processor.CreateEuclideanProcessor(
                        processor.VSpaceDimension
                    );
        }

        
        internal void SetBasisBladeToArrayNaming(SymbolicContext context, IGasKVector<ISymbolicExpressionAtomic> kVector, string arrayVarName)
        {
            context.SetExternalNamesByTermIndex(
                kVector,
                index => $"{arrayVarName}[{index}]"
            );
        }


        private void GenerateKVectorClassFile()
        {
            CodeFilesComposer.InitalizeFile(CurrentNamespace + "kVector.cs");

            var fileGen = new OutermorphismClassFileComposer(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();

            //Note: You can use this method instead to finalize, save, free memory, 
            //and unselect the active file composer in one step during code generation 
            //when the you are done with the file:
            //CodeFilesComposer.SaveActiveFile();
        }

        private void GenerateKVectorStaticFile()
        {
            CodeFilesComposer.InitalizeFile(CurrentNamespace + "kVectorStatic.cs");

            var fileGen = new StaticCodeFileComposer(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateKVectorIsZeroMethodsFile()
        {
            CodeFilesComposer.InitalizeFile("IsZero.cs");

            var fileGen = new IsZeroMethodsFileComposer(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateKVectorEqualsMethodsFile()
        {
            CodeFilesComposer.InitalizeFile("Equals.cs");

            var fileGen = new EqualsMethodsFileComposer(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateKVectorInvolutionMethodsFile()
        {
            CodeFilesComposer.InitalizeFile("Involutions.cs");

            var fileGen = new InvolutionMethodsFileComposer(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateKVectorNormMethodsFile()
        {
            CodeFilesComposer.InitalizeFile("Norms.cs");

            var fileGen = new NormMethodsFileComposer(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

//        private void GenerateKVectorSdfMethodsFile()
//        {
//            //Make sure the PointToMultivector3D macro is defined inside the signature
//            var pointToMultivectorSymbolicContext =
//                Processor.SymbolicContexts.FirstOrDefault(
//                    m => m.Name == "PointToMultivector3D"
//                );

//            if (ReferenceEquals(pointToMultivectorSymbolicContext, null))
//                return;

//            var sdfRayStepResultStruct = AddGaClcStructure(
//                "structure SdfRayStepResult (sdf0 : scalar, sdf1 : scalar)"
//            );

//            var sdfNormalResultStruct = AddGaClcStructure(
//                "structure SdfNormalResult (d1 : scalar, d2 : scalar, d3 : scalar, d4 : scalar)"
//            );

//            var sdfOpnsSymbolicContextCode = @"
//macro #signature#.SdfOpns(mv : Multivector, x : scalar, y : scalar, z : scalar) : scalar
//begin
//    let mv1 = PointToMultivector3D(x, y, z) op mv
    
//    return mv1 sp reverse(mv1)
//end
//".Replace("#signature#", CurrentNamespace);

//            var sdfIpnsSymbolicContextCode = @"
//macro #signature#.SdfIpns(mv : Multivector, x : scalar, y : scalar, z : scalar) : scalar
//begin
//    let mv1 = PointToMultivector3D(x, y, z) lcp mv
    
//    return mv1 sp reverse(mv1)
//end
//".Replace("#signature#", CurrentNamespace);

//            CodeFilesComposer.InitalizeFile("ScalarDistanceFunctions.cs");

//            var fileGen = new SdfMethodsFileComposer(this);

//            fileGen.Generate();

//            CodeFilesComposer.UnselectActiveFile();
//        }

        private void GenerateKVectorMiscMethodsFile()
        {
            CodeFilesComposer.InitalizeFile("Misc.cs");

            var fileGen = new MiscMethodsFileComposer(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }
        

        internal void GenerateBilinearProductMethodFile(GaLanguageOperationSpecs opSpecs, uint inGrade1, uint inGrade2)
        {
            CodeFilesComposer.DownFolder(opSpecs.GetName());

            var (isValid, outGrade) = opSpecs.GetKVectorsBilinearProductGrade(
                    VSpaceDimension,
                    inGrade1,
                    inGrade2
                );

            if (!isValid)
                throw new InvalidOperationException();

            var methodName = opSpecs.GetName(
                inGrade1, inGrade2, outGrade
            );

            CodeFilesComposer.InitalizeFile(methodName + ".cs");

            var fileGen =
                new BilinearProductMethodFileComposer(
                    this,
                    opSpecs,
                    inGrade1,
                    inGrade2,
                    outGrade
                );

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();

            CodeFilesComposer.UpFolder();
        }

        internal void GenerateBilinearProductMethodFile(GaLanguageOperationSpecs opSpecs, uint inGrade1, uint inGrade2, uint outGrade)
        {
            CodeFilesComposer.DownFolder(opSpecs.GetName());

            var methodName = opSpecs.GetName(
                inGrade1, inGrade2, outGrade
            );

            CodeFilesComposer.InitalizeFile(methodName + ".cs");

            var fileGen =
                new BilinearProductMethodFileComposer(
                    this,
                    opSpecs,
                    inGrade1,
                    inGrade2,
                    outGrade
                );

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();

            CodeFilesComposer.UpFolder();
        }

        internal void GenerateScalarProductMethodFile(GaLanguageOperationSpecs opSpecs, uint inGrade)
        {
            CodeFilesComposer.DownFolder(opSpecs.GetName());
            
            var methodName = opSpecs.GetName(
                inGrade, inGrade
            );

            CodeFilesComposer.InitalizeFile(methodName + ".cs");

            var fileGen =
                new ScalarProductMethodFileComposer(
                    this,
                    opSpecs,
                    inGrade
                );

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();

            CodeFilesComposer.UpFolder();
        }

        private void GenerateBilinearProductMainMethodFile(GaLanguageOperationSpecs opSpecs, string zeroCondition, Func<uint, uint, uint> getFinalGrade, Func<uint, uint, bool> isLegalGrade)
        {
            CodeFilesComposer.InitalizeFile(opSpecs.GetName() + ".cs");

            var fileGen =
                new BilinearProductMainMethodFileComposer(
                    this,
                    opSpecs,
                    zeroCondition,
                    getFinalGrade,
                    isLegalGrade
                );

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateVectorsOuterProductFile()
        {
            CodeFilesComposer.InitalizeFile("VectorsOP.cs");

            var fileGen = new VectorsOpMethodsFileComposer(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateBilinearProductFiles()
        {
            var opSpecsArray = new[]
            {
                GaLanguageOperationKind.BinaryOuterProduct.CreateEuclideanOperationSpecs(),
                
                GaLanguageOperationKind.BinaryLeftContractionProduct.CreateEuclideanOperationSpecs(),
                GaLanguageOperationKind.BinaryLeftContractionProduct.CreateMetricOperationSpecs(),

                GaLanguageOperationKind.BinaryRightContractionProduct.CreateEuclideanOperationSpecs(),
                GaLanguageOperationKind.BinaryRightContractionProduct.CreateMetricOperationSpecs(),

                GaLanguageOperationKind.BinaryFatDotProduct.CreateEuclideanOperationSpecs(),
                GaLanguageOperationKind.BinaryFatDotProduct.CreateMetricOperationSpecs(),

                GaLanguageOperationKind.BinaryHestenesInnerProduct.CreateEuclideanOperationSpecs(),
                GaLanguageOperationKind.BinaryHestenesInnerProduct.CreateMetricOperationSpecs(),
            };
                

            foreach (var opSpecs in opSpecsArray)
            {
                foreach (var inGrade1 in Processor.Grades)
                {
                    foreach (var inGrade2 in Processor.Grades)
                    {
                        var (isValid, outGrade) = opSpecs.GetKVectorsBilinearProductGrade(
                            VSpaceDimension,
                            inGrade1,
                            inGrade2
                        );

                        if (!isValid)
                            continue;

                        GenerateBilinearProductMethodFile(
                            opSpecs,
                            inGrade1,
                            inGrade2
                        );
                    }
                }
            }

            GenerateBilinearProductMainMethodFile(
                opSpecsArray[0],
                "Grade + blade2.Grade > " + VSpaceDimension,
                (g1, g2) => g1 + g2,
                (g1, g2) => g1 + g2 <= VSpaceDimension
            );

            GenerateVectorsOuterProductFile();

            GenerateBilinearProductMainMethodFile(
                opSpecsArray[1],
                "Grade > blade2.Grade",
                (g1, g2) => g2 - g1,
                (g1, g2) => g1 <= g2
            );
            
            GenerateBilinearProductMainMethodFile(
                opSpecsArray[2],
                "Grade > blade2.Grade",
                (g1, g2) => g2 - g1,
                (g1, g2) => g1 <= g2
            );
            
            GenerateBilinearProductMainMethodFile(
                opSpecsArray[3],
                "Grade < blade2.Grade",
                (g1, g2) => g1 - g2,
                (g1, g2) => g1 >= g2
            );
            
            GenerateBilinearProductMainMethodFile(
                opSpecsArray[4],
                "Grade < blade2.Grade",
                (g1, g2) => g1 - g2,
                (g1, g2) => g1 >= g2
            );
            
            GenerateBilinearProductMainMethodFile(
                opSpecsArray[5],
                "Grade != blade2.Grade",
                (g1, g2) => (uint) Math.Abs(g1 - g2),
                (g1, g2) => g1 != g2
            );
            
            GenerateBilinearProductMainMethodFile(
                opSpecsArray[6],
                "Grade != blade2.Grade",
                (g1, g2) => (uint) Math.Abs(g1 - g2),
                (g1, g2) => g1 != g2
            );
            
            GenerateBilinearProductMainMethodFile(
                opSpecsArray[7],
                "Grade != blade2.Grade && Grade != 0 && blade2.Grade != 0",
                (g1, g2) => (uint) Math.Abs(g1 - g2),
                (g1, g2) => g1 != g2 && g1 != 0 && g2 != 0
            );
            
            GenerateBilinearProductMainMethodFile(
                opSpecsArray[8],
                "Grade != blade2.Grade && Grade != 0 && blade2.Grade != 0",
                (g1, g2) => (uint) Math.Abs(g1 - g2),
                (g1, g2) => g1 != g2 && g1 != 0 && g2 != 0
            );
        }
        
        private void GenerateScalarProductFiles()
        {
            var opSpecsArray = new []
            {
                GaLanguageOperationKind.BinaryScalarProduct.CreateMetricOperationSpecs(),
                GaLanguageOperationKind.BinaryScalarProduct.CreateEuclideanOperationSpecs()
            };

            foreach (var opSpecs in opSpecsArray)
            {
                foreach (var inGrade in Processor.Grades)
                {
                    GenerateScalarProductMethodFile(
                        opSpecs,
                        inGrade
                    );
                }

                GenerateBilinearProductMainMethodFile(
                    opSpecs,
                    "Grade != blade2.Grade",
                    (g1, g2) => g1 - g2,
                    (g1, g2) => g1 == g2
                );
            }
        }
        

        private void GenerateGeometricProductFiles()
        {
            var opSpecs = 
                GaLanguageOperationKind.BinaryGeometricProduct.CreateMetricOperationSpecs();

            var codeGen = new BinaryGpFilesComposer(this, opSpecs, false);

            codeGen.Generate();
        }

        private void GenerateEuclideanGeometricProductFiles()
        {
            var opSpecs = 
                GaLanguageOperationKind.BinaryGeometricProduct.CreateEuclideanOperationSpecs();

            var codeGen = new BinaryGpFilesComposer(this, opSpecs, false);

            codeGen.Generate();
        }

        private void GenerateEuclideanGeometricProductDualFiles()
        {
            var opSpecs = GaLanguageOperationKind.BinaryGeometricProductDual.CreateEuclideanOperationSpecs();

            var codeGen = new BinaryGpFilesComposer(this, opSpecs, true);

            codeGen.Generate();
        }

        private void GenerateDeltaProductFile()
        {
            var opSpecs = 
                GaLanguageOperationKind
                    .BinaryDeltaProduct
                    .CreateMetricOperationSpecs();

            CodeFilesComposer.InitalizeFile(opSpecs + ".cs");

            var codeGen = new DpMethodsFileComposer(this, opSpecs);

            codeGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateDeltaProductDualFile()
        {
            var opSpecs = 
                GaLanguageOperationKind
                    .BinaryDeltaProductDual
                    .CreateMetricOperationSpecs();

            CodeFilesComposer.InitalizeFile(opSpecs + ".cs");

            var codeGen = new DpDualMethodsFileComposer(this, opSpecs);

            codeGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateUnaryEuclideanGeometricProductFiles()
        {
            var opSpecsArray = new[]
            {
                GaLanguageOperationKind.UnaryGeometricProductSquared.CreateMetricOperationSpecs(),
                GaLanguageOperationKind.UnaryGeometricProductReverse.CreateMetricOperationSpecs(),
                GaLanguageOperationKind.UnaryGeometricProductSquared.CreateEuclideanOperationSpecs(),
                GaLanguageOperationKind.UnaryGeometricProductReverse.CreateEuclideanOperationSpecs()
            };
                
            foreach (var opSpecs in opSpecsArray)
            {
                CodeFilesComposer.InitalizeFile(opSpecs + ".cs");

                var codeGen = new UnaryGpMethodsFileComposer(this, opSpecs);

                codeGen.Generate();

                CodeFilesComposer.UnselectActiveFile();
            }
        }

        private void GenerateApplyVersorMainMethod(GaLanguageOperationSpecs opSpecs)
        {
            CodeFilesComposer.InitalizeFile(opSpecs.GetName() + ".cs");

            var fileGen = new ApplyVersorMainMethodFileComposer(this, opSpecs);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateApplyVersorFiles()
        {
            var opSpecsArray = new[]
            {
                GaLanguageOperationKind.BinaryProject.CreateEuclideanOperationSpecs(),
                GaLanguageOperationKind.BinaryReflect.CreateEuclideanOperationSpecs(),
                GaLanguageOperationKind.BinaryComplement.CreateEuclideanOperationSpecs(),
                GaLanguageOperationKind.BinaryRotate.CreateEuclideanOperationSpecs(),
                GaLanguageOperationKind.BinaryProject.CreateMetricOperationSpecs(),
                GaLanguageOperationKind.BinaryReflect.CreateMetricOperationSpecs(),
                GaLanguageOperationKind.BinaryComplement.CreateMetricOperationSpecs(),
                GaLanguageOperationKind.BinaryRotate.CreateMetricOperationSpecs(),
            };

            foreach (var opSpecs in opSpecsArray)
            {
                foreach (var inGrade1 in Processor.Grades)
                {
                    if (inGrade1.IsOdd() && opSpecs.OperationKind == GaLanguageOperationKind.BinaryRotate)
                        continue;

                    foreach (var inGrade2 in Processor.Grades)
                    {
                        var methodName =
                            opSpecs.GetName(inGrade1, inGrade2, inGrade2);

                        CodeFilesComposer.DownFolder(opSpecs.GetName());

                        CodeFilesComposer.InitalizeFile(methodName + ".cs");

                        var fileGen = new ApplyVersorMethodFileComposer(
                            this,
                            opSpecs,
                            inGrade1,
                            inGrade2
                        );

                        fileGen.Generate();

                        CodeFilesComposer.UnselectActiveFile();

                        CodeFilesComposer.UpFolder();
                    }
                }

                GenerateApplyVersorMainMethod(opSpecs);
            }
        }

        private void GenerateFactorMethod(uint inGrade, ulong inId)
        {
            CodeFilesComposer.DownFolder("Factor");

            CodeFilesComposer.InitalizeFile("Factor" + inId + ".cs");

            var fileGen = new FactorMethodFileComposer(this, inGrade, inId);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();

            CodeFilesComposer.UpFolder();
        }

        private void GenerateFactorFiles()
        {
            CodeFilesComposer.InitalizeFile("Factor.cs");

            var fileGen = new FactorMainMethodsFileComposer(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();

            for (var inGrade = 2U; inGrade <= VSpaceDimension; inGrade++)
            {
                var kvSpaceDimension = 
                    Processor.KvSpaceDimension(inGrade);

                for (var inIndex = 0UL; inIndex < kvSpaceDimension; inIndex++)
                {
                    var inId = Processor.BasisBladeId(inGrade, inIndex);

                    GenerateFactorMethod(inGrade, inId);
                }
            }

        }

        //TODO: Meet and join algorithm without factorization
        //Given blades A and B of grades gA and gB
        //
        //EGPDual_gAgBgX is given by ((A gp B) lcp reverse(I)).@GgX@
        //
        //1) find grade of C = (A dp B) call it gC. Use DPGrade() function
        //2) compute the blade D = EGPDual_gAgBgD(A, B) with grade gD = n - gC
        //that is, D = dual(A dp B)
        //3) find grade of S = D dp A call it gS
        //4) compute the blade S = EGP_gDgAgS(S, A) with grade gS
        //that is, S = dual(A dp B) dp A
        //5) Meet = S lcp B
        //6) Join = S ^ B

        private void GenerateKVectorFiles()
        {
            if (Progress != null)
                Progress.Enabled = true;

            var progressId = this.ReportStart(
                "Generating KVector class code files"
                );

            GenerateKVectorClassFile();

            GenerateKVectorStaticFile();

            CodeFilesComposer.DownFolder("KVectors");

            GenerateKVectorIsZeroMethodsFile();

            GenerateKVectorEqualsMethodsFile();

            GenerateKVectorInvolutionMethodsFile();

            GenerateKVectorNormMethodsFile();

            //GenerateKVectorSdfMethodsFile();

            GenerateKVectorMiscMethodsFile();


            GenerateBilinearProductFiles();
            
            GenerateScalarProductFiles();


            GenerateGeometricProductFiles();

            GenerateEuclideanGeometricProductFiles();

            GenerateEuclideanGeometricProductDualFiles();


            GenerateDeltaProductFile();

            GenerateDeltaProductDualFile();

            GenerateUnaryEuclideanGeometricProductFiles();

            //GenerateApplyVersorFiles();

            GenerateFactorFiles();

            //TODO: Create blade subroutines to reflect blades in other blades:
            /*
                If A, B are blades, we can define several blade-redlection GA relations (table 7.1):

                //This relation does not take orientation of resulting blade into consideration
                A.ReflectInBlade(B) => B gp A gp reverse(B)/norm2(B)

                //These take the orientation of the result into consideration
                A.DirectReflectInDirectBlade(B) => -1 ^ (grade(A) * [grade(B) + 1]) * A.ReflectInBlade(B)
                A.DirectReflectInDualBlade(B) => -1 ^ (grade(A) * grade(B)) * A.ReflectInBlade(B)
                A.DualReflectInDirectBlade(B) => -1 ^ ([n - 1] * [grade(A) + 1] * [grade(B) + 1]) * A.ReflectInBlade(B)
                A.DualReflectInDualBlade(B) => -1 ^ ([grade(A) + 1] * grade(B)) * A.ReflectInBlade(B)
             */

            CodeFilesComposer.UpFolder();

            if (Progress != null)
                Progress.Enabled = true;

            this.ReportFinish(progressId);
        }


        private void GenerateFrameUtilsClassFile()
        {
            CodeFilesComposer.InitalizeFile(CurrentNamespace + "Utils.cs");

            var fileGen = new FrameUtilsClassFileComposer(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateFrameUtilsFiles()
        {
            if (Progress != null)
                Progress.Enabled = true;

            var progressId = this.ReportStart(
                "Generating Utils class code files"
            );

            GenerateFrameUtilsClassFile();

            CodeFilesComposer.DownFolder("FrameUtils");

            CodeFilesComposer.UpFolder();

            if (Progress != null)
                Progress.Enabled = true;

            this.ReportFinish(progressId);
        }


        private void GenerateMultivectorClassFile()
        {
            CodeFilesComposer.InitalizeFile(CurrentNamespace + "Multivector.cs");

            var fileGen = new MultivectorClassFileComposer(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateMultivectorFiles()
        {
            if (Progress != null)
                Progress.Enabled = true;

            var progressId = this.ReportStart(
                "Generating Multivector class code files"
            );

            GenerateMultivectorClassFile();

            CodeFilesComposer.DownFolder("Multivectors");

            CodeFilesComposer.UpFolder();

            if (Progress != null)
                Progress.Enabled = true;

            this.ReportFinish(progressId);
        }


        private void GenerateVectorClassFile()
        {
            CodeFilesComposer.InitalizeFile(CurrentNamespace + "Vector.cs");

            var fileGen = new VectorClass.VectorClassFileComposer(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateVectorFiles()
        {
            var progressId = this.ReportStart(
                "Generating Vector class code files"
                );

            GenerateVectorClassFile();

            CodeFilesComposer.DownFolder("Vectors");

            CodeFilesComposer.UpFolder();

            this.ReportFinish(progressId);
        }


        private void GenerateFactoredBladeClassFile()
        {
            CodeFilesComposer.InitalizeFile(CurrentNamespace + "FactoredBlade.cs");

            var fileGen = new FactoredBladeClassFileComposer(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateFactoredBladeFiles()
        {
            var progressId = this.ReportStart(
                "Generating FactoredBlade class code files"
                );

            GenerateFactoredBladeClassFile();

            CodeFilesComposer.DownFolder("FactoredBlades");

            CodeFilesComposer.UpFolder();

            this.ReportFinish(progressId);
        }


        private void GenerateOutermorphismClassFile()
        {
            CodeFilesComposer.InitalizeFile(CurrentNamespace + "Outermorphism.cs");

            var fileGen = new OutermorphismClassFileComposer(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateOutermorphismApplyMethodFile(uint inGrade)
        {
            CodeFilesComposer.InitalizeFile("Map_" + inGrade + ".cs");

            var fileGen = new MapMethodFileComposer(this, inGrade);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateOutermorphismFiles()
        {
            var progressId = this.ReportStart(
                "Generating Outermorphism class code files"
                );

            GenerateOutermorphismClassFile();

            CodeFilesComposer.DownFolder("Outermorphisms");

            CodeFilesComposer.DownFolder("Map");

            for (var inGrade = 1U; inGrade <= VSpaceDimension; inGrade++)
                GenerateOutermorphismApplyMethodFile(inGrade);

            CodeFilesComposer.UpFolder();

            CodeFilesComposer.UpFolder();

            this.ReportFinish(progressId);
        }

        private bool GeneratePreComputationsCode(GaSymbolicContextCodeComposer contextCodeComposer)
        {
            //Generate comments
            GaSymbolicContextCodeComposer.DefaultGenerateCommentsBeforeComputations(contextCodeComposer);

            //Temp variables declaration
            if (contextCodeComposer.Context.TargetTempVarsCount > MaxTargetLocalVars)
            {
                //Add array declaration code
                contextCodeComposer.SyntaxList.Add(
                    contextCodeComposer.GaLanguage.SyntaxFactory.DeclareLocalArray(
                        GaLanguage.ScalarTypeName,
                        "tempArray",
                        contextCodeComposer.Context.TargetTempVarsCount.ToString()
                        )
                    );

                contextCodeComposer.SyntaxList.AddEmptyLine();
            }
            else
            {
                var tempVarNames =
                    contextCodeComposer
                        .Context
                        .IntermediateVariables
                        .Select(item => item.ExternalName)
                        .Distinct();

                //Add temp variables declaration code
                foreach (var tempVarName in tempVarNames)
                    contextCodeComposer.SyntaxList.Add(
                        contextCodeComposer.GaLanguage.SyntaxFactory.DeclareLocalVariable(GaLanguage.ScalarTypeName, tempVarName)
                    );

                contextCodeComposer.SyntaxList.AddEmptyLine();
            }

            return true;
        }

        private void GeneratePostComputationsCode(GaSymbolicContextCodeComposer contextCodeComposer)
        {
            //Generate comments
            GaSymbolicContextCodeComposer.DefaultGenerateCommentsAfterComputations(contextCodeComposer);
        }


        protected override bool InitializeTemplates()
        {
            //Initialize templates used in code composition

            Templates.Parse(GaClcCodeTemplates);

            Templates.Parse(FrameUtilsTemplates);

            Templates.Parse(KVectorTemplates);

            Templates.Parse(MultivectorTemplates);

            Templates.Parse(VectorTemplates);

            Templates.Parse(OutermorphismTemplates);

            Templates.Parse(KVectorStaticTemplates);

            Templates.Parse(KVectorEqualsTemplates);

            Templates.Parse(KVectorIsZeroTemplates);

            Templates.Parse(KVectorInvolutionsTemplates);

            Templates.Parse(KVectorNormTemplates);

            Templates.Parse(KVectorSdfTemplates);

            Templates.Parse(KVectorMiscTemplates);

            Templates.Parse(KVectorBilinearProductsTemplates);

            Templates.Parse(KVectorFactorizationTemplates);

            //Template for encoding grade1 multivectors as variables by basis blade index
            Templates.Add("vmv", new ParametricTextComposer("#", "#", "#Var##index#"));

            return true;
        }

        protected override void InitializeSubComponents()
        {
            //Setup some components of the code library composer

            DefaultContextOptions.AllowGenerateCode = true;

            DefaultContextCodeComposerOptions.AllowGenerateComputationComments = 
                true;

            DefaultContextCodeComposerOptions.ActionBeforeGenerateComputations = 
                GeneratePreComputationsCode;

            DefaultContextCodeComposerOptions.ActionAfterGenerateComputations = 
                GeneratePostComputationsCode;
        }

        protected override void FinalizeSubComponents()
        {
            
        }
        
        protected override bool VerifyReadyToGenerate()
        {
            return true;
        }

        protected override void ComposeTextFiles()
        {
            if (Progress != null)
                Progress.Enabled = true;

            var progressId = this.ReportStart(
                $"Generating code files for namespace {RootNamespace}"
            );

            CurrentNamespace = RootNamespace;

            CodeFilesComposer.DownFolder(CurrentNamespace);


            GenerateFrameUtilsFiles();

            GenerateMultivectorFiles();

            GenerateKVectorFiles();

            GenerateApplyVersorFiles();

            GenerateVectorFiles();

            GenerateFactoredBladeFiles();

            GenerateOutermorphismFiles();


            CodeFilesComposer.UpFolder();

            if (Progress != null)
                Progress.Enabled = true;

            this.ReportFinish(progressId);
        }

        public override GaCodeLibraryComposerBase CreateEmptyComposer()
        {
            var libGen = new GaLibraryComposer(
                RootNamespace, 
                Processor
            );

            libGen.DefaultContextOptions.SetOptions(DefaultContextOptions);
            libGen.DefaultContextCodeComposerOptions.SetOptions(DefaultContextCodeComposerOptions);

            return libGen;
        }
    }
}
