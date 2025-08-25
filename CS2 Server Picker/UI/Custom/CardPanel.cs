using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CS2_Server_Picker.UI.Custom
{
    /// <summary>
    /// A custom panel with rounded corners, soft shadow, and configurable styling.
    /// </summary>
    public class CardPanel : Panel
    {
        // Designer-hidden properties for visual customization
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CornerRadius { get; set; } = 12;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color BorderColor { get; set; } = Color.LightGray;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int BorderThickness { get; set; } = 1;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color FillColor { get; set; } = Color.White;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ShadowSize { get; set; } = 8;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color ShadowColor { get; set; } = Color.FromArgb(60, 0, 0, 0); // semi-transparent black

        /// <summary>
        /// Custom paint logic for rounded card with shadow and border.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Define drawing bounds with shadow offset
            var rect = new Rectangle(
                ShadowSize,
                ShadowSize,
                Width - ShadowSize * 2,
                Height - ShadowSize * 2);

            using var path = RoundedRect(rect, CornerRadius);

            // Draw soft shadow using gradient brush
            using var shadowBrush = new PathGradientBrush(path)
            {
                CenterColor = ShadowColor,
                SurroundColors = new[] { Color.Transparent }
            };
            g.FillPath(shadowBrush, path);

            // Draw card fill and border
            using var brush = new SolidBrush(FillColor);
            using var pen = new Pen(BorderColor, BorderThickness);
            g.FillPath(brush, path);
            g.DrawPath(pen, path);
        }

        /// <summary>
        /// Creates a rounded rectangle path for the given bounds and radius.
        /// </summary>
        private GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90); // Top-left
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90); // Top-right
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90); // Bottom-right
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90); // Bottom-left
            path.CloseFigure();
            return path;
        }
    }
}
