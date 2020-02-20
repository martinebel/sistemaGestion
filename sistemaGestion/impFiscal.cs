using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace sistemaGestion
{
    class impFiscal
    {
        Conexion dbCon = Conexion.Instance();
        FiscalPrinterLib.HASAR HASAR1 = new FiscalPrinterLib.HASAR();
        
        private bool esDisponible;
        private int portNumber;
        private string PdV;
        private string lastNumber;
        private string lastError;

        public impFiscal()
        {
            var cmd = new MySqlCommand("select pdvfiscal,puertofiscal,fiscalhabil from config", dbCon.Connection);
            var reader = cmd.ExecuteReader();
            reader.Read();
            PdV = reader.GetString(0);
            portNumber = reader.GetInt32(1);
            if(reader.GetString(2)=="1")
            { esDisponible = true; }
            else
            { esDisponible = false; }
            reader.Close();
            HASAR1.ErrorFiscal += HASAR1_ErrorFiscal1;
            HASAR1.ErrorImpresora += HASAR1_ErrorImpresora;
            Progress += new Action<int>(MyEventHandler);
        }

        public event Action<int> Progress;

        private void MyEventHandler(int value)
        {
            
        }

        private void HASAR1_ErrorImpresora(int Flags)
        {
            HASAR1.TratarDeCancelarTodo();
            lastError = HASAR1.DescripcionStatusImpresor(Flags);
        }

        private void HASAR1_ErrorFiscal1(int Flags)
        {
            HASAR1.TratarDeCancelarTodo();
            lastError= HASAR1.DescripcionStatusFiscal(Flags);
        }

        

        public bool Initialize()
        {
            if (!isAvailable()) { lastError = "Impresora Fiscal No Habilitada";return false; }
            try
            {
                Progress(10);
                HASAR1.Transporte = FiscalPrinterLib.TiposDeTransporte.PUERTO_SERIE;
                Progress(20);
                HASAR1.Modelo = FiscalPrinterLib.ModelosDeImpresoras.MODELO_P320;
                Progress(30);
                HASAR1.Puerto = 32;
                Progress(40);
                HASAR1.Baudios = 9600;
                Progress(50);
                HASAR1.Comenzar();
                //HASAR1.EspecificarNombreDeFantasia("", "");
                Progress(60);
                HASAR1.ConfigurarControlador(FiscalPrinterLib.ParametrosDeConfiguracion.COPIAS_DOCUMENTOS, "2");
                Progress(70);
                HASAR1.ConfigurarControlador(FiscalPrinterLib.ParametrosDeConfiguracion.IMPRESION_MARCO, "M");
                Progress(80);
                HASAR1.PrecioBase = false;
                Progress(90);
                HASAR1.TratarDeCancelarTodo();
                Progress(100);
                return true;
            }
            catch (Exception ex)
            {
                lastError = ex.Message;
                return false;
            }
        }

        
        private FiscalPrinterLib.TiposDeDocumento tipoDoc(string codigo)
        {
            
            switch(codigo)
            {
                case "80":return FiscalPrinterLib.TiposDeDocumento.TIPO_CUIT;break;
                case "86": return FiscalPrinterLib.TiposDeDocumento.TIPO_CUIL; break;
                case "87": return FiscalPrinterLib.TiposDeDocumento.TIPO_CI; break;
                case "89": return FiscalPrinterLib.TiposDeDocumento.TIPO_LE; break;
                case "90": return FiscalPrinterLib.TiposDeDocumento.TIPO_LC; break;

                case "94": return FiscalPrinterLib.TiposDeDocumento.TIPO_PASAPORTE; break;
                case "96": return FiscalPrinterLib.TiposDeDocumento.TIPO_DNI; break;
                case "99": return FiscalPrinterLib.TiposDeDocumento.TIPO_NINGUNO; break;
                default:
                    return FiscalPrinterLib.TiposDeDocumento.TIPO_NINGUNO; break;
            }
        }

        private FiscalPrinterLib.TiposDeResponsabilidades tipoIva(string codigo)
        {
            switch (codigo)
            {
                case "1": return FiscalPrinterLib.TiposDeResponsabilidades.RESPONSABLE_INSCRIPTO; break;
                case "2": return FiscalPrinterLib.TiposDeResponsabilidades.RESPONSABLE_NO_INSCRIPTO; break;
                case "3": return FiscalPrinterLib.TiposDeResponsabilidades.NO_RESPONSABLE; break;
                case "4": return FiscalPrinterLib.TiposDeResponsabilidades.RESPONSABLE_EXENTO; break;
                case "5": return FiscalPrinterLib.TiposDeResponsabilidades.CONSUMIDOR_FINAL; break;

                case "6": return FiscalPrinterLib.TiposDeResponsabilidades.MONOTRIBUTO; break;
                case "7": return FiscalPrinterLib.TiposDeResponsabilidades.NO_CATEGORIZADO; break;
                case "11": return FiscalPrinterLib.TiposDeResponsabilidades.RESPONSABLE_INSCRIPTO; break;
                case "14": return FiscalPrinterLib.TiposDeResponsabilidades.MONOTRIBUTO_SOCIAL; break;
                default:
                    return FiscalPrinterLib.TiposDeResponsabilidades.CONSUMIDOR_FINAL; break;
            }
        }

        public bool Print(string ticketNumber)
        {
            try {
                //obtener tipo de empresa
                //obtener el tipo de empresa
                Progress(10);
                string tipoempresa="1";
                var cmd = new MySqlCommand("select tipo from config", dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tipoempresa = reader.GetString(0);
                }
                reader.Close();
                //obtener datos de la venta
                cmd = new MySqlCommand("select ventas.*,clientes.clirazonsocial,clientes.clicuit,clientes.clitipodoc,clientes.clidireccion,clientes.clilocalidad,clientes.cliprovincia,clientes.clitipo from ventas inner join clientes on ventas.clicodigo=clientes.clicodigo where faccodigo="+ticketNumber, dbCon.Connection);
                reader = cmd.ExecuteReader();
                reader.Read();
                string tipofactura = reader.GetString(3);
                switch (reader.GetString(3))
                {
                    case "1": //FA

                        HASAR1.DatosCliente(reader.GetString(17), reader.GetString(18), FiscalPrinterLib.TiposDeDocumento.TIPO_CUIT, FiscalPrinterLib.TiposDeResponsabilidades.RESPONSABLE_INSCRIPTO, reader.GetString(20) + " " + reader.GetString(21) + " " + reader.GetString(22));
                        HASAR1.AbrirComprobanteFiscal(FiscalPrinterLib.DocumentosFiscales.FACTURA_A);
                        break;
                    case "3": //NCA
                        HASAR1.DatosCliente(reader.GetString(17), reader.GetString(18), FiscalPrinterLib.TiposDeDocumento.TIPO_CUIT, FiscalPrinterLib.TiposDeResponsabilidades.RESPONSABLE_INSCRIPTO, reader.GetString(20) + " " + reader.GetString(21) + " " + reader.GetString(22));
                        HASAR1.DocumentoDeReferencia[1] = "0000-00000000";
                        HASAR1.AbrirDNFH(FiscalPrinterLib.DocumentosNoFiscales.NOTA_CREDITO_A);
                        break;
                    case "6": //FB
                        HASAR1.DatosCliente(reader.GetString(17), reader.GetString(18), FiscalPrinterLib.TiposDeDocumento.TIPO_CUIT, FiscalPrinterLib.TiposDeResponsabilidades.RESPONSABLE_INSCRIPTO, reader.GetString(20) + " " + reader.GetString(21) + " " + reader.GetString(22));
                        HASAR1.AbrirComprobanteFiscal(FiscalPrinterLib.DocumentosFiscales.FACTURA_B);
                        break;
                    case "8": //NCB
                        HASAR1.DatosCliente(reader.GetString(17), reader.GetString(18), FiscalPrinterLib.TiposDeDocumento.TIPO_CUIT, FiscalPrinterLib.TiposDeResponsabilidades.RESPONSABLE_INSCRIPTO, reader.GetString(20) + " " + reader.GetString(21) + " " + reader.GetString(22));
                        HASAR1.DocumentoDeReferencia[1] = "0000-00000000";
                        HASAR1.AbrirDNFH(FiscalPrinterLib.DocumentosNoFiscales.NOTA_CREDITO_B);
                        break;
                    case "11": //FC
                        HASAR1.DatosCliente(reader.GetString(17), reader.GetString(18),tipoDoc(reader.GetString(19)), tipoIva(reader.GetString(23)), reader.GetString(20) + " " + reader.GetString(21) + " " + reader.GetString(22));
                        HASAR1.AbrirComprobanteFiscal(FiscalPrinterLib.DocumentosFiscales.FACTURA_B);
                        break;
                    case "13": //NCC
                        HASAR1.DatosCliente(reader.GetString(17), reader.GetString(18), tipoDoc(reader.GetString(19)), tipoIva(reader.GetString(23)), reader.GetString(20) + " " + reader.GetString(21) + " " + reader.GetString(22));
                        HASAR1.DocumentoDeReferencia[1] = "0000-00000000";
                        HASAR1.AbrirDNFH(FiscalPrinterLib.DocumentosNoFiscales.NOTA_CREDITO_B);
                        break;

                }
                Progress(30);
                double total = reader.GetDouble(11);
                double recargo = reader.GetDouble(10);
                double descuento = reader.GetDouble(9);
                reader.Close();
                Progress(40);
                cmd = new MySqlCommand("select detalleventas.*,productos.proiva as impuesto from detalleventas inner join productos on productos.procodigo=detalleventas.procodigo where faccodigo=" + ticketNumber, dbCon.Connection);
                reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    HASAR1.ImprimirItem(reader.GetString(2), reader.GetDouble(3), reader.GetDouble(4), reader.GetDouble(7), 0);
                }
                reader.Close();
                Progress(70);
                object _temp=0; object _un=2;
                if(recargo>0)
                {
                    HASAR1.DescuentoGeneral("Intereses Por Financiacion", recargo, false);
                }
                if (descuento > 0)
                {
                    HASAR1.DescuentoGeneral("Descuento General", descuento, true);
                }

                Progress(80);
                switch (tipofactura)
                {
                    case "1": //FA
                    case "6":
                    case "11":
                        HASAR1.ImprimirPago("EFECTIVO", total, null, out _temp);
                        HASAR1.CerrarComprobanteFiscal(null, out _temp);
                        break;
                    case "3": //NCA
                    case "8":
                    case "13":
                       HASAR1.CerrarDNFH(null, out _temp);
                        break;
                }
                Progress(100);
                lastNumber = _temp.ToString();
                return true;
            }
            catch (Exception ex)
            {
                HASAR1.TratarDeCancelarTodo();
                lastError = ex.Message;
                return false;
            }

        }

        public bool cierreZ()
        {
            object _var1;object _var2; object _var3; object _var4; object _var5; object _var6; object _var7; object _var8; object _var9;
            object _var10; object _var11; object _var12; object _var13; object _var14; object _var15; object _var16; object _var17; object _var18;
            object _var19; object _var20; object _var21; object _var22; object _var23; object _var24; object _var25;

            try
            {
                HASAR1.ReporteZ(out _var1,out _var2,out _var3,out _var4,out _var5,out _var6,out _var7,out _var8, out _var9, out _var10, out _var11, out _var12, out _var13, out _var14, out _var15, out _var16, out _var17, out _var18, out _var19, out _var20, out _var21, out _var22,out _var23, out _var24, out _var25);
                return true;
            }
            catch (Exception ex)
            {
                HASAR1.TratarDeCancelarTodo();
                lastError = ex.Message;
                return false;
            }
        }

        public int getStatus()
        {
            return 0;
        }

        public bool isAvailable()
        {
            return esDisponible;
        }

        public string getLastError()
        {
            Progress(100);
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
