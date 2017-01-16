using Astro.Features.Effects;
using UnityEngine;

namespace GreenPuffer.Characters
{
    public interface ICharacter : IAffectable<IAbilitiesModifier>
    {
        bool IsAlive { get; }
        IAbilities Abilities { get; }

        void Move(Vector3 movement);
        void Attack(ICharacter target);
        void TakeDamage(float damage);
        void Kill();
    }
}
