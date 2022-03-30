﻿using Antomi.Data.Entities.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antomi.Core.DTOs.Product;

namespace Antomi.Core.Services.Interfaces
{
    public interface IProductService
    {
        #region Informations
        List<SelectListItem> GetGroups();
        List<SelectListItem> GetSubGroupsOfGroups(int groupId);

        #endregion
        #region Product
        void AddProduct(Product product,IFormFile productPic);
        ShowProductsInAdminViewModel GetProductsForShowInAdmin(int pageId=1,string filterName="");   
        Product GetProductById(int productId);
        void EditProduct(Product product,IFormFile productPic);
        void UpdateProduct(Product product);
        void DeleteProduct(int productId);
        ShowProductItemsViewModel GetProducts(int pageId=1,string filterProductName="",string orderType="createDate"
            ,int minPrice=0,int maxPrice=0,List<int> selectedGroups=null,int take=12);
        Product GetProductForShow(int productId);
        #endregion

        #region Groups
        List<ProductGroup> GetAllGroups(string filterGroupName = "");
        void AddGroup(ProductGroup group, IFormFile groupPic);
        ProductGroup GetGroupById(int groupId);
        void EditGroup(ProductGroup group, IFormFile groupPic);
        void UpdateGroup(ProductGroup group);
        void DeleteGroup(int groupId);
        #endregion

        #region Images
        void AddImageToProduct(int productId, IFormFile imagePic);
        List<ProductImage> GetImagesOfProduct(int productId);
        void DeleteImage(int imageId);
        #endregion

        #region Inventory
        ShowProductsInventoryViewModel GetProductsForShowInventory(int pageId, string filterProductName = "");
        void AddInventory(ProductInventory inventory);
        List<ProductInventory> GetProductInventoryHistory(int productId);
        #endregion
        #region Colors

        void AddColorToProduct(ProductColor productColor);
        void DeleteColor(int colorId);
        ProductColor GetProductColorById(int colorId);
        List<ProductBoxInformationsViewModel> GetRelatedProducts(int productId);

        #endregion
    }
}
