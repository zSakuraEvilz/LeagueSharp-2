﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using forms = System.Windows.Forms;

namespace PastingSharp
{
    public class Program
    {
        public static string contents = "";
        public static string[] linestoprint;
        public static Menu menu;
        public static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        public static void Game_OnGameLoad(EventArgs args)
        {

            menu = new Menu("PastingSharp", "pasting", true);
            menu.AddItem(new MenuItem("sleep", "Pause between pastes").SetValue(new Slider(1, 0, 15)));
            menu.AddItem(new MenuItem("paste", "Paste")).SetValue(new KeyBind("P".ToCharArray()[0], KeyBindType.Press));
            Game.PrintChat("PastingSharp loaded. Press P to paste.");

            Game.OnGameUpdate += Game_OnGameUpdate;
        }
        public static void Game_OnGameUpdate(EventArgs args)
        {

            if (forms.Clipboard.ContainsText())
            {
                contents = forms.Clipboard.GetText();
                if (contents.Contains("\n"))
                {
                    var separator = new string[] {"\n"};
                    linestoprint = contents.Split(separator,StringSplitOptions.None);
                }
            }

            if (menu.Item("paste").GetValue<KeyBind>().Active)
            {
                if (linestoprint == null)
                {
                    Game.Say(contents);
                }
                else
                {
                    foreach (string s in linestoprint)
                    {
                        Game.Say(s);
                    }
                    var linestoprintsize = contents.Count();
                    Array.Clear(linestoprint, 0, linestoprintsize);
                }
                var sleep = (menu.Item("sleep").GetValue<int>());
                if (sleep != 0)
                {
                    System.Threading.Thread.Sleep(sleep);
                }
            }
            
        }
    }
}
