using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HideReversi
{
    class GameManager
    {
        /// <summary>
        /// ゲームの状態を表す列挙子
        /// </summary>
        public enum GameState
        {
            GAME_CONTINUE = 0,
            GAME_CANNOT_PUT = 1,
            GAME_END = 2,
        }

        ReversiProcessor reversiProcessor = new ReversiProcessor();
        DrawingManager drawingManager = new DrawingManager();

        int nextPut;

        public const int panelWidth = DrawingManager.PANEL_CHIP_WIDTH;
        public const int panelHeight = DrawingManager.PANEL_CHIP_HEIGHT;

        /// <summary>
        /// ゲーム開始時に必要な処理
        /// </summary>
        /// <param name="drawBoard"></param>
        /// <param name="nextPutView"></param>
        public void StartGame(Canvas drawBoard, Canvas nextPutView, Canvas stone1, Canvas stone2)
        {
            nextPut = 1;

            reversiProcessor.InitializeBoard();

            drawingManager.DrawingOpenBoard(drawBoard, reversiProcessor.Board);
            drawingManager.DrawSingleStone(nextPutView, nextPut);
            drawingManager.DrawSingleStone(stone1, 1);
            drawingManager.DrawSingleStone(stone2, 2);

            return;
        }

        /// <summary>
        /// ゲームの1ターンごとに行う処理
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <param name="drawBoard"></param>
        /// <param name="nextPutView"></param>
        /// <returns></returns>
        public GameState ProcessingGame(int posX, int posY, Canvas drawBoard, Canvas nextPutView, Label countLabel1, Label countLabel2)
        {
            if (reversiProcessor.CheckPutable(posX, posY) == false)
            {
                return GameState.GAME_CANNOT_PUT;
            }
            reversiProcessor.PutStone(posX, posY, nextPut);
            reversiProcessor.ReverseStone(posX, posY, nextPut);

            switch(nextPut)
            {
                case 1:
                    nextPut = 2;
                    break;
                case 2:
                    nextPut = 1;
                    break;
                default:
                    nextPut = 0;
                    break;
            }

            drawingManager.DrawingHideBoard(drawBoard, reversiProcessor.Board);
            //drawingManager.DrawingOpenBoard(drawBoard, reversiProcessor.Board);

            drawingManager.DrawSingleStone(nextPutView, nextPut);
            drawingManager.DrawStoneCount(countLabel1, reversiProcessor.CountStone(1));
            drawingManager.DrawStoneCount(countLabel2, reversiProcessor.CountStone(2));

            if (reversiProcessor.CheckGameEnd() == true)
            {
                return GameState.GAME_END;
            }

            return GameState.GAME_CONTINUE;
        }

        public void EndGame(Canvas drawBoard)
        {
            drawingManager.DrawingOpenBoard(drawBoard, reversiProcessor.Board);

            return;
        }
    }
}
