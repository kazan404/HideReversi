using System.Windows;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Resources;
using System;
using System.IO;
using System.Windows.Media;

namespace HideReversi
{
    class DrawingManager
    {
        public const int PANEL_CHIP_WIDTH = 36;
        public const int PANEL_CHIP_HEIGHT = 36;

        BitmapImage allPanelPiece;

        CroppedBitmap panelCroppedImage;
        List<CroppedBitmap> stoneCroppedImage = new List<CroppedBitmap>();

        public DrawingManager()
        {
            allPanelPiece = new BitmapImage();
            allPanelPiece.BeginInit();
            allPanelPiece.UriSource = new Uri(@".\img\tile.png", UriKind.Relative);
            allPanelPiece.CacheOption = BitmapCacheOption.OnLoad;
            allPanelPiece.EndInit();
            // allPanelPiece.StreamSource = Properties.Resources.ResourceManager.GetStream("Tile.resx");

            Int32Rect rect = new Int32Rect(0, 3 * PANEL_CHIP_HEIGHT, PANEL_CHIP_WIDTH, PANEL_CHIP_HEIGHT);
            panelCroppedImage = new CroppedBitmap(allPanelPiece, rect);
            //panelCroppedImage.Source = allPanelPiece;
            //panelCroppedImage.SourceRect = rect;

            for (int i = 0; i < 3; i++)
            {
                rect.X = 0;
                rect.Y = i * PANEL_CHIP_HEIGHT;
                rect.Width = PANEL_CHIP_WIDTH;
                rect.Height = PANEL_CHIP_HEIGHT;
                stoneCroppedImage.Add(new CroppedBitmap(allPanelPiece, rect));
            }

        }

        /// <summary>
        /// 石の種類を隠して盤面を描画する
        /// </summary>
        /// <param name="drawBoard"></param>
        /// <param name="board"></param>
        public void DrawingHideBoard(Canvas drawBoard, int[,] board)
        {
            List<Image> drawBoardPanelList = new List<Image>();
            drawBoard.Children.Clear();
            for(int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    switch(board[i,j])
                    {
                        case 0:
                            drawBoardPanelList.Add(new Image());
                            Canvas.SetTop(drawBoardPanelList[drawBoardPanelList.Count - 1], i * PANEL_CHIP_HEIGHT);
                            Canvas.SetLeft(drawBoardPanelList[drawBoardPanelList.Count - 1], j * PANEL_CHIP_WIDTH);
                            drawBoardPanelList[drawBoardPanelList.Count - 1].Source = panelCroppedImage;
                            drawBoard.Children.Add(drawBoardPanelList[drawBoardPanelList.Count - 1]);
                            break;
                        case 1:
                        case 2:
                            drawBoardPanelList.Add(new Image());
                            Canvas.SetTop(drawBoardPanelList[drawBoardPanelList.Count - 1], i * PANEL_CHIP_HEIGHT);
                            Canvas.SetLeft(drawBoardPanelList[drawBoardPanelList.Count - 1], j * PANEL_CHIP_WIDTH);
                            drawBoardPanelList[drawBoardPanelList.Count - 1].Source = stoneCroppedImage[0];
                            drawBoard.Children.Add(drawBoardPanelList[drawBoardPanelList.Count - 1]);
                            break;
                        default:
                            drawBoardPanelList.Add(new Image());
                            Canvas.SetTop(drawBoardPanelList[drawBoardPanelList.Count - 1], i * PANEL_CHIP_HEIGHT);
                            Canvas.SetLeft(drawBoardPanelList[drawBoardPanelList.Count - 1], j * PANEL_CHIP_WIDTH);
                            drawBoardPanelList[drawBoardPanelList.Count - 1].Source = panelCroppedImage;
                            drawBoard.Children.Add(drawBoardPanelList[drawBoardPanelList.Count - 1]);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 石の種類を表示して盤面を描画する
        /// </summary>
        /// <param name="drawBoard"></param>
        /// <param name="board"></param>
        public void DrawingOpenBoard(Canvas drawBoard, int[,] board)
        {
            List<Image> drawBoardPanelList = new List<Image>();
            drawBoard.Children.Clear();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    switch (board[i, j])
                    {
                        case 0:
                            drawBoardPanelList.Add(new Image());
                            Canvas.SetTop(drawBoardPanelList[drawBoardPanelList.Count - 1], i * PANEL_CHIP_HEIGHT);
                            Canvas.SetLeft(drawBoardPanelList[drawBoardPanelList.Count - 1], j * PANEL_CHIP_WIDTH);
                            drawBoardPanelList[drawBoardPanelList.Count - 1].Source = panelCroppedImage;
                            drawBoard.Children.Add(drawBoardPanelList[drawBoardPanelList.Count - 1]);
                            break;
                        case 1:
                            drawBoardPanelList.Add(new Image());
                            Canvas.SetTop(drawBoardPanelList[drawBoardPanelList.Count - 1], i * PANEL_CHIP_HEIGHT);
                            Canvas.SetLeft(drawBoardPanelList[drawBoardPanelList.Count - 1], j * PANEL_CHIP_WIDTH);
                            drawBoardPanelList[drawBoardPanelList.Count - 1].Source = stoneCroppedImage[1];
                            drawBoard.Children.Add(drawBoardPanelList[drawBoardPanelList.Count - 1]);
                            break;
                        case 2:
                            drawBoardPanelList.Add(new Image());
                            Canvas.SetTop(drawBoardPanelList[drawBoardPanelList.Count - 1], i * PANEL_CHIP_HEIGHT);
                            Canvas.SetLeft(drawBoardPanelList[drawBoardPanelList.Count - 1], j * PANEL_CHIP_WIDTH);
                            drawBoardPanelList[drawBoardPanelList.Count - 1].Source = stoneCroppedImage[2];
                            drawBoard.Children.Add(drawBoardPanelList[drawBoardPanelList.Count - 1]);
                            break;
                        default:
                            drawBoardPanelList.Add(new Image());
                            Canvas.SetTop(drawBoardPanelList[drawBoardPanelList.Count - 1], i * PANEL_CHIP_HEIGHT);
                            Canvas.SetLeft(drawBoardPanelList[drawBoardPanelList.Count - 1], j * PANEL_CHIP_WIDTH);
                            drawBoardPanelList[drawBoardPanelList.Count - 1].Source = panelCroppedImage;
                            drawBoard.Children.Add(drawBoardPanelList[drawBoardPanelList.Count - 1]);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Canvas上に石やパネルを一枚だけ描画する
        /// </summary>
        /// <param name="drawNextArea"></param>
        /// <param name="stoneID"></param>
        public void DrawSingleStone(Canvas drawNextArea, int stoneID)
        {
            Image nextStone = new Image();
            switch(stoneID)
            {
                case 1:
                    nextStone.Source = stoneCroppedImage[1];
                    drawNextArea.Children.Add(nextStone);
                    break;
                case 2:
                    nextStone.Source = stoneCroppedImage[2];
                    drawNextArea.Children.Add(nextStone);
                    break;
                default:
                    nextStone.Source = stoneCroppedImage[0];
                    drawNextArea.Children.Add(nextStone);
                    break;
            }

        }

        /// <summary>
        /// 石の個数をLabelに描画する
        /// </summary>
        /// <param name="counterLabel"></param>
        /// <param name="count"></param>
        public void DrawStoneCount(Label counterLabel, int count)
        {
            counterLabel.Content = count;
        }
    }
}
