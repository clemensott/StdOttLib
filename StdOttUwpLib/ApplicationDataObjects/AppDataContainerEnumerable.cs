using System.Collections.Generic;
using System.Linq;
using Windows.Storage;

namespace StdOttUwp.ApplicationDataObjects
{
    public abstract class AppDataContainerEnumerable<T> : AppDataContainerObject
        where T : AppDataContainerObject, new()
    {
        protected static readonly StringTreeComparer comparer = new StringTreeComparer();

        protected IEnumerable<KeyValuePair<string, AppDataContainerObject>> GetListKeyValuePairs()
        {
            if (Container != null)
            {
                foreach (KeyValuePair<string, ApplicationDataContainer> pair in Container.Containers.Where(p => long.TryParse(p.Key, out _)))
                {
                    AppDataContainerObject obj;

                    if (!objects.TryGetValue(pair.Key, out obj))
                    {
                        obj = new T();
                        obj.SetContainer(pair.Value, false);
                    }

                    yield return new KeyValuePair<string, AppDataContainerObject>(pair.Key, obj);
                }

                yield break;
            }

            foreach (KeyValuePair<string, AppDataContainerObject> pair in objects.Where(p => long.TryParse(p.Key, out _)))
            {
                yield return pair;
            }
        }

        protected IEnumerable<KeyValuePair<string, AppDataContainerObject>> GetOrderedKeyValuePairs()
        {
            return GetListKeyValuePairs().OrderBy(p => p.Key, comparer);
        }
    }
}
