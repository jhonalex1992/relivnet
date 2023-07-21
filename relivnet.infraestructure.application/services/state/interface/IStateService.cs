using relivnet.domain.models;
using relivnet.infraestructure.application.models.requests;
using relivnet.infraestructure.application.models.responses;

namespace relivnet.infraestructure.application
{
    public partial interface IApplicationService
    {
        StateResponseModel CreateState(StateRequestModel stateRequestModel);
        StateResponseModel UpdateState(StateRequestModel stateRequestModel);
        int DeleteState(int id);
        StateResponseModel GetStateId(int id);
        PagedCollection<StateResponseModel> GetAllStates(int offset, int limit, string? filter);
    }
}
