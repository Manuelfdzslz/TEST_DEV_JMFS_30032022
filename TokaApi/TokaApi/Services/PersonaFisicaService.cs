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


        public async Task<PersonasFisica> PostAsync(PersonasFisica m)
        {

            try
            {
                
                SP_AgregarPersonaFisica r=await _context.AgregarPersonaFisicaAsync(m.Nombre
                   ,m.ApellidoPaterno
                   , m.ApellidoMaterno
                   , m.RFC
                   , m.FechaNacimiento
                   , m.UsuarioAgrega);

                if (r.ERROR<0)
                {
                    throw new Exception(r.MENSAJEERROR);
                }
                m.IdPersonaFisica = r.ERROR;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return m;
        }

        public async Task<PersonasFisica> PutAsync(PersonasFisica m)
        {
            PersonasFisica res = new PersonasFisica();
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
                // Data is accessible through the DataReader object here.
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

                if (error < 0)
                {
                    throw new Exception(mensaje);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return res;
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                SP_AgregarPersonaFisica r = await _context.DeletePersonaFisicaAsync(id);

                if (r.ERROR < 0)
                {
                    throw new Exception(r.MENSAJEERROR);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
    }
}
