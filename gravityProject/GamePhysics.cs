using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace gravityProject
{
    internal class GamePhysics
    {
        public void EnemyBoundaries(List<Enemy> enemies , List<EnemyCollider> enemyColliders )
        {
            foreach(var enemy in enemies)
            {
                if (enemy != null)
                {
                  if(!enemy.isStopped)
                    {
                      if (!enemy.enemyIsFlipped)
                      {
                          enemy.enemyPos.X += 1;
                      }
                      else
                      {
                          enemy.enemyPos.X -= 1;
                      }
                    }

                    foreach(var enemyCollider in enemyColliders)
                    {
                        if (enemyCollider != null)
                        {
                            if (enemy.enemyPos.Intersects(enemyCollider.ColliderPos))
                            {
                                if (!enemy.enemyIsFlipped)
                                {
                                    enemy.enemyIsFlipped = true;
                                }
                                else
                                {
                                    enemy.enemyIsFlipped = false;
                                }
                            }
                        }

                    }

                }
            }
        }
        public void PlayerIntersectsWithGround(Player player ,List <Ground> grounds, bool isFlipped)
        {
            foreach(var ground in grounds)
            {
                if (ground != null)
                {
                    if (player.playerPos.Intersects(ground.GroundPos))
                    {
                        if (player.playerPos.Y <= ground.GroundPos.Y - 90)
                        {
                            player.playerPos.Y = ground.GroundPos.Y - player.playerPos.Height;
                        }

                        else
                        { 
                            if (isFlipped)
                            {
                                if(player.playerPos.Y <= ground.GroundPos.Y )
                                {
                                player.playerPos.X = ground.GroundPos.X + player.playerPos.Width - 24;
                                }
                            }
                            else
                            {
                                if (player.playerPos.Y <= ground.GroundPos.Y )
                                {
                                 player.playerPos.X = ground.GroundPos.X - player.playerPos.Width - 10;
                                }
                            }  
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Space))
                        {
                             player.hasJump = true;
                        }
                    }

                }


            }
    
        }
        public bool PlayerGravity(Player player)
        {
                player.playerPos.Y = player.playerPos.Y - player.playerVelocity;

            if (player.timePassed <= 1.8)
                {
                    player.playerVelocity = player.playerVelocity - 1;
                    player.playerPos.Y = player.playerPos.Y + player.playerVelocity / 2;
                    if (player.playerVelocity <= 0 && player.timePassed <= 1.5)
                    {
                        player.timePassed = 2d;
                        player.playerVelocity = 10;
                        return player.hasJump = false;
                    }
                    //player.hasJump = false;
                }
            return player.hasJump = true;
        }
        public Color PlayerIntersectsWithEnemy(Player player ,List<Enemy> enemies)
        {
            foreach(var enemy in enemies)
            {
                if(enemy != null)
                {
                   if(player.playerPos.Intersects(enemy.enemyPos))
                   {
                        enemy.isStopped = true;
                        PlayerTakingDamage(player);
                        if (player.playerPos.X > enemy.enemyPos.X)
                        {
                            enemy.enemyIsFlipped = false;
                            enemies.Remove(enemy);
                        }
                        else
                        {
                            enemy.enemyIsFlipped = true;
                        }
                        return player.playerColor = Color.Red;
                    }
                    else
                    {
                        enemy.isStopped = false;
                    }
                }
              
            }
            return player.playerColor = Color.White;


        }

        public void playerHealing(Player player , List <Items> items)
        {
            foreach(Items item in items) 
            {
                if(item != null)
                {
                if (player.playerPos.Intersects(item.injectPos))
                {
                    player.playerHealth += 20;
                    item.injectPos.X = 0;
                }
                }
            }
        }

        private void PlayerTakingDamage(Player player)
        {

            player.playerHealth-= 1;



            if(player.playerHealth == 0)
            {

            
                player.isDead= true;
          


            }

            



        }




    }
}
