/// <summary>
/// インターフェース
/// </summary>
public interface IActionable
{
    bool IsPunching { get; }

    void Init();
    void Idol();
    void LeftPunch();
    void RightPunch();
    void Guard();
    void Damage();
}
