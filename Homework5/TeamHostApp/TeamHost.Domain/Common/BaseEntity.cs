using System.ComponentModel.DataAnnotations.Schema;
using TeamHost.Domain.Common.Interfaces;

namespace TeamHost.Domain.Common;

/// <summary>
/// Базовая сущность
/// </summary>
public abstract class BaseEntity : IEntity
{
    private readonly List<BaseEvent> _domainEvents = new();
 
    /// <summary>
    /// Id сущности
    /// </summary>
    public int Id { get; set; }
 
    /// <summary>
    /// Доменные события
    /// </summary>
    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();
 
    /// <summary>
    /// Добавить доменное событие
    /// </summary>
    /// <param name="domainEvent">Новое доменное событие</param>
    public void AddDomainEvent(BaseEvent domainEvent) => _domainEvents.Add(domainEvent);
    
    /// <summary>
    /// Удалить доменное событие
    /// </summary>
    /// <param name="domainEvent">Событие</param>
    public void RemoveDomainEvent(BaseEvent domainEvent) => _domainEvents.Remove(domainEvent);
    
    /// <summary>
    /// Удалить все доменные события
    /// </summary>
    public void ClearDomainEvents() => _domainEvents.Clear();
}