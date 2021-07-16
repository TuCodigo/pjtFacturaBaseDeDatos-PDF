using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Windows.Forms;
using System.Data;

namespace ClassDato
{
  public  class clsConexiones
    {

        public static MySqlConnection cnx = new MySqlConnection("server=127.0.0.1; database=DB_Ventas; Uid=root; pwd=1234;");



        public static void EjecutaQuery( string Opcion)
        {
            

            
            MySqlCommand cmd = new MySqlCommand();

            
            switch (Opcion)
            {

                case "RP":
                    cmd = new MySqlCommand($"INSERT INTO `producto`(`NombreProducto`, `Precio`) VALUES ('{ClassBT.clsProducto.NombreProducto}',{ClassBT.clsProducto.Precio})", cnx);
                    break;

                case "RV":
                    cmd = new MySqlCommand($"INSERT INTO ventas ( Fecha, `Costo`) VALUES ('{ClassBT.clsVenta.Fecha}',{ClassBT.clsVenta.Costo})", cnx);
                    break;

                case "RDV":
                    cmd = new MySqlCommand($"INSERT INTO detallaventa ( idProductofk, Cantidad, Costo, idVentasfk) VALUES ({ClassBT.clsDetallesVenta.idProdcutofk},{ClassBT.clsDetallesVenta.Cantidad},{ClassBT.clsDetallesVenta.CostoDetalle},{ClassBT.clsDetallesVenta.IdVentafk})", cnx);
                    break;

            }
            
             try
            {
                cnx.Open();
                cmd.ExecuteNonQuery();
                cnx.Close();
                
            }
            catch (Exception e)
            {

                MessageBox.Show("ERROR: "+e);
            }
            finally { cnx.Close(); }

        }
        public static DataTable EjecutaQueryConsulta(string Dato,string Opcion)
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter();


            switch (Opcion)
            {

                case "C":
                    da = new MySqlDataAdapter("SELECT * FROM `producto` ", cnx);
                    break;


                case "IDV":
                    da = new MySqlDataAdapter("SELECT MAX( idVentas) FROM `ventas`", cnx);
                    break;

            
                case "CDV":
                    da = new MySqlDataAdapter("SELECT idVentas, Fecha, V.Costo, idProducto, NombreProducto, Precio, idProductofk, dv.Cantidad FROM ventas as V , producto as p,detallaventa as dv WHERE V.idVentas=dv.idVentasfk and idProducto=idProductofk", cnx);
                    break;

            }
            try
            {
                cnx.Open();
                da.Fill(dt);
                cnx.Close();

            }
            catch (Exception e)
            {

                MessageBox.Show("ERROR: " + e);
            }
            finally { cnx.Close(); }
            return dt;

        }

    }
}
