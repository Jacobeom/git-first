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


    public class ProductRepository : RepositoryBase<Product>
    {

        public ProductRepository(): base()
        {
        } 

        public ProductRepository(String connectionString)
            : base(connectionString)
        {
        } 

        public override string TableName
        {
            get { return "Product";}
        }

        public override IRowMapper<Product> RowMapper
        {
            get { return MapBuilder<Product>.MapAllProperties().Build(); }
        }

        public class ProductKeyParameterMapper : IParameterMapper
        {
            public void AssignParameters(DbCommand command, object[] parameterValues)
            {
                DbParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@ProductKey";
                parameter.Value = parameterValues[0];
                command.Parameters.Add(parameter);
            }
        }

        public override Product SaveEntity(Product entity)
        {
            String query =
                @"INSERT INTO [Product] ([ProductName],[ProductKey],[Hidden]) VALUES (@ProductName, @ProductKey, @Hidden) SELECT @@Identity";

            DbCommand command = Database.GetSqlStringCommand(query);
            Database.AddInParameter(command,"@ProductName",DbType.String,entity.ProductName);
            Database.AddInParameter(command,"@ProductKey",DbType.String,entity.ProductKey);
            Database.AddInParameter(command,"@Hidden",DbType.Boolean,entity.Hidden);
            
            int id = Convert.ToInt32(this.Database.ExecuteScalar(command));
            entity.Id = id;
            
            return entity;
        }

        public override Product UpdateEntity(Product entity)
        {
            String query =
                @"UPDATE [Product] SET [ProductName] = @ProductName, [ProductKey] = @ProductKey, [Hidden] = @Hidden WHERE [Id]= @Id";

            DbCommand command = Database.GetSqlStringCommand(query);
            Database.AddInParameter(command, "@ProductName", DbType.String, entity.ProductName);
            Database.AddInParameter(command, "@ProductKey", DbType.String, entity.ProductKey);
            Database.AddInParameter(command, "@Hidden", DbType.Boolean, entity.Hidden);
            Database.AddInParameter(command, "@Id", DbType.Int32, entity.Id);

            this.Database.ExecuteNonQuery(command);

            return entity;
        }


        public Product FindByProductKey(string productKey)
        {
            Product result = null;
            String selectQuery = @"SELECT * FROM [Product] WHERE [ProductKey] = @ProductKey";

            IParameterMapper parameterMapper = new ProductKeyParameterMapper();
            SqlStringAccessor<Product> accessor = new SqlStringAccessor<Product>(Database, selectQuery, parameterMapper, RowMapper);

            List<Product> listProducts = accessor.Execute(productKey).ToList<Product>();

            if (listProducts.Count > 0)
                result = listProducts.FirstOrDefault();

            return result;
        }

     
    }

}
