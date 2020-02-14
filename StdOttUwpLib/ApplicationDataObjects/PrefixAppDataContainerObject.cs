using Windows.Storage;

namespace StdOttUwp.ApplicationDataObjects
{
    public class PrefixAppDataContainerObject : AppDataContainerObject
    {
        public string Prefix { get; }

        public PrefixAppDataContainerObject(string prefix, ApplicationDataContainer container) : base(container)
        {
            Prefix = prefix;
        }

        protected override bool TryGetValue<T>(string propertyName, out T value)
        {
            return base.TryGetValue(Prefix + propertyName, out value);
        }

        protected override bool SetValue(string propertyName, object value)
        {
            return base.SetValue(Prefix + propertyName, value);
        }
    }
}
