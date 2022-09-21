namespace InState.Abstracts
{
    public abstract class Activity<TStateData, TTriggers>
    {
        private State<TStateData, TTriggers> originatingState;

        public Activity(State<TStateData, TTriggers> originatingState)
        {
            this.originatingState = originatingState;
        }

        public virtual State<TStateData, TTriggers> Execute()
        {
            return originatingState;
        }

        public virtual State<TStateData, TTriggers> Execute<TExecuteData>(TExecuteData data)
        {
            return originatingState;
        }

        public virtual State<TStateData, TTriggers> Execute<TExecuteDataFirst, TExecuteDataSecond>(TExecuteDataFirst firstData, TExecuteDataSecond secondData)
        {
            return originatingState;
        }
    }
}