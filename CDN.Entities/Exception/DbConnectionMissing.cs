namespace CDN.Entities.Exception;

public class DbConnectionMissing(string message = "") : System.Exception(message);