using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System;
using System.Windows.Forms;


namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class Example01 : Form
    {
        public Example01()
        {
            InitializeComponent();
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            QrEncoder Codificador = new QrEncoder(ErrorCorrectionLevel.H);

            // crear un codigo QR
            QrCode Codigo = new QrCode();

            // generar generar  un codigo apartir de datos, y pasar el codigo por referencia
            Codificador.TryEncode(txtValor.Text, out Codigo);

            // generar un graficador 
            GraphicsRenderer Renderisado = new GraphicsRenderer(new FixedCodeSize(200, QuietZoneModules.Zero), Brushes.Black, Brushes.White);

            // generar un flujo de datos 
            MemoryStream ms = new MemoryStream();

            // escribir datos en el renderizado
            Renderisado.WriteToStream(Codigo.Matrix, ImageFormat.Png, ms);

            // generar controles para ponerlos en el form
            var ImagenQR = new Bitmap(ms);
            var ImgenSalida = new Bitmap(ImagenQR, new Size(PanelResultado.Width, PanelResultado.Height));

            // asignar la imagen al panel 
            PanelResultado.BackgroundImage = ImgenSalida;
        }
    }
}
