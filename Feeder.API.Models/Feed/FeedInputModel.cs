namespace Feeder.API.Models.Feed
{
    using Data.Entities;
    using System.ComponentModel.DataAnnotations;

    public class FeedInputModel
    {
        [Required(ErrorMessage = "You should fill out the title.")]
        [MinLength(5, ErrorMessage = "Title can't be less than 5 character.")]
        [MaxLength(100, ErrorMessage = "Title can't be more than 100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "You should fill out a valid link")]
        public string Link { get; set; }

        [Required(ErrorMessage = "You should determine the type")]
        public Feed.SourceType SourceType  { get; set; }
    }
}
