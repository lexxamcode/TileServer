using Gdd.Domain.Model.Requests;

namespace Gdd.Domain.Model.Rules.Base;

/// <summary>
/// Базовый абстрактный класс для правила
/// </summary>
public abstract class Rule
{
    /// <summary>
    /// Действие (Разрешить, запретить)
    /// </summary>
    public AccessPolicy AccessPolicy { get; set; }

    /// <summary>
    /// Метод проверки текущего запроса с помощью правила
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <returns>Соответствует ли запрос правилу</returns>
    public abstract Task<bool> CheckIfRequestMatchesRuleAsync(Request request);
}
