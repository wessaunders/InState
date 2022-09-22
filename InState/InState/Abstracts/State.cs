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

        /// <summary>
        /// Identifies the current activity associated with the current transition
        /// </summary>
        /// <value>Activity<TStateData, TTriggers></value>
        public Activity<TStateData, TTriggers> AssociatedActivity { get; internal set; }

        public List<TStateData> Data { get; set; }

        /// <summary>
        /// Indicates the name of the step
        /// </summary>
        /// <value>string</value>
        public abstract string Name { get; }

        public IList<TransitionBehavior<TStateData, TTriggers>> PermittedTransitions { get; private set; }

        public TransitionBehavior<TStateData, TTriggers> When(TTriggers trigger)
        {
            TransitionBehavior<TStateData, TTriggers> transitionBehavior = new TransitionBehavior<TStateData, TTriggers>(trigger, this);
            return transitionBehavior;
        }
    }
}