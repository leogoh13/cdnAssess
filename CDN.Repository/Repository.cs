using System.Data;
using CDN.Common.Repository;
using CDN.Entities.DbEntity;
using CDN.Entities.Exception;
using Dapper;
using static Dapper.SqlMapper;

namespace CDN.Repository;

public class Repository(DbConnectionSettings dbConnSettings) : IRepository
{
    public async Task<bool> ExecuteAsync(string sql, object? parameters = null)
    {
        using var conn = GetConnection();
        using var transaction = conn.BeginTransaction();

        try
        {
            var result = await conn.ExecuteAsync(sql, parameters, transaction);
            transaction.Commit();

            return result != 0;
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }

    /// <summary>
    ///     Perfect for executing a stored procedure that returns several sets of data.
    /// </summary>
    /// <param name="sql">SQL string with parameter variables</param>
    /// <param name="parameters">DynamicParameters type</param>
    /// <returns>IDataReader</returns>
    /// <exception cref="DbStoredProcedureException"></exception>
    public async Task<IDataReader?> ExecuteReaderAsync(string sql, object? parameters = null)
    {
        using var conn = GetConnection();
        using var transaction = conn.BeginTransaction();

        try
        {
            var result = await conn.ExecuteReaderAsync(sql, parameters, transaction);

            if (result == null) throw new DbStoredProcedureException(sql);

            transaction.Commit();
            return result;
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task<List<TModelType>?> QueryAsync<TModelType>(string sql, object? parameters = null) where TModelType : class, IDbTable
    {
        using var conn = GetConnection();
        using var transaction = conn.BeginTransaction();

        try
        {
            var result = await conn.QueryAsync<TModelType>(sql, parameters, transaction);
            var modelTypes = result.ToList();

            transaction.Commit();
            return modelTypes;
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task<TModelType?> QueryFirstOrDefaultAsync<TModelType>(string sql, object? parameters = null) where TModelType : class, IDbTable
    {
        using var conn = GetConnection();
        using var transaction = conn.BeginTransaction();

        try
        {
            var result = await conn.QueryFirstOrDefaultAsync<TModelType>(sql, parameters, transaction);

            if (result == null) throw new DbNoRecordsFound();

            transaction.Commit();
            return result;
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task<TModelType?> QuerySingleAsync<TModelType>(string sql, object? parameters = null) where TModelType : class, IDbTable
    {
        using var conn = GetConnection();
        using var transaction = conn.BeginTransaction();

        try
        {
            var result = await conn.QuerySingleAsync<TModelType>(sql, parameters, transaction);

            if (result == null) throw new DbNoRecordsFound();

            transaction.Commit();
            return result;
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task<TType?> ExecuteScalarAsync<TType>(string sql, object? parameters = null) where TType : struct
    {
        using var conn = GetConnection();
        using var transaction = conn.BeginTransaction();

        try
        {
            var result = await conn.ExecuteScalarAsync<TType>(sql, parameters, transaction);
            transaction.Commit();
            return result;
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }

    private IDbConnection GetConnection()
    {
        var conn = new DbConnectionHelper(dbConnSettings).GetConnection();
        conn.Open();
        return conn;
    }
}

public class MultiResult<T1, T2, T3> where T2 : new() where T3 : new()
{
    public T1 First { get; set; } = default!;
    public T2 Second { get; set; } = new();
    public T3 Third { get; set; } = new();
}