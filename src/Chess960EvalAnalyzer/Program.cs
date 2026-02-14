using Microsoft.FSharp.Core;
using Stockfish;
using System.Globalization;
using System.Text;

namespace Chess960EvalAnalyzer
{
    public class Program
    {
        public static readonly string STOCKFISH_PATH = "stockfish-windows-x86-64-avx2.exe";
        public static readonly string POSITIONS_FILE_PATH = "chess960-positions.txt";
        public static readonly string RESULTS_FILE_PATH = "results.csv";
        public static readonly int STOCKFISH_DEPTH = 25;
        public static List<PositionInfo> POSITION_INFO_LIST = new List<PositionInfo>();

        public static void Main(string[] args)
        {
            LoadAllPositions();

            using (var stockfish = new Lib.Stockfish(FSharpOption<string>.Some(STOCKFISH_PATH), FSharpOption<int>.Some(STOCKFISH_DEPTH), null))
            {
                stockfish.UpdateEngineParameters(
                    FSharpOption<IReadOnlyDictionary<string, object>>.Some(
                        new Dictionary<string, object>
                        {
                            { "UCI_Chess960", "true" }
                        }
                    )
                );

                POSITION_INFO_LIST.ForEach(positionInfoItem => positionInfoItem.Evaluation = CalculateEval(stockfish, positionInfoItem.Placement));
            }

            ExportResults();
            Console.WriteLine($"Exported to {RESULTS_FILE_PATH}");
            Console.ReadKey();
        }

        private static void ExportResults()
        {
            var lines = POSITION_INFO_LIST
                .OrderByDescending(x => x.Evaluation)
                .Select(x => string.Format(
                    CultureInfo.InvariantCulture,
                    "{0},{1},{2}",
                    x.Index,
                    x.Evaluation,
                    x.Placement));

            File.WriteAllLines(RESULTS_FILE_PATH, lines, Encoding.UTF8);
        }

        private static void LoadAllPositions()
        {
            foreach (var (line, index) in File.ReadLines(POSITIONS_FILE_PATH).Select((line, index) => (line, index)))
            {
                POSITION_INFO_LIST.Add(new PositionInfo() { Index = index, Placement = line });
            }
        }

        public static float CalculateEval(Lib.Stockfish stockfish, string pieceOrder)
        {
            string fen = $"{pieceOrder.ToLower()}/pppppppp/8/8/8/8/PPPPPPPP/{pieceOrder.ToUpper()} w KQkq - 0 1";
            stockfish.SetFenPosition(fen, FSharpOption<bool>.Some(true));
            return Convert.ToSingle(stockfish.GetEvaluation()["value"]) / 100f;
        }
    }
}
