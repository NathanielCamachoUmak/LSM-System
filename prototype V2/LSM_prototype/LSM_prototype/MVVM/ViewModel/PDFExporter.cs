using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace LSM_prototype.MVVM.ViewModel
{
    public class PDFExporter
    {
        public void ExportToPDF(string filepart)
        {
            PdfWriter writer = new PdfWriter(filepart); 
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            
        }
    }
}
