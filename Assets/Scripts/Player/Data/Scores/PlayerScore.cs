using System;
using Ball;
using Data;
using GameModeSystem;
using UnityEngine;

namespace Player.Data.Scores
{
    public class PlayerScore : MonoBehaviour
    {
        public static readonly ScoreData SinglePlayScoreData = new ScoreData(SavesType.SinglePlayScore);
        public static readonly ScoreData MultiPlayScoreData = new ScoreData(SavesType.MultiPlayScore);

        private Action OnGoal;
        private void Awake()
        {
            SinglePlayScoreData.BestScore = PlayerPrefs.GetInt(SinglePlayScoreData.ScoreKey, 0);

            if(GameMode.Mode == GameModeType.MultiPlay)
                OnGoal = () => { SinglePlayScoreData.Score = 0; };
        }

        public static void Reset()
        {
            SinglePlayScoreData.Reset();
            MultiPlayScoreData.Reset();
        }

        private void OnEnable()
        {
            BallCollisionDetector.OnGoal += OnGoal;
        }

        private void OnDisable()
        {
            BallCollisionDetector.OnGoal -= OnGoal;
        }
    }
}