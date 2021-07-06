using System;
using System.Collections.Generic;
using System.Diagnostics;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Symbolic.Mathematica
{
    public sealed class MathematicaConnection
    {
        private IKernelLink _internalLink;

        public Stopwatch Stopwatch { get; } = new Stopwatch();


        public MathematicaInterface Cas { get; private set; }

        public IKernelLink KernelLink
        {
            get
            {
                if (!ReferenceEquals(_internalLink, null)) 
                    return _internalLink;

                //This launches the Mathematica kernel:
                _internalLink = MathLinkFactory.CreateKernelLink();

                //Discard the initial InputNamePacket the kernel will send when launched.
                _internalLink.WaitAndDiscardAnswer();

                //Pass objects by reference to mathematica to speed processing
                //_internalLink.EnableObjectReferences();

                EvaluateInitializationScript();

                return _internalLink;
            }
        }

        public bool HasError 
            => _internalLink.Error != 0;

        public string LastErrorMessage 
            => _internalLink.ErrorMessage;

        public Exception LastException 
            => _internalLink.LastError;

        public bool IsReady 
            => _internalLink != null;

        public bool EnableCache { get; set; }
            = false;

        public int CacheHits { get; private set; }

        public int CacheMisses { get; private set; }

        private readonly Dictionary<string, Expr> _exprCache 
            = new Dictionary<string, Expr>();


        internal MathematicaConnection(MathematicaInterface parentCas)
        {
            Cas = parentCas;
        }


        private void EvaluateInitializationScript()
        {
            const string script = @"diagonalQ[m_List]/;ArrayDepth[m]===2&&Equal@@Dimensions[m]:=And@@Flatten[MapIndexed[#1===0||Equal@@#2&,m,{2}]]; diagonalQ[m_]:=Return[False];";

            EvaluateToExpr(script);
        }

        public bool CloseLink()
        {
            if (!IsReady) 
                return true;

            _internalLink.Close();
            _internalLink = null;

            return true;
        }

        public bool ClearErrors()
        {
            return ReferenceEquals(_internalLink, null) || 
                   _internalLink.ClearError();
        }


        //public Image EvaluateToImage(Expr exprObject, int width = 0, int height = 0)
        //{
        //    CacheMisses++;

        //    Stopwatch.Start();

        //    ClearErrors();
        //    var result = KernelLink.EvaluateToImage(exprObject, width, height);

        //    Stopwatch.Stop();

        //    return result;
        //}

        //public Image EvaluateToImage(string exprText, int width = 0, int height = 0)
        //{
        //    CacheMisses++;

        //    Stopwatch.Start();

        //    ClearErrors();
        //    var img = KernelLink.EvaluateToImage(exprText, width, height);

        //    Stopwatch.Stop();

        //    return img;
        //}

        public string EvaluateToInputForm(Expr exprObject, int pageWidth = 0)
        {
            CacheMisses++;

            Stopwatch.Start();

            ClearErrors();
            var result = KernelLink.EvaluateToInputForm(exprObject, pageWidth);

            Stopwatch.Stop();

            return result;
        }

        public string EvaluateToInputForm(string exprText, int pageWidth = 0)
        {
            CacheMisses++;

            Stopwatch.Start();

            ClearErrors();
            var result = KernelLink.EvaluateToInputForm(exprText, pageWidth);

            Stopwatch.Stop();

            return result;
        }

        public string EvaluateToOutputForm(Expr exprObject, int pageWidth = 0)
        {
            CacheMisses++;

            Stopwatch.Start();

            ClearErrors();
            var result = KernelLink.EvaluateToOutputForm(exprObject, pageWidth);

            Stopwatch.Stop();

            return result;
        }

        public string EvaluateToOutputForm(string exprText, int pageWidth = 0)
        {
            CacheMisses++;

            Stopwatch.Start();

            ClearErrors();
            var result = KernelLink.EvaluateToOutputForm(exprText, pageWidth);

            Stopwatch.Stop();

            return result;
        }

        //public Image EvaluateToTypeset(Expr exprObject, int width = 0)
        //{
        //    CacheMisses++;

        //    Stopwatch.Start();

        //    ClearErrors();
        //    var result = KernelLink.EvaluateToTypeset(exprObject, width);

        //    Stopwatch.Stop();

        //    return result;
        //}

        //public Image EvaluateToTypeset(string exprText, int width = 0)
        //{
        //    CacheMisses++;

        //    Stopwatch.Start();

        //    ClearErrors();
        //    var result = KernelLink.EvaluateToTypeset(exprText, width);

        //    Stopwatch.Stop();

        //    return result;
        //}


        public Expr EvaluateToExpr(Expr exprObject)
        {
            Stopwatch.Start();

            string exprText = null;

            if (EnableCache && _exprCache.TryGetValue(exprText = exprObject.ToString(), out var outExpr))
            {
                CacheHits++;

                Stopwatch.Stop();

                return outExpr;
            }

            ClearErrors();
            KernelLink.Evaluate(exprObject);
            KernelLink.WaitForAnswer();

            outExpr = KernelLink.GetExpr();

            CacheMisses++;

            if (EnableCache)
                _exprCache.Add(exprText ?? exprObject.ToString(), outExpr);

            //if (outExpr.ToString() == "Indeterminate")
            //{
            //    var x = 0;
            //}

            Stopwatch.Stop();

            return outExpr;
        }

        public Expr EvaluateToExpr(string exprText)
        {
            Stopwatch.Start();

            if (EnableCache && _exprCache.TryGetValue(exprText, out var outExpr))
            {
                CacheHits++;

                Stopwatch.Stop();

                return outExpr;
            }

            ClearErrors();
            KernelLink.Evaluate(exprText);
            KernelLink.WaitForAnswer();

            outExpr = KernelLink.GetExpr();

            CacheMisses++;

            if (EnableCache)
                _exprCache.Add(exprText, outExpr);

            //if (outExpr.ToString() == "Indeterminate")
            //{
            //    var x = 0;
            //}

            Stopwatch.Stop();

            return outExpr;
        }

        public string EvaluateToString(Expr exprObject)
        {
            Stopwatch.Start();

            //string expr_text = expr_object.ToString();

            ClearErrors();
            KernelLink.Evaluate(exprObject);
            KernelLink.WaitForAnswer();

            var outExpr = KernelLink.GetString();

            CacheMisses++;

            Stopwatch.Stop();

            return outExpr;
        }

        public string EvaluateToString(string exprText)
        {
            Stopwatch.Start();

            ClearErrors();
            KernelLink.Evaluate(exprText);
            KernelLink.WaitForAnswer();

            var outExpr = KernelLink.GetString();

            CacheMisses++;

            Stopwatch.Stop();

            return outExpr;
        }
    }
}
