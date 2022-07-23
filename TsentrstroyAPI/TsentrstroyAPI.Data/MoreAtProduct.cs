using System.Collections.Generic;
using TsentrstroyAPI.Data;

namespace TsentrstroyAPI.Model
{
    public class MoreAtProduct
    {
        public int Id { get; set; }

        public string Gost { get; set; }
        
        public List<string> Advantages { get; set; }
        
        public List<AreasOfUseTab> AreasOfUse { get; set; }
        
        public string Compound { get; set; }
        public List<FeatureListTab> FeatureList { get; set; }
        public List<string> Notes { get; set; }
        
        public Product Product { get; set; }
        
    }
}