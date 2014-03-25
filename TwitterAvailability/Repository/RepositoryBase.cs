using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using TwitterAvailability.Dto;

namespace TwitterAvailability.Repository
{
    public abstract class RepositoryBase<T>
    {
        
        public class IDParameterMapper : IParameterMapper
        {
            public void AssignParameters(DbCommand command, object[] parameterValues)
            {
                DbParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@Id";
                parameter.Value = parameterValues[0];
                command.Parameters.Add(parameter);
            }
        }

        public RepositoryBase()
        {

        }

        public RepositoryBase(String connectionString)
        {
            _database = new SqlDatabase(connectionString);
        } 

        private Database _database;


        public Database Database
        {
            get
            {
                if(_database == null)
                    _database = EnterpriseLibraryContainer.Current.GetInstance<Database>("Availability");
                return _database;

            }
        }

        public abstract string TableName { get; }

        public abstract IRowMapper<T> RowMapper { get; }

        public virtual T FindByUniqueId(int id)
        {
            String selectQuery = string.Format(@"SELECT * FROM {0} WHERE [Id] = @Id",TableName);
            
            IParameterMapper parameterMapper = new IDParameterMapper();
            SqlStringAccessor<T> accessor = new SqlStringAccessor<T>(Database, selectQuery, parameterMapper, RowMapper);

            return accessor.Execute(id).ToList<T>().FirstOrDefault();
        }

        public virtual IList<T> FindAll()
        {
            String selectQuery = string.Format(@"SELECT * FROM {0}",TableName);
        
            return Database.ExecuteSqlStringAccessor<T>(selectQuery, RowMapper).ToList();
        }

        public virtual void DeleteEntity(int id)
        {

            String selectQuery = string.Format(@"DELETE {0} WHERE [Id] = @Id", TableName);

            DbCommand command = Database.GetSqlStringCommand(selectQuery);
            Database.AddInParameter(command, "@Id", DbType.Int32, id);

            Database.ExecuteNonQuery(command);
          
        }

        public abstract T SaveEntity(T entity);

        public abstract T UpdateEntity(T entity);
    }
}
