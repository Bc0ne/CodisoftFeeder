namespace Feeder.Data.Entities
{
    using System.Collections.Generic;

    public class Collection
    {
        public long Id { get; private set; }
        public string CollectionName { get; private set; }

        public static Collection New(string collectionName)
        {
            return new Collection
            {
                CollectionName = collectionName
            };
        }
    }
}
