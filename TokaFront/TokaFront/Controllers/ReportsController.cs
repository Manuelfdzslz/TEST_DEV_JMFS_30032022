using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TokaFront.Attributes;
using TokaFront.Models;

namespace TokaFront.Controllers
{
    [ServiceFilter(typeof(Authentication))]
    public class ReportsController : Controller
    {
        protected readonly IHttpClientFactory _httpClientFactory;
        protected static string credentials;

        public ReportsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public  ActionResult Index()
        {
            return View();
        }

            // GET: ReportController
        [HttpPost]
        public async Task<ActionResult> GetTableData(DataTableAjaxPostModel model)
        {
            var taskData =  _GetDataAsync();
            TableDataSet dataSet = new TableDataSet();
            dataSet.Draw = model.draw;
            dataSet.Data = await taskData;
            dataSet.RecordsTotal = dataSet.Data.Count;
            dataSet.RecordsFiltered = dataSet.Data.Count;

            dataSet.Data=dataSet.Data.Skip(model.start).Take(model.length).ToList();
               
            return Json(new { 
                Draw= dataSet.Draw, 
                Data = dataSet.Data,
                dataSet.RecordsTotal,
                dataSet.RecordsFiltered
            });

           

        }

        [HttpGet]
        public async Task<IActionResult> CreateReportAsync()
        {
            var data = _GetDataAsync();
            var wb = new XLWorkbook();
            string fileName = "Reporte_cliente.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            var sheetClients = wb.Worksheets.Add("Clientes");
            int row = 3;
            sheetClients.Cell(row, 1).Value = "IdCliente";
            sheetClients.Cell(row, 2).Value = "Nombre";
            sheetClients.Cell(row, 3).Value = "Paterno";
            sheetClients.Cell(row, 4).Value = "Materno";
            sheetClients.Cell(row, 5).Value = "IdViaje";
            sheetClients.Cell(row, 6).Value = "RFC";
            sheetClients.Cell(row, 7).Value = "Sucursal";
            sheetClients.Cell(row, 8).Value = "Razon Social";
            sheetClients.Cell(row, 9).Value = "Fecha Registro Empresa";


            List<Cliente> results = await data;
            row ++;
            foreach (var d in results)
            {
                sheetClients.Cell(row, 1).SetValue<string>(d.IdCliente.ToString());
                sheetClients.Cell(row, 1).Style.NumberFormat.NumberFormatId = 0;
                sheetClients.Cell(row, 2).SetValue<string>(d.Nombre);
                sheetClients.Cell(row, 3).SetValue<string>(d.Paterno);
                sheetClients.Cell(row, 4).SetValue<string>(d.Materno);
                sheetClients.Cell(row, 5).SetValue<string>(d.IdViaje.ToString());
                sheetClients.Cell(row, 5).Style.NumberFormat.NumberFormatId = 0;
                sheetClients.Cell(row, 6).SetValue<string>(d.RFC);
                sheetClients.Cell(row, 7).SetValue<string>(d.Sucursal);
                sheetClients.Cell(row, 8).SetValue<string>(d.RazonSocial);
                sheetClients.Cell(row, 9).SetValue<string>(d.FechaRegistroEmpresa.ToString());

                row++;
            }

            var firstCell = sheetClients.FirstCellUsed();
            var lastCell = sheetClients.LastCellUsed();
            var range = sheetClients.Range(firstCell.Address, lastCell.Address);
            var table = range.CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium15;
            sheetClients.Columns().AdjustToContents();

            sheetClients.Cell(1, 1).Value = "Reportes de Clientes";
            sheetClients.Cell(1, 1).Style.Font.Bold = true;
            sheetClients.Cell(1, 1).Style.Font.SetFontSize(14);
            sheetClients.Range(sheetClients.Cell(1, 1), sheetClients.Cell(1, 4)).Merge();


            sheetClients.Cell(2, 1).Value = "Creado en " + ":  " + DateTime.Now.ToString("MM/dd/yyyy HH:mm");
            sheetClients.Range(sheetClients.Cell(2, 1), sheetClients.Cell(2, 4)).Merge();

            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                var content = stream.ToArray();
                return File(content, contentType, fileName);
            } 

        }

        private async Task<List<Cliente>> _GetDataAsync()
        {
            List<Cliente> results = new List<Cliente>();
            if (string.IsNullOrEmpty(credentials))
            {
                credentials = await _AuthenticateAsync();
            }

            var client = _httpClientFactory.CreateClient("Toka");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(credentials);
            var resp = client.GetAsync($"api/customers");
            HttpResponseMessage response = resp.Result;
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                JObject obj = JObject.Parse(res);
                var data = obj.SelectToken("Data").ToString();
                results = JsonConvert.DeserializeObject<List<Cliente>>(data);
                
                 

            }
            return results;

        }
        private async Task<string> _AuthenticateAsync()
        {
            var tk = "";
            var client = _httpClientFactory.CreateClient("Toka");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            StringContent content = new StringContent(JsonConvert.SerializeObject(new { Username = "ucand0021", Password= "yNDVARG80sr@dDPc2yCT!" }), Encoding.UTF8, "application/json");
            var resp = client.PostAsync($"api/login/authenticate", content);
            HttpResponseMessage response = resp.Result;
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                JObject obj = JObject.Parse(res);
                tk = obj.SelectToken("Data").ToString();

            }
            return tk;
        }


    }
}
