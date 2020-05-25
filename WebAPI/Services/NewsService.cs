﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Entities;

namespace WebAPI.Services
{
    public class NewsService : INewsService
    {
        APIContext _context;

        public NewsService(APIContext context)
        {
            _context = context;
        }

        public async Task<int> Save(News model)
        {
            if (model.NewsId == 0)
            {
                await _context.AddAsync<News>(model);
               
            }
            else
            {
                var news = await _context.FindAsync<News>(model.NewsId);
               
                    news.Theme = model.Theme;
                    news.Title = model.Title;
                    news.Content = model.Content;
                    _context.Update(news);
            }

            return await _context.SaveChangesAsync(); ;
        }

        public async Task<IEnumerable<News>> GetAll()
        {
            return await _context.News.ToListAsync<News>();
        }

        public async Task<News> GetById(int id)
        {
            return await _context.FindAsync<News>(id);
        }

        public async Task<int> Delete(int id)
        {
            var news =  await _context.FindAsync<News>(id);
            _context.Remove<News>(news);
            
            return await _context.SaveChangesAsync();
        }
    }
}
