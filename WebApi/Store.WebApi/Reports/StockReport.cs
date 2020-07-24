using System;
using System.IO;
using System.Collections.Generic;
using Store.Business.Layer.Dtos;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Kernel.Geom;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.IO.Font.Constants;

namespace Store.WebApi.Reports
{
    public static class StockReport
    {
        public static byte[] Generate(IList<ProductStockReport> products)
        {
            byte[] content = null;
            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    var writer = new PdfWriter(memoryStream);
                    var pdfDocument = new PdfDocument(writer);
                    var document = new Document(pdfDocument, PageSize.A4);
                    document.SetMargins(50, 20, 20, 20);
                    document.SetFontSize(10);

                    var solidBorder1 = new SolidBorder(1);
                    var tableHeader = new Table(3);
                    tableHeader.SetWidth(UnitValue.CreatePercentValue(100));
                    Image image = new Image(ImageDataFactory.Create("Images/icons.png"));
                    var borderRadius = new BorderRadius(3);
                    image.SetBorderRadius(borderRadius);
                    image.SetBorder(new SolidBorder(1));
                    var iconCell = new Cell().Add(image);
                    iconCell.SetPadding(0);
                    iconCell.SetTextAlignment(TextAlignment.LEFT);
                    iconCell.SetBorder(Border.NO_BORDER);
                    tableHeader.AddCell(iconCell);

                    var title = new Paragraph("Reporte de Ventas");
                    title.SetFontColor(ColorConstants.RED);
                    title.SetFontSize(20);
                    title.SetTextAlignment(TextAlignment.CENTER);
                    title.SetBackgroundColor(ColorConstants.ORANGE, 0.5f);
                    title.SetBorderRadius(borderRadius);
                    var titleCell = new Cell().Add(title);
                    titleCell.SetPadding(0);
                    titleCell.SetVerticalAlignment(VerticalAlignment.BOTTOM);
                    titleCell.SetBorder(Border.NO_BORDER);
                    tableHeader.AddCell(titleCell);

                    var date = new Paragraph("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm tt"));

                    date.SetBorder(solidBorder1);
                    var dateCell = new Cell().Add(date);
                    dateCell.SetPadding(0);
                    dateCell.SetTextAlignment(TextAlignment.RIGHT);
                    dateCell.SetBorder(Border.NO_BORDER);
                    tableHeader.AddCell(dateCell);
                    tableHeader.SetMarginBottom(20);

                    document.Add(tableHeader);

                    var table = new Table(new float[] { 2, 5, 4, 3, 1, 1, 2, 1 });
                    table.SetWidth(UnitValue.CreatePercentValue(100));

                    table.SetBorder(solidBorder1);

                    PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                    table.AddHeaderCell(new Cell().Add(new Paragraph("Código")).SetFont(font).SetBorderBottom(solidBorder1).SetBorderRight(solidBorder1).SetBackgroundColor(ColorConstants.ORANGE));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Descripción")).SetFont(font).SetBorderBottom(solidBorder1).SetBorderLeft(solidBorder1).SetBackgroundColor(ColorConstants.ORANGE));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Detalles")).SetFont(font).SetBorderBottom(solidBorder1).SetBorderLeft(solidBorder1).SetBackgroundColor(ColorConstants.ORANGE));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Marca")).SetFont(font).SetBorderBottom(solidBorder1).SetBorderLeft(solidBorder1).SetBackgroundColor(ColorConstants.ORANGE));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Talle")).SetFont(font).SetBorderBottom(solidBorder1).SetBorderLeft(solidBorder1).SetBackgroundColor(ColorConstants.ORANGE));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Color")).SetFont(font).SetBorderBottom(solidBorder1).SetBorderLeft(solidBorder1).SetBackgroundColor(ColorConstants.ORANGE));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Categoria")).SetFont(font).SetBorderBottom(solidBorder1).SetBorderLeft(solidBorder1).SetBackgroundColor(ColorConstants.ORANGE));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Stock")).SetFont(font).SetBorderBottom(solidBorder1).SetBorderLeft(solidBorder1).SetBackgroundColor(ColorConstants.ORANGE));

                    foreach (ProductStockReport productStock in products)
                    {
                        table.AddCell(new Cell().Add(new Paragraph(productStock.Code)));
                        table.AddCell(new Cell().Add(new Paragraph(productStock.Description)));
                        table.AddCell(new Cell().Add(new Paragraph(productStock.Detail + "")));
                        table.AddCell(new Cell().Add(new Paragraph(productStock.Brand + "")));
                        table.AddCell(new Cell().Add(new Paragraph(productStock.Size)));
                        table.AddCell(new Cell().Add(new Paragraph(productStock.Color)));
                        table.AddCell(new Cell().Add(new Paragraph(productStock.Category)));
                        table.AddCell(new Cell().Add(new Paragraph(productStock.Stock + "")));
                    }

                    document.Add(table);
                    document.Close();

                    content = memoryStream.GetBuffer();
                    memoryStream.Flush();
                    memoryStream.Close();
                    memoryStream.Dispose();
                }
                return content;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
