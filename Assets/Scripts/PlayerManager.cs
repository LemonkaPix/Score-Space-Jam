using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using LootLocker.Requests;
using TMPro;
using UnityEngine.XR;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] AudioMixer[] audioMixers;
    [SerializeField] LeaderBoard leaderboard;
    public TMP_InputField playerName;

    void Start()
    {
        float volume = PlayerPrefs.GetFloat("Volume");
        foreach (var mixer in audioMixers)
        {
            mixer.SetFloat("Volume", volume);
        }
        if (playerName != null) playerName.text = PlayerPrefs.GetString("Nick");
        StartCoroutine(SetUpRoutine());
    }
    public void SetPlayerName()
    {
        LootLockerSDKManager.SetPlayerName(playerName.text, (response) =>
        {
            if (response.success)
            {
                PlayerPrefs.SetString("Nick", playerName.text);
                Debug.Log("Succesfully set player name");
            }
            else
            {
                Debug.Log("Could not set player name" + response.Error);
            }
        });
    }
    IEnumerator SetUpRoutine()
    {
        yield return LoginRoutine();
        yield return leaderboard.FetchTopHighscoresRoutine();
    }

    IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Player was logged in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("Could not start session");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}
