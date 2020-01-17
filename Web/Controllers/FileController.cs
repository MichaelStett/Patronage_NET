using System;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Patronage_NET.Web.Controllers.Help;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Patronage_NET.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Patronage_NET.Application.Files.Queries.GetFile;

namespace Patronage_NET.Web.Controllers
{
    // TODO: [Authorize]
    public class FileController : BaseController
    {
        private readonly IHelper _helper;

        public FileController(IHelper helper)
        {
            _helper = helper;
        }

        /// <summary>
        /// Get file sepcified by name
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns> Requested file</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(200, Type = typeof(PhysicalFileResult))]
        [ProducesResponseType(404, Type = null)]
        [ProducesResponseType(500)]
        [HttpGet]
        public async Task<ActionResult<PhysicalFileResult>> Get(string name)
        {
            var res = await Mediator.Send(new GetFileQuery { FileName = name });
            return Ok(res.Result);
        }

        /// <summary>
        /// Create new file with specified name and content
        /// </summary>
        /// <param name="myfile"></param>
        /// <returns>Nothing</returns>
        /// <response code="204">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = null)]
        [ProducesResponseType(500)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Myfile myfile)
        {
            var name = myfile.Name;
            var content = myfile.Content;

            var path = _helper.GetPath();

            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            if (!_helper.IsContentValid(content))
            {
                throw new Exception(HttpStatusCode.BadRequest.ToString());
            }

            var filepath = System.IO.Path.Combine(path, name);

            await System.IO.File.WriteAllTextAsync(filepath, content);

            return NoContent();
        }

        /// <summary>
        /// Update file with specified name with content
        /// </summary>
        /// <param name="myfile"></param>
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
        public async Task<IActionResult> Put([FromBody]Myfile myfile)
        {
            var name = myfile.Name;
            var content = myfile.Content;

            var path = _helper.GetPath();

            var filepath = System.IO.Path.Combine(path, name);

            if (!System.IO.File.Exists(filepath))
            {
                throw new Exception(HttpStatusCode.BadRequest.ToString());
            }

            if (!_helper.IsContentValid(content))
            {
                throw new Exception(HttpStatusCode.BadRequest.ToString());
            }

            var newpath = _helper.GetNewPath(filepath, name, content);

            await System.IO.File.AppendAllTextAsync(newpath, Environment.NewLine + content);

            return NoContent();
        }
    }
}
