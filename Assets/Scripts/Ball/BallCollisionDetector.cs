using System;
using Player.Data.Scores;
using ScreenSystem;
using UnityEngine;
using Utils.CameraBorder;

namespace Ball
{
    public class BallCollisionDetector : MonoBehaviour
    {
        public static event Action OnGoal;
        public static event Action OnPlayer;
        public static int GoalCount { get; set; }
        protected static bool OnSelfGoal { get; set; }

        private void Awake()
        {
            OnPlayer += PlayerScore.SinglePlayScoreData.Increase;
            OnGoal += PlayerScore.SinglePlayScoreData.Reset;
        }
        
        protected static void InvokeOnGoal()
        {
            OnGoal?.Invoke();
        }

        protected static bool CheckGoal(Transform other, out CameraBorder cameraBorder)
        {
            if(other.transform.TryGetComponent(out cameraBorder) == false) 
                return false;
            
            return cameraBorder.SideType == SideType.Top || cameraBorder.SideType == SideType.Buttom;
        }
        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.GetComponent<Player.Player>() != null) 
                OnPlayer?.Invoke();
            
            if (CheckGoal(other.transform, out var cameraBorder) == false) return;
                
            InvokeOnGoal();
            ScreenSwitcher.Instance.ShowScreen(ScreenType.Start);
        }

        private void OnDisable()
        {
            OnPlayer -= PlayerScore.SinglePlayScoreData.Increase;
        }
    }
}