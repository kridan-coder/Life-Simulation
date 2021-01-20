using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class PerlinNoise
    {
        private byte[] PermutationTable = new byte[1024];
        private Random Rand = new Random();
        private const float K = 0.02f;

        public PerlinNoise()
        {
            Rand.NextBytes(PermutationTable);
        }

        static float Lerp(float a, float b, float t)
        {
            // return a * (t - 1) + b * t; можно переписать с одним умножением (раскрыть скобки, взять в другие скобки):
            return a + (b - a) * t;
        }
        static float QunticCurve(float t)
        {
            return t * t * t * (t * (t * 6 - 15) + 10);
        }
        float[] GetPseudoRandomGradientVector(int x, int y)
        {
            // хэш-функция с Простыми числами, обрезкой результата до размера массива со случайными байтами
            int v = (int)(((x * 1836311903) ^ (y * 2971215073) + 4807526976) & 1023);
            v = PermutationTable[v] & 3;

            switch (v)
            {
                case 0: return new float[] { 1, 0 };
                case 1: return new float[] { -1, 0 };
                case 2: return new float[] { 0, 1 };
                default: return new float[] { 0, -1 };
            }
        }
        static float Dot(float[] a, float[] b)
        {
            return a[0] * b[0] + a[1] * b[1];
        }
        public float Noise(float fx, float fy)
        {
            // сразу находим координаты левой верхней вершины квадрата
            int left = (int)System.Math.Floor(fx);
            int top = (int)System.Math.Floor(fy);

            // а теперь локальные координаты точки внутри квадрата
            float pointInQuadX = fx - left;
            float pointInQuadY = fy - top;

            // извлекаем градиентные векторы для всех вершин квадрата:
            float[] topLeftGradient = GetPseudoRandomGradientVector(left, top);
            float[] topRightGradient = GetPseudoRandomGradientVector(left + 1, top);
            float[] bottomLeftGradient = GetPseudoRandomGradientVector(left, top + 1);
            float[] bottomRightGradient = GetPseudoRandomGradientVector(left + 1, top + 1);

            // вектора от вершин квадрата до точки внутри квадрата:
            float[] distanceToTopLeft = new float[] { pointInQuadX, pointInQuadY };
            float[] distanceToTopRight = new float[] { pointInQuadX - 1, pointInQuadY };
            float[] distanceToBottomLeft = new float[] { pointInQuadX, pointInQuadY - 1 };
            float[] distanceToBottomRight = new float[] { pointInQuadX - 1, pointInQuadY - 1 };

            // считаем скалярные произведения между которыми будем интерполировать
            /*
             tx1--tx2
              |    |
             bx1--bx2
            */

            float tx1 = Dot(distanceToTopLeft, topLeftGradient);
            float tx2 = Dot(distanceToTopRight, topRightGradient);
            float bx1 = Dot(distanceToBottomLeft, bottomLeftGradient);
            float bx2 = Dot(distanceToBottomRight, bottomRightGradient);

            // готовим параметры интерполяции, чтобы она не была линейной:
            pointInQuadX = QunticCurve(pointInQuadX);
            pointInQuadY = QunticCurve(pointInQuadY);

            // собственно, интерполяция:
            float tx = Lerp(tx1, tx2, pointInQuadX);
            float bx = Lerp(bx1, bx2, pointInQuadX);
            float tb = Lerp(tx, bx, pointInQuadY);

            // возвращаем результат:
            return tb;
        }

        public float MultiOctaveNoise(float fx, float fy, int octaves, float persistence = 0.5f)
        {

            fx *= K;
            fy *= K;

            float amplitude = 1; // сила применения шума к общей картине, будет уменьшаться с "мельчанием" шума
                                 // как сильно уменьшаться - регулирует persistence
            float max = 0; // необходимо для нормализации результата
            float result = 0; // накопитель результата

            while (octaves-- > 0)
            {
                max += amplitude;
                result += Noise(fx, fy) * amplitude;
                amplitude *= persistence;
                fx *= 2; // удваиваем частоту шума (делаем его более мелким) с каждой октавой
                fy *= 2;
            }

            return result / max;
        }
    }
}
