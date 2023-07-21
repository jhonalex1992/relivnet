using relivnet.domain.entities;
using relivnet.domain.models;
using relivnet.infraestructure.application.models.requests;
using relivnet.infraestructure.application.models.responses;
using relivnet.infraestructure.util;
using relivnet.infraestructure.util.exceptions;

namespace relivnet.infraestructure.application
{
    public partial class ApplicationService : IApplicationService
    {
        public StateResponseModel CreateState(StateRequestModel stateRequestModel)
        {
            bool exist = this._stateDomainRepository.AnySync(x => x.StateId == stateRequestModel.StateId);
            if (exist)
            {
                throw new CustomException(ExceptionSettings.ALREDY_EXIST);
            }

            StateEntity stateEntity = this._mapper.Map<StateEntity>(stateRequestModel);
            this._stateDomainRepository.AddSync(stateEntity);
            StateResponseModel stateResponseModel = this._mapper.Map<StateResponseModel>(stateEntity);
            return stateResponseModel;
        }

        public StateResponseModel UpdateState(StateRequestModel stateRequestModel)
        {
            StateEntity stateEntity =
                this._stateDomainRepository.SingleSync(x => x.StateId == stateRequestModel.StateId);
            if (stateEntity == null)
            {
                throw new CustomException(ExceptionSettings.NOT_EXIST);
            }

            stateEntity.Description = stateRequestModel.Description;
            _stateDomainRepository.UpdateSync(stateEntity);

            StateResponseModel stateResponseModel = this._mapper.Map<StateResponseModel>(stateEntity);
            return stateResponseModel;
        }

        public int DeleteState(int id)
        {
            StateEntity stateEntity = this._stateDomainRepository.SingleSync(x => x.StateId == id);
            if (stateEntity == null)
            {
                throw new CustomException(ExceptionSettings.NOT_FOUND);
            }

            stateEntity.IsDelete = true;
            return this._stateDomainRepository.UpdateSync(stateEntity);
        }

        public StateResponseModel GetStateId(int id)
        {
            List<string> navigationsProperties = new List<string>();
            StateEntity stateEntity =
                this._stateDomainRepository.FirstOrDefaultSync(x => x.StateId == id, navigationsProperties);
            if (stateEntity == null)
            {
                throw new CustomException(ExceptionSettings.NOT_FOUND);
            }

            return this._mapper.Map<StateResponseModel>(stateEntity);
        }

        public PagedCollection<StateResponseModel> GetAllStates(int offset, int limit, string? filter)
        {
            List<string> navigationsProperties = new List<string>();
            PagedCollection<StateEntity> stateEntityList = this._stateDomainRepository.GetPaginWhereSync(
                x => !x.IsDelete &&
                     (string.IsNullOrEmpty(filter) || x.Description.ToUpper().Contains(filter.ToUpper())),
                offset, limit, navigationsProperties);

            return new PagedCollection<StateResponseModel>()
            {
                Limit = stateEntityList.Limit,
                Offset = stateEntityList.Offset,
                Size = stateEntityList.Size,
                Items = this._mapper.Map<StateResponseModel[]>(stateEntityList.Items)
            };
        }
    }
}

