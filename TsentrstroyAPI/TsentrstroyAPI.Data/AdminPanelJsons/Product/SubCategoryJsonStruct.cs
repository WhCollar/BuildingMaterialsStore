using System.Collections.Generic;

namespace TsentrstroyAPI.Data.AdminPanelJsons.Product
{
    public class SubCategoryJsonStruct
    {
        public int Id { get; set; }
            
        public string Name { get; set; }

        public bool IsActive { get; set; }
            
        public List<Data.Product> Products { get; set; }
    }
}