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
        public CategoryResponseModel CreateCategory(CategoryRequestModel categoryRequestModel)
        {
            bool exist = this._categoryDomainRepository.AnySync(x => x.CategoryId == categoryRequestModel.CategoryId);
            if (exist)
            {
                throw new CustomException(ExceptionSettings.ALREDY_EXIST);
            }

            CategoryEntity categoryEntity = this._mapper.Map<CategoryEntity>(categoryRequestModel);
            this._categoryDomainRepository.AddSync(categoryEntity);
            CategoryResponseModel categoryResponseModel = this._mapper.Map<CategoryResponseModel>(categoryEntity);
            return categoryResponseModel;
        }

        public CategoryResponseModel UpdateCategory(CategoryRequestModel categoryRequestModel)
        {
            CategoryEntity categoryEntity =
                this._categoryDomainRepository.SingleSync(x => x.CategoryId == categoryRequestModel.CategoryId);
            if (categoryEntity == null)
            {
                throw new CustomException(ExceptionSettings.NOT_EXIST);
            }

            categoryEntity.Description = categoryRequestModel.Description;
            _categoryDomainRepository.UpdateSync(categoryEntity);

            CategoryResponseModel categoryResponseModel = this._mapper.Map<CategoryResponseModel>(categoryEntity);
            return categoryResponseModel;
        }

        public int DeleteCategory(int id)
        {
            CategoryEntity categoryEntity = this._categoryDomainRepository.SingleSync(x => x.CategoryId == id);
            if (categoryEntity == null)
            {
                throw new CustomException(ExceptionSettings.NOT_FOUND);
            }

            categoryEntity.IsDelete = true;
            return this._categoryDomainRepository.UpdateSync(categoryEntity);
        }

        public CategoryResponseModel GetCategoryId(int id)
        {
            List<string> navigationsProperties = new List<string>();
            CategoryEntity categoryEntity =
                this._categoryDomainRepository.FirstOrDefaultSync(x => x.CategoryId == id, navigationsProperties);
            if (categoryEntity == null)
            {
                throw new CustomException(ExceptionSettings.NOT_FOUND);
            }
            return this._mapper.Map<CategoryResponseModel>(categoryEntity);
        }

        public PagedCollection<CategoryResponseModel> GetAllCategory(int offset, int limit, string? filter)
        {
            List<string> navigationsProperties = new List<string>();
            PagedCollection<CategoryEntity> categoryEntityList = this._categoryDomainRepository.GetPaginWhereSync(
                x => !x.IsDelete &&
                     (string.IsNullOrEmpty(filter) || x.Description.ToUpper().Contains(filter.ToUpper())),
                offset, limit, navigationsProperties);

            return new PagedCollection<CategoryResponseModel>()
            {
                Limit = categoryEntityList.Limit,
                Offset = categoryEntityList.Offset,
                Size = categoryEntityList.Size,
                Items = this._mapper.Map<CategoryResponseModel[]>(categoryEntityList.Items)
            };
        }
    }
}

