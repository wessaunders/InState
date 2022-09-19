using System.Collections.Generic;
using System.Linq;
using InState.Behaviors;
using InState.Enums;

namespace InState.Abstracts
{
    /// <summary>
    /// Tracks the state of the machine states and associated data
    /// </summary>
    /// <typeparam name="T">Indicates the type that will be used to store the state data</typeparam>
    public abstract class InStateMachine<T>
    {
        private State<T>? currentState;

        /// <summary>
        /// Default constructor
        /// </summary>
        public InStateMachine()
        {
            States = new List<State<T>>();
        }

        /// <summary>
        /// Returns the current state
        /// </summary>
        /// <value></value>
        public State<T> CurrentState
        {
            get
            {
                return currentState;
            }
        }

        public State<T> Fire(Triggers trigger)
        {
            if (States.Any())
            {
                if (currentState != null)
                {
                    TransitionBehavior<T> matchingTransitionBehavior = currentState
                        .PermittedTransitions
                        .FirstOrDefault(x => x.TransitionTrigger.Equals(trigger));

                    if (matchingTransitionBehavior != null)
                    {
                        State<T> permittedTransition = matchingTransitionBehavior.PermittedTransition;
                        if (permittedTransition != null)
                        {
                            currentState = permittedTransition;
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

        public void InitialState(State<T> initialState)
        {
            currentState = initialState;
        }

        /// <summary>
        /// Gets or sets the states in the machine
        /// </summary>
        /// <value></value>
        public List<State<T>> States { get; set; }

    }
}
