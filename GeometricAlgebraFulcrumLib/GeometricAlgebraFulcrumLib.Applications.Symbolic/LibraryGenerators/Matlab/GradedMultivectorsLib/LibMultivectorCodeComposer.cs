using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.Matlab.GradedMultivectorsLib.Combinations;
using GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.Matlab.GradedMultivectorsLib.Types;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Files;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.Matlab.GradedMultivectorsLib;

public class LibMultivectorCodeComposer :
    LibSubCodeComposer
{
    public static LibMultivectorCodeComposer Create(
        LibCodeComposerSpecs specs,
        TextFilesComposer codeFilesComposer
    )
    {
        return new LibMultivectorCodeComposer(specs, codeFilesComposer);
    }


    private LibMultivectorCodeComposer(LibCodeComposerSpecs specs, TextFilesComposer codeFilesComposer)
        : base(specs, codeFilesComposer)
    {
    }

    
    private IReadOnlyList<string> GetIndexRangeCode()
    {
        return (Specs.VSpaceDimensions + 1).GetRange(
            g =>
            {
                var (index1, index2) = 
                    Specs.GetKVectorScalarRange(g);

                return index1 == index2 
                    ? (index1 + 1).ToString() 
                    : $"{index1 + 1}:{index2 + 1}";
            }
        ).ToImmutableArray();
    }

    private IReadOnlyList<string> GetLaTeXBasisSubscripts()
    {
        var n = Specs.VSpaceDimensions;
        var q = Specs.Metric.NegativeSignatureBasisCount;
        var r = Specs.Metric.ZeroSignatureBasisCount;
        var p = n - (q + r);

        var subscriptList = new List<string>(n);

        if (Specs.Metric.IsEuclidean)
        {
            subscriptList.AddRange(p.GetRange(k => $"{k + 1}"));
        }
        else if (Specs.Metric.IsProjective)
        {
            subscriptList.Add("0");
            subscriptList.AddRange(p.GetRange(k => $"{k + 1}"));
        }
        else if (Specs.Metric.IsConformal)
        {
            subscriptList.Add("-");
            subscriptList.Add("+");
            subscriptList.AddRange((p - 1).GetRange(k => $"{k + 1}"));
        }
        else
        {
            if (q == 1)
                subscriptList.Add("-");
            else if (q > 1)
                subscriptList.AddRange(q.GetRange(k => $"-{k + 1}"));

            if (r == 1)
                subscriptList.Add("0");
            else if (r > 1)
                subscriptList.AddRange(r.GetRange(k => $"0{k + 1}"));

            if (p > 0)
                subscriptList.AddRange(p.GetRange(k => $"{k + 1}"));
        }
        
        return subscriptList;
    }
    
    private string GetBasisBladeLaTeXCode(int grade)
    {
        var basisVectorLaTeX = 
            GetLaTeXBasisSubscripts();

        var codeList = KVectorTypes[grade]
            .KvSpaceDimensions
            .GetRange(index =>
                {
                    var id = (int)BasisBladeUtils.BasisBladeGradeIndexToId(
                        (uint)grade, 
                        (ulong)index
                    );

                    if (id == 0) return @"""""";

                    return @"""\bm{e}_" + 
                           id.PatternToPositions()
                               .Select(i => basisVectorLaTeX[i])
                               .Concatenate(",", "{", @"}""");
                }
            ).ToImmutableArray();

        return codeList.Length == 1
            ? codeList[0]
            : codeList.Concatenate("; ", "[", "]");
    }
    
    private string GetBasisBladeLaTeXCode()
    {
        var basisVectorLaTeX = 
            GetLaTeXBasisSubscripts();

        return GaSpaceDimensions
            .GetRange()
            .OrderBy(id => id.Grade())
            .ThenBy(id => id)
            .Select(id =>
                {
                    if (id == 0) return @"""""";

                    return @"""\bm{e}_" + 
                           id.PatternToPositions()
                               .Select(i => basisVectorLaTeX[i])
                               .Concatenate(",", "{", @"}""");
                }
            ).Concatenate("; ", "[", "]");
    }

    private string GetUnilinearBasisToBasisMappingCode(string functionName, Func<RGaBasisBlade, IRGaSignedBasisBlade> basisMapping)
    {
        var switch1CodeComposer = new LinearTextComposer();

        switch1CodeComposer
            .AppendLine("switch inMv.Grade")
            .IncreaseIndentation();

        for (var g1 = 0; g1 <= VSpaceDimensions; g1++)
        {
            var inType1 = KVectorTypes[g1];

            var termTable = 
                LibUnilinearCombination.Create(
                    inType1,
                    basisMapping
                ).SelectOutputType(Types);

            var outGrade = 
                termTable.OutputType.IsMultivector 
                    ? -1 
                    : ((LibTypeKVector)termTable.OutputType).Grade;

            var methodName = 
                $"{functionName}{termTable.InputType.TypeSymbol}{termTable.OutputType.TypeSymbol}";

            switch1CodeComposer
                .AppendLine($"case {g1}")
                .AppendLine($"    outMv = {Specs.GetQualifiedMultivectorClassName()}({outGrade}, {methodName}(inMv.Data));");
        }

        {
            var inType1 = MultivectorType;

            var termTable = 
                LibUnilinearCombination.Create(
                    inType1,
                    basisMapping
                ).SelectOutputType(Types);

            var outGrade = 
                termTable.OutputType.IsMultivector 
                    ? -1 
                    : ((LibTypeKVector)termTable.OutputType).Grade;

            var methodName = 
                $"{functionName}{termTable.InputType.TypeSymbol}{termTable.OutputType.TypeSymbol}";

            switch1CodeComposer
                .AppendLine($"otherwise")
                .AppendLine($"    outMv = {Specs.GetQualifiedMultivectorClassName()}({outGrade}, {methodName}(inMv.Data));");
        }

        switch1CodeComposer
            .DecreaseIndentation()
            .AppendLineAtNewLine("end");

        var switch1Code = 
            switch1CodeComposer.ToString().PrefixTextLines("    ");

        var methodCode = $@"
function outMv = {functionName}(inMv)
    arguments
        inMv (1,1) {Specs.GetQualifiedMultivectorClassName()}
    end

{switch1Code}
end
".Trim();

        return methodCode;
    }
    
    private string GetSingleInputBilinearBasisToScalarMappingCode(string functionName, Func<RGaBasisBlade, RGaBasisBlade, IRGaSignedBasisBlade> basisMapping)
    {
        var switch1CodeComposer = new LinearTextComposer();

        switch1CodeComposer
            .AppendLine("switch inMv.Grade")
            .IncreaseIndentation();

        for (var g1 = 0; g1 <= VSpaceDimensions; g1++)
        {
            var inType1 = KVectorTypes[g1];

            var termTable = 
                LibBilinearCombination.Create(
                    inType1,
                    inType1,
                    basisMapping
                ).SelectOutputType(Types);

            var outGrade = 
                termTable.OutputType.IsMultivector 
                    ? -1 
                    : ((LibTypeKVector)termTable.OutputType).Grade;

            if (outGrade != 0)
                throw new InvalidOperationException();

            var methodName = 
                $"{functionName}{termTable.Input1Type.TypeSymbol}";

            switch1CodeComposer
                .AppendLine($"case {g1}")
                .AppendLine($"    outScalar = {methodName}(inMv.Data);");
        }
        
        {
            var inType1 = MultivectorType;

            var termTable = 
                LibBilinearCombination.Create(
                    inType1,
                    inType1,
                    basisMapping
                ).SelectOutputType(Types);

            var outGrade = 
                termTable.OutputType.IsMultivector 
                    ? -1 
                    : ((LibTypeKVector)termTable.OutputType).Grade;
            
            if (outGrade != 0)
                throw new InvalidOperationException();

            var methodName = 
                $"{functionName}{termTable.Input1Type.TypeSymbol}";

            switch1CodeComposer
                .AppendLine($"otherwise")
                .AppendLine($"    outScalar = {methodName}(inMv.Data);");
        }

        switch1CodeComposer
            .DecreaseIndentation()
            .AppendLineAtNewLine("end");

        var switch1Code = 
            switch1CodeComposer.ToString().PrefixTextLines("    ");

        var methodCode = $@"
function outScalar = {functionName}(inMv)
    arguments
        inMv (1,1) {Specs.GetQualifiedMultivectorClassName()}
    end

{switch1Code}
end
".Trim();

        return methodCode;
    }

    private string GetBilinearBasisToBasisMappingCode(string functionName, Func<RGaBasisBlade, RGaBasisBlade, IRGaSignedBasisBlade> basisMapping, bool constructOutputMultivector = true)
    {
        var switch1CodeComposer = new LinearTextComposer();

        switch1CodeComposer
            .AppendLine("switch inMv1.Grade")
            .IncreaseIndentation();

        for (var g1 = 0; g1 <= VSpaceDimensions; g1++)
        {
            var inType1 = KVectorTypes[g1];

            var switch2CodeComposer = new LinearTextComposer();

            switch2CodeComposer
                .AppendLine("switch inMv2.Grade")
                .IncreaseIndentation();

            for (var g2 = 0; g2 <= VSpaceDimensions; g2++)
            {
                var inType2 = KVectorTypes[g2];

                var termTable = 
                    LibBilinearCombination.Create(
                        inType1,
                        inType2,
                        basisMapping
                    ).SelectOutputType(Types);

                var outGrade = 
                    termTable.OutputType.IsMultivector 
                        ? -1 
                        : ((LibTypeKVector)termTable.OutputType).Grade;

                var methodName = 
                    $"{functionName}{termTable.Input1Type.TypeSymbol}{termTable.Input2Type.TypeSymbol}{termTable.OutputType.TypeSymbol}";

                var outCode = constructOutputMultivector
                    ? $"{Specs.GetQualifiedMultivectorClassName()}({outGrade}, {methodName}(inMv1.Data, inMv2.Data))"
                    : $"{methodName}(inMv1.Data, inMv2.Data)";

                switch2CodeComposer
                    .AppendLine($"case {g2}")
                    .AppendLine($"    outMv = {outCode};");
            }

            {
                var inType2 = MultivectorType;

                var termTable = 
                    LibBilinearCombination.Create(
                        inType1,
                        inType2,
                        basisMapping
                    ).SelectOutputType(Types);

                var outGrade = 
                    termTable.OutputType.IsMultivector 
                        ? -1 
                        : ((LibTypeKVector)termTable.OutputType).Grade;

                var methodName = 
                    $"{functionName}{termTable.Input1Type.TypeSymbol}{termTable.Input2Type.TypeSymbol}{termTable.OutputType.TypeSymbol}";
                
                var outCode = constructOutputMultivector
                    ? $"{Specs.GetQualifiedMultivectorClassName()}({outGrade}, {methodName}(inMv1.Data, inMv2.Data))"
                    : $"{methodName}(inMv1.Data, inMv2.Data)";

                switch2CodeComposer
                    .AppendLine($"otherwise")
                    .AppendLine($"    outMv = {outCode};");
            }

            switch2CodeComposer
                .DecreaseIndentation()
                .AppendLineAtNewLine("end");

            var switch2Code = 
                switch2CodeComposer.ToString().PrefixTextLines("    ");

            switch1CodeComposer
                .AppendLineAtNewLine($"case {g1}")
                .AppendLine(switch2Code);
        }

        {
            var inType1 = MultivectorType;

            var switch2CodeComposer = new LinearTextComposer();

            switch2CodeComposer
                .AppendLine("switch inMv2.Grade")
                .IncreaseIndentation();

            for (var g2 = 0; g2 <= VSpaceDimensions; g2++)
            {
                var inType2 = KVectorTypes[g2];

                var termTable = 
                    LibBilinearCombination.Create(
                        inType1,
                        inType2,
                        basisMapping
                    ).SelectOutputType(Types);

                var outGrade = 
                    termTable.OutputType.IsMultivector 
                        ? -1 
                        : ((LibTypeKVector)termTable.OutputType).Grade;

                var methodName = 
                    $"{functionName}{termTable.Input1Type.TypeSymbol}{termTable.Input2Type.TypeSymbol}{termTable.OutputType.TypeSymbol}";
                
                var outCode = constructOutputMultivector
                    ? $"{Specs.GetQualifiedMultivectorClassName()}({outGrade}, {methodName}(inMv1.Data, inMv2.Data))"
                    : $"{methodName}(inMv1.Data, inMv2.Data)";

                switch2CodeComposer
                    .AppendLine($"case {g2}")
                    .AppendLine($"    outMv = {outCode};");
            }
            
            {
                var inType2 = MultivectorType;

                var termTable = 
                    LibBilinearCombination.Create(
                        inType1,
                        inType2,
                        basisMapping
                    ).SelectOutputType(Types);

                var outGrade = 
                    termTable.OutputType.IsMultivector 
                        ? -1 
                        : ((LibTypeKVector)termTable.OutputType).Grade;

                var methodName = 
                    $"{functionName}{termTable.Input1Type.TypeSymbol}{termTable.Input2Type.TypeSymbol}{termTable.OutputType.TypeSymbol}";
                
                var outCode = constructOutputMultivector
                    ? $"{Specs.GetQualifiedMultivectorClassName()}({outGrade}, {methodName}(inMv1.Data, inMv2.Data))"
                    : $"{methodName}(inMv1.Data, inMv2.Data)";

                switch2CodeComposer
                    .AppendLine($"otherwise")
                    .AppendLine($"    outMv = {outCode};");
            }

            switch2CodeComposer
                .DecreaseIndentation()
                .AppendLineAtNewLine("end");

            var switch2Code = 
                switch2CodeComposer.ToString().PrefixTextLines("    ");

            switch1CodeComposer
                .AppendLineAtNewLine($"otherwise")
                .AppendLine(switch2Code);
        }

        switch1CodeComposer
            .DecreaseIndentation()
            .AppendLineAtNewLine("end");

        var switch1Code = 
            switch1CodeComposer.ToString().PrefixTextLines("    ");

        var methodCode = $@"
function outMv = {functionName}(inMv1, inMv2)
    arguments
        inMv1 (1,1) {Specs.GetQualifiedMultivectorClassName()}
        inMv2 (1,1) {Specs.GetQualifiedMultivectorClassName()}
    end

{switch1Code}
end
".Trim();

        return methodCode;
    }

    
    public string GetPropertiesCode()
    {
        var codeComposer = new LinearTextComposer();

        codeComposer
            .AppendLine("properties (SetAccess = immutable)")
            .IncreaseIndentation()
            .AppendLine("Grade (1,1) int32,")
            .AppendLine("ScalarCount (1,1) uint32,")
            .AppendLine("SampleCount (1,1) uint32,")
            .AppendLine("Data double");

        if (Specs.Metric.IsConformal)
        {

        }

        codeComposer
            .DecreaseIndentation()
            .Append("end");

        return codeComposer.ToString();
    }

    public string GetConstructorCode()
    {
        var scalarCountCodeComposer = new LinearTextComposer();

        for (var g = 0; g <= VSpaceDimensions; g++)
            scalarCountCodeComposer
                .AppendLine($"case {g}")
                .AppendLine($"    obj.Grade = {g};")
                .AppendLine($"    obj.ScalarCount = {KVectorTypes[g].KvSpaceDimensions};");

        scalarCountCodeComposer
            .AppendLine($"otherwise")
            .AppendLine($"    obj.Grade = -1;")
            .AppendLine($"    obj.ScalarCount = {MultivectorType.GaSpaceDimensions};");

        var scalarCountCode = 
            scalarCountCodeComposer.ToString().PrefixTextLines("        ");

        var methodCode = @$"
function obj = {MultivectorType.ClassName}(grade, data)
    arguments
        grade (1,1) int32
        data (:,:) double
    end
    
    switch grade
{scalarCountCode}
    end

    if (obj.ScalarCount ~= size(data, 1))
        error('Invalid number of scalars per multivector');
    end

    obj.SampleCount = size(data, 2);
    obj.Data = data;
end
".Trim();

        return methodCode;
    }

    public string GetMultivectorPartsCode()
    {
        var partNameList = new List<string>()
        {
            "Scalar",
            "Vector",
            "Bivector",
            "Trivector"
        };

        for (var g = 4; g <= Specs.VSpaceDimensions; g++)
            partNameList.Add(g + "Vector");

        var indexRangeCode = 
            (Specs.VSpaceDimensions + 1).GetRange(
                g =>
                {
                    var (index1, index2) = 
                        Specs.GetKVectorScalarRange(g);

                    return index1 == index2 
                        ? (index1 + 1).ToString() 
                        : $"{index1 + 1}:{index2 + 1}";
                }
        ).ToImmutableArray();

        var codeComposer = new LinearTextComposer();

        for (var g = 0; g <= Specs.VSpaceDimensions; g++)
        {
            var getKVectorPartMethodCode = @$"
function outMv = get{partNameList[g]}Part(inMv)
    arguments
        inMv (1,1) {Specs.GetQualifiedMultivectorClassName()}
    end

    if (inMv.Grade == {g})
        outMvData = inMv.Data;
    elseif (inMv.Grade < 0)
        outMvData = inMv.Data({indexRangeCode[g]},:);
    end

    outMv = {Specs.GetQualifiedMultivectorClassName()}({g}, outMvData);
end
".Trim();

            codeComposer
                .AppendLine(getKVectorPartMethodCode)
                .AppendLine();
        }

        {
            var switchCode = 
                (VSpaceDimensions + 1).GetRange(
                    g =>
                        $"            case {g}{Environment.NewLine}" + 
                        "                " + $"outMvData = inMv.Data({indexRangeCode[g]},:);"
                ).Concatenate(Environment.NewLine);

            var getKVectorPartMethodCode = $@"
function outMv = getKVectorPart(inMv, grade)
    arguments
        inMv (1,1) {Specs.GetQualifiedMultivectorClassName()}
        grade (1,1) int32
    end

    if (grade < 0 || grade > {Specs.VSpaceDimensions})
        error('Invalid k-vector grade');
    end

    if (inMv.Grade == grade)
        outMvData = inMv.Data;
    elseif (inMv.Grade < 0)
        switch grade
{switchCode}
        end
    end

    outMv = {Specs.GetQualifiedMultivectorClassName()}(grade, outMvData);
end
".Trim();

            codeComposer
                .AppendLine(getKVectorPartMethodCode)
                .AppendLine();
        }

        {
            var switchCode = 
                (VSpaceDimensions + 1).GetRange(
                    g =>
                        $"        case {g}{Environment.NewLine}" + 
                        "            " + $"outMvData({indexRangeCode[g]},:) = inMv.Data;"
                ).Concatenate(Environment.NewLine);

            var getFullMultivectorMethodCode = $@"
function outMv = getFullMultivector(inMv)
    arguments
        inMv (1,1) {Specs.GetQualifiedMultivectorClassName()}
    end

    switch inMv.Grade
{switchCode}
        otherwise
            outMvData = inMv.Data;
    end

    outMv = {Specs.GetQualifiedMultivectorClassName()}(-1, outMvData);
end
".Trim();

            codeComposer
                .AppendLine(getFullMultivectorMethodCode)
                .AppendLine();
        }

        {
            var switchCode = 
                (VSpaceDimensions + 1).GetRange(
                    g =>
                        $"        case {g}{Environment.NewLine}" + 
                        "            " + $"outMvData({indexRangeCode[g]},:) = inMv.Data;"
                ).Concatenate(Environment.NewLine);

            var getDataArrayMethodCode = $@"
function outMvData = getDataArray(inMv)
    arguments
        inMv (1,1) {Specs.GetQualifiedMultivectorClassName()}
    end

    outMvData = inMv.Data;
end

function outMvData = getFullMultivectorDataArray(inMv)
    arguments
        inMv (1,1) {Specs.GetQualifiedMultivectorClassName()}
    end

    outMvData = zeros({GaSpaceDimensions}, inMv.SampleCount, 'double');

    switch inMv.Grade
{switchCode}
        otherwise
            outMvData = inMv.Data;
    end
end
".Trim();

            codeComposer
                .AppendLine(getDataArrayMethodCode)
                .AppendLine();
        }

        return codeComposer.ToString();
    }

    public string GetUnaryOperationsCode()
    {
        var codeComposer = new LinearTextComposer();

        codeComposer
            .AppendLine(GetUnilinearBasisToBasisMappingCode(
                "negative",
                b1 => b1.Negative()
            )).AppendLine()
            .AppendLine(GetUnilinearBasisToBasisMappingCode(
                "reverse",
                b1 => b1.Reverse()
            )).AppendLine()
            .AppendLine(GetUnilinearBasisToBasisMappingCode(
                "gradeInvolution",
                b1 => b1.GradeInvolution()
            )).AppendLine()
            .AppendLine(GetUnilinearBasisToBasisMappingCode(
                "cliffordConjugate",
                b1 => b1.CliffordConjugate()
            )).AppendLine()
            .AppendLine(GetUnilinearBasisToBasisMappingCode(
                "conjugate",
                b1 => b1.Conjugate()
            )).AppendLine();
        
        if (!Metric.IsDegenerate)
        {
            var basisPseudoScalarInverse =
                Metric.CreateBasisPseudoScalarInverse(VSpaceDimensions);

            codeComposer
                .AppendLine(GetUnilinearBasisToBasisMappingCode(
                    "dual",
                    b1 => b1.Lcp(basisPseudoScalarInverse)
                )).AppendLine();
        }

        var basisPseudoScalar = 
            Metric.CreateBasisPseudoScalar(VSpaceDimensions);

        codeComposer
            .AppendLine(GetUnilinearBasisToBasisMappingCode(
                "unDual",
                b1 => b1.Lcp(basisPseudoScalar)
            )).AppendLine();

        codeComposer.AppendLine(
            $@"
function outMv = inverse(inMv)
    arguments
        inMv (1,1) {Specs.GetQualifiedMultivectorClassName()}
    end

    reverseMv = inMv.reverse();
    outMv = reverseMv.mrdivide(inMv.sp(reverseMv));
end

function outMv = pseudoInverse(inMv)
    arguments
        inMv (1,1) {Specs.GetQualifiedMultivectorClassName()}
    end

    conjugateMv = inMv.conjugate();
    outMv = conjugateMv.mrdivide(inMv.sp(conjugateMv));
end
".Trim()
        );

        return codeComposer.ToString();
    }

    public string GetUnaryPlusMinusCode()
    {
        return @$"
function outMv = uplus(inMv)
    arguments
        inMv (1,1) {Specs.GetQualifiedMultivectorClassName()}
    end

    outMv = {Specs.GetQualifiedMultivectorClassName()}(inMv.Grade, inMv.Data);
end

function outMv = uminus(inMv)
    arguments
        inMv (1,1) {Specs.GetQualifiedMultivectorClassName()}
    end

    outMv = {Specs.GetQualifiedMultivectorClassName()}(inMv.Grade, -inMv.Data);
end
".Trim();
    }
    
    public string GetUnaryTimesDivideScalarCode()
    {
        return @$"
function outMv = mtimes(inMv, inScalar)
    arguments
        inMv (1,1) {Specs.GetQualifiedMultivectorClassName()}
        inScalar (:,:) double
    end

    inScalarSize = size(inScalar);

    if (inScalarSize(1) == 1 && inScalarSize(2) == 1)
        outMv = {Specs.GetQualifiedMultivectorClassName()}(inMv.Grade, inMv.Data * inScalar);
    elseif (inScalarSize(1) == 1 && inScalarSize(2) == inMv.SampleCount)
        outMv = {Specs.GetQualifiedMultivectorClassName()}(inMv.Grade, inMv.Data .* repmat(inScalar, [inMv.ScalarCount, 1]));
    else
        error('Invalid size of scalar array inScalar');
    end
end

function outMv = mrdivide(inMv, inScalar)
    arguments
        inMv (1,1) {Specs.GetQualifiedMultivectorClassName()}
        inScalar (:,:) double
    end

    inScalarSize = size(inScalar);

    if (inScalarSize(1) == 1 && inScalarSize(2) == 1)
        outMv = {Specs.GetQualifiedMultivectorClassName()}(inMv.Grade, inMv.Data / inScalar);
    elseif (inScalarSize(1) == 1 && inScalarSize(2) == inMv.SampleCount)
        outMv = {Specs.GetQualifiedMultivectorClassName()}(inMv.Grade, inMv.Data ./ repmat(inScalar, [inMv.ScalarCount, 1]));
    else
        error('Invalid size of scalar array inScalar');
    end
end
".Trim();
    }

    public string GetNormOperationsCode()
    {
        var codeComposer = new LinearTextComposer();

        codeComposer
            .AppendLine(GetSingleInputBilinearBasisToScalarMappingCode(
                "normSquared",
                (b1, b2) => b1.Sp(b2.Reverse())
            )).AppendLine();

        codeComposer.AppendLine($@"
function outScalar = norm(inMv)
    arguments
        inMv (1,1) {Specs.GetQualifiedMultivectorClassName()}
    end

    outScalar = sqrt(abs(inMv.normSquared()));
end
".Trim()
        ).AppendLine();
        
        codeComposer
            .AppendLine(GetSingleInputBilinearBasisToScalarMappingCode(
                "pseudoNormSquared",
                (b1, b2) => b1.Sp(b2.Conjugate())
            )).AppendLine();

        codeComposer.AppendLine($@"
function outScalar = pseudoNorm(inMv)
    arguments
        inMv (1,1) {Specs.GetQualifiedMultivectorClassName()}
    end

    outScalar = sqrt(abs(inMv.pseudoNormSquared()));
end
".Trim()
        ).AppendLine();

        return codeComposer.ToString();
    }

    public string GetBinaryOperationsCode()
    {
        var codeComposer = new LinearTextComposer();

        codeComposer
            .AppendLine(GetBilinearBasisToBasisMappingCode(
                "sp",
                (b1, b2) => b1.Sp(b2), 
                false
            )).AppendLine()
            .AppendLine(GetBilinearBasisToBasisMappingCode(
                "op",
                (b1, b2) => b1.Op(b2))
            ).AppendLine()
            .AppendLine(GetBilinearBasisToBasisMappingCode(
                "lcp",
                (b1, b2) => b1.Lcp(b2))
            ).AppendLine()
            .AppendLine(GetBilinearBasisToBasisMappingCode(
                "rcp",
                (b1, b2) => b1.Rcp(b2))
            ).AppendLine()
            .AppendLine(GetBilinearBasisToBasisMappingCode(
                "fdp",
                (b1, b2) => b1.Fdp(b2))
            ).AppendLine()
            .AppendLine(GetBilinearBasisToBasisMappingCode(
                "cp",
                (b1, b2) => b1.Cp(b2))
            ).AppendLine()
            .AppendLine(GetBilinearBasisToBasisMappingCode(
                "acp",
                (b1, b2) => b1.Acp(b2))
            ).AppendLine()
            .AppendLine(GetBilinearBasisToBasisMappingCode(
                "gp",
                (b1, b2) => b1.Gp(b2))
            ).AppendLine();

        return codeComposer.ToString();
    }

    public string GetBinaryPlusMinusCode()
    {
        var indexRangeCode = GetIndexRangeCode();
        
        var switch1Code = 
            (VSpaceDimensions + 1).GetRange(
                g =>
                    $"            case {g}{Environment.NewLine}" + 
                    "                " + $"outMvData({indexRangeCode[g]},:) = inMv1Data;"
            ).Concatenate(Environment.NewLine);
        
        var switch2Code = 
            (VSpaceDimensions + 1).GetRange(
                g =>
                    $"            case {g}{Environment.NewLine}" + 
                    "                " + $"outMvData({indexRangeCode[g]},:) = outMvData({indexRangeCode[g]},:) + inMv2Data;"
            ).Concatenate(Environment.NewLine);
        
        var switch3Code = 
            (VSpaceDimensions + 1).GetRange(
                g =>
                    $"            case {g}{Environment.NewLine}" + 
                    "                " + $"outMvData({indexRangeCode[g]},:) = outMvData({indexRangeCode[g]},:) - inMv2Data;"
            ).Concatenate(Environment.NewLine);

        var plusMethodCode = @$"
function outMv = plus(inMv1, inMv2)
    arguments
        inMv1 (1,1) {Specs.GetQualifiedMultivectorClassName()}
        inMv2 (1,1) {Specs.GetQualifiedMultivectorClassName()}
    end

    sampleCount1 = size(inMv1, 2);
    sampleCount2 = size(inMv2, 2);

    if (sampleCount1 ~= sampleCount2 && sampleCount1 ~= 1 && sampleCount2 ~= 1)
        error('Number of samples in both inputs must either match or equal 1');
    end

    sampleCount = max(sampleCount1, sampleCount2);

    if (sampleCount1 == sampleCount)
        inMv1Data = inMv1.Data;
    else
        inMv1Data = repmat(inMv1.Data, 1, sampleCount);
    end

    if (sampleCount2 == sampleCount)
        inMv2Data = inMv2.Data;
    else
        inMv2Data = repmat(inMv2.Data, 1, sampleCount);
    end

    if (inMv1.Grade == inMv2.Grade)
        outMv = {Specs.GetQualifiedMultivectorClassName()}(inMv1.Grade, inMv1Data + inMv2Data);
    else
        outMvData = zeros({Specs.MultivectorType.ScalarCount}, sampleCount, 'double');

        switch inMv1.Grade
{switch1Code}
            otherwise
                outMvData = inMv1Data;
        end
        
        switch inMv2.Grade
{switch2Code}
            otherwise
                outMvData = outMvData + inMv2Data;
        end

        outMv = {Specs.GetQualifiedMultivectorClassName()}(-1, outMvData);
    end
end
".Trim();

        var minusMethodCode = @$"
function outMv = minus(inMv1, inMv2)
    arguments
        inMv1 (1,1) {Specs.GetQualifiedMultivectorClassName()}
        inMv2 (1,1) {Specs.GetQualifiedMultivectorClassName()}
    end

    sampleCount1 = size(inMv1, 2);
    sampleCount2 = size(inMv2, 2);

    if (sampleCount1 ~= sampleCount2 && sampleCount1 ~= 1 && sampleCount2 ~= 1)
        error('Number of samples in both inputs must either match or equal 1');
    end

    sampleCount = max(sampleCount1, sampleCount2);

    if (sampleCount1 == sampleCount)
        inMv1Data = inMv1.Data;
    else
        inMv1Data = repmat(inMv1.Data, 1, sampleCount);
    end

    if (sampleCount2 == sampleCount)
        inMv2Data = inMv2.Data;
    else
        inMv2Data = repmat(inMv2.Data, 1, sampleCount);
    end

    if (inMv1.Grade == inMv2.Grade)
        outMv = {Specs.GetQualifiedMultivectorClassName()}(inMv1.Grade, inMv1Data - inMv2Data);
    else
        outMvData = zeros({Specs.MultivectorType.ScalarCount}, sampleCount, 'double');

        switch inMv1.Grade
{switch1Code}
            otherwise
                outMvData = inMv1Data;
        end
        
        switch inMv2.Grade
{switch3Code}
            otherwise
                outMvData = outMvData - inMv2Data;
        end

        outMv = {Specs.GetQualifiedMultivectorClassName()}(-1, outMvData);
    end
end
".Trim();

        var codeComposer = new LinearTextComposer();

        codeComposer
            .AppendLine(plusMethodCode)
            .AppendLine()
            .AppendLine(minusMethodCode)
            .AppendLine();

        return codeComposer.ToString();
    }

    public string GetLaTeXCode()
    {
        var switchCodeComposer = new LinearTextComposer();

        switchCodeComposer
            .IncreaseIndentation()
            .AppendLine("switch inMv.Grade")
            .IncreaseIndentation();

        for (var grade = 0; grade <= VSpaceDimensions; grade++)
        {
            var basisBladeLaTeXCode = GetBasisBladeLaTeXCode(grade);

            switchCodeComposer
                .AppendLine($"case {grade}")
                .AppendLine($"    basisText = {basisBladeLaTeXCode};");
        }

        {
            var basisBladeLaTeXCode = GetBasisBladeLaTeXCode();

            switchCodeComposer
                .AppendLine($"otherwise")
                .AppendLine($"    basisText = {basisBladeLaTeXCode};");
        }

        switchCodeComposer
            .DecreaseIndentation()
            .AppendLineAtNewLine("end");

        return @$"
function outText = getLaTeX(inMv)
	arguments
		inMv (1,1) {Specs.GetQualifiedMultivectorClassName()}
	end

{switchCodeComposer}
	
	outText = strings(1, inMv.SampleCount);
	
	for s = 1:inMv.SampleCount
		[i,~,v] = find(inMv.Data(:,s));

        if (isempty(i))
            outText(1, s) = ""0"";
        else
    		outText(1, s) = extractAfter(join(""+ ("" + string(v) + "") "" + basisText(i)), 2);
        end
	end
end
".Trim();
    }

    
    public string GetMethodsCode()
    {
        var codeComposer = new LinearTextComposer();

        codeComposer
            .AppendLine("methods")
            .IncreaseIndentation()
            .AppendLineAtNewLine(GetConstructorCode())
            .AppendLine()
            .AppendLine(GetMultivectorPartsCode())
            .AppendLine()
            .AppendLine(GetUnaryPlusMinusCode())
            .AppendLine()
            .AppendLine(GetUnaryTimesDivideScalarCode())
            .AppendLine()
            .AppendLine(GetNormOperationsCode())
            .AppendLine()
            .AppendLine(GetUnaryOperationsCode())
            .AppendLine()
            .AppendLine(GetBinaryPlusMinusCode())
            .AppendLine()
            .AppendLine(GetBinaryOperationsCode())
            .AppendLine()
            .AppendLine(GetLaTeXCode())
            .AppendLine()
            .DecreaseIndentation()
            .AppendLine("end");

        return codeComposer.ToString();
    }

    public override TextFilesComposer GenerateCode()
    {
        var mvClassName =
            MultivectorType.ClassName;

        CodeFilesComposer.InitializeFile(mvClassName + ".m");

        var codeComposer = CodeFilesComposer.ActiveFileTextComposer;

        codeComposer
            .AppendLine(Specs.GetFileHeaderText())
            .AppendLine($"classdef {MultivectorType.ClassName}")
            .IncreaseIndentation()
            .AppendLineAtNewLine(GetPropertiesCode())
            .AppendLine()
            .AppendLine(GetMethodsCode())
            .DecreaseIndentation()
            .AppendLineAtNewLine("end");

        CodeFilesComposer.ActiveFileComposer.FinalizeText(
            code => code.RemoveRepeatedEmptyLines()
        );

        return CodeFilesComposer;
    }
}