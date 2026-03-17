using QueryQuest.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryQuest.Core.Models
{
    public class ScoreHandler : IScoreHandler
    {
        private int _currentScore = 0;
        public int CurrentScore
        {
            get => _currentScore;
            set => _currentScore = value;
        }

        private int _streak = 0;
        public int Streak
        {
            get => _streak;
            set => _streak = value; 
        }
        private int _correctAnswerCount = 0;
        public int CorrectAnswerCount
        {
            get => _correctAnswerCount;
            set => _correctAnswerCount = value;
        }
        private int _highestStreakCount = 0;
        public int HighestStreakCount
        {
            get => _highestStreakCount;
            set => _highestStreakCount = value;
        }
        public void AddCorrectAnswer()
        {
            Streak++;
            CorrectAnswerCount++;
            if (Streak > HighestStreakCount)
            {
                HighestStreakCount = Streak;
            }
            CurrentScore += (1 * Streak);
        }
        public void HandleWrongAnswer()
        {
            Streak = 0;
        }
        public void Reset()
        {
            CurrentScore = 0;
            Streak = 0;
            HighestStreakCount = 0;
            CorrectAnswerCount = 0;
        }
    }
}
