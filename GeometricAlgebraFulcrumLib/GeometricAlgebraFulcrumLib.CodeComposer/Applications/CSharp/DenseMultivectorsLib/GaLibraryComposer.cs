using System;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.CodeComposer.Composers;
using GeometricAlgebraFulcrumLib.CodeComposer.LanguageServers;
using GeometricAlgebraFulcrumLib.CodeComposer.LanguageServers.CSharp;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Symbolic;
using TextComposerLib.Text.Parametric;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseMultivectorsLib
{
    public sealed class GaLibraryComposer :
        GaCodeLibraryComposerBase
    {
        public static string GenerateCode(int vSpaceDimensions)
        {
            var codeLibraryComposer = new GaLibraryComposer(vSpaceDimensions);

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

        public int VSpaceDimensions { get; }

        public ulong GaSpaceDimensions 
            => 1UL << VSpaceDimensions;

        public string RootNamespace 
            => $"EGA{VSpaceDimensions}D";
        
        public ParametricTextComposer MultivectorClassTemplate 
            => Templates["multivectorClass"];


        public GaLibraryComposer(int vSpaceDimensions) : 
            base(GaClcLanguageServer.CSharp(new GaClcMathematicaExprToCSharpConverter()))
        {
            if (vSpaceDimensions < 2)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            VSpaceDimensions = vSpaceDimensions;
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


        private string GetMultivectorBinaryOperationCode(Func<IGaMultivectorStorage<ISymbolicExpressionAtomic>, IGaMultivectorStorage<ISymbolicExpressionAtomic>, IGaMultivectorStorage<ISymbolicExpressionAtomic>> binaryOperationFunc, Func<ulong, string> namingFunc1, Func<ulong, string> namingFunc2, Func<ulong, string> namingFunc3)
        {
            var context = new SymbolicContext();

            context.AttachMathematicaExprSimplifier();

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

            var codeComposer = new GaClcSymbolicContextCodeComposer(this, context);

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
                (mv1, mv2) => mv1.Add(mv2),
                id => $"mv1._scalarsArray[{id}]",
                id => $"mv2._scalarsArray[{id}]",
                id => $"mv._scalarsArray[{id}]"
            );

            MultivectorClassTemplate["mvSubtractCode"] = GetMultivectorBinaryOperationCode(
                (mv1, mv2) => mv1.Subtract(mv2),
                id => $"mv1._scalarsArray[{id}]",
                id => $"mv2._scalarsArray[{id}]",
                id => $"mv._scalarsArray[{id}]"
            );

            MultivectorClassTemplate["mvGpCode"] = GetMultivectorBinaryOperationCode(
                (mv1, mv2) => mv1.EGp(mv2),
                id => $"_scalarsArray[{id}]",
                id => $"mv2._scalarsArray[{id}]",
                id => $"mv._scalarsArray[{id}]"
            );

            MultivectorClassTemplate["mvOpCode"] = GetMultivectorBinaryOperationCode(
                (mv1, mv2) => mv1.Op(mv2),
                id => $"_scalarsArray[{id}]",
                id => $"mv2._scalarsArray[{id}]",
                id => $"mv._scalarsArray[{id}]"
            );

            MultivectorClassTemplate["mvLcpCode"] = GetMultivectorBinaryOperationCode(
                (mv1, mv2) => mv1.ELcp(mv2),
                id => $"_scalarsArray[{id}]",
                id => $"mv2._scalarsArray[{id}]",
                id => $"mv._scalarsArray[{id}]"
            );

            MultivectorClassTemplate["mvRcpCode"] = GetMultivectorBinaryOperationCode(
                (mv1, mv2) => mv1.ERcp(mv2),
                id => $"_scalarsArray[{id}]",
                id => $"mv2._scalarsArray[{id}]",
                id => $"mv._scalarsArray[{id}]"
            );

            MultivectorClassTemplate["mvFdpCode"] = GetMultivectorBinaryOperationCode(
                (mv1, mv2) => mv1.EFdp(mv2),
                id => $"_scalarsArray[{id}]",
                id => $"mv2._scalarsArray[{id}]",
                id => $"mv._scalarsArray[{id}]"
            );

            MultivectorClassTemplate["mvHipCode"] = GetMultivectorBinaryOperationCode(
                (mv1, mv2) => mv1.EHip(mv2),
                id => $"_scalarsArray[{id}]",
                id => $"mv2._scalarsArray[{id}]",
                id => $"mv._scalarsArray[{id}]"
            );

            MultivectorClassTemplate["mvAcpCode"] = GetMultivectorBinaryOperationCode(
                (mv1, mv2) => mv1.EAcp(mv2),
                id => $"_scalarsArray[{id}]",
                id => $"mv2._scalarsArray[{id}]",
                id => $"mv._scalarsArray[{id}]"
            );

            MultivectorClassTemplate["mvCpCode"] = GetMultivectorBinaryOperationCode(
                (mv1, mv2) => mv1.ECp(mv2),
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

        public override GaCodeLibraryComposerBase CreateEmptyComposer()
        {
            return new GaLibraryComposer(VSpaceDimensions);
        }
    }
}