using System.ComponentModel.DataAnnotations;

namespace DocumentTrackingSystem.Web.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
