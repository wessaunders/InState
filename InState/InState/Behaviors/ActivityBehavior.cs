using System.Linq;
using InState.Abstracts;
using InState.Interfaces;

namespace InState.Behaviors
{
    public class ActivityBehavior<TStateData, TTriggers>
    {
        private State<TStateData, TTriggers> targetState;
        private TransitionBehavior<TStateData, TTriggers> transitionBehavior;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="trigger">Identifies the trigger that will be associated with the activity</param>
        /// <param name="targetState">Indicates the targeted state</param>
        public ActivityBehavior(State<TStateData, TTriggers> targetState, 
            TransitionBehavior<TStateData, TTriggers> transitionBehavior)
        {
            this.targetState = targetState;
            this.transitionBehavior = transitionBehavior;
        }

        public Activity<TStateData, TTriggers> Activity { get; private set; }
        
        public State<TStateData, TTriggers> AfterTransition(Activity<TStateData, TTriggers> activity)
        {
            Activity = activity;
            return targetState;
        }
    }
}