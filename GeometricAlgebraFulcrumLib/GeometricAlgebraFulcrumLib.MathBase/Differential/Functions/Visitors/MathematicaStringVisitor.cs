using DataStructuresLib;
using Microsoft.CSharp.RuntimeBinder;
using TextComposerLib.Text;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.MathBase.Differential.Functions.Visitors
{
    public class MathematicaStringVisitor :
        IDynamicTreeVisitor<DifferentialFunction, string>
    {
        public static MathematicaStringVisitor DefaultVisitor { get; }
            = new MathematicaStringVisitor();


        public bool UseExceptions
            => true;

        public bool IgnoreNullElements 
            => false;


        public string VarName { get; set; } = "x";


        public string Fallback(DifferentialFunction item, RuntimeBinderException excException)
        {
            throw new NotImplementedException();
        }


        public string Visit(DfConstant dfConstant)
        {
            return dfConstant.Value.ToString("G");
        }
        
        public string Visit(DfVar dfVar)
        {
            return VarName;
        }
        
        public string Visit(DfCos dfCos)
        {
            if (dfCos.TreeDepth <= 3)
            {
                var arg1Text = dfCos.Argument.AcceptVisitor(this);

                return $"Cos[{arg1Text}]";
            }

            var composer = new LinearTextComposer();

            var argText =
                dfCos.Argument.AcceptVisitor(this);

            composer
                .AppendLine("Cos[")
                .IncreaseIndentation()
                .Append(argText)
                .DecreaseIndentation()
                .AppendAtNewLine("]");

            return composer.ToString();
        }
        
        public string Visit(DfSin dfSin)
        {
            if (dfSin.TreeDepth <= 3)
            {
                var arg1Text = dfSin.Argument.AcceptVisitor(this);

                return $"Sin[{arg1Text}]";
            }

            var composer = new LinearTextComposer();

            var argText =
                dfSin.Argument.AcceptVisitor(this);

            composer
                .AppendLine("Sin[")
                .IncreaseIndentation()
                .Append(argText)
                .DecreaseIndentation()
                .AppendAtNewLine("]");

            return composer.ToString();
        }
        
        public string Visit(DfExp dfExp)
        {
            if (dfExp.TreeDepth <= 3)
            {
                var arg1Text = dfExp.Argument.AcceptVisitor(this);

                return $"Exp[{arg1Text}]";
            }

            var composer = new LinearTextComposer();

            var argText =
                dfExp.Argument.AcceptVisitor(this);

            composer
                .AppendLine("Exp[")
                .IncreaseIndentation()
                .Append(argText)
                .DecreaseIndentation()
                .AppendAtNewLine("]");

            return composer.ToString();
        }
        
        public string Visit(DfPowerScalar dfPowerScalar)
        {
            var powerText = dfPowerScalar.PowerValue.ToString("G");

            if (dfPowerScalar.TreeDepth <= 3)
            {
                var arg1Text = dfPowerScalar.Argument.AcceptVisitor(this);

                return $"Power[{arg1Text}, {powerText}]";
            }

            var composer = new LinearTextComposer();

            var argText =
                dfPowerScalar.Argument.AcceptVisitor(this);

            composer
                .AppendLine("Power[")
                .IncreaseIndentation()
                .Append(argText)
                .AppendLine(",")
                .Append(powerText)
                .DecreaseIndentation()
                .AppendAtNewLine("]");

            return composer.ToString();
        }

        public string Visit(DfTimes dfTimes)
        {
            if (dfTimes.TreeDepth <= 3)
            {
                return dfTimes
                    .Arguments
                    .Select(f => f.AcceptVisitor(this))
                    .Concatenate(", ", "Times[", "]");
            }

            var composer = new LinearTextComposer();

            var argText =
                dfTimes
                    .Arguments
                    .Select(f => f.AcceptVisitor(this))
                    .Concatenate("," + Environment.NewLine);

            composer
                .AppendLine("Times[")
                .IncreaseIndentation()
                .Append(argText)
                .DecreaseIndentation()
                .AppendAtNewLine("]");

            return composer.ToString();
        }

        public string Visit(DfPlus dfPlus)
        {
            if (dfPlus.TreeDepth <= 3)
            {
                return dfPlus
                    .Arguments
                    .Select(f => f.AcceptVisitor(this))
                    .Concatenate(", ", "Plus[", "]");
            }

            var composer = new LinearTextComposer();

            var argText =
                dfPlus
                    .Arguments
                    .Select(f => f.AcceptVisitor(this))
                    .Concatenate("," + Environment.NewLine);

            composer
                .AppendLine("Plus[")
                .IncreaseIndentation()
                .Append(argText)
                .DecreaseIndentation()
                .AppendAtNewLine("]");

            return composer.ToString();
        }

        public string Visit(DifferentialCustomFunction dfCustom)
        {
            return dfCustom.Name;
        }
    }
}
