#region

using LiWiMus.Core.Settings;
using Microsoft.AspNetCore.Http;

#endregion

namespace LiWiMus.Web.Shared.Services.Interfaces;

public interface IFormFileSaver
{
    Task<string> SaveWithRandomNameAsync(IFormFile file, DataType type);
}