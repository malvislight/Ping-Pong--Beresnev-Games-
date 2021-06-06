using Player.Data.Scores;

namespace UI.TextSetters
{
    public class ScoreTextSetter : TextSetter
    {
        protected override void Set()
        {
            Text.text = $"{ScoreData.Score}";
        }

        protected override void Subscribe(ScoreData scoreData)
        {
            scoreData.OnScoreChange += Set;
        }

        protected override void Unsubscribe(ScoreData scoreData)
        {
            scoreData.OnScoreChange -= Set;
        }
    }
}