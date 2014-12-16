using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Common.ClassLibrary.Utils
{
    public class BitmapUtils
    {
        #region グラデーションイメージ

        /// <summary>
        /// グラデーションのビットマップを返却する
        /// </summary>
        /// <param name="Width">横幅</param>
        /// <param name="Heigth">縦幅</param>
        /// <param name="StartColor">グラデーションの開始色</param>
        /// <param name="EndColor">グラデーションの終了色</param>
        /// <param name="GradientMode">1=右上から左下　2=左上から右下　3=左から右　4=上から下</param>
        /// <returns></returns>
        public static Bitmap GetGradationImage(int Width, int Heigth, Color StartColor, Color EndColor, int GradientMode)
        {

            /* ビットマップとGraphicsオブジェクトの作成 */
            Bitmap bmp = new Bitmap(Width, Heigth);
            Graphics g = Graphics.FromImage(bmp);

            LinearGradientBrush gradBrush = null;
            /* グラデーション・ブラシの作成 */
            // 縦方向にグラデーション //

            switch (GradientMode)
            {
                case 1:
                    gradBrush = new LinearGradientBrush(g.VisibleClipBounds, StartColor, EndColor, LinearGradientMode.BackwardDiagonal);
                    break;
                case 2:
                    gradBrush = new LinearGradientBrush(g.VisibleClipBounds, StartColor, EndColor, LinearGradientMode.ForwardDiagonal);
                    break;
                case 3:
                    gradBrush = new LinearGradientBrush(g.VisibleClipBounds, StartColor, EndColor, LinearGradientMode.Horizontal);
                    break;
                case 4:
                    gradBrush = new LinearGradientBrush(g.VisibleClipBounds, StartColor, EndColor, LinearGradientMode.Vertical);
                    break;
                default:
                    gradBrush = new LinearGradientBrush(g.VisibleClipBounds, StartColor, EndColor, LinearGradientMode.Vertical);
                    break;


            }
            /* ビットマップをグラデーション・ブラシで塗る */
            g.FillRectangle(gradBrush, g.VisibleClipBounds);

            gradBrush.Dispose();
            g.Dispose();

            return bmp;

        }

        #endregion

    }
}
