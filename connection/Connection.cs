using Pedidos.model;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Pedidos.connection
{
    public class Connection
    {
        // Creamos una variable de referencia a la cadena de conexión almacenada en la configuración del proyecto.

        //private string cadenaConexion = Server=tcp:dam2023-di.database.windows.net,1433;Initial Catalog=prueba1;Persist Security Info=False;User ID=usuario;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        private string connectionStr = "Server=tcp:sufis2dam.database.windows.net,1433;Initial Catalog=sufi-ui-s2dam;Persist Security Info=False;User ID=sufiadmin;Password=tomas000-;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        // Variables para recuperar información de la Base de datos
        //private OleDbConnection CN;
        //private OleDbCommand CMD;
        //private OleDbDataReader RDR;

        private SqlConnection CN;
        private SqlCommand CMD;
        private SqlDataReader RDR;



        public ObservableCollection<Pedido> GetPedidos()
        {
            // Instanciamos la variable CN pasandole a su constructor la variable "cadenaConexion".
            CN = new SqlConnection(connectionStr);
            // Instanciamos la variable CMD pasandole a su constructor la instrucción OleDb que debe ejecutar
            // así como  la variable CN que le indica en que base de datos debe ejecutar dicha instrucción.
            CMD = new SqlCommand("SELECT * FROM Pedidos", CN);
            // Tipo de comando.
            CMD.CommandType = CommandType.Text;

            // Creamos una colección de tipo Coche que "envuelve"
            // a los registros de la tabla que se van a recuperar.
            ObservableCollection<Pedido> PedidoList =
                new ObservableCollection<Pedido>();

            try // Intentamos...
            {
                CN.Open(); // Abrir la conexión.
                RDR = CMD.ExecuteReader(); // Ejecutar la instrucción OleDb SELECT.

                while (RDR.Read()) // recorrer todos los registros.
                {

                    // Crear un objecto que "envuelve" el registro actual.
                    Pedido currentPedido = new Pedido();
                    currentPedido.NPedido = RDR["NPedido"].ToString();
                    currentPedido.Cliente = RDR["Cliente"].ToString();
                    currentPedido.DNI = RDR["DNI"].ToString();

                    // Agregar el objeto a la coleccion.
                    PedidoList.Add(currentPedido);
                }
            }
            catch (Exception ex)
            {

                throw ex; // Lanzamos excepción.
            }
            finally
            {
                CN.Close(); // Cerramos la conexión.
            }

            return PedidoList; // Regresamos la lista.
        }

        // Método que inserta un nuevo libro en la tabla.
        public void SaveNewPedido(Pedido newPedido)
        {
            CN = new SqlConnection(connectionStr);
            CMD = new SqlCommand();
            CMD.Connection = CN;
            CMD.CommandType = CommandType.Text;


            CMD.CommandText = "INSERT INTO Pedidos (brand, model, enginetype, stock, price, year) VALUES (@p1,@p2,@p3,@p4,@p5,@p6);";


            //// establecemos los valores que tomarán los parámetros de la instrucción OleDb.
            CMD.Parameters.AddWithValue("@p1", newPedido.Brand);
            CMD.Parameters.AddWithValue("@p2", newPedido.Model);
            CMD.Parameters.AddWithValue("@p3", newPedido.EngineType.ToString());
            CMD.Parameters.AddWithValue("@p4", newPedido.Stock);
            CMD.Parameters.AddWithValue("@p5", newPedido.Price);
            CMD.Parameters.AddWithValue("@p6", newPedido.Year);


            // Insertamos registro sin utilizar parámetros
            //CMD.CommandText = "INSERT INTO Libros VALUES ('" + nuevoLibro.Titulo + "', '" + nuevoLibro.Isbn + "', '" + nuevoLibro.Autor + "', " +
            //        "'" + nuevoLibro.Editorial + "')";


            CN.Open();
            CMD.ExecuteNonQuery();

            MessageBox.Show("Record added successfully");


            CN.Close();



        }

        public int DeletePedido(string brand, string model)
        {
            CN = new SqlConnection(connectionStr);
            CMD = new SqlCommand("DELETE FROM Pedidos WHERE brand = @p0 AND model = @p1", CN);
            CMD.CommandType = CommandType.Text;
            CMD.Parameters.AddWithValue("@p0", brand);
            CMD.Parameters.AddWithValue("@p1", model);

            // Eliminamos registro sin parámetros
            //CMD = new OleDbCommand("DELETE FROM Libros WHERE ISBN = '" + isbn + "'", CN);


            try
            {
                CN.Open();
                return CMD.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CN.Close();
            }
        }

        public int UpdateExistingPedido(string brand,
            string model,
            string engine,
            int stock,
            double price,
            int year)
        {
            CN = new SqlConnection(connectionStr);
            CMD = new SqlCommand("UPDATE Pedidos" +
                " SET brand = @p1, model = @p2, enginetype = @p3, stock = @p4, price = @p5, year = @p6" +
                " WHERE brand = @p1 AND model = @p2", CN);

            // set values for every parameter
            CMD.Parameters.AddWithValue("@p1", brand);
            CMD.Parameters.AddWithValue("@p2", model);
            CMD.Parameters.AddWithValue("@p3", engine);
            CMD.Parameters.AddWithValue("@p4", stock);
            CMD.Parameters.AddWithValue("@p5", price);
            CMD.Parameters.AddWithValue("@p6", year);

            CMD.CommandType = CommandType.Text;
            try
            {
                CN.Open();
                return CMD.ExecuteNonQuery();// Devuelve el número de filas afectadas en este caso debe ser 1.
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CN.Close();
            }
        }
    }
}
