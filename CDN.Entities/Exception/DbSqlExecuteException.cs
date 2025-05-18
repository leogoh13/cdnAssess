namespace CDN.Entities.Exception;

public class DbSqlExecuteException(string message) : System.Exception(message);
public class DbSqlExecuteReaderException() : System.Exception("Reader is null");