using System;

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

        public Document(string documentName, string documentPath, string documentType, DateTime date)
        {
            SetDocumentName(documentName);
            SetDocumentPath(documentPath);
            SetDocumentType(documentType);
            SetDate(date);
        }

        public static Document Rehydrate(
            int id,
            string documentName, 
            string documentPath, 
            string documentType, 
            DateTime date
            )
        {
            var document = new Document(documentName, documentPath, documentType, date);
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

        public void UpdateDocumentPath(string newDocumentPath)
        {
            SetDocumentPath(newDocumentPath);
        }

        private void SetDocumentType(string documentType)
        {
            if (string.IsNullOrWhiteSpace(documentType))
                throw new ArgumentException("Document type name cannot be empty");

            DocumentType = documentType.Trim();
        }

        public void UpdateDocumentType(string newDocumentType)
        {
            SetDocumentType(newDocumentType);
        }

        private void SetDate(DateTime date)
        {
            if (date.Year < 2000 || date.Year > DateTime.Now.Year + 1)
                throw new ArgumentException("Invalid year");

            Day = date.Day;
            MonthId = date.Month;
            Year = date.Year;
        }
    }
}
