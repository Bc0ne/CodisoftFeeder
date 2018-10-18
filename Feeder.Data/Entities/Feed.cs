namespace Feeder.Data.Entities
{
    public class Feed
    {
        public long Id { get; private set; }

        public string Title { get; private set; }

        public string Link { get; private set; }

        public long CollectionId { get; private set; }

        public virtual Collection Collection { get; private set; }

        public static Feed New(string title, string link, Collection collection)
        {
            return new Feed
            {
                Title = title,
                Link = link,
                Collection = collection
            };
        }
    }
}
