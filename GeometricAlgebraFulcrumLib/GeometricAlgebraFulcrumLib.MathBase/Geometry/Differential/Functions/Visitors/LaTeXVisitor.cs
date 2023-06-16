using DataStructuresLib;
using Microsoft.CSharp.RuntimeBinder;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Differential.Functions.Visitors
{
    public class LaTeXVisitor :
        IDynamicTreeVisitor<DifferentialFunction, string>
    {
        public static LaTeXVisitor DefaultVisitor { get; }
            = new LaTeXVisitor();


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
            var argText = dfCos.Argument.AcceptVisitor(this);

            return @$"\cos \left[ {argText} \right]";
        }
    
        public string Visit(DfSin dfSin)
        {
            var argText = dfSin.Argument.AcceptVisitor(this);

            return @$"\sin \left[ {argText} \right]";
        }
    
        public string Visit(DfExp dfExp)
        {
            var argText = dfExp.Argument.AcceptVisitor(this);

            return @$"\exp \left[ {argText} \right]";
        }
        
        public string Visit(DfPowerScalar dfPowerScalar)
        {
            var argText = dfPowerScalar.Argument.AcceptVisitor(this);
            var powerText = dfPowerScalar.PowerValue.ToString("G");

            return @$"{argText}^{{{powerText}}}";
        }

        public string Visit(DfTimes dfTimes)
        {
            return dfTimes
                .Arguments
                .Select(f => f.AcceptVisitor(this))
                .Concatenate(" ", @" \left( ", @" \right) ");
        }

        public string Visit(DfPlus dfPlus)
        {
            return dfPlus
                .Arguments
                .Select(f => f.AcceptVisitor(this))
                .Concatenate(" + ");
        }


    }
}