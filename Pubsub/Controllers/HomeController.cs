using Google.Cloud.PubSub.V1;
using Google.Protobuf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pubsub.ViewModels;
using System;
using System.Collections.Generic;

namespace Pubsub.Controllers
{
    public class HomeController : Controller
    {
        static int totalCronCount = 0;
        static int messageCount = 0;

        readonly PubsubOptions _options;
        readonly PublisherClient _publisher;

        public HomeController(IOptions<PubsubOptions> options,
            PublisherClient publisher)
        {
            _options = options.Value;
            _publisher = publisher;
        }

        [HttpGet]
        public string Messages()
        {
            Random r = new Random();
            int messageAmount = r.Next(1, 3);
            for (int l = 0; l < messageAmount; l++)
            {
                var pubsubMessage = new PubsubMessage()
                {
                    Data = ByteString.CopyFromUtf8(r.Next(0, 100).ToString())
                };
                pubsubMessage.Attributes["token"] = _options.VerificationToken;
                _publisher.PublishAsync(pubsubMessage);
            }

            totalCronCount = totalCronCount + 1;
            messageCount = messageCount + messageAmount;

            return "example";
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var model = new MessageList();
            if (!_options.HasGoodProjectId())
            {
                model.MissingProjectId = true;
                return View(model);
            }

            model.CronCallCounter = totalCronCount;
            model.CronMessageCounter = messageCount;
            return View(model);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
