using System.Data;
using CDN.Entities.DbEntity;
using Dapper;
using static Dapper.SqlMapper;

namespace CDN.Common.Repository;

public interface IRepository
{
    Task<bool> ExecuteAsync(string sql, object? parameters = null);
    Task<IDataReader?> ExecuteReaderAsync(string sql, object? parameters = null);

    Task<List<TModelType>?> QueryAsync<TModelType>(string sql, object? parameters = null)
        where TModelType : class, IDbTable;

    Task<TModelType?> QueryFirstOrDefaultAsync<TModelType>(string sql, object? parameters = null)
        where TModelType : class, IDbTable;

    Task<TModelType?> QuerySingleAsync<TModelType>(string sql, object? parameters = null)
        where TModelType : class, IDbTable;

    Task<TType?> ExecuteScalarAsync<TType>(string sql, object? parameters = null)
        where TType : struct;

    // Task<List<object?>> QueryMultipleAsync<T>(string sql, object? parameters = null) where T : class;
}