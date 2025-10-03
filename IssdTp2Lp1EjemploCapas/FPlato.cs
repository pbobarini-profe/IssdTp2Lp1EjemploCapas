using Business;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IssdTp2Lp1EjemploCapas
{
    public partial class FPlato : Form
    {
        List<Plato> platos = new List<Plato>();
        Plato platoSeleccionado = new Plato();

        public FPlato()
        {
            InitializeComponent();
        }

        private void FPlato_Load(object sender, EventArgs e)
        {
            platos = BPlato.Listar();
            platoBindingSource.DataSource = platos;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string descripcion = tbDescripcion.Text;
            decimal precio;
            if(descripcion != "" && tbPrecio.Text != "")
            {
                precio = decimal.Parse(tbPrecio.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
                Plato plato = new Plato
                {
                    Descripcion = descripcion,
                    Precio = precio
                };
                Guid guid = BPlato.Crear(plato);
                platos.Add(plato);
                platoBindingSource.DataSource = null;
                platoBindingSource.DataSource = platos;
                MessageBox.Show("Registro Agregado");
            }
            else
            {
                MessageBox.Show("Ingrese los datos necesarios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            platoSeleccionado = (Plato)platoBindingSource.Current;
            if(platoSeleccionado != null)
            {
                tbDescripcion.Text = platoSeleccionado.Descripcion;
                tbPrecio.Text = platoSeleccionado.Precio.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string descripcion = tbDescripcion.Text;
            if (descripcion != "" && tbPrecio.Text != "")
            {
                decimal precio = decimal.Parse(tbPrecio.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
                platoSeleccionado.Descripcion = descripcion;
                platoSeleccionado.Precio = precio;
                BPlato.Actualizar(platoSeleccionado);
                Plato plato = platos.Find(p => p.Id == platoSeleccionado.Id);
                plato.Descripcion = platoSeleccionado.Descripcion;
                plato.Precio = platoSeleccionado.Precio;
                MessageBox.Show("Registro modificado");
                platoBindingSource.DataSource = null;
                platoBindingSource.DataSource = platos;
            }
            else
            {
                MessageBox.Show("Ingrese los datos necesarios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show(
                   "¿Estás seguro de que quieres eliminar este registro?",
                   "Confirmación",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Question
               );
                if (result == DialogResult.Yes)
                {
                    BPlato.Eliminar(platoSeleccionado.Id);
                    platos.RemoveAll(p => p.Id == platoSeleccionado.Id);
                    platoBindingSource.DataSource = null;
                    platoBindingSource.DataSource = platos;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
