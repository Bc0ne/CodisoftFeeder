namespace Feeder.API.Models.Collection
{
    using System.ComponentModel.DataAnnotations;

    public class CollectionUpdateModel
    {
        [Required(ErrorMessage = "You should fill out the Collection name")]
        [MinLength(5, ErrorMessage = "Collection name can't be less than 5 characters.")]
        [MaxLength(100, ErrorMessage = "Collection name can't be more than 100 characters.")]
        public string CollectionName { get; set; }
    }
}
