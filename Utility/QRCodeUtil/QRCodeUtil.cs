using System.Drawing;
using QRCoder;

namespace CarSee.Utility.QRCodeUtil
{
    public class QRCodeUtil
    {
        public static Bitmap CreateQRCode(string text)
        {
            QRCodeGenerator QrGenerator = new QRCodeGenerator();
            QRCodeData QrCodeInfo = QrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            QRCode QrCode = new QRCode(QrCodeInfo);
            Bitmap QrBitmap = QrCode.GetGraphic(60);  

            return QrBitmap;
        }
    }
}