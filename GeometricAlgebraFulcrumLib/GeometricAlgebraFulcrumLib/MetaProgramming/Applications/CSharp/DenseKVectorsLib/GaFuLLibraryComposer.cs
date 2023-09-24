using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.DenseKVectorsLib.FactoredBlade;
using GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.DenseKVectorsLib.FrameUtils;
using GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.DenseKVectorsLib.KVector;
using GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.DenseKVectorsLib.Multivector;
using GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.DenseKVectorsLib.Outermorphism;
using GeometricAlgebraFulcrumLib.MetaProgramming.Composers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Languages;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using TextComposerLib.Loggers.Progress;
using TextComposerLib.Text.Parametric;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.DenseKVectorsLib
{
    public sealed partial class GaFuLLibraryComposer 
        : GaFuLCodeLibraryComposerBase//, IGeometricAlgebraSpace
    {
        /// <summary>
        /// Generate for a single signature
        /// </summary>
        /// <param name="rootNamespace"></param>
        /// <param name="processor"></param>
        /// <param name="vSpaceDimensions"></param>
        /// <param name="generateMetaContextCode"></param>
        /// <returns></returns>
        public static GaFuLLibraryComposer Generate(string rootNamespace, XGaProcessor<IMetaExpressionAtomic> processor, int vSpaceDimensions, bool generateMetaContextCode = true)
        {
            var libGen =
                new GaFuLLibraryComposer(
                    rootNamespace, 
                    processor,
                    vSpaceDimensions
                );

            libGen.DefaultContextOptions.AllowGenerateCode = 
                generateMetaContextCode;

            libGen.Generate();

            return libGen;
        }


        internal int MaxTargetLocalVars 
            => 1024;

        internal XGaProcessor<IMetaExpressionAtomic> GeometricProcessor { get; }

        internal XGaProcessor<IMetaExpressionAtomic> EuclideanProcessor { get; }

        public int VSpaceDimensions { get; }
        
        public int GradesCount 
            => VSpaceDimensions + 1;

        public IEnumerable<int> Grades 
            => GradesCount.GetRange();

        internal string RootNamespace { get; }

        internal string CurrentNamespace { get; private set; }
        

        public override string Name 
            => "Graded Multivectors Library Composer";

        public override string Description 
            => "Generate multiple files holding a library for processing multivectors of a given GA signature. A Multivector is represented using zero or more non-zero k-vectors";


        private GaFuLLibraryComposer(string rootNamespace, XGaProcessor<IMetaExpressionAtomic> processor, int vSpaceDimensions)
            : base(GaFuLLanguageServerBase.CSharpFloat64())
        {
            RootNamespace = rootNamespace;
            VSpaceDimensions = vSpaceDimensions;
            GeometricProcessor = processor;
            EuclideanProcessor =
                processor.IsEuclidean
                    ? processor
                    : processor.ScalarProcessor.CreateEuclideanXGaProcessor();
        }

        
        internal void SetBasisBladeToArrayNaming(MetaContext context, XGaKVector<IMetaExpressionAtomic> kVector, string arrayVarName)
        {
            context.SetExternalNamesByTermIndex(
                kVector,
                index => $"{arrayVarName}[{index}]"
            );
        }


        private void GenerateKVectorClassFile()
        {
            CodeFilesComposer.InitializeFile(CurrentNamespace + "kVector.cs");

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
            CodeFilesComposer.InitializeFile(CurrentNamespace + "kVectorStatic.cs");

            var fileGen = new StaticCodeFileComposer(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateKVectorIsZeroMethodsFile()
        {
            CodeFilesComposer.InitializeFile("IsZero.cs");

            var fileGen = new IsZeroMethodsFileComposer(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateKVectorEqualsMethodsFile()
        {
            CodeFilesComposer.InitializeFile("Equals.cs");

            var fileGen = new EqualsMethodsFileComposer(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateKVectorInvolutionMethodsFile()
        {
            CodeFilesComposer.InitializeFile("Involutions.cs");

            var fileGen = new InvolutionMethodsFileComposer(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateKVectorNormMethodsFile()
        {
            CodeFilesComposer.InitializeFile("Norms.cs");

            var fileGen = new NormMethodsFileComposer(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

//        private void GenerateKVectorSdfMethodsFile()
//        {
//            //Make sure the PointToMultivector3D macro is defined inside the signature
//            var pointToMultivectorMetaContext =
//                GeometricProcessor.MetaContexts.FirstOrDefault(
//                    m => m.Name == "PointToMultivector3D"
//                );

//            if (ReferenceEquals(pointToMultivectorMetaContext, null))
//                return;

//            var sdfRayStepResultStruct = AddGeoClcStructure(
//                "structure SdfRayStepResult (sdf0 : scalar, sdf1 : scalar)"
//            );

//            var sdfNormalResultStruct = AddGeoClcStructure(
//                "structure SdfNormalResult (d1 : scalar, d2 : scalar, d3 : scalar, d4 : scalar)"
//            );

//            var sdfOpnsMetaContextCode = @"
//macro #signature#.SdfOpns(mv : Multivector, x : scalar, y : scalar, z : scalar) : scalar
//begin
//    let mv1 = PointToMultivector3D(x, y, z) op mv
    
//    return mv1 sp reverse(mv1)
//end
//".Replace("#signature#", CurrentNamespace);

//            var sdfIpnsMetaContextCode = @"
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
            CodeFilesComposer.InitializeFile("Misc.cs");

            var fileGen = new MiscMethodsFileComposer(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }
        

        internal void GenerateBilinearProductMethodFile(GaFuLLanguageOperationSpecs opSpecs, int inGrade1, int inGrade2)
        {
            CodeFilesComposer.DownFolder(opSpecs.GetName());

            var (isValid, outGrade) = opSpecs.GetKVectorsBilinearProductGrade(
                    VSpaceDimensions,
                    inGrade1,
                    inGrade2
                );

            if (!isValid)
                throw new InvalidOperationException();

            var methodName = opSpecs.GetName(
                inGrade1, inGrade2, outGrade
            );

            CodeFilesComposer.InitializeFile(methodName + ".cs");

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

        internal void GenerateBilinearProductMethodFile(GaFuLLanguageOperationSpecs opSpecs, int inGrade1, int inGrade2, int outGrade)
        {
            CodeFilesComposer.DownFolder(opSpecs.GetName());

            var methodName = opSpecs.GetName(
                inGrade1, inGrade2, outGrade
            );

            CodeFilesComposer.InitializeFile(methodName + ".cs");

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

        internal void GenerateScalarProductMethodFile(GaFuLLanguageOperationSpecs opSpecs, int inGrade)
        {
            CodeFilesComposer.DownFolder(opSpecs.GetName());
            
            var methodName = opSpecs.GetName(
                inGrade, inGrade
            );

            CodeFilesComposer.InitializeFile(methodName + ".cs");

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

        private void GenerateBilinearProductMainMethodFile(GaFuLLanguageOperationSpecs opSpecs, string zeroCondition, Func<int, int, int> getFinalGrade, Func<int, int, bool> isLegalGrade)
        {
            CodeFilesComposer.InitializeFile(opSpecs.GetName() + ".cs");

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
            CodeFilesComposer.InitializeFile("VectorsOP.cs");

            var fileGen = new VectorsOpMethodsFileComposer(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateBilinearProductFiles()
        {
            var opSpecsArray = new[]
            {
                GaFuLLanguageOperationKind.BinaryOuterProduct.CreateEuclideanOperationSpecs(),
                
                GaFuLLanguageOperationKind.BinaryLeftContractionProduct.CreateEuclideanOperationSpecs(),
                GaFuLLanguageOperationKind.BinaryLeftContractionProduct.CreateMetricOperationSpecs(),

                GaFuLLanguageOperationKind.BinaryRightContractionProduct.CreateEuclideanOperationSpecs(),
                GaFuLLanguageOperationKind.BinaryRightContractionProduct.CreateMetricOperationSpecs(),

                GaFuLLanguageOperationKind.BinaryFatDotProduct.CreateEuclideanOperationSpecs(),
                GaFuLLanguageOperationKind.BinaryFatDotProduct.CreateMetricOperationSpecs(),

                GaFuLLanguageOperationKind.BinaryHestenesInnerProduct.CreateEuclideanOperationSpecs(),
                GaFuLLanguageOperationKind.BinaryHestenesInnerProduct.CreateMetricOperationSpecs(),
            };
                

            foreach (var opSpecs in opSpecsArray)
            {
                foreach (var inGrade1 in Grades)
                {
                    foreach (var inGrade2 in Grades)
                    {
                        var (isValid, outGrade) = opSpecs.GetKVectorsBilinearProductGrade(
                            VSpaceDimensions,
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
                "Grade + blade2.Grade > " + VSpaceDimensions,
                (g1, g2) => g1 + g2,
                (g1, g2) => g1 + g2 <= VSpaceDimensions
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
                (g1, g2) => Math.Abs(g1 - g2),
                (g1, g2) => g1 != g2
            );
            
            GenerateBilinearProductMainMethodFile(
                opSpecsArray[6],
                "Grade != blade2.Grade",
                (g1, g2) => Math.Abs(g1 - g2),
                (g1, g2) => g1 != g2
            );
            
            GenerateBilinearProductMainMethodFile(
                opSpecsArray[7],
                "Grade != blade2.Grade && Grade != 0 && blade2.Grade != 0",
                (g1, g2) => Math.Abs(g1 - g2),
                (g1, g2) => g1 != g2 && g1 != 0 && g2 != 0
            );
            
            GenerateBilinearProductMainMethodFile(
                opSpecsArray[8],
                "Grade != blade2.Grade && Grade != 0 && blade2.Grade != 0",
                (g1, g2) => Math.Abs(g1 - g2),
                (g1, g2) => g1 != g2 && g1 != 0 && g2 != 0
            );
        }
        
        private void GenerateScalarProductFiles()
        {
            var opSpecsArray = new []
            {
                GaFuLLanguageOperationKind.BinaryScalarProduct.CreateMetricOperationSpecs(),
                GaFuLLanguageOperationKind.BinaryScalarProduct.CreateEuclideanOperationSpecs()
            };

            foreach (var opSpecs in opSpecsArray)
            {
                foreach (var inGrade in Grades)
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
                GaFuLLanguageOperationKind.BinaryGeometricProduct.CreateMetricOperationSpecs();

            var codeGen = new BinaryGpFilesComposer(this, opSpecs, false);

            codeGen.Generate();
        }

        private void GenerateEuclideanGeometricProductFiles()
        {
            var opSpecs = 
                GaFuLLanguageOperationKind.BinaryGeometricProduct.CreateEuclideanOperationSpecs();

            var codeGen = new BinaryGpFilesComposer(this, opSpecs, false);

            codeGen.Generate();
        }

        private void GenerateEuclideanGeometricProductDualFiles()
        {
            var opSpecs = GaFuLLanguageOperationKind.BinaryGeometricProductDual.CreateEuclideanOperationSpecs();

            var codeGen = new BinaryGpFilesComposer(this, opSpecs, true);

            codeGen.Generate();
        }

        private void GenerateDeltaProductFile()
        {
            var opSpecs = 
                GaFuLLanguageOperationKind
                    .BinaryDeltaProduct
                    .CreateMetricOperationSpecs();

            CodeFilesComposer.InitializeFile(opSpecs + ".cs");

            var codeGen = new DpMethodsFileComposer(this, opSpecs);

            codeGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateDeltaProductDualFile()
        {
            var opSpecs = 
                GaFuLLanguageOperationKind
                    .BinaryDeltaProductDual
                    .CreateMetricOperationSpecs();

            CodeFilesComposer.InitializeFile(opSpecs + ".cs");

            var codeGen = new DpDualMethodsFileComposer(this, opSpecs);

            codeGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateUnaryEuclideanGeometricProductFiles()
        {
            var opSpecsArray = new[]
            {
                GaFuLLanguageOperationKind.UnaryGeometricProductSquared.CreateMetricOperationSpecs(),
                GaFuLLanguageOperationKind.UnaryGeometricProductReverse.CreateMetricOperationSpecs(),
                GaFuLLanguageOperationKind.UnaryGeometricProductSquared.CreateEuclideanOperationSpecs(),
                GaFuLLanguageOperationKind.UnaryGeometricProductReverse.CreateEuclideanOperationSpecs()
            };
                
            foreach (var opSpecs in opSpecsArray)
            {
                CodeFilesComposer.InitializeFile(opSpecs + ".cs");

                var codeGen = new UnaryGpMethodsFileComposer(this, opSpecs);

                codeGen.Generate();

                CodeFilesComposer.UnselectActiveFile();
            }
        }

        private void GenerateApplyVersorMainMethod(GaFuLLanguageOperationSpecs opSpecs)
        {
            CodeFilesComposer.InitializeFile(opSpecs.GetName() + ".cs");

            var fileGen = new ApplyVersorMainMethodFileComposer(this, opSpecs);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateApplyVersorFiles()
        {
            var opSpecsArray = new[]
            {
                GaFuLLanguageOperationKind.BinaryProject.CreateEuclideanOperationSpecs(),
                GaFuLLanguageOperationKind.BinaryReflect.CreateEuclideanOperationSpecs(),
                GaFuLLanguageOperationKind.BinaryComplement.CreateEuclideanOperationSpecs(),
                //GaFuLLanguageOperationKind.BinaryRotate.CreateEuclideanOperationSpecs(),
                GaFuLLanguageOperationKind.BinaryProject.CreateMetricOperationSpecs(),
                GaFuLLanguageOperationKind.BinaryReflect.CreateMetricOperationSpecs(),
                GaFuLLanguageOperationKind.BinaryComplement.CreateMetricOperationSpecs(),
                GaFuLLanguageOperationKind.BinaryRotate.CreateMetricOperationSpecs(),
            };

            foreach (var opSpecs in opSpecsArray)
            {
                foreach (var inGrade1 in Grades)
                {
                    //if (inGrade1.IsOdd() && opSpecs.OperationKind == GaFuLLanguageOperationKind.BinaryRotate)
                    //    continue;

                    foreach (var inGrade2 in Grades)
                    {
                        var methodName =
                            opSpecs.GetName(inGrade1, inGrade2, inGrade2);

                        CodeFilesComposer.DownFolder(opSpecs.GetName());

                        CodeFilesComposer.InitializeFile(methodName + ".cs");

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

        private void GenerateFactorMethod(int inGrade, ulong inId)
        {
            CodeFilesComposer.DownFolder("Factor");

            CodeFilesComposer.InitializeFile("Factor" + inId + ".cs");

            var fileGen = new FactorMethodFileComposer(this, inGrade, inId);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();

            CodeFilesComposer.UpFolder();
        }

        private void GenerateFactorFiles()
        {
            CodeFilesComposer.InitializeFile("Factor.cs");

            var fileGen = new FactorMainMethodsFileComposer(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();

            for (var inGrade = 2; inGrade <= VSpaceDimensions; inGrade++)
            {
                var kvSpaceDimensions = 
                    VSpaceDimensions.KVectorSpaceDimension(inGrade);

                for (var inIndex = 0UL; inIndex < kvSpaceDimensions; inIndex++)
                {
                    var inId = BasisBladeUtils.BasisBladeGradeIndexToId(inGrade, inIndex);

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
            CodeFilesComposer.InitializeFile(CurrentNamespace + "Utils.cs");

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
            CodeFilesComposer.InitializeFile(CurrentNamespace + "Multivector.cs");

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
            CodeFilesComposer.InitializeFile(CurrentNamespace + "Vector.cs");

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
            CodeFilesComposer.InitializeFile(CurrentNamespace + "FactoredBlade.cs");

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
            CodeFilesComposer.InitializeFile(CurrentNamespace + "Outermorphism.cs");

            var fileGen = new OutermorphismClassFileComposer(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateOutermorphismApplyMethodFile(int inGrade)
        {
            CodeFilesComposer.InitializeFile("Map_" + inGrade + ".cs");

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

            for (var inGrade = 1; inGrade <= VSpaceDimensions; inGrade++)
                GenerateOutermorphismApplyMethodFile(inGrade);

            CodeFilesComposer.UpFolder();

            CodeFilesComposer.UpFolder();

            this.ReportFinish(progressId);
        }

        private bool GeneratePreComputationsCode(GaFuLMetaContextCodeComposer contextCodeComposer)
        {
            //Generate comments
            GaFuLMetaContextCodeComposer.DefaultGenerateCommentsBeforeComputations(contextCodeComposer);

            //Temp variables declaration
            if (contextCodeComposer.Context.GetTargetTempVarsCount() > MaxTargetLocalVars)
            {
                //Add array declaration code
                contextCodeComposer.SyntaxList.Add(
                    contextCodeComposer.GeoLanguage.SyntaxFactory.DeclareLocalArray(
                        GeoLanguage.ScalarTypeName,
                        "tempArray",
                        contextCodeComposer.Context.GetTargetTempVarsCount().ToString()
                        )
                    );

                contextCodeComposer.SyntaxList.AddEmptyLine();
            }
            else
            {
                var tempVarNames =
                    contextCodeComposer
                        .Context.GetIntermediateVariables()
                        .Select(item => item.ExternalName)
                        .Distinct();

                //Add temp variables declaration code
                foreach (var tempVarName in tempVarNames)
                    contextCodeComposer.SyntaxList.Add(
                        contextCodeComposer.GeoLanguage.SyntaxFactory.DeclareLocalVariable(GeoLanguage.ScalarTypeName, tempVarName)
                    );

                contextCodeComposer.SyntaxList.AddEmptyLine();
            }

            return true;
        }

        private void GeneratePostComputationsCode(GaFuLMetaContextCodeComposer contextCodeComposer)
        {
            //Generate comments
            GaFuLMetaContextCodeComposer.DefaultGenerateCommentsAfterComputations(contextCodeComposer);
        }


        protected override bool InitializeTemplates()
        {
            //Initialize templates used in code composition

            Templates.Parse(GeoClcCodeTemplates);

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

        public override GaFuLCodeLibraryComposerBase CreateEmptyComposer()
        {
            var libGen = new GaFuLLibraryComposer(
                RootNamespace, 
                GeometricProcessor,
                VSpaceDimensions
            );

            libGen.DefaultContextOptions.SetOptions(DefaultContextOptions);
            libGen.DefaultContextCodeComposerOptions.SetOptions(DefaultContextCodeComposerOptions);

            return libGen;
        }
    }
}
