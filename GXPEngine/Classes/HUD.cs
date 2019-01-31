﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.Classes
{
    public class HUD : GameObject
    {
        private Player _player;

        private Sprite _life1, _life2, _mana1, _mana2;

        private Font _font;

        private EasyDraw _easyDraw;

        private int _health_potion_number, _mana_potion_number;

        public HUD()
        {

            Sprite stats = new Sprite("Data/stats.png");
            AddChild(stats);

            _player = ((MyGame)game).GetPlayer();

            Bitmap Bmp = new Bitmap(100, 100);
            Graphics gfx = Graphics.FromImage(Bmp);
            SolidBrush brush = new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#FF969F"));
            gfx.FillRectangle(brush, 0, 0, 100, 100);
            _life1 = new Sprite(Bmp);
            _life1.x = 97;
            _life1.y = 14.5f;
            _life1.width = 46;
            _life1.height = 3;
            AddChild(_life1);


            
            brush = new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#E70052"));
            gfx.FillRectangle(brush, 0, 0, 100, 100);
            _life2 = new Sprite(Bmp);
            _life2.x = 97;
            _life2.y = 17.5f;
            _life2.width = 46;
            _life2.height = 3;
            AddChild(_life2);

            //TODO check the same order
            brush = new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#6DCFF6"));
            gfx.FillRectangle(brush, 0, 0, 100, 100);
            _mana1 = new Sprite(Bmp);
            _mana1.x = 176;
            _mana1.y = 14.5f;
            _mana1.width = 56;
            _mana1.height = 3;
            AddChild(_mana1);

            brush = new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#0080FF"));
            gfx.FillRectangle(brush, 0, 0, 100, 100);
            _mana2 = new Sprite(Bmp);
            _mana2.x = 176;
            _mana2.y = 17.5f;
            _mana2.width = 56;
            _mana2.height = 3;
            AddChild(_mana2);
            
            for (int i = 0; i< 3; i++)
            {
                Sprite notch = new Sprite("Data/notch.png");
                notch.SetXY(189 + 19 * i, 9);
                AddChild(notch);
            }

            _easyDraw = new EasyDraw(100, 100);
            AddChild(_easyDraw);

            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile("Data/LCD Solid.ttf");
            _font = new Font(new FontFamily(pfc.Families[0].Name), 10, FontStyle.Regular);

            updateHUD();
        }

        public void Update()
        {            
            //Updating rectangles witdh based on the player life
            _life1.width = (46 * _player.GetLifePoints()) / 100;
            _life2.width = (46 * _player.GetLifePoints()) / 100;

            
        }

        public void IncrementHealthPotionsNumber()
        {
            _health_potion_number++;
            updateHUD();
        }

        public void IncrementManaPotionsNumber()
        {
            _mana_potion_number++;
            updateHUD();
        }

        public int GetHealthPotionsNumber()
        {
            return _health_potion_number;
        }

        public int GetManaPotionsNumber()
        {
           return _mana_potion_number;
        }

        private void updateHUD()
        {
            _easyDraw.Clear(Color.Transparent);

            //Life potions number
            _easyDraw.graphics.DrawString(_health_potion_number.ToString(), _font, new SolidBrush(Color.White), new PointF(17, 12));

            //Mana potions number
            _easyDraw.graphics.DrawString(_mana_potion_number.ToString(), _font, new SolidBrush(Color.White), new PointF(45, 12));
        }
    }
}