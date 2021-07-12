using AutoMapper;
using Core;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Service;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigurationManagerUI.Models;

namespace ConfigurationManagerUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMongoDbService<ConfigurationModel> _mongoDbService;
        private readonly IMapper _mapper;

        public HomeController(IMongoDbService<ConfigurationModel> mongoDbService, IMapper mapper)
        {
            _mongoDbService = mongoDbService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var filter = Builders<ConfigurationModel>.Filter.Empty;
            var configs = await _mongoDbService.GetAll(filter);

            return View(_mapper.Map<IEnumerable<ConfigurationViewModel>>(configs));
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ConfigurationViewModel config)
        {
            await _mongoDbService.Add(_mapper.Map<ConfigurationModel>(config));

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(string configId)
        {
            var filter = Builders<ConfigurationModel>.Filter.Eq(x => x.Id, configId);
            var config = await _mongoDbService.Get(filter);

            return View(_mapper.Map<ConfigurationViewModel>(config));
        }

        [HttpPost]
        public async Task<IActionResult> Update(ConfigurationViewModel config)
        {
            var filter = Builders<ConfigurationModel>.Filter.Eq(x => x.Id, config.Id);
            await _mongoDbService.Update(filter, _mapper.Map<ConfigurationModel>(config));

            return RedirectToAction("Index");
        }
    }
}
