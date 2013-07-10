using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;


namespace PDFService
{
    class Program
    {
        static void Main(string[] args)
        {
            combinaPDF(new List<string> { "C:\\Users\\Jorge\\Desktop\\Partituras\\Variado\\03 Ver.pdf",
                "C:\\Users\\Jorge\\Desktop\\Partituras\\Variado\\04 Hor.pdf" });

        }

        private static void combinaPDF(List<string> files)
        {
            string originalFile = files[0];
            string destFile = "";
            PdfDocument output;

            //try
            //{
            //    output = PdfReader.Open(originalFile, PdfDocumentOpenMode.Modify);
            //}
            //catch (PdfSharp.PdfSharpException)
            //{
            //    destFile = System.IO.Path.GetTempFileName();
            //    System.IO.File.Copy(originalFile, destFile, true);
            //    output = new PdfDocument(destFile);
            //}
            
            destFile = System.IO.Path.GetTempFileName();
            output = new PdfDocument(destFile);

            foreach (string file in files)
            {
                //if (file != originalFile || !string.IsNullOrWhiteSpace(destFile))
                //{
                    PdfDocument input = PdfReader.Open(file, PdfDocumentOpenMode.Import);
                    int count = input.PageCount;
                    for (int idx = 0; idx < count; idx++)
                    {
                        PdfPage page = input.Pages[idx];
                        output.AddPage(page);
                    }
                    System.IO.File.Delete(file);
                //}
            }
            output.Save(originalFile);
            output.Close();
            output.Dispose();

            if (!string.IsNullOrWhiteSpace(destFile))
            {
                System.IO.File.Delete(originalFile);
                System.IO.File.Copy(destFile, originalFile, true);
                System.IO.File.Delete(destFile);
            }
        }
    }
}
