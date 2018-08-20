using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HideReversi
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        GameManager gameManager = new GameManager();
        GameManager.GameState processResult;

        public MainWindow()
        {
            InitializeComponent();

            gameManager.StartGame(Canvas_board, Canvas_next, Canvas_stone1, Canvas_stone2);
        }

        /// <summary>
        /// マウスの左ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Canvas_board_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (processResult == GameManager.GameState.GAME_END)
            {
                return;
            }

            Point mousePoint = e.GetPosition(Canvas_board);

            int cellX = (int)Math.Floor(mousePoint.X / GameManager.panelWidth);
            int cellY = (int)Math.Floor(mousePoint.Y / GameManager.panelHeight);

            processResult = gameManager.ProcessingGame(cellX, cellY, Canvas_board, Canvas_next, Label_stone1, Label_stone2);

            if(processResult == GameManager.GameState.GAME_END)
            {
                gameManager.EndGame(Canvas_board);
                return;
            }

            return;
        }

        private void Canvas_board_MouseLeave(object sender, MouseEventArgs e)
        {
            // gameManager.EndGame(Canvas_board);

            return;
        }
    }
}
