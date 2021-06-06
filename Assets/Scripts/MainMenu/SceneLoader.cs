using GameModeSystem;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private Button _multiPlayerButton;
        [SerializeField] private Button _singlePlayerButton;
        private void Start()
        {
            if(PhotonNetwork.IsConnected) 
                PhotonNetwork.Disconnect();
            
            _multiPlayerButton.onClick.AddListener(LoadMultiPlayerScene);
            _singlePlayerButton.onClick.AddListener(LoadSinglePlayerScene);
        }

        private static void LoadMultiPlayerScene()
        {
            GameMode.Mode = GameModeType.MultiPlay;
            SceneManager.LoadScene("Lobby");
        }
        
        private static void LoadSinglePlayerScene()
        {
            GameMode.Mode = GameModeType.SinglePlay;
            SceneManager.LoadScene("Single Player Game");
        }
    }
}