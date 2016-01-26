using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gluteneria.Models
{
    public class User 
    {
        public string Email { get; set; }
        public string name  { get; set; }
        public string passwordHash { get; set; }
              
    }

    public class HotSeat
    {
        [Key]
        public int Id { get; set; }

        public User User { get; set; }

    }





    class ApplicatonDbContext : DbContext
    {


        public ApplicatonDbContext(): base("DefaultConnection")
        {

        }
    }
}
