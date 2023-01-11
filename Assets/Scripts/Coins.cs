using TMPro;
using UnityEngine;
using YG;

public class Coins : MonoBehaviour
{
    [SerializeField]private TMP_Text coinsCountText;
    private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;
    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;
    private void Update()
    {
        coinsCountText.text = Resources.Coins.ToString();
    }

    private void Rewarded(int id)
    {
        PowerUps.AddToken();
        Debug.Log("Add Token");
    }

    public void OpenRewardedAd(int id)
    {
        YandexGame.RewVideoShow(id);
    }
}
