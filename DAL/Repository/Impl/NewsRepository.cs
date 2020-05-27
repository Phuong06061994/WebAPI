using AutoMapper;
using DAL.Request;
using DAL.Response;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebAPI.Entities;

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
                paramaters.Add("@CreatedBy", request.CreatedBy);
                paramaters.Add("@id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

                var result = await conn.ExecuteAsync("Create_News", paramaters, null, null, CommandType.StoredProcedure);

                return result;
            }
        }

        public async Task<IEnumerable<NewsResponse>> GetAll()
        {
            using (var conn = new SqlConnection(_connectStrings))
            {
                if(conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();

                }

                var result = await conn.QueryAsync<News>("Get_News_All", null, null, null, CommandType.Text);
                var resultResponse = _mapper.Map<IEnumerable<News>,IEnumerable<NewsResponse>>(result);
                return resultResponse;
            }
        }
    }
}
