﻿using System.Collections.Generic;

namespace TextComposerLib.Code.JavaScript.LibraryComposers
{
    public class JsClassConstructorData :
        JsClassMemberDefinitionData
    {
        public override string MemberJsName 
            => string.Empty;

        public List<JsFunctionArgumentData> ArgumentDataList { get; }
            = new List<JsFunctionArgumentData>();


        internal JsClassConstructorData(JsClassDefinitionData classData) 
            : base(classData)
        {
        }
    }

}