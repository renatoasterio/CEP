using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EDNE
{
    class Program
    {
        static void Main(string[] args)
        {
            var lista = new Dictionary<int, int>()
            {
                { 15135000, 15140000 },
                { 89890000, 97418000 },
                { 97420000, 99990000 },
                { 15140000, 18580000 },
                { 18590000, 35110000 },
                { 35112000, 36515000 },
                { 36520000, 38294000 },
                { 38295000, 44150000 },
                { 44160000, 46875000 },
                { 46880000, 55588000 },
                { 55590000, 58339000 },
                { 58340000, 59760000 },
                { 59770000, 64335000 },
                { 64340000, 65609999 },
                { 65610000, 69260000 },
                { 69265000, 76352000 },
                { 76355000, 78630000 },
                { 78635000, 85267000 },
                { 85270000, 87830000 },
                { 87840000, 89888000 }
            };

            foreach (var item in lista)
                new Thread(new ThreadStart(() => { Busca(item.Key, item.Value); })).Start();

            //CorreiosService.consultaCEP c = new CorreiosService.consultaCEP("08260030");
            //var end = new CorreiosService.AtendeClienteClient().consultaCEP("08260030");
        }

        static void Busca(int inicio, int fim)
        {
            try
            {
                for (int i = inicio; i < fim; i++)
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
            }
            catch (Exception)
            {
                File.AppendAllText(@"C:\Users\Renato Asterio\source\repos\CEP\EDNE\cep.txt", Environment.NewLine + $"{inicio}, {fim}");
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
                File.AppendAllText(@"C:\Users\Renato Asterio\source\repos\CEP\EDNE\log.txt", Environment.NewLine + Environment.NewLine + e.Message + Environment.NewLine + JsonConvert.SerializeObject(end));
            }

        }
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
