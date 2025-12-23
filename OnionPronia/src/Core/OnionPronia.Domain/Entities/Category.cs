
namespace OnionPronia.Domain.Entities
{
    public class Category:BaseNameableEntity
    {
        //Relational
        ICollection<Product> Products { get; set; }

    }
}
