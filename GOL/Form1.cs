using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOL
{
    public partial class Form1 : Form
    {

        public enum BoundaryType
        {
            Torodial,
            Finite
        }

        StringBuilder sb;

        private int m_univerWidth = 20;
        private int m_universeHeight = 20;

        bool[,] universe;
        bool[,] scratchPad; 
        // Drawing colors        
        Color gridColor = Color.Black;
        Color cellColor = Color.Gray;

        // The Timer class
        Timer timer = new Timer();

        // Generation count
        int generations = 0;

        bool canTick;

        private int m_currentSeed;
        private bool m_useSeed;
        private bool m_useRandom;
        private int m_livingCells;
        private int m_generationalDelay;
        private bool m_displayGrid;
        private bool m_displayNeighborCount;
        private bool m_displayHud;
        private BoundaryType m_boundaryType;
        public Form1()
        {
            InitializeComponent();

            // Setup the timer
            m_generationalDelay = 100;
            timer.Interval = m_generationalDelay; // milliseconds
            timer.Tick += Timer_Tick;
            timer.Enabled = true; // start timer running

            pauseSimButton.Enabled = false;
            stopSimButton.Enabled = false;
            stepSimButton.Enabled = false;
            canTick = false;

            Random rand = new Random();
            m_currentSeed = rand.Next(1000, int.MaxValue);
            m_useSeed = false;
            m_useRandom = false;

            universe = new bool[m_univerWidth, m_universeHeight];
           scratchPad = new bool[m_univerWidth, m_universeHeight];
            m_livingCells = 0;
            m_displayGrid = true;
            m_displayNeighborCount = true;
            m_displayHud = true;
            sb = new StringBuilder();
            m_boundaryType = BoundaryType.Torodial;
        }
        #region Template

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
            UpdateLivingCellsDisplay();
            PrintHUD();
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
                    if(m_displayGrid)
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
                
                UpdateLivingCellsDisplay();
                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewUniverse();
        }

        #endregion




        #region Utility
        private void CreateNewUniverse()
        {
            Random r;
            if (m_useSeed)
            {
                r = new Random(m_currentSeed);
            }
            else
            {
                r = new Random();
            }


            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    if (m_useRandom)
                    {
                        universe[x, y] = r.Next(0, 2) > 0;
                    }
                    else
                    {
                        universe[x, y] = false;
                    }
                    UpdateLivingCellsDisplay();
                }
            }
            ResetStatusBar();
            randomUniverse.Enabled = true;
            graphicsPanel1.Invalidate();
        }
        private void UpdateLivingCellsDisplay()
        {
            m_livingCells = 0;
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    if(universe[x,y])
                    {
                        m_livingCells++;
                    }
                }
            }
             
            toolStripLivingCells.Text = "Living Cells = " + m_livingCells.ToString();
        }
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
            randomUniverse.Enabled = false;
        }
        private void ResetStatusBar()
        {
            generations = 0;
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();

            m_livingCells = 0;
            toolStripLivingCells.Text = "Living Cells = " + m_livingCells.ToString();
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

            if (_sourceX < 0 && m_boundaryType == BoundaryType.Torodial)
            {
                targetX = _targetCollection.GetLength(0) - 1;
            }
            else if(_sourceX < 0 && m_boundaryType == BoundaryType.Finite)
            {
                targetX = 0;
            }
            else
            {
                targetX = _sourceX % _targetCollection.GetLength(0);
            }

            if (_sourceY < 0 && m_boundaryType == BoundaryType.Torodial)
            {
                targetY = _targetCollection.GetLength(1) - 1;
            }
            else if (_sourceY < 0 && m_boundaryType == BoundaryType.Finite)
            {
                targetY = 0;
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
            if (!m_displayNeighborCount)
                return;


            Font font = new Font("Arial", 100/m_universeHeight);

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
        private void PrintHUD()
        {
            hudLabel.Visible = m_displayHud;

            if (!m_displayHud)
                return;
            
            sb.AppendLine($"Generation:\t\t{generations}");
            sb.AppendLine($"Cell Count:\t\t{m_universeHeight*m_univerWidth}");
            sb.AppendLine($"Boundary:\t\t{m_boundaryType}");
            sb.AppendLine($"Universe Width:\t\t{m_univerWidth}");
            sb.AppendLine($"Universe Height:\t\t{m_universeHeight}");

            hudLabel.Text = sb.ToString();

            sb.Clear();
        }
        #endregion

        #region Events
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
            UpdateLivingCellsDisplay();
        }
        private void randomUniverse_Click(object sender, EventArgs e)
        {
            ModalRandom mr = new ModalRandom();
            mr.OnDisplay(m_currentSeed);

            if (DialogResult.OK == mr.ShowDialog())
            {
                m_useSeed = mr.UseSeed;
                m_currentSeed = mr.CurrentSeed;
                m_useRandom = mr.UseRandom;
                CreateNewUniverse();
            }

        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveToDisk();
        }
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveToDisk();
        }
        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            LoadFromDisk();
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadFromDisk();
        }
        private void settingsButton_Click(object sender, EventArgs e)
        {
            ModalSettings ms = new ModalSettings(m_universeHeight, m_univerWidth, m_generationalDelay, m_boundaryType);
            HandleOnPause();

          
            if(DialogResult.OK == ms.ShowDialog())
            {
                int newHeight = ms.UniverseHeight;
                int newWidth = ms.UniverseWidth;
                m_generationalDelay = ms.GenerationalDelay;
                BoundaryType newBoundaryType = ms.BoundaryType;

                if(newHeight != m_universeHeight || newWidth != m_univerWidth || newBoundaryType != m_boundaryType)
                {
                    Warning warn = new Warning("WARNING: The universe will be reset with this change");
                    if(DialogResult.OK == warn.ShowDialog())
                    {
                        m_univerWidth = newWidth;
                        m_universeHeight = newHeight;
                        m_boundaryType = newBoundaryType;

                        universe = new bool[m_univerWidth, m_universeHeight];
                        scratchPad = new bool[m_univerWidth, m_universeHeight];


                        ResetStatusBar();
                        CreateNewUniverse();
                    }
                }

                timer.Interval = m_generationalDelay;
            }

            
        }
        private void gridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_displayGrid = gridToolStripMenuItem.Checked;
            graphicsPanel1.Invalidate();
        }

        private void neighborCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_displayNeighborCount = neighborCountToolStripMenuItem.Checked;
            graphicsPanel1.Invalidate();
        }

        private void hUDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_displayHud = hUDToolStripMenuItem.Checked;
        }
        #endregion

        #region GOL Rules

        /// <summary>
        /// Living cells with less than 2 living neighbors die in the next generation
        /// </summary>
        /// <param name="_sourceX"></param>
        /// <param name="_sourceY"></param>
        private void GOLRuleOne(int _sourceX, int _sourceY, int _neighborCount)
        {
            if (!universe[_sourceX, _sourceY])
                return;
            if (_neighborCount < 2)
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
            if (_neighborCount == 2 || _neighborCount == 3)
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
            if (_neighborCount == 3)
            {
                scratchPad[_sourceX, _sourceY] = true;

            }
        }
        #endregion

        private void SaveToDisk()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "All Files|*.*|Cells|*.cells";
            dlg.FilterIndex = 2; dlg.DefaultExt = "cells";


            if (DialogResult.OK == dlg.ShowDialog())
            {
                StreamWriter writer = new StreamWriter(dlg.FileName);

                writer.WriteLine($"Height-{Height}");
                writer.WriteLine($"Width-{Width}");
                // Iterate through the universe one row at a time.
                for (int y = 0; y < universe.GetLength(1); y++)
                {
                    // Create a string to represent the current row.
                    String currentRow = string.Empty;

                    // Iterate through the current row one cell at a time.
                    for (int x = 0; x < universe.GetLength(0); x++)
                    {
                        currentRow = (universe[x, y] == true) ? $"({x},{y})-1" : $"({x},{y})-0";
                        writer.WriteLine(currentRow);
                    }


                }

                // After all rows and columns have been written then close the file.
                writer.Close();
            }
        }
        private bool ParseUniverseDims(string _label,string _key,string _value,ref int _dimension)
        {
            if(_key.Equals(_label, StringComparison.OrdinalIgnoreCase))
            {
                if(!int.TryParse(_value, out _dimension))
                {
                    return false;
                }
            }
            return true;
        }
        private bool ParseUniverseCell(string _cellID, string _value)
        {
            //Hacky, gonna cheat a bit since we know the length of the prefix (x,y), Will be FUBAR if someone messes with the 
            //file manually
            int xPos = -1;
            int yPos = -1;
            int cellAlive = -1;

            if(!int.TryParse(_cellID[1].ToString(), out xPos))
            {
                return false;
            }
           else if(!int.TryParse(_cellID[3].ToString(), out yPos))
            {
                return false;
            }
            else if(!!int.TryParse(_value, out cellAlive))
            {
                return false;
            }

            universe[xPos, yPos] = (cellAlive == 1) ? true : false;
            return true;

        }
        private void LoadFromDisk()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "All Files|*.*|Cells|*.cells";
            dlg.FilterIndex = 2;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                StreamReader reader = new StreamReader(dlg.FileName);

                // Create a couple variables to calculate the width and height
                // of the data in the file.
              
                // Iterate through the file once to get its size.
                while (!reader.EndOfStream)
                {
                    // Read one row at a time.
                    string row = reader.ReadLine();

                    string[] entry = row.Split('-');
                    if (ParseUniverseDims(entry[0], "Height", entry[1], ref m_universeHeight))
                    {
                        entry = reader.ReadLine().Split('-');
                    }
                    else if (ParseUniverseDims(entry[0], "Width", entry[1], ref m_univerWidth))
                    {
                        entry = reader.ReadLine().Split('-');
                        universe = new bool[m_univerWidth, m_universeHeight];
                    }
                    else if (ParseUniverseCell(entry[0], entry[1])) { }
                   
                  
                }

               
                // Close the file.
                reader.Close();
            }
        }

       
    }
}
