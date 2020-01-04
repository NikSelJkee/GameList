using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameListApp
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public Company Company { get; set; }
    }
}
