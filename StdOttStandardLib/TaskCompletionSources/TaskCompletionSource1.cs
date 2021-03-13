using System.Threading.Tasks;

namespace StdOttStandard.TaskCompletionSources
{
    public class TaskCompletionSource<TOut, TIn> : TaskCompletionSource<TOut>
    {
        public TIn Input { get; set; }

        public TaskCompletionSource()
        {
        }

        public TaskCompletionSource(object state) : base(state)
        {
        }

        public TaskCompletionSource(TaskCreationOptions creationOptions) : base(creationOptions)
        {
        }

        public TaskCompletionSource(object state, TaskCreationOptions creationOptions) : base(state, creationOptions)
        {
        }

        public TaskCompletionSource(TIn input)
        {
            Input = input;
        }
    }
}
