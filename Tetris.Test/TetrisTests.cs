using System.Collections.Concurrent;
using System.Drawing;
using Tetris.Models;

namespace Tetris.Test
{
    [TestClass]
    public class ShapeTests
    {
        private List<Square> CreateField(int width, int height)
        {
            var result = new List<Square>();
            for(var i = 0; i < width * 20; i += 20)
            {
                for(var j = 0; j < height * 20; j += 20)
                {
                    result.Add(new Square
                    {
                        X = i,
                        Y = j,
                        Color = null
                    });
                }
            }
            return result;
        }

        [TestMethod]
        [DataRow(5, true)]
        [DataRow(10, false)]
        public void MoveDownTest(int squaresCount, bool block)
        {
            var field = CreateField(100, 100);
            var shape = new Shape
            {
                BlockShape = block,
                Squares = new ConcurrentBag<Square>(field.Take(squaresCount))
            };

            foreach(var square in shape.Squares)
                square.Color = Color.Green;
            

            Bitmap bitmap = new Bitmap(1000, 1000, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);

            var valid = shape.MoveDown(graphics, new ConcurrentBag<Square>(field));

            Assert.IsTrue(valid != block);
            Assert.IsTrue(shape.Squares.Count == squaresCount);

            if(!block)
                Assert.IsFalse(shape.BlockShape);
        }

        [TestMethod]
        [DataRow(5, true)]
        [DataRow(10, false)]
        public void MoveRightTest(int squaresCount, bool block)
        {
            var field = CreateField(100, 100);
            var shape = new Shape
            {
                BlockShape = block,
                Squares = new ConcurrentBag<Square>(field.Take(squaresCount))
            };

            foreach (var square in shape.Squares)
                square.Color = Color.Green;


            Bitmap bitmap = new Bitmap(1000, 1000, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);

            var valid = shape.MoveRight(graphics, new ConcurrentBag<Square>(field));

            Assert.IsTrue(valid != block);
            Assert.IsTrue(shape.Squares.Count == squaresCount);

            if (!block)
                Assert.IsFalse(shape.BlockShape);
        }


        [TestMethod]
        [DataRow(5, true)]
        [DataRow(10, false)]
        public void MoveLeftTest(int squaresCount, bool block)
        {
            var field = CreateField(100, 100);
            var shape = new Shape
            {
                BlockShape = block,
                Squares = new ConcurrentBag<Square>(field.TakeLast(squaresCount))
            };

            foreach (var square in shape.Squares)
                square.Color = Color.Green;


            Bitmap bitmap = new Bitmap(1000, 1000, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);

            var valid = shape.MoveLeft(graphics, new ConcurrentBag<Square>(field));

            Assert.IsTrue(valid != block);
            Assert.IsTrue(shape.Squares.Count == squaresCount);

            if (!block)
                Assert.IsFalse(shape.BlockShape);
        }


        [TestMethod]
        [DataRow(5, true)]
        [DataRow(10, false)]
        public void RotateTest(int squaresCount, bool block)
        {
            var field = CreateField(100, 100);
            var shape = new Shape
            {
                BlockShape = block,
                Squares = new ConcurrentBag<Square>(field.Where(c => c.X == 50 * 20 && c.Y >= 50 * 20).Take(squaresCount))
            };
            shape.CenterSquare = shape.Squares.First();

            foreach (var square in shape.Squares)
                square.Color = Color.Green;


            Bitmap bitmap = new Bitmap(1000, 1000, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);

            var valid = shape.Rotate(graphics, new ConcurrentBag<Square>(field));

            Assert.IsTrue(valid != block);
            Assert.IsTrue(shape.Squares.Count == squaresCount);

            if (!block)
                Assert.IsFalse(shape.BlockShape);
        }
    }
}

