using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    public class Shoes
    {
        public string ShoesImageUrl { get; set; }
        public List<string> ShoesVariants { get; set; }
        public string ShoesBrand { get; set; }
        public string ShoesTitle { get; set; }
        public string ShoesPrice { get; set; }

        public Shoes()
        {
            ShoesVariants = new List<string>();
        }
    }
}
