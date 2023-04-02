using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UrlShorteningService.API.Configuations;
using UrlShorteningService.API.Models;
using UrlShorteningService.Application.Interfaces;
using UrlShorteningService.Domain.Common;
using UrlShorteningService.Domain.DTOs;
using UrlShorteningService.Persistence.UnitOfWork;

namespace UrlShorteningService.API.Controllers
{
    [Route("")]
    public class LinkController : Controller
    {
        private readonly ILinkService _linkService;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public LinkController(
            ILinkService linkService,
            IConfiguration configuration,
            IUnitOfWork unitOfWork
        )
        {
            _linkService = linkService ?? throw new ArgumentNullException(nameof(linkService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        [HttpGet("{shortenedUrl}")]
        public async Task<IActionResult> RedirectToOrigin(string shortenedUrl)
        {
            if (string.IsNullOrEmpty(shortenedUrl))
                return BadRequest(GenericResponse<NoDataDto>.Fail("Shortened Url address should not be empty.", 404));

            if (shortenedUrl.Length > 6)
                return BadRequest(GenericResponse<NoDataDto>.Fail("Hash portion must be less and equal to 6 character.", 404));


            shortenedUrl = $"{_configuration["SiteUrl"]}/{shortenedUrl}";
            var record = await _linkService.GetPerfectUrl(shortenedUrl);

            if (record == null)
                return BadRequest(GenericResponse<NoDataDto>.Fail("Couldnt find perfect url.", 404));

            return Redirect(new Uri(record.PerfectUrl).AbsoluteUri);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ShorteningRequestModel requestModel)
        {
            if (string.IsNullOrEmpty(requestModel.PerfectUrl))
                return BadRequest(GenericResponse<NoDataDto>.Fail("Perfect Url address should not be empty.", 404));

            if (!requestModel.PerfectUrl.IsWellFormedUri())
                return BadRequest(GenericResponse<NoDataDto>.Fail("Perfect Url address is not well formatted.", 404));

            var shortenedLink = await _linkService.GetShortenedUrl(requestModel.PerfectUrl);

            if (shortenedLink != null)
            {
                return Ok(GenericResponse<LinkDto>.Success(shortenedLink, 200));
            }

            var generatedShortedUrl = $"{_configuration["SiteUrl"]}/{UrlExtensions.GenerateShortCode(6)}";
            LinkDto isExist = null;
            do
            {
                isExist = await _linkService.GetPerfectUrl(generatedShortedUrl);
                if (isExist != null)
                    generatedShortedUrl = $"{_configuration["SiteUrl"]}/{UrlExtensions.GenerateShortCode(6)}";
            }
            while (isExist != null);

            shortenedLink = new LinkDto
            {
                PerfectUrl = requestModel.PerfectUrl,
                ShortenedUrl = generatedShortedUrl,
                Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                CreateDate = DateTime.Now,
                Deleted = false,
                DeleteDate = null
            };

            shortenedLink = await _linkService.AddLink(shortenedLink);

            _unitOfWork.Commit();

            return Ok(GenericResponse<LinkDto>.Success(shortenedLink, 201));
        }

        [HttpPost("custom")]
        public async Task<IActionResult> Post([FromBody] CustomShorteningRequestModel requestModel)
        {
            if (string.IsNullOrEmpty(requestModel.PerfectUrl))
                return BadRequest(GenericResponse<NoDataDto>.Fail("Perfect Url address should not be empty.", 404));

            if (!requestModel.PerfectUrl.IsWellFormedUri())
                return BadRequest(GenericResponse<NoDataDto>.Fail("Perfect Url address is not well formatted.", 404));

            if (requestModel.ShortenedHashPortion.Length > 6)
                return BadRequest(GenericResponse<NoDataDto>.Fail("Hash portion must be less and equal to 6 character.", 404));

            var shortenedLink = await _linkService.GetShortenedUrl(requestModel.PerfectUrl);

            if (shortenedLink != null)
            {
                return Ok(GenericResponse<LinkDto>.Success(shortenedLink, 200));
            }

            var generatedShortedUrl = $"{_configuration["SiteUrl"]}/{requestModel.ShortenedHashPortion}";
            LinkDto isExist = await _linkService.GetPerfectUrl(generatedShortedUrl); ;

            if (isExist != null)
                return BadRequest(GenericResponse<NoDataDto>.Fail("Hash portion already registred with another perfect url. Please try with another.", 404));

            shortenedLink = new LinkDto
            {
                PerfectUrl = requestModel.PerfectUrl,
                ShortenedUrl = generatedShortedUrl,
                Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                CreateDate = DateTime.Now,
                Deleted = false,
                DeleteDate = null
            };

            shortenedLink = await _linkService.AddLink(shortenedLink);

            _unitOfWork.Commit();

            return Ok(GenericResponse<LinkDto>.Success(shortenedLink, 201));
        }

    }
}

