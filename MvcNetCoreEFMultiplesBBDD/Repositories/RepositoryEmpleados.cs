using Microsoft.EntityFrameworkCore;
using MvcNetCoreEFMultiplesBBDD.Data;
using MvcNetCoreEFMultiplesBBDD.Models;


#region VISTAS
/* 
 alter view V_EMPLEADOS
as
	select EMP_NO as IDEMPLEADO, EMP.APELLIDO,EMP.OFICIO,EMP.SALARIO,DEPT.DNOMBRE as DEPARTAMENTO, DEPT.LOC AS LOCALIDAD
	from EMP inner join DEPT
	on EMP.DEPT_NO = DEPT.DEPT_NO
go
 
 */
#endregion
#region PROCEDURES
/* 
 CREATE PROCEDURE SP_ALL_EMPLEADOS
AS
	SELECT * FROM V_EMPLEADOS
GO
CREATE OR REPLACE PROCEDURE SP_ALL_EMPLEADOS
(p_cursor_empleados out sys_refcursor)
AS
BEGIN
  OPEN p_cursor_empleados FOR
  SELECT * FROM V_EMPLEADOS;
END;
 
 */
#endregion
namespace MvcNetCoreEFMultiplesBBDD.Repositories
{
    public class RepositoryEmpleados:IRepositoryEmpleados
    {
        private HospitalContext context;
        public RepositoryEmpleados(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<EmpleadoView>> GetEmpleadosAsync()
        {
            var sql = "SP_ALL_EMPLEADOS";
            var consulta = this.context.EmpleadosView.FromSqlRaw(sql);
            return await consulta.ToListAsync();
            
            //var consulta = from datos in this.context.EmpleadosView
            //               select datos;
            //return await consulta.ToListAsync();
        }

        public async Task<EmpleadoView> FindEmpleadoAsync(int idEmpleado)
        {
            var consulta = from datos in this.context.EmpleadosView
                           where datos.IdEmpleado == idEmpleado
                           select datos;
            return await consulta.FirstOrDefaultAsync();
        }
    }
}
