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
    [Header("ADS variables")]
    [Tooltip("Variavel de verificação se pode ou não mostrar o anuncio")]
    public static bool showAds = true;

    [Tooltip("Tempo entre um anuncio e outro")]
    public static DateTime? nextAdsReward = null;

    /// <summary>
    /// Nome da cena a ser carregada
    /// </summary>
    private static string sceneName = "cena1";

    /// <summary>
    /// Metodo responsavel por mostrar o anuncio (Ads). Esse metodo deve ser chamado no local que deseja ter anuncio.
    /// </summary>
    public static void ShowAd()
    {
        // Verificação se os anuncios estão habilitados.
#if UNITY_ADS
        // Inicialização das opções.
        ShowOptions opcoes = new ShowOptions();
        // Seta o callback com a função unPause.
        opcoes.resultCallback = unPause;
        // Verifica se os anuncios estão prontos para serem mostrados.
        if (Advertisement.IsReady())
        {
            //Mostra o anuncio - Ads.
            Advertisement.Show(opcoes);
        }
#endif
    }

    // Verificação se os anuncios estão habilitados.
#if UNITY_ADS
    /// <summary>
    /// Função para despausar o jogo apos a execução do anuncio.
    /// </summary>
    /// <param name="_"> Varialvel passada por padrão, não sera usada nesta parte</param>
    public static void unPause(ShowResult _)
    {
        // Seta o time scale para um, para voltar a execução do jogo.
        Time.timeScale = 1;
    }

    /// <summary>
    /// Função para mostrar um anuncio com recompensa.
    /// </summary>
    public static void showRewardAd()
    {
        // Pega o nome da cena a ser carregada salva.
        sceneName = PlayerPrefs.GetString("sceneName");
        // Verifica se tem alguma cena salva.
        if ("" == sceneName)
        {
            // Caso não tenha nada, seta por padrão a cena1.
            sceneName = "cena1";
        }
        // Seta o tempo de espera para o proximo anuncio com recompensa.
        // Com 90 secondos de espera
        nextAdsReward = DateTime.Now.AddSeconds(90);
        // Verifica se o anuncio esta pronto para ser executado
        if (Advertisement.IsReady())
        {
            // Cria as opções do anuncio.
            ShowOptions opcoes = new ShowOptions
            {
                // Seta a função ResulManipulation como execução do callback.
                resultCallback = ResulManipulation
            };
            //Mostra o anuncio - Ads.
            Advertisement.Show(opcoes);
        }
    }

    /// <summary>
    /// Função para manipulação do callback dos anuncios
    /// </summary>
    /// <param name="result">Opções com base no que o usuario escolheu</param>
    public static void ResulManipulation(ShowResult result)
    {
        // Fara a "Escolha" das opções do resultado do anuncio.
        switch (result)
        {
            // Se o usuario viu todo o anuncio, ganha a recompensa.
            case ShowResult.Finished:
                // Seta PlayerPrefs, para indicar que que tem que spawar o player no local correto
                PlayerPrefs.SetString("respaw", "respaw");
                // Carrega a cena com base no nome.
                SceneManager.LoadScene(sceneName);
                break;

            // Se o usuario pulou o anuncio não faz nada.
            case ShowResult.Skipped:
            // Se falhou o anuncio não faz nada.
            case ShowResult.Failed:
                Debug.Log("Do nothing!");
                break;
        }
        // Seta o time scale para 1, voltando o jogo a execução normal.
        Time.timeScale = 1;
    }
#endif
}
