using System.Threading.Tasks;

namespace StdOttStandard.TaskCompletionSources
{
    public class TaskCompletionSource<TOut, TIn0, TIn1, TIn2> : TaskCompletionSource<TOut>
    {
        public TIn0 Input0 { get; }

        public TIn1 Input1 { get; }

        public TIn2 Input2 { get; }

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

        public TaskCompletionSource(TIn0 input0, TIn1 input1, TIn2 input2)
        {
            Input0 = input0;
            Input1 = input1;
            Input2 = input2;
        }
    }
}
