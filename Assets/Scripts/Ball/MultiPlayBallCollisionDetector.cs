using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Player;
using Player.Data.Scores;
using UnityEngine;

namespace Ball
{
    public class MultiPlayBallCollisionDetector : BallCollisionDetector
    {
        private void Awake()
        {
            OnGoal += SendEvent;
        }

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            if (CheckGoal(other.transform, out var cameraBorder) == false) return;
            
            OnSelfGoal = cameraBorder.SideType == PlayerGenerator.CurrentPlayer.SideType;
            
            GoalCount++;
            InvokeOnGoal();
        }

        private static void SendEvent()
        {
            PlayerScore.MultiPlayScoreData.Score += OnSelfGoal ? 1 : 0;
            PhotonNetwork.RaiseEvent(1, OnSelfGoal, RaiseEventOptions.Default, SendOptions.SendReliable);
        }

        private void OnDisable()
        {
            OnGoal -= SendEvent;
        }
    }
}