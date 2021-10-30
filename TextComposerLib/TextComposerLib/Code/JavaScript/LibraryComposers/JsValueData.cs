﻿using System.Diagnostics.CodeAnalysis;

namespace TextComposerLib.Code.JavaScript.LibraryComposers
{
    public abstract class JsValueData
    {
        public JsClassNameData ValueType { get; }


        protected JsValueData([NotNull] JsClassNameData valueType)
        {
            ValueType = valueType;
        }

        //protected JsValueData()
        //{
        //    throw new NotImplementedException();
        //}


        public abstract string GetJsCode();

        public abstract string GetCsCode();
    }
}