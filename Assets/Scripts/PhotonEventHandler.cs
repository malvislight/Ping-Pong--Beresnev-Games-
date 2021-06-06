using Ball;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Player.Data.Scores;

public class PhotonEventHandler : MonoBehaviourPunCallbacks, IOnEventCallback
{
    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == 1)
        {
            BallCollisionDetector.GoalCount++;
            PlayerScore.MultiPlayScoreData.Score += (bool)photonEvent.CustomData ? 0 : 1;
        }
    }
    
}