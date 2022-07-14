using System.Collections.Generic;

namespace Origin.YMC.Repositories
{
    public interface ISqlCommand
    {
        int ExecuteCommand(string sqlCommand, params object[] parameters);
        IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters);
    }
}
