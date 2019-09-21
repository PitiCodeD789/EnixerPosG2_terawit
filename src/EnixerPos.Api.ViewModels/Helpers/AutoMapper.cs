using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Api.ViewModels.Helpers
{
    public class RealAutoMapper
    {
        public H Map<T, H>(T post)
            where T : class
            where H : class
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<T, H>());
            var mapper = new Mapper(config);
            H postDTO = mapper.Map<H>(post);
            return postDTO;
        }

        public List<H> Map<T, H>(List<T> model) //T - TypeIn, H - TypeOut
            where T : class
            where H : class
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<T, H>());
            var mapper = new Mapper(config);
            List<H> DTOs = mapper.Map<List<H>>(model);
            return DTOs;
        }
    }
}
