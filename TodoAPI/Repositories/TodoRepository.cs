namespace TodoAPI.Repositories;

using Dapper;
using TodoAPI.Helpers;
using TodoAPI.Entities;

public interface ITodoRepository
{
    Task<IEnumerable<Todo>> GetAll();
    Task<Todo> GetById(int id);
    Task Create(Todo user);
    Task Update(Todo user);
    Task Delete(int id);
}

public class TodoRepository : ITodoRepository
{
    private DataContext _context;

    public TodoRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Todo>> GetAll()
    {
        using var connection = _context.CreateConnection();
        var sql = """
            SELECT * FROM tb_todo
        """;
        return await connection.QueryAsync<Todo>(sql);
    }

    public async Task<Todo> GetById(int id)
    {
        using var connection = _context.CreateConnection();
        var sql = """
            SELECT * FROM tb_todo 
            WHERE id = @id
        """;
        return await connection.QuerySingleOrDefaultAsync<Todo>(sql, new { id });
    }

 
    public async Task Create(Todo user)
    {
        using var connection = _context.CreateConnection();
        var sql = """
            INSERT INTO tb_todo (name, description)
            VALUES (@Name, @Description)
        """;
        await connection.ExecuteAsync(sql, user);
    }

    public async Task Update(Todo user)
    {
        using var connection = _context.CreateConnection();
        var sql = """
            UPDATE tb_todo 
            SET name = @Name,
                description = @Description
            WHERE id = @Id
        """;
        await connection.ExecuteAsync(sql, user);
    }

    public async Task Delete(int id)
    {
        using var connection = _context.CreateConnection();
        var sql = """
            DELETE FROM tb_todo 
            WHERE id = @id
        """;
        await connection.ExecuteAsync(sql, new { id });
    }
}
