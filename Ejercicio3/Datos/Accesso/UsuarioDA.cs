using Datos.Entidad;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Accesso
{
    public class UsuarioDA
    {
        readonly string Cadena = "Server=localhost; Port=3306; Database=Ejercicio3; Uid=root; Pwd=123456789;";

        MySqlConnection conexion;
        MySqlCommand comando;

        public Usuario Login(string Codigo, string Clave)
        {
            Usuario user = null;

            try
            {
                //Consulta a MySQL
                string sql = "SELECT * FROM usuario WHERE Codigo = @Codigo AND Clave=@Clave";

               
                conexion = new MySqlConnection(Cadena);
                conexion.Open(); 

                comando = new MySqlCommand(sql, conexion);  
                comando.Parameters.AddWithValue("@Codigo", Codigo);
                comando.Parameters.AddWithValue("@Clave", Clave);

                MySqlDataReader reader = comando.ExecuteReader(); 

                while (reader.Read())
                {
                    user = new Usuario();
                    user.Codigo = reader[0].ToString();
                    user.Nombre = reader[1].ToString();
                    user.Apellido = reader[2].ToString();
                    user.Edad = Convert.ToInt32(reader[3].ToString());
                    user.Clave = reader[4].ToString();
                    
                }
                reader.Close();
                conexion.Close();

            }
            catch (Exception ex) 
            {


            }

            return user;
        }

        public DataTable ListarUsuarios()
        {
            DataTable ListaUsuariosDT = new DataTable();

            try
            {
                string sql = "SELECT * FROM usuario;"; 

                conexion = new MySqlConnection(Cadena);
                conexion.Open();
                comando = new MySqlCommand(sql, conexion);

                MySqlDataReader reader = comando.ExecuteReader(); 
                ListaUsuariosDT.Load(reader);
            }
            catch (Exception ex)
            {


            }
            return ListaUsuariosDT;
        }

    }
}
