using System.Collections.Generic;
using UnityEngine;

public class RareAnimalsBookConfig : ScriptableObject
{
    public List<BookPare> South = new List<BookPare>();
    //public List<BookPare> West = new List<BookPare>();
    //public List<BookPare> East = new List<BookPare>();
    //public List<BookPare> North = new List<BookPare>(); 
    //public List<BookPare> Big = new List<BookPare>(); 
    //public List<BookPare> Inside = new List<BookPare>();
    //public List<BookPare> Out = new List<BookPare>();
    //public List<BookPare> Middle = new List<BookPare>();
}

[System.Serializable]
public class BookPare
{
    public List<TextSprite> Middle = new List<TextSprite>();
    public string Key = "";
}

[System.Serializable]
public class TextSprite
{
    public Sprite Sprite;
    public TextAsset Asset;
}
