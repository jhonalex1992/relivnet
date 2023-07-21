using relivnet.domain.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace relivnet.infraestructure.application.models.responses
{
    public class ProductReponseModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public int StateId { get; set; }
        public CategoryModel Category { get; set; }
        public StateModel State { get; set; }

    }
}
