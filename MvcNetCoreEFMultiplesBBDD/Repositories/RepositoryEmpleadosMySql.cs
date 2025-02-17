using Microsoft.EntityFrameworkCore;
using MvcNetCoreEFMultiplesBBDD.Data;
using MvcNetCoreEFMultiplesBBDD.Models;

#region PROCEDURES
/*
 DELIMITER $$

CREATE PROCEDURE SP_ALL_EMPLEADOS()
BEGIN
    SELECT * FROM V_EMPLEADOS;
END$$

DELIMITER ;


DELIMITER $$

CREATE PROCEDURE SP_FIND_EMPLEADO(IN p_idEmpleado INT)
BEGIN
    SELECT * FROM V_EMPLEADOS WHERE IDEMPLEADO = p_idEmpleado;
END$$

DELIMITER 
*/
#endregion
namespace MvcNetCoreEFMultiplesBBDD.Repositories
{
    public class RepositoryEmpleadosMySql : IRepositoryEmpleados
    {
        private HospitalContext context;

        public RepositoryEmpleadosMySql(HospitalContext context)
        {
            this.context = context;
        }
        public async Task<List<EmpleadoView>> GetEmpleadosAsync()
        {
            string sql = "CALL SP_ALL_EMPLEADOS()";
            var consulta = this.context.EmpleadosView.FromSqlRaw(sql);
            return await consulta.ToListAsync();
            //var consulta = from datos in this.context.EmpleadosView
            //               select datos;

            //return await consulta.ToListAsync();
        }
        public async Task<EmpleadoView> FindEmpleadoAsync(int idEmpleado)
        {

            string sql = "CALL SP_FIND_EMPLEADO(@p0)";
            var consulta =  await this.context.EmpleadosView.FromSqlRaw(sql, idEmpleado).ToListAsync();
            return consulta.FirstOrDefault();
            //var consulta = from datos in this.context.EmpleadosView
            //               where datos.IdEmpleado == idEmpleado
            //               select datos;
            //return await consulta.FirstOrDefaultAsync();
        }

       
    }
}
