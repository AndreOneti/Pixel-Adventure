using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_ADS
using UnityEngine.Advertisements;
#endif
/// <summary>
/// Classe responsavel por gerenciar todos os anuncios (Ads) do jogo.
/// </summary>
public class UnityAdControle : MonoBehaviour
{

    public static bool showAds = true;

    /// <summary>
    /// Metodo responsavel por mostrar o anuncio (Ads). Esse metodo deve ser chamado no local que deseja ter anuncio.
    /// </summary>
    public static void ShowAd()
    {
    #if UNITY_ADS
	    if(Advertisement.IsReady()){
        //Mostra o anuncio - Ads
	    Advertisement.Show();
	}
    #endif
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
