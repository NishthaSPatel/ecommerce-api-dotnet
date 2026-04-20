using DataAccess.Context;
using DataAccess.Repositories.IRepository.Production;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Repository.Production
{
    public class ProductionReferenceRepository(CoreFunctionalityContext context) : RepositoryBase(context), IProductionReferenceRepository
    {
        #region Brand

        public async Task<bool> DeactivateBrand(long id)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(x => x.Id == id);
            if (brand != null)
            {
                brand.IsDeleted = true;
                _context.Entry(brand).State = EntityState.Modified;

                var productList = await _context.Products.Where(x => x.BrandId == id).ToListAsync();

                foreach (var product in productList)
                {
                    product.IsDeleted = true;
                }
            }
            await _context.SaveChangesAsync();

            return true;
        }

        #endregion

        #region Category

        public async Task<bool> DeactivateCategory(long id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category != null)
            {
                category.IsDeleted = true;
                _context.Entry(category).State = EntityState.Modified;

                var productList = await _context.Products.Where(x => x.CategoryId == id).ToListAsync();

                foreach (var product in productList)
                {
                    product.IsDeleted = true;
                }
            }
            await _context.SaveChangesAsync();

            return true;
        }

        #endregion

        #region ProductType

        public async Task<bool> DeactivateProductType(long id)
        {
            var productType = await _context.ProductTypes.FirstOrDefaultAsync(x => x.Id == id);
            if (productType != null)
            {
                productType.IsDeleted = true;
                _context.Entry(productType).State = EntityState.Modified;

                var productList = await _context.Products.Where(x => x.ProductTypeId == id).ToListAsync();

                foreach (var product in productList)
                {
                    product.IsDeleted = true;
                }
            }
            await _context.SaveChangesAsync();

            return true;
        }

        #endregion

        #region MediaType

        public async Task<bool> DeactivateMediaType(long id)
        {
            var mediaType = await _context.MediaTypes.Include(x => x.Medias).FirstOrDefaultAsync(x => x.Id == id);
            if (mediaType != null)
            {
                mediaType.IsDeleted = true;
                _context.Entry(mediaType).State = EntityState.Modified;

                foreach (var media in mediaType.Medias)
                    media.IsDeleted = true;
            }
            await _context.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}