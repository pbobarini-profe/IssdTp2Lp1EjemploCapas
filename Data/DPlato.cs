using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Data
{
    public class DPlato
    {
        public static Guid Create(Plato p)
        {
            if (p.Id == Guid.Empty) p.Id = Guid.NewGuid();

            const string sql = @"INSERT INTO dbo.Platos (Id, Descripcion, Precio)
                                 VALUES (@Id, @Descripcion, @Precio)";
            using (var cn = Db.GetConnection())
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = p.Id;
                cmd.Parameters.Add("@Descripcion", SqlDbType.NVarChar, 100).Value = p.Descripcion;
                cmd.Parameters.Add("@Precio", SqlDbType.Decimal).Value = p.Precio;
                cmd.Parameters["@Precio"].Precision = 15; cmd.Parameters["@Precio"].Scale = 2;

                cn.Open();
                cmd.ExecuteNonQuery();
            }
            return p.Id;
        }

        public static Plato GetById(Guid id)
        {
            const string sql = @"SELECT Id, Descripcion, Precio FROM dbo.Platos WHERE Id = @Id";
            using (var cn = Db.GetConnection())
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = id;
                cn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.Read()) return null;
                    return new Plato
                    {
                        Id = dr.GetGuid(0),
                        Descripcion = dr.GetString(1),
                        Precio = dr.GetDecimal(2)
                    };
                }
            }
        }

        public static List<Plato> ListAll()
        {
            var list = new List<Plato>();
            const string sql = @"SELECT Id, Descripcion, Precio FROM dbo.Platos ORDER BY Descripcion";
            using (var cn = Db.GetConnection())
            using (var cmd = new SqlCommand(sql, cn))
            {
                cn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new Plato
                        {
                            Id = dr.GetGuid(0),
                            Descripcion = dr.GetString(1),
                            Precio = dr.GetDecimal(2)
                        });
                    }
                }
            }
            return list;
        }

        public static void Update(Plato p)
        {
            const string sql = @"UPDATE dbo.Platos SET Descripcion=@Descripcion, Precio=@Precio WHERE Id=@Id";
            using (var cn = Db.GetConnection())
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@Descripcion", SqlDbType.NVarChar, 100).Value = p.Descripcion;
                cmd.Parameters.Add("@Precio", SqlDbType.Decimal).Value = p.Precio;
                cmd.Parameters["@Precio"].Precision = 15; cmd.Parameters["@Precio"].Scale = 2;
                cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = p.Id;

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void Delete(Guid id)
        {
            const string sql = @"DELETE FROM dbo.Platos WHERE Id=@Id";
            using (var cn = Db.GetConnection())
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = id;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
