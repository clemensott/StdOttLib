using System.Threading.Tasks;

namespace StdOttStandard.TaskCompletionSources
{
    public class TaskCompletionSource<TOut, TIn0, TIn1> : TaskCompletionSource<TOut>
    {
        public TIn0 Input0 { get; set; }

        public TIn1 Input1 { get; set; }

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

        public TaskCompletionSource(TIn0 input0, TIn1 input1)
        {
            Input0 = input0;
            Input1 = input1;
        }
    }
}
