using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTextureGenerator : Singleton<BlockTextureGenerator>
{
    public Texture2D palette; //assumes 10x2 texture.
    public Texture2D filledBlock;
    public Texture2D whiteBlock;
    public Texture2D filledBlockBordered;
    public Texture2D whiteBlockBordered;

    private bool ready = false;
    public bool Ready { get { return ready; } }
    
    public List<Texture2D> textures = new List<Texture2D>();
    public List<Sprite> sprites = new List<Sprite>();
    public List<Texture2D> texturesBordered = new List<Texture2D>();
    public List<Sprite> spritesBordered = new List<Sprite>();
    public void Awake()
    {
        StartCoroutine(generateTextures());
    }

    public IEnumerator generateTextures()
    {
        if (palette.width != 10 || palette.height != 2)
        {
            Debug.Log("Error, require 10x2 pixel palette texture");
            yield break;
        }
        Color[] pixels = palette.GetPixels();

        for (int i = 0; i < 10; i++)
        {
            Color primary = pixels[i];
            Color secondary = pixels[i + 10];

            //Original textures
            textures.Add(generateTexture(whiteBlock, primary));
            yield return null;
            textures.Add(generateTexture(filledBlock, primary));
            yield return null;
            textures.Add(generateTexture(filledBlock, secondary));
            yield return null;
            //bordered
            texturesBordered.Add(generateTexture(whiteBlockBordered, primary));
            yield return null;
            texturesBordered.Add(generateTexture(filledBlockBordered, primary));
            yield return null;
            texturesBordered.Add(generateTexture(filledBlockBordered, secondary));
            yield return null;
        }

        foreach (Texture2D tex in textures)
        {
            Sprite s = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
            sprites.Add(s);
        }
        foreach (Texture2D tex in texturesBordered)
        {
            Sprite s = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
            spritesBordered.Add(s);
        }

        ready = true;
    }

    public Texture2D generateTexture(Texture2D source, Color fill)
    {
        Color32[] pixels = source.GetPixels32();
        for (int i = 0; i < pixels.Length; i++)
        {
            if (pixels[i].a == 0)
            {
                pixels[i] = fill;
            }
        }
        Texture2D result = new Texture2D(source.width, source.height);
        result.filterMode = FilterMode.Point;
        result.SetPixels32(pixels);
        result.Apply();
        return result;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
