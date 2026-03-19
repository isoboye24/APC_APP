using System;

namespace APC.Applications.DTO
{
    public class DocumentDTO
    {
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
        public string DocumentType { get; set; }
        public DateTime Date { get; set; }
        public string FormattedDate { get; set; }
    }
}
