﻿using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ReportingService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiControllerBase : ControllerBase
    {
        private ISender _sender = null!;

        protected ISender Sender => _sender ??=
            HttpContext.RequestServices.GetService<ISender>()!;
    }
}