﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GXPEngine.Classes
{
    public class Shop : Sprite
    {
        private Sprite _background, _shop_rect;

        private Items _items;

        private int _indexItems, _indexMode;

        private EasyDraw _easyDraw;

        private Font _font;

        private Player _player;

        private HUD _hud;

        public Shop(float x, float y) : base("Data/shop.png")
        {
            SetScaleXY(2f);
            this.x = x;
            this.y = y;


            _player = ((MyGame)game).GetPlayer();
            _hud = ((MyGame)game).GetHud();

            Bitmap Bmp = new Bitmap(100, 100);
            Graphics gfx = Graphics.FromImage(Bmp);
            SolidBrush brush = new SolidBrush(Color.White);
            gfx.FillRectangle(brush, 0, 0, 100, 100);
            _background = new Sprite(Bmp);
            _background.width = 26;
            _background.height = 26;
            _background.x = 41;
            _background.y = 131;
            AddChild(_background);

            _shop_rect = new Sprite("Data/shop_rect.png");
            _shop_rect.SetXY(164, 46);
            AddChild(_shop_rect);



            for (int i = 0; i < 5; i++)
            {                
                Sprite _itemSprite = new Sprite("Data/item" + i + ".png");
                if (i > 2)
                {
                    _itemSprite.SetXY(41 + 1 + (i * 32), 133);
                }
                else
                {
                    _itemSprite.SetXY(41 + 3 + (i * 32), 134);
                }
                AddChild(_itemSprite);
            }

         
            string path = "Data/Items.xml";

            XmlSerializer serializer = new XmlSerializer(typeof(Items));

            StreamReader reader = new StreamReader(path);
            _items = (Items)serializer.Deserialize(reader);
            reader.Close();


            _easyDraw = new EasyDraw(300, 300);
            AddChild(_easyDraw);

            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile("Data/LCD Solid.ttf");
            _font = new Font(new FontFamily(pfc.Families[0].Name), 10, FontStyle.Regular);

            _easyDraw.graphics.DrawString("WELCOME", _font, new SolidBrush(Color.White), new PointF(80, 200));
        }
        void Update()   
        {
            if (!((MyGame)game).IsPaused())
            {
                if (visible)
                {
                    keyHandler();
                }
            }
        }

        private void keyHandler()
        {


            if (Input.GetKeyDown(Key.A))
            {
                if (_background.x > 41)
                {
                    _background.x -= 32;
                    _indexItems--;
                    Item _item = _items.Item[_indexItems];
                    _easyDraw.Clear(Color.Transparent);

                    _easyDraw.graphics.DrawString(_item.Cost.ToString(), _font, new SolidBrush(Color.White), new PointF(65, 172.5f));
                    _easyDraw.graphics.DrawString(_player.GetCoinsNumber().ToString(), _font, new SolidBrush(Color.White), new PointF(172, 172.5f));
                }

            }
            else if(Input.GetKeyDown(Key.D))
            {
                if (_background.x < 169)
                {
                    _background.x += 32;
                    _indexItems++;

                    Item _item = _items.Item[_indexItems];
                    _easyDraw.Clear(Color.Transparent);
                    _easyDraw.graphics.DrawString(_item.Cost.ToString(), _font, new SolidBrush(Color.White), new PointF(65, 172.5f));
                    _easyDraw.graphics.DrawString(_player.GetCoinsNumber().ToString(), _font, new SolidBrush(Color.White), new PointF(172, 172.5f));
                }
         
            }
            else if (Input.GetKeyDown(Key.W))
            {
                
                if (_shop_rect.y > 46)
                {
                    _shop_rect.y -= 18;
                    _indexMode--;



                }
                Item _item = _items.Item[4];


                _easyDraw.graphics.DrawString(_item.Value, _font, new SolidBrush(Color.White), new PointF(65, 172.5f));
                _easyDraw.graphics.DrawString(_player.GetCoinsNumber().ToString(), _font, new SolidBrush(Color.White), new PointF(172, 172.5f));

            }
            else if (Input.GetKeyDown(Key.S))
            {
                if(_shop_rect.y < 82)
                {
                    _shop_rect.y += 18;
                    _indexMode++;
                }
            }
            else if (Input.GetKeyDown(Key.ENTER))
            {
               
                Item _item = _items.Item[_indexItems];
                switch(_indexMode)
                {
                    case 0:
                        if (_player.GetCoinsNumber() >= _item.Cost)
                        {
                            _player.SetCoinsNumber(false, _item.Cost);
                            _easyDraw.Clear(Color.Transparent);
                            _easyDraw.graphics.DrawString(_player.GetCoinsNumber().ToString(), _font, new SolidBrush(Color.White), new PointF(172, 172.5f));
                            _easyDraw.graphics.DrawString(_item.Cost.ToString(), _font, new SolidBrush(Color.White), new PointF(65, 172.5f));

                            switch (_indexItems)
                            {
                                case 3:
                                    _hud.SetHealthPotionsNumber(true);
                                    break;
                                case 4:
                                    _hud.SetManaPotionsNumber(true);                                
                                    break;
                            }

                        }
                        else
                        {
                            _easyDraw.graphics.DrawString("NO ENOUGH MONEY", _font, new SolidBrush(Color.White), new PointF(55, 200));
                        }
                        break;

                    case 1:
                        switch (_item.Type)
                        {
                            case "weapon":
                                _easyDraw.graphics.DrawString("DAMAGE:" + _item.Damage, _font, new SolidBrush(Color.White), new PointF(36, 200));
                                _easyDraw.graphics.DrawString("RANGE:" + _item.Range, _font, new SolidBrush(Color.White), new PointF(125, 200));
                                
                                break;
                            case "potion":
                                _easyDraw.graphics.DrawString("HEALING:(" + _item.Value + "%)", _font, new SolidBrush(Color.White), new PointF(60, 200));
                                break;
                        }
                        break;

                    case 2:
                        visible = false;
                        _player.SetState(Player.State.IDLE);
                       
                        break;
                }

            }



            
        }


    }
}
