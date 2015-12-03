using UFO.Dal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Transactions;

namespace UFO.Dal.SqlServer
{
    public class Database : IDatabase
    {
        private readonly string connectionString;

        public Database(string connectionString)
        {
            this.connectionString = connectionString;
        }


        public DbCommand CreateCommand(string commandText)
        {
            return new SqlCommand(commandText);
        }

        public int DeclareParameter(DbCommand command, string name, DbType type)
        {
            if (!command.Parameters.Contains(name))
            {
                return command.Parameters.Add(new SqlParameter(name, type));
            }
            else
            {
                throw new ArgumentException(string.Format("Parameter {0} already exists", name));
            }
        }

        public void DefineParameter(DbCommand command, string name, DbType type, object value)
        {
            int paramIndex = DeclareParameter(command, name, type);
            command.Parameters[paramIndex].Value = value;
        }

        public void SetParameter(DbCommand command, string name, object value)
        {
            if (command.Parameters.Contains(name))
            {
                command.Parameters[name].Value = value;
            }
            else
            {
                throw new ArgumentException(string.Format("Parameter {0} is not declared", name));
            }
        }

        public int ExecuteNonQuery(DbCommand command)
        {
            DbConnection connection = null;
            try
            {
                connection = GetOpenConnection();
                command.Connection = connection;
                return command.ExecuteNonQuery();
            }   
            finally
            {
                ReleaseConnection(connection);
            }
        }

        public IDataReader ExecuteReader(DbCommand command)
        {
            DbConnection connection = null;
            try
            {
                connection = GetOpenConnection();
                command.Connection = connection;

                //let the reader close the connestion only if it is not shared
                var beahvior = Transaction.Current == null ?
                    CommandBehavior.CloseConnection :
                    CommandBehavior.Default;

                return command.ExecuteReader();
            }
            catch //catch any exception
            {
                ReleaseConnection(connection);
                throw; //rethrow the exception
            }

            //We don't use finally here because the reader still
            //need the connection. It will be closed by the reader's
            //Dispose method

        }

        #region connection management

        [ThreadStatic]
        private static DbConnection sharedConnection;


        //private DbConnection GetOpenConnection()
        private DbConnection CreateOpenConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        private DbConnection GetOpenConnection()
        {
            Transaction currentTransaction = Transaction.Current;

            if (currentTransaction == null)
            {
                return CreateOpenConnection();
            } else
            {
                if (sharedConnection == null)
                {
                    sharedConnection = CreateOpenConnection();
                    currentTransaction.TransactionCompleted +=
                        (s, e) =>
                        {
                            sharedConnection.Close();
                            sharedConnection = null;
                        };
                }

                return sharedConnection;
            }

        }

        private void ReleaseConnection(DbConnection connection)
        {
            //connection?.Close();

            //close connection if no transaction is active

            if (connection != null && Transaction.Current == null)
                connection.Close();
            
        }

        #endregion
    }


}
