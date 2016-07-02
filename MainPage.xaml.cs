using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
//using System.Windows.Media;
using Windows.UI;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Media.Imaging;
using System.Threading.Tasks;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PaletteGenerator {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void noBase(object sender, RoutedEventArgs e) {
            var random = new Random();
            var basecolor = random.Next(0x100000);
            if (baseColor.IsChecked.Equals(true)) {
                useBase();
            } else {
                if (invalidText.Opacity.Equals(1)) {
                    invalidStoryboardOut.Begin();
                }
                randomizePalette(basecolor);
            }
        }

        private void useBase() {
            string color1;
            if (colorText1.Text.Length > 7) {
                color1 = colorText1.Text.Substring(3, 6);
            } else {
                color1 = colorText1.Text.Substring(1, 6);
            }
            try {
                var basecolor = Int32.Parse(color1, System.Globalization.NumberStyles.HexNumber);
                if (invalidText.Opacity.Equals(1)) {
                    invalidStoryboardOut.Begin();
                }
                randomizePalette(basecolor);
            } catch {
                invalidStoryboardIn.Begin();
            }
        }

        private void randomizePalette(int basecolor) {
            var random = new Random();
            var color1 = String.Format("#{0:X6}", basecolor);

            string color2 = null, color3 = null, color4 = null, color5 = null, color6 = null;

            if (randomMode.IsSelected.Equals(true)) {
                var mode = random.Next(6);
                if (mode == 1) {
                    crazy(basecolor, ref color2, ref color3, ref color4, ref color5, ref color6);
                } else if (mode == 2) {
                    smooth(basecolor, ref color2, ref color3, ref color4, ref color5, ref color6);
                } else if (mode == 3 || mode == 4) {
                    varied(basecolor, ref color2, ref color3, ref color4, ref color5, ref color6);
                } else {
                    accent(basecolor, ref color2, ref color3, ref color4, ref color5, ref color6);
                }
            }else if (smoothMode.IsSelected.Equals(true)) {
                smooth(basecolor, ref color2, ref color3, ref color4, ref color5, ref color6);
            } else if (accentMode.IsSelected.Equals(true)) {
                accent(basecolor, ref color2, ref color3, ref color4, ref color5, ref color6);
            } else if (variedMode.IsSelected.Equals(true)) {
                varied(basecolor, ref color2, ref color3, ref color4, ref color5, ref color6);
            } else {
                crazy(basecolor, ref color2, ref color3, ref color4, ref color5, ref color6);
            }

            colorText1.Text = color1;
            colorText2.Text = color2;
            colorText3.Text = color3;
            colorText4.Text = color4;
            colorText5.Text = color5;
            colorText6.Text = color6;

            colorText1.BorderBrush = StringToBrushNoA(color1);
            colorText2.BorderBrush = StringToBrushNoA(color2);
            colorText3.BorderBrush = StringToBrushNoA(color3);
            colorText4.BorderBrush = StringToBrushNoA(color4);
            colorText5.BorderBrush = StringToBrushNoA(color5);
            colorText6.BorderBrush = StringToBrushNoA(color6);

            colorRect1.Fill = StringToBrushNoA(color1);
            colorRect2.Fill = StringToBrushNoA(color2);
            colorRect3.Fill = StringToBrushNoA(color3);
            colorRect4.Fill = StringToBrushNoA(color4);
            colorRect5.Fill = StringToBrushNoA(color5);
            colorRect6.Fill = StringToBrushNoA(color6);
        }

        private void accent(int basecolor, ref string color2, ref string color3, ref string color4, ref string color5, ref string color6) {

            var hex2 = basecolor + 0x202020;
            color2 = String.Format("#{0:X6}", hex2);
            var hex3 = hex2 + 0x202020;
            color3 = String.Format("#{0:X6}", hex3);
            var hex4 = hex3 + 0x202020;
            color4 = String.Format("#{0:X6}", hex4);
            var hex5 = hex4 + 0x202020;
            color5 = String.Format("#{0:X6}", hex5);
            var hex6 = 0xFFFFFF ^ basecolor;
            color6 = String.Format("#{0:X6}", hex6);
        }

        private void varied(int basecolor, ref string color2, ref string color3, ref string color4, ref string color5, ref string color6) {

            var hex2 = basecolor + 0x202020;
            color2 = String.Format("#{0:X6}", hex2);
            var hex3 = hex2 + 0x202020;
            color3 = String.Format("#{0:X6}", hex3);
            var hex4 = 0xFFFFFF ^ basecolor;
            color4 = String.Format("#{0:X6}", hex4);
            var hex5 = hex4 + 0x202020;
            color5 = String.Format("#{0:X6}", hex5);
            var hex6 = hex5 + 0xF0F0F0;
            color6 = String.Format("#{0:X6}", hex6);
        }

        private void crazy(int basecolor, ref string color2, ref string color3, ref string color4, ref string color5, ref string color6) {

            var hex2 = basecolor + 0x202020;
            color2 = String.Format("#{0:X6}", hex2);
            var hex3 = 0x808080 ^ basecolor;
            color3 = String.Format("#{0:X6}", hex3);
            var hex4 = hex3 + 0x202020;
            color4 = String.Format("#{0:X6}", hex4);
            var hex5 = 0xF0F0F0 ^ basecolor;
            color5 = String.Format("#{0:X6}", hex5);
            var hex6 = hex5 + 0x202020;
            color6 = String.Format("#{0:X6}", hex6);
        }

        private void smooth(int basecolor, ref string color2, ref string color3, ref string color4, ref string color5, ref string color6) {

            var hex2 = basecolor + 0x101010;
            color2 = String.Format("#{0:X6}", hex2);
            var hex3 = hex2 + 0x101010;
            color3 = String.Format("#{0:X6}", hex3);
            var hex4 = hex3 + 0x101010;
            color4 = String.Format("#{0:X6}", hex4);
            var hex5 = hex4 + 0x101010;
            color5 = String.Format("#{0:X6}", hex5);
            var hex6 = hex5 + 0x101010;
            color6 = String.Format("#{0:X6}", hex6);
        }

        private void toClipboard(object sender, PointerRoutedEventArgs e) {
            var data = new DataPackage();
            var colorString = ((SolidColorBrush)((Windows.UI.Xaml.Shapes.Rectangle)sender).Fill).Color.ToString();
            data.SetText(colorString);
            Clipboard.SetContent(data);
            copiedText.Foreground = StringToBrush(colorString);
            copiedStoryboard.Begin();
        }

        public static SolidColorBrush StringToBrush(String color) {
            return new SolidColorBrush(Color.FromArgb(Convert.ToByte(color.Substring(1, 2), 16), Convert.ToByte(color.Substring(3, 2), 16), Convert.ToByte(color.Substring(5, 2), 16), Convert.ToByte(color.Substring(7, 2), 16)));
        }

        public static SolidColorBrush StringToBrushNoA(String color) {
            byte a = 0xFF;
            return new SolidColorBrush(Color.FromArgb(a, Convert.ToByte(color.Substring(1, 2), 16), Convert.ToByte(color.Substring(3, 2), 16), Convert.ToByte(color.Substring(5, 2), 16)));
        }

        private void savePalette(object sender, RoutedEventArgs e) {
            WriteableBitmap writeableBmp = BitmapFactory.New(512, 512);
            writeableBmp.Clear(Colors.Red);
            writeableBmp.DrawRectangle(2, 4, 12, 10, Colors.Red);

            SaveImage(writeableBmp.PixelBuffer);
            
        }

        static async void SaveImage(Windows.Storage.Streams.IBuffer buffer) {
            var savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            savePicker.FileTypeChoices.Add("Bitmap", new List<string>() { ".bmp" });
            savePicker.SuggestedFileName = "Palette";

            //var byteArray = StreamToByte(stream);

            Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();

            if (file != null) {
                Windows.Storage.CachedFileManager.DeferUpdates(file);
                await Windows.Storage.FileIO.WriteBufferAsync(file,buffer);
                Windows.Storage.Provider.FileUpdateStatus status =
                    await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);
                if (status == Windows.Storage.Provider.FileUpdateStatus.Complete) {
                    var error = new Windows.UI.Popups.MessageDialog("Palette " + file.Name + " saved.");
                    await error.ShowAsync();
                } else {
                    var error = new Windows.UI.Popups.MessageDialog("Palette " + file.Name + " couldn't be saved.");
                    await error.ShowAsync();

                }
            }

            //FileStream file = File.OpenWrite("C:/Users/silvi/Pictures/palette.bmp");
            //stream.CopyTo(file);
        }

        //From stackoverflow
        public static byte[] StreamToByte(Stream input) {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream()) {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0) {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        private void sharePalette(object sender, RoutedEventArgs e) {
            DataTransferManager.ShowShareUI();
        }
    }

}
