using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace LabRepaso
{
    public partial class Form1 : Form
    {
        List<Empleado> empleados = new List<Empleado>();
        List<Asistencia> asistencias = new List<Asistencia>();
        List<Reporte> reportes = new List<Reporte>();
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void MostrarEmpleado()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = empleados;
            dataGridView1.Refresh();

        }
        public void CargarEmpleados()
        {
            string fileName = "historial.txt";

            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);


            while (reader.Peek() > -1)
            //Esta linea envía el texto leído a un control richTextBox, se puede cambiar para que
            //lo muestre en otro control por ejemplo un combobox
            {
                Empleado empleado = new Empleado();
                empleado.NoEmpleado = Convert.ToInt32(reader.ReadLine());
                empleado.Nombre = reader.ReadLine();
                empleado.Sueldo = Convert.ToDecimal(reader.ReadLine());

                empleados.Add(empleado);

            }

        }
        private void CargarAsistencia()
        {
            string fileName = "historial2.txt";

            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);


            while (reader.Peek() > -1)
            //Esta linea envía el texto leído a un control richTextBox, se puede cambiar para que
            //lo muestre en otro control por ejemplo un combobox
            {
                Asistencia asistencia = new Asistencia();
                asistencia.NoEmpleado = Convert.ToInt32(reader.ReadLine());
                asistencia.HorasMes= Convert.ToInt32(reader.ReadLine());
                asistencia.Mes = Convert.ToInt32(reader.ReadLine());

                asistencias.Add(asistencia);

            }
        }
        private void MostrarAsistencia()
        {
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = asistencias;
            dataGridView2.Refresh();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            MostrarEmpleado();
            //CargarEmpleados();
            CargarAsistencia();
            MostrarAsistencia();
            
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Empleado empleado in empleados)
            {
                int noEmpleados = empleado.NoEmpleado;
                foreach (Asistencia asistencia in asistencias)
                {
                    if (empleado.NoEmpleado == asistencia.NoEmpleado)
                    {
                        Reporte reporte = new Reporte();
                        reporte.NombreEmpleado = empleado.Nombre;
                        reporte.Mes = asistencia.Mes;
                        reporte.SueldoMensual = empleado.Sueldo * asistencia.HorasMes;

                        reportes.Add(reporte);


                    }
                }
            }
            dataGridView3.DataSource = null;
            dataGridView3.DataSource = reportes;
            dataGridView3.Refresh();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarEmpleados();
            comboBox1.DisplayMember = "Nombre";
            comboBox1.DataSource = empleados;
            CargarAsistencia();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int noEmpleados = Convert.ToInt32(comboBox1.SelectedValue);


            Empleado empleadoEncontrado = empleados.Find(c => c.NoEmpleado == noEmpleados);
            Asistencia asistenciaEncontrada = asistencias.Find(u => u.NoEmpleado == noEmpleados);

            decimal sueldo = empleadoEncontrado.Sueldo = asistenciaEncontrada.HorasMes;
            label1.Text = empleadoEncontrado.Nombre;
            label1.Text = sueldo.ToString();
        }
    }
}
