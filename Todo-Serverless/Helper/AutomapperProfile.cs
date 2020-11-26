using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Todo_Serverless.DTOs;
using Todo_Serverless.Models;

namespace Todo_Serverless.Helper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            this.CreateMap<TodoModel, TodoDTO>().ReverseMap();
            this.CreateMap<TodoModel, TodoReadDTO>().ReverseMap();


            this.CreateMap<UserModel, UserDTO>().ReverseMap();
            this.CreateMap<UserModel, UserReadDTO>().ReverseMap();
        }
    }
}
