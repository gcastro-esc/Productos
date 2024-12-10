using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Productos
{
    public partial class frmVentas : Form
    {
        double subTotal = 0, Pago = 0;
        public frmVentas()
        {
            InitializeComponent();
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
        private void cbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indice = cbCategoria.SelectedIndex;
            switch (indice)
            {
                case 0:
                    picProducto.Image = Productos.Properties.Resources.fresco;
                    break;
                case 1:
                    picProducto.Image = Productos.Properties.Resources.Enlatados;
                    break;
                case 2:
                    picProducto.Image = Productos.Properties.Resources.Higiene;
                    break;
                case 3:
                    picProducto.Image = Productos.Properties.Resources.Ropa;
                    break;
                case 4:
                    picProducto.Image = Productos.Properties.Resources.Oficina;
                    break;
            }
        }

        private void frmVentas_Load(object sender, EventArgs e)
        {
            lvCompras.View = View.Details;
            lvCompras.Columns.Add("Descripción", 150, HorizontalAlignment.Center);
            lvCompras.Columns.Add("Categoría", 150, HorizontalAlignment.Center);
            lvCompras.Columns.Add("Precio", 100, HorizontalAlignment.Center);
            lvCompras.Columns.Add("Cantidad", 100, HorizontalAlignment.Center);
            lvCompras.Columns.Add("A pagar", 100, HorizontalAlignment.Center);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrWhiteSpace(txtDescripción.Text) || cbCategoria.SelectedIndex < 0 || string.IsNullOrWhiteSpace(txtPrecio.Text)))
            {
                MessageBox.Show("No se han ingresado todos los detalles del producto, intenta nuevamente", "Datos incompletos");
            }
            else
            {
                string producto, categoria;
                double precio;
                int cantidad;
                producto = txtDescripción.Text;
                categoria = cbCategoria.SelectedItem.ToString();
                precio = Convert.ToDouble(txtPrecio.Text);
                cantidad = Convert.ToInt32(numCantidad.Value);

                Pago = precio * cantidad;

                ListViewItem elemento = new ListViewItem(producto);
                elemento.SubItems.Add(categoria);
                elemento.SubItems.Add(precio.ToString());
                elemento.SubItems.Add(cantidad.ToString());
                elemento.SubItems.Add(Pago.ToString());
                lvCompras.Items.Add(elemento);

                subTotal = subTotal + Pago;
                lblSubTotal.Text = "SubTotal - $ " + Math.Round(subTotal, 2);

                txtDescripción.Clear();
                cbCategoria.SelectedIndex = -1;
                txtPrecio.Clear();
                numCantidad.Value = 1;
                picProducto.Image = null;
                txtDescripción.Focus();
            }     
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (lvCompras.SelectedItems.Count > 0)
            {
                ListViewItem elemento = lvCompras.SelectedItems[0];
                Pago = Convert.ToDouble(elemento.SubItems[4].Text);
                lvCompras.Items.Remove(elemento);
                subTotal = subTotal - Pago;
                lblSubTotal.Text = "SubTotal - $ " + Math.Round(subTotal, 2);
                MessageBox.Show("Producto eliminado correctamente");
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un producto para eliminar", "Sin selección");
            }
        }
    }
}
