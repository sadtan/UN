using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Utilites : MonoBehaviour
{
    // Start is called before the first frame update
    public static Utilites current;

    private struct CacheImg {
        public Sprite texture {get; set;}
        public string path  {get; set;}
    }
    private List<CacheImg> cacheList;
    void Awake() {
        current = this;
        cacheList = new List<CacheImg>();
    }

    public Sprite loadImage(string imageName) {
        Sprite tempS = Resources.Load<Sprite>("img/" + imageName);
        if (tempS == null)
        tempS = Resources.Load<Sprite>("UI/ImgNotFound");
        try {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Application.streamingAssetsPath + "/img/");
            // System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Application.dataPath + "/Resources/img");
            System.IO.FileInfo[] files = dir.GetFiles(imageName + ".*");

            // if (files.Length > 0)
            //     Log("Utilites", files[0].ToString());

            string path = "img/" + imageName;
            tempS = Resources.Load<Sprite>(path);

            if (tempS == null) {
                // dir = new System.IO.DirectoryInfo(Application.streamingAssetsPath + "/img/");

                files = dir.GetFiles(imageName + ".*");
                if (files.Length > 0) 
                    return(LoadNewSprite(files[0].ToString(), imageName));
                    // Log("Utilites", files[0].ToString());
                
            } 
            if (tempS != null) {
                return tempS;
            }
            else 

            return Resources.Load<Sprite>("UI/ImgNotFound");
        } catch (IOException e) when ((e.HResult & 0x0000FFFF) == 32 ) {
            Log("Utilities", e.Message);
            return tempS;
        } catch (IOException e) when ((e.HResult & 0x0000FFFF) == 80) {
            Log("Utilities", e.Message);
            return tempS;
        } catch (IOException e) {
            Log("Utilities", e.Message);
            return tempS;
        }  
        // return Resources.Load<Sprite>("UI/ImgNotFound");

    }

    public void Log(string className, string message) {
        Debug.Log("[" + className + "] : " + message);
    }

    public Sprite LoadNewSprite(string FilePath, string FileName, float PixelsPerUnit = 100.0f) {
   
     // Load a PNG or JPG image from disk to a Texture2D, assign this texture to a new sprite and return its reference

    foreach (CacheImg cache in cacheList) {
        if (cache.path == FilePath)
        return (cache.texture);
    }
     
    Texture2D SpriteTexture = LoadTexture(FilePath);
    // = Sprite.Create(SpriteTexture);
    Sprite NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height),new Vector2(0,0), PixelsPerUnit);

    CacheImg cacheS = new CacheImg();
    cacheS.texture = NewSprite;
    cacheS.path = FilePath;
    cacheList.Add(cacheS);

    // byte[] SaveData;
    // SaveData = NewSprite.texture.EncodeToPNG();
    // File.WriteAllBytes(Application.streamingAssetsPath + "/img/saved/" + FileName + ".png", SaveData);
    
     return NewSprite;
   }
 
   public Texture2D LoadTexture(string FilePath) {
 
     // Load a PNG or JPG file from disk to a Texture2D
     // Returns null if load fails
 
     Texture2D Tex2D;
     byte[] FileData;

     
 
     if (File.Exists(FilePath)) {
       FileData = File.ReadAllBytes(FilePath);
       
       Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
       
        if (Tex2D.LoadImage(FileData)) {
            
            return Tex2D;
        }          
    }  
    
    return null;                     // Return null if load failed
   }
}
