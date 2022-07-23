using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace TsentrstroyAPI.Data
{
    public class SubCategory : EntityFrameworkBase
    {
        [NotNull]
        public string Name { get; set; }
        
        [NotNull, JsonIgnore]
        public Category Category { get; set; }
        
        public bool IsActive { get; set; }
    }
}