using Photon.Pun;
using UnityEngine;

namespace Ball
{
    public class MultiPlayBallMovement : BallMovement
    {
        protected override void Awake()
        {
            base.Awake();
            if (PhotonNetwork.CurrentRoom.PlayerCount != 2) return;
            OnGoal = () =>
            {
                SetRandomSpeed();
                SetDirection(GetRandomDirection());
            };
            SetDirection(Random.insideUnitCircle);
        }
    }
}