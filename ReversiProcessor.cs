using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HideReversi
{
    public class ReversiProcessor
    {
        const int TILE_HEIGHT = 8;
        const int TILE_WIDTH = 8;

        // ゲーム版を表す
        // 0:何もなし　1：黒マス　2：白マス
        int[,] board = new int[TILE_HEIGHT, TILE_WIDTH];
        public int[,] Board
        {
            get
            {
                return board;
            }
        }

        public ReversiProcessor()
        {
            for(int i=0; i < TILE_HEIGHT * TILE_WIDTH; i++)
            {
                board[i / TILE_WIDTH, i % TILE_WIDTH] = 0;
            }
        }
        /// <summary>
        /// ゲームの盤上をクリアにし、最初の石を配置する
        /// </summary>
        public void InitializeBoard()
        {
            for (int i = 0; i < TILE_HEIGHT * TILE_WIDTH; i++)
            {
                board[i / TILE_WIDTH, i % TILE_WIDTH] = 0;
            }
            // ボード中央に白と黒を置く
            board[(TILE_HEIGHT - 1) / 2, (TILE_WIDTH - 1) / 2] = 1;
            board[(TILE_HEIGHT - 1) / 2, TILE_WIDTH / 2] = 2;
            board[TILE_HEIGHT / 2, (TILE_WIDTH - 1) / 2] = 2;
            board[TILE_HEIGHT / 2, TILE_WIDTH / 2] = 1;
        }

        /// <summary>
        /// 石が置けるかどうかを確認する
        /// </summary>
        /// <param name="posX">石が置かれた位置：横</param>
        /// <param name="posY">石が置かれた位置：縦</param>
        /// <param name="stone">置いた石の種類</param>
        public bool CheckPutable(int posX, int posY)
        {
            // 配置したい場所にほかの石が置かれていたら配置不可能
            if(board[posY, posX] != 0)
            {
                return false;
            }

            // 周辺８マスに一つでも石があれば配置可能
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    // 
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }
                    if (posX + i < 0 || posX + i >= TILE_WIDTH ||
                        posY + j < 0 || posY + j >= TILE_HEIGHT)
                    {
                        continue;
                    }
                    if (board[posY + j, posX + i] != 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 指定の位置に石を配置する
        /// </summary>
        /// <param name="posX">配置位置：横</param>
        /// <param name="posY">配置位置：縦</param>
        /// <param name="stone">置く石の種類</param>
        public void PutStone(int posX, int posY, int stone)
        {
            board[posY, posX] = stone;
        }

        /// <summary>
        /// 石の反転処理を行う
        /// </summary>
        /// <param name="posX">石が置かれた位置：横</param>
        /// <param name="posY">石が置かれた位置：縦</param>
        /// <param name="stone">置いた石の種類</param>
        public void ReverseStone(int posX, int posY, int stone)
        {
            for(int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }
                    SerchStone(posX, posY, i, j, stone);
                }
            }
        }

        /// <summary>
        /// ゲームが終わったかどうかを判定する
        /// </summary>
        /// <remarks>true：ゲーム終了、false:ゲーム続行可能</remarks>
        /// <returns></returns>
        public bool CheckGameEnd()
        {
            for(int i = 0; i < TILE_HEIGHT; i++)
            {
                for(int j = 0; j < TILE_WIDTH; j++)
                {
                    if(board[i, j] == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 石が反転できるかを一定方向に向かって調べる
        /// </summary>
        /// <param name="posX">基準位置：横</param>
        /// <param name="posY">基準位置：縦</param>
        /// <param name="directionX">調査する方向：横</param>
        /// <param name="directionY">調査する方向：縦</param>
        /// <param name="stone">置いた石の種類</param>
        /// <returns></returns>
        bool SerchStone(int posX, int posY, int directionX, int directionY, int stone)
        {
            int serchPosX = posX + directionX;
            int serchPosY = posY + directionY;

            if(serchPosX > TILE_WIDTH - 1 || serchPosY > TILE_HEIGHT - 1 ||
                serchPosX < 0 || serchPosY < 0)
            {
                return false;
            }

            if(board[serchPosY, serchPosX] == stone)
            {
                return true;
            }
            else if(board[serchPosY, serchPosX] != 0)
            {
                if(SerchStone(serchPosX, serchPosY, directionX, directionY, stone) == true)
                {
                    board[serchPosY, serchPosX] = stone;
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 石の配置個数を数える
        /// </summary>
        /// <param name="stoneID"></param>
        /// <returns></returns>
        public int CountStone(int stoneID)
        {
            int countStone = 0;
            for(int i= 0; i < TILE_HEIGHT; i++)
            {
                for (int j = 0; j < TILE_WIDTH; j++)
                {
                    if(board[i, j] == stoneID)
                    {
                        countStone++;
                    }
                }
            }
            return countStone;
        }
    }
}
