namespace GreenPuffer.Characters
{
    public enum CharacterRank
    {
        Unknown = -1,
        C = 0,
        B = 1,
        A = 2,
        S = 3
    }

    public static class CharacterRankExtensions
    {
        public static float GetWeight(this CharacterRank rank)
        {
            float unit = 1.0f / 4;
            return unit + ((int)rank * unit);
        }
    }
}
