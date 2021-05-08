using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data.SqlClient;
using System.Configuration;

namespace Database
{
    public class Base : IBase
    {
        private string connectionString = ConfigurationManager.AppSettings["SqlConnection"];
        public int Key
        {
            get
            {
                foreach(PropertyInfo pi in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    OpcoesBase opcoes = (OpcoesBase)pi.GetCustomAttribute(typeof(OpcoesBase));
                    if(opcoes!=null && opcoes.ChavePrimaria)
                    {
                        return Convert.ToInt32(pi.GetValue(this));
                    }
                }
                return 0;
            }
        }

        public List<IBase> Busca()
        {
            var list = new List<IBase>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<string> where = new List<string>();
                string chavePrimaria = string.Empty;
                foreach (PropertyInfo pi in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    OpcoesBase opcoesBase = (OpcoesBase)pi.GetCustomAttribute(typeof(OpcoesBase));
                    if (opcoesBase != null)
                    {
                        if (opcoesBase.ChavePrimaria)
                        {
                            chavePrimaria = pi.Name + "=" + pi.GetValue(this);
                        }
                        if(opcoesBase.UsaBD && !opcoesBase.ChavePrimaria)
                        {
                            if(pi.GetValue(this) != null)
                            {
                                if(tipoPropriedade(pi) == "varchar(50)" || tipoPropriedade(pi) == "datetime")
                                {
                                    where.Add(pi.Name + "='" + pi.GetValue(this) + "'");
                                }
                                else
                                {
                                    where.Add(pi.Name + "=" + pi.GetValue(this));
                                }
                            }
                        }
                    }
                }
                string sql;
                if (Key==0)
                {
                    sql = "select * from " + this.GetType().Name + "s ";
                    if (where.Count > 0)
                    {
                        sql += " where " + string.Join(" or ", where.ToArray());
                    }
                }
                else
                {
                    sql = "select * from " + this.GetType().Name + "s where "+chavePrimaria;
                }
                SqlCommand sqlCommand = new SqlCommand(sql, connection);
                sqlCommand.Connection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    var obj = (IBase)Activator.CreateInstance(this.GetType());
                    foreach(PropertyInfo pi in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    {
                        OpcoesBase opcoes = (OpcoesBase)pi.GetCustomAttribute(typeof(OpcoesBase));
                        if (opcoes != null)
                        {
                            pi.SetValue(obj, reader[pi.Name]);
                        }
                        list.Add(obj);
                    }
                }
            }
                return list;
        }

        private string tipoPropriedade(PropertyInfo pi)
        {
            switch (pi.PropertyType.Name)
            {
                case "Int32":
                    return "int";
                case "Int64":
                    return "bigint";
                case "double":
                    return "decimal(9,2)";
                case "DateTime":
                    return "datetime";
                default:
                    return "varchar(100)";
            }
        }

        public virtual void Salvar()
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                List<string> campos = new List<string>();
                List<string> valores = new List<string>();
                foreach(PropertyInfo pi in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    OpcoesBase opcoes = (OpcoesBase)pi.GetCustomAttribute(typeof(OpcoesBase));
                    if(opcoes!=null && opcoes.UsaBD && !opcoes.ChavePrimaria)
                    {
                        if (this.Key == 0)
                        {
                            campos.Add(pi.Name);
                            valores.Add("'" + pi.GetValue(this) + "'");
                        }
                        else
                        {
                            if (!opcoes.ChavePrimaria)
                            {
                                valores.Add(" " + pi.Name + "='" + pi.GetValue(this) + "'");
                            }
                        }
                    }
                }
                string sql = null;
                if (this.Key == 0){
                    sql = "insert into " + this.GetType().Name + "s (" + string.Join(", ", campos.ToArray()) + ")" +
                        " values (" + string.Join(", ", valores.ToArray()) + ");";
                }
                else
                {
                    sql = "update " + this.GetType().Name + "s set" + string.Join(", ", valores.ToArray()) + " where Id=" + this.Key + ";";                
                }
                SqlCommand command = new SqlCommand(sql, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<IBase> Todos()
        {
            var list = new List<IBase>();
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "select * from " + this.GetType().Name + "s";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var obj = (IBase)Activator.CreateInstance(this.GetType());
                    foreach(PropertyInfo pi in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    {
                        OpcoesBase opcoesBase = (OpcoesBase)pi.GetCustomAttribute(typeof(OpcoesBase));
                        if(opcoesBase!=null && opcoesBase.UsaBD)
                        {
                            pi.SetValue(obj, reader[pi.Name]);
                        }
                    }
                    list.Add(obj);
                }
            }
            return list;
        }

        public void Excluir()
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "delete from " + this.GetType().Name + "s where id = " + this.Key + ";";
                SqlCommand delete = new SqlCommand(sql, connection);
                delete.Connection.Open();
                delete.ExecuteNonQuery();
            }
        }

        public void CreateTable()
        {
          using(SqlConnection connection = new SqlConnection(connectionString))
          {
                List<string> campos = new List<string>();
                string chavePrimaria = "";
                foreach (PropertyInfo pi in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    OpcoesBase opcoesBase = (OpcoesBase)pi.GetCustomAttribute(typeof(OpcoesBase));
                    if(opcoesBase != null && opcoesBase.UsaBD)
                    {
                        if (opcoesBase.ChavePrimaria)
                        {
                            chavePrimaria = pi.Name + " int identity, ";
                        }
                        else{
                            campos.Add(pi.Name + " " + tipoPropriedade(pi) + " ");
                        }
                    }
                }
                string tabela = "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].["+this.GetType().Name+"s]') AND type in (N'U'))"
                    + "DROP TABLE[dbo].["+this.GetType().Name+"s];";
                SqlCommand dropCreateTable = new SqlCommand(tabela, connection);
                dropCreateTable.Connection.Open();
                dropCreateTable.ExecuteNonQuery();

                string sql = "CREATE TABLE " + this.GetType().Name + "s( ";
                sql += chavePrimaria;
                sql += string.Join(", ",campos.ToArray());
                sql += ");";
                dropCreateTable = new SqlCommand(sql, connection);
                dropCreateTable.ExecuteNonQuery();
          }
        }
    }
}
