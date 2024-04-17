using System.Collections;
using System.Collections.Generic;

public class Achivement
{
    public int diamonds;
    public int kills;
    public int score;
    public int timeRemain;
    public int appleCount;
    public int bananaCount;
    public int melonCount;
    public int GetBonusScore() => timeRemain * 100;
    public int GetFinalScore() => score + GetBonusScore();
}
