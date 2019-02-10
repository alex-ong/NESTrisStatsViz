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

    public enum BlockType
    {
        WHITE = 0,
        PRIMARY = 1,
        SECONDARY = 2
    }
    public enum Border
    {
        BORDER,
        ORIGINAL
    }

    private bool ready = false;
    public bool Ready { get { return ready; } }

    [SerializeField]
    private List<Texture2D> textures = new List<Texture2D>();
    [SerializeField]
    private List<Sprite> sprites = new List<Sprite>();
    [SerializeField]
    private List<Texture2D> texturesBordered = new List<Texture2D>();
    [SerializeField]
    private List<Sprite> spritesBordered = new List<Sprite>();
    public void Awake()
    {
        StartCoroutine(generateTextures());
    }

    static protected int getIndex(int level, Border b, BlockType bt)
    {
        level %= 10;
        int index = (int)bt + 3 * level;
        return index;
    }


    public Sprite getLevelSprite(int level, Border b, BlockType bt)
    {
        if (!ready) return null;
        List<Sprite> items = (b == Border.BORDER) ? this.spritesBordered : this.sprites;
        return items[getIndex(level, b, bt)];
    }

    public Texture2D getLevelTexture(int level, Border b, BlockType bt)
    {
        if (!ready) return null;
        List<Texture2D> items = (b == Border.BORDER) ? this.texturesBordered : this.textures;
        return items[getIndex(level, b, bt)];
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
            Color primary = pixels[i + 10];
            Color secondary = pixels[i];

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

}
