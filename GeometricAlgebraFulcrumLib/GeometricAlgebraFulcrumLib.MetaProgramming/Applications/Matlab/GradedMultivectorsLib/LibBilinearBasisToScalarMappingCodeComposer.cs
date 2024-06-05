using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.MetaProgramming.Applications.Matlab.GradedMultivectorsLib.Combinations;
using GeometricAlgebraFulcrumLib.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Files;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.Matlab.GradedMultivectorsLib;

public class LibBilinearBasisToScalarMappingCodeComposer :
    LibSubCodeComposer
{
    public static LibBilinearBasisToScalarMappingCodeComposer Create(
        LibCodeComposerSpecs specs,
        TextFilesComposer codeFilesComposer,
        string productFunctionName,
        bool isSingleInput,
        Func<RGaBasisBlade, RGaBasisBlade, IRGaSignedBasisBlade> basisMapFunc
    )
    {
        return new LibBilinearBasisToScalarMappingCodeComposer(specs, codeFilesComposer)
        {
            ProductFunctionName = productFunctionName,
            IsSingleInputFunction = isSingleInput,
            BasisMapFunc = basisMapFunc
        };
    }


    public string ProductFunctionName { get; init; }

    public bool IsSingleInputFunction { get; init; }

    public Func<RGaBasisBlade, RGaBasisBlade, IRGaSignedBasisBlade> BasisMapFunc { get; init; }


    private LibBilinearBasisToScalarMappingCodeComposer(LibCodeComposerSpecs specs, TextFilesComposer codeFilesComposer)
        : base(specs, codeFilesComposer)
    {
    }


    private void GetBilinearOperationCode(LibBilinearCombination termTable)
    {
        var input1Name = IsSingleInputFunction ? "inMv" : "inMv1";
        var input2Name = IsSingleInputFunction ? "inMv" : "inMv2";

        var scalarCode =
            termTable
                .GetIdTermListPairs()
                .OrderBy(p => 
                    termTable.OutputType.GetScalarIndex(p.Key)
                ).Select(p =>
                    {
                        var lhsIdCode = GradedMultivectorLibUtils.GetBasisBladeCode(
                            p.Key,
                            id => termTable.OutputType.GetScalarName("outScalar", id)
                        );

                        var rhsCode = p.Value.GetRhsCode(
                            id => termTable.Input1Type.GetScalarName(input1Name, id),
                            id => termTable.Input2Type.GetScalarName(input2Name, id)
                        );

                        return $"{lhsIdCode} = {rhsCode};";
                    }
                ).Concatenate(Environment.NewLine);
        
        var initCode = IsSingleInputFunction
            ? $@"
arguments
    inMv ({termTable.Input1Type.ScalarCount},:) double
end

sampleCount = size(inMv, 2);

outScalar = zeros([1, sampleCount], 'double');
".Trim()
            : @$"
arguments
    inMv1 ({termTable.Input1Type.ScalarCount},:) double
    inMv2 ({termTable.Input2Type.ScalarCount},:) double
end

sampleCount1 = size(inMv1, 2);
sampleCount2 = size(inMv2, 2);

if (sampleCount1 ~= sampleCount2 && sampleCount1 ~= 1 && sampleCount2 ~= 1)
    error('Number of columns in both inputs must either match or equal 1');
end

sampleCount = max(sampleCount1, sampleCount2);

outScalar = zeros([{termTable.OutputType.ScalarCount}, sampleCount], 'double');
".Trim();

        var functionName = 
            $"{ProductFunctionName}{termTable.Input1Type.TypeSymbol}";

        CodeFilesComposer.InitializeFile($"{functionName}.m");

        var codeComposer = CodeFilesComposer.ActiveFileTextComposer;

        var functionArgumentCode = 
            IsSingleInputFunction ? "inMv" : "inMv1, inMv2";

        codeComposer
            .AppendLine(Specs.GetFileHeaderText())
            .AppendLine($"function outScalar = {functionName}({functionArgumentCode})")
            .IncreaseIndentation()
            .AppendLine(initCode)
            .AppendLine()
            .AppendLine(scalarCode)
            .DecreaseIndentation()
            .AppendLine("end")
            .AppendLine();

        CodeFilesComposer.ActiveFileComposer.FinalizeText(
            code => code.RemoveRepeatedEmptyLines()
        );
    }


    public override TextFilesComposer GenerateCode()
    {
        if (IsSingleInputFunction)
        {
            foreach (var inType1 in Types)
            {
                var termTable = 
                    LibBilinearCombination.Create(
                        inType1,
                        inType1,
                        BasisMapFunc
                    ).SelectOutputType(Types);

                GetBilinearOperationCode(termTable);
            }
        }
        else
        {
            foreach (var inType1 in Types)
            foreach (var inType2 in Types)
            {
                var termTable =
                    LibBilinearCombination.Create(
                        inType1,
                        inType2,
                        BasisMapFunc
                    ).SelectOutputType(Types);

                GetBilinearOperationCode(termTable);
            }
        }

        return CodeFilesComposer;
    }
}