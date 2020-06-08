using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StdOttStandard.Dispatch
{
    class DispatcherHandle
    {
        private readonly Action action;
        private readonly Func<object> func;

        public object Result { get; private set; }

        public Exception Exception { get; private set; }

        public DispatcherHandle(Action action)
        {
            this.action = action;
        }

        public DispatcherHandle(Func<object> func)
        {
            this.func = func;
        }

        public void Run()
        {
            try
            {
                if (action != null) action.Invoke();
                else if (func != null) Result = func();
                else throw new Exception("No function to dispatch given");
            }
            catch (Exception e)
            {
                Exception = e;
            }
        }
    }
}
