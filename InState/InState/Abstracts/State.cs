using System.Collections.Generic;
using System.Threading.Tasks;
using InState.Behaviors;
using InState.Enums;

namespace InState.Abstracts
{
    public abstract class State<T>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public State()
        {
            Data = new List<T>();
            PermittedTransitions = new List<TransitionBehavior<T>>();
        }

        /// <summary>
        /// Indicates the state data that has already been processed
        /// </summary>
        /// <value></value>
        public List<T> Data { get; set; }

        /// <summary>
        /// Indicates the name of the state
        /// </summary>
        /// <value>string</value>
        public abstract string Name { get; }

        /// <summary>
        /// The transitions that are permitted for this state
        /// </summary>
        /// <value>IList<TransitionBehavior<T>></value>
        internal IList<TransitionBehavior<T>> PermittedTransitions { get; private set; }

        /// <summary>
        /// Process the provided state data
        /// </summary>
        /// <returns>Task<State<T>></returns>
        public virtual async Task<State<T>> Process() { return null; }

        /// <summary>
        /// Process the provided state data
        /// </summary>
        /// <param name="stateData">Indicates the state-specific data to process</param>
        /// <returns>Task<State<T>></returns>
        public virtual async Task<State<T>> Process(object stateData) { return null; }

        /// <summary>
        /// Process the provided state data
        /// </summary>
        /// <param name="stateData">Indicates the state-specific data to process</param>
        /// <typeparam name="V">Identifies the type of the state-specific data to process</typeparam>
        /// <returns>Task<State<T>></returns>
        public virtual async Task<State<T>> Process<V>(V stateData) { return null; }

        /// <summary>
        /// Process the provided state data
        /// </summary>
        /// <param name="firstDataItem">Indicates the first state-specific data item to process<</param>
        /// <param name="secondDataItem">Indicates the second state-specific data item to process</param>
        /// <typeparam name="V">Identifies the type of the first state-specific data item to process</typeparam>
        /// <typeparam name="Z">Identifies the type of the first state-specific data item to process</typeparam>
        /// <returns></returns>
        public virtual async Task<State<T>> Process<V, Z>(V firstDataItem, Z secondDataItem) { return null; }

        /// <summary>
        /// Associates a new transition behavior with the specified trigger which is used to define
        /// the permitted transitions
        /// </summary>
        /// <param name="trigger">Trigger enum</param>
        /// <returns>TransitionBehavior<T></returns>
        public TransitionBehavior<T> When(Triggers trigger)
        {
            TransitionBehavior<T> transitionBehavior = new TransitionBehavior<T>(trigger, this);
            PermittedTransitions.Add(transitionBehavior);

            return transitionBehavior;
        }
    }
}
