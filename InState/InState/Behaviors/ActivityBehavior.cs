using InState.Abstracts;

namespace InState.Behaviors
{
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

        public Activity<TStateData, TTriggers> Activity { get; private set; }
        
        public State<TStateData, TTriggers> Then(Activity<TStateData, TTriggers> activity)
        {
            Activity = activity;
            return targetState;
        }

        public TransitionBehavior<TStateData, TTriggers> When(TTriggers trigger)
        {
            TransitionBehavior<TStateData, TTriggers> transitionBehavior = new TransitionBehavior<TStateData, TTriggers>(trigger, originatingState);
            return transitionBehavior;
        }
    }
}