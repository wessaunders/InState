using System.Collections.Generic;
using System.Threading.Tasks;
using InState.Behaviors;

namespace InState.Interfaces
{
    public interface IState<TStateData, TTriggers>
    {
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

        /// <summary>
        /// Process the provided state data
        /// </summary>
        /// <returns>TIState<TStateData, TTriggers></returns>
        IState<TStateData, TTriggers> Process();

        /// <summary>
        /// Process the provided state data
        /// </summary>
        /// <param name="stateData">Indicates the state-specific data to process</param>
        /// <returns>IState<TStateData, TTriggers></returns>
        IState<TStateData, TTriggers> Process(object stateData);

        /// <summary>
        /// Process the provided state data
        /// </summary>
        /// <param name="firstDataItem">Indicates the first state-specific data item to process<</param>
        /// <param name="secondDataItem">Indicates the second state-specific data item to process</param>
        /// <returns>IState<TStateData, TTriggers></returns>
        IState<TStateData, TTriggers> Process(object firstDataItem, object secondDataItem);
    }
}