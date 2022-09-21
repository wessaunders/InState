using System.Collections.Generic;
using System.Threading.Tasks;
using InState.Behaviors;
using InState.Interfaces;

namespace InState.Abstracts
{
    public abstract class State<TStateData, TTriggers> : IState<TStateData, TTriggers>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public State()
        {
            Data = new List<TStateData>();
            PermittedTransitions = new List<TransitionBehavior<TStateData, TTriggers>>();
        }

        public List<TStateData> Data { get; set; }

        /// <summary>
        /// Indicates the name of the step
        /// </summary>
        /// <value>string</value>
        public abstract string Name { get; }

        public IList<TransitionBehavior<TStateData, TTriggers>> PermittedTransitions { get; private set; }

        /// <summary>
        /// Process the provided state data
        /// </summary>
        /// <returns>TIState<TStateData, TTriggers></returns>
        public virtual IState<TStateData, TTriggers> Process()
        {
            return null;
        }

        /// <summary>
        /// Process the provided state data
        /// </summary>
        /// <param name="stateData">Indicates the state-specific data to process</param>
        /// <returns>IState<TStateData, TTriggers></returns>
        public virtual IState<TStateData, TTriggers> Process(object stateData)
        {
            return null;
        }

        /// <summary>
        /// Process the provided state data
        /// </summary>
        /// <param name="firstDataItem">Indicates the first state-specific data item to process<</param>
        /// <param name="secondDataItem">Indicates the second state-specific data item to process</param>
        /// <returns>IState<TStateData, TTriggers></returns>
        public virtual IState<TStateData, TTriggers> Process(object firstDataItem, object secondDataItem)
        {
            return null;
        }

        public TransitionBehavior<TStateData, TTriggers> When(TTriggers trigger)
        {
            TransitionBehavior<TStateData, TTriggers> transitionBehavior = new TransitionBehavior<TStateData, TTriggers>(trigger, this);
            PermittedTransitions.Add(transitionBehavior);

            return transitionBehavior;
        }
    }
}