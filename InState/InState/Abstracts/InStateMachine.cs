using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InState.Behaviors;
using InState.Interfaces;

namespace InState.Abstracts
{
    /// <summary>
    /// Tracks the state of the machine states and associated data
    /// </summary>
    /// <typeparam name="TStateData">Indicates the type that of data that will be shared by the states</typeparam>
    /// <typeparam name="TTriggers">Indicates the triggers type</typeparam>
    public abstract class InStateMachine<TStateData, TTriggers>
    {
        private IState<TStateData, TTriggers> currentState;

        /// <summary>
        /// Default constructor
        /// </summary>
        public InStateMachine()
        {
            States = new List<IState<TStateData, TTriggers>>();
        }

        /// <summary>
        /// Returns the current state
        /// </summary>
        /// <value></value>
        public IState<TStateData, TTriggers> CurrentState
        {
            get
            {
                return currentState;
            }
        }

        /// <summary>
        /// Transition to the state specified by the provided trigger from the current state
        /// </summary>
        /// <param name="trigger">Indicates the trigger to use to determine the appropriate state to transition to</param>
        /// <returns>IState<TStateData, TTriggers></returns>
        public IState<TStateData, TTriggers> Fire(TTriggers trigger)
        {
            if (States.Any())
            {
                if (currentState != null)
                {
                    IState<TStateData, TTriggers> originalState = currentState;

                    TransitionBehavior<TStateData, TTriggers> matchingTransitionBehavior = currentState
                        .PermittedTransitions
                        .FirstOrDefault(x => x.TransitionTrigger.Equals(trigger));

                    if (matchingTransitionBehavior != null)
                    {
                        State<TStateData, TTriggers> permittedTransition = matchingTransitionBehavior.PermittedTransition;
                        if (permittedTransition != null)
                        {
                            if (matchingTransitionBehavior.AfterTransitionActivity != null)
                            {
                                permittedTransition.AssociatedActivity = matchingTransitionBehavior
                                    .AfterTransitionActivity
                                    .Activity;
                            }

                            currentState = permittedTransition;
                            currentState.Data = originalState.Data;
                        }
                    }
                }
                else
                {
                    currentState = States.First();
                }
            }
            
            return currentState;
        }

        /// <summary>
        /// Transition to the state specified by the provided trigger from the current state and process the
        /// new state activity with the provided data
        /// </summary>
        /// <param name="trigger">Indicates the trigger to use to determine the appropriate state to transition to</param>
        /// <param name="data">Indicates the data to apply to the new state activity</param>
        /// <typeparam name="V">Identifies the type of data to provide to the new state activity</typeparam>
        /// <returns>IState<TStateData, TTriggers></returns>
        public IState<TStateData, TTriggers> Fire<V>(TTriggers trigger, V data)
        {
            IState<TStateData, TTriggers> transitionedState = Fire(trigger);

            if (transitionedState.AssociatedActivity != null)
            {
                Activity<TStateData, TTriggers> activity = transitionedState.AssociatedActivity;
                transitionedState = activity.Execute(data);
            }

            return transitionedState;
        }

        /// <summary>
        /// Transition to the state specified by the provided trigger from the current state and process the
        /// new state activity with the provided data
        /// </summary>
        /// <param name="trigger">Indicates the trigger to use to determine the appropriate state to transition to</param>
        /// <param name="firstDataItem">Indicates the first data item to apply to the new state activity</param>
        /// <param name="secondDataItem">Indicates the second data item to apply to the new state activity</param>
        /// <typeparam name="V">Identifies the type of data of the first data item to provide to the new state activity</typeparam>
        /// <typeparam name="Z">Identifies the type of data of the second data item to provide to the new state activity</typeparam>
        /// <returns>IState<TStateData, TTriggers></returns>
        public IState<TStateData, TTriggers> Fire<V, Z>(TTriggers trigger, V firstDataItem, Z secondDataItem)
        {
            IState<TStateData, TTriggers> transitionedState = Fire(trigger);

            if (transitionedState.AssociatedActivity != null)
            {
                Activity<TStateData, TTriggers> activity = transitionedState.AssociatedActivity;
                transitionedState = activity.Execute(firstDataItem, secondDataItem);
            }

            return transitionedState;
        }

        /// <summary>
        /// Transition to the state specified by the provided trigger from the current state and 
        /// asynchronously process the new state activity with the provided data
        /// </summary>
        /// <param name="trigger">Indicates the trigger to use to determine the appropriate state to transition to</param>
        /// <param name="data">Indicates the data to apply to the new state activity</param>
        /// <typeparam name="V">Identifies the type of data to provide to the new state activity</typeparam>
        /// <returns>IState<TStateData, TTriggers></returns>
        public async Task<IState<TStateData, TTriggers>> FireAsync<V>(TTriggers trigger, V data)
        {
            IState<TStateData, TTriggers> transitionedState = Fire(trigger);
            
            if (transitionedState.AssociatedActivity != null)
            {
                Activity<TStateData, TTriggers> activity = transitionedState.AssociatedActivity;
                transitionedState = await Task.Run(() => activity.Execute(data));
            }

            return transitionedState;
        }

        /// <summary>
        /// Transition to the state specified by the provided trigger from the current state and 
        /// asynchronously process the new state activity with the provided data
        /// </summary>
        /// <param name="trigger">Indicates the trigger to use to determine the appropriate state to transition to</param>
        /// <param name="firstDataItem">Indicates the first data item to apply to the new state activity</param>
        /// <param name="secondDataItem">Indicates the second data item to apply to the new state activity</param>
        /// <typeparam name="V">Identifies the type of data of the first data item to provide to the new state activity</typeparam>
        /// <typeparam name="Z">Identifies the type of data of the second data item to provide to the new state activity</typeparam>
        /// <returns>IState<TStateData, TTriggers></returns>
        public async Task<IState<TStateData, TTriggers>> FireAsync<V, Z>(TTriggers trigger, V firstDataItem, Z secondDataItem)
        {
            IState<TStateData, TTriggers> transitionedState = Fire(trigger);

            if (transitionedState.AssociatedActivity != null)
            {
                Activity<TStateData, TTriggers> activity = transitionedState.AssociatedActivity;
                transitionedState = await Task.Run(() => activity.Execute(firstDataItem, secondDataItem));
            }

            return transitionedState;
        }

        /// <summary>
        /// Sets the initial state of the state machine to the provided state
        /// </summary>
        /// <param name="initialState">Indicates the state that should be set as the initial state</param>
        public void InitialState(IState<TStateData, TTriggers> initialState)
        {
            currentState = initialState;
        }

        /// <summary>
        /// Gets or sets the states in the machine
        /// </summary>
        /// <value></value>
        public List<IState<TStateData, TTriggers>> States { get; set; }
    }
}
