using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TokaApi.Data
{
    public partial class TokaContext : DbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SP_AgregarPersonaFisica>().HasNoKey();
        }


        public async Task<SP_AgregarPersonaFisica> AgregarPersonaFisicaAsync(string nombre, string apellidoPaterno, string apellidoMaterno, string rFC, DateTime? fechaNacimiento, int? usuarioAgrega )
        {

            IEnumerable<SP_AgregarPersonaFisica> persona;
            try
            {
                
                var param1 = new SqlParameter("nombreParam", nombre == null ? (object)DBNull.Value : nombre);
                var param2 = new SqlParameter("apellidoPaternoParam", apellidoPaterno == null ? (object)DBNull.Value : apellidoPaterno);
                var param3 = new SqlParameter("apellidoMaternoParam", apellidoMaterno == null ? (object)DBNull.Value : apellidoMaterno);
                var param4 = new SqlParameter("rFCParam", rFC == null ? (object)DBNull.Value : rFC);
                var param5 = new SqlParameter("fechaNacimientoParam", fechaNacimiento == null ? (object)DBNull.Value : fechaNacimiento);
                var param6 = new SqlParameter("usuarioAgregaParam", usuarioAgrega == null ? (object)DBNull.Value : usuarioAgrega);
                string sqlQuery = "EXEC [dbo].[sp_AgregarPersonaFisica] @Nombre = @nombreParam, @ApellidoPaterno = @apellidoPaternoParam, @ApellidoMaterno = @apellidoMaternoParam, @RFC = @rFCParam, @FechaNacimiento = @fechaNacimientoParam, @UsuarioAgrega = @usuarioAgregaParam";
                persona = await Set<SP_AgregarPersonaFisica>().FromSqlRaw(sqlQuery, param1, param2, param3, param4, param5, param6).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return persona.First();
        }


        public async Task<SP_AgregarPersonaFisica> DeletePersonaFisicaAsync(int IdPersonaFisica)
        {
            IEnumerable<SP_AgregarPersonaFisica> persona;
            try
            {

                var param1 = new SqlParameter("IdPersonaFisicaParam", IdPersonaFisica);
               
                string sqlQuery = "EXEC [dbo].[sp_EliminarPersonaFisica] @IdPersonaFisica = @IdPersonaFisicaParam";
                persona = await Set<SP_AgregarPersonaFisica>().FromSqlRaw(sqlQuery, param1).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return persona.First();
        }
    }
}
