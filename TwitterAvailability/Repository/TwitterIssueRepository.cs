using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Common.Logging;
using Microsoft.Practices.EnterpriseLibrary.Data;
using TwitterAvailability.Dto;

namespace TwitterAvailability.Repository
{
    public class TwitterIssueRepository : RepositoryBase<TwitterIssue>
    {
        public TwitterIssueRepository(): base()
        {
        } 

        public TwitterIssueRepository(String connectionString) : base(connectionString)
        {
        } 

        

        public override string TableName
        {
            get { return "TwitterIssue"; }
        }

        public override IRowMapper<TwitterIssue> RowMapper
        {
            get { return MapBuilder<TwitterIssue>.MapAllProperties()
                                                 .Map(x=>x.TypeString).ToColumn("Type")
                                                 .DoNotMap(x=>x.Type)
                                                 .Build();
            }
        }

        public override TwitterIssue SaveEntity(TwitterIssue entity)
        {
            String query =
               @"INSERT INTO [TwitterIssue] 
                            ([TwitterId],[IntenalId],[Type],[ProductKey],[CountryKey],[Description],[OriginalMessage],[TwitterDate],[StartingDate],[EndingDate],[EffectiveDate],[EstimatedMinutes],[ProductId],[PreviousTwittId],[Finished])
                     VALUES (@TwitterId,@IntenalId,@Type,@ProductKey,@CountryKey,@Description,@OriginalMessage,@TwitterDate,@StartingDate,@EndingDate,@EffectiveDate,@EstimatedMinutes,@ProductId,@PreviousTwittId, @Finished) 
                 SELECT @@Identity";

            DbCommand command = Database.GetSqlStringCommand(query);
            Database.AddInParameter(command, "@TwitterId", DbType.String, entity.TwitterId);
            Database.AddInParameter(command, "@IntenalId", DbType.String, entity.IntenalId);
            Database.AddInParameter(command, "@Type", DbType.String, entity.TypeString);
            Database.AddInParameter(command, "@ProductKey", DbType.String, entity.ProductKey);
            Database.AddInParameter(command, "@CountryKey", DbType.String, entity.CountryKey);
            Database.AddInParameter(command, "@Description", DbType.String, entity.Description);
            Database.AddInParameter(command, "@OriginalMessage", DbType.String, entity.OriginalMessage);
            Database.AddInParameter(command, "@TwitterDate", DbType.DateTime, entity.TwitterDate);
            Database.AddInParameter(command, "@ProductId", DbType.Int32, entity.ProductId);
            Database.AddInParameter(command, "@Finished", DbType.Boolean, entity.Finished);

            if (entity.StartingDate.HasValue)
                Database.AddInParameter(command, "@StartingDate", DbType.DateTime, entity.StartingDate);
            else
                Database.AddInParameter(command, "@StartingDate", DbType.DateTime, null);

            if (entity.EndingDate.HasValue)
                Database.AddInParameter(command, "@EndingDate", DbType.DateTime, entity.EndingDate);
            else
                Database.AddInParameter(command, "@EndingDate", DbType.DateTime, null);

            if (entity.EffectiveDate.HasValue)
                Database.AddInParameter(command, "@EffectiveDate", DbType.DateTime, entity.EffectiveDate);
            else
                Database.AddInParameter(command, "@EffectiveDate", DbType.DateTime, null);


            if (entity.EstimatedMinutes.HasValue)
                Database.AddInParameter(command, "@EstimatedMinutes", DbType.Int32, entity.EstimatedMinutes);
            else
                Database.AddInParameter(command, "@EstimatedMinutes", DbType.Int32, null);

            if (entity.PreviousTwittId.HasValue)
                Database.AddInParameter(command, "@PreviousTwittId", DbType.Int32, entity.PreviousTwittId.Value);
            else
                Database.AddInParameter(command, "@PreviousTwittId", DbType.Int32, null);

            int id = Convert.ToInt32(this.Database.ExecuteScalar(command));
            entity.Id = id;

            return entity;
        }

        public override TwitterIssue UpdateEntity(TwitterIssue entity)
        {

            String query =
                @"UPDATE [TwitterIssue]
                  SET [TwitterId] = @TwitterId, [IntenalId] = @IntenalId,[Type] = @Type, [ProductKey] = @ProductKey, [CountryKey] =  @CountryKey ,[Description] = @Description, [OriginalMessage] = @OriginalMessage, [TwitterDate] = @TwitterDate, [StartingDate] = @StartingDate, [EndingDate] = @EndingDate, [EffectiveDate] = @EffectiveDate, [EstimatedMinutes] = @EstimatedMinutes,[ProductId] = @ProductId, [PreviousTwittId] = @PreviousTwittId, [Finished] = @Finished
                   WHERE [Id] = @Id";

            DbCommand command = Database.GetSqlStringCommand(query);
            Database.AddInParameter(command, "@TwitterId", DbType.String, entity.TwitterId);
            Database.AddInParameter(command, "@IntenalId", DbType.String, entity.IntenalId);
            Database.AddInParameter(command, "@Type", DbType.String, entity.TypeString);
            Database.AddInParameter(command, "@ProductKey", DbType.String, entity.ProductKey);
            Database.AddInParameter(command, "@CountryKey", DbType.String, entity.CountryKey);
            Database.AddInParameter(command, "@Description", DbType.String, entity.Description);
            Database.AddInParameter(command, "@OriginalMessage", DbType.String, entity.OriginalMessage);
            Database.AddInParameter(command, "@TwitterDate", DbType.DateTime, entity.TwitterDate);
            Database.AddInParameter(command, "@ProductId", DbType.Int32, entity.ProductId);
            Database.AddInParameter(command, "@Finished", DbType.Boolean, entity.Finished);

            if (entity.StartingDate.HasValue)
                Database.AddInParameter(command, "@StartingDate", DbType.DateTime, entity.StartingDate);
            else
                Database.AddInParameter(command, "@StartingDate", DbType.DateTime, null);

            if (entity.EndingDate.HasValue)
                Database.AddInParameter(command, "@EndingDate", DbType.DateTime, entity.EndingDate);
            else
                Database.AddInParameter(command, "@EndingDate", DbType.DateTime, null);

            if (entity.EffectiveDate.HasValue)
                Database.AddInParameter(command, "@EffectiveDate", DbType.DateTime, entity.EffectiveDate);
            else
                Database.AddInParameter(command, "@EffectiveDate", DbType.DateTime, null);

            if (entity.EstimatedMinutes.HasValue)
                Database.AddInParameter(command, "@EstimatedMinutes", DbType.Int32, entity.EstimatedMinutes);
            else
                Database.AddInParameter(command, "@EstimatedMinutes", DbType.Int32, null);

            if (entity.PreviousTwittId.HasValue)
                Database.AddInParameter(command, "@PreviousTwittId", DbType.Int32, entity.PreviousTwittId.Value);
            else
                Database.AddInParameter(command, "@PreviousTwittId", DbType.Int32, null);

            Database.AddInParameter(command, "@Id", DbType.Int32, entity.Id);

            this.Database.ExecuteNonQuery(command);

            return entity;
        }

        public TwitterIssue FindByTweeterId(String tweeterId)
        {
            TwitterIssue result = null;
            String selectQuery = @"SELECT * FROM [TwitterIssue] WHERE [TwitterId] = @TwitterId";

            IParameterMapper parameterMapper = new TwitterIdParameterMapper();
            SqlStringAccessor<TwitterIssue> accessor = new SqlStringAccessor<TwitterIssue>(Database, selectQuery, parameterMapper, RowMapper);

            IList<TwitterIssue> list = accessor.Execute(tweeterId).ToList<TwitterIssue>();

            if (list.Count>0)
                result = list.FirstOrDefault();

            return result;
        }


        public TwitterIssue FindByInternalIdAndTypeOrderByEffectiveDate(DateTime effectiveDate, String intenalId, IList<String> types)
        {

            TwitterIssue result = null;
            String selectQuery = @"SELECT TOP 1 * FROM TwitterIssue WHERE 
                                    ((SELECT count(T.Id) FROM TwitterIssue AS T where T.PreviousTwittId=TwitterIssue.Id) = 0) AND 
                                     Finished=0 and EffectiveDate <@EffectiveDate and 
                                     IntenalId=@IntenalId and 
                                     Type in (select Type from @Types) order by EffectiveDate desc";

            IParameterMapper parameterMapper = new TypesEffectiveDateAndInternalIdParameterMapper();
            SqlStringAccessor<TwitterIssue> accessor = new SqlStringAccessor<TwitterIssue>(Database, selectQuery, parameterMapper, RowMapper);

            IList<TwitterIssue> list = accessor.Execute(effectiveDate, intenalId, types).ToList<TwitterIssue>();

            if (list.Count > 0)
                result = list.FirstOrDefault();
            
            return result;


        }

        public TwitterIssue FindByInternalIdAndTypeOrderByTwitterDate(DateTime twitterDate, String intenalId, IList<String> types)
        {
           
            TwitterIssue result = null;
            String selectQuery = @"SELECT TOP 1 * FROM TwitterIssue WHERE 
                                    ((SELECT count(T.Id) FROM TwitterIssue AS T where T.PreviousTwittId=TwitterIssue.Id) = 0) AND 
                                     Finished=0 and TwitterDate <@TwitterDate and 
                                     IntenalId=@IntenalId and 
                                     Type in (select Type from @Types) order by TwitterDate desc";

            IParameterMapper parameterMapper = new TypesTwitterDateAndInternalIdParameterMapper();
            SqlStringAccessor<TwitterIssue> accessor = new SqlStringAccessor<TwitterIssue>(Database, selectQuery, parameterMapper, RowMapper);

            IList<TwitterIssue> list = accessor.Execute(twitterDate,intenalId, types).ToList<TwitterIssue>();

            if (list.Count > 0)
                result = list.FirstOrDefault();

            return result;


        }

        public TwitterIssue FindByInternalIdAndTypeOrderByEffectiveDateAndTwitterDate(DateTime twitterDate, String intenalId, IList<String> types)
        {
            
            TwitterIssue result = null;
            String selectQuery = @"SELECT TOP 1 * FROM TwitterIssue WHERE 
                                    ((SELECT count(T.Id) FROM TwitterIssue AS T where T.PreviousTwittId=TwitterIssue.Id) = 0) AND 
                                     Finished=0 and EffectiveDate <@TwitterDate and 
                                     IntenalId=@IntenalId and 
                                     Type in (select Type from @Types) order by TwitterDate desc";

            IParameterMapper parameterMapper = new TypesTwitterDateAndInternalIdParameterMapper();
            SqlStringAccessor<TwitterIssue> accessor = new SqlStringAccessor<TwitterIssue>(Database, selectQuery, parameterMapper, RowMapper);

            IList<TwitterIssue> list = accessor.Execute(twitterDate,intenalId, types).ToList<TwitterIssue>();

            if (list.Count > 0)
            {
                result = list.FirstOrDefault();
            }
            return result;


        }

         public IList<TwitterIssue> FindAllLeafNodesRightFormed(DateTime from, DateTime to)
         {
             String selectQuery =
                    @"select * from TwitterIssue as T WHERE 
                                        (
	                                     ((SELECT count(TI.Id) FROM TwitterIssue AS TI where TI.PreviousTwittId=T.Id) = 0 ) and
	                                      ((select count(*) from TwitterIssue TI where 
				                                    TI.Id in(select TOP 1 Id from dbo.GetNodesFromLeaf(T.Id) order by level desc) and 
				                                    TI.Type in ('#TEFOP','#BTP','#VBTPX')) >0) and
	                                       (T.EffectiveDate >= @FromDate and T.EffectiveDate <= @ToDate)
	  
	                                    )";


             IParameterMapper parameterMapper = new FromToEffectiveDateParameterMapper();
             SqlStringAccessor<TwitterIssue> accessor = new SqlStringAccessor<TwitterIssue>(Database, selectQuery, parameterMapper, RowMapper);

             return accessor.Execute(from, to).ToList<TwitterIssue>();
         }

         public IList<TwitterIssue> FindAllParentNodesFromThis(int id)
         {
             String selectQuery =
                    @"select 
                        	TI.* 
                      from
	                        dbo.GetNodesFromLeaf(@Id) as Hierarchy inner join 
	                        dbo.TwitterIssue as TI on Hierarchy.Id = TI.Id 
                            order by Hierarchy.level desc";


             IParameterMapper parameterMapper = new IDParameterMapper();
             SqlStringAccessor<TwitterIssue> accessor = new SqlStringAccessor<TwitterIssue>(Database, selectQuery, parameterMapper, RowMapper);

             return accessor.Execute(id).ToList<TwitterIssue>();
         }


         public int RemoveFromDate(DateTime dateTime)
         {
             String query =
                @"DELETE [TwitterIssue] WHERE [EffectiveDate] < @EffectiveDate";

             DbCommand command = Database.GetSqlStringCommand(query);
             Database.AddInParameter(command, "@EffectiveDate", DbType.DateTime, dateTime);

             return this.Database.ExecuteNonQuery(command);
         }

        public class TypesTwitterDateAndInternalIdParameterMapper : IParameterMapper
        {
            public void AssignParameters(DbCommand command, object[] parameterValues)
            {
                SqlCommand sqlCommand = (SqlCommand) command;

                SqlParameter parameter0 = sqlCommand.CreateParameter();
                parameter0.DbType = DbType.DateTime;
                parameter0.ParameterName = "@TwitterDate";
                parameter0.Value = parameterValues[0];
                command.Parameters.Add(parameter0);

                SqlParameter parameter1 = sqlCommand.CreateParameter();
                parameter1.DbType = DbType.String;
                parameter1.ParameterName = "@IntenalId";
                parameter1.Value = parameterValues[1];
                command.Parameters.Add(parameter1);

                IList<String> types = (IList<string>) parameterValues[2];
                DataTable typesTable = new DataTable("Items");
                typesTable.Columns.Add("Type", typeof(string));
                foreach (string type in types)
                {
                    typesTable.Rows.Add(type);
                }

                SqlParameter parameter2 = sqlCommand.CreateParameter();
                parameter2.SqlDbType = SqlDbType.Structured;
                parameter2.ParameterName = "@Types";
                parameter2.TypeName = "dbo.TableType";
                parameter2.Value = typesTable;
                command.Parameters.Add(parameter2);
            }
        }

        public class TypesEffectiveDateAndInternalIdParameterMapper : IParameterMapper
        {
            public void AssignParameters(DbCommand command, object[] parameterValues)
            {
                SqlCommand sqlCommand = (SqlCommand)command;

                SqlParameter parameter0 = sqlCommand.CreateParameter();
                parameter0.DbType = DbType.DateTime;
                parameter0.ParameterName = "@EffectiveDate";
                parameter0.Value = parameterValues[0];
                command.Parameters.Add(parameter0);

                SqlParameter parameter1 = sqlCommand.CreateParameter();
                parameter1.DbType = DbType.String;
                parameter1.ParameterName = "@IntenalId";
                parameter1.Value = parameterValues[1];
                command.Parameters.Add(parameter1);

                IList<String> types = (IList<string>)parameterValues[2];
                DataTable typesTable = new DataTable("Items");
                typesTable.Columns.Add("Type", typeof(string));
                foreach (string type in types)
                {
                    typesTable.Rows.Add(type);
                }

                SqlParameter parameter2 = sqlCommand.CreateParameter();
                parameter2.SqlDbType = SqlDbType.Structured;
                parameter2.ParameterName = "@Types";
                parameter2.TypeName = "dbo.TableType";
                parameter2.Value = typesTable;
                command.Parameters.Add(parameter2);
            }
        }

        public class TwitterIdParameterMapper : IParameterMapper
        {
            public void AssignParameters(DbCommand command, object[] parameterValues)
            {
                DbParameter parameter = command.CreateParameter();
                parameter.DbType = DbType.String;
                parameter.ParameterName = "@TwitterId";
                parameter.Value = parameterValues[0];
                command.Parameters.Add(parameter);
            }
        }

        public class FromToEffectiveDateParameterMapper : IParameterMapper
        {
            public void AssignParameters(DbCommand command, object[] parameterValues)
            {
                DbParameter parameter1 = command.CreateParameter();
                parameter1.DbType = DbType.DateTime;
                parameter1.ParameterName = "@FromDate";
                parameter1.Value = parameterValues[0];
                command.Parameters.Add(parameter1);

                DbParameter parameter2 = command.CreateParameter();
                parameter2.DbType = DbType.DateTime;
                parameter2.ParameterName = "@ToDate";
                parameter2.Value = parameterValues[1];
                command.Parameters.Add(parameter2);
            }
        }
    }
}
