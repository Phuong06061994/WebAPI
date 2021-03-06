﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Request;
using DAL.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utilities.Constant;
using Web.Models;
using Web.Models.News;
using Web.Service;

namespace Web.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsApiClient _newsApiClient;

        public NewsController(INewsApiClient newsApiClient)
        {
            _newsApiClient = newsApiClient;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _newsApiClient.GetAll();
            if(data == null)
            {
                if (User.Identity.IsAuthenticated){
                    TempData["message"] = "You are not authorization";
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Login", "User");
            }
            
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewsRequest model)
        {
            var userId = User.Claims.First(c => c.Type == SystemConstants.UserClaim.Id).Value;
            model.UserId = Guid.Parse(userId);

            var data = await _newsApiClient.Create(model);
            if(data == false)
            {
                return BadRequest("cap nhat khong thanh cong");
            }

            return RedirectToAction("Index","News");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var data = await _newsApiClient.GetById(id);
            if (data == null)
            {
                return BadRequest("Ban khong sua duoc");
            }
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NewsResponse model)
        {
            var data = await _newsApiClient.Update(model);
            if (data == false)
            {
                return BadRequest("cap nhat khong thanh cong");
            }
            
            return RedirectToAction("Index", "News");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _newsApiClient.Delete(id);
            if (data == false)
            {
                return BadRequest("Xoa khong thanh cong");
            }

            return RedirectToAction("Index", "News");
        }

    }
}