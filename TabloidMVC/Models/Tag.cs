using System.Composition.Convention;
using System.ComponentModel.DataAnnotations;

namespace TabloidMVC.Models
{
    public class Tag
    {
        // Tag identifier and Name of the Tag
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
