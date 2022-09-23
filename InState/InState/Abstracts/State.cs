using System.Collections.Generic;
using InState.Behaviors;
using InState.Interfaces;

namespace InState.Abstracts
{
    /// <summary>
    /// State describes a specific state of the state machine, including data, associated activities, and permitted transitions
    /// </summary>
    /// <typeparam name="TStateData">Type of data that will be stored in the state</typeparam>
    /// <typeparam name="TTriggers">Type of triggers that will be used to signal state transitions</typeparam>
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

        /// <summary>
        /// Indicates the data associated with the state
        /// </summary>
        /// <value>List<TStateData></value>
        public List<TStateData> Data { get; set; }

        /// <summary>
        /// Indicates the name of the step
        /// </summary>
        /// <value>string</value>
        public abstract string Name { get; }

        /// <summary>
        /// Identifies the permitted transitions from this state
        /// </summary>
        /// <value>IList<TransitionBehavior<TStateData, TTriggers>></value>
        public IList<TransitionBehavior<TStateData, TTriggers>> PermittedTransitions { get; private set; }

        /// <summary>
        /// Describes a transition associated with the state that is indicated by the specified trigger
        /// </summary>
        /// <param name="trigger">Indicates the trigger that will start the transition from the state</param>
        /// <returns>TransitionBehavior<TStateData, TTriggers></returns>
        public TransitionBehavior<TStateData, TTriggers> When(TTriggers trigger)
        {
            TransitionBehavior<TStateData, TTriggers> transitionBehavior = new TransitionBehavior<TStateData, TTriggers>(trigger, this);
            return transitionBehavior;
        }
    }
}