using GameModeSystem;
using Player.Data.Scores;
using TMPro;
using UnityEngine;

namespace UI.TextSetters
{
    public class BestScoreTextSetter : TextSetter
    {
        protected string LabelText { get; set; }

        protected override void Set()
        {
            Text.text = $"{LabelText}{ScoreData.BestScore}";
        }

        protected override void Subscribe(ScoreData scoreData)
        {
            scoreData.OnBestScoreChange += Set;
        }

        protected override void Unsubscribe(ScoreData scoreData)
        {
            scoreData.OnBestScoreChange -= Set;
        }
    }
}