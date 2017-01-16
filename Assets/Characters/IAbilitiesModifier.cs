namespace GreenPuffer.Characters
{
    public interface IAbilitiesModifier
    {
        float Hp { set; }
        float MaxHp { set; }
        float Strength { set; }
        float Armor { set; }
        float Luck { set; }
        float Speed { set; }
    }
}
