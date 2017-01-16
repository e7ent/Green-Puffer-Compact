using System;

namespace GreenPuffer.Characters
{
    class AICharacterEventArgs : EventArgs
    {
        public PlayerCharacter Player { get; private set; }

        public AICharacterEventArgs(PlayerCharacter player)
        {
            this.Player = player;
        }
    }
}
