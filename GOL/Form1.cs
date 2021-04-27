using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOL
{
    public partial class Form1 : Form
    {
        private const int UNIVERSE_WIDTH = 20;
        private const int UNIVERSE_HEIGHT = 20;

        // The universe array
        bool[,] universe = new bool[UNIVERSE_WIDTH, UNIVERSE_HEIGHT];
        bool[,] scratchPad = new bool[UNIVERSE_WIDTH, UNIVERSE_HEIGHT];
        // Drawing colors        
        Color gridColor = Color.Black;
        Color cellColor = Color.Gray;

        // The Timer class
        Timer timer = new Timer();

        // Generation count
        int generations = 0;

        bool canTick;

        private int m_currentSeed;

        public Form1()
        {
            InitializeComponent();

            // Setup the timer
            timer.Interval = 100; // milliseconds
            timer.Tick += Timer_Tick;
            timer.Enabled = true; // start timer running

            pauseSimButton.Enabled = false;
            stopSimButton.Enabled = false;
            stepSimButton.Enabled = false;
            canTick = false;

            Random rand = new Random();
            m_currentSeed = rand.Next(1000, int.MaxValue);
        }

        // Calculate the next generation of cells
        private void NextGeneration()
        {

           

            // Increment generation count
            generations++;

           

            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
        }

        // The event called by the timer every Interval milliseconds.
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!canTick)
                return;

           
            NextGeneration();
             PerformSimulationOnScratchpad();
        }

        private void graphicsPanel1_Paint(object sender, PaintEventArgs e)
        {
            // Calculate the width and height of each cell in pixels
            // CELL WIDTH = WINDOW WIDTH / NUMBER OF CELLS IN X
            int cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);
            // CELL HEIGHT = WINDOW HEIGHT / NUMBER OF CELLS IN Y
            int cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);


            // A Pen for drawing the grid lines (color, width)
            Pen gridPen = new Pen(gridColor, 1);


            // A Brush for filling living cells interiors (color)
            Brush cellBrush = new SolidBrush(cellColor);

            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    // A rectangle to represent each cell in pixels
                    Rectangle cellRect = Rectangle.Empty;
                    cellRect.X = x * cellWidth;
                    cellRect.Y = y * cellHeight;
                    cellRect.Width = cellWidth;
                    cellRect.Height = cellHeight;


                    // Fill the cell with a brush if alive
                    if (universe[x, y] == true)
                    {
                        e.Graphics.FillRectangle(cellBrush, cellRect);
                    }

                    // Outline the cell with a pen
                    e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);

                    //evaluate neighbors
                    int neighbors = 0;
                    int deadCellNeighbors = 0;
                    EvaluateNeighbors(universe,x, y,ref neighbors,ref deadCellNeighbors ,e,cellRect);
                    PrintNeighborCount(neighbors, e, cellRect);
                    PrintNeighborCount(deadCellNeighbors, e, cellRect);
                }
            }

            
            // Cleaning up pens and brushes
            gridPen.Dispose();
            cellBrush.Dispose();
        }

        private void graphicsPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            // If the left mouse button was clicked
            if (e.Button == MouseButtons.Left)
            {
                // Calculate the width and height of each cell in pixels
                int cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);
                int cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);

                // Calculate the cell that was clicked in
                // CELL X = MOUSE X / CELL WIDTH
                int x = e.X / cellWidth;
                // CELL Y = MOUSE Y / CELL HEIGHT
                int y = e.Y / cellHeight;

                // Toggle the cell's state
                universe[x, y] = !universe[x, y];

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    universe[x, y] = false;
                }
            }
            ResetGenerations();
            graphicsPanel1.Invalidate();
        }

        private void startSimButton_Click(object sender, EventArgs e)
        {
            HandleOnStart();
        }

        private void pauseSimButton_Click(object sender, EventArgs e)
        {
            HandleOnPause();
        }

        private void stopSimButton_Click(object sender, EventArgs e)
        {
            HandleOnStop();
           
        }

        private void stepSimButton_Click(object sender, EventArgs e)
        {
            NextGeneration();
            PerformSimulationOnScratchpad();
        }

        #region Utility
        private void HandleOnPause()
        {
            canTick = !canTick;
            stepSimButton.Enabled = true;
            startSimButton.Enabled = true;
        }
        private void HandleOnStop()
        {
            canTick = false;
            startSimButton.Enabled = true;
            stopSimButton.Enabled = false;
            pauseSimButton.Enabled = false;
            stepSimButton.Enabled = false;
        }
        private void HandleOnStart()
        {
            canTick = true;
            pauseSimButton.Enabled = true;
            stopSimButton.Enabled = true;
            startSimButton.Enabled = false;
        }
        private void ResetGenerations()
        {
            generations = 0;
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
        }
        private void EvaluateNeighbors(bool[,] _targetCollection, int _sourceX, int _sourceY,ref int _liveCellNeighbors,ref int _deadCellNeighbors ,PaintEventArgs e = null, Rectangle _cellRect = new Rectangle())
        {
            
            if (_targetCollection[_sourceX, _sourceY])
            {
                EvaluateNeighbor(_targetCollection, _sourceX + 1, _sourceY ,ref _liveCellNeighbors);
                EvaluateNeighbor(_targetCollection, _sourceX + 1, _sourceY - 1 ,ref _liveCellNeighbors);
                EvaluateNeighbor(_targetCollection, _sourceX, _sourceY - 1, ref _liveCellNeighbors);
                EvaluateNeighbor(_targetCollection, _sourceX - 1, _sourceY - 1, ref _liveCellNeighbors);
                EvaluateNeighbor(_targetCollection, _sourceX - 1, _sourceY, ref _liveCellNeighbors);
                EvaluateNeighbor(_targetCollection, _sourceX - 1, _sourceY + 1, ref _liveCellNeighbors);
                EvaluateNeighbor(_targetCollection, _sourceX, _sourceY + 1, ref _liveCellNeighbors);
                EvaluateNeighbor(_targetCollection, _sourceX + 1, _sourceY + 1, ref _liveCellNeighbors);
            }
            else
            {
                EvaluateNeighbor(_targetCollection, _sourceX + 1, _sourceY, ref _deadCellNeighbors);
                EvaluateNeighbor(_targetCollection, _sourceX + 1, _sourceY - 1, ref _deadCellNeighbors);
                EvaluateNeighbor(_targetCollection, _sourceX, _sourceY - 1, ref _deadCellNeighbors);
                EvaluateNeighbor(_targetCollection, _sourceX - 1, _sourceY - 1, ref _deadCellNeighbors);
                EvaluateNeighbor(_targetCollection, _sourceX - 1, _sourceY, ref _deadCellNeighbors);
                EvaluateNeighbor(_targetCollection, _sourceX - 1, _sourceY + 1, ref _deadCellNeighbors);
                EvaluateNeighbor(_targetCollection, _sourceX, _sourceY + 1, ref _deadCellNeighbors);
                EvaluateNeighbor(_targetCollection, _sourceX + 1, _sourceY + 1, ref _deadCellNeighbors);
            }
           
        }
        private void EvaluateNeighbor(bool[,] _targetCollection, int _sourceX, int _sourceY,ref int _neighborCount)
        {
            int targetX = 0;
            int targetY = 0;

            if (_sourceX < 0)
            {
                targetX = _targetCollection.GetLength(0) - 1;
            }
            else
            {
                targetX = _sourceX % _targetCollection.GetLength(0);
            }

            if (_sourceY < 0)
            {
                targetY = _targetCollection.GetLength(1) - 1;
            }
            else
            {
                targetY = _sourceY % _targetCollection.GetLength(1);
            }

           

            if (_targetCollection[targetX, targetY])
                _neighborCount++;

        }
        private void PrintNeighborCount(int _neighborCount,PaintEventArgs e, Rectangle _cellRect)
        {
            if (_neighborCount <= 0)
                return;

            Font font = new Font("Arial", 100/UNIVERSE_HEIGHT);

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            e.Graphics.DrawString(_neighborCount.ToString(), font, Brushes.Black, _cellRect, stringFormat);
        }
       private void CopyCollection(bool[,] _source, ref bool[,] _target)
        {
            for (int y = 0; y < _source.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < _source.GetLength(0); x++)
                {
                    _target[x, y] = _source[x, y];
                }
            }
        }
        private void SwapCollections(ref bool[,] _target, ref bool[,] _source)
        {
            bool[,] temp = _source;
            _source = _target;
            _target = temp;
        }
        private void PerformSimulationOnScratchpad()
        {
            CopyCollection(universe, ref scratchPad);

            for (int y = 0; y < scratchPad.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < scratchPad.GetLength(0); x++)
                {
                    int neighborCount = 0;
                    int deadCellNeighborCount = 0;
                    EvaluateNeighbors(universe, x, y, ref neighborCount, ref deadCellNeighborCount);
                    GOLRuleOne(x, y,neighborCount);
                    GOLRuleTwo(x, y, neighborCount);
                    GOLRuleThree(x, y, neighborCount);
                    GOLRuleFour(x, y, deadCellNeighborCount);
                }
            }

            SwapCollections(ref universe, ref scratchPad);
            graphicsPanel1.Invalidate();
        }
        /// <summary>
        /// Living cells with less than 2 living neighbors die in the next generation
        /// </summary>
        /// <param name="_sourceX"></param>
        /// <param name="_sourceY"></param>
        private void GOLRuleOne(int _sourceX, int _sourceY, int _neighborCount)
        {
            if (!universe[_sourceX, _sourceY])
                return;
            if(_neighborCount < 2)
            {
                scratchPad[_sourceX, _sourceY] = false;
                
            }
        }
        /// <summary>
        /// Living cells with more than 3 living neighbors die in the next generation.
        /// </summary>
        /// <param name="_sourceX"></param>
        /// <param name="_sourceY"></param>
        /// <param name="_neighborCount"></param>
        private void GOLRuleTwo(int _sourceX, int _sourceY, int _neighborCount)
        {
            if (!universe[_sourceX, _sourceY])
                return;
            if (_neighborCount > 3)
            {
                scratchPad[_sourceX, _sourceY] = false;
            }
        }
        /// <summary>
        /// Living cells with 2 or 3 living neighbors live in the next generation.
        /// </summary>
        /// <param name="_sourceX"></param>
        /// <param name="_sourceY"></param>
        /// <param name="_neighborCount"></param>
        private void GOLRuleThree(int _sourceX, int _sourceY, int _neighborCount)
        {
            if (!universe[_sourceX, _sourceY])
                return;
            if (_neighborCount == 2 ||_neighborCount == 3)
            {
                scratchPad[_sourceX, _sourceY] = true;
               
            }
        }
        /// <summary>
        /// Dead cells with exactly 3 living neighbors live in the next generation.
        /// </summary>
        /// <param name="_sourceX"></param>
        /// <param name="_sourceY"></param>
        /// <param name="_neighborCount"></param>
        private void GOLRuleFour(int _sourceX, int _sourceY, int _neighborCount)
        {
            if (universe[_sourceX, _sourceY])
                return;
            if ( _neighborCount == 3)
            {
                scratchPad[_sourceX, _sourceY] = true;
              
            }
        }
        #endregion

        private void randomUniverse_Click(object sender, EventArgs e)
        {
            ModalRandom mr = new ModalRandom();

        }
    }
}
