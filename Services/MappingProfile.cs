﻿using AutoMapper;
using Project_Quizz_API.Models;
using Project_Quizz_API.Models.DTOs;

namespace Project_Quizz_API.Services
{
    /// <summary>
    /// Mapping profile for the automapper.
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Quiz_Match_Overview_User, QuizMatchOverviewUserDto>();
        }
    }
}
