using System.Data;
using Microsoft.EntityFrameworkCore;
using MvcNetCoreEFMultiplesBBDD.Data;
using MvcNetCoreEFMultiplesBBDD.Models;
using Oracle.ManagedDataAccess.Client;

namespace MvcNetCoreEFMultiplesBBDD.Repositories
{
    public class RepositoryEmpleadosOracle : IRepositoryEmpleados
    {
        private HospitalContext context;
        public RepositoryEmpleadosOracle(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<EmpleadoView>> GetEmpleadosAsync()
        {
            string sql = "BEGIN SP_ALL_EMPLEADOS(:p_cursor_empleados); END;";
            OracleParameter pamCursor = new OracleParameter();
            pamCursor.ParameterName = "p_cursor_empleados";
            pamCursor.Value = null;
            pamCursor.Direction = ParameterDirection.Output;
            pamCursor.OracleDbType = OracleDbType.RefCursor;
            var consulta = this.context.EmpleadosView.FromSqlRaw(sql, pamCursor);
            return await consulta.ToListAsync();
        }

        public Task<EmpleadoView> FindEmpleadoAsync(int idEmpleado)
        {
            throw new NotImplementedException();
        }

        
    }
}
