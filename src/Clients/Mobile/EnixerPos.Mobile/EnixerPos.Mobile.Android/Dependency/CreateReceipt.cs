using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EnixerPos.Mobile.Dependency;
using EnixerPos.Mobile.Droid.Dependency;
using Java.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(CreateReceipt))]
namespace EnixerPos.Mobile.Droid.Dependency
{
    public class CreateReceipt : ICreateReceipt
    {
        public async Task<bool> SavePhotoAsync(byte[] data, string folder, string filename)
        {
            try
            {
                Java.IO.File picturesDirectory = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures);
                Java.IO.File folderDirectory = picturesDirectory;

                if (!string.IsNullOrEmpty(folder))
                {
                    folderDirectory = new Java.IO.File(picturesDirectory, folder);
                    folderDirectory.Mkdirs();
                }

                using (Java.IO.File bitmapFile = new Java.IO.File(folderDirectory, filename))
                {
                    bitmapFile.CreateNewFile();

                    using (FileOutputStream outputStream = new FileOutputStream(bitmapFile))
                    {
                        await outputStream.WriteAsync(data);
                    }

                    // Make sure it shows up in the Photos gallery promptly.
                    MediaScannerConnection.ScanFile(Android.App.Application.Context,
                                                    new string[] { bitmapFile.Path },
                                                    new string[] { "image/png", "image/jpeg" }, null);
                }
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}