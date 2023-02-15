using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Repositories.DataTransferObjects.Product
{
    public class UpdateProductDto : BaseProductDto
    {
        [Required]
        public string Id { get; set; }
    }
}
