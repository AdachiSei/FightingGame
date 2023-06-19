/// <summary>
/// インターフェース
/// </summary>
public interface IActionable
{
    public bool IsPunching { get; }

    public void Idol();
    public void LeftPunch();
    public void RightPunch();
    public void Guard();
    public void Damage();
}
