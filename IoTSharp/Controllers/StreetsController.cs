using IoTSharp.Contracts;
using IoTSharp.Controllers.Models;
using IoTSharp.Data;
using IoTSharp.Dtos;
using IoTSharp.Extensions;
using IoTSharp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IoTSharp.Controllers
{
    /// <summary>
    /// 客户管理
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class StreetsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StreetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 获取指定租户下的所有街道
        /// </summary>
        /// <returns></returns>
        [HttpPost("Tenant/{tenantId}/All")]
        [Authorize(Roles = nameof(UserRole.NormalUser))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ApiResult<List<Street>>> GetAllStreets([FromRoute] Guid tenantId)
        {
            return new ApiResult<List<Street>>(ApiCode.Success, "OK", await _context.Street.Where(c => c.Tenant.Id == tenantId && c.Deleted == false).ToListAsync());
        }

        /// <summary>
        /// 获取指定租户下的所有街道
        /// </summary>
        /// <returns></returns>
        [HttpPost("Tenant")]
        [Authorize(Roles = nameof(UserRole.TenantAdmin))]
        [Authorize(Roles = nameof(UserRole.SystemAdmin))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ApiResult<PagedData<Street>>> GetStreets([FromBody] StreetParam m)
        {
            var profile = this.GetUserProfile();
            if (m.tenantId != Guid.Empty)
            {
                if (User.IsInRole(nameof(UserRole.SystemAdmin)))
                {
                    var querym = _context.Street.Include(c => c.Tenant).Where(c => !c.Deleted);
                    var data = await m.Query(querym, c => c.NeighName);
                    return new ApiResult<PagedData<Street>>(ApiCode.Success, "OK", data);
                }
                else
                {
                    var querym = _context.Street.Include(c => c.Tenant).Where(c => !c.Deleted && c.Tenant.Id == m.tenantId);
                    var data = await m.Query(querym, c => c.NeighName);
                    return new ApiResult<PagedData<Street>>(ApiCode.Success, "OK", data);
                }
            }
            else
            {

                return new ApiResult<PagedData<Street>>(ApiCode.NotFoundCustomer, "没有指定小区ID", new PagedData<Street>());
            }

        }

        /// <summary>
        /// 返回指定id的街道
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = nameof(UserRole.NormalUser))]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ApiResult<Street>> GetStreet(Guid id)
        {
            var street = await _context.Street.FindAsync(id);
            if (street == null)
            {
                return new ApiResult<Street>(ApiCode.NotFoundCustomer, "This street was not found", null);
            }
            return new ApiResult<Street>(ApiCode.Success, "OK", street);
        }

        /// <summary>
        /// 修改指定租户为 指定的信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="street"></param>
        /// <returns></returns>
        [Authorize(Roles = nameof(UserRole.CustomerAdmin))]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ApiResult<Street>> PutStreet(Guid id, StreetDto street)
        {
            if (id != street.Id)
            {
                return new ApiResult<Street>(ApiCode.InValidData, "InValidData", street);
            }
            if (street.TenantId != Guid.Empty)
            {
                street.Tenant = _context.Tenant.Find(street.TenantId);
            }
            _context.Entry(street).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();

                return new ApiResult<Street>(ApiCode.Success, "", street);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Street.Any(e => e.Id == id && e.Tenant.Id == street.TenantId))
                {
                    return new ApiResult<Street>(ApiCode.NotFoundCustomer, "This street was not found", street);
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// 为当前客户所在的租户新增小区
        /// </summary>
        /// <param name="street"></param>
        /// <returns></returns>
        [Authorize(Roles = nameof(UserRole.CustomerAdmin))]
        [HttpPost]
        public async Task<ApiResult<Street>> PostStreet(StreetDto street)
        {
            if (street.TenantId != Guid.Empty && (User.IsInRole(nameof(UserRole.SystemAdmin)) || User.IsInRole(nameof(UserRole.TenantAdmin))))
            {
                var tent = _context.Tenant.Find(street.TenantId);
                street.Tenant = tent;
            }
            else
            {
                var tid = User.Claims.First(c => c.Type == IoTSharpClaimTypes.Tenant);
                var tidguid = new Guid(tid.Value);
                var tent = _context.Tenant.Find(tidguid);
                street.Tenant = tent;
            }
            _context.Street.Add(street);
            await _context.SaveChangesAsync();

            return new ApiResult<Street>(ApiCode.Success, "Ok", await _context.Street.SingleOrDefaultAsync(c => c.Id == street.Id));
        }

        /// <summary>
        /// 删除指定的小区ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Customers/5
        [Authorize(Roles = nameof(UserRole.TenantAdmin))]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ApiResult<Street>> DeleteStreet(Guid id)
        {
            var street = await _context.Street.FindAsync(id);
            if (street == null)
            {
                return new ApiResult<Street>(ApiCode.NotFoundCustomer, "This street was not found", null);
            }
            street.Deleted = true;
            _context.Street.Update(street);
            await _context.SaveChangesAsync();
            return new ApiResult<Street>(ApiCode.Success, "Ok", street);
        }
    }
}