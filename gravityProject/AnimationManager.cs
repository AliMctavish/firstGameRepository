using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace gravityProject
{
    internal class AnimationManager
    {
        public void itemsAnimation(List <Items> items, ContentManager Content)
        {
            foreach(var item in items) 
            {
            item.coinsTexture = Content.Load<Texture2D>($"coin{item.CoinCounter}");
       
            if (item.CoinCounter == 11 )
            {
              item.coinsTexture = Content.Load<Texture2D>("coin11");
              item.CoinCounter = 1;
            }
            item.CoinCounter += 1;
            }
        }  
        public void enemyAnimation(List<Enemy> enemies, ContentManager Content)
        {
            foreach(var enemy in enemies) 
            {
                   if(!enemy.isStopped)
                    {
                        enemy.enemyTexture = Content.Load<Texture2D>($"EnemyMoving{enemy.counter}");
                        if (enemy.counter == 4)
                        {
                            enemy.enemyTexture = Content.Load<Texture2D>("EnemyMoving4");
                            enemy.counter = 1;
                        }
                        enemy.counter += 1;
                    }
                    else
                    {
                        enemy.enemyTexture = Content.Load<Texture2D>($"animations/EnemyAttack{enemy.attackCounter}");
                        if(enemy.attackCounter == 6)
                        {
                            enemy.enemyTexture = Content.Load<Texture2D>("animations/EnemyAttack1");
                            enemy.attackCounter = 1;
                        }
                          enemy.attackCounter += 1;
                    }
            }
        }
        public void playerAnimationIdle(Player player , ContentManager Content)
        {
            if (!Keyboard.GetState().IsKeyDown(Keys.D) && !Keyboard.GetState().IsKeyDown(Keys.A))
            {
                player.playerTexture = Content.Load<Texture2D>($"animations/playerMovement{player.PlayerAnimationCounter}");
            }
            player.PlayerAnimationCounter += 1;
            if(player.PlayerAnimationCounter == 8)
            {
                player.PlayerAnimationCounter = 1;
            }
               
        }
    }
}
