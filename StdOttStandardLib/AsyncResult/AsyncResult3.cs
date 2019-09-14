namespace StdOttStandard.AsyncResult
{
    public class AsyncResult<TOut, TIn0, TIn1, TIn2> : AsyncResult<TOut>
    {
        public TIn0 Input0 { get; }

        public TIn1 Input1 { get; }

        public TIn2 Input2 { get; }

        public AsyncResult(TIn0 input0, TIn1 input1, TIn2 input2)
        {
            Input0 = input0;
            Input1 = input1;
            Input2 = input2;
        }
    }
}
