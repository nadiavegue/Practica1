using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pr1
{
    public partial class RelojDigital : Form
    {
        private TimeSpan m_DesfaseHorario = new TimeSpan(0);
        private RelojAnalogico m_RelojAnalogico = new RelojAnalogico();
        public RelojDigital()
        {
            InitializeComponent();
            MostrarHoraActual();
            m_RelojAnalogico.Show(this);
        }
       
        private void MostrarHoraActual()
        {
            DateTime hora = DateTime.Now + m_DesfaseHorario;
            ct_HoraActual.Text = hora.ToLongTimeString();
            m_RelojAnalogico.Hora = hora;
        }

        private void RelojDigital_Load(object sender, EventArgs e)
        {

        }

        private void salir(object sender, EventArgs e)
        {
            Close();
        }

        private void AcercaDe(object sender, EventArgs e)
        {
            AcercaDe dlg = new AcercaDe();
            dlg.ShowDialog();
        }

        private void Actualizar(object sender, EventArgs e)
        {
            MostrarHoraActual();
        }

        private void RelojDigital_Shown(object sender, EventArgs e)
        {
            m_RelojAnalogico.Location = new Point(
            this.Location.X + 50 + 10, this.Location.Y);
        }

        private void Temporizador(object sender, EventArgs e)
        {
            MostrarHoraActual();
        }

        private void Mostrar_click(object sender, EventArgs e)
        {
            if (m_RelojAnalogico.Visible)
            {
                m_RelojAnalogico.Hide();
                bt_Mostrar.Text = "Mostrar analógico";
            }

            else
            {
                m_RelojAnalogico.Show();
                bt_Mostrar.Text = "Ocultar analógico";
            }
        }

        internal void CambiarHora(int horas, int minutos, int second)
        {
            DateTime actual = DateTime.Now;
            DateTime hora = new DateTime(actual.Year, actual.Month, actual.Day, horas, minutos, second);

            m_DesfaseHorario = hora - actual;

            MostrarHoraActual();
        }
    }
}
