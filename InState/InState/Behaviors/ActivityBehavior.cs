using InState.Abstracts;

namespace InState.Behaviors
{
    /// <summary>
    /// Describes the activity that can be associated with a transition or a state
    /// </summary>
    /// <typeparam name="TStateData">Type of data that will be stored in the state</typeparam>
    /// <typeparam name="TTriggers">Type of triggers that will be used to signal state transitions</typeparam>
    public class ActivityBehavior<TStateData, TTriggers>
    {
        State<TStateData, TTriggers> originatingState;
        private State<TStateData, TTriggers> targetState;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="originatingState">Identifies the originating state</param>
        /// <param name="targetState">Indicates the targeted state</param>
        public ActivityBehavior(State<TStateData, TTriggers> originatingState, State<TStateData, TTriggers> targetState)
        {
            this.originatingState = originatingState;
            this.targetState = targetState;
        }

        /// <summary>
        /// Identifies the activity associated with the current activity behavior
        /// </summary>
        /// <value>Activity<TStateData, TTriggers></value>
        public Activity<TStateData, TTriggers> Activity { get; private set; }
        
        /// <summary>
        /// Defines the activity that happens after transitioning to a state
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        public State<TStateData, TTriggers> Then(Activity<TStateData, TTriggers> activity)
        {
            Activity = activity;
            return targetState;
        }

        /// <summary>
        /// Describes a transition associated with the originating state that is indicated by the specified trigger
        /// </summary>
        /// <param name="trigger">Indicates the trigger that will start the transition from the originating state</param>
        /// <returns>TransitionBehavior<TStateData, TTriggers></returns>
        public TransitionBehavior<TStateData, TTriggers> When(TTriggers trigger)
        {
            TransitionBehavior<TStateData, TTriggers> transitionBehavior = new TransitionBehavior<TStateData, TTriggers>(trigger, originatingState);
            return transitionBehavior;
        }
    }
}