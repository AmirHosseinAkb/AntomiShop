using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antomi.Core.Convertors;
using Antomi.Core.DTOs.Product;
using Antomi.Core.Generators;
using Antomi.Core.Services.Interfaces;
using Antomi.Data.Context;
using Antomi.Data.Entities.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Antomi.Core.Services
{
    public class ProductService : IProductService
    {
        private AntomiContext _context;
        public ProductService(AntomiContext context)
        {
            _context = context;
        }

        public void AddProduct(Product product,IFormFile productPic)
        {
            product.ProductImageName = "Product.png";
            if (productPic != null)
            {
                product.ProductImageName = NameGenerator.GenerateUniqName() + Path.GetExtension(productPic.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "Product",
                    "Images",
                    product.ProductImageName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    productPic.CopyTo(stream);
                }
                string thumbPath= Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "Product",
                    "Thumbnails",
                    product.ProductImageName);
                ImageConvertor imageConvertor = new ImageConvertor();
                imageConvertor.Image_resize(imagePath, thumbPath, 400);
            }
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(int productId)
        {
            var product = GetProductById(productId);
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "Product",
                "Images",
                product.ProductImageName);
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
            string thumbPath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "Product",
                "Thumbnails",
                product.ProductImageName);
            if (File.Exists(thumbPath))
            {
                File.Delete(thumbPath);
            }
            product.IsDeleted=true;
            _context.SaveChanges();
        }

        public void EditProduct(Product product, IFormFile productPic)
        {
            if (productPic != null)
            {
                string imagePath = "";
                string thumbPath = "";
                if (product.ProductImageName != "Product.png")
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "Product",
                        "Images",
                        product.ProductImageName);
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                    thumbPath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "Product",
                        "Thumbnails",
                        product.ProductImageName);
                    if (File.Exists(thumbPath))
                    {
                        File.Delete(thumbPath);
                    }
                }
                product.ProductImageName = NameGenerator.GenerateUniqName() + Path.GetExtension(productPic.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "Product",
                        "Images",
                        product.ProductImageName);
                using(var stream=new FileStream(imagePath, FileMode.Create))
                {
                    productPic.CopyTo(stream);
                }
                thumbPath= Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "Product",
                        "Thumbnails",
                        product.ProductImageName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    productPic.CopyTo(stream);
                }
            }
            UpdateProduct(product);
        }

        public List<SelectListItem> GetGroups()
        {
            return _context.ProductGroups.Where(g=>g.ParentId==null)
                .Select(g => new SelectListItem()
                {
                    Text = g.GroupTitle,
                    Value = g.GroupId.ToString()
                }).ToList();
        }

        public Product GetProductById(int productId)
        {
            return _context.Products.Find(productId);
        }

        public ShowProductsInAdminViewModel GetProductsForShowInAdmin(int pageId = 1, string filterName = "")
        {
            IQueryable<Product> result = _context.Products;
            if (!string.IsNullOrEmpty(filterName))
            {
                result = result.Where(p => p.ProductTitle.Contains(filterName));
            }
            int take = 2;
            int skip = (pageId - 1) * take;
            var showProductsVM = new ShowProductsInAdminViewModel()
            {
                Products = result.Skip(skip).Take(take).ToList(),
                CurrentPage = pageId,
                PageCount = result.Count() / take
            };
            if (result.Count() % take != 0)
            {
                showProductsVM.PageCount++;
            }
            return showProductsVM;
        }

        public List<SelectListItem> GetSubGroupsOfGroups(int groupId)
        {
            return _context.ProductGroups.Where(g => g.ParentId==groupId)
                .Select(g => new SelectListItem()
                {
                    Text = g.GroupTitle,
                    Value = g.GroupId.ToString()
                }).ToList();
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
    }
}
