using System;
using Ball;
using Photon.Pun;
using Player.Data.Scores;
using ScreenSystem;

public class PhotonCallbacksManager : MonoBehaviourPunCallbacks
{
    public static event Action OnAnyLeftRoomAction;
    public static void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        ScoreReset();
        ScreenSwitcher.Instance.ShowScreen(ScreenType.Wait);
        OnAnyLeftRoomAction?.Invoke();
    }

    public override void OnLeftRoom()
    {
        ScoreReset();
        ScreenSwitcher.Instance.ShowScreen(ScreenType.Wait);
        PhotonNetwork.LoadLevel("MainMenu");
    }

    private void ScoreReset()
    {
        PlayerScore.Reset();
        BallCollisionDetector.GoalCount = 0;
    }
}