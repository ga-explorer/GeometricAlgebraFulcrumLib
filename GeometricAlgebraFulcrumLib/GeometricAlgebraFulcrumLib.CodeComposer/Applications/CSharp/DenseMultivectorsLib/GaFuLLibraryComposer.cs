using System;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra;
using GeometricAlgebraFulcrumLib.CodeComposer.Composers;
using GeometricAlgebraFulcrumLib.CodeComposer.Languages;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using TextComposerLib.Text.Parametric;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseMultivectorsLib
{
    public sealed class GaFuLLibraryComposer :
        GaFuLCodeLibraryComposerBase
    {
        public static string GenerateCode(uint vSpaceDimensions)
        {
            var codeLibraryComposer = new GaFuLLibraryComposer(vSpaceDimensions);

            if (codeLibraryComposer.VerifyReadyToGenerate() == false)
                return string.Empty;

            codeLibraryComposer.InitializeGenerator();

            codeLibraryComposer.ComposeTextFiles();

            codeLibraryComposer.CodeFilesComposer.FinalizeAllFiles();

            codeLibraryComposer.FinalizeGenerator();

            return codeLibraryComposer.CodeFilesComposer.ToString();
        }


        public override string Name 
            => "C# Dense Multivector code library composer";

        public override string Description 
            => "C# Dense Multivector code library composer";

        public IGeometricAlgebraProcessor<ISymbolicExpressionAtomic> GeometricProcessor { get; }

        public uint VSpaceDimensions { get; }

        public ulong GaSpaceDimensions 
            => 1UL << (int) VSpaceDimensions;

        public string RootNamespace 
            => $"EGA{VSpaceDimensions}D";
        
        public ParametricTextComposer MultivectorClassTemplate 
            => Templates["multivectorClass"];


        public GaFuLLibraryComposer(uint vSpaceDimensions) : 
            base(GaFuLLanguageServerBase.CSharp())
        {
            if (vSpaceDimensions < 2)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            VSpaceDimensions = vSpaceDimensions;
            GeometricProcessor = new SymbolicContext().CreateGeometricAlgebraEuclideanProcessor(vSpaceDimensions);
        }


        protected override bool InitializeTemplates()
        {
            Templates.Clear();

            var codeTemplate1 = new ParametricTextComposer("#", "#");

            codeTemplate1.SetTemplateText(@"
using System.Collections;
using System.Collections.Generic;

namespace EGA#vSpaceDimensions#D
{
    public sealed class Multivector
        : IReadOnlyList<double>
    {
        public static Multivector operator +(Multivector mv1, Multivector mv2)
        {
            var mv = new Multivector();

            #mvAddCode#

            return mv;
        }

        public static Multivector operator -(Multivector mv1, Multivector mv2)
        {
            var mv = new Multivector();

            #mvSubtractCode#

            return mv;
        }


        private readonly double[] _scalarsArray
            = new double[#gaSpaceDimensions#];


        public int Count => #gaSpaceDimensions#;

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

            #mvGpCode#

            return mv;
        }

        public Multivector Op(Multivector mv2)
        {
            var mv = new Multivector();

            #mvOpCode#

            return mv;
        }

        public Multivector Lcp(Multivector mv2)
        {
            var mv = new Multivector();

            #mvLcpCode#

            return mv;
        }

        public Multivector Rcp(Multivector mv2)
        {
            var mv = new Multivector();

            #mvRcpCode#

            return mv;
        }

        public Multivector Fdp(Multivector mv2)
        {
            var mv = new Multivector();

            #mvFdpCode#

            return mv;
        }

        public Multivector Hip(Multivector mv2)
        {
            var mv = new Multivector();

            #mvHipCode#

            return mv;
        }

        public Multivector Acp(Multivector mv2)
        {
            var mv = new Multivector();

            #mvAcpCode#

            return mv;
        }

        public Multivector Cp(Multivector mv2)
        {
            var mv = new Multivector();

            #mvCpCode#

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
"
            );

            Templates.Add("multivectorClass", codeTemplate1);

            return true;
        }

        protected override void InitializeSubComponents()
        {
            
        }

        protected override bool VerifyReadyToGenerate()
        {
            return true;
        }


        private string GetMultivectorBinaryOperationCode(Func<IMultivectorStorage<ISymbolicExpressionAtomic>, IMultivectorStorage<ISymbolicExpressionAtomic>, IMultivectorStorage<ISymbolicExpressionAtomic>> binaryOperationFunc, Func<ulong, string> namingFunc1, Func<ulong, string> namingFunc2, Func<ulong, string> namingFunc3)
        {
            var context = new SymbolicContext();

            //context.AttachMathematicaExpressionEvaluator();

            var mv1 = 
                context.ParameterVariablesFactory.CreateDenseMultivector(
                    VSpaceDimensions,
                    id => $"mv1s{id}"
                );

            var mv2 = 
                context.ParameterVariablesFactory.CreateDenseMultivector(
                    VSpaceDimensions,
                    id => $"mv2s{id}"
                );

            context.MergeExpressions = true;

            var mv3 = 
                binaryOperationFunc(mv1, mv2);

            context.MergeExpressions = false;

            mv3.SetIsOutput(true);

            mv1.SetExternalNamesByTermId(namingFunc1);
            mv2.SetExternalNamesByTermId(namingFunc2);
            mv3.SetExternalNamesByTermId(namingFunc3);

            //Console.WriteLine("Before Simplification:");
            //Console.WriteLine(context);
            //Console.WriteLine();

            //context.SimplifyRhsExpressions();

            //Console.WriteLine("After Simplification:");
            //Console.WriteLine(context);
            //Console.WriteLine();

            context.OptimizeContext();

            context.SetIntermediateExternalNamesByNameIndex(index => $"tempVar{index}");

            var codeComposer = new GaFuLSymbolicContextCodeComposer(GeoLanguage, context);

            return codeComposer.Generate();
        }


        protected override void ComposeTextFiles()
        {
            // All code is generated inside a single file
            CodeFilesComposer.InitalizeFile("Multivector.cs");

            //Fill code parts of parametric template
            MultivectorClassTemplate["vSpaceDimensions"] = VSpaceDimensions.ToString();

            MultivectorClassTemplate["gaSpaceDimensions"] = GaSpaceDimensions.ToString();

            MultivectorClassTemplate["mvAddCode"] = GetMultivectorBinaryOperationCode(
                (mv1, mv2) => GeometricProcessor.Add(mv1, mv2),
                id => $"mv1._scalarsArray[{id}]",
                id => $"mv2._scalarsArray[{id}]",
                id => $"mv._scalarsArray[{id}]"
            );

            MultivectorClassTemplate["mvSubtractCode"] = GetMultivectorBinaryOperationCode(
                (mv1, mv2) => GeometricProcessor.Subtract(mv1, mv2),
                id => $"mv1._scalarsArray[{id}]",
                id => $"mv2._scalarsArray[{id}]",
                id => $"mv._scalarsArray[{id}]"
            );

            MultivectorClassTemplate["mvGpCode"] = GetMultivectorBinaryOperationCode(
                (mv1, mv2) => GeometricProcessor.EGp(mv1, mv2),
                id => $"_scalarsArray[{id}]",
                id => $"mv2._scalarsArray[{id}]",
                id => $"mv._scalarsArray[{id}]"
            );

            MultivectorClassTemplate["mvOpCode"] = GetMultivectorBinaryOperationCode(
                (mv1, mv2) => GeometricProcessor.Op(mv1, mv2),
                id => $"_scalarsArray[{id}]",
                id => $"mv2._scalarsArray[{id}]",
                id => $"mv._scalarsArray[{id}]"
            );

            MultivectorClassTemplate["mvLcpCode"] = GetMultivectorBinaryOperationCode(
                (mv1, mv2) => GeometricProcessor.ELcp(mv1, mv2),
                id => $"_scalarsArray[{id}]",
                id => $"mv2._scalarsArray[{id}]",
                id => $"mv._scalarsArray[{id}]"
            );

            MultivectorClassTemplate["mvRcpCode"] = GetMultivectorBinaryOperationCode(
                (mv1, mv2) => GeometricProcessor.ERcp(mv1, mv2),
                id => $"_scalarsArray[{id}]",
                id => $"mv2._scalarsArray[{id}]",
                id => $"mv._scalarsArray[{id}]"
            );

            MultivectorClassTemplate["mvFdpCode"] = GetMultivectorBinaryOperationCode(
                (mv1, mv2) => GeometricProcessor.EFdp(mv1, mv2),
                id => $"_scalarsArray[{id}]",
                id => $"mv2._scalarsArray[{id}]",
                id => $"mv._scalarsArray[{id}]"
            );

            MultivectorClassTemplate["mvHipCode"] = GetMultivectorBinaryOperationCode(
                (mv1, mv2) => GeometricProcessor.EHip(mv1, mv2),
                id => $"_scalarsArray[{id}]",
                id => $"mv2._scalarsArray[{id}]",
                id => $"mv._scalarsArray[{id}]"
            );

            MultivectorClassTemplate["mvAcpCode"] = GetMultivectorBinaryOperationCode(
                (mv1, mv2) => GeometricProcessor.EAcp(mv1, mv2),
                id => $"_scalarsArray[{id}]",
                id => $"mv2._scalarsArray[{id}]",
                id => $"mv._scalarsArray[{id}]"
            );

            MultivectorClassTemplate["mvCpCode"] = GetMultivectorBinaryOperationCode(
                (mv1, mv2) => GeometricProcessor.ECp(mv1, mv2),
                id => $"_scalarsArray[{id}]",
                id => $"mv2._scalarsArray[{id}]",
                id => $"mv._scalarsArray[{id}]"
            );

            //Generate template code
            var classCode = 
                MultivectorClassTemplate.GenerateText();

            CodeFilesComposer.ActiveFileTextComposer.AppendLineAtNewLine(classCode);

            CodeFilesComposer.FinalizeActiveFile();
        }

        protected override void FinalizeSubComponents()
        {
            
        }

        public override GaFuLCodeLibraryComposerBase CreateEmptyComposer()
        {
            return new GaFuLLibraryComposer(VSpaceDimensions);
        }
    }
}