using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EDNE
{
    class Program
    {
        static bool close = false;
        private static ManualResetEvent mre = new ManualResetEvent(false);
        private static Tuple<int, int>[] Ultimos;

        static void Main(string[] args)
        {
            string textFile = File.ReadAllText(@"C:\github\CEP\EDNE\cep.json");
            Tuple<int, int>[] lista = JsonConvert.DeserializeObject<Tuple<int, int>[]>(textFile);
            Ultimos = new Tuple<int, int>[lista.Length];

            int c = 0;
            foreach (var item in lista)
            {
                Thread t = new Thread(() => Busca(item.Item1, item.Item2, c));
                t.Name = "Thread_" + c;
                t.Start();
                Thread.Sleep(100);
                c++;
            }

            mre.Set();
            SetConsoleCtrlHandler(new HandlerRoutine(ConsoleCtrlCheck), true);
            while (!close)
            {

            }
        }

        static void Busca(int inicio, int fim, int index)
        {
            for (int i = inicio; i < fim; i++)
            {
                Ultimos[index] = new Tuple<int, int>(i, fim);
                try
                {
                    using (HttpClient cli = new HttpClient())
                    {
                        var resp = cli.GetAsync("https://api.postmon.com.br/v1/cep/" + i.ToString().PadLeft(8, '0')).Result;
                        if (resp.IsSuccessStatusCode)
                        {
                            var r = resp.Content.ReadAsStringAsync().Result;
                            var end = JsonConvert.DeserializeObject<Endereco>(r);
                            if (end != null)
                            {
                                Insert(end);
                                Console.WriteLine(end.cep);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    File.AppendAllText(@"C:\github\CEP\EDNE\erro.txt", Environment.NewLine + $"{inicio}, {fim}");
                }
            }
        }

        static async Task Insert(Endereco end)
        {
            try
            {
                using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["correio"].ConnectionString))
                {
                    c.Execute(@"IF NOT EXISTS(SELECT 1 FROM dbo.TB_CEP WHERE cep = @cep) 
                    BEGIN
                    INSERT INTO dbo.TB_CEP
                    ( 
	                    logradouro ,
	                    bairro ,
	                    cep ,
	                    cidade ,
	                    cidade_ibge ,
	                    estado ,
	                    estado_ibge ,
	                    uf
                    )
                    VALUES
                    ( 
	                    @logradouro ,
	                    @bairro ,
	                    @cep ,
	                    @cidade ,
	                    @cidade_ibge ,
	                    @estado ,
	                    @estado_ibge ,
	                    @uf
                    ) 
                    END", new
                    {
                        logradouro = end.logradouro,
                        bairro = end.bairro,
                        cep = end.cep,
                        cidade = end.cidade,
                        cidade_ibge = end.cidade_info?.codigo_ibge,
                        estado = end.estado_info?.nome,
                        estado_ibge = end.estado_info?.codigo_ibge,
                        uf = end.estado
                    });
                }
            }
            catch (Exception e)
            {
                File.AppendAllText(@"C:\github\CEP\EDNE\log.txt", Environment.NewLine + Environment.NewLine + e.Message + Environment.NewLine + JsonConvert.SerializeObject(end));
            }
        }

        private static bool ConsoleCtrlCheck(CtrlTypes ctrlType)
        {
            File.WriteAllText(@"C:\github\CEP\EDNE\cep.json", JsonConvert.SerializeObject(Ultimos));
            Thread.Sleep(2000);
            return close = true;
        }


        #region unmanaged
        // Declare the SetConsoleCtrlHandler function
        // as external and receiving a delegate.
        [DllImport("Kernel32")]
        public static extern bool SetConsoleCtrlHandler(HandlerRoutine Handler, bool Add);

        // A delegate type to be used as the handler routine
        // for SetConsoleCtrlHandler.
        public delegate bool HandlerRoutine(CtrlTypes CtrlType);

        // An enumerated type for the control messages
        // sent to the handler routine.
        public enum CtrlTypes
        {

            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT,
            CTRL_CLOSE_EVENT,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT
        }
        #endregion
    }

    public class Endereco
    {
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string logradouro { get; set; }
        public Estado_Info estado_info { get; set; }
        public string cep { get; set; }
        public Cidade_Info cidade_info { get; set; }
        public string estado { get; set; }
    }

    public class Estado_Info
    {
        public string area_km2 { get; set; }
        public string codigo_ibge { get; set; }
        public string nome { get; set; }
    }

    public class Cidade_Info
    {
        public string area_km2 { get; set; }
        public string codigo_ibge { get; set; }
    }
}
