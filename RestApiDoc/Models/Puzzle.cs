using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace RestApiDoc.Models
{
    public class Puzzle
    {
        private static readonly string PUZZLE_NAME = $@"{Directory.GetCurrentDirectory()}\Data\puzzles\arhitektura.png";

        public ObservableCollection<PuzzlePiece> puzzlePiece = new();

        public void Initialize()
        {
            var bitmap = new Bitmap(PUZZLE_NAME);
            var w = bitmap.Width / 3;
            var h = bitmap.Height / 3;
            int x = 0, y = 0;

            for (int i = 1; i <= 9; i++)
            {
                var select = new Rectangle(x, y, w, h);
                var cropBmp = bitmap.Clone(select, bitmap.PixelFormat);

                puzzlePiece.Add(new PuzzlePiece
                {
                    index = i - 1,
                    Piece = Convert(cropBmp)
                });

                if (i % 3 == 0)
                {
                    y += h;
                    x = 0;
                    continue;
                }

                x += w;
            }

            var rand = new Random();

            for (int i = 0; i < 9; i++)
            {
                int random = rand.Next(0, 8);

                var buffer = puzzlePiece[i];

                puzzlePiece[i] = puzzlePiece[random];

                puzzlePiece[random] = buffer;
            }
        }

        public static BitmapImage Convert(Bitmap src)
        {
            var ms = new MemoryStream();
            src.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            var image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }

        public bool Validate(ObservableCollection<PuzzlePiece> itemPlacement)
        {
            ObservableCollection<PuzzlePiece> placement = itemPlacement;

            for (int i = 0; i < placement.Count; i++)
            {
                if ((placement.IndexOf(placement[i]) != placement[i].index) || placement.IndexOf(placement[i]) < 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}