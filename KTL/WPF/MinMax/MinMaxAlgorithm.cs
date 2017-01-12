using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Classes;

namespace WPF.MinMax
{
    public class MinMaxAlgorithm
    {
        int LongestSeries;
        List<GameColor> AllColors;

        public MinMaxAlgorithm(int _longestSeries, List<GameColor> _allColors)
        {
            LongestSeries = _longestSeries;
            AllColors = _allColors;
        }

        public double RunMinMaxAlfaBetaPruning(List<GameCell> gameBoard, int depth,int selectedCell)
        {
            GameNode node = new GameNode(gameBoard, LongestSeries);
            node.CellToColor = selectedCell;
            return AlfaBetaPruning(node, depth, double.MinValue, double.MaxValue,false);
        }
        public double RunMinMaxClassicSelectCell (List<GameCell> gameBoard, int depth, int selectedCell)
        {
            GameNode node = new GameNode(gameBoard, LongestSeries);
            node.CellToColor = selectedCell;
            return MinMax(node, depth, false);
        }
        public double RunMinMaxClassic(List<GameCell> gameBoard, int depth)
        {
            GameNode node = new GameNode(gameBoard, LongestSeries);

            return MinMax(node, depth, true);
        }
        private double MinMax(GameNode node, int depth, bool maximizingPlayer1)
        {
            double bestValue;
            // Jezeli to jest wezel koncowy to cos z tym zrob albo faktycznie mozemy to olac i tak funkcja ewaluujaca sprawdzi
            if (depth == 0)
                return node.EvaulationFunction();

            // To napraw, aby na pewno dobrze sprawdzalo
            if (maximizingPlayer1)
            {
                bestValue = double.MinValue;
                var nodeList = GenerateNodesForPlayer1(node);
                foreach (var newNode in nodeList)
                {
                    var result = MinMax(newNode, depth - 1, false);
                    bestValue = bestValue > result ? bestValue : result;
                }
                return bestValue;
            }
            else
            {
                bestValue = double.MaxValue;
                var nodeList = GenerateNodesForPlayer2(node);
                foreach (var newNode in nodeList)
                {
                    var result = MinMax(newNode, depth - 1, true);
                    bestValue = bestValue < result ? bestValue : result;
                }
                return bestValue;
            }
        }


        private double AlfaBetaPruning(GameNode node, int depth, double alfa, double beta, bool maximizingPlayer1)
        {
            // Jezeli to jest wezel koncowy to cos z tym zrob albo faktycznie mozemy to olac i tak funkcja ewaluujaca sprawdzi
            if (depth == 0)
            {
             var retValue =   node.EvaulationFunction();
                if (retValue > 400 && node.wasExtended == false)
                {
                    depth += 6;
                    node.wasExtended = true;
                }
                else
                    return retValue;
            }
            if (maximizingPlayer1)
            {
                var nodeList = GenerateNodesForPlayer1(node);
                foreach (var newNode in nodeList)
                {
                    var result = AlfaBetaPruning(newNode, depth - 1, alfa, beta, false);
                    beta = beta < result ? beta : result;
                    if (alfa >= beta)
                    {
                        break;
                    }
                }
                if (nodeList.Count() == 0)
                    return node.EvaulationFunction(); 
                return beta;
            }
            else
            {
                var nodeList = GenerateNodesForPlayer2(node);
                foreach (var newNode in nodeList)
                {
                    var result = AlfaBetaPruning(newNode, depth - 1, alfa, beta,true);
                    alfa = alfa > result ? alfa : result;
                    if (alfa >= beta)
                    {
                        break;
                    }
                }
                if (nodeList.Count() == 0)
                    return node.EvaulationFunction();

                return alfa;
            }



        }
        private List<GameNode> GenerateNodesForPlayer2(GameNode node)
        {
            List<GameNode> retLst = new List<GameNode>();
            foreach (var item in AllColors)
            {
                GameNode nowyNode = new GameNode(node);
                nowyNode.GameBoard.FirstOrDefault(x => x.CellNumber == node.CellToColor).Color = item;
                retLst.Add(nowyNode);
            }
            return retLst;
        }

        private List<GameNode> GenerateNodesForPlayer1(GameNode node)
        {
            List<GameNode> retLst = new List<GameNode>();
            foreach (var item in node.GameBoard.Where(x => x.Color.ColorId == 0).Select(x => x.CellNumber))
            {
                GameNode nowyNode = new GameNode(node);
                nowyNode.CellToColor = item;
                retLst.Add(nowyNode);
            }

            return retLst;
        }


        public static bool IsOdd(int value)
        {
            return value % 2 != 0;
        }
    }
}
