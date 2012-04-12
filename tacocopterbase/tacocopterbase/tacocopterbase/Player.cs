using System;

namespace tacocopterbase
{
    public class Player
    {
        int score;

        public Player()
        {
            score = 0;
        }

        public int getScore()
        { return score; }

        public void setScore(int n)
        { score = n; }


    }
}