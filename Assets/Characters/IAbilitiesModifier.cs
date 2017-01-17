namespace GreenPuffer.Characters
{
    public interface IAbilitiesModifier
    {
        float Hp { get; set; }
        float MaxHp { get; set; }
        float Exp { get; set; }
        float Strength { get; set; }
        float Armor { get; set; }
        float Luck { get; set; }
        float Speed { get; set; }
    }
}
