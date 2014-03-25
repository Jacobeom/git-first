using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using TwitterAvailability.Dto;

namespace TwitterAvailability.Repository
{
    public class IssueDetailRepository : RepositoryBase<IssueDetail>
    {
        public IssueDetailRepository()
            : base()
        {
        } 


        public IssueDetailRepository(String connectionString)
            : base(connectionString)
        {
        } 

        public override string TableName
        {
            get { return "IssueDetail"; }
        }

        public override IRowMapper<IssueDetail> RowMapper
        {
            get { return MapBuilder<IssueDetail>.MapAllProperties().Build(); }
        }

        public override IssueDetail SaveEntity(IssueDetail entity)
        {
            String query =
                @"INSERT INTO [IssueDetail] ([Date],[Description],[FileName],[ContentType],[Data],[InternalID])  VALUES        
                (@Date, @Description, @FileName, @ContentType,@Data, @InternalID)  SELECT @@Identity";

            DbCommand command = Database.GetSqlStringCommand(query);
          
            Database.AddInParameter(command,"@Date",DbType.String,entity.Date);
            Database.AddInParameter(command,"@Description",DbType.String,entity.Description);
            Database.AddInParameter(command,"@FileName",DbType.String,entity.FileName);
            Database.AddInParameter(command,"@ContentType",DbType.String,entity.ContentType);
            Database.AddInParameter(command,"@Data", DbType.Binary, entity.Data);
            Database.AddInParameter(command,"@InternalID",DbType.String,entity.InternalId);
            
            int id = Convert.ToInt32(this.Database.ExecuteScalar(command));
            entity.Id = id;
            
            return entity;

        }

        public override IssueDetail UpdateEntity(IssueDetail entity)
        {
            String query =
                @"UPDATE [IssueDetail] SET [Date] = @Date ,[Description] = @Description,[FileName] = @FileName,[ContentType] = @ContentType,[Data] = @Data,[InternalID] = @InternalID WHERE Id=@Id";

            DbCommand command = Database.GetSqlStringCommand(query);
            Database.AddInParameter(command, "@Date", DbType.String, entity.Date);
            Database.AddInParameter(command, "@Description", DbType.String, entity.Description);
            Database.AddInParameter(command, "@FileName", DbType.String, entity.FileName);
            Database.AddInParameter(command, "@ContentType", DbType.String, entity.ContentType);
            Database.AddInParameter(command, "@Data", DbType.Byte, entity.Data);
            Database.AddInParameter(command, "@InternalID", DbType.String, entity.InternalId);
            Database.AddInParameter(command, "@Id", DbType.Int32, entity.Id);

            this.Database.ExecuteNonQuery(command);

            return entity;

        }

        public virtual Byte[] GetDataFromUniqueId(int id)
        {
            Byte[] result;
            
            String selectQuery = @"SELECT Top 1 Data FROM [IssueDetail] WHERE [Id] = @Id";
            
            DbCommand command = Database.GetSqlStringCommand(selectQuery);
            Database.AddInParameter(command, "@Id", DbType.Int32, id);

            result = (Byte[]) Database.ExecuteScalar(command);

            return result;
        }


        public virtual IList<IssueDetail> FindAllByInternalId(string internalId)
        {
            String selectQuery = @"SELECT * FROM [IssueDetail] where [InternalId] = @InternalId";

          
            IParameterMapper parameterMapper = new InternalIdParameterMapper();
            SqlStringAccessor<IssueDetail> accessor = new SqlStringAccessor<IssueDetail>(Database, selectQuery, parameterMapper, RowMapper);

            return accessor.Execute(internalId).ToList<IssueDetail>().ToList();
        }
    }


    public class InternalIdParameterMapper : IParameterMapper
    {
        public void AssignParameters(DbCommand command, object[] parameterValues)
        {
            DbParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@InternalId";
            parameter.Value = parameterValues[0];
            command.Parameters.Add(parameter);
        }
    }
}
