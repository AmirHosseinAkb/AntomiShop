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
using Antomi.Data.Entities.Order;
using Antomi.Data.Entities.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Antomi.Core.Services
{
    public class ProductService : IProductService
    {
        private AntomiContext _context;
        public ProductService(AntomiContext context)
        {
            _context = context;
        }

        public void AddColorToProduct(ProductColor productColor)
        {
            _context.ProductColors.Add(productColor);
            _context.SaveChanges();
        }

        public void AddGroup(ProductGroup group, IFormFile groupPic)
        {
            group.GroupImageName = "Group.png";
            if (groupPic != null)
            {
                group.GroupImageName = NameGenerator.GenerateUniqName() + Path.GetExtension(groupPic.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "Groups",
                    group.GroupImageName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    groupPic.CopyTo(stream);
                }
            }
            _context.ProductGroups.Add(group);
            _context.SaveChanges();
        }

        public void AddInventoryHistory(InventoryHistory history)
        {
            _context.InventoryHistories.Add(history);
            _context.SaveChanges();
        }

        public void AddImageToProduct(int productId, IFormFile imagePic)
        {
            ProductImage productImage = new ProductImage()
            {
                ImageName = NameGenerator.GenerateUniqName() + Path.GetExtension(imagePic.FileName),
                ProductId = productId
            };
            if (imagePic != null)
            {
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "Product",
                    "ProductImages",
                    productImage.ImageName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imagePic.CopyTo(stream);
                }
            }
            _context.ProductImages.Add(productImage);
            _context.SaveChanges();
        }

        public void AddProduct(Product product, IFormFile productPic)
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
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "Product",
                    "Thumbnails",
                    product.ProductImageName);
                ImageConvertor imageConvertor = new ImageConvertor();
                imageConvertor.Image_resize(imagePath, thumbPath, 400);
            }
            _context.Products.Add(product);
            ProductInventory productInventory = new ProductInventory()
            {
                ProductId = product.ProductId,
                ProductCount = 0,
            };
            _context.ProductInventories.Add(productInventory);
            _context.SaveChanges();
        }

        public void BuyProduct(int productId, string email, int colorId, int count)
        {
            var product = GetProductById(productId);
            var userId = _context.Users.SingleOrDefault(u => u.Email == EmailConvertor.FixEmail(email)).UserId;
            var order = _context.Orders.SingleOrDefault(o => o.UserId == userId && !o.IsFinally);
            if (order == null)
            {
                order = new Order()
                {
                    UserId = userId,
                    IsFinally = false,
                    PaymentStatus = "در حال انتظار",
                    OrderSum=product.ProductPrice*count,
                    PaidPrice=product.ProductPrice*count
                };
                _context.Orders.Add(order);
                _context.SaveChanges();
                OrderDetail orderDetail = new OrderDetail()
                {
                    OrderId = order.OrderId,
                    ProductId = product.ProductId,
                    ColorId=colorId,
                    Count = count,
                    UnitPrice = product.ProductPrice
                };
                _context.ProductInventories.SingleOrDefault(i => i.ProductId == product.ProductId).ProductCount -= count;
                _context.OrderDetails.Add(orderDetail);
                _context.SaveChanges();
            }
            else
            {
                var orderDetail = _context.OrderDetails.SingleOrDefault(d => d.OrderId == order.OrderId && d.ProductId == productId&&d.ColorId==colorId);
                if (orderDetail == null)
                {
                    orderDetail = new OrderDetail()
                    {
                        OrderId = order.OrderId,
                        ProductId = product.ProductId,
                        ColorId=colorId,
                        Count = count,
                        UnitPrice = product.ProductPrice
                    };
                    _context.OrderDetails.Add(orderDetail);
                }
                else
                {
                    orderDetail.Count+=count;
                }
                order.OrderSum += product.ProductPrice * count;
                order.PaidPrice+=product.ProductPrice * count;
                _context.ProductInventories.SingleOrDefault(i => i.ProductId == product.ProductId).ProductCount -= count;
                _context.SaveChanges();
            }
            
        }

        public void DeleteColor(int colorId)
        {
            var color = _context.ProductColors.Find(colorId);
            color.IsDeleted = true;
            _context.SaveChanges();
        }

        public void DeleteGroup(int groupId)
        {
            var group = GetGroupById(groupId);
            group.IsDeleted = true;
            _context.SaveChanges();
        }

        public void DeleteImage(int imageId)
        {
            var image = _context.ProductImages.Find(imageId);
            _context.ProductImages.Remove(image);
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
            product.IsDeleted = true;
            _context.SaveChanges();
        }

        public void EditGroup(ProductGroup group, IFormFile groupPic)
        {
            if (groupPic != null)
            {
                string imagePath = "";
                if (group.GroupImageName != "Group.png")
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "Groups",
                        group.GroupImageName);
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }
                group.GroupImageName = NameGenerator.GenerateUniqName() + Path.GetExtension(groupPic.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "Groups",
                        group.GroupImageName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    groupPic.CopyTo(stream);
                }
            }
            UpdateGroup(group);
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
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    productPic.CopyTo(stream);
                }
                thumbPath = Path.Combine(Directory.GetCurrentDirectory(),
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

        public List<ProductGroup> GetAllGroups(string filterGroupName = "")
        {
            IQueryable<ProductGroup> result = _context.ProductGroups.Include(g => g.ProductGroups).ThenInclude(g => g.ProductGroups);
            if (!string.IsNullOrEmpty(filterGroupName))
            {
                result = result.Where(g => g.GroupTitle.Contains(filterGroupName));
            }
            return result.ToList();
        }

        public ProductGroup GetGroupById(int groupId)
        {
            return _context.ProductGroups.Find(groupId);
        }

        public List<SelectListItem> GetGroups()
        {
            return _context.ProductGroups.Where(g => g.ParentId == null)
                .Select(g => new SelectListItem()
                {
                    Text = g.GroupTitle,
                    Value = g.GroupId.ToString()
                }).ToList();
        }

        public List<ProductImage> GetImagesOfProduct(int productId)
        {
            return _context.ProductImages.Where(i => i.ProductId == productId).ToList();
        }

        public Product GetProductById(int productId)
        {
            return _context.Products.Find(productId);
        }

        public ProductColor GetProductColorById(int colorId)
        {
            return _context.ProductColors.Find(colorId);
        }

        public Product GetProductForShow(int productId)
        {
            return _context.Products
                .Include(p => p.ProductGroup)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductColors)
                .Include(p => p.ProductInventory)
                .SingleOrDefault(p => p.ProductId == productId);
        }

        public List<InventoryHistory> GetProductInventoryHistory(int productId)
        {
            return _context.InventoryHistories.Where(i => i.ProductId == productId).ToList();
        }

        public ShowProductItemsViewModel GetProducts(int pageId = 1, string filterProductName = "", string orderType = "createDate"
            , int minPrice = 0, int maxPrice = 0, List<int> selectedGroups = null, int take = 12)
        {
            IQueryable<Product> result = _context.Products.Include(p => p.ProductInventory);
            if (!string.IsNullOrEmpty(filterProductName))
            {
                result = result.Where(p => p.ProductTitle.Contains(filterProductName));
            }
            switch (orderType)
            {
                case "createDate":
                    {
                        result = result.OrderByDescending(p => p.CreateDate);
                        break;
                    }
                case "ascendingPrice":
                    {
                        result = result.OrderBy(p => p.ProductPrice);
                        break;
                    }
                case "descendingPrice":
                    {
                        result = result.OrderByDescending(p => p.ProductPrice);
                        break;
                    }
            }
            if (minPrice > 0)
            {
                result = result.Where(p => p.ProductPrice > minPrice);
            }
            if (maxPrice > 0)
            {
                result = result.Where(p => p.ProductPrice < maxPrice);
            }
            if (selectedGroups != null)
            {
                foreach (int groupId in selectedGroups)
                {
                    result = result.Where(p => p.GroupId == groupId || p.SubId == groupId || p.SecSubId == groupId);
                }
            }
            int skip = (pageId - 1) * take;
            var pageCount = result.Count() / take;
            if (result.Count() % take != 0)
            {
                pageCount++;
            }
            ShowProductItemsViewModel productItemsViewModel = new ShowProductItemsViewModel()
            {
                ProductBoxInformations = result.Skip(skip).Take(take).Select(p => new ProductBoxInformationsViewModel()
                {
                    ProductId = p.ProductId,
                    ProductTitle = p.ProductTitle,
                    ProductImageName = p.ProductImageName,
                    InventoryCount = p.ProductInventory.ProductCount,
                    ProductPrice = p.ProductPrice
                }).ToList(),
                CurrentPage = pageId,
                PageCount = pageCount
            };
            return productItemsViewModel;
        }

        public ShowProductsInAdminViewModel GetProductsForShowInAdmin(int pageId = 1, string filterName = "")
        {
            IQueryable<Product> result = _context.Products.Include(p => p.ProductColors);
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

        public ShowProductsInventoryViewModel GetProductsForShowInventory(int pageId, string filterProductName = "")
        {
            IQueryable<Product> result = _context.Products;
            if (!string.IsNullOrEmpty(filterProductName))
            {
                result = result.Where(p => p.ProductTitle.Contains(filterProductName));
            }
            int take = 10;
            int skip = (pageId - 1) * take;
            ShowProductsInventoryViewModel showProductsInventory = new ShowProductsInventoryViewModel()
            {
                InventoryInformations = result.Include(p => p.ProductInventory).Skip(skip).Take(take).Select(p => new InventoryInformationsViewModel()
                {
                    ProductId = p.ProductId,
                    ProductPrice = p.ProductPrice,
                    ProductTitle = p.ProductTitle,
                    InventoryCount = p.ProductInventory.ProductCount,
                    CreateDate = p.CreateDate
                }).ToList(),
                CurrentPage = pageId,
                PageCount = result.Count() / take
            };
            if (result.Count() % take != 0)
            {
                showProductsInventory.PageCount++;
            }
            return showProductsInventory;
        }

        public List<ProductBoxInformationsViewModel> GetRelatedProducts(int productId)
        {
            var product = GetProductById(productId);
            List<string> ProductTags = product.ProductTags.Split('-', StringSplitOptions.RemoveEmptyEntries).ToList();
            IQueryable<Product> result = _context.Products;
            foreach (var tag in ProductTags)
            {
                var currentTag = tag;
                result = result.Where(p => p.ProductTags.Contains(tag));
            }
            return result.Take(10).Select(p => new ProductBoxInformationsViewModel()
            {
                ProductId = p.ProductId,
                ProductTitle = p.ProductTitle,
                ProductPrice = p.ProductPrice,
                ProductImageName = p.ProductImageName,
                InventoryCount = p.ProductInventory.ProductCount
            }).ToList();
        }

        public List<SelectListItem> GetSubGroupsOfGroups(int groupId)
        {
            return _context.ProductGroups.Where(g => g.ParentId == groupId)
                .Select(g => new SelectListItem()
                {
                    Text = g.GroupTitle,
                    Value = g.GroupId.ToString()
                }).ToList();
        }

        public void UpdateGroup(ProductGroup group)
        {
            _context.ProductGroups.Update(group);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void ChangeProductInventory(int productId, int count)
        {
            _context.ProductInventories.SingleOrDefault(i => i.ProductId == productId).ProductCount += count;
            _context.SaveChanges();
        }
    }
}
