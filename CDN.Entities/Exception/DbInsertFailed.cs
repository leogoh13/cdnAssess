namespace CDN.Entities.Exception;

public class DbInsertFailed(string message) : System.Exception(message);