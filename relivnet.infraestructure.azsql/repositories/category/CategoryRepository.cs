using relivnet.domain.entities;
using relivnet.domain.repositories;
using relivnet.infraestructure.azsql.contexts;
using relivnet.infraestructure.azsql.repositories.generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace relivnet.infraestructure.azsql.repositories.category
{
    public class CategoryRepository : GenericDataMedianetDbRepository<CategoryEntity>, ICategoryDomainRepository
    {
        public CategoryRepository(RelivnetDbContext contex) : base(contex)
        {
            Context = contex;
        }
    }
}
