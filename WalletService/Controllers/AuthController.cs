using Microsoft.AspNetCore.Mvc;
using WalletService.Dtos;
using WalletService.Utils;

namespace WalletService.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        public IActionResult Authenticate([FromBody] AuthDto account)
        {
            string hashedCode = HashValues.Compute(account.code);
            return Ok();
        }
    }

}