using Game1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using Project1.GeneticAlgotithm;
using System;

namespace Project1
{
    
    public enum Directions {  RIGHT, LEFT, UP  };
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;

        private List<Vector3> track_heights = new List<Vector3> {
            new Vector3(-10,0,0),
            new Vector3(-7,0,0),
            new Vector3(-4,0,0),
            new Vector3(-1,0,0),
            new Vector3(2,0,0),
            //new Vector3(2,3,0),
            ////new Vector3(5, 6, 0),
            new Vector3(5,0,0),
            new Vector3(8,0,0),
            new Vector3(11,0,0),
            new Vector3(14,0,0)
            };

        private Track track;
        private List<Player> players = new List<Player>();
        private bool going = false;

        private const int population_size =4,
            elitism_count = 2,
            tournament_size = 3,
            max_generation = 800;
        private const double mutation_rate = 0.005,
            crossover_rate = 0.95;
        private GeneticAlgorithm ga; 
        private Population population;
        private int generation = 0, geneIndex = 0;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            this._graphics.PreferredBackBufferWidth = 1600;
            this._graphics.PreferredBackBufferHeight = 900;
            _graphics.ApplyChanges();
            ga = new GeneticAlgorithm(population_size, mutation_rate, crossover_rate, elitism_count, tournament_size);
        }

        protected override void Initialize()
        {
            population = ga.initPopulaion();
            track = new Track(track_heights);
            for (int i = 1; i <= population_size; i++)
                players.Add(new Player("Player " + i, track.getStartPos())); 
            base.Initialize();
        }

        protected override void LoadContent()
        {
            track.load(GraphicsDevice);
            foreach (var player in players)
            {
                player.load(GraphicsDevice);
            }        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Escape))
                Exit();
            if (state.IsKeyDown(Keys.Space) && !going)
                going = true;
            if (!going)
            {
                base.Update(gameTime);
                return;
            }

            //minden játékos-egyed párosra
            for (int idx = 0; idx < population_size; idx++) {
                Player player = players[idx];
                Individual individual = population.getIndividual(idx);

                if (!player.isThisMoving())
                {
                    //távolság a talajtól
                    Vector3 fallMove = track.fallTo(player.Position);
                    //leesés
                    if (fallMove == Vector3.Up)
                    {
                        player.restart(track.getStartPos() + Vector3.Up * 3);
                        individual.Fitness -= 10;
                    }

                    if (!player.won)
                    {
                        //ha még van kijátszatlan lépése
                        if ((population.getChomosomeLength() > geneIndex))
                        {
                            Directions next = individual.getGene(geneIndex);
                            movePlayer(idx, next);
                            geneIndex++;
                        }
                        //végigmentünk az egyedek lépésein
                        else
                        {
                            if (!ga.isTerminationConditionMet(population) &&  //a legfitebb
                                !ga.isTerminationConditionMet(generation, max_generation)) //túl sok generáció volt már
                            {

                                foreach (var ind in population.getPopulation())
                                {
                                    int i = 1;
                                    Directions last = ind.getGene(0);
                                    while(i < ind.getChromosomeLength())
                                    {
                                        Directions now = ind.getGene(i);
                                        if (((last == Directions.RIGHT || last == Directions.UP) && now == Directions.LEFT) ||
                                            (last == Directions.LEFT && (now == Directions.RIGHT || now == Directions.UP)))
                                            ind.Fitness -= 2;
                                        last = now;
                                        i++;
                                    }
                                }
                                Debug.WriteLine(generation + ". generation is over, max fitness is " + population.getFittest(0).Fitness + ".");
                                    
                                foreach (var temp in population.getPopulation())
                                {
                                    Debug.Write(temp.Fitness + "\t");
                                }
                                
                                newGeneration();
                                Debug.WriteLine("\n");
                            }
                            else
                            {
                                //játék vége
                                Debug.WriteLine("The game has ended!");
                                going = false;
                            }
                        }
                    }
                    if (fallMove != Vector3.Zero)
                        player.nextMoveTo(fallMove);
                }
                else
                {
                    player.move();
                    if (player.Position == (track.getGoalPos() + Vector3.Up * 3))
                    {
    Debug.WriteLine(players[idx].Name + " reached the goal.");
                        individual.Fitness += 1000;
                        player.win();
                    }
                }
            }
            base.Update(gameTime);
        }

        private void newGeneration()
        {
            population = ga.crossoverPopulation(population);
            population = ga.mutateRandom(population);
            generation++;
            geneIndex = 0;
            foreach (var player in players)
            {
                player.reset(track.getStartPos()+Vector3.Up*3);
            }
        }
        void movePlayer(int idx, Directions d)
        {
            switch (d)
            {
                case Directions.RIGHT:
                    if (!track.isBlocked(players[idx].Position + Vector3.Right * 3))
                    {
                        players[idx].nextMoveTo(Vector3.Right * 3);
                        population.getIndividual(idx).Fitness += 5;
                    }
                    break;
                case Directions.LEFT:
                    if (!track.isBlocked(players[idx].Position + Vector3.Left * 3))
                    {
                        players[idx].nextMoveTo(Vector3.Left * 3);
                        population.getIndividual(idx).Fitness -= 3;
                    }
                        break;
                case Directions.UP:
                    if (!track.isBlocked(players[idx].Position + Vector3.Up * 3 + Vector3.Right * 3))
                    {
                        players[idx].nextMoveTo(Vector3.Up * 3 + Vector3.Right * 3);
                        population.getIndividual(idx).Fitness += 5;
                    }
                        break;
                default:
                    break;
            }
        }

        public void end() {  }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            Camera.Main.AspectRatio = GraphicsDevice.Viewport.AspectRatio;
            track.draw(Camera.Main);
            foreach (var player in players)
            {
                player.draw(Camera.Main);
            }

            base.Draw(gameTime);
        }
    }
}
