using System.Data.SqlTypes;
using AutoMapper;
using DataAccess.Models.Production;
using DataAccess.Repositories.IRepository.Production;
using Services.IService.Production;
using Services.DTO.ResponseDTO.Production;
using Services.DTO.RequestDTO.Production;

#nullable disable

namespace Services.Service.Production
{
    public class ProductionReferenceService(IMapper mapper, IProductionReferenceRepository productionReferenceRepository) : IProductionReferenceService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IProductionReferenceRepository _productionReferenceRepository = productionReferenceRepository;

        #region Brand

        public List<BrandDTO> GetBrands(string[] entities)
        {
            var response = _productionReferenceRepository.GetAll<Brand>(x => !x.IsDeleted, entities);
            var BrandList = _mapper.Map<List<BrandDTO>>(response);

            return BrandList;
        }

        // public async Task<BrandDTO> GetBrand(long id)
        // {
        //     var response = await _productionReferenceRepository.GetAsync<Brand>(id);
        //     if (response != null && !response.IsDeleted)
        //         return _mapper.Map<BrandDTO>(response);
        //     else
        //         return null;
        // }

        public async Task<BrandDTO> CreateBrand(BrandModel BrandModel)
        {
            var BrandMapper = _mapper.Map<Brand>(BrandModel);
            var response = await _productionReferenceRepository.CreateAsync(BrandMapper) ?? throw new SqlTypeException();
            var BrandDTO = _mapper.Map<BrandDTO>(response);

            return BrandDTO;
        }

        public async Task<bool> UpdateBrand(long id, BrandModel BrandModel)
        {
            if (id != BrandModel.Id)
                throw new InvalidDataException();

            var Brand = _mapper.Map<Brand>(BrandModel);
            return await _productionReferenceRepository.UpdateAsync(id, Brand);
        }

        public async Task<bool> DeactivateBrand(long id)
        {
            if (id <= 0)
                throw new InvalidDataException();

            return await _productionReferenceRepository.DeactivateBrand(id);
        }

        #endregion

        #region Category

        public List<CategoryDTO> GetCategories(string[] entities)
        {
            var response = _productionReferenceRepository.GetAll<Category>(x => !x.IsDeleted, entities);
            var CategoryList = _mapper.Map<List<CategoryDTO>>(response);

            return CategoryList;
        }

        // public async Task<CategoryDTO> GetCategory(long id)
        // {
        //     var response = await _productionReferenceRepository.GetAsync<Category>(id);
        //     if (response != null && !response.IsDeleted)
        //         return _mapper.Map<CategoryDTO>(response);
        //     else
        //         return null;
        // }

        public async Task<CategoryDTO> CreateCategory(CategoryModel CategoryModel)
        {
            var CategoryMapper = _mapper.Map<Category>(CategoryModel);
            var response = await _productionReferenceRepository.CreateAsync(CategoryMapper) ?? throw new SqlTypeException();
            var CategoryDTO = _mapper.Map<CategoryDTO>(response);

            return CategoryDTO;
        }

        public async Task<bool> UpdateCategory(long id, CategoryModel CategoryModel)
        {
            if (id != CategoryModel.Id)
                throw new InvalidDataException();

            var Category = _mapper.Map<Category>(CategoryModel);
            return await _productionReferenceRepository.UpdateAsync(id, Category);
        }

        public async Task<bool> DeactivateCategory(long id)
        {
            if (id <= 0)
                throw new InvalidDataException();

            return await _productionReferenceRepository.DeactivateCategory(id);
        }

        #endregion

        #region ProductType

        public List<ProductTypeDTO> GetProductTypes(string[] entities)
        {
            var response = _productionReferenceRepository.GetAll<ProductType>(x => !x.IsDeleted, entities);
            var ProductTypeList = _mapper.Map<List<ProductTypeDTO>>(response);

            return ProductTypeList;
        }

        // public async Task<ProductTypeDTO> GetProductType(long id)
        // {
        //     var response = await _productionReferenceRepository.GetAsync<ProductType>(id);
        //     if (response != null && !response.IsDeleted)
        //         return _mapper.Map<ProductTypeDTO>(response);
        //     else
        //         return null;
        // }

        public async Task<ProductTypeDTO> CreateProductType(ProductTypeModel ProductTypeModel)
        {
            var ProductTypeMapper = _mapper.Map<ProductType>(ProductTypeModel);
            var response = await _productionReferenceRepository.CreateAsync(ProductTypeMapper) ?? throw new SqlTypeException();
            var ProductTypeDTO = _mapper.Map<ProductTypeDTO>(response);

            return ProductTypeDTO;
        }

        public async Task<bool> UpdateProductType(long id, ProductTypeModel ProductTypeModel)
        {
            if (id != ProductTypeModel.Id)
                throw new InvalidDataException();

            var ProductType = _mapper.Map<ProductType>(ProductTypeModel);
            return await _productionReferenceRepository.UpdateAsync(id, ProductType);
        }

        public async Task<bool> DeactivateProductType(long id)
        {
            if (id <= 0)
                throw new InvalidDataException();

            return await _productionReferenceRepository.DeactivateProductType(id);
        }

        #endregion

        #region MediaType

        public List<MediaTypeDTO> GetMediaTypes(string[] entities)
        {
            var response = _productionReferenceRepository.GetAll<MediaType>(x => !x.IsDeleted, entities);
            var mediaTypeList = _mapper.Map<List<MediaTypeDTO>>(response);

            return mediaTypeList;
        }

        // public async Task<MediaTypeDTO> GetMediaType(long id)
        // {
        //     var response = await _productionReferenceRepository.GetAsync<MediaType>(id);
        //     if (response != null && !response.IsDeleted)
        //         return _mapper.Map<MediaTypeDTO>(response);
        //     else
        //         return null;
        // }

        public async Task<MediaTypeDTO> CreateMediaType(MediaTypeModel mediaTypeModel)
        {
            var mediaTypeMapper = _mapper.Map<MediaType>(mediaTypeModel);
            var response = await _productionReferenceRepository.CreateAsync(mediaTypeMapper) ?? throw new SqlTypeException();
            var mediaTypeDTO = _mapper.Map<MediaTypeDTO>(response);

            return mediaTypeDTO;
        }

        public async Task<bool> UpdateMediaType(long id, MediaTypeModel mediaTypeModel)
        {
            if (id != mediaTypeModel.Id)
                throw new InvalidDataException();

            var mediaType = _mapper.Map<MediaType>(mediaTypeModel);
            return await _productionReferenceRepository.UpdateAsync(id, mediaType);
        }

        public async Task<bool> DeactivateMediaType(long id)
        {
            if (id <= 0)
                throw new InvalidDataException();

            return await _productionReferenceRepository.DeactivateMediaType(id);
        }

        #endregion
    }
}