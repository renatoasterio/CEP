using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
            new Thread(new ThreadStart(() => { SP(); })).Start();
            new Thread(new ThreadStart(PB)).Start();
            new Thread(new ThreadStart(RJ)).Start();
            new Thread(new ThreadStart(RS)).Start();
            new Thread(new ThreadStart(SC)).Start();
            new Thread(new ThreadStart(RN)).Start();
            new Thread(new ThreadStart(AC)).Start();
            new Thread(new ThreadStart(DF)).Start();
            new Thread(new ThreadStart(MT)).Start();

            //CorreiosService.consultaCEP c = new CorreiosService.consultaCEP("08260030");
            //var end = new CorreiosService.AtendeClienteClient().consultaCEP("08260030");
        }

        static void SP()
        {
            for (int i = 1135330; i < 19999999; i++)
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
        static void PB()
        {
            for (int i = 58138332; i < 58999999; i++)
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
        static void RJ()
        {
            for (int i = 20132629; i < 28999999; i++)
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
        static void RS()
        {

            for (int i = 9132710; i < 99999999; i++)
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
        static void SC()
        {

            for (int i = 88136119; i < 89999999; i++)
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
        static void RN()
        {

            for (int i = 59134857; i < 59999999; i++)
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
        static void AC()
        {

            for (int i = 69977375; i < 69999999; i++)
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
        static void DF()
        {

            for (int i = 70130003; i < 73699999; i++)
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
        static void MT()
        {

            for (int i = 78139038; i < 78999999; i++)
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
