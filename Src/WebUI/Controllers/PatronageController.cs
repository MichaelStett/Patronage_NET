using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Application.Patronage.Queries.GetFile;
using Northwind.Application.Patronage.Commands.CreateFile;
using Northwind.Application.Patronage.Commands.UpdateFile;
using Northwind.Application.Patronage.Queries.GetLogs;
using Northwind.Application.Patronage.Queries.GetReversedString;
using System.Net.Http;
using System.Net.Mime;

namespace Northwind.WebUI.Controllers
{
    [Authorize]
    public class PatronageController : BaseController
    {
        /// <summary>
        /// Get file specified by name
        /// </summary>
        /// <returns> Requested file</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(200, Type = typeof(byte[]))]
        [ProducesResponseType(404, Type = null)]
        [ProducesResponseType(500)]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<byte[]>> GetFile(string name)
        {
            var data = await Mediator.Send(new GetFileQuery { Name = name });
            return File(data, MediaTypeNames.Text.Plain, name, true);
        }

        /// <summary>
        /// Create new file with specified name and content
        /// </summary>
        /// <returns>Nothing</returns>
        /// <response code="204">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = null)]
        [ProducesResponseType(500)]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> CreateFile(string name, string data)
        {
            var isSuccess = await Mediator.Send(new CreateFileCommand { Name = name, Data = data });

            if (!isSuccess)
                return BadRequest();

            return NoContent();
        }

        /// <summary>
        /// Update file with specified name with content
        /// </summary>
        /// <returns>Nothing</returns>
        /// <response code="204">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = null)]
        [ProducesResponseType(404, Type = null)]
        [ProducesResponseType(500)]
        [HttpPut]
        [AllowAnonymous]
        public async Task<ActionResult> UpdateFile(string name, string data)
        {
            var isSuccess = await Mediator.Send(new UpdateFileCommand { Name = name, Data = data });

            if (!isSuccess)
                return BadRequest();

            return NoContent();
        }

        /// <summary>
        /// Get logs
        /// </summary>
        /// <returns>Array of logs</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<string[]>> GetLogs()
        {
            return base.Ok(await Mediator.Send(new GetLogsQuery()));
        }

        /// <summary>
        /// Get reversed string
        /// </summary>
        /// <param name="text"></param>
        /// <returns>String</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<string>> GetReversedString(string text)
        {
            return base.Ok(await Mediator.Send(new GetReversedStringQuery { Text = text }));
        }
    }
}