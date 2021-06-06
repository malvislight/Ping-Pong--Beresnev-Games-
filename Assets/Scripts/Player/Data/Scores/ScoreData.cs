using System;
using Data;
using UnityEngine;

namespace Player.Data.Scores
{
    public class ScoreData
    {
        public ScoreData(SavesType savesType)
        {
            ScoreKey = SavingKeyProvider.GetKey(savesType);
        }

        public readonly string ScoreKey;

        public event Action OnScoreChange;
        public event Action OnBestScoreChange;
        public event Action OnReset;
        
        private int _score;
        public int Score
        {
            get => _score;
            set
            {
                if (value > BestScore)
                    BestScore = value;

                _score = value;
                OnScoreChange?.Invoke();
            }
        }
        
        private int _bestScore;
        public int BestScore
        {
            get => _bestScore;
            set
            {
                PlayerPrefs.SetInt(ScoreKey, value);
                _bestScore = value;
                OnBestScoreChange?.Invoke();
            }
        }

        public void Reset()
        {
            Score = 0;
            OnReset?.Invoke();
        }
        
        public void Increase()
        {
            Score++;
        }
    }
}