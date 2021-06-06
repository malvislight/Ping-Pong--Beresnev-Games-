using GameModeSystem;
using Player.Data.Scores;
using TMPro;
using UnityEngine;

namespace UI.TextSetters
{
    public abstract class TextSetter : MonoBehaviour
    {
        protected ScoreData ScoreData;
        protected TextMeshProUGUI Text;
        private void Awake()
        {
            Text = GetComponent<TextMeshProUGUI>();

            ScoreData = GameMode.Mode == GameModeType.SinglePlay 
                ? PlayerScore.SinglePlayScoreData 
                : PlayerScore.MultiPlayScoreData;
        }
        
        private void OnEnable()
        {
            Subscribe(ScoreData);
        }

        private void Start()
        {
            Set();
        }

        protected abstract void Set();
        protected abstract void Subscribe(ScoreData scoreData);
        protected abstract void Unsubscribe(ScoreData scoreData);

        private void OnDisable()
        {
            Unsubscribe(ScoreData);
        }
    }
}