using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WalletService.Repositories;
using WalletService.Dtos;
using WalletService.Models;
using System.Collections.Generic;
using System;

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
        public ActionResult<IEnumerable<WalletMainDto>> GetWallets()
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
        public ActionResult<WalletMainDto> CreateWallet(WalletCreateDto payload)
        {
            try
            {
                WalletMainDto newPayload = new WalletMainDto()
                {
                    Name = payload.Name,
                    Type = payload.Type,
                    AccountNumber = payload.AccountNumber,
                    AccountScheme = payload.AccountScheme,
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
                return BadRequest(new { ErrorMessage = ex.Message, ErrorCode = 400 });
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