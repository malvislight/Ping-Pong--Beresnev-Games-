using GameModeSystem;
using Player.Data.Scores;
using TMPro;
using UnityEngine;

namespace UI.TextSetters
{
    public class BestScoreTextSetterExtention : BestScoreTextSetter
    {
        [SerializeField] private GameModeType _gameModeType;
        [SerializeField] private bool _saveLabelText;
        private void Awake()
        {
            Text = GetComponent<TextMeshProUGUI>();

            if(_saveLabelText)
                LabelText = Text.text;
            
            ScoreData = _gameModeType == GameModeType.SinglePlay 
                ? PlayerScore.SinglePlayScoreData 
                : PlayerScore.MultiPlayScoreData;
        }
    }
}