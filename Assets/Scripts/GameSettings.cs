public static class GameSettings
{
    public static string TeamAName = "A Takýmý";
    public static string TeamBName = "B Takýmý";
    public static bool SelectedMode = true; // true = Klasik, false = Bomba
    public static bool IsTeamAStartingFirst;

    public enum BombStartingRule
    {
        Sequential,     // Sýrayla (A, B, A, B)
        FullRandom,     // Her tur tamamen rastgele
        LoserStarts,    // Bomba kimde patladýysa o baþlar (ilk tur random)
        Competitive     // Puaný geride olan baþlar
    }

}