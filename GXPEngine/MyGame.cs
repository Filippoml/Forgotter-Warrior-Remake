using System;                                   // System contains a lot of default C# libraries 
using System.Drawing;
using GXPEngine;                                // GXPEngine contains the engine
using GXPEngine.Classes;
using GXPEngine.Core;
using TiledMapParser;

/// <summary>
/// Defines the <see cref="MyGame" />
/// </summary>
public class MyGame : Game
{
    /// <summary>
    /// Defines the sprite
    /// </summary>
    internal AnimationSprite sprite;

    /// <summary>
    /// Defines the objects
    /// </summary>
    private static AnimationSprite[] objects;

    /// <summary>
    /// Defines the id_tiles
    /// </summary>
    private static int[] id_tiles;

    /// <summary>
    /// Defines the _player
    /// </summary>
    private Player _player;

    /// <summary>
    /// Defines the num_objects
    /// </summary>
    internal int num_objects = 0;

    /// <summary>
    /// Gets or sets the Objects
    /// </summary>
    public static AnimationSprite[] Objects
    {
        get { return objects; }
        set { objects = value; }
    }

    /// <summary>
    /// Gets or sets the Id_Tiles
    /// </summary>
    public static int[] Id_Tiles
    {
        get { return id_tiles; }
        set { id_tiles = value; }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MyGame"/> class.
    /// </summary>
    public MyGame() : base(800, 600, false, false, pPixelArt: true)		// Create a window that's 800x600 and NOT fullscreen
    {
        
        objects = new AnimationSprite[1000];
        id_tiles = new int[1000];

        //Background creation
        Bitmap Bmp = new Bitmap(width, height);
        using (Graphics gfx = Graphics.FromImage(Bmp))
        using (SolidBrush brush = new SolidBrush(Color.FromArgb(135, 206, 235)))
        {
            gfx.FillRectangle(brush, 0, 0, width, height);
        }

        Sprite _background = new Sprite(Bmp);
        AddChild(_background);
        _background.width = 5000;
        _background.height = 5000;


        Map level = MapParser.ReadMap("Data/level.tmx");

        for (int i = 0; i<level.Layers.Length; i++)
        {
            Layer _currentLayer = level.Layers[i];

            Data leveldata = _currentLayer.Data;
  

            Texture2D texture = new Texture2D(level.TileSets[0].Image.FileName);


            String levelData = _currentLayer.Data.innerXML.ToString();
            levelData = levelData.Replace("\n", "");

            int[] tiles = Array.ConvertAll(levelData.Split(','), int.Parse);

            int columns = level.Layers[0].Width;
            int rows = level.Layers[0].Height;
            for (int j = 0; j < tiles.Length; j++)
            {

                if (tiles[j] != 0)
                {

                    int x = j % columns;
                    int y = j / columns;
                    sprite = new AnimationSprite(texture.bitmap, level.TileSets[0].Columns, level.TileSets[0].Rows);
                    AddChild(sprite);

                    objects[num_objects] = sprite;

                    id_tiles[num_objects] = tiles[j];


                    num_objects++;
                    sprite.x = x * 30;
                    sprite.y = y * 30;
                    sprite.currentFrame = tiles[j] - 1;
                }

            }
        }
        

        _player = new Player();
        AddChild(_player);

        game.Translate(0, -800);
    }

    /// <summary>
    /// The Update
    /// </summary>
     void Update()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] != null)
            {
                if (_player.HitTest(objects[i]))
                {
                    i = objects.Length;
                    //Console.WriteLine("test");
                }
            }
        }


    }

    /// <summary>
    /// The Main
    /// </summary>
    internal static void Main()							// Main() is the first method that's called when the program is run
    {
        new MyGame().Start();
        
        
        // Create a "MyGame" and start it
    }
}
