using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

#if UNITY_ADS
using UnityEngine.Advertisements;
#endif

/// <summary>
/// Classe responsavel pelas ações dos botões
/// </summary>
public class Buttons : MonoBehaviour
{
    [Tooltip("'Modal' Com as opção de continue, restart e menu")]
    public GameObject pausedScreen;

    /// <summary>
    /// Metodo default do unity, executa antes das demais funções.
    /// </summary>
    void Start()
    {
        // Verificação se os anuncios estão habilitados.
        // Caso esteja habilitado inicializa com a lisçensa a baixo para IOS.
#if UNITY_ADS
        // Verifica se é despositivo IOS ou Android
#if UNITY_IOS
            Advertisement.Initialize("3654750", true);
#elif UNITY_ANDROID
            Advertisement.Initialize("3654751", true);
#else
        // Inicializa com o ID padrão.
        // Com base nesse ID, os ADS estão funcionando na plataforma PC também.
        Advertisement.Initialize("3654750", true);
#endif
#endif
        // Verifica se esta na tela de GameOver antes de buscar o botão de continue.
        if ("GameOver" == SceneManager.GetActiveScene().name)
        {
            // Faz a busca do botão quando a cena é iniciada.
            Button botao = GameObject.Find("Continue").GetComponent<Button>();
            // Verifica se o botão foi achado.
            if (botao)
            {
#if UNITY_ADS
                // Se tiver anuncio chama a função asincrona de verificação do tempo entre os anuncios.
                StartCoroutine(disbleContinueButton(botao));
#else
        // Se não tiver anuncio some com o botão.
        botao.SetActive(false);
#endif
            }
        }
    }

    /// <summary>
    /// Metodo para pausar o Game.
    /// E habilitar o menu de pause.
    /// </summary>
    public void PauseGame()
    {
        // Seta o time scale para zero, parando toda a execução do jogo
        Time.timeScale = 0;
        // Habilita o menu de pause com as opções resume, restart e menu
        pausedScreen.SetActive(true);
    }

    /// <summary>
    /// Metodo para continuar o Game.
    /// E desabilitar o menu de pause.
    /// </summary>
    public void ResumeGame()
    {
        // Desabilita o menu de pause
        pausedScreen.SetActive(!true);
        // Verificação se os anuncios estão habilitados.
#if UNITY_ADS
        // Verificação se os anuncios esta pronto para rodar.
        if (UnityAdControle.showAds)
        {
            // Roda um anuncio.
            UnityAdControle.ShowAd();
        }
#else
        // Despausa o game caso os anuncios não estejam habilitados.
        // Seta o time scale para um, voltando toda a execução do jogo ao normal
        Time.timeScale = 1;
#endif
    }

    /// <summary>
    /// Metodo para carregar uma cena com base no nome dela
    /// </summary>
    /// <param name="sceneName">Nome da cena a ser carregada</param>
    public void LoadScene(string sceneName)
    {
        // Seta o time scale para um, para garantir a execução do jogo.
        Time.timeScale = 1;
        // Carrega a cena com base no nome.
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Metodo similar ao LoadScene.
    /// Carrega uma cena com base no nome dela, porem salva um PlayerPrefs,
    /// para poder spawnar o player no local correto
    /// </summary>
    /// <param name="sceneName">Nome da cena a ser carregada</param>
    public void LoadSceneByAdds(string sceneName)
    {
        // Salva o nome da cena
        PlayerPrefs.SetString("sceneName", sceneName);
        // Seta o time scale para um, para garantir a execução do jogo.
        Time.timeScale = 0;
        // Chama a função de mostrar anuncio com recompensa
        UnityAdControle.showRewardAd();
    }

    /// <summary>
    /// Metodo asincrono de verificação do tempo entre os anuncios.
    /// </summary>
    /// <param name="continueBtn">Botão de continue</param>
    /// <returns></returns>
    public IEnumerator disbleContinueButton(Button continueBtn)
    {
        // Pega a referencia para o texto do botão
        var buttonText = continueBtn.GetComponentInChildren<Text>();

        // Faz um loop "infinito" até dar o tempo de mostrar outro anuncio.
        while (true)
        {
            // Verifica se tem algum tempo de espera para o anuncio setado e se é menor que o tempo atual.
            if (UnityAdControle.nextAdsReward.HasValue && (DateTime.Now < UnityAdControle.nextAdsReward.Value))
            {
                // Desabilita a interação com o botão.
                continueBtn.interactable = false;
                // Pega o tempo restante ate poder mostrar outro anuncio.
                TimeSpan restante = UnityAdControle.nextAdsReward.Value - DateTime.Now;
                // Cria a string com a contagem regressiva até o proximo anuncio.
                var regressionCount = String.Format("{0:D2}:{1:D2}", restante.Minutes, restante.Seconds);
                // Seta o texto do botão com o tempo restante.
                buttonText.text = regressionCount;
                // Retorno da Coroutine com a espera de 1 segundo.
                yield return new WaitForSeconds(1.0f);
            }
            else
            {
                // Habilita a interação com o botão.
                continueBtn.interactable = !false;
                // Seta um listener no click do botão, chamando a função de reward dos anuncios.
                continueBtn.onClick.AddListener(UnityAdControle.showRewardAd);
                // Seta o texto do botão com Continuar por padrão.
                buttonText.text = "Continuar";
                // Para a execução do loop.
                break;
            }
        }

    }
}
