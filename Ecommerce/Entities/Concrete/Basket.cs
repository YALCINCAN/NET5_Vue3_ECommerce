using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.Concrete
{
    public class Basket:IEntity
    {
        public Basket()
        {
            BasketItems = new List<BasketItem>();
        }
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }
    }
}
