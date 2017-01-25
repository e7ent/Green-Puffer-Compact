using System;
using System.ComponentModel;
using UnityEngine;

namespace GreenPuffer.Characters
{
    [Serializable]
    class CharacterAbilities : IAbilities, IAbilitiesModifier
    {
        [SerializeField]
        private CharacterRank rank = CharacterRank.C;
        [SerializeField]
        private float hp = 10, maxHp = 10, exp = 0, maxExp = 10, strength = 0, armor = 0, luck = 0, speed = 0;

        public CharacterRank Rank { get { return rank; } }
        public float Hp { get { return hp; } set { hp = Mathf.Clamp(value, 0, MaxHp); Invoke("Hp"); } }
        public float MaxHp { get { return maxHp; } set { maxHp = value; Invoke("MaxHp"); } }
        public float Exp { get { return exp; } set { exp = value; Invoke("Exp"); } }
        public float MaxExp { get { return maxExp; } }
        public float Strength { get { return strength; } set { strength = value; Invoke("Strength"); } }
        public float Armor { get { return armor; } set { armor = value; Invoke("Armor"); } }
        public float Luck { get { return luck; } set { luck = value; Invoke("Luck"); } }
        public float Speed { get { return speed; } set { speed = value; Invoke("Speed"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Invoke(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
