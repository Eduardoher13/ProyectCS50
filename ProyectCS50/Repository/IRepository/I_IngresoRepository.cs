using ProyectCS50.Models;

namespace ProyectCS50.Repository.IRepository
{
    public interface I_IngresoRepository: IRepository<Ingreso>
    {
        Task<Ingreso> UpdateAsync(Ingreso entity);

        //Task<double> CalcNoctunidadRisgoLab(double salarioBase);
        //Task<double> CaclHorasExtras(double horas, double salarioBase);
        //Task<double> CalcAntiguedad(int years, double salarioBase);

        //Task<int> GetYearsTrabajadosById(int id);
        //Task<double> CalSalario(double salarioBase, double antiguedad, double riesgoLaboral, double nocturnidad, double horasExtras, double otrosIngresos);
        //Task<Ingreso> ObtenerRegistroIngreso(int id);

        //Task<bool> ObtenerIngresoPorNumeroEmpleado(int id);
    }
}
