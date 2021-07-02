using System.IO;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class LoadImage : MonoBehaviour
{
    [SerializeField] private Image image1;
    [SerializeField] private Image image2;
    [SerializeField] private Image image3;
    [SerializeField] private Button button;
    [SerializeField] private Button button2;
    [SerializeField] private TMP_InputField address;
    [SerializeField] private TMP_InputField port;
    

    void Start()
    {
        button.onClick.AddListener(LoadImages);   
        button2.onClick.AddListener(LoadCatalog);
        Application.targetFrameRate = 30;
        address.text = "http://192.168.1.114";
        port.text = "63196";
    }

    private void SaveLocalCopyCatalog(string catalogJson)
    {
        string path = $"{Application.persistentDataPath}/com.unity.addressables/";
        FileInfo fi = new FileInfo(path + catalogJson);
        if (fi.Exists)
            return;

        string catalogHash = catalogJson.Replace(".json", ".hash");
        TextAsset json = Resources.Load<TextAsset>(catalogJson);
        TextAsset hash = Resources.Load<TextAsset>(catalogHash);

        Debug.Log($"Create {catalogJson} at {path}");
        using (FileStream fileStream = File.Open(path + catalogJson, FileMode.Create))
        using (StreamWriter writer = new StreamWriter(fileStream))
            writer.Write(json.text);

        Debug.Log($"Create {catalogHash} at {path}");
        using (FileStream fileStream = File.Open(path + catalogHash, FileMode.Create))
        using (StreamWriter sw = new StreamWriter(fileStream))
            sw.Write(hash.text);

    }

    private async void LoadCatalog()
    {
        string path = $"{address.text}:{port.text}/";
        string catalogJson = "catalog_1.0.json";
        //SaveLocalCopyCatalog(catalogJson);
        Debug.LogWarning($"Start load {path}{catalogJson}");
        AsyncOperationHandle<IResourceLocator> handle = Addressables.LoadContentCatalogAsync($"{path}{catalogJson}");

        await handle.Task;
        
        Debug.LogWarning($"End loading with message: {handle.Status}");
    }

    private async void LoadImages()
    {
        Debug.LogWarning($"Start load images");
        AsyncOperationHandle<Sprite> handle1 = Addressables.LoadAssetAsync<Sprite>("Images/img1.jpg[img1]");
        AsyncOperationHandle<Sprite> handle2 = Addressables.LoadAssetAsync<Sprite>("Images/img2.jpg[img2]");
        AsyncOperationHandle<Sprite> handle3 = Addressables.LoadAssetAsync<Sprite>("Images/img3.jpg[img3]");
        
        await Task.WhenAll(handle1.Task, handle2.Task, handle3.Task);
        
        Debug.LogWarning($"Use loaded images");
        image1.sprite = handle1.Result;
        image2.sprite = handle2.Result;
        image3.sprite = handle3.Result;
    }
    
    

}
