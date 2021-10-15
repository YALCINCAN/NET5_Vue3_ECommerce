using AutoFilterer.Attributes;
using AutoFilterer.Enums;
using AutoFilterer.Types;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.Filters
{
    public class CategoryFilter: FilterBase
    {
        [StringFilterOptions(StringFilterOption.Contains)]
        public string Slug { get; set; }
    }
}
