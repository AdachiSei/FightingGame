/// <summary>
/// インターフェース
/// </summary>
public interface IActionable
{
    bool IsPunching { get; }
    int HP { get; }

    void Init();
    void Idol();
    void LeftPunch();
    void RightPunch();
    void Guard();
    void AddForce(bool _isDamege = true);
    void SetHP(int hp);
    void Dead();
}
