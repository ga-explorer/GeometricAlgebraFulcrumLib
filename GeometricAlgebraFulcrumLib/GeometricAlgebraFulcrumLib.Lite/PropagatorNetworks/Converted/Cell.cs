//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace GeometricAlgebraFulcrumLib.Lite.PropagatorNetworks.Converted
//{
//    /// <summary>
//    /// The storage unit of the propagator network.
//    /// Each cell may have content, stored in the `content` attribute -- that is
//    /// `None` if there is no content.
//    /// Each cell may have neighbors, stored in the `neighbors` attribute. It is
//    /// a list of propagators that are interested in the cell's content.
//    /// When the cell receives content, it alerts all neighbor propagators, so
//    /// they can update other cells based on this new content.
//    /// </summary>
//    public class Cell
//    {
//        public readonly HashSet<object> Neighbors = new HashSet<object>();

//        public string Name { get; }

//        public object? Content { get; set; }

        
//        /// <summary>
//        /// Initialize a `Cell` object, with no neighbors.
//        /// </summary>
//        /// <param name="name"></param>
//        /// <param name="content">if provided, it is added as the cell's content.</param>
//        public Cell(string name = "", object? content = null)
//        {
//            Name = name;
//            Content = content;

//            AddContent(content);

//            Console.WriteLine($"New cell {ToString()}");
//        }


//        /// <summary>
//        /// Add a propagator to the cell's neighbors, and alert it using the scheduler.
//        /// The propagator is added only if it isn't already a neighbor.
//        /// </summary>
//        /// <param name="n"></param>
//        public void NewNeighbor(object n)
//        {
//            if (!Neighbors.Add(n)) return;

//            // scheduler.alert_propagators(n)
//        }

//        /// <summary>
//        /// Add content to the cell and alert its neighbors if the cell is empty.
//        /// If the content to be added is `None` or is equal to the cell's
//        /// content, nothing is done.
//        /// If there is content in the cell, and it differs from the parameter's
//        /// content, there is inconsistency in the system; it raises a `ValueError`.
//        /// </summary>
//        /// <param name="increment">the content to be added.</param>
//        public void AddContent(object? increment)
//        {
//            var answer = merge(Content, increment);

//            if (answer != Content)
//            {
//                Console.WriteLine($"Adding content {answer} to {this}");

//                Content = answer;

//                //scheduler.alert_propagators(Neighbors);
//            }
//        }

//        public sealed override string ToString()
//        {
//            var nameText = string.IsNullOrEmpty(Name) ? string.Empty : Name;
//            var contentText = Content?.ToString() ?? "<empty>";

//            return $"Cell({nameText}, {contentText})";
//        }
//    }
//}
