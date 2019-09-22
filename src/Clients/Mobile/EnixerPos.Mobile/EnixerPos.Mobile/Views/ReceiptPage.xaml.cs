using EnixerPos.Api.ViewModels.Sale;
using EnixerPos.Mobile.Dependency;
using EnixerPos.Mobile.ViewModels;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EnixerPos.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReceiptPage : ContentPage
    {
        SKCanvas canvas;
        SKBitmap toBitmap;
        ChargeViewModel _vm;
        ReceiptViewModel Receipt = new ReceiptViewModel()
        {
            Discount = 0,
            TicketNumber = 1234,
            ItemList = new List<OrderItemModel>()
            {
                new OrderItemModel(){ItemName = "Americano", DiscountedPrice = 30, ItemDiscount = 20, ItemPrice = 55, IsDiscounted = true, OptionName = "Iced", OptionPrice = 5, Quantity = 1},
                new OrderItemModel(){ItemName = "Americano", DiscountedPrice = 55, ItemDiscount = 20, ItemPrice = 55, IsDiscounted = false, OptionName = "Iced", OptionPrice = 5, Quantity = 1},
                new OrderItemModel(){ItemName = "Latte", DiscountedPrice = 30, ItemDiscount = 20, ItemPrice = 55, IsDiscounted = true, OptionName = "Iced", OptionPrice = 5, Quantity = 1},
                new OrderItemModel(){ItemName = "Cappucino", DiscountedPrice = 55, ItemDiscount = 20, ItemPrice = 55, IsDiscounted = false, OptionName = "Iced", OptionPrice = 5, Quantity = 1},
            },
            CreateDateTime = DateTime.Now,
            Total = 30,
            TotalDiscount = 25,
        };

        public ReceiptPage(ChargeViewModel vm)
        {
            _vm = vm;
            InitializeComponent();
            Receipt = vm.Receipt;
            CreateReceipt();
            BindingContext = vm;
        }

        public ReceiptPage()
        {
            InitializeComponent();
            CreateReceipt();
        }

        private void SKCanvasView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            var surface = e.Surface;
            var canvas = surface.Canvas;
            canvas.Clear();
            canvas.DrawBitmap(toBitmap, 0, 0);
            //if (toBitmap != null)
            //{
            //    info.Height = info.Width;
            //    var resizedBitmap = toBitmap.Resize(info, SKBitmapResizeMethod.Mitchell);
            //    canvas.DrawBitmap(resizedBitmap, info.Width / 2 - resizedBitmap.Width / 2, info.Height / 2 - resizedBitmap.Height / 2);
            //}
        }

        bool CreateReceipt()
        {
            try
            {
                ICreateReceipt photoLibrary = DependencyService.Get<ICreateReceipt>();
                string filename = DateTime.Now.ToString("yyyyMMdd-hhmmss");
                string extension = ".png";
                var data = CreateImage();
                //bool result = photoLibrary.SavePhotoAsync(data, "Xamarin", filename + extension).Result;
                sKCanvasView.PaintSurface += SKCanvasView_PaintSurface;  // += SKCanvasView_PaintSurface;
                //if (!result)
                //{
                //    DisplayAlert("Error", "Cannot save new receipt please enable local storage", "Ok");
                //}
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private byte[] CreateImage()
        {
            this.GetType().Assembly.GetManifestResourceNames();
            var assembly = typeof(ReceiptPage).GetTypeInfo().Assembly;


            var canvasWidth = 1000;
            var canvasHeight = 800 + (Receipt.ItemList.Count * 170);
            sKCanvasView.HeightRequest = canvasHeight;
            sKCanvasView.WidthRequest = canvasWidth;
            toBitmap = new SKBitmap(canvasWidth, canvasHeight, SKColorType.Rgb565, SKAlphaType.Opaque);

            canvas = new SKCanvas(toBitmap);
            canvas.Clear(SKColors.White);
            canvas.ResetMatrix();

            var defaultText = new SKPaint
            {
                Typeface = SKTypeface.Default,
                TextSize = 40.0f,
                IsAntialias = true,
                Color = new SKColor(0, 0, 0, 255),
                TextAlign = SKTextAlign.Left
            };
            var discountedItemPrice = new SKPaint
            {
                Typeface = SKTypeface.Default,
                TextSize = 40.0f,
                IsAntialias = true,
                Color = new SKColor(0, 0, 0, 255),
                TextAlign = SKTextAlign.Right
            };
            var smallText = new SKPaint
            {
                Typeface = SKTypeface.Default,
                TextSize = 30.0f,
                IsAntialias = true,
                Color = new SKColor(128, 128, 128, 255),
                TextAlign = SKTextAlign.Left
            };
            var totalPricePaint = new SKPaint
            {
                Typeface = SKTypeface.Default,
                TextSize = 80.0f,
                IsAntialias = true,
                Color = new SKColor(0, 0, 0, 255),
                TextAlign = SKTextAlign.Center
            };
            var totalPaint = new SKPaint
            {
                Typeface = SKTypeface.Default,
                TextSize = 40.0f,
                IsAntialias = true,
                Color = new SKColor(128, 128, 128, 255),
                TextAlign = SKTextAlign.Center
            };
            var originalPricePaint = new SKPaint
            {
                Typeface = SKTypeface.Default,
                TextSize = 30.0f,
                IsAntialias = true,
                Color = new SKColor(128, 128, 128, 255),
                TextAlign = SKTextAlign.Right,
            };
            var linePaint = new SKPaint
            {
                Color = new SKColor(128, 128, 128, 255),
                StrokeWidth = 3,
            };

            canvas.DrawText(Receipt.Total.ToString("#,##.00"), canvasWidth/2, 100, totalPricePaint);
            canvas.DrawText("Total", canvasWidth / 2, 200, totalPaint);

            canvas.DrawText("Order: "+"App.StoreName", 50, 300, defaultText);
            canvas.DrawText("Cashier: "+ "App.User", 50, 350, defaultText);
            canvas.DrawText("POS: "+"App.Pos", 50, 400, defaultText);

            canvas.DrawLine(new SKPoint() { X = 50, Y = 450 }, new SKPoint() { X = canvasWidth - 50, Y = 450 }, linePaint);

            int currentCursor = 500;
            foreach (var item in Receipt.ItemList)
            {
                currentCursor += 50;
                canvas.DrawText(item.ItemName, 50, currentCursor, defaultText);

                if (item.IsDiscounted)
                {
                    canvas.DrawText(item.DiscountedPrice.ToString("#,##.00"), canvasWidth - 50, currentCursor, discountedItemPrice);
                    canvas.DrawText(item.ItemPrice.ToString("#,##.00"), canvasWidth - 300, currentCursor, originalPricePaint);
                }
                else
                {
                    canvas.DrawText(item.ItemPrice.ToString("#,##.00"), canvasWidth - 50, currentCursor, discountedItemPrice);
                }

                currentCursor += 40;
                canvas.DrawText(" x " + item.Quantity, 50, currentCursor, smallText);
                currentCursor += 40;
                canvas.DrawText($"+ {item.OptionName} ({item.OptionPrice.ToString("#,##.00")})", 50, currentCursor, smallText);
                currentCursor += 40;
                if (item.IsDiscounted)
                {
                    if (item.IsDiscountPercentage)
                        canvas.DrawText($"Discount {item.ItemDiscount.ToString()}%", 50, currentCursor, smallText);
                    else
                        canvas.DrawText($"Discount {item.ItemDiscount.ToString("#,##.00")}", 50, currentCursor, smallText);
                    currentCursor += 40;
                }
            }
            if (Receipt.Discount > 0)
            {
                currentCursor += 60;
                canvas.DrawLine(new SKPoint() { X = 50, Y = currentCursor }, new SKPoint() { X = canvasWidth - 50, Y = currentCursor }, linePaint);
                currentCursor += 50;
                canvas.DrawText("Discount ", 50, currentCursor, defaultText);
                canvas.DrawText(Receipt.Discount.ToString("#,##.00"), canvasWidth - 50, currentCursor, discountedItemPrice);
            }
            currentCursor += 60;
            canvas.DrawLine(new SKPoint() { X = 50, Y = currentCursor }, new SKPoint() { X = canvasWidth - 50, Y = currentCursor }, linePaint);
            currentCursor += 50;
            canvas.DrawText("Total", 50, currentCursor, defaultText);
            canvas.DrawText("Price", canvasWidth - 50, currentCursor, discountedItemPrice);


            canvas.Flush();

            var image = SKImage.FromBitmap(toBitmap);
            var data = image.Encode(SKEncodedImageFormat.Png, 90);
            return data.ToArray();
        }

        private void Back_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new SaleView());
        }
    }
}