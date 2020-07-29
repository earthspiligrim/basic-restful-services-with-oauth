using FirstRestApiWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FirstRestApiWebService.Controllers
{
    [Authorize]
    [RoutePrefix("api")]
    public class ListItemsController : ApiController
    {

        private static List<CustomListItem> _listItems { get; set; } = new List<CustomListItem>();


        private static void FillUpTheList()
        {
            _listItems.Add(new CustomListItem() { Id = 12, Text = "gopi" });
            _listItems.Add(new CustomListItem() { Id = 13, Text = "hello" });
            _listItems.Add(new CustomListItem() { Id = 14, Text = "sai" });
            _listItems.Add(new CustomListItem() { Id = 15, Text = "krishna" });
        }


        // GET api/<controller>
        [HttpGet]
        [Route("listitems")]
        public IEnumerable<CustomListItem> Get()
        {
            ListItemsController.FillUpTheList();

            return _listItems;
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("listitem/{id}")]
        public HttpResponseMessage Get(int id)
        {
            if (_listItems.FirstOrDefault(x => x.Id == id) != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, _listItems.FirstOrDefault(x => x.Id == id));
            }

            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("listitem/")]
        public HttpResponseMessage Post([FromBody]CustomListItem model)
        {
            if (string.IsNullOrEmpty(model?.Text))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            ListItemsController.FillUpTheList();
            var maxId = 0;
            if (_listItems.Count > 0)
            {
                maxId = _listItems.Max(x => x.Id);
            }
            model.Id = maxId + 1;
            _listItems.Add(model);
            return Request.CreateResponse(HttpStatusCode.Created, model);
        }

        // PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/<controller>/5
        [HttpGet]
        [Route("listitem/deletelistitem/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            var itemFound = _listItems.FirstOrDefault(x => x.Id == id);
            if (itemFound != null)
            {
                _listItems.Remove(itemFound);
                return Request.CreateResponse(HttpStatusCode.OK, itemFound);
            }

            //var couldNotDeleteObject = { ResponseMessage: "Could not find the item you wanted to delete"};
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }


        [HttpPut]
        [Route("listitem/updatelistitem")]
        public HttpResponseMessage Put(int id, [FromBody]CustomListItem model)
        {
            if(string.IsNullOrEmpty(model?.Text))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var itemFound = _listItems.FirstOrDefault(x => x.Id == id);
            if(itemFound != null)
            {
                itemFound.Text = model.Text;
                return Request.CreateResponse(HttpStatusCode.OK, itemFound);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}