using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Entities
{

    public class Document
    {
        public int DocumentId { get; private set; }
        public string DocumentName { get; private set; }
        public string DocumentPath { get; private set; }
        public string DocumentType { get; private set; }
        public int Day { get; private set; }
        public int MonthId { get; private set; }
        public int Year { get; private set; }

        public Document(string documentName, string documentPath, string documentType, int day, int monthId, int year)
        {
            SetDocumentName(documentName);
            SetDocumentPath(documentPath);
        }

        public static Document Rehydrate(
            int id,
            string documentName, 
            string documentPath, 
            string documentType, 
            int day, 
            int monthId, 
            int year
            )
        {
            var document = new Document(documentName, documentPath, documentType, day, monthId, year);
            document.DocumentId = id;
            return document;
        }

        private void SetDocumentName(string documentName)
        {
            if (string.IsNullOrWhiteSpace(documentName))
                throw new ArgumentException("Document name cannot be empty");

            DocumentName = documentName.Trim();
        }

        public void UpdateDocumentName(string newDocumentName)
        {
            SetDocumentName(newDocumentName);
        }

        private void SetDocumentPath(string documentPath)
        {
            if (string.IsNullOrWhiteSpace(documentPath))
                throw new ArgumentException("Document path cannot be empty");

            DocumentPath = documentPath.Trim();
        }

        public void UpdateSection(string newDocumentPath)
        {
            SetDocumentPath(newDocumentPath);
        }
    }
}
