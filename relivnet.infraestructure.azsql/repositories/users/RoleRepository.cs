using relivnet.domain.entities;
using relivnet.domain.repositories;
using relivnet.infraestructure.azsql.contexts;
using relivnet.infraestructure.azsql.repositories.generics;

namespace relivnet.infraestructure.azsql.repositories.users;

public class RoleRepository : GenericDataMedianetDbRepository<RoleEntity>, IRoleDomainRepository
{
    
    public RoleRepository(RelivnetDbContext contex) : base(contex)
    {
        Context = contex;
    }
    
}