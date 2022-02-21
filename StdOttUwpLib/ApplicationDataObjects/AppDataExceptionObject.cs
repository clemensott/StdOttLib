using System;

namespace StdOttUwp.ApplicationDataObjects
{
    public class AppDataExceptionObject
    {
        public string Message { get; set; }

        public string StackTrace { get; set; }

        public string Source { get; set; }

        public string TypeName { get; set; }

        public string Error { get; set; }

        public DateTime Timestamp { get; set; }

        public AppDataExceptionObject InnerException { get; set; }

        public static explicit operator AppDataExceptionObject (Exception e)
        {
            if (e == null)
            {
                return null;
            }

            return new AppDataExceptionObject()
            {
                Message = e.Message,
                StackTrace = e.StackTrace,
                Source = e.Source,
                TypeName = e.GetType().Name,
                Error = e.ToString(),
                Timestamp = DateTime.Now,
                InnerException = (AppDataExceptionObject)e.InnerException,
            };
        }
    }
}
