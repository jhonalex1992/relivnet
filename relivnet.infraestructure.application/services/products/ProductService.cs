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
        public ProductReponseModel CreateProduct(ProductRequestModel productRequestModel)
        {
            bool exist = this._productDomainRepository.AnySync(x => x.ProductId == productRequestModel.ProductId);
            if (exist)
            {
                throw new CustomException(ExceptionSettings.ALREDY_EXIST);
            }

            ProductEntity productEntity = this._mapper.Map<ProductEntity>(productRequestModel);
            productEntity.Category =
                this._categoryDomainRepository.SingleSync(x => x.CategoryId == productRequestModel.CategoryId);
            productEntity.State = this._stateDomainRepository.SingleSync(x => x.StateId == productRequestModel.StateId);
            this._productDomainRepository.AddSync(productEntity);
            ProductReponseModel productResponseModel = this._mapper.Map<ProductReponseModel>(productEntity);
            return productResponseModel;
        }

        public ProductReponseModel UpdateProduct(ProductRequestModel productRequestModel)
        {
            ProductEntity productEntity =
                this._productDomainRepository.SingleSync(x => x.ProductId == productRequestModel.ProductId);
            if (productEntity == null)
            {
                throw new CustomException(ExceptionSettings.NOT_EXIST);
            }

            productEntity.Name = productRequestModel.Name;
            productEntity.Price = productRequestModel.Price;
            productEntity.Stock = productRequestModel.Stock;
            productEntity.CategoryId = productRequestModel.CategoryId;
            productEntity.Category =
                this._categoryDomainRepository.SingleSync(x => x.CategoryId == productRequestModel.CategoryId);
            productEntity.StateId = productRequestModel.StateId;
            productEntity.State =
                this._stateDomainRepository.SingleSync(x => x.StateId == productRequestModel.StateId);

            _productDomainRepository.UpdateSync(productEntity);

            ProductReponseModel productReponseModel = this._mapper.Map<ProductReponseModel>(productEntity);
            return productReponseModel;
        }

        public int DeleteProduct(int id)
        {
            ProductEntity productEntity = this._productDomainRepository.SingleSync(x => x.ProductId == id);
            if (productEntity == null)
            {
                throw new CustomException(ExceptionSettings.NOT_FOUND);
            }

            productEntity.IsDelete = true;
            return this._productDomainRepository.UpdateSync(productEntity);
        }

        public ProductReponseModel GetProductId(int id)
        {
            List<string> navigationsProperties = new List<string>();
            navigationsProperties.Add("Category");
            navigationsProperties.Add("State");
            ProductEntity productEntity =
                this._productDomainRepository.FirstOrDefaultSync(x => x.ProductId == id, navigationsProperties);
            if (productEntity == null)
            {
                throw new CustomException(ExceptionSettings.NOT_FOUND);
            }

            return this._mapper.Map<ProductReponseModel>(productEntity);
        }

        public PagedCollection<ProductReponseModel> GetAllProducts(int offset, int limit, string? filter)
        {
            List<string> navigationsProperties = new List<string>();
            navigationsProperties.Add("Category");
            navigationsProperties.Add("State");
            PagedCollection<ProductEntity> productEntityList = this._productDomainRepository.GetPaginWhereSync(
                x => !x.IsDelete &&
                     (string.IsNullOrEmpty(filter) || x.Name.ToUpper().Contains(filter.ToUpper())),
                offset, limit, navigationsProperties);

            return new PagedCollection<ProductReponseModel>()
            {
                Limit = productEntityList.Limit,
                Offset = productEntityList.Offset,
                Size = productEntityList.Size,
                Items = this._mapper.Map<ProductReponseModel[]>(productEntityList.Items)
            };
        }
    }
}

