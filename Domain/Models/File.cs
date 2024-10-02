using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class File
    {
        public string FileId { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public int MessageId { get; set; }
        public string ClientId { get; set; } = null!;
        public DateTime UploadAt { get; set; } = DateTime.Now;

        public virtual User Client { get; set; } = null!;
        public virtual Message Message { get; set; } = null!;
    }
}