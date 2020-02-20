using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Management;


namespace sistemaGestion
{
    class Conexion
    {
        
        public string nombreBase;
        private static Conexion _instance = null;
        public static Conexion Instance()
        {
            if (_instance == null)
                _instance = new Conexion();
            return _instance;
        }

        private MySqlConnection connection = null;
        public string connstring;
        public MySqlConnection Connection
        {
            get { return connection; }
        }

        public void Close()
        {
            connection.Close();
        }

        public bool IsConnect()
        {
            bool result = true;
            if (Connection == null)
            {

                string servidor, db, usuario, pass;
                StreamReader read = new StreamReader(Application.StartupPath + "\\servidorSQL.ini");

                servidor = read.ReadLine();
                db = read.ReadLine();
                usuario = read.ReadLine();
                pass = read.ReadLine();
                read.Close();
                nombreBase = db;

                nombreBase = db;
                connstring = "Server=" + servidor + ";database=" + db + ";UID=" + usuario + ";password=" + pass;
                connection = new MySqlConnection(connstring);
                connection.Open();
                result = true;
            }

            return result;
        }

        /* constructor */
        /// <summary>
        /// Inicia una instancia de conexión a la base de datos.
        /// </summary>
        public Conexion()
        {
            string servidor = "", db = "", usuario = "", pass = "";
           
            try
            {

                StreamReader read = new StreamReader(Application.StartupPath + "\\servidorSQL.ini");

                servidor = read.ReadLine();
                db = read.ReadLine();
                usuario = read.ReadLine();
                pass = read.ReadLine();
               
                read.Close();

              
                nombreBase = db;
                connstring = "Server=" + servidor + ";database=" + db + ";UID=" + usuario + ";password=" + pass;
                connection = new MySqlConnection(connstring);
                connection.Open();



            }
            catch (FileNotFoundException fnt)
            {
                //MessageBox.Show(fnt.Message + "\nRevise la configuracion.", "Error de Archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //config cfg = new config();
                //cfg.ShowDialog();
            }
            catch (MySqlException fnt)
            {
                switch (fnt.Number)
                {

                    case 1049: // Unable to connect to any of the specified MySQL hosts (Check Server,Port)
                        string connstring = "Server=" + servidor + ";database=information_schema;UID=" + usuario + ";password=" + pass;
                        connection = new MySqlConnection(connstring);
                        connection.Open();
                        break;
                    default:
                        //MessageBox.Show(fnt.Message + "\nRevise la configuracion.", "Error de BD" + fnt.Number.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //config cfg = new config();
                        //cfg.ShowDialog();
                        break;
                }

            }
        }


        public string extraerCodigo(string cadena)
        {
            return cadena.Substring(0, cadena.IndexOf('-'));
        }

        public int nuevoid(string campo, string tabla)
        {
            int final = 0;
            var cmd = new MySqlCommand("select max(" + campo + ") as total from " + tabla, this.Connection);
            var reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                reader.Close();
                return 1;
            }
            else
            {
                while (reader.Read())
                {
                    final = reader.GetInt32(0);
                }

                reader.Close();
                final++;
                return final;
            }
            
        }

        public string getHDDSerial()
        {
            string numerito = "";
            try
            {
               
                    var cmd = new MySqlCommand("select number from config", this.Connection);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        numerito = reader.GetString(0).ToUpper();
                    }
                    reader.Close();
                    if (numerito.Trim() == "")
                    {
                        ManagementClass driveClass = new ManagementClass("Win32_DiskDrive");

                        ManagementObjectCollection drives = driveClass.GetInstances();

                        foreach (ManagementObject drv in drives)
                        {
                            numerito = drv["SerialNumber"].ToString() + drv["Signature"].ToString();
                            break;
                        }
                    }


                
                return numerito.ToUpper();
            }
            catch
            {
                return (Environment.UserName + Environment.OSVersion.VersionString + Environment.MachineName).ToUpper();
            }
        }

        public System.Data.DataSet CommandQuery(string SQLString)
        {
            return null;
        }
    }
}
