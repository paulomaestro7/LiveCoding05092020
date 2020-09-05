using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Live.Coding.Caqui.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Live.Caqui.WebApi.Controllers
{
    [ApiController]
    [Route("Satisfation")]
    public class SatisfationController : ControllerBase
    {
        private const string Error = "Algo deu errado";
        private readonly ILogger<SatisfationController> _logger;

        private static List<UserModel> _listUser = new List<UserModel>();
        private static List<SatisfactionModel> _listSatisfaction = new List<SatisfactionModel>()
        {
            new SatisfactionModel(){
                Description = "Muito Satisfeito",
                Count = 0
            },
            new SatisfactionModel(){
                Description = "Satisfeito",
                Count = 0
            },
            new SatisfactionModel(){
                Description = "Razoavelmente Satisfeito",
                Count = 0
            },
            new SatisfactionModel(){
                Description = "Pouco Satisfeito",
                Count = 0
            },
            new SatisfactionModel(){
                Description = "Insatisfeito",
                Count = 0
            }
        };



        public SatisfationController(ILogger<SatisfationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("GetUser")]
        public async Task<ActionResult> GetUser(string User, string Password)
        {
            try
            {
                var hash = "";

                var user = _listUser.Where(x => x.Login == User && x.Password == Password).FirstOrDefault();

                if (user != null)
                    hash = Util.CreateMD5(User + Password);


                return Ok(hash);
            }
            catch (Exception ex)
            {
                return BadRequest(Error);
            }
        }
        [HttpPost]
        [Route("PostUser")]
        public async Task<ActionResult> PostUser([FromBody] UserModel User)
        {
            try
            {
                var result = true;

                var user = _listUser.Where(x => x.Login == User.Login && x.Password == User.Password).Any();

                if (user)
                {
                    result = false;
                }
                else
                {
                    User.HashUser = Util.CreateMD5(User.Login + User.Password);
                    _listUser.Add(User);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(Error);
            }
        }

        [HttpGet]
        [Route("GetSatisfaction")]
        public async Task<ActionResult> GetSatisfaction(string Hash)
        {
            try
            {
                var result = new List<SatisfactionModel>();
                var user = _listUser.Where(x => x.HashUser == Hash).Any();
                if (user)
                {
                    result = _listSatisfaction;
                }
                else
                {
                    Unauthorized();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(Error);
            }
        }
        [HttpPost]
        [Route("PostSatisfaction")]
        public async Task<ActionResult> PostSatisfaction([FromBody] SatisfactionModel Satisfaction)
        {
            try
            {
                var user = _listUser.Where(x => x.HashUser == Satisfaction.HashUser).Any();

                if (user)
                {
                    _listSatisfaction.Where(x => x.Description == Satisfaction.Description)
                                     .Select(s => {
                                         s.Count++;
                                         return s;
                                     }).ToList();
                }
                else
                {
                    Unauthorized();
                }
                return Ok(_listSatisfaction);
            }
            catch (Exception ex)
            {
                return BadRequest(Error);
            }
        }
    }
}
