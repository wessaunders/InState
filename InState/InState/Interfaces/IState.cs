using System.Collections.Generic;
using InState.Abstracts;
using InState.Behaviors;

namespace InState.Interfaces
{
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
        /// Indicates the transitions that are permitted from the current state
        /// </summary>
        /// <value>IList<TransitionBehavior<TStateData, TTriggers>></value>
        IList<TransitionBehavior<TStateData, TTriggers>> PermittedTransitions { get; }
    }
}