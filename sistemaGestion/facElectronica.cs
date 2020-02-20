using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;

namespace sistemaGestion
{
    class facElectronica
    {
        Conexion dbCon = Conexion.Instance();
        private bool esDisponible;
        private int initialNumber;
        private string PdV;
        private string lastNumber;
        private string lastError;
        private string CUIT;
        private string tipo;
        private string path;

        public facElectronica(string appPath)
        {
            var cmd = new MySqlCommand("select pdvelectronico,elechabil,cuit,tipo from config", dbCon.Connection);
            var reader = cmd.ExecuteReader();
            reader.Read();
            PdV = reader.GetString(0);
            CUIT = reader.GetString(2);
            tipo = reader.GetString(3);
            if (reader.GetString(1) == "1")
            { esDisponible = true; }
            else
            { esDisponible = false; }
            reader.Close();
            if (!checkFiles()) { esDisponible = false; }
            path = appPath;
        }

        public bool Initialize()
        {
            if (!isAvailable()) { lastError = "Factura Electronica No Habilitada"; return false; }
            if (!checkFiles()) { return false; }
            lastError = "Impresora Fiscal No Contemplado";
            return false;
        }

        public bool Print(string ticketNumber)
        {
            //devolver numero de factura y PdV
            return false;
        }

        public bool isAvailable()
        {
            return esDisponible;
        }

        private bool checkFiles()
        {
            if (!File.Exists(path + "\\certificado.crt"))
            {
                lastError = "Certificado no encontrado";
                return false;
            }

            if (!File.Exists(path + "\\clave.key"))
            {
                lastError = "Clave Privada no Encontrada";
                return false;
            }
            return true;
        }

        public string getLastError()
        {
            return lastError;
        }

        public string getLastId()
        {
            return lastNumber;
        }

        public string getPdV()
        {
            return PdV;
        }

    }
}
