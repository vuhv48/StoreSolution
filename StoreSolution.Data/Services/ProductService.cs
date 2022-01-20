using StoreSolution.Data.Areas.Stores.ViewModels;
using StoreSolution.Data.Common;
using StoreSolution.Data.Data.EF;
using StoreSolution.Data.Models;
using System.Net.Http.Headers;

namespace StoreSolution.Data.Services
{
    public class ProductService : IProductService
    {
        private readonly StoreDbContext _context;
        private readonly IStorageService _storageService;
        public ProductService(StoreDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CreateDate = DateTime.Now,
                StockQuantity = 1,
                VendorId = 1,
                BrandId = 1,
                CreateById = 1,
                IsDeleted = false,

            };
            //Save image
            if (request.ThumbnailImage != null)
            {
                product.ProductImages = new List<ProductImage>
                    {
                        new ProductImage()
                        {
                            Caption = "Thumabnail image",
                            DateCreate = DateTime.Now,
                            FileSize = request.ThumbnailImage.Length,
                            ImagePath = await this.SaveFile(request.ThumbnailImage),
                            IsDefault = true,
                            ProductId = product.Id
                        }
                    };
            }
            //           
            _context.Products.Add(product);
            
            await _context.SaveChangesAsync();
            var productInCategory = new ProductInCategory()
            {
                CategoryId = 1,
                ProductId = product.Id,
                IsFeaturedProduct = true,
            };
            _context.ProductCategories.Add(productInCategory);

            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null) throw new Exception("Cannot product");
            var images = _context.ProductImages.Where(i => i.ProductId == productId);
            foreach(var image in images)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            }
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<ProductVm> GetById(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if(product == null)
            {
                throw new Exception();
            }
            var image =  _context.ProductImages.Where(x => x.ProductId == productId && x.IsDefault == true).FirstOrDefault();
            var categories =  (from c in _context.Categories
                                    join pc in _context.ProductCategories on c.Id equals pc.CategoryId
                                    where pc.ProductId == productId
                                    select c.Name).ToList();
            var productVm = new ProductVm
            {
                Id = productId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                VendorId = product.VendorId,
                BrandId = product.BrandId,
                ThumbnailImage = image != null ? image.ImagePath : "no-image.jpg",
                
            };
            return productVm;

        }
        public async Task<IEnumerable<ProductVm>> GetAll()
        {
            //var query = from c in _context.Categories
            //            join pc in _context.ProductCategories on c.Id equals pc.CategoryId
            //            join p in _context.Products on pc.ProductId equals p.Id
            //            join pi in _context.ProductImages on p.Id equals pi.ProductId
            //            into picture from ProductImages in picture.DefaultIfEmpty()
            //            select new { c, pc, p, pi };

            var query = from p in _context.Products
                        join pi in _context.ProductImages on p.Id equals pi.ProductId
                        //into pi from ProductImages in pi.DefaultIfEmpty()
                        select new {p,pi};

            var data = query.Select(x => new ProductVm { 
                    Id = x.p.Id,
                    Name = x.p.Name,
                    Description= x.p.Description,
                    Price= x.p.Price,
                    StockQuantity= x.p.StockQuantity,
                    VendorId= x.p.VendorId,
                    BrandId= x.p.BrandId,
                    ThumbnailImage = x.pi.ImagePath,
                    CreateDate = x.p.CreateDate,

            }).ToList();

            return data;
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            if(product == null)
                throw new Exception();
           
            if (request.ThumbnailImage != null)
            {
                var thumbnailImage = _context.ProductImages.FirstOrDefault(i=>i.IsDefault == true && i.ProductId == request.Id);
                if(thumbnailImage != null)
                {
                    thumbnailImage.FileSize = request.ThumbnailImage.Length;
                    thumbnailImage.ImagePath = await this.SaveFile(request.ThumbnailImage);
                    _context.ProductImages.Update(thumbnailImage);
                }
            }
            
            product.Name = request.Name;
            product.Description = request.Description;
            _context.Products.Update(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductVm>> GetLatestProducts()
        {
            var query = from p in _context.Products
                        join pi in _context.ProductImages on p.Id equals pi.ProductId
                        //into pi from ProductImages in pi.DefaultIfEmpty()
                        select new { p, pi };

            var data = query.Select(x => new ProductVm
            {
                Id = x.p.Id,
                Name = x.p.Name,
                Description = x.p.Description,
                Price = x.p.Price,
                StockQuantity = x.p.StockQuantity,
                VendorId = x.p.VendorId,
                BrandId = x.p.BrandId,
                ThumbnailImage = x.pi.ImagePath,
                CreateDate = x.p.CreateDate,

            }).OrderBy(x=>x.CreateDate).ToList();

            return data;
        }

        public async Task<IEnumerable<ProductVm>> GetProductsWithCategory(int categoryId)
        {
            var query = from c in _context.Categories
                         join pc in _context.ProductCategories on c.Id equals pc.CategoryId
                         join p in _context.Products on pc.ProductId equals p.Id
                         join pi in _context.ProductImages on p.Id equals pi.ProductId
                         where c.Id == categoryId
                         select new { c,pc,p,pi };
            var data = query.Select(x => new ProductVm
            {
                Id = x.p.Id,
                Name = x.p.Name,
                Description = x.p.Description,
                Price = x.p.Price,
                StockQuantity = x.p.StockQuantity,
                VendorId = x.p.VendorId,
                BrandId = x.p.BrandId,
                ThumbnailImage = x.pi.ImagePath,
                CreateDate = x.p.CreateDate,
            }).OrderBy(x => x.CreateDate).ToList();

            return data;
        }

        public Task<IEnumerable<ProductVm>> GetProductsWithCategoryAndBrand()
        {
            throw new NotImplementedException();
        }
    }
}
