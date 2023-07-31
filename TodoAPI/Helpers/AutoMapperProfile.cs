namespace TodoAPI.Helpers;

using AutoMapper;
using TodoAPI.Models;
using TodoAPI.Entities;

public class AutoMapperProfile : Profile
	{
    public AutoMapperProfile()
    {
        // CreateRequest -> User
        CreateMap<CreateRequest, Todo>();

        // UpdateRequest -> User
        CreateMap<UpdateRequest, Todo>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore both null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));
    }
}


