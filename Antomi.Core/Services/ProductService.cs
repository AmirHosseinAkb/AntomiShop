using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antomi.Core.Services.Interfaces;
using Antomi.Data.Context;
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
        public List<SelectListItem> GetGroupsForSelect()
        {
            return _context.ProductGroups.Where(g=>g.ParentId==null)
                .Select(g => new SelectListItem()
                {
                    Text = g.GroupTitle,
                    Value = g.GroupId.ToString()
                }).ToList();
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
    }
}
