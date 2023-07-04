//using System.Collections;

//namespace GraphicsComposerLib.Rendering.KonvaJs
//{
//    public sealed class GrKonvaJsObjectList :
//        IReadOnlyList<GrKonvaJsObject>
//    {
//        private readonly Dictionary<string, GrKonvaJsObject> _objectDictionary 
//            = new Dictionary<string, GrKonvaJsObject>();


//        public int Count 
//            => _objectDictionary.Count;

//        public GrKonvaJsObject this[int index] 
//            => _objectDictionary.Skip(index).First().Value;
    
//        public GrKonvaJsObject this[string constName] 
//            => _objectDictionary[constName];


//        public GrKonvaJsObjectList Add(GrKonvaJsObject babylonObject)
//        {
//            _objectDictionary.Add(babylonObject.ConstName, babylonObject);

//            return this;
//        }

//        public bool Contains(GrKonvaJsObject babylonObject)
//        {
//            return _objectDictionary.TryGetValue(babylonObject.ConstName, out var babylonObject1) && 
//                   ReferenceEquals(babylonObject, babylonObject1);
//        }
        
//        public bool TryGetObject(string name, out GrKonvaJsObject? babylonObject)
//        {
//            return _objectDictionary.TryGetValue(name, out babylonObject);
//        }

//        public IEnumerator<GrKonvaJsObject> GetEnumerator()
//        {
//            return _objectDictionary.Values.GetEnumerator();
//        }

//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return GetEnumerator();
//        }
//    }
//}