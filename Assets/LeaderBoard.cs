using System.Collections;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class LeaderBoard : MonoBehaviour
{
    int leaderboradID = 10887;
    public TextMeshProUGUI playersNames;
    public TextMeshProUGUI playersScores;
    [SerializeField] GameObject mainMenuPanel;
    // Start is called before the first frame update

    public void CloseLeaderboard()
    {
        gameObject.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, leaderboradID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully uploaded score");
                done = true;
            }
            else
            {
                Debug.Log("Failed" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    public IEnumerator FetchTopHighscoresRoutine()
    {
        bool done = false;
        LootLockerSDKManager.GetScoreListMain(leaderboradID, 29, 0, (response) =>
        {
            if (response.success)
            {
                string tempPlayerNames = "Names\n";
                string tempPlayerScores = "Scores\n";

                LootLockerLeaderboardMember[] members = response.items;
                foreach (var member in members)
                {
                    tempPlayerNames += member.rank + ". ";
                    if (member.player.name != "")
                    {
                        tempPlayerNames += member.player.name;
                    }
                    else
                    {
                        tempPlayerNames += member.player.id;
                    }
                    tempPlayerScores += member.score + "\n";
                    tempPlayerNames += "\n";
                }
                done = true;
                playersNames.text = tempPlayerNames;
                playersScores.text = tempPlayerScores;

            }
            else
            {
                Debug.Log("Failed" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}
