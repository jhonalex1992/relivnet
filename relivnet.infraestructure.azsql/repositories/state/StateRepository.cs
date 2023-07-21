using relivnet.domain.entities;
using relivnet.domain.repositories;
using relivnet.infraestructure.azsql.contexts;
using relivnet.infraestructure.azsql.repositories.generics;

namespace relivnet.infraestructure.azsql.repositories.state
{
    public class StateRepository : GenericDataMedianetDbRepository<StateEntity>, IStateDomainRepository
    {
        public StateRepository(RelivnetDbContext contex) : base(contex)
        {
            Context = contex;
        }
    }
}
