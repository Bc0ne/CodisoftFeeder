namespace Feeder.API.Models.Feed
{
    using Data.Entities;
    using System.ComponentModel.DataAnnotations;

    public class FeedInputModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Link { get; set; }

        public Feed.SourceType SourceType  { get; set; }
    }
}
