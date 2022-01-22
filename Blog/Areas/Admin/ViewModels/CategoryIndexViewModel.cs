using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Areas.Admin.ViewModels
{
    public class CategoryIndexViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PostCount { get; set; }
    }
}
