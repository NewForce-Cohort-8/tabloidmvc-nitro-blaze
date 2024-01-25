

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System.Security.Claims;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class TagController : Controller
    {
        private readonly ITagRepository _tagRepository;

        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public IActionResult Index()
        {
            var tags = _tagRepository.GetAll();
            return View(tags);
        }

        //public IActionResult Details(int id)
        //{
        //    var tag = _tagRepository.GetPublishedTagById(id);
        //    if (tag == null)
        //    {
        //        int userId = GetCurrentUserProfileId();
        //        tag = _tagRepository.GetUserTagById(id, userId);
        //        if (tag == null)
        //        {
        //            return NotFound();
        //        }
        //    }
        //    return View(tag);
        //}

        //public IActionResult Create()
        //{
        //    var vm = new TagCreateViewModel();
        //    vm.TagOptions = _tagRepository.GetAll();
        //    return View(vm);
        //}

        [HttpPost]
        //public IActionResult Create(TagCreateViewModel vm)
        //{
        //    try
        //    {
        //        vm.Tag.CreateDateTime = DateAndTime.Now;
        //        vm.Tag.IsApproved = true;
        //        vm.Tag.UserProfileId = GetCurrentUserProfileId();

        //        _tagRepository.Add(vm.Post);

        //        return RedirectToAction("Details", new { id = vm.Tag.Id });
        //    }
        //    catch
        //    {
        //        vm.TagOptions = _tagRepository.GetAll();
        //        return View(vm);
        //    }
        //}

        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}