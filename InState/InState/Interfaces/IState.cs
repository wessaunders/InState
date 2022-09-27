using System.Collections.Generic;
using InState.Abstracts;
using InState.Behaviors;

namespace InState.Interfaces
{
    /// <summary>
    /// Describes the interface that all states must conform to
    /// </summary>
    /// <typeparam name="TStateData">Type of data that will be stored in the state</typeparam>
    /// <typeparam name="TTriggers">Type of triggers that will be used to signal state transitions</typeparam>
    public interface IState<TStateData, TTriggers>
    {
        /// <summary>
        /// Identifies the current activity associated with the current transition
        /// </summary>
        /// <value>Activity<TStateData, TTriggers></value>
        Activity<TStateData, TTriggers> AssociatedActivity { get; }

        /// <summary>
        /// Contains the data associated with the current state
        /// as well as previous states
        /// </summary>
        /// <value><List<TStateData>/value>
        List<TStateData> Data { get; set; }

        /// <summary>
        /// Indicates the name of the state
        /// </summary>
        /// <value>string</value>
        string Name { get; }

        /// <summary>
        /// Indicates the transitions that are permitted from the current state
        /// </summary>
        /// <value>IList<TransitionBehavior<TStateData, TTriggers>></value>
        IList<TransitionBehavior<TStateData, TTriggers>> PermittedTransitions { get; }
    }
}