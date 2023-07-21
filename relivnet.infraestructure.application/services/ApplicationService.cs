using AutoMapper;
using Microsoft.Extensions.Configuration;
using relivnet.domain.repositories;

namespace relivnet.infraestructure.application
{
    public partial class ApplicationService : IApplicationService
    {
        private readonly IUserDomainRepository _userDomainRepository;
        private readonly IRoleDomainRepository _roleDomainRepository;
        private readonly IProductDomainRepository _productDomainRepository;
        private readonly ICategoryDomainRepository _categoryDomainRepository;
        private readonly IStateDomainRepository _stateDomainRepository;
        private readonly IMapper _mapper;
        private readonly string _tokenKey;
        private readonly IConfiguration _configuration;

        public ApplicationService(
            IUserDomainRepository userDomainRepository,
            IProductDomainRepository productDomainRepository,
            ICategoryDomainRepository categoryDomainRepository,
            IStateDomainRepository stateDomainRepository,
            IMapper mapper,
            IConfiguration configuration, IRoleDomainRepository roleDomainRepository)
        {
            this._configuration = configuration;
            _roleDomainRepository = roleDomainRepository;
            this._userDomainRepository = userDomainRepository;
            this._productDomainRepository = productDomainRepository;
            this._categoryDomainRepository = categoryDomainRepository;
            this._stateDomainRepository = stateDomainRepository;
            this._mapper = mapper;
            this._tokenKey = this._configuration["Jwt:Key"];
        }
    }
}