using System.Collections.Generic;

namespace TsentrstroyAPI.Data
{
    public class ProductFeatures
    {
        public int Id { get; set; }

        public string Compound { get; set; }
        public List<InformationTab> Tabs { get; set; }
        public List<string> Notes { get; set; }
    }
}