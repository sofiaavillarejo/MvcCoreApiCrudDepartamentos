﻿using System.Net.Http.Headers;
using System.Text;
using MvcCoreApiCrudDepartamentos.Models;
using Newtonsoft.Json;

namespace MvcCoreApiCrudDepartamentos.Services
{
    public class ServiceDepartamentos
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue header;

        public ServiceDepartamentos(IConfiguration configuration)
        {
            this.header = new MediaTypeWithQualityHeaderValue("applciation/json");
            this.UrlApi = configuration.GetValue<string>("ApiUrls:ApiDepartamentos");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Departamento>> GetDepartamentosAsync()
        {
            string request = "api/departamento";
            List<Departamento> data = await this.CallApiAsync<List<Departamento>>(request);
            return data;
        }

        public async Task<Departamento> FindDepartamento(int idDepartamento)
        {
            string request = "api/departamento" + idDepartamento;
            Departamento data = await this.CallApiAsync<Departamento>(request);
            return data;
        }

        //LOS METODOS DE ACCION QUE RECIBEN OBJETOS PUEDEN SER GENERICOS Y RECIBIR T
        //SI LOS METODOS RECIBEN LA INFORMACION POR URL SI QUE NO SUELEN SER GENERICOS

        public async Task InsertDepartamentoAsync(int id, string nombre, string localidad) 
        { 
            using (HttpClient client = new HttpClient()) 
            {
                string request = "api/departamento";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                //aqui no seria muy necesario porque es solo cuando recibimos datos, pero lo ponemos aun así por si acaso
                client.DefaultRequestHeaders.Accept.Add(this.header);
                //CREAMOS NUESTRO MODEL A ENVIAR
                Departamento dpto = new Departamento
                {
                    IdDepartamento = id,
                    Nombre = nombre,
                    Localidad = localidad    
                };
                //CONVERTIMOS EL MODEL A JSON
                string json = JsonConvert.SerializeObject(dpto);
                //PARA ENVIAR DATOS, SE USA StringContent DONDE DEBEMOS ENVIAR EL JSON, FORMATO Y TIPO
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
            }
        }
    }
}
