using Ball;
using Player.Data.Scores;

namespace UI.TextSetters
{
    public class EnemyScoreTextSetter : TextSetter
    {
        protected override void Set()
        {
            Text.text = $"{MultiPlayBallCollisionDetector.GoalCount - ScoreData.Score}";
        }

        protected override void Subscribe(ScoreData scoreData)
        {
            scoreData.OnScoreChange += Set;
            scoreData.OnReset += Reset;
        }

        protected override void Unsubscribe(ScoreData scoreData)
        {
            scoreData.OnScoreChange -= Set;
            scoreData.OnReset += Reset;
        }

        private void Reset()
        {
            Text.text = "0";
        }
    }
}