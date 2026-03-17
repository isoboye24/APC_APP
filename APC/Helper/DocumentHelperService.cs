using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace APC.Helper
{
    public class DocumentHelperService
    {
        public enum DocumentGridType
        {
            Basic
        }

        public static void ConfigureDocumentGrid(DataGridView grid, DocumentGridType type)
        {
            switch (type)
            {
                case DocumentGridType.Basic:
                    GeneralHelper.SetVisibleColumns(grid, "DocumentName", "DocumentType", "Date");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "DocumentName", "Document Name" },
                                    { "DocumentType", "Document Type" }
                                });
                    break;
                default:
                    GeneralHelper.SetVisibleColumns(grid, "", "", "");
                    break;
            }
        }
    }
}
