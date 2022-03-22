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

        #endregion
    }
}