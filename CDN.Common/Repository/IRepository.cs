using System.Data;
using CDN.Entities.DbEntity;
using Dapper;
using static Dapper.SqlMapper;

namespace CDN.Common.Repository;

public interface IRepository
{
    Task<bool> ExecuteAsync(string sql, DynamicParameters? parameters = null);
    Task<IDataReader?> ExecuteReaderAsync(string sql, DynamicParameters? parameters = null);

    Task<List<TModelType>?> QueryAsync<TModelType>(string sql, DynamicParameters? parameters = null)
        where TModelType : class, IDbTable;

    Task<TModelType?> QueryFirstOrDefaultAsync<TModelType>(string sql, DynamicParameters? parameters = null)
        where TModelType : class, IDbTable;

    Task<TModelType?> QuerySingleAsync<TModelType>(string sql, DynamicParameters? parameters = null)
        where TModelType : class, IDbTable;

    Task<TType?> ExecuteScalarAsync<TType>(string sql, DynamicParameters? parameters = null)
        where TType : struct;

    Task<GridReader> QueryMultipleAsync(string sql, DynamicParameters? parameters = null);
}