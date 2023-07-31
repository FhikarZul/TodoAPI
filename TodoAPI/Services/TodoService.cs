namespace TodoAPI.Services;

using AutoMapper;
using TodoAPI.Models;
using TodoAPI.Repositories;
using TodoAPI.Entities;

public interface ITodoService
{
    Task<IEnumerable<Todo>> GetAll();
    Task<Todo> GetById(int id);
    Task Create(CreateRequest model);
    Task Update(int id, UpdateRequest model);
    Task Delete(int id);
}

public class TodoService : ITodoService
{
    private ITodoRepository _userRepository;
    private readonly IMapper _mapper;

    public TodoService(
        ITodoRepository userRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Todo>> GetAll()
    {
        return await _userRepository.GetAll();
    }

    public async Task<Todo> GetById(int id)
    {
        var user = await _userRepository.GetById(id);

        return user ?? throw new KeyNotFoundException("todo not found");
    }

    public async Task Create(CreateRequest model)
    {
        // validate
        //if (await _userRepository.GetByEmail(model.Email!) != null)
        //    throw new AppException("User with the email '" + model.Email + "' already exists");

        // map model to new user object
        var user = _mapper.Map<Todo>(model);

        // hash password
        //user.PasswordHash = BCrypt.HashPassword(model.Password);

        // save user
        await _userRepository.Create(user);
    }

    public async Task Update(int id, UpdateRequest model)
    {
        var user = await _userRepository.GetById(id) ?? throw new KeyNotFoundException("User not found");

        // validate
        //var emailChanged = !string.IsNullOrEmpty(model.Email) && user.Email != model.Email;
        //if (emailChanged && await _userRepository.GetByEmail(model.Email!) != null)
        //    throw new AppException("User with the email '" + model.Email + "' already exists");

        // hash password if it was entered
        //if (!string.IsNullOrEmpty(model.Password))
        //    user.PasswordHash = BCrypt.HashPassword(model.Password);

        // copy model props to user
        _mapper.Map(model, user);

        // save user
        await _userRepository.Update(user);
    }

    public async Task Delete(int id)
    {
        await _userRepository.Delete(id);
    }
}
