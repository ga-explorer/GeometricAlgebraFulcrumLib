//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using DataStructuresLib.Collections.Queues;

//namespace GeometricAlgebraFulcrumLib.Lite.PropagatorNetworks.Converted
//{
//    public class Scheduler
//    {
//        private SetQueue<Propagator> _alerted_propagators = new SetQueue<Propagator>();
//        private SetQueue<Propagator> _propagators_ever_alerted = new SetQueue<Propagator>();
//        private Stack<Propagator> _abort_process_stack = new Stack<Propagator>();
//        private object? _last_value_of_run = null;

//        public Scheduler()
//        {
            
//        }


//        /// <summary>
//        /// Initialize the scheduler, emptying its queues and registers.
//        /// </summary>
//        public void Initialize()
//        {
//            _alerted_propagators.Clear();
//            _propagators_ever_alerted.Clear();
//            _abort_process_stack.Clear();
//            _last_value_of_run = null;
//        }


//    }
//}
