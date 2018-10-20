namespace Feeder.API.Models.Feed
{
    using System.ComponentModel.DataAnnotations;

    public class FeedUpdateModel
    {
        [Required(ErrorMessage = "You should fill out the title.")]
        [MinLength(5, ErrorMessage = "Title can't be less than 5 character.")]
        [MaxLength(100, ErrorMessage = "Title can't be more than 100 characters")]
        public string Title { get; set; }
    }
}
