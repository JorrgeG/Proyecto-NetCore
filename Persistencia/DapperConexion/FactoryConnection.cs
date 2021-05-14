using System.Data;

namespace Persistencia.DapperConexion
{
    public class FactoryConnection : IFactoryConnection
    {
        private IDbConnection _connection;

        public FactoryConnection(IDbConnection connection)
        {
            _connection = connection;
        }
        public void CloseConnection()
        {
            throw new System.NotImplementedException();
        }

        public IDbConnection GetConnection()
        {
            throw new System.NotImplementedException();
        }
    }
}