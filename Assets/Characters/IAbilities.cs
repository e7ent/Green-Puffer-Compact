using System.ComponentModel;

namespace GreenPuffer.Characters
{
    public interface IAbilities : INotifyPropertyChanged
    {
        CharacterRank Rank { get; }
        float Hp { get; }
        float MaxHp { get; }
        float Exp { get; }
        float MaxExp { get; }
        float Strength { get; }
        float Armor { get; }
        float Luck { get; }
        float Speed { get; }
    }
}
