namespace Fire_Emblem_View
{
    public interface IView
    {
        void AnnounceWinner(string winnerName);
        void ShowAttack(string attacker, string defender, int damage);
        void ShowHp(string characterName, int hp);
        void ShowSkills(string characterName, string[] skills);
        string AskUserForOption(string prompt, string[] options);
    }
}