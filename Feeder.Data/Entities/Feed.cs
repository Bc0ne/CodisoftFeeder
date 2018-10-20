namespace Feeder.Data.Entities
{
    public class Feed
    {
        public enum SourceType : int
        {
            RSS,
            Atom
        }

        public long Id { get; private set; }

        public string Title { get; private set; }

        public string Link { get; private set; }

        public SourceType Type { get; private set; } = SourceType.RSS;

        public long CollectionId { get; private set; }

        public virtual Collection Collection { get; private set; }

        public static Feed New(string title, string link, SourceType type, Collection collection)
        {
            return new Feed
            {
                Title = title,
                Link = link,
                Collection = collection,
                Type = type
            };
        }

        public void UpdateTitle(string title)
        {
            Title = title;
        }
    }
}
