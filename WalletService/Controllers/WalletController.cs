using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WalletService.Repositories;
using WalletService.Dtos;
using WalletService.Utils;
using WalletService.Models;
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

        [HttpGet]
        public ActionResult<IEnumerable<WalletInsertDto>> GetWallets()
        {
            var wallet = repo.GetWallets();
            return Ok(dbMapper.Map<IEnumerable<WalletMainDto>>(wallet));
        }

        [HttpGet]
        [Route("{id:guid}")]
        public ActionResult<WalletMainDto> GetWalletById([FromRoute] Guid id)
        {
            var wallet = repo.GetWalletById(id);
            if (wallet == null) return NotFound();

            return Ok(dbMapper.Map<WalletMainDto>(wallet));
        }

        [HttpPost]
        public async Task<ActionResult<WalletMainDto>> CreateWallet(WalletCreateDto payload)
        {
            try
            {
                string hashedNumber = new HashValues(payload.AccountNumber).Compute();
                // Check uniqueness of accountNumber
                var walletExist = await repo.WalletsExist(hashedNumber);
                if (walletExist) return BadRequest(new { message = "A wallet with this account number already exist", errorCode = 400 });

                if (payload.Type == "card" && payload.AccountNumber.Length >= 6)
                    payload.AccountNumber = payload.AccountNumber.Substring(0, 6); // cut the first 6 digits

                // Get all wallets of account holder and make sure user does not have more than 5 wallets
                var total = repo.TotalWalletsOwned(payload.Owner);
                if (total >= 5) return BadRequest(new { message = "User has more than 5 wallets", errorCode = 400 });

                WalletInsertDto newPayload = new WalletInsertDto()
                {
                    Name = payload.Name,
                    Type = payload.Type,
                    AccountNumber = payload.AccountNumber,
                    AccountScheme = payload.AccountScheme,
                    AccountHash = hashedNumber,
                    Owner = payload.Owner,
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
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message, errorCode = 400 });
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public ActionResult RemoveWallet([FromRoute] Guid id)
        {
            try
            {
                var done = repo.DeleteWallet(id);
                return Ok(done);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message, ErrorCode = 400 });
            }
        }
    }
}