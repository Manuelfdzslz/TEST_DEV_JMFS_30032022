using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using TokaApi.Data;
using TokaApi.Interfaces;
using TokaApi.Models;
using TokaApi.Models.Exceptions;

namespace TokaApi.Services
{
    public class PersonaFisicaService: IPersonaFisca
    {
        private readonly TokaContext _context;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public PersonaFisicaService(TokaContext context,IConfiguration config,IMapper mapper)
        {
            _context = context;
            _config = config;
            _mapper = mapper;
        }

        public async Task<PersonasFisica> GetByIDAsync(int id)
        {
            PersonasFisica persona = new PersonasFisica();
            Tb_PersonasFisica dbPerson = await _context.Tb_PersonasFisicas.FindAsync(id);
            if (dbPerson!=null)
            {
                persona = _mapper.Map<PersonasFisica>(dbPerson);
                return persona;
            }
            return null;
        }

        public async Task<IEnumerable<PersonasFisica>> GetAsync()
        {
            List<PersonasFisica> list = new List<PersonasFisica>();
            List<Tb_PersonasFisica> dbList = await _context.Tb_PersonasFisicas.ToListAsync();

            foreach (var i in dbList)
            {
                PersonasFisica p = new PersonasFisica
                {
                    Activo = i.Activo,
                    ApellidoMaterno = i.ApellidoMaterno,
                    ApellidoPaterno = i.ApellidoPaterno,
                    FechaActualizacion = i.FechaActualizacion,
                    FechaNacimiento = i.FechaNacimiento,
                    FechaRegistro = i.FechaRegistro,
                    IdPersonaFisica = i.IdPersonaFisica,
                    Nombre = i.Nombre,
                    RFC = i.RFC,
                    UsuarioAgrega = i.UsuarioAgrega
                };
                list.Add(p);
            }
            return list;
        }


        public async Task<ApiResponse> PostAsync(PersonasFisica m)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                
                SP_AgregarPersonaFisica r=await _context.AgregarPersonaFisicaAsync(m.Nombre
                   ,m.ApellidoPaterno
                   , m.ApellidoMaterno
                   , m.RFC
                   , m.FechaNacimiento
                   , m.UsuarioAgrega);

                response.IsSuccess = true;
                response.Code = "200";
                response.Message = r.MENSAJEERROR;
                response.Errors.Add(r.MENSAJEERROR);

                if (r.ERROR<0)
                {
                    response.IsSuccess = false;
                    response.Code = "500";
                    response.Message = r.MENSAJEERROR;
                    response.Errors.Add(r.MENSAJEERROR);
                    
                }
                m.IdPersonaFisica = r.ERROR;
            }
            catch (Exception ex)
            {

                response.IsSuccess = false;
                response.Code = "500";
                response.Message = ex.Message;
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        public async Task<ApiResponse> PutAsync(PersonasFisica m)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                SqlConnection sqlConnection1 = new SqlConnection(_config.GetConnectionString("Database"));
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;

                cmd.CommandText = "[dbo].[sp_ActualizarPersonaFisica]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdPersonaFisica", m.IdPersonaFisica);
                cmd.Parameters.AddWithValue("@Nombre", m.Nombre);
                cmd.Parameters.AddWithValue("@ApellidoPaterno", m.ApellidoPaterno);
                cmd.Parameters.AddWithValue("@ApellidoMaterno", m.ApellidoMaterno);
                cmd.Parameters.AddWithValue("@RFC", m.RFC);
                cmd.Parameters.AddWithValue("@FechaNacimiento", m.FechaNacimiento);
                cmd.Parameters.AddWithValue("@UsuarioAgrega", m.UsuarioAgrega);
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();

                reader = await cmd.ExecuteReaderAsync();
                int error = 0;
                string mensaje = "";
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        error = reader.GetInt32(0);
                        mensaje = reader.GetString(1);
                    }
                }

                reader.Close();
                sqlConnection1.Close();

                response.IsSuccess = true;
                response.Code = "200";
                response.Message = mensaje;
                response.Errors.Add(mensaje);

                if (error < 0)
                {
                    response.IsSuccess = false;
                    response.Code = "500";
                    response.Message = mensaje;
                    response.Errors.Add(mensaje);
                }
              
            }
            catch (Exception ex)
            {

                response.IsSuccess = false;
                response.Code = "500";
                response.Message = ex.Message;
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            ApiResponse error = new ApiResponse();
            try
            {
                SP_AgregarPersonaFisica r = await _context.DeletePersonaFisicaAsync(id);

                error.IsSuccess = true;
                error.Code = "200";
                error.Message = r.MENSAJEERROR;
                error.Errors.Add(r.MENSAJEERROR);
                
                if (r.ERROR < 0)
                {
                    error.IsSuccess = false;
                    error.Code = "500";
                    error.Message = r.MENSAJEERROR;
                    error.Errors.Add(r.MENSAJEERROR);
                }
              
            }
            catch (Exception ex)
            {

                error.Code = "500";
                error.Message = ex.Message;
                error.Errors.Add(ex.Message);

            }
            return error;

        }
    }
}
