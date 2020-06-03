using AutoMapper;
using DAL.Dto;
using DAL.Request;
using DAL.Response;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repository.Impl
{
    public class NewsRepository : INewsRepository
    {
        private readonly string _connectStrings;
        private readonly IMapper _mapper;

        public NewsRepository(IConfiguration configuration, IMapper mapper)
        {
            _connectStrings = configuration.GetConnectionString("DBContext");
            _mapper = mapper;
        }

       

        public async Task<IEnumerable<NewsModel>> GetAll()
        {
            using (var conn = new SqlConnection(_connectStrings))
            {
                if(conn.State == ConnectionState.Closed)
                {
                    conn.Open();

                }

                var result = await conn.QueryAsync<News>("Get_News_All", null, null, null, CommandType.Text);
                var resultResponse = _mapper.Map<IEnumerable<News>,IEnumerable<NewsResponse>>(result);
                return resultResponse;
            }
        }

        public async Task<int> Create(NewsRequest request)
        {
            using (var conn = new SqlConnection(_connectStrings))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();

                }
                var paramaters = new DynamicParameters();
                paramaters.Add("@Title", request.Title);
                paramaters.Add("@Theme", request.Theme);
                paramaters.Add("@Content", request.Content);
                paramaters.Add("@UserId", request.UserId);

                var result = await conn.ExecuteAsync("Create_News", paramaters, null, null, CommandType.StoredProcedure);

                return result;
            }
        }

        public async Task<int> Update(NewsRequest request)
        {
            using (var conn = new SqlConnection(_connectStrings))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();

                }
                var paramaters = new DynamicParameters();
                paramaters.Add("@Title", request.Title);
                paramaters.Add("@Theme", request.Theme);
                paramaters.Add("@Content", request.Content);
                paramaters.Add("@UserId", request.UserId);
                paramaters.Add("@id",request.NewsId);

                var result = await conn.ExecuteAsync("Update_News_ById", paramaters, null, null, CommandType.StoredProcedure);

                return result;
            }
        }

        public async Task<int> Delete(int id)
        {
            using (var conn = new SqlConnection(_connectStrings))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var paramaters = new DynamicParameters();
                paramaters.Add("@id", id);

                var result = await conn.ExecuteAsync("Delete_News_ById", paramaters, null, null, CommandType.StoredProcedure);

                return result;
            }
        }

        public async Task<NewsResponse> GetById(int id)
        {
            using (var conn = new SqlConnection(_connectStrings))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                
                var paramater = new DynamicParameters();
                paramater.Add("@id", id);
                var result = await conn.QueryAsync<NewsResponse>("Get_News_ById", paramater, null, null, CommandType.StoredProcedure);
               
                return result.FirstOrDefault();
            }
        }
    }
}
