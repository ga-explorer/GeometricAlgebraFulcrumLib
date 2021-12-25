﻿using System.Collections.Generic;

namespace TextComposerLib.Text.Parametric
{
    public sealed class UniqueNameComposer : 
        ParametricTextComposer
    {
        public static readonly string NameParamName = "name";

        public static readonly string IndexParamName = "index";


        public string IndexFormatString { get; set; }

        private readonly Dictionary<string, int> _namesDictionary = new Dictionary<string, int>();


        public string NameParamValue
        {
            get { return this[NameParamName]; }
            set { this[NameParamName] = value; }
        }

        public string IndexParamValue
        {
            get { return this[IndexParamName]; }
            set { this[IndexParamName] = value; }
        }


        public UniqueNameComposer()
            : base("#", "#", "#name##index#")
        {
        }

        public UniqueNameComposer(string leftDelimiter, string rightDelimiter)
            : base(leftDelimiter, rightDelimiter)
        {
            
        }

        public UniqueNameComposer(string leftDelimiter, string rightDelimiter, string templateText)
            : base(leftDelimiter, rightDelimiter, templateText)
        {

        }


        public void Reset()
        {
            _namesDictionary.Clear();

            ClearBindings();
        }

        public string GetUniqueName(string baseName)
        {
            if (_namesDictionary.ContainsKey(baseName))
            {
                var index = _namesDictionary[baseName] + 1;

                if (ContainsParameter(NameParamName))
                    this[NameParamName] = baseName;

                if (ContainsParameter(IndexParamName))
                    this[IndexParamName] =
                        string.IsNullOrEmpty(IndexFormatString)
                        ? index.ToString()
                        : index.ToString(IndexFormatString);

                var uniqueName = GenerateText();

                _namesDictionary[baseName] = index;

                return uniqueName;
            }

            _namesDictionary.Add(baseName, 0);

            return baseName;
        }
    }
}
