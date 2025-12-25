
namespace OnionPronia.Domain.Entities
{
    public class Category:BaseNameableEntity
    {
        //Relational
        public ICollection<Product> Products { get; set; }

    }
}
