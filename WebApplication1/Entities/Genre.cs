using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Entities
{
    public class Genre
    {
        public int Id { get; set; }

        public required string Name { get; set; }
    }
}