using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BPlato
    {
        public static Guid Crear(Plato p)
        {
            try
            {
                return Data.DPlato.Create(p);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public static void Actualizar(Plato p)
        {
            try
            {
                Data.DPlato.Update(p);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Eliminar(Guid id)
        {
            try
            {
                Data.DPlato.Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Plato Obtener(Guid id)
        {
            try
            {
                return Data.DPlato.GetById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Plato> Listar()
        {
            try
            {
                return Data.DPlato.ListAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
