using AutoMapper;
using DAL.Dto;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Impl
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;

        public PermissionRepository(IConfiguration configuration, IMapper mapper)
        {
            _connectionString = configuration.GetConnectionString("DBContext");
            _mapper = mapper;
        }
        public async Task<IEnumerable<string>> GetAllByUserId(Guid userId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();

                var paramaters = new DynamicParameters();
                paramaters.Add("@userId", userId.ToString());

                var result = await conn.QueryAsync<string>("Get_Permission_ByUserId", paramaters, null, null, System.Data.CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
