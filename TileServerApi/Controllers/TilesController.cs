﻿using Microsoft.AspNetCore.Mvc;
using TileServerApi.Model;

namespace TileServerApi.Controllers;

/// <summary>
/// Контроллер для получения тайлов по их координатам
/// </summary>
/// <param name="tileRepository">Сервис работы с тайлами</param>
[ApiController]
[Route("[controller]")]
public class TilesController(ITileRepository tileRepository) : ControllerBase
{
    private readonly ITileRepository _tileRepository = tileRepository;

    /// <summary>
    /// Метод получения тайла по его координатам
    /// </summary>
    /// <param name="z">Координата z (приближение)</param>
    /// <param name="x">Координата x</param>
    /// <param name="y">Координата y</param>
    /// <returns>Изображение тайла</returns>
    [HttpGet("{z}/{x}/{y}")]
    public ActionResult GetTile(string z, string x, string y)
    {
        return _tileRepository.GetTile(z, x, y);
    }
}
