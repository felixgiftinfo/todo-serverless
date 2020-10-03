using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Todo_API.DTOs;
using Todo_API.Models;

namespace Todo_API.Helper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            this.CreateMap<TodoModel, TodoDTO>().ReverseMap();
            this.CreateMap<TodoModel, TodoReadDTO>().ReverseMap();
        }
    }
}
