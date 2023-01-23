using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.DbAccess
{
    public interface ISqlDataAccess
    {
        Task<IEnumerable<T>> CallSP<T, U>(string SQLStatment, U parameters, string connectionId = "Default");
        Task<long> ExecuteScalar<T, U>(string SQLStatment, U parameters, string connectionId = "Default");
        Task<IEnumerable<T>> LoadData<T, U>(string SQLStatment, U parameters, string connectionId = "Default");
        Task SaveData<T>(string SQLStatment, T parameters, string connectionId = "Default");
    }
}