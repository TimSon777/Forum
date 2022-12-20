using AutoMapper;
using LiWiMus.Core.Offices;
using LiWiMus.SharedKernel.Interfaces;
using LiWiMus.Web.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LiWiMus.Web.MVC.Areas.Company.Controllers;

[Area("Company")]
public class OfficesController : Controller
{
    private readonly IRepository<Office> _repository;
    private readonly IMapper _mapper;

    public OfficesController(IRepository<Office> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public IActionResult Offices()
    {
        return View();
    }
    public async Task<IActionResult> Index()
    {
        var offices = await _repository.ListAsync();
        var officesDtos = _mapper.MapList<Office, OfficeDto>(offices);
        return Json(officesDtos);
    }
}