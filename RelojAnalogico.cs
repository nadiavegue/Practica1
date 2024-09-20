using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Pr1
{
    public partial class RelojAnalogico : Form
    {   
        private Point m_Centro = new Point();
        private int m_Radio;
        private DateTime m_Hora;

        public RelojAnalogico()
        {
            InitializeComponent();
            ActualizarDimensiones();
        }

        private void ActualizarDimensiones()
        {
            m_Centro.X = this.ClientSize.Width / 2;
            m_Centro.Y = this.ClientSize.Height / 2;
            m_Radio = Math.Min(m_Centro.X, m_Centro.Y);
        }
        public DateTime Hora
        {
            set
            {
                m_Hora = value;
                Invalidate();
            }
        }

        private void RelojAnalógico_FormClosing(object sender,FormClosingEventArgs e)
        {
            if (this.Focused)
                e.Cancel = true;
        }

        private void Cambio_tamaño(object sender, EventArgs e)
        {
            ActualizarDimensiones();
            Invalidate();
        }

        private void Manecillas(object sender, PaintEventArgs e)
        {
            if (m_Radio <= 10)
                return;

            Graphics gfx = e.Graphics;                                                                      // Representa la superficie de dibujo

            Pen lapizNormal = new Pen(Color.Black, 1);                      // Marcas
            Pen lapizGordoNegro = new Pen(Color.Black, 2);                  // Marcas
            Pen lapizMuyGordoAzul = new Pen(Color.Blue, 4);                 // Horario
            Pen lapizGordoVerde = new Pen(Color.Green, 2);                  // Minutero
            Pen lapizGordoRojo = new Pen(Color.Red, 2);                     // Secundero
            HatchBrush brochaGris = new HatchBrush(HatchStyle.Sphere, Color.Gray, Color.Gray);
            float alfa, x, y;

            // Llevamos el origen de coordenadas al centro de la ventana
            // y hacemos que el eje "y" aumente hacia arriba
            Matrix matriz = new Matrix(1, 0, 0, -1, m_Centro.X, m_Centro.Y);

            gfx.Transform = matriz;

            // Esfera
            float radioEsfera = m_Radio * .95f;
            gfx.DrawEllipse(lapizNormal, -radioEsfera, -radioEsfera, radioEsfera * 2, radioEsfera * 2);

            // Marcas de los minutos
            for (int i = 0; i < 60; i++)
            {
                alfa = (float)(i * Math.PI * 2 / 60);
                x = (float)(Math.Sin(alfa) * m_Radio);
                y = (float)(Math.Cos(alfa) * m_Radio);

                if (i % 5 == 0)
                    gfx.DrawLine(lapizGordoNegro, x * .85f, y * .85f, x * .95f, y * .95f);
                else
                    gfx.DrawLine(lapizNormal, x * .9f, y * .9f, x * .95f, y * .95f);
            }


            // Manecilla de las horas
            alfa = (float)((m_Hora.Hour % 12) * Math.PI * 2 / 12);
            alfa += (float)((m_Hora.Minute) * Math.PI * 2 / 12 / 60);
            x = (float)(Math.Sin(alfa) * m_Radio);
            y = (float)(Math.Cos(alfa) * m_Radio);
            gfx.DrawLine(lapizMuyGordoAzul, 0, 0, x * .5f, y * .5f);

            // Manecilla de los minutos con lápiz verde
            alfa = (float)((m_Hora.Minute % 60) * Math.PI * 2 / 60);
            alfa += (float)((m_Hora.Second) * Math.PI * 2 / 60 / 60);
            x = (float)(Math.Sin(alfa) * m_Radio);
            y = (float)(Math.Cos(alfa) * m_Radio);
            gfx.DrawLine(lapizGordoVerde, 0, 0, x * .7f, y * .7f);

            // Manecilla de los segundos con lapiz rojo
            alfa = (float)((m_Hora.Second % 60) * Math.PI * 2 / 60);
            x = (float)(Math.Sin(alfa) * m_Radio);
            y = (float)(Math.Cos(alfa) * m_Radio);
            gfx.DrawLine(lapizGordoRojo, 0, 0, x * .85f, y * .85f);

            // Botón Central

            float radioBoton = m_Radio * .1f / 2;

            gfx.DrawEllipse(lapizGordoNegro, -radioBoton, -radioBoton, radioBoton * 2, radioBoton * 2);
            gfx.FillEllipse(brochaGris, -radioBoton, -radioBoton, radioBoton * 2, radioBoton * 2);


        }
        private void RelojAnalogico_MouseDown(object sender, MouseEventArgs e)
        {
            ArrastrarAgujas(sender, e);
        }

        private void RelojAnalogico_MouseMove(object sender, MouseEventArgs e)
        {
            ArrastrarAgujas(sender, e);
        }

        private void ArrastrarAgujas(object sender, MouseEventArgs e)
        {
            bool botonDerIzq = e.Button == MouseButtons.Right || e.Button == MouseButtons.Left;
            bool pulsadoCentro = (e.X == m_Centro.X && e.Y == m_Centro.Y);

            if (m_Radio < 10 || !botonDerIzq || pulsadoCentro)
                return;

            double alfa;
            int horas, minutos;

            alfa = Math.Atan2(m_Centro.X - e.X, e.Y - m_Centro.Y);
            minutos = (int)((alfa / Math.PI / 2 * 12 + 6) * 60);
            horas = (minutos / 60) % 12;
            minutos %= 60;

            if (e.Button == MouseButtons.Right)
                horas += 12;

            Console.WriteLine(horas + ":" + minutos + ":" + m_Hora.Second);

            // Con estas 2 lineas, pasamos la hora del reloj analogico al digital, para que muestre la hora acorde
            RelojDigital relojD = (RelojDigital)this.Owner;                             // RelojD es una referencia al objeto RelojDifital. This es una referencia al objeto RelojAnalogico
            relojD.CambiarHora(horas, minutos, m_Hora.Second);
        
        }

    }    
}
