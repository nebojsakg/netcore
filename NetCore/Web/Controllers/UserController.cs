using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using DelegateDecompiler.EntityFrameworkCore;
using Common.Extensions;
using Core.Common;
using Core.Common.Model;
using Core.Common.Model.Search;
using Core.Users.Command;
using Core.Users.Query.Extensions;
using Core.Users.Query.Request;
using Core.Users.Query.Response;
using Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Controllers
{
    [Route("api/users")]
    public class UserController : BaseController
    {
        private readonly UserService _userService;
        private readonly EnumsService _enumsService;

        public UserController(DatabaseContext ctx, UserService userService, EnumsService enumsService) : base(ctx)
        {
            _userService = userService;
            _enumsService = enumsService;
        }

        // GET api/users/5
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<Response<UserDetails>> Get(int id)
        {
            var result = await ctx.Users
                .ProjectTo<UserDetails>()
                .DecompileAsync()
                .FirstAsync(s => s.Id == id);

            return OkResponse(result);
        }

        // PUT api/users/5
        [HttpPut("{id}")]
        public async Task<Response<UserDetails>> Put(int id, [FromBody]UserDetails cmd)
        {
            var user = await _userService.Update(id, cmd);

            return await Get(user.Id);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<Response<bool>> Delete(int id)
        {
            await _userService.Delete(id);

            return OkResponse(true);
        }

        // GET api/users-metadata
        [Route("/api/users-metadata")]
        public async Task<dynamic> GetMetadataAsync()
        {
            var result = new
            {
                Roles = await ctx.Roles.ProjectTo<RoleDetails>().ToListAsync()
            };

            return OkResponse(result);
        }

        // POST api/users/search
        [Route("/api/users/search")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<Response<SearchResponse<UserDetails>>> Search([FromBody]UserQuery request)
        {
            var result = await ctx.Users
                .Search(request)
                .ProjectTo<UserDetails>()
                .ToPaginated(request.PageNumber, request.PageSize);

            return OkResponse(result);
        }

        // GET api/users/typeahead
        [Route("/api/users/typeahead")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<Response<List<BasicUser>>> SearchTypeahead(string searchTerm)
        {
            var result = await ctx.Users
                .Where(u => u.Name.Contains(searchTerm))
                .ProjectTo<BasicUser>()
                .ToListAsync();

            return OkResponse(result);
        }
    }
}
