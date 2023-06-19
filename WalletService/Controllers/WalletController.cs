using System;
using System.Collections.Generic;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WalletService.Repositories;
using WalletService.Dtos;
using WalletService.Utils;
using WalletService.Models;
using WalletService.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace WalletService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletRepo repo;
        private readonly IMapper dbMapper;

        public WalletController(IWalletRepo repository, IMapper mapper)
        {
            repo = repository;
            dbMapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WalletMainDto>>> GetWalletsByAccount()
        {
            try
            {
                var accountID = User.FindFirst("accountid").Value;

                var wallet = await repo.GetOwnersWallets(accountID);
                return Ok(dbMapper.Map<IEnumerable<WalletMainDto>>(wallet));
            }
            catch (System.Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Internal Server Error");
            }
        }

        [Authorize]
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult<WalletMainDto>> GetWalletById([FromRoute] Guid id)
        {
            try
            {
                var accountID = User.FindFirst("accountid").Value;
                var wallet = await repo.GetOwnerWalletById(id, accountID);
                if (wallet == null) return NotFound();

                return Ok(dbMapper.Map<WalletMainDto>(wallet));
            }
            catch (System.Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Internal Server Error");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<WalletMainDto>> CreateWallet(WalletCreateDto payload)
        {
            try
            {
                var accountID = User.FindFirst("accountid").Value;
                string hashedNumber = HashValues.Compute(payload.AccountNumber);
                // Check uniqueness of accountNumber
                var walletExist = await repo.WalletsExist(hashedNumber);
                if (walletExist) return BadRequest(new { message = "A wallet with this account number already exist", errorCode = 400 });

                if (payload.Type == "card" && payload.AccountNumber.Length >= 6)
                    payload.AccountNumber = payload.AccountNumber.Substring(0, 6); // cut the first 6 digits

                // Get all wallets of account holder and make sure user does not have more than 5 wallets
                var total = await repo.TotalWalletsOwned(accountID);
                if (total >= 5) return BadRequest(new { message = "User has more than 5 wallets", errorCode = 400 });

                WalletInsertDto newPayload = new WalletInsertDto()
                {
                    Name = payload.Name,
                    Type = payload.Type,
                    AccountNumber = payload.AccountNumber,
                    AccountScheme = payload.AccountScheme,
                    AccountHash = hashedNumber,
                    Owner = accountID,
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Now
                };

                var newWallet = dbMapper.Map<Wallet>(newPayload);
                repo.CreateWallet(newWallet);
                var saved = repo.SaveChanges();
                if (!saved)
                {
                    return BadRequest();
                }

                var result = dbMapper.Map<WalletMainDto>(newWallet);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Internal Server Error");
            }
        }

        // ADMIN SPECIFIC ENDPOINTS
        [Authorize(Policy = IdentityData.AdminUserPolicyName)]
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<ActionResult> RemoveWallet([FromRoute] Guid id)
        {
            try
            {
                var done = await repo.DeleteWallet(id);
                if (done) return Ok();
                return NotFound();
            }
            catch (System.Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Internal Server Error");
            }
        }

        [Authorize(Policy = IdentityData.AdminUserPolicyName)]
        [HttpGet]
        [Route("admin")]
        public async Task<ActionResult<IEnumerable<WalletMainDto>>> GetWallets([FromQuery] WalletQueryDto query)
        {
            try
            {
                var wallet = await repo.GetWallets(query.page, query.pagesize);
                return Ok(dbMapper.Map<IEnumerable<WalletMainDto>>(wallet));
            }
            catch (System.Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Internal Server Error");
            }
        }

        [Authorize(Policy = IdentityData.AdminUserPolicyName)]
        [HttpGet]
        [Route("admin/{id:guid}")]
        public async Task<ActionResult<WalletMainDto>> GetWallet([FromRoute] Guid id)
        {
            try
            {
                var wallet = await repo.GetWalletById(id);
                if (wallet == null) return NotFound();
                return Ok(dbMapper.Map<WalletMainDto>(wallet));
            }
            catch (System.Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Internal Server Error");
            }
        }
    }
}