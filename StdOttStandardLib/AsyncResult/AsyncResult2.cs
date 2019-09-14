namespace StdOttStandard.AsyncResult
{
    public class AsyncResult<TOut, TIn0, TIn1> : AsyncResult<TOut>
    {
        public TIn0 Input0 { get; }

        public TIn1 Input1 { get; }

        public AsyncResult(TIn0 input0, TIn1 input1)
        {
            Input0 = input0;
            Input1 = input1;
        }
    }
}
