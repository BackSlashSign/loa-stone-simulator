using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using Unity.VisualScripting;

public class AdBanner : MonoBehaviour
{

    private string _adBannerUnitId = "ca-app-pub-9815340316685784/8883650899";         //banner
    private string _adBannerUnitId_test = "ca-app-pub-3940256099942544/6300978111";    //sample banner


    private string adId;
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        MobileAds.RaiseAdEventsOnUnityMainThread = true;
        MobileAds.Initialize(initStatus => {/* This callback is called once the MobileAds SDK is initialized.*/});

        RequestConfiguration requestConfiguration = new RequestConfiguration();
        requestConfiguration.TestDeviceIds.Add("2077ef9a63d2b398840261c8221a0c9b");
        MobileAds.SetRequestConfiguration(requestConfiguration);
        
        RequestBanner();
    }
    public void RequestBanner()
    {
        string adId = Debug.isDebugBuild ? _adBannerUnitId_test : _adBannerUnitId;
        // Create a 320x50 banner at the top of the screen.
        BannerView bannerView = new BannerView(adId, AdSize.Banner, AdPosition.BottomRight);
        // Create an empty ad request.
        AdRequest request = new AdRequest();
        // Load the banner with the request.
        bannerView.LoadAd(request);
    }
}
