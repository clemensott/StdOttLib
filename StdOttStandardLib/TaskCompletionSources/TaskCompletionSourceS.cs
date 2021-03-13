using System.Threading.Tasks;

namespace StdOttStandard.TaskCompletionSources
{
    public class TaskCompletionSourceS<TInOut> : TaskCompletionSource<TInOut>
    {
        public TInOut Input { get; }

        public TaskCompletionSourceS()
        {
        }

        public TaskCompletionSourceS(object state) : base(state)
        {
        }

        public TaskCompletionSourceS(TaskCreationOptions creationOptions) : base(creationOptions)
        {
        }

        public TaskCompletionSourceS(object state, TaskCreationOptions creationOptions) : base(state, creationOptions)
        {
        }

        public TaskCompletionSourceS(TInOut input)
        {
            Input = input;
        }
    }
}
