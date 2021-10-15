using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ReviewDTO : IDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int ProductId { get; set; }
        public byte RatingValue { get; set; }
    }
}
