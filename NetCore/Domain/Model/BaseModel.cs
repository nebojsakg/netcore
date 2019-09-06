using System;

namespace Domain.Model
{
    public abstract class BaseModel
    {
        public int Id { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; }

        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
    }
}
