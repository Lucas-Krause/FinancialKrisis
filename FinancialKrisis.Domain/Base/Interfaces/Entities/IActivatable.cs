namespace FinancialKrisis.Domain.Interfaces;

public interface IActivatable
{
    bool IsActive { get; }
    public void Deactivate();
}
