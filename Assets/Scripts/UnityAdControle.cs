using System;
using UnityEngine;

#if UNITY_ADS
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
#endif

/// <summary>
/// Classe responsavel por gerenciar todos os anuncios (Ads) do jogo.
/// </summary>
public class UnityAdControle : MonoBehaviour
{

    public static bool showAds = true;

    private static string sceneName = "cena1";

    public static DateTime? nextAdsReward = null;

    /// <summary>
    /// Metodo responsavel por mostrar o anuncio (Ads). Esse metodo deve ser chamado no local que deseja ter anuncio.
    /// </summary>
    public static void ShowAd()
    {
#if UNITY_ADS
        ShowOptions opcoes = new ShowOptions();
        opcoes.resultCallback = unPause;
        if (Advertisement.IsReady())
        {
            //Mostra o anuncio - Ads
            Advertisement.Show(opcoes);
        }
#endif
    }

#if UNITY_ADS
    public static void unPause(ShowResult result)
    {
        Time.timeScale = 1;
    }

    public static void showRewardAd()
    {
        sceneName = PlayerPrefs.GetString("sceneName");
        if("" == sceneName)
        {
            sceneName = "cena1";
        }
        nextAdsReward = DateTime.Now.AddSeconds(90);
        if (Advertisement.IsReady())
        {
            ShowOptions opcoes = new ShowOptions
            {
                resultCallback = ResulManipulation
            };
            //Mostra o anuncio - Ads
            Advertisement.Show(opcoes);
        }
    }

    public static void ResulManipulation(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                // Seta PlayerPrefs, para indicar que que tem que spawar o player no local correto
                PlayerPrefs.SetString("respaw", "respaw");
                // Carrega a cena com base no nome.
                SceneManager.LoadScene(sceneName);
                break;

            case ShowResult.Skipped:
                Debug.Log("HA HA");
                break;

            case ShowResult.Failed:
                Debug.Log("HA HA");
                break;
        }
        Time.timeScale = 1;
    }
#endif
}
