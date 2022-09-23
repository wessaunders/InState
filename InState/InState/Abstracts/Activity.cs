namespace InState.Abstracts
{
    /// <summary>
    /// Describes an activity that provides executable functionality that can be associated with a transition or a state
    /// </summary>
    /// <typeparam name="TStateData">Type of data that will be stored in the state</typeparam>
    /// <typeparam name="TTriggers">Type of triggers that will be used to signal state transitions</typeparam>
    public abstract class Activity<TStateData, TTriggers>
    {
        private State<TStateData, TTriggers> originatingState;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="originatingState">Identifies the state that originated the activity</param>
        public Activity(State<TStateData, TTriggers> originatingState)
        {
            this.originatingState = originatingState;
        }

        /// <summary>
        /// Overridable method that can be used to perform custom logic
        /// </summary>
        /// <returns>State<TStateData, TTriggers></returns>
        public virtual State<TStateData, TTriggers> Execute()
        {
            return originatingState;
        }

        /// <summary>
        /// Overridable method that accepts a single parameter and can be used to perform custom logic
        /// </summary>
        /// <param name="data">The parameter to pass to the execute method</param>
        /// <typeparam name="TExecuteData">Indicates the type of the parameter</typeparam>
        /// <returns>State<TStateData, TTriggers></returns>
        public virtual State<TStateData, TTriggers> Execute<TExecuteData>(TExecuteData data)
        {
            return originatingState;
        }

        /// <summary>
        /// Overridable method that accepts two parameters and can be used to perform custom logic
        /// </summary>
        /// <param name="firstData">The first parameter to pass to the execute method</param>
        /// <param name="secondData">The second parameter to pass to the execute method</param>
        /// <typeparam name="TExecuteDataFirst">Indicates the type of the first parameter</typeparam>
        /// <typeparam name="TExecuteDataSecond">Indicates the type of the second parameter</typeparam>
        /// <returns>State<TStateData, TTriggers></returns>
        public virtual State<TStateData, TTriggers> Execute<TExecuteDataFirst, TExecuteDataSecond>(TExecuteDataFirst firstData, TExecuteDataSecond secondData)
        {
            return originatingState;
        }
    }
}