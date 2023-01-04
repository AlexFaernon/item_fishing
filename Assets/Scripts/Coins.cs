using TMPro;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField]private TMP_Text coinsCountText;
    private int coinsCount;
    private YandexSDK sdk;

    private void Start()
    {
        sdk = YandexSDK.instance;
        sdk.onRewardedAdReward += Reward;
        coinsCountText.text = coinsCount.ToString();
    }

    private void Reward(string placement)
    {
      if (placement == "coin")
      {
          coinsCount += 2;
          coinsCountText.text = coinsCount.ToString();
      }
    }
}
