using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ReviewDetail
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime ReviewDate { get; set; }
        public UserDetail User { get; set; }
        public byte RatingValue { get; set; }
    }
}
