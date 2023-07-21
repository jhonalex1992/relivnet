
namespace relivnet.domain.entities
{
    public class StateEntity: BaseEntity
    {
        public int StateId { get; set; }
        public string Description { get; set; }
        public List<ProductEntity> Products { get; set; }
    }
}
