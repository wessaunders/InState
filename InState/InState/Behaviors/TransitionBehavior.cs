using System.Linq;
using InState.Abstracts;
using InState.Interfaces;

namespace InState.Behaviors
{
    public class TransitionBehavior<TStateData, TTriggers>
    {
        //private State<TStateData, TTriggers> originatingState;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="trigger">Identifies the trigger that will be associated with the transition</param>
        /// <param name="originatingState">Indicates the originating state</param>
        public TransitionBehavior(TTriggers trigger, State<TStateData, TTriggers> originatingState)
        {
            OriginatingState = originatingState;
            TransitionTrigger = trigger;

            OriginatingState.PermittedTransitions.Add(this);
        }

        internal ActivityBehavior<TStateData, TTriggers> AfterTransitionActivity { get; set; }

        public State<TStateData, TTriggers> OriginatingState { get; private set; }

        /// <summary>
        /// Defines which transition is permitted from the specified state and trigger
        /// </summary>
        /// <value>State<TStateData, TTriggers>?</value>
        internal State<TStateData, TTriggers>? PermittedTransition { get; private set; }

        /// <summary>
        /// Define the step the current state can transition to
        /// </summary>
        /// <param name="stateToTransitionTo">Identifies the state that can be transitioned to</param>
        /// <returns>ActivityBehavior<TStateData, TTriggers></returns>
        public ActivityBehavior<TStateData, TTriggers> TransitionTo(State<TStateData, TTriggers> stateToTransitionTo)
        {
            ActivityBehavior<TStateData, TTriggers> activityBehavior = null;
            
            if (stateToTransitionTo != null)
            {
                activityBehavior = new ActivityBehavior<TStateData, TTriggers>(OriginatingState, stateToTransitionTo);
                AfterTransitionActivity = activityBehavior;

                PermittedTransition = stateToTransitionTo;
            }

            return activityBehavior;
        }

        internal TTriggers TransitionTrigger { get; private set; }
    }
}
