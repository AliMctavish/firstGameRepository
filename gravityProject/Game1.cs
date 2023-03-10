using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace gravityProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private Texture2D HealthBar ;
        private Texture2D coinCounter;
        private LevelMapper levelMapper = new LevelMapper();
        private List<Ground> ground;
        private List<Enemy> enemies;
        private List<EnemyCollider> enemyColliders;
        Player player;
        int moveCounter = 5;
        private List <Items> items;
        float waitingTime2 = 0;
        int Counter = 1;
        private SoundEffect chestSound;
        private SoundEffect coinSound;
        double moveTimer = 1;
        float animateCounter2 = 0.1f;
        GamePhysics GamePhysics; 
        float waitingTime = 0;
        private int numberOfcoins = 0;
        private bool isInside = false;
        private bool played = false;
        Maps level =  new Maps();
        AnimationManager animation;
        private int jumpConuter = 0;
        private bool isFlipped = false;
        int selectLevel = 6;
        bool lol = false;

        private double timePassed=2d;
      
        private Texture2D backgroundColor;
        public Game1()
        {       
                _graphics = new GraphicsDeviceManager(this);
                Content.RootDirectory = "Content";
                IsMouseVisible = true;
                _graphics.PreferredBackBufferWidth = 1600;
                _graphics.PreferredBackBufferHeight = 800;
                _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
          
            level = new Maps();



            HealthBar = Content.Load<Texture2D>("HealthBar");
            coinCounter = Content.Load<Texture2D>("coin1");



            player = new Player();

            items = new List<Items>();
            ground = new List<Ground>();
            enemies = new List<Enemy>();
            enemyColliders = new List<EnemyCollider>();
            

            animation = new AnimationManager();





        
          
            


            // TODO: Add your initialization logic here6


            player.playerTexture = Content.Load<Texture2D>("animations/playerMovement1");
            backgroundColor = Content.Load<Texture2D>("background-export");
            chestSound = Content.Load<SoundEffect>("sound");
            _font = Content.Load<SpriteFont>("File");
            coinSound = Content.Load<SoundEffect>("coinSound");



        
            player.playerPos = new Rectangle(500 , 200 , 76, 98);
           
                
            base.Initialize();

        }
        
        protected override void LoadContent()
        {
            GamePhysics = new GamePhysics();
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            // TODO: use this.Content to load your game content here
        }


        protected override void Update(GameTime gameTime)
        {


            
          

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                

                player.isDead = false;
                player.playerPos.X = 100;
                player.playerPos.Y = 100;
                player.playerHealth = 100;
                numberOfcoins = 0;
                enemies.Clear();
                enemyColliders.Clear();
                items.Clear();
                ground.Clear();
                lol = true;
                selectLevel = 6;


                

            }
            if(Keyboard.GetState().IsKeyDown(Keys.I))
            {
                player.isDead = false;
                player.playerPos.X = 100;
                player.playerPos.Y = 100;
                player.playerHealth = 100;
                numberOfcoins = 0;
                enemies.Clear();
                enemyColliders.Clear();
                items.Clear();
                ground.Clear();
                lol = true;
                selectLevel = 1;
            } 
            
            
            if(Keyboard.GetState().IsKeyDown(Keys.P))
            {
                player.isDead = false;
                player.playerPos.X = 100;
                player.playerPos.Y = 100;
                player.playerHealth = 100;
                numberOfcoins = 0;
                enemies.Clear();
                enemyColliders.Clear();
                items.Clear();
                ground.Clear();
                lol = true;
                selectLevel = 2;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.K))

            {
                player.isDead = false;
                player.playerPos.X = 100;
                player.playerPos.Y = 100;
                player.playerHealth = 100;
                numberOfcoins = 0;
                enemies.Clear();
                enemyColliders.Clear();
                items.Clear();
                ground.Clear();
                lol = true;
                selectLevel = 3;
            } if(Keyboard.GetState().IsKeyDown(Keys.L))
            {
                player.isDead = false;
                player.playerPos.X = 100;
                player.playerPos.Y = 100;
                player.playerHealth = 100;
                numberOfcoins = 0;
                enemies.Clear();
                enemyColliders.Clear();
                items.Clear();
                ground.Clear();
                lol = true;
                selectLevel = 4;
            } if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                player.isDead = false;
                player.playerPos.X = 100;
                player.playerPos.Y = 100;
                player.playerHealth = 100;
                numberOfcoins = 0;
                ground.Clear();
                enemies.Clear();
                enemyColliders.Clear();
                items.Clear();
                ground.Clear();
                lol = true;
                selectLevel = 5;
            }

            if (lol == true)
            {
                string[] map = level.LoadLevel(selectLevel);
                levelMapper.StartMapping(ground, map, items, enemies, Content, enemyColliders);
                lol = false;
            }



            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(player.isDead == true)
            {

                player.hasJump = true;
                player.playerPos.Y+= 10;
            
            }

            if (player.isDead == false)
            {
                float time = (float)gameTime.ElapsedGameTime.TotalSeconds * 140;
                waitingTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

           


                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    moveTimer += gameTime.ElapsedGameTime.TotalSeconds * 6;
                    for (int i = 1; i < 5; i++)
                    {
                        if (moveTimer > i)
                        {
                            player.playerTexture = Content.Load<Texture2D>($"animations/PlayerWalking{i}");
                        }
                        if (moveTimer > 5)
                        {
                            player.playerTexture = Content.Load<Texture2D>($"animations/PlayerWalking5");
                            moveTimer = 1;
                        }
                    }
                    player.playerPos.X = player.playerPos.X + (int)time + 2;

                    //for (int i = 0; i < ground.Length; i++)
                    //{
                    //    if (ground[i] != null)
                    //    {
                    //    ground[i].GroundPos.X = ground[i].GroundPos.X + 1 - (int)time;
                    //    }

                    //}
                    if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                    {
                        player.playerPos.X += 2;
                    }
                }


                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    player.playerPos.X = player.playerPos.X - (int)time - 2;
                    moveTimer += gameTime.ElapsedGameTime.TotalSeconds * 6;
                    for (int i = 1; i < 5; i++)
                    {
                        if (moveTimer > i)
                        {
                            player.playerTexture = Content.Load<Texture2D>($"animations/PlayerWalking{i}");
                        }
                        if (moveTimer > 5)
                        {
                            player.playerTexture = Content.Load<Texture2D>($"animations/PlayerWalking5");
                            moveTimer = 1;
                        }
                    }
                    //for (int i = 0; i < ground.Length; i++)
                    //{
                    //    if (ground[i] != null)
                    //    { 
                    //        ground[i].GroundPos.X = ground[i].GroundPos.X - 1 + (int)time;
                    //    }
                    //}
                    if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                    {
                        player.playerPos.X -= 2;
                    }
                }

                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i] != null)
                    {
                        if (player.playerPos.Intersects(items[i].chestPos))
                        {
                            isInside = true;
                            if (played == false)
                            {
                                chestSound.Play();
                                played = true;
                            }

                        }
                    }

                }
                if (isInside)
                {
                    float animateCounter = 0.1f;
                    for (int i = 2; i < items.Count; i++)
                    {
                        waitingTime2 += (float)gameTime.ElapsedGameTime.TotalSeconds;


                        if (waitingTime2 >= animateCounter)
                        {
                            if (items[i] != null)
                            {
                                items[i].chestTexture = Content.Load<Texture2D>($"chest{Counter}");
                            }
                            if (waitingTime2 >= 1)
                            {
                                isInside = false;
                            }
                            animateCounter += 0.1f;
                        }
                    }


                }
              

                for (int i = 0; i < items.Count; i++)
                {
                  
                        if (player.playerPos.Intersects(items[i].coinsPos))
                        {
                       items.Remove(items[i]);
                            coinSound.Play();
                            numberOfcoins++;
                     
                        }
                    
                }
                Debug.WriteLine(items.Count);
                //animations

                if (waitingTime > animateCounter2)
                {
                    animation.itemsAnimation(items, Content);

                    animation.enemyAnimation(enemies, Content);

                    animation.playerAnimationIdle(player, Content);

                    animateCounter2 += 0.1f;
                }

                //colliders and physics methods

                if (player.hasJump == true)
                {
                    player.timePassed -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                    GamePhysics.PlayerGravity(player);
                }
                GamePhysics.PlayerIntersectsWithGround(player, ground, isFlipped);

                GamePhysics.PlayerIntersectsWithEnemy(player, enemies);

                GamePhysics.EnemyBoundaries(enemies, enemyColliders);

                GamePhysics.playerHealing(player , items);

                player.playerPos.Y += 4;

            }
      
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            _spriteBatch.Draw(backgroundColor, new Rectangle(0, 0 , 1600,900), Color.White);
            //_spriteBatch.Draw(ballTexture, new Vector2( player.playerPos.X -32 ,  player.playerPos.Y - 50), Color.White);
            if (!isFlipped)
            {
                _spriteBatch.Draw(player.playerTexture, new Rectangle(player.playerPos.X - 34, player.playerPos.Y - 34, player.playerPos.Width, player.playerPos.Height), null, player.playerColor, 0, Vector2.Zero, SpriteEffects.None, 0);
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    isFlipped = true;
                }
            }
            else
            {
                _spriteBatch.Draw(player.playerTexture, new Rectangle(player.playerPos.X - 34, player.playerPos.Y - 34, player.playerPos.Width, player.playerPos.Height), null,player.playerColor, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    isFlipped = false;
                }
            }
        

            //Game Debugging Is Here
            _spriteBatch.DrawString(_font,"Player Position On Y : " + player.playerPos.Y , new Vector2(10, 0), color: Color.White);
          
            for(int i = 0; i < player.playerHealth; i++)
            {
                if(i == 80)
                {
                    _spriteBatch.Draw(HealthBar, new Rectangle(1400, 10, 40, 40), color: Color.White);
                }
                if(i == 60)
                {
                    _spriteBatch.Draw(HealthBar, new Rectangle(1440, 10, 40, 40), color: Color.White);
                }
                if(i == 20)
                {
                   _spriteBatch.Draw(HealthBar, new Rectangle(1480, 10, 40, 40), color: Color.White);
                }
                if (i == 5)
                {
                    _spriteBatch.Draw(HealthBar, new Rectangle(1520, 10, 40, 40), color: Color.White);
                }

            }
            _spriteBatch.Draw(coinCounter, new Rectangle(1510, 50, 70, 70), color: Color.White);

            _spriteBatch.DrawString(_font,"Player Position On X : " + player.playerPos.X , new Vector2(10, 20), color: Color.White);
            _spriteBatch.DrawString(_font,"Ability To Jump : " + player.hasJump , new Vector2(10, 40), color: Color.White);
            _spriteBatch.DrawString(_font,"Time Since Jumped : " + timePassed , new Vector2(10,60), color: Color.White);
            _spriteBatch.DrawString(_font,"Jump Counter : " + jumpConuter , new Vector2(10,80), color: Color.White);
            _spriteBatch.DrawString(_font,"Player Health : " + player.playerHealth , new Vector2(10,140), color: Color.White);
            _spriteBatch.DrawString(_font , numberOfcoins + "x"  , new Vector2(1470,67), color: Color.Black ,0 , new Vector2(0,0) ,2, 0 , 0);
            //Game Debugging Is Here
            for (int i = 0; i < ground.Count; i++)
            {
             _spriteBatch.Draw(ground[i].groundTexture, new Vector2(ground[i].GroundPos.X - 38 , ground[i].GroundPos.Y - 35), Color.White);      
            }

            for (int i = 0; i < items.Count; i++)
                {
               
                _spriteBatch.Draw(items[i].coinsTexture, new Vector2(items[i].coinsPos.X - 35, items[i].coinsPos.Y - 35), Color.White);

                //_spriteBatch.Draw(items[i].injectTexture, new Vector2(items[i].injectPos.X - 35, items[i].injectPos.Y - 35), Color.White);

                
                }

            for(int i = 0; i < enemies.Count; i++)
            {
               
                    switch (enemies[i].enemyIsFlipped)
                    {
                        case false :
                            {
                                _spriteBatch.Draw(enemies[i].enemyTexture, new Rectangle(enemies[i].enemyPos.X - 35, enemies[i].enemyPos.Y - 20, 90, 100), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                                break;
                            }
                        case true:
                            {
                                _spriteBatch.Draw(enemies[i].enemyTexture, new Rectangle(enemies[i].enemyPos.X - 35, enemies[i].enemyPos.Y - 20, 90, 100), null, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                                break;
                            }
                    }
                  
                

            }
              
            
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}